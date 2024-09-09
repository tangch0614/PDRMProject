Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAccessControl2
    Inherits ABase

    Private Property UserValid() As Boolean
        Get
            If Not ViewState("UserValid") Is Nothing Then
                Return CBool(ViewState("UserValid"))
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            ViewState("UserValid") = value
        End Set
    End Property
    Private Property UserID() As Long
        Get
            If Not ViewState("UserID") Is Nothing Then
                Return CLng(ViewState("UserID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("UserID") = value
        End Set
    End Property
    Private Property UserRole() As Long
        Get
            If Not ViewState("UserRole") Is Nothing Then
                Return CLng(ViewState("UserRole"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("UserRole") = value
        End Set
    End Property


    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
        End If
    End Sub

#Region "Language"

    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("AdminAccessControl")
        lblHeader.Text = GetText("AdminAccessControl")
        lblInfoHeader.Text = GetText("MenuList")
        'Fr MID
        lblUserID.Text = GetText("Username")

        btnView.Text = GetText("Search")
        'Button
        btnUpdate.Text = GetText("Update")
        btnReset.Text = GetText("Reset")
        hfConfirm.Value = GetText("MsgConfirm")
    End Sub

    Private Sub SetMenuText()
        If rpMain.Items.Count > 0 Then
            For i As Integer = 0 To rpMain.Items.Count - 1
                Dim rpSub As Repeater = CType(rpMain.Items(i).FindControl("rpSub"), Repeater)
                Dim lblMainMenu As Label = CType(rpMain.Items(i).FindControl("lblMainTitle"), Label)
                lblMainMenu.Text = GetText(lblMainMenu.Text)
                If rpSub.Items.Count > 0 Then
                    For j As Integer = 0 To rpSub.Items.Count - 1
                        Dim lblSubTitle As Label = CType(rpSub.Items(j).FindControl("lblSubTitle"), Label)
                        lblSubTitle.Text = GetText(lblSubTitle.Text)
                    Next
                End If
            Next
        End If
    End Sub


#End Region

#Region "Initialize"

    Private Sub GenerateTree(ByVal roleID As Long)
        rpMain.DataSource = ""
        rpMain.DataBind()
        Dim adminMenu As DataTable = MenuManager.GetAdminMenuList(roleID)
        If adminMenu.Rows.Count > 0 Then
            Dim parentRows() As DataRow = adminMenu.Select("fldParentMenuID = 0 And fldPermanent <> 1", "fldMenuOrder")
            If parentRows.Length > 0 Then
                Dim parentTable As DataTable = adminMenu.Clone
                For i As Integer = 0 To parentRows.Length - 1
                    parentTable.ImportRow(parentRows(i))
                Next
                rpMain.DataSource = parentTable
                rpMain.DataBind()
            End If
            For i As Integer = 0 To rpMain.Items.Count - 1
                Dim lbtExpand As Object = CType(rpMain.Items(i).FindControl("lbtExpand"), Object)
                Dim liMain As Object = CType(rpMain.Items(i).FindControl("liMain"), Object)
                Dim hfMainID As HiddenField = CType(rpMain.Items(i).FindControl("hfMainID"), HiddenField)
                Dim chkMain As CheckBox = CType(rpMain.Items(i).FindControl("chkMain"), CheckBox)
                liMain.Attributes("class") = "parent"
                Dim rpSub As Repeater = CType(rpMain.Items(i).FindControl("rpSub"), Repeater)
                Dim childRows() As DataRow = adminMenu.Select("fldParentMenuID = " & hfMainID.Value & "And fldPermanent <> 1", "fldMenuOrder")
                If childRows.Length > 0 Then
                    chkMain.Visible = False
                    Dim childTable As DataTable = adminMenu.Clone
                    For j As Integer = 0 To childRows.Length - 1
                        childTable.ImportRow(childRows(j))
                    Next
                    rpSub.DataSource = childTable
                    rpSub.DataBind()
                Else
                    chkMain.Visible = True
                    lbtExpand.Attributes("class") = "fa fa-minus-square fw"
                End If
            Next
            SetMenuText()
        End If
    End Sub

    Private Sub GetUserAccessibleList(ByVal userID As Long)
        Dim restrictedMenu As String() = UserMenuManager.GetRestrictedList(userID, "A")
        If Not restrictedMenu Is Nothing AndAlso restrictedMenu.Count > 0 Then
            For i As Integer = 0 To rpMain.Items.Count - 1
                Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
                Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
                chkMain.Checked = Not restrictedMenu.Contains(hfMainID.Value)
                Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
                For j As Integer = 0 To rpSub.Items.Count - 1
                    Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
                    Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
                    chkSub.Checked = Not restrictedMenu.Contains(hfSubID.Value)
                Next
            Next
        Else
            For i As Integer = 0 To rpMain.Items.Count - 1
                Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
                Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
                chkMain.Checked = True
                Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
                For j As Integer = 0 To rpSub.Items.Count - 1
                    Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
                    Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
                    chkSub.Checked = True
                Next
            Next
        End If
    End Sub

    'Private Sub GetUserAccessibleList(ByVal userID As Long)
    '    Dim userMenu As String() = UserMenuManager.GetPermittedList(userID, "A")
    '    If Not userMenu Is Nothing AndAlso userMenu.Count > 0 Then
    '        For i As Integer = 0 To rpMain.Items.Count - 1
    '            Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
    '            Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
    '            chkMain.Checked = userMenu.Contains(hfMainID.Value)
    '            Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
    '            For j As Integer = 0 To rpSub.Items.Count - 1
    '                Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
    '                Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
    '                chkSub.Checked = userMenu.Contains(hfSubID.Value)
    '            Next
    '        Next
    '    Else
    '        For i As Integer = 0 To rpMain.Items.Count - 1
    '            Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
    '            Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
    '            chkMain.Checked = True
    '            Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
    '            For j As Integer = 0 To rpSub.Items.Count - 1
    '                Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
    '                Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
    '                chkSub.Checked = True
    '            Next
    '        Next
    '    End If
    'End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        GetUserAccessibleList(UserID)
    End Sub

#End Region

#Region "validation"

    Protected Sub txtUserID_TextChanged(sender As Object, e As EventArgs)
        dvInfo.Visible = False
        UserID = 0
        lblUserValidate.Visible = False
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs)
        dvInfo.Visible = False
        UserID = 0
        lblUserValidate.Visible = False
        If Not String.IsNullOrEmpty(txtUserID.Text) Then
            UserID = AdminManager.VerifyAdminCode(txtUserID.Text)
            UserValid = UserID > 0
            lblUserValidate.Visible = True
            If UserValid Then
                Dim User As AdminObj = AdminManager.GetAdmin(UserID)
                If Not User Is Nothing Then
                    lblUserValidate.Text = GetText("MsgValidUserID")
                    lblUserValidate.ForeColor = Drawing.Color.Blue
                    GenerateTree(User.fldLevel)
                    GetUserAccessibleList(UserID)
                    dvInfo.Visible = True
                Else
                    lblUserValidate.Text = GetText("ErrorUserIDNotFound")
                    lblUserValidate.ForeColor = Drawing.Color.Red
                End If
            Else
                lblUserValidate.Text = GetText("ErrorUserIDNotFound")
                lblUserValidate.ForeColor = Drawing.Color.Red
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & lblUserValidate.Text & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Window", "alert('" & GetText("ErrorItemRequired").Replace("vITEM", GetText("Username")) & "');", True)
        End If
    End Sub

    'Protected Sub chkMain_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim itemIndex As Integer = DirectCast(DirectCast(sender, CheckBox).NamingContainer, RepeaterItem).ItemIndex
    '    Dim chkMain As CheckBox = rpMain.Items(itemIndex).FindControl("chkMain")
    '    Dim rpSub As Repeater = rpMain.Items(itemIndex).FindControl("rpSub")
    '    For i As Integer = 0 To rpSub.Items.Count - 1
    '        Dim chkSub As CheckBox = rpSub.Items(i).FindControl("chkSub")
    '        chkSub.Checked = chkMain.Checked
    '    Next
    'End Sub

    'Protected Sub chkSub_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim subItemIndex As Integer = DirectCast(DirectCast(sender, CheckBox).NamingContainer, RepeaterItem).ItemIndex
    '    Dim subItem As Repeater = DirectCast(DirectCast(sender, CheckBox).NamingContainer.BindingContainer, Repeater)
    '    Dim mainItemIndex As Integer = DirectCast(subItem.NamingContainer, RepeaterItem).ItemIndex
    '    Dim rpSub As Repeater = rpMain.Items(mainItemIndex).FindControl("rpSub")
    '    Dim chkMain As CheckBox = rpMain.Items(mainItemIndex).FindControl("chkMain")
    '    Dim chkSub As CheckBox = rpSub.Items(subItemIndex).FindControl("chkSub")
    '    If chkSub.Checked = True Then
    '        If chkMain.Checked = False Then chkMain.Checked = True
    '    Else
    '        For i As Integer = 0 To rpSub.Items.Count - 1
    '            Dim chk As CheckBox = rpSub.Items(i).FindControl("chkSub")
    '            If chk.Checked = True Then
    '                Exit For
    '            End If
    '            If i = rpSub.Items.Count - 1 Then
    '                chkMain.Checked = chk.Checked
    '            End If
    '        Next
    '    End If
    'End Sub

#End Region

#Region "Update"

    Private Function UpdateAccess() As Boolean
        Dim restrictList As List(Of String) = New List(Of String)
        For i As Integer = 0 To rpMain.Items.Count - 1
            Dim subMenuSelected As Integer = 0
            Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
            Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
            Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
            If chkMain.Checked = True Then
                restrictList.Remove(hfMainID.Value)
            Else
                If Not restrictList.Contains(hfMainID.Value) Then
                    restrictList.Add(hfMainID.Value)
                End If
            End If
            For j As Integer = 0 To rpSub.Items.Count - 1
                Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
                Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
                If chkSub.Checked = True Then
                    restrictList.Remove(hfMainID.Value)
                    restrictList.Remove(hfSubID.Value)
                    subMenuSelected += 1
                Else
                    If Not restrictList.Contains(hfSubID.Value) Then
                        restrictList.Add(hfSubID.Value)
                    End If
                End If
            Next
            If subMenuSelected <= 0 And rpSub.Items.Count > 0 Then
                If Not restrictList.Contains(hfMainID.Value) Then
                    restrictList.Add(hfMainID.Value)
                End If
            End If
        Next
        Dim result As Boolean = UserMenuManager.SaveRestrict(UserID, "A", restrictList.ToArray)
        Return result
    End Function

    'Private Function UpdateAccess() As Boolean
    '    Dim menuList As List(Of String) = SetPermanentAccess(New List(Of String))
    '    Dim subMenuUnselected As Integer = 0
    '    Dim mainMenuUnselected As Integer = 0
    '    For i As Integer = 0 To rpMain.Items.Count - 1
    '        Dim subMenuSelected As Integer = 0
    '        Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
    '        Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
    '        Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
    '        If chkMain.Checked = True Then
    '            If Not menuList.Contains(hfMainID.Value) Then
    '                menuList.Add(hfMainID.Value)
    '            End If
    '        Else
    '            mainMenuUnselected += 1
    '        End If
    '        For j As Integer = 0 To rpSub.Items.Count - 1
    '            Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
    '            Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
    '            If chkSub.Checked = True Then
    '                If Not menuList.Contains(hfMainID.Value) Then
    '                    menuList.Add(hfMainID.Value)
    '                End If
    '                If Not menuList.Contains(hfSubID.Value) Then
    '                    menuList.Add(hfSubID.Value)
    '                End If
    '                subMenuSelected += 1
    '            Else
    '                If menuList.Contains(hfSubID.Value) Then
    '                    menuList.Remove(hfSubID.Value)
    '                End If
    '                subMenuUnselected += 1
    '            End If
    '        Next
    '        If subMenuSelected <= 0 Then
    '            If menuList.Contains(hfMainID.Value) Then
    '                menuList.Remove(hfMainID.Value)
    '            End If
    '        End If
    '    Next
    '    If subMenuUnselected = 0 AndAlso mainMenuUnselected = 0 Then
    '        menuList.Clear()
    '    End If
    '    Dim result As Boolean = UserMenuManager.SavePermit(UserID, "A", menuList.ToArray)
    '    Return result
    'End Function

    Private Function SetPermanentAccess(ByVal menuList As List(Of String)) As List(Of String)
        Dim datatable As DataTable = MenuManager.GetPermanentMenuList("A")
        If Not datatable Is Nothing AndAlso datatable.Rows.Count > 0 Then '
            For i As Integer = 0 To datatable.Rows.Count - 1
                menuList.Add(datatable.Rows(i)("fldMenuID"))
            Next
        End If
        Return menuList
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If UserValid AndAlso UpdateAccess() Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE ADMIN USER ACL", "Admin ID: " & UserID, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetUserAccessibleList(UserID)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

#End Region

End Class