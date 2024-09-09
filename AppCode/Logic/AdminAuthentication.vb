Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Web.Security
Imports System.Web
Imports AppCode.BusinessObject
Imports AppCode.DataAccess
Imports System.Transactions

Namespace BusinessLogic
    Public Class AdminAuthentication

        Public Shared Function GetUserData(ByVal index As Integer) As Object
            Dim authCookie As HttpCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)
            If Not authCookie Is Nothing Then
                Dim userdata As String = FormsAuthentication.Decrypt(authCookie.Value).UserData()
                If Not String.IsNullOrEmpty(userdata) Then
                    Dim dataArr As String() = userdata.Split(",")
                    Return dataArr(index)
                End If
            End If
            Return Nothing
        End Function

        Public Shared Function GetAuthTicket() As FormsAuthenticationTicket
            Dim authCookie As HttpCookie = HttpContext.Current.Request.Cookies(FormsAuthentication.FormsCookieName)
            If Not authCookie Is Nothing Then
                Return FormsAuthentication.Decrypt(authCookie.Value)
            End If
            Return Nothing
        End Function

        Public Shared Function ValidateSecurityCredential(ByVal UserID As Long, ByVal SecurityPassword As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                result = AdminAuthenticationDB.ValidateSecurityCredential(UserID, SecurityPassword, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function ValidateCredential(ByVal LoginID As String, ByVal Password As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                result = AdminAuthenticationDB.ValidateCredential(LoginID, Password, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function ValidateCredential(ByVal UserID As Long, ByVal Password As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim loginID As String = AdminDB.GetAdminLoginID(UserID, myConnection)
                If Not loginID Is Nothing Then
                    result = AdminAuthenticationDB.ValidateCredential(loginID, Password, myConnection)
                End If
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function Login(ByVal LoginID As String, ByVal Password As String) As Integer
            'Dim hashedPassword As String = UtilityManager.EncryptData(Password)
            Dim result As Integer = 0
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim userID As Long = -1
                Dim myUser As AdminObj = AdminAuthenticationDB.GetLoginDetail(LoginID, myConnection)
                If Not myUser Is Nothing Then
                    userID = myUser.fldID
                    'If myUser.fldUserType.Equals(UserType) OrElse UserType = Nothing Then 'Autheticate login in by user type, hide this line if not using login by user type
                    If AdminAuthenticationDB.LoginAttempt(myUser.fldID, myUser.fldLoginAttempt, myUser.fldLastLoginAttempt, myConnection) Then
                        If AdminAuthenticationDB.ValidateCredential(myUser.fldID, LoginID, Password, myConnection) Then
                            Dim sessionID As String = UtilityManager.EncryptData(LoginID & UtilityManager.GenerateRandomNumber(6) & UtilityDB.GetServerDateTime(myConnection).ToString("ddMMyyyyHHmmss"))
                            If AdminAuthenticationDB.DuplicateLogin(myUser.fldID, sessionID, myConnection) Then
                                AdminAuthenticationDB.KickSession(myUser.fldID, myConnection)
                            End If
                            AdminAuthenticationDB.InsertSession(LoginID, myUser.fldID, "A", sessionID, UtilityManager.GetPublicIpAddress, myConnection)
                            myUser = AdminDB.GetAdmin(myUser.fldID, myConnection)
                            Dim userdata As String = ConcatString(",", sessionID, "A", myUser.fldID, myUser.fldCode, myUser.fldLanguage, myUser.fldLevel)
                            Dim ticket As FormsAuthenticationTicket = New FormsAuthenticationTicket(1, LoginID, DateTime.Now, DateTime.Now.AddMinutes(60), True, userdata)
                            Dim hashTicket As String = FormsAuthentication.Encrypt(ticket)
                            Dim ticketCookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, hashTicket)
                            'ticketCookie.Expires = DateTime.Now.AddMinutes(15)
                            HttpContext.Current.Response.Cookies.Add(ticketCookie)
                            'UserCookie(myUser.fldID, "A", myUser.fldLevel, myUser.fldLanguage, sessionID, myConnection)
                            result = 1
                        Else
                            result = -3 'Incorrect password
                        End If
                    Else
                        result = -2 'Login attempt limit
                    End If
                    'Else
                    'result = -1 User not found
                    'End If
                Else
                    result = -1 'User not found
                End If
                AdminAuthenticationDB.AuthenticationLog("LOGIN", userID, LoginID, Password, UtilityManager.GetPublicIpAddress, If(result = 1, "Y", "N"), myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Sub Logout(ByVal UserID As Long, ByVal SessionID As String)
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                AdminAuthenticationDB.RemoveSession(UserID, SessionID, myConnection)
                FormsAuthentication.SignOut()
                HttpContext.Current.Session.Abandon()
                Dim myCookies() As String = HttpContext.Current.Request.Cookies.AllKeys
                For Each cookie As String In myCookies
                    HttpContext.Current.Response.Cookies(cookie).Expires = DateTime.Now.AddDays(-1)
                Next
                AdminAuthenticationDB.AuthenticationLog("LOGOUT", UserID, "", "", UtilityManager.GetPublicIpAddress, "Y", myConnection)
                myConnection.Close()
            End Using
        End Sub

        Public Shared Function ValidateSession(ByVal UserID As Long, ByVal SessionID As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AdminAuthenticationDB.ValidateSession(UserID, SessionID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function KickSession(ByVal UserID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AdminAuthenticationDB.KickSession(UserID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Private Shared Function ConcatString(ByVal separator As Char, ByVal ParamArray values() As String) As String
            Dim str As String = ""
            For i As Integer = 0 To values.Length - 1
                str &= values(i)
                If Not i = values.Length - 1 Then
                    str &= separator
                End If
            Next
            Return str
        End Function

        'Public Shared Function IsAuthorized(ByVal UserID As Long, ByVal UserType As String) As Boolean
        '    Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
        '        Dim result As Boolean = False
        '        myConnection.Open()
        '        If UserDB.GetUser(UserID, myConnection).userType.Equals(UserType) Then
        '            result = True
        '        End If
        '        myConnection.Close()
        '        Return result
        '    End Using
        'End Function

        'Private Shared Sub UserCookie(ByVal UserID As Long, ByVal UserType As Char, ByVal UserLevel As Long, ByVal Language As String, ByVal SessionID As String, ByVal myConnection As MySqlConnection)
        '    Dim myUser As AdminObj = AdminDB.GetAdmin(UserID, myConnection)

        '    Dim myCookie As HttpCookie = New HttpCookie("User")
        '    myCookie.Values("UserID") = UserID
        '    myCookie.Values("UserType") = UserType
        '    myCookie.Values("UserLevel") = UserLevel
        '    myCookie.Values("Language") = Language
        '    myCookie.Values("SessionID") = SessionID
        '    myCookie.Expires = DateTime.Now.AddDays(1)
        '    HttpContext.Current.Response.Cookies.Add(myCookie)
        'End Sub

    End Class
End Namespace
