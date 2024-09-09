Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AActsSectionList
    Inherits ABase

    Private Property DeviceID() As Long
        Get
            If Not ViewState("DeviceID") Is Nothing Then
                Return CLng(ViewState("DeviceID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("DeviceID") = value
        End Set
    End Property

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()

        'Header/Title
        lblPageTitle.Text = GetText("Section")
        lblHeader.Text = GetText("Section")
        lblInfo.Text = GetText("Maklumat Seksyen")
        'Search
        lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'details
        lblActs.Text = GetText("Acts")
        rfvActs.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Acts"))
        lblName.Text = GetText("Section")
        rfvName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Section"))
        lblDesc.Text = GetText("Description")
        rfvDesc.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Description"))
        lblStatus.Text = GetText("Status")
        'Buttons/Message
        btnAdd.Text = GetText("Add")
        btnBack.Text = GetText("Back")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetStatus()
        GetActs()
        BindTable()
    End Sub

    Private Sub GetStatus()
        ddlStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        ddlStatus.SelectedIndex = 0

        ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        ddlSStatus.SelectedIndex = 0
    End Sub

    Private Sub GetActs()
        ddlActs.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Acts")), 0, True))
        ddlActs.SelectedIndex = 0
    End Sub

    Private Sub GetUserData(ByVal admin As AdminObj)
        'txtImei.Text = admin.fldCode
        'txtContactNo.Text = admin.fldName
        ddlStatus.SelectedValue = admin.fldStatus
    End Sub
#End Region

#Region "Table binding"

    Private Sub BindTable()
        'Dim myDataTable As DataTable = AdminManager.SearchAdminList(0, ddlSLevel.SelectedValue, 0, txtSUserID.Text, txtSName.Text, "", "", ddlSStatus.SelectedValue, "", "")
        'If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
        '    rptTable.DataSource = myDataTable
        '    rptTable.DataBind()
        'Else
        rptTable.DataSource = ""
        rptTable.DataBind()
        'End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("editUser") Then
            DeviceID = e.CommandArgument
            plTable.Visible = False
            plUpdate.Visible = True
            Dim admin As AdminObj = AdminManager.GetAdmin(DeviceID)
            If Not admin Is Nothing Then
                GetUserData(admin)
            End If
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

#End Region

#Region "Search"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        'If Not String.IsNullOrEmpty(txtSUserID.Text & txtSName.Text & txtSIC.Text) Then
        BindTable()
        plUpdate.Visible = False
        plTable.Visible = True
        'Else
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorNoSearchValue") & "');", True)
        'End If
    End Sub

#End Region

#Region "validation"

    'Private Function DataChanged(ByVal admin As AdminObj) As Boolean
    '    If txtUserID.Text.Equals(admin.fldCode) AndAlso
    '        txtName.Text.Equals(admin.fldName) AndAlso
    '        txtIC.Text.Equals(admin.fldICNo) AndAlso
    '        ddlCountry.SelectedValue.Equals(admin.fldCountryID) AndAlso
    '        ddlLevel.SelectedValue = admin.fldLevel AndAlso
    '        ddlStatus.SelectedValue.Equals(admin.fldStatus) Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgNoChanges") & "');", True)
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

#End Region

#Region "Save To DB"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        'UserIsAuthenticated()
        'Dim admin As AdminObj = AdminManager.GetAdmin(DeviceID)
        'If Not admin Is Nothing Then
        '    If UpdateDetails(admin) Then
        '        UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE ADMIN", "Admin ID: " & admin.fldID & ", Admin Code: " & admin.fldCode, "")
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
        '    Else
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        '    End If
        'End If
    End Sub

    Private Function UpdateDetails(ByVal admin As AdminObj) As Boolean
        'admin.fldimei = txtImei.Text.ToUpper
        'admin.fldcontactno = txtContactNo.Text
        'admin.fldStatus = ddlStatus.SelectedValue
        'Dim result As Boolean = AdminManager.Save(admin) > 0
        'Return result
    End Function

#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        'Dim admin As AdminObj = AdminManager.GetAdmin(UserID)
        'GetUserData(admin)
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        plTable.Visible = False
        plUpdate.Visible = True
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        plTable.Visible = True
        plUpdate.Visible = False
    End Sub

#End Region

End Class