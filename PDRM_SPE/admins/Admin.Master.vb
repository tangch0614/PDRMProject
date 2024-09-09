Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class Admin
    Inherits Base1

    Protected Overloads Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        InitPage()
    End Sub

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Authorization() Then
                SetText()
                GetAdmin()
                MenuBar()
            Else
                Response.Redirect("~/AccessDenied.aspx")
            End If
        End If
    End Sub

    Private Sub InitPage()
    End Sub

#Region "LANGUAGE"

    'Private Sub GenerateLanguageCookie(ByVal Language As String)
    '    Dim languageCookie As HttpCookie = Response.Cookies("LanguageCookie")
    '    If languageCookie Is Nothing Then
    '        languageCookie = New HttpCookie("LanguageCookie")
    '        languageCookie.Expires = DateTime.Now.AddDays(1)
    '    End If
    '    languageCookie.Value = Language
    'End Sub

    Protected Sub btnLanguage_Click(sender As Object, e As EventArgs)
        GenerateLanguageCookie(sender.CommandArgument)
        Response.Redirect(Request.Url.AbsoluteUri)
    End Sub

    Private Sub SetText()
        Dim datetime As DateTime = UtilityManager.GetServerDateTime
        Page.Header.Title = Application("Company")
        lblCompanyName.Text = GetText("ElectronicManagementSystem")
        lblLogout.Text = GetText("Logout")
        lblLogout2.Text = GetText("Logout")
        lblProfile.Text = GetText("Profile")
        lblProfile2.Text = GetText("Profile")
        lblFooter.Text = "" 'String.Format("Copyright &copy; {0} {1}. All Right Reserved", datetime.Year, Application("Company"))
    End Sub

#End Region

#Region "Admin"

    Private Sub GetAdmin()
        Dim admin As AdminObj = AdminManager.GetAdmin(CLng(AdminAuthentication.GetUserData(2)))
        Dim usertype As String = ""
        If admin.fldLevel = 0 Then
            usertype = GetText("System")
        Else
            usertype = GetText(RoleManager.GetRoleName(admin.fldLevel, "A"))
        End If
        lblUsername.Text = admin.fldCode
        lblUsername.Text = admin.fldCode
        lblUserType.Text = usertype
        lblUserType2.Text = usertype
        admin = Nothing
    End Sub

    Private Function Authorization() As Boolean
        Dim result As Boolean = False
        If Not Page.Request.Url.AbsolutePath.ToLower.EndsWith("AccessDenied.aspx") AndAlso Page.Request.Url.AbsolutePath.ToLower.StartsWith("/admins") Then
            result = RoleManager.Authorization(CLng(AdminAuthentication.GetUserData(5)), "A", ".." & Page.Request.Url.AbsolutePath) AndAlso UserMenuManager.Authorization(CLng(AdminAuthentication.GetUserData(2)), "A", ".." & Page.Request.Url.AbsolutePath)
        Else
            result = True
        End If
        Return result
    End Function

    Protected Sub lbtLogout_Click(sender As Object, e As EventArgs)
        'If Not Request.Cookies("User") Is Nothing Then
        'Dim cookie As HttpCookie = Request.Cookies("User")
        AdminAuthentication.Logout(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0)))
        Response.Redirect("~/Login.aspx")
        'End If
    End Sub

#End Region

#Region "Menu"

    Private Sub MenuBar()
        Dim role As Integer = CLng(AdminAuthentication.GetUserData(5))
        Dim menulist As DataTable = MenuManager.GetAdminMenuList(role)
        Dim restrictedlist As String() = UserMenuManager.GetRestrictedList(CLng(AdminAuthentication.GetUserData(2)), "A")
        If menulist.Rows.Count > 0 Then
            Dim mainMenuTable As DataTable = menulist.Clone
            Dim mainMenuRows() As DataRow = menulist.Select("fldParentMenuID = 0", "fldMenuOrder")
            If mainMenuRows.Length > 0 Then
                For i As Integer = 0 To mainMenuRows.Length - 1
                    If Not restrictedlist Is Nothing Then
                        If Not restrictedlist.Contains(mainMenuRows(i)("fldMenuID")) Then
                            mainMenuTable.ImportRow(mainMenuRows(i))
                        End If
                    Else
                        mainMenuTable.ImportRow(mainMenuRows(i))
                    End If
                Next
            End If
            rpMainMenu.DataSource = mainMenuTable
            rpMainMenu.DataBind()

            For i As Integer = 0 To rpMainMenu.Items.Count - 1
                Dim liMainMenu As Object = CType(rpMainMenu.Items(i).FindControl("liMainMenu"), Object)
                Dim rpSubMenu As Repeater = CType(rpMainMenu.Items(i).FindControl("rpSubMenu"), Repeater)
                Dim lblMenuID As Label = CType(rpMainMenu.Items(i).FindControl("lblMenuID"), Label)
                Dim lbtMainMenu As LinkButton = CType(rpMainMenu.Items(i).FindControl("lbtMainMenu"), LinkButton)
                Dim subMenuRows() As DataRow = menulist.Select("fldParentMenuID = " & lblMenuID.Text, "fldMenuOrder")
                If subMenuRows.Length > 0 Then
                    Dim lblIcon As Label = CType(rpMainMenu.Items(i).FindControl("lblIcon"), Label)
                    liMainMenu.Attributes("class") = "menu-dropdown classic-menu-dropdown"
                    lblIcon.Visible = True
                    lbtMainMenu.Attributes("onclick") = "return false;"
                    Dim subMenuTable As DataTable = menulist.Clone
                    For j As Integer = 0 To subMenuRows.Length - 1
                        If Not restrictedlist Is Nothing Then
                            If Not restrictedlist.Contains(subMenuRows(j)("fldMenuID")) Then
                                subMenuTable.ImportRow(subMenuRows(j))
                            End If
                        Else
                            subMenuTable.ImportRow(subMenuRows(j))
                        End If
                    Next
                    rpSubMenu.DataSource = subMenuTable
                    rpSubMenu.DataBind()
                Else
                    lbtMainMenu.Attributes("class") = "nav-link"
                End If
            Next
            BindActive()
        End If
    End Sub

    Private Sub BindActive()
        Dim splitPath As String() = HttpContext.Current.Request.Url.AbsolutePath.Split("/")
        Dim currentPath As String = splitPath(splitPath.Length - 1) 'get current path
        Dim subMenuPath As String = Nothing 'sub menu path
        Dim mainMenuPath As String = Nothing 'main menu path
        If rpMainMenu.Items.Count > 0 Then
            For i As Integer = 0 To rpMainMenu.Items.Count - 1
                Dim stopper As Boolean = False
                Dim liMainMenu As Object = CType(rpMainMenu.Items(i).FindControl("liMainMenu"), Object)
                Dim lbtMainMenu As LinkButton = CType(rpMainMenu.Items(i).FindControl("lbtMainMenu"), LinkButton)
                Dim rpSubMenu As Repeater = CType(rpMainMenu.Items(i).FindControl("rpSubMenu"), Repeater)
                If Not lbtMainMenu.Attributes("href").Equals("#") Then
                    splitPath = lbtMainMenu.Attributes("href").Split("/")
                    mainMenuPath = splitPath(splitPath.Length - 1)
                End If
                If rpSubMenu.Items.Count > 0 Then
                    For j As Integer = 0 To rpSubMenu.Items.Count - 1
                        Dim lbtSubMenu As LinkButton = CType(rpSubMenu.Items(j).FindControl("lbtSubMenu"), LinkButton)
                        Dim liSubMenu As Object = CType(rpSubMenu.Items(j).FindControl("liSubMenu"), Object)
                        splitPath = lbtSubMenu.Attributes("href").Split("/")
                        subMenuPath = splitPath(splitPath.Length - 1)
                        If Not mainMenuPath Is Nothing AndAlso subMenuPath.Equals(currentPath) Then
                            liMainMenu.Attributes("class") = "menu-dropdown classic-menu-dropdown active"
                            liSubMenu.Attributes("class") = "active"
                            lbtSubMenu.Attributes("class") = "nav-link active"
                            stopper = True
                            Exit For
                        Else
                            stopper = False
                        End If
                    Next
                    If stopper Then
                        Exit For
                    End If
                Else
                    If mainMenuPath.Equals(currentPath) Then
                        liMainMenu.Attributes("class") = "active"
                        Exit For
                    End If
                End If
            Next
        End If
    End Sub

#End Region

End Class