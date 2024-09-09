Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAccessControl
    Inherits ABase

    Private Property CurRoleID() As Long
        Get
            If Not ViewState("CurRoleID") Is Nothing Then
                Return CLng(ViewState("CurRoleID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("CurRoleID") = value
        End Set
    End Property

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            GetRole()
            GenerateTree()
            GetRoleAccessibleList(ddlRole.SelectedValue)
        End If
    End Sub

#Region "Language"

    Private Sub SetText() 'Header/Title
        lblPageTitle.Text = GetText("AdminTypeAccessControl")
        lblHeader.Text = GetText("AdminTypeAccessControl")
        lblInfoHeader.Text = GetText("MenuList")
        'Fr MID
        lblRole.Text = GetText("RoleLevel")
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
                lblMainMenu.Text = gettext(lblMainMenu.Text)
                If rpSub.Items.Count > 0 Then
                    For j As Integer = 0 To rpSub.Items.Count - 1
                        Dim lblSubTitle As Label = CType(rpSub.Items(j).FindControl("lblSubTitle"), Label)
                        lblSubTitle.Text = gettext(lblSubTitle.Text)
                    Next
                End If
            Next
        End If
    End Sub


#End Region

#Region "Initialize"

    Private Sub GetRole()
        ddlRole.DataSource = RoleManager.GetRoleList("A")
        ddlRole.DataTextField = "fldRoleName"
        ddlRole.DataValueField = "fldRoleID"
        ddlRole.DataBind()
        ddlRole.SelectedIndex = 0
    End Sub

    Private Sub GenerateTree()
        'Dim adminMenu As DataTable = RoleManager.GetRoleMenu(roleID)
        Dim adminMenu As DataTable = MenuManager.GetAdminMenuList()
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

    Private Sub GetRoleAccessibleList(ByVal roleID As Long)
        CurRoleID = roleID
        Dim roleMenu As String() = RoleManager.GetRoleMenuList(roleID, "A")
        For i As Integer = 0 To rpMain.Items.Count - 1
            Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
            Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
            chkMain.Checked = roleMenu.Contains(hfMainID.Value)
            Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
            For j As Integer = 0 To rpSub.Items.Count - 1
                Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
                Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
                chkSub.Checked = roleMenu.Contains(hfSubID.Value)
            Next
        Next
    End Sub

    Protected Sub ddlRole_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetRoleAccessibleList(ddlRole.SelectedValue)
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        GetRoleAccessibleList(ddlRole.SelectedValue)
    End Sub

#End Region

#Region "validation"

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
        Dim menuList As List(Of String) = SetPermanentAccess(New List(Of String))
        For i As Integer = 0 To rpMain.Items.Count - 1
            Dim subMenuSelected As Integer = 0
            Dim rpSub As Repeater = rpMain.Items(i).FindControl("rpSub")
            Dim chkMain As CheckBox = rpMain.Items(i).FindControl("chkMain")
            Dim hfMainID As HiddenField = rpMain.Items(i).FindControl("hfMainID")
            If chkMain.Checked = True Then
                If Not menuList.Contains(hfMainID.Value) Then
                    menuList.Add(hfMainID.Value)
                End If
            End If
            For j As Integer = 0 To rpSub.Items.Count - 1
                Dim hfSubID As HiddenField = rpSub.Items(j).FindControl("hfSubID")
                Dim chkSub As CheckBox = rpSub.Items(j).FindControl("chkSub")
                If chkSub.Checked = True Then
                    If Not menuList.Contains(hfMainID.Value) Then
                        menuList.Add(hfMainID.Value)
                    End If
                    If Not menuList.Contains(hfSubID.Value) Then
                        menuList.Add(hfSubID.Value)
                    End If
                    subMenuSelected += 1
                Else
                    If menuList.Contains(hfSubID.Value) AndAlso chkMain.Checked = False Then
                        menuList.Remove(hfSubID.Value)
                    End If
                End If
            Next
            If subMenuSelected <= 0 And rpSub.Items.Count > 0 Then
                If menuList.Contains(hfMainID.Value) Then
                    menuList.Remove(hfMainID.Value)
                End If
            End If
        Next
        Dim result As Boolean = RoleManager.Save(CurRoleID, "A", menuList.ToArray)
        Return result
    End Function

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
        If UpdateAccess() Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE ADMIN TYPE ACL", "Admin Type: " & ddlRole.SelectedValue, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');window.location.href = '../admins/AccessControl.aspx'", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

#End Region

End Class