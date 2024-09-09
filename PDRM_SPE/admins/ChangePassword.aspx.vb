Imports AppCode.BusinessLogic

Public Class AChangePassword
    Inherits ABase

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("ChangePassword")
        lblHeader.Text = GetText("LPassword")
        'Current login password
        lblCurrentLPassword.Text = GetText("CurrentLPassword")

        rfvCurrentLPassword.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("CurrentLPassword"))
        'New login password
        lblLPassword.Text = GetText("NewLPassword")

        rfvLPassword.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("NewLPassword"))
        revLPassword.ErrorMessage = GetText("ErrorPasswordLength")
        cvLPassword.ErrorMessage = GetText("ErrorNewCurrentPasswordCompare")
        'New confirm login password 
        lblLPasswordConfirm.Text = GetText("ConfirmLPassword")

        rfvLPasswordConfirm.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ConfirmLPassword"))
        cvLPasswordConfirm.ErrorMessage = GetText("ErrorConfirmPasswordCompare")
        'Buttons/Message
        'Security password popup
        btnChangeLPassword.Text = GetText("ChangePassword")
        hfTransactionConfirm.Value = GetText("MsgConfirm")
    End Sub
#End Region

#Region "Change Password"
    Protected Sub btnChangeLPassword_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If AdminAuthentication.ValidateCredential(CLng(AdminAuthentication.GetUserData(2)), txtCurrentLPassword.Text) Then
            If AdminManager.ChangeLoginPassword(CLng(AdminAuthentication.GetUserData(2)), txtLPassword.Text) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "CHANGE PASSWORD", "Changed admin " & AdminAuthentication.GetUserData(3) & " password", "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgChangePasswordSuccess") & "');window.location.href = '../admins/ChangePassword.aspx';", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorChangePasswordFailed") & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorIncorrectCurrentLPassword") & "');", True)
        End If
    End Sub
#End Region

End Class