Imports AppCode.BusinessLogic

Public Class MBase
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Maintenance() Then
                Response.Redirect("~/UnderMaintenance.aspx")
            Else
                'UserActivated()
            End If
        End If
    End Sub

    Private Function Maintenance() As Boolean
        Return SettingManager.GetSettingValue("Maintenance") = 1
    End Function

    Public Overloads Sub UserIsAuthenticated()
        Dim result As Boolean = Page.Request.IsAuthenticated AndAlso MemberAuthentication.ValidateSession(CLng(MemberAuthentication.GetUserData(2)), CStr(MemberAuthentication.GetUserData(0)))
        If Not result Then
            MemberAuthentication.Logout(CLng(MemberAuthentication.GetUserData(2)), CStr(MemberAuthentication.GetUserData(0)))
            Response.Redirect("~/Login_m.aspx")
        End If
    End Sub

    Public Sub UserActivated()
        If Not Request.Url.AbsoluteUri.EndsWith("MActivateAccount.aspx") Then
            If Not MembershipManager.VerifyActivation(CLng(MemberAuthentication.GetUserData(2))) Then
                Response.Redirect("~/member/MActivateAccount.aspx")
            End If
        End If
    End Sub

End Class