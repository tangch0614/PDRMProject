Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AIPKList
    Inherits ABase

    Private Property PSID() As Long
        Get
            If Not ViewState("PSID") Is Nothing Then
                Return CLng(ViewState("PSID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("PSID") = value
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
        lblPageTitle.Text = GetText("IPKList")
        lblHeader.Text = GetText("IPKList")
        lblInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("IPK"))
        'Search
        lblSName.Text = GetText("Name")
        lblSState.Text = GetText("Name")
        lblSState.Text = GetText("State")
        'lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'details
        lblName.Text = GetText("Name")
        rfvName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Name"))
        lblContactNo.Text = GetText("ContactNum")
        rfvContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ContactNum"))
        lblState.Text = GetText("State")
        rfvState.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("State"))
        'lblStatus.Text = GetText("Status")
        'Buttons/Message
        btnBack.Text = GetText("Back")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        'GetStatus()
        GetState()
        BindTable()
    End Sub

    'Private Sub GetStatus()
    '    ddlStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
    '    ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
    '    ddlStatus.SelectedIndex = 0

    '    ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
    '    ddlSStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
    '    ddlSStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
    '    ddlSStatus.SelectedIndex = 0
    'End Sub

    Private Sub GetState()
        Dim datatable As DataTable = CountryManager.GetCountryStateList("MY")
        ddlState.DataSource = datatable
        ddlState.DataTextField = "fldState"
        ddlState.DataValueField = "fldState"
        ddlState.DataBind()
        ddlState.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("State")), "", True))
        ddlState.SelectedIndex = 0

        ddlSState.DataSource = datatable
        ddlSState.DataTextField = "fldState"
        ddlSState.DataValueField = "fldState"
        ddlSState.DataBind()
        ddlSState.Items.Insert(0, New ListItem(GetText("All"), "", True))
        ddlSState.SelectedIndex = 0
    End Sub

    Private Sub GetUserData(ByVal admin As AdminObj)
        'txtImei.Text = admin.fldCode
        'txtContactNo.Text = admin.fldName
        'ddlStatus.SelectedValue = admin.fldStatus
    End Sub
#End Region

#Region "Table binding"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        BindTable()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Private Sub BindTable()
        Dim myDataTable As DataTable = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "IPK", ddlSState.SelectedValue, "", "", "Y")
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("updateps") Then
            PSID = e.CommandArgument
            plTable.Visible = False
            plUpdate.Visible = True
            'Dim admin As AdminObj = AdminManager.GetAdmin(PSID)
            'If Not admin Is Nothing Then
            '    GetUserData(admin)
            'End If
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

#End Region

#Region "validation"

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

#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        txtSName.Text = ""
        ddlSState.SelectedIndex = 0
        'ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        'Dim admin As AdminObj = AdminManager.GetAdmin(UserID)
        'GetUserData(admin)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        plTable.Visible = True
        plUpdate.Visible = False
    End Sub

#End Region

End Class