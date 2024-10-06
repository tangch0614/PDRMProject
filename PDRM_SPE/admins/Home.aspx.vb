Imports AppCode.BusinessLogic

Public Class AHome
    Inherits ABase

    Protected Property UserID() As Long
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
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("Home")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        SetText()
        UserID = AdminAuthentication.GetUserData(2)
        SetDeparmentColor()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();initdashboarddata();initNotifications();", True)
    End Sub

    Private Sub SetDeparmentColor()
        dvJenayah.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(1))
        dvKomersil.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(2))
        dvNarkotik.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(3))
        dvCawanganKhas.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(4))
    End Sub

    Protected Sub rptActiveEMD_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me)
            Dim lbtEMDList As LinkButton = e.Item.FindControl("lbtEMDList")
            scriptManager.RegisterAsyncPostBackControl(lbtEMDList)
        End If
    End Sub

    Protected Sub rptActiveEMD_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("showlist") Then
            lblActiveEMDListTitle.Text = GetText("ActiveEMD") & " - " & PoliceStationManager.GetDepartmentName(e.CommandArgument)
            rptActiveEMDList.DataSource = EMDDeviceManager.GetActiveDeviceList(e.CommandArgument)
            rptActiveEMDList.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#dvActiveEMDList').modal('show');", True)
        End If
    End Sub

    Protected Sub lbtActiveEMD_Click(sender As Object, e As EventArgs)
        rptActiveEMD.DataSource = EMDDeviceManager.CountActiveEMDList_ByDept()
        rptActiveEMD.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#dvActiveEMD').modal('show');", True)
    End Sub

    Protected Sub lbtInactiveEMD_Click(sender As Object, e As EventArgs)
        Dim datatable As DataTable = EMDDeviceManager.GetInactiveDeviceList(-1)
        rptInactiveEMDList.DataSource = datatable
        rptInactiveEMDList.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#dvInactiveEMDList').modal('show');", True)
    End Sub

    Protected Sub lbtViolateTermsList_Click(sender As Object, e As EventArgs)
        Dim datatable As DataTable = New DataTable
        If sender.CommandArgument.Equals("jenayah_alert") Then
            lblViolateTermsTitle.Text = GetText("ViolateTerms") & " - " & PoliceStationManager.GetDepartmentName(1)
            datatable = AlertManager.CountAlertGroup(-1, -1, 1, 0, "", -1)
        ElseIf sender.CommandArgument.Equals("komersil_alert") Then
            lblViolateTermsTitle.Text = GetText("ViolateTerms") & " - " & PoliceStationManager.GetDepartmentName(2)
            datatable = AlertManager.CountAlertGroup(-1, -1, 2, 0, "", -1)
        ElseIf sender.CommandArgument.Equals("narkotik_alert") Then
            lblViolateTermsTitle.Text = GetText("ViolateTerms") & " - " & PoliceStationManager.GetDepartmentName(3)
            datatable = AlertManager.CountAlertGroup(-1, -1, 3, 0, "", -1)
        ElseIf sender.CommandArgument.Equals("cawangankhas_alert") Then
            lblViolateTermsTitle.Text = GetText("ViolateTerms") & " - " & PoliceStationManager.GetDepartmentName(4)
            datatable = AlertManager.CountAlertGroup(-1, -1, 4, 0, "", -1)
        Else
            lblViolateTermsTitle.Text = GetText("ViolateTerms")
            datatable = AlertManager.CountAlertGroup(-1, -1, -1, 0, "", -1)
        End If
        rptTotal_Alert.DataSource = datatable
        rptTotal_Alert.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#dvViolateTermsList').modal('show');", True)
    End Sub
#End Region

End Class