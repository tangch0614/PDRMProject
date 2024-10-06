Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAlertNotificationReport
    Inherits ABase

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()

        'Header/Title
        lblPageTitle.Text = GetText("AlertReport")
        lblHeader.Text = GetText("AlertReport")
        'Search
        lblSDate.Text = GetText("Date")
        lblSTime.Text = GetText("Time")
        lblSEMD.Text = GetText("EMD")
        lblSOPP.Text = GetText("OPP")
        lblSState.Text = GetText("State")
        'lblSViolateTerms.Text = GetText("ViolateTerms")
        'lblSSeverity.Text = GetText("Severity")
        lblSProcessStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        btnExport.Text = GetText("Excel")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetEMDDevice()
        GetOPPList()
        'GetViolateTerms()
        'GetSeverity()
        GetState()
        GetProcessStatus()
        GetTime()
        Dim datetime As DateTime = UtilityManager.GetServerDateTime()
        hfDateFrom.Text = datetime.ToString("yyyy-MM-dd")
        hfDateTo.Text = datetime.AddDays(1).ToString("yyyy-MM-dd")
        BindTable()
    End Sub

    Private Sub GetTime()
        Dim startTime As DateTime = DateTime.ParseExact("00:00:00", "HH:mm:ss", Nothing)
        Dim endTime As DateTime = DateTime.ParseExact("23:30:00", "HH:mm:ss", Nothing)
        While startTime <= endTime
            ' Add time to dropdown list
            ddlFrTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ddlToTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ' Increment time by 1 hour
            startTime = startTime.AddHours(1)
        End While
        ddlFrTime.SelectedValue = "00:00:00"
        ddlToTime.SelectedValue = "00:00:00"
    End Sub

    Private Sub GetEMDDevice()
        ddlSEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlSEMD.DataTextField = "fldImei"
        ddlSEMD.DataValueField = "fldID"
        ddlSEMD.DataBind()
        ddlSEMD.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlSEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOPPList()
        Dim datatable As DataTable = OPPManager.GetOPPList(-1, "Y", "")
        datatable.Columns.Add("fldNameIC", GetType(String), "fldName + '-' + fldICNo")
        ddlSOPP.DataSource = datatable
        ddlSOPP.DataTextField = "fldNameIC"
        ddlSOPP.DataValueField = "fldID"
        ddlSOPP.DataBind()
        ddlSOPP.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlSOPP.SelectedIndex = 0
    End Sub

    Private Sub GetProcessStatus()
        ddlSProcessStatus.Items.Add(New ListItem(GetText("All"), -1))
        ddlSProcessStatus.Items.Add(New ListItem(GetText("Pending"), 0))
        ddlSProcessStatus.Items.Add(New ListItem(GetText("Acknowledge") & "/" & GetText("Hold"), 1))
        ddlSProcessStatus.Items.Add(New ListItem(GetText("Completed"), 2))
        ddlSProcessStatus.SelectedIndex = 0
    End Sub

    Private Sub GetState()
        ddlSState.DataSource = CountryManager.GetCountryStateList("MY")
        ddlSState.DataTextField = "fldState"
        ddlSState.DataValueField = "fldState"
        ddlSState.DataBind()
        ddlSState.Items.Insert(0, New ListItem(GetText("All"), ""))
        ddlSState.SelectedIndex = 0
    End Sub

    'Private Sub GetViolateTerms()
    '    Dim datatable As DataTable = AlertManager.GetViolateTermsList()
    '    ddlSViolateTerms.DataSource = datatable
    '    ddlSViolateTerms.DataTextField = "fldAlert"
    '    ddlSViolateTerms.DataValueField = "fldAlert"
    '    ddlSViolateTerms.DataBind()
    '    ddlSViolateTerms.Items.Insert(0, New ListItem(GetText("All"), ""))
    '    ddlSViolateTerms.SelectedIndex = 0
    'End Sub

    'Private Sub GetSeverity()
    '    ddlSSeverity.Items.Add(New ListItem(GetText("All"), ""))
    '    ddlSSeverity.Items.Add(New ListItem(GetText("High"), "high"))
    '    ddlSSeverity.Items.Add(New ListItem(GetText("Medium"), "medium"))
    '    ddlSSeverity.Items.Add(New ListItem(GetText("Low"), "low"))
    '    ddlSSeverity.SelectedIndex = 0
    'End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = AlertManager.GetAlertReport(ddlSEMD.SelectedValue, ddlSOPP.SelectedValue, -1, ddlSState.SelectedValue, "", "", ddlSProcessStatus.SelectedValue, "", hfDateFrom.Text & " " & ddlFrTime.SelectedValue, hfDateTo.Text & " " & ddlToTime.SelectedValue)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub
    Protected Function GetLink(ByVal id As Long) As String
        Return "OpenPopupWindow('../admins/AlertInfo.aspx?id=" & id & "&i=" & UtilityManager.MD5Encrypt(id & "processalert") & "',1280,800);return false"
    End Function

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        BindTable()
    End Sub

#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        ddlSEMD.SelectedIndex = 0
        ddlSOPP.SelectedIndex = 0
        ddlSState.SelectedIndex = 0
        ddlSProcessStatus.SelectedIndex = 0
        'ddlSViolateTerms.SelectedIndex = 0
        'ddlSSeverity.SelectedIndex = 0
        Dim datetime As DateTime = UtilityManager.GetServerDateTime()
        hfDateFrom.Text = datetime.ToString("yyyy-MM-dd")
        hfDateTo.Text = datetime.ToString("yyyy-MM-dd")
        rptTable.DataSource = ""
        rptTable.DataBind()
    End Sub

#End Region

#Region "Export Excel"

    Protected Sub btnExport_Click(sender As Object, e As EventArgs)
        Dim DataTable As DataTable = AlertManager.GetAlertReport(ddlSEMD.SelectedValue, ddlSOPP.SelectedValue, -1, ddlSState.SelectedValue, "", "", ddlSProcessStatus.SelectedValue, "", hfDateFrom.Text, hfDateTo.Text)
        If Not DataTable Is Nothing And DataTable.Rows.Count > 0 Then
            DataTable.Columns.Add("fldNum", GetType(Integer))
            DataTable.Columns.Add("fldActsAndSection", GetType(String))
            DataTable.Columns.Add("fldProcessStatusName", GetType(String))
            DataTable.Columns.Add("fldLastProcessDateTimeStr", GetType(String))
            For i As Integer = 0 To DataTable.Rows.Count - 1
                DataTable.Rows(i)("fldNum") = i + 1
                DataTable.Rows(i)("fldActsAndSection") = DataTable.Rows(i)("fldActs") & " - " & DataTable.Rows(i)("fldActsSection")
                DataTable.Rows(i)("fldProcessStatusName") = If(CInt(DataTable.Rows(i)("fldProcessStatus")) = 1, GetText("Acknowledge") & "/" & GetText("Hold"), If(CInt(DataTable.Rows(i)("fldProcessStatus")) = 2, GetText("Completed"), GetText("Pending")))
                DataTable.Rows(i)("fldLastProcessDateTimeStr") = If(CInt(DataTable.Rows(i)("fldProcessStatus")) = 2, DataTable.Rows(i)("fldLastProcessDateTime"), "-")
            Next
            Dim fieldList As String(,) = {{"fldNum", GetText("Num")},
                                            {"fldOPPName", GetText("OPPItem").Replace("vITEM", GetText("Name"))},
                                            {"fldOPPICNo", GetText("OPPItem").Replace("vITEM", GetText("ICNum"))},
                                            {"fldEMDName", GetText("EMD")},
                                            {"fldEMDImei", GetText("IMEI")},
                                            {"fldActsAndSection", GetText("Acts")},
                                            {"fldState", GetText("State")},
                                            {"fldMukim", GetText("Township")},
                                            {"fldMsg", GetText("ViolateTerms")},
                                            {"fldDateTime", GetText("AlertDateTime")},
                                            {"fldProcessStatusName", GetText("Status")},
                                            {"fldLastProcessDateTimeStr", GetText("CompleteDateTime")},
                                            {"fldProcessRemark", GetText("ActionTaken")},
                                            {"fldLastProcessByName", GetText("ClosingBy")}}
            ExportToExcel_xlsx(DataTable, fieldList, "LaporanMakluman" & Date.Now().ToString("yyyyMMdd"))
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('No data records for export!')", True)
        End If
    End Sub

#End Region

End Class