Imports System.Web.SessionState
Imports AppCode.BusinessLogic

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        Application("Company") = SettingManager.GetSettingValue("CompanyShortName")
        Application("DefaultLanguage") = SettingManager.GetSettingValue("DefaultLanguage")
        Application("DefaultCountry") = SettingManager.GetSettingValue("DefaultCountry")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
        'look if any security information exists for this request
        If HttpContext.Current.User IsNot Nothing Then
            'see if this user is authenticated, any authenticated cookie (ticket) exists for this user
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                'see if the authentication is done using FormsAuthentication
                If TypeOf (HttpContext.Current.User.Identity) Is FormsIdentity Then
                    ' Get the roles stored for this request from the ticket
                    ' get the identity of the user
                    Dim identity As FormsIdentity = CType(HttpContext.Current.User.Identity, FormsIdentity)
                    ' get the forms authetication ticket of the user
                    Dim ticket As FormsAuthenticationTicket = identity.Ticket
                    If Not ticket Is Nothing Then
                        Dim userdata() As String = ticket.UserData.Split(",")
                        Dim result As Boolean = False
                        If userdata(1).Equals("M") Then
                            result = MemberAuthentication.ValidateSession(CLng(userdata(2)), CStr(userdata(0)))
                        ElseIf userdata(1).Equals("A") Then
                            result = AdminAuthentication.ValidateSession(CLng(userdata(2)), CStr(userdata(0)))
                        End If
                        If result Then
                            Dim roles As String() = {userdata(1)}
                            ' create generic principal and assign it to the current request
                            HttpContext.Current.User = New System.Security.Principal.GenericPrincipal(identity, roles)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class