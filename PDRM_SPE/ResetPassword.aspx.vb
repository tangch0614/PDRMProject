Imports AppCode.BusinessLogic
Imports System.IO
Imports AppCode.BusinessObject

Public Class ResetPassword
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetLanguage()
            SetText()
        End If
    End Sub

#Region "LANGUAGE"

    Private Sub GenerateLanguageCookie(ByVal Language As String)
        Dim languageCookie As HttpCookie = Response.Cookies("LanguageCookie")
        If languageCookie Is Nothing Then
            languageCookie = New HttpCookie("LanguageCookie")
            languageCookie.Expires = DateTime.Now.AddDays(1)
        End If
        languageCookie.Value = Language
    End Sub

    Private Sub SetText()
        Dim datetime As DateTime = UtilityManager.GetServerDateTime
        Page.Header.Title = "Member | " & Application("Company")
        lblLanguage.Text = GetText("Language")
        lblPageTitle.Text = GetText("PasswordRecovery")
        lblLoginID.Text = GetText("LoginID")
        lblEmail.Text = GetText("Email")
        lblCaptcha.Text = GetText("CaptchaCode")
        rfvLoginID.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("LoginID"))
        rfvEmail.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Email"))
        rfvCaptcha.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("CaptchaCode"))
        revEmail.ErrorMessage = GetText("ErrorInvalidEmail")
        txtLoginID.Attributes("placeholder") = GetText("LoginID")
        txtEmail.Attributes("placeholder") = GetText("Email")
        txtCaptcha.Attributes("placeholder") = GetText("Captcha")
        btnReset.Text = GetText("ResetPassword")
        btnCancel.Text = GetText("Cancel")
        hfConfirm.Value = GetText("MsgConfirm")
        lblFooter.Text = String.Format("Copyright &copy; {0} {1}. All Right Reserved", datetime.Year, Application("Company"))
        'email template
    End Sub

    Protected Sub ddlLanguage_SelectedIndexChanged(sender As Object, e As EventArgs)
        GenerateLanguageCookie(ddlLanguage.SelectedValue)
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

#End Region

#Region "INITIALIZE"

    Private Sub GetLanguage()
        ddlLanguage.DataSource = UtilityManager.GetLanguageList()
        ddlLanguage.DataTextField = "fldName"
        ddlLanguage.DataValueField = "fldCode"
        ddlLanguage.DataBind()
        ddlLanguage.SelectedValue = Request.Cookies("LanguageCookie").Value
    End Sub

#End Region

#Region "Validation"

    Private Function VerifyEmail() As Boolean
        If MemberAuthentication.VerifyEmail(txtLoginID.Text, txtEmail.Text) Then
            Return True
        Else
            RefreshCaptcha()
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & GetText("ErrorIncorrectEmail") & "');", True)
            Return False
        End If
    End Function

    Private Sub RefreshCaptcha()
        txtCaptcha.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "captcha", "RefreshImage('imgCaptcha','txtCaptcha');", True)
    End Sub

    Private Function CaptchaCodeValid() As Boolean
        Dim captcha As String = CStr(Session("CaptchaImageText"))
        If Session("CaptchaImageText") Is Nothing OrElse String.IsNullOrEmpty(captcha) Then
            lblCaptchaValidate.Text = GetText("ErrorCaptchaExpired")
            lblCaptchaValidate.ForeColor = Drawing.Color.Red
            lblCaptchaValidate.Visible = True
            Return False
        Else
            If txtCaptcha.Text.ToLower = captcha.ToLower Then
                lblCaptchaValidate.Visible = False
                Return True
            Else
                lblCaptchaValidate.Text = GetText("ErrorCaptchaIncorrect")
                lblCaptchaValidate.ForeColor = Drawing.Color.Red
                lblCaptchaValidate.Visible = True
                Return False
            End If
            '//IMPORTANT: You must remove session value for security after the CAPTCHA test//
            Session.Remove("CaptchaImageText")
        End If
    End Function

#End Region

#Region "Action"

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        If CaptchaCodeValid() AndAlso VerifyEmail() AndAlso ResetPassword() Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & GetText("MsgResetPasswordSuccess") & "');window.location.href = '../Login_m.aspx'", True)
        Else
            RefreshCaptcha()
        End If
    End Sub

    Private Function ResetPassword() As Boolean
        Dim newPassword As String = UtilityManager.GeneratePassword(6)
        Dim member As MembershipObj = MembershipManager.GetMember(txtLoginID.Text)
        Dim txtMailBody As String = Nothing
        If MemberAuthentication.ResetPassword_BulkEmail(newPassword, txtLoginID.Text, txtEmail.Text, GetText("MailResetPasswordSubject").Replace("vCOMPANY", Application("Company")), _
                                              GetText("MailResetPasswordBodyHTML").Replace("vLOGO", Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath & "/assets/img/companylogo.png").Replace("vCOMPANY", Application("Company")).Replace("vNAME", member.fldName).Replace("vDATETIME", UtilityManager.GetServerDateTime.ToString("yyyy-MM-dd HH:mm:sstt")).Replace("vPASSWORD", newPassword)) Then
            Return True
        Else
            txtCaptcha.Text = ""
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & GetText("ErrorResetPasswordFailed") & "');", True)
            Return False
        End If
    End Function

    'Private Function ResetPassword() As Boolean
    '    Dim newPassword As String = UtilityManager.GeneratePassword(6)
    '    Dim member As MembershipObj = MembershipManager.GetMember(txtLoginID.Text)
    '    Dim txtMailBody As String = Nothing
    '    If MemberAuthentication.ResetPassword(newPassword, txtLoginID.Text, txtEmail.Text, GetText("MailResetPasswordSubject").Replace("vCOMPANY", Application("Company")), PopulateMailBody(UtilityManager.SecureString(member.fldCode), member.fldName, UtilityManager.GetServerDateTime, newPassword)) Then
    '        Return True
    '    Else
    '        txtCaptcha.Text = ""
    '        ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & GetText("ErrorResetPasswordFailed") & "');", True)
    '        Return False
    '    End If
    'End Function

    Private Function PopulateMailBody(ByVal memberCode As String, ByVal memberName As String, ByVal dateTime As String, ByVal password As String) As String
        Dim body As String = String.Empty
        Dim company As String = Application("CompanyName")
        Dim address As String = ""
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~/EmailTemplate2.html"))
        body = reader.ReadToEnd
        body = body.Replace("{txtContent}", GetText("MailResetPasswordBody").Replace("vNAME", memberName).Replace("vMID", memberCode).Replace("vDATETIME", dateTime).Replace("vPASSWORD", password).Replace("vCOMPANY", company))
        body = body.Replace("{txtFootNote}", GetText("MailFootNote").Replace("vCOMPANY", company))
        body = body.Replace("{imgLogo}", Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath & "assets/img/logo.png")
        Return body
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Login_m.aspx")
    End Sub

#End Region

End Class