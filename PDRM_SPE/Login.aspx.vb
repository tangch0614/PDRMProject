Imports AppCode.BusinessLogic

Public Class Login
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Page.Request.IsAuthenticated AndAlso AdminAuthentication.ValidateSession(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0))) Then
                If User.IsInRole("A") Then
                    Response.Redirect("~/admins/Home.aspx")
                End If
            End If
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
        Page.Header.Title = Application("Company")
        lblLanguage.Text = GetText("Language")
        lblLoginTitle.Text = GetText("ElectronicManagementSystem")
        lblLoginWelcome.Text = GetText("Welcome")
        lblLoginCaption.Text = GetText("PleaseLogin")
        lblLoginID.Text = GetText("Username")
        lblPassword.Text = GetText("Password")
        rfvLoginID.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Username"))
        rfvPassword.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Password"))
        txtLoginID.Attributes("placeholder") = GetText("EnterItem").Replace("vITEM", GetText("Username"))
        txtPassword.Attributes("placeholder") = GetText("EnterItem").Replace("vITEM", GetText("Password"))
        btnLogin.Text = GetText("Login")
        lblFooter.Text = "" 'String.Format("Copyright &copy; {0} {1}. All Right Reserved", datetime.Year, Application("Company"))
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

#Region "AUTHENTICATION"

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim result As Long = AdminAuthentication.Login(txtLoginID.Text, txtPassword.Text)
        Dim returnUrl As String = Nothing
        If result >= 0 Then
            'returnUrl = Request.QueryString("ReturnUrl")
            'If returnUrl = Nothing Then returnUrl = Request.Url.AbsoluteUri
            If returnUrl = Nothing Then returnUrl = "~/admins/Home.aspx"
            Response.Redirect(returnUrl)
        ElseIf result = -1 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Window", "alert('" & GetText("ErrorNoCredentialMatch") & "');", True)
        ElseIf result = -2 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Window", "alert('" & GetText("ErrorLoginAttemptExceeded").Replace("vTIME", SettingManager.GetSettingValue("LoginLockMinutes")) & "');", True)
        ElseIf result = -3 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Window", "alert('" & GetText("ErrorNoCredentialMatch2") & "');", True)
        ElseIf result = -4 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Window", "alert('" & GetText("ErrorDuplicateLogin") & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Window", "alert('" & GetText("ErrorOccur") & "');", True)
        End If
    End Sub

#End Region

#Region "Validation"

#End Region

End Class