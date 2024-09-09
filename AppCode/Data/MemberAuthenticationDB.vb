Imports MySql.Data.MySqlClient
Imports System.Data
Imports AppCode.BusinessObject
Imports AppCode.BusinessLogic
Imports System.Web

Namespace DataAccess

    Public Class MemberAuthenticationDB

        Public Shared Function AuthenticationLog(type As String, userID As Long, loginID As String, password As String, ipaddress As String, status As String, myconnection As MySqlConnection) As Boolean
            Dim result As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("Insert into tbllogin_log (fldDateTime, fldType, fldUserID, fldUserType, fldLoginID, fldPassword, fldIPAddress, fldStatus) Values (Current_Timestamp, @fldType, @fldUserID, 'M', @fldLoginID, @fldPassword, @fldIPAddress, @fldStatus)", myconnection)
            myCommand.Parameters.AddWithValue("@fldType", type)
            myCommand.Parameters.AddWithValue("@fldUserID", userID)
            myCommand.Parameters.AddWithValue("@fldLoginID", loginID)
            myCommand.Parameters.AddWithValue("@fldPassword", password)
            myCommand.Parameters.AddWithValue("@fldIPAddress", ipaddress)
            myCommand.Parameters.AddWithValue("@fldStatus", status)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function ResetPassword(ByVal newPassword As String, ByVal MemberCode As String, ByVal Email As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("UPDATE tblmembership SET fldPassword = @password, fldTransactionPassword = @password WHERE fldCode = @fldCode And fldEmail = @fldEmail", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@password", newPassword)
            myCommand.Parameters.AddWithValue("@fldCode", MemberCode)
            myCommand.Parameters.AddWithValue("@fldEmail", Email)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function VerifyEmail(ByVal MemberCode As String, ByVal Email As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim isValid As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM tblmembership WHERE fldCode = @fldCode And fldEmail = @fldEmail", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", MemberCode)
            myCommand.Parameters.AddWithValue("@fldEmail", Email)
            Return myCommand.ExecuteScalar > 0
        End Function

        Public Shared Function ValidateSecurityCredential(ByVal UserID As Long, ByVal SecurityPassword As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim isValid As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM tblmembership WHERE fldID = @fldID And fldTransactionPassword LIKE BINARY @fldTransactionPassword", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", UserID)
            myCommand.Parameters.AddWithValue("@fldTransactionPassword", SecurityPassword)
            Return myCommand.ExecuteScalar > 0
        End Function

        Public Shared Function ValidateSecurityCredential(ByVal MemberCode As String, ByVal SecurityPassword As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim isValid As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM tblmembership WHERE fldCode = @fldCode And fldTransactionPassword LIKE BINARY @fldTransactionPassword", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", MemberCode)
            myCommand.Parameters.AddWithValue("@fldTransactionPassword", SecurityPassword)
            Return myCommand.ExecuteScalar > 0
        End Function

        Public Shared Function GetLoginDetail(ByVal LoginID As String, ByVal myConnection As MySqlConnection) As MembershipObj
            Dim myUser As MembershipObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT fldID, fldLoginAttempt, fldLastLoginAttempt, fldLastLogin, fldLanguage FROM tblmembership WHERE fldCode = @fldCode And fldStatus = 'A'", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", LoginID)
            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myUser = New MembershipObj()
                    myUser.fldID = myReader.GetInt64("fldID")
                    myUser.fldLoginAttempt = myReader.GetInt32("fldLoginAttempt")
                    myUser.fldLastLoginAttempt = myReader.GetDateTime("fldLastLoginAttempt")
                    myUser.fldLastLogin = myReader.GetDateTime("fldLastLogin")
                    myUser.fldLanguage = myReader.GetString("fldLanguage")
                End If
                myReader.Close()
            End Using
            Return myUser
        End Function

        Public Shared Function LoginAttempt(ByVal UserID As Long, ByVal Attempts As Integer, ByVal LastLoginAttempt As DateTime, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Boolean = True
            Dim serverDateTime As DateTime = UtilityDB.GetServerDateTime(myConnection)
            Dim lockTime As Integer = SettingDB.GetSettingValue("LoginLockMinutes", myConnection)
            Dim attemptLimit As Integer = SettingDB.GetSettingValue("LoginAttemptsLimit", myConnection)
            If Attempts >= attemptLimit Then
                If (serverDateTime - LastLoginAttempt).TotalMinutes < lockTime Then
                    result = False
                Else
                    ResetAttempt(UserID, myConnection)
                End If
            Else
                If (serverDateTime - LastLoginAttempt).TotalMinutes >= lockTime Then
                    ResetAttempt(UserID, myConnection)
                End If
            End If
            Return result
        End Function

        Public Shared Function ValidateCredential(ByVal UserID As Long, ByVal LoginID As String, ByVal Password As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim isValid As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM tblmembership WHERE fldCode = @fldCode And fldPassword LIKE BINARY @fldPassword", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", LoginID)
            myCommand.Parameters.AddWithValue("@fldPassword", Password)
            If myCommand.ExecuteScalar > 0 Then
                isValid = True
                UpdateAttempt(UserID, True, myConnection)
            Else
                UpdateAttempt(UserID, False, myConnection)
            End If
            Return isValid
        End Function

        Public Shared Function ValidateCredential(ByVal LoginID As String, ByVal Password As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim isValid As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM tblmembership WHERE fldCode = @fldCode And fldPassword LIKE BINARY @fldPassword", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", LoginID)
            myCommand.Parameters.AddWithValue("@fldPassword", Password)
            Return myCommand.ExecuteScalar > 0
        End Function

        Public Shared Function DuplicateLogin(ByVal UserID As Long, ByVal SessionID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldSessionID From tblsession Where fldUserID = @fldUserID", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserID", UserID)
            If Not myCommand.ExecuteScalar = Nothing Then
                If Not myCommand.ExecuteScalar.Equals(SessionID) Then
                    result = True
                End If
            End If
            Return result
        End Function

        Public Shared Function ValidateSession(ByVal UserID As Long, ByVal SessionID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select count(*) From tblsession Where fldUserID = @fldUserID And fldSessionID = @fldSessionID ", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserID", UserID)
            myCommand.Parameters.AddWithValue("@fldSessionID", SessionID)
            result = myCommand.ExecuteScalar
            Return result > 0
        End Function

        Public Shared Function InsertSession(ByVal LoginID As String, ByVal UserID As Long, ByVal UserType As String, ByVal SessionID As String, ByVal IPAddress As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim serverDateTime As DateTime = UtilityDB.GetServerDateTime(myConnection)
            Dim myCommand As MySqlCommand = New MySqlCommand("Insert Into tblsession (fldSessionID, fldUserID, fldUserType, fldLoginDateTime, fldUserCode, fldIPAddress) Values (@fldSessionID, @fldUserID, @fldUserType, @fldLoginDateTime, @fldUserCode, @fldIPAddress)", myConnection)
            myCommand.Parameters.AddWithValue("@fldSessionID", SessionID)
            myCommand.Parameters.AddWithValue("@fldUserID", UserID)
            myCommand.Parameters.AddWithValue("@fldUserType", UserType)
            myCommand.Parameters.AddWithValue("@fldLoginDateTime", serverDateTime)
            myCommand.Parameters.AddWithValue("@fldUserCode", LoginID)
            myCommand.Parameters.AddWithValue("@fldIPAddress", IPAddress)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function RemoveSession(ByVal UserID As Long, ByVal SessionID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblsession Where fldSessionID = @fldSessionID AND fldUserID = @fldUserID", myConnection)
            myCommand.Parameters.AddWithValue("@fldSessionID", SessionID)
            myCommand.Parameters.AddWithValue("@fldUserID", UserID)
            result = myCommand.ExecuteNonQuery()
            If result > 0 Then
                myCommand = New MySqlCommand("Update tblmembership Set fldLastLogOut = @fldLastLogOut Where fldID = @fldID", myConnection)
                myCommand.Parameters.AddWithValue("@fldLastLogOut", UtilityDB.GetServerDateTime(myConnection))
                myCommand.Parameters.AddWithValue("@fldID", UserID)
                result = myCommand.ExecuteNonQuery()
                ResetAttempt(UserID, myConnection)
            End If
            Return result > 0
        End Function

        Public Shared Function KickSession(ByVal UserID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete from tblsession Where fldUserID = @fldUserID", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserID", UserID)
            result = myCommand.ExecuteNonQuery()
            ResetAttempt(UserID, myConnection)
            Return result > 0
        End Function

        Private Shared Sub UpdateAttempt(ByVal UserID As Long, ByVal ResetAttempt As Boolean, ByVal myConnection As MySqlConnection)
            Dim myCommand As MySqlCommand = Nothing
            Dim serverDateTime As DateTime = UtilityDB.GetServerDateTime(myConnection)
            If ResetAttempt Then
                myCommand = New MySqlCommand("Update tblmembership Set fldLoginAttempt = 0, fldLastLogin = @fldLastLogin, fldLastLoginAttempt = @fldLastLoginAttempt WHERE fldID = @fldID", myConnection)
                myCommand.Parameters.AddWithValue("@fldID", UserID)
                myCommand.Parameters.AddWithValue("@fldLastLogin", serverDateTime)
                myCommand.Parameters.AddWithValue("@fldLastLoginAttempt", serverDateTime)
                myCommand.ExecuteNonQuery()
            Else
                myCommand = New MySqlCommand("Update tblmembership Set fldLoginAttempt = fldLoginAttempt + 1, fldLastLoginAttempt = @fldLastLoginAttempt WHERE fldID = @fldID", myConnection)
                myCommand.Parameters.AddWithValue("@fldID", UserID)
                myCommand.Parameters.AddWithValue("@fldLastLoginAttempt", serverDateTime)
                myCommand.ExecuteNonQuery()
            End If
        End Sub

        Private Shared Sub ResetAttempt(ByVal UserID As Long, ByVal myConnection As MySqlConnection)
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblmembership Set fldLoginAttempt = 0 WHERE fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", UserID)
            myCommand.ExecuteNonQuery()
        End Sub

    End Class
End Namespace
