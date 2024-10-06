Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAlertNotificationList_Past
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
        lblPageTitle.Text = GetText("PastAlertList")
        lblHeader.Text = GetText("PastAlertList")
        'Search
        lblSEMD.Text = GetText("EMD")
        lblSOPP.Text = GetText("OPP")
        lblSViolateTerms.Text = GetText("ViolateTerms")
        lblSSeverity.Text = GetText("Severity")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetEMDDevice()
        GetOPPList()
        GetViolateTerms()
        GetSeverity()
        Dim datetime As DateTime = UtilityManager.GetServerDateTime()
        hfDateFrom.Text = datetime.AddMonths(-1).ToString("yyyy-MM-dd")
        hfDateTo.Text = datetime.ToString("yyyy-MM-dd")
        BindTable()
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

    Private Sub GetViolateTerms()
        Dim datatable As DataTable = AlertManager.GetViolateTermsList()
        ddlSViolateTerms.DataSource = datatable
        ddlSViolateTerms.DataTextField = "fldAlert"
        ddlSViolateTerms.DataValueField = "fldAlert"
        ddlSViolateTerms.DataBind()
        ddlSViolateTerms.Items.Insert(0, New ListItem(GetText("All"), ""))
        ddlSViolateTerms.SelectedIndex = 0
    End Sub

    Private Sub GetSeverity()
        ddlSSeverity.Items.Add(New ListItem(GetText("All"), ""))
        ddlSSeverity.Items.Add(New ListItem(GetText("High"), "high"))
        ddlSSeverity.Items.Add(New ListItem(GetText("Medium"), "medium"))
        ddlSSeverity.Items.Add(New ListItem(GetText("Low"), "low"))
        ddlSSeverity.SelectedIndex = 0
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = AlertManager.GetAlertList(-1, ddlSEMD.SelectedValue, ddlSOPP.SelectedValue, 2, ddlSViolateTerms.SelectedValue, ddlSSeverity.SelectedValue, hfDateFrom.Text, hfDateTo.Text, -1, -1, " fldID Desc ")
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
        ddlSViolateTerms.SelectedIndex = 0
        ddlSSeverity.SelectedIndex = 0
        Dim datetime As DateTime = UtilityManager.GetServerDateTime()
        hfDateFrom.Text = datetime.ToString("yyyy-MM-dd")
        hfDateTo.Text = datetime.ToString("yyyy-MM-dd")
        rptTable.DataSource = ""
        rptTable.DataBind()
    End Sub

#End Region

End Class