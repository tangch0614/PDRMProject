Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AOfficerList
    Inherits ABase

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

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()

        'Header/Title
        lblPageTitle.Text = GetText("OfficerList")
        lblHeader.Text = GetText("OfficerList")
        lblInfo.Text = GetText("OfficerInfo")
        lblLPasswordHeader.Text = GetText("ChangePassword")
        'Search
        lblSUserID.Text = GetText("Username")
        lblSName.Text = GetText("Name")
        lblSPoliceNo.Text = GetText("PoliceIDNo")
        lblSDepartment.Text = GetText("Department")
        lblSPoliceStation.Text = GetText("PoliceStation")
        lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'details
        lblUserID.Text = GetText("Username")
        lblName.Text = GetText("Name")
        lblIC.Text = GetText("ICNum")
        rfvIC.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ICNum"))
        lblContactNo.Text = GetText("ContactNum")
        rfvContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ContactNum"))
        lblPoliceNo.Text = GetText("PoliceIDNo")
        lblDepartment.Text = GetText("Department")
        lblPoliceStation.Text = GetText("PoliceStation")
        'lblCountry.Text = GetText("Country")
        lblLevel.Text = GetText("RoleLevel")
        'lblLanguage.Text = GetText("PreferredLanguage")
        lblStatus.Text = GetText("Status")
        'New login password
        lblLPassword.Text = GetText("NewPassword")
        rfvLPassword.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("NewPassword"))
        revLPassword.ErrorMessage = GetText("ErrorPasswordLength")
        'New confirm login password 
        lblLPasswordConfirm.Text = GetText("ConfirmPassword")
        rfvLPasswordConfirm.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ConfirmPassword"))
        cvLPasswordConfirm.ErrorMessage = GetText("ErrorConfirmPasswordCompare")
        'Buttons/Message
        btnBack.Text = GetText("Back")
        btnBack2.Text = GetText("Back")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        btnChangeLPassword.Text = GetText("ChangePassword")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetLevel()
        GetStatus()
        GetPoliceStation()
        GetDepartment()
        'GetLanguage()
        'GetCountry()
        BindTable()
    End Sub

    Private Sub GetLevel()
        ddlLevel.DataSource = RoleManager.GetRoleList("A")
        ddlLevel.DataTextField = "fldRoleName"
        ddlLevel.DataValueField = "fldRoleID"
        ddlLevel.DataBind()
    End Sub

    Private Sub GetStatus()
        ddlStatus.Items.Add(New ListItem(GetText("Active"), "A", True))
        ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "D", True))
        ddlStatus.SelectedIndex = 0

        ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Active"), "A", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Inactive"), "D", True))
        ddlSStatus.SelectedIndex = 0
    End Sub

    'Private Sub GetLanguage()
    '    ddlLanguage.DataSource = UtilityManager.GetLanguageList()
    '    ddlLanguage.DataTextField = "fldName"
    '    ddlLanguage.DataValueField = "fldCode"
    '    ddlLanguage.DataBind()
    'End Sub

    'Private Sub GetCountry()
    '    ddlCountry.DataSource = CountryManager.GetCountryList()
    '    ddlCountry.DataTextField = "fldName"
    '    ddlCountry.DataValueField = "fldID"
    '    ddlCountry.DataBind()
    'End Sub

    Private Sub GetPoliceStation()
        Dim datatable As DataTable = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "", "", "", "", "Y")
        ddlPoliceStation.DataSource = datatable
        ddlPoliceStation.DataTextField = "fldName"
        ddlPoliceStation.DataValueField = "fldID"
        ddlPoliceStation.DataBind()
        ddlPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), 0, True))
        ddlPoliceStation.SelectedIndex = 0

        ddlSPoliceStation.DataSource = datatable
        ddlSPoliceStation.DataTextField = "fldName"
        ddlSPoliceStation.DataValueField = "fldID"
        ddlSPoliceStation.DataBind()
        ddlSPoliceStation.Items.Insert(0, New ListItem(GetText("None"), 0, True))
        ddlSPoliceStation.Items.Insert(0, New ListItem(GetText("All"), -1, True))
        ddlSPoliceStation.SelectedIndex = 0
    End Sub

    Private Sub GetDepartment()
        Dim datatable As DataTable = PoliceStationManager.GetDepartmentList("Y")
        ddlDepartment.DataSource = datatable
        ddlDepartment.DataTextField = "fldName"
        ddlDepartment.DataValueField = "fldID"
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Department")), 0))
        ddlDepartment.SelectedIndex = 0

        ddlSDepartment.DataSource = datatable
        ddlSDepartment.DataTextField = "fldName"
        ddlSDepartment.DataValueField = "fldID"
        ddlSDepartment.DataBind()
        ddlSDepartment.Items.Insert(0, New ListItem(GetText("None"), 0, True))
        ddlSDepartment.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlSDepartment.SelectedIndex = 0
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = AdminManager.SearchAdminList(0, 3, 0, txtSUserID.Text, txtSName.Text, "", txtSPoliceNo.Text, ddlSDepartment.SelectedValue, ddlSPoliceStation.SelectedValue, "", ddlSStatus.SelectedValue, "", "")
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        UserID = 0
        If e.CommandName.Equals("editUser") Then
            UserID = e.CommandArgument
            Dim admin As AdminObj = AdminManager.GetAdmin(UserID)
            GetUserData(admin)
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

    Private Sub GetUserData(ByVal admin As AdminObj)
        If Not admin Is Nothing Then
            txtUserID.Text = admin.fldCode
            txtName.Text = admin.fldName
            txtIC.Text = admin.fldICNo
            txtContactNo.Text = admin.fldContactNo
            txtPoliceNo.Text = admin.fldPoliceNo
            ddlDepartment.SelectedValue = admin.fldDeptID
            ddlPoliceStation.SelectedValue = admin.fldPoliceStationID
            'ddlCountry.SelectedValue = admin.fldCountryID
            ddlLevel.SelectedValue = admin.fldLevel
            'ddlLanguage.SelectedValue = admin.fldLanguage
            ddlStatus.SelectedValue = admin.fldStatus
            plTable.Visible = False
            plUpdate.Visible = True
        Else
            txtUserID.Text = ""
            txtName.Text = ""
            txtIC.Text = ""
            txtContactNo.Text = ""
            txtPoliceNo.Text = ""
            ddlDepartment.SelectedIndex = 0
            ddlPoliceStation.SelectedIndex = 0
            'ddlCountry.SelectedValue = admin.fldCountryID
            ddlLevel.SelectedIndex = 0
            'ddlLanguage.SelectedValue = admin.fldLanguage
            ddlStatus.SelectedValue = "A"
            plTable.Visible = True
            plUpdate.Visible = False
        End If
    End Sub
#End Region

#Region "Search"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        BindTable()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

#End Region

#Region "validation"

#End Region

#Region "Save To DB"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim admin As AdminObj = AdminManager.GetAdmin(UserID)
        If Not admin Is Nothing Then
            If UpdateDetails(admin) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE USER", "User ID: " & admin.fldID & ", User Code: " & admin.fldCode, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Private Function UpdateDetails(ByVal admin As AdminObj) As Boolean
        'admin.fldLanguage = ddlLanguage.SelectedValue
        admin.fldName = txtName.Text
        admin.fldICNo = txtIC.Text
        admin.fldPoliceNo = txtPoliceNo.Text
        admin.fldContactNo = txtContactNo.Text
        admin.fldDeptID = ddlDepartment.SelectedValue
        admin.fldPoliceStationID = ddlPoliceStation.SelectedValue
        'admin.fldCountryID = ddlCountry.SelectedValue
        'admin.fldLevel = ddlLevel.SelectedValue
        admin.fldStatus = ddlStatus.SelectedValue
        Dim result As Boolean = AdminManager.Save(admin) > 0
        Return result
    End Function

#End Region

#Region "Change Password"
    Protected Sub btnChangeLPassword_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If AdminManager.ChangeLoginPassword(UserID, txtLPassword.Text) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgChangePasswordSuccess") & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorChangePasswordFailed") & "');", True)
        End If
    End Sub
#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        txtSUserID.Text = ""
        txtSName.Text = ""
        ddlSDepartment.SelectedIndex = 0
        ddlSPoliceStation.SelectedIndex = 0
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim admin As AdminObj = AdminManager.GetAdmin(UserID)
        GetUserData(admin)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        UserID = 0
        GetUserData(Nothing)
    End Sub

#End Region

End Class