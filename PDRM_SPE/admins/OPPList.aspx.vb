﻿Imports System.Drawing.Imaging
Imports System.IO
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AOPPList
    Inherits ABase

    Private Property OPPID() As Long
        Get
            If Not ViewState("OPPID") Is Nothing Then
                Return CLng(ViewState("OPPID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("OPPID") = value
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
        lblPageTitle.Text = GetText("ManageOPP")
        lblHeader.Text = GetText("ManageOPP")
        'Search
        lblSName.Text = GetText("Name")
        lblSICNo.Text = GetText("ICNum")
        lblSOrdRefNo.Text = GetText("OrderRefNo")
        'lblSEMD.Text = GetText("EMD")
        lblSDepartment.Text = GetText("Department")
        lblSPoliceStation.Text = GetText("PoliceStation")
        lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'subject
        lblSubject.Text = GetText("SubjectInfo")
        lblSubjectName.Text = GetText("Name")
        rfvSubjectName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Name"))
        lblSubjectICNo.Text = GetText("ICNum")
        rfvSubjectICNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ICNum"))
        lblSubjectContactNo.Text = GetText("ContactNum")
        rfvSubjectContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ContactNum"))
        lblAddress.Text = GetText("ResidentialAddress")
        rfvAddress.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ResidentialAddress"))
        lblState.Text = GetText("State")
        rfvState.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("State"))
        lblDistrict.Text = GetText("District")
        rfvDistrict.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("District"))
        lblMukim.Text = GetText("Township")
        rfvMukim.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Township"))
        lblPoliceStation.Text = GetText("PoliceStation")
        rfvPoliceStation.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("PoliceStation"))
        lblDepartment.Text = GetText("Department")
        rfvDepartment.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Department"))
        lblOffenceDesc.Text = GetText("Offence")
        rfvOffenceDesc.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Offence"))
        'order
        lblOrderInfo.Text = GetText("Order")
        lblActs.Text = GetText("OrderCategory")
        rfvActs.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Acts"))
        rfvActsSection.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Section"))
        lblOrderIssuedBy.Text = GetText("OrderIssuedBy")
        rfvOrderIssuedBy.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OrderIssuedBy"))
        'txtOrderIssuedBy.Attributes("placeholder") = GetText("Nyatakan pihak mengeluaran perintah")
        lblOrderIssuedDate.Text = GetText("OrderIssuedDate")
        lblOrderRefNo.Text = GetText("OrderRefNo")
        rfvOrderRefNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OrderRefNo"))
        lblOrderDate.Text = GetText("OrderDate")
        lblOrderPeriod.Text = GetText("OrderPeriod")
        rfvOrderPeriod.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OrderPeriod"))
        'police station
        lblReportInfo.Text = GetText("PoliceStationInfo")
        lblRptPoliceStation.Text = GetText("SelfReportPoliceStation")
        rfvRptPoliceStation.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SelfReportPoliceStation"))
        lblRptPSContactNo.Text = GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))
        lblSDNo.Text = GetText("SDNum")
        rfvSDNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SDNum"))
        lblOCSName.Text = GetText("OCS")
        rfvOCSName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCS"))
        lblOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        rfvOCSContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCSItem").Replace("vITEM", GetText("ContactNum")))
        lblReportDay.Text = GetText("SelfReportDay")
        lblReportTime.Text = GetText("SelfReportTime")
        'overseer
        lblOverseerInfo.Text = GetText("Overseer")
        lblOverseer.Text = GetText("Overseer")
        rfvOverseer.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Overseer"))
        lblOverseerIDNo.Text = GetText("PoliceIDNo")
        lblOverseerIPK.Text = GetText("Contingent")
        lblOverseerDept.Text = GetText("Department")
        lblOverseerContactNo.Text = GetText("ContactNum")
        'oversight
        lblOversightInfo.Text = GetText("Oversight")
        lblRestrictTime.Text = GetText("ResidenceRestrictionPeriod")
        lblGeofenceInfo.Text = GetText("Geofence")
        lblGeofenceMukim.Text = GetText("Township")
        rfvGeofenceMukim.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Township") & " (" & GetText("Geofence") & ")")
        lblGeofenceStatus.Text = GetText("GeofenceStatus")
        'emd
        lblEMDInstallDate.Text = GetText("InstallationDate")
        lblEMDDeviceInfo.Text = GetText("EMD")
        lblEMD.Text = GetText("EMD")
        rfvEMD.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("EMD"))
        chkBeacon.Text = GetText("SmartTag")
        chkOBC.Text = GetText("OBC")
        chkBeacon.Text = GetText("Beacon")
        chkCharger.Text = GetText("Charger")
        chkStrap.Text = GetText("Strap")
        chkCable.Text = GetText("Cable")
        'lblStatus.Text = GetText("Status")
        'file upload
        lblFileInfo.Text = GetText("OthersInfo")
        lblPhoto1.Text = GetText("Picture") & " (" & GetText("Face") & ")"
        btnPhoto1.Text = GetText("SelectItem").Replace("vITEM", GetText("Picture"))
        lblPhoto2.Text = GetText("Picture") & " (" & GetText("FullBody") & ")"
        btnPhoto2.Text = GetText("SelectItem").Replace("vITEM", GetText("Picture"))
        lblAttachment1.Text = GetText("Attachment") & " 1"
        btnAttachment1.Text = GetText("SelectItem").Replace("vITEM", GetText("File"))
        lblAttachment2.Text = GetText("Attachment") & " 2"
        btnAttachment2.Text = GetText("SelectItem").Replace("vITEM", GetText("File"))
        lblRemark.Text = GetText("Remark")
        'popup subject
        lblPSubject.Text = GetText("SubjectInfo")
        lblPSubjectName.Text = GetText("Name")
        lblPSubjectICNo.Text = GetText("ICNum")
        lblPSubjectContactNo.Text = GetText("ContactNum")
        lblPAddress.Text = GetText("ResidentialAddress")
        lblPState.Text = GetText("State")
        lblPDistrict.Text = GetText("District")
        lblPMukim.Text = GetText("Township")
        lblPPoliceStation.Text = GetText("PoliceStation")
        lblPDepartment.Text = GetText("Department")
        lblPOffenceDesc.Text = GetText("Offence")
        'popup order
        lblPOrderInfo.Text = GetText("Order")
        lblPActs.Text = GetText("OrderCategory")
        lblPOrderIssuedBy.Text = GetText("OrderIssuedBy")
        'txtpOrderIssuedBy.Attributes("placeholder") = GetText("Nyatakan pihak mengeluaran perintah")
        lblPOrderIssuedDate.Text = GetText("OrderIssuedDate")
        lblPOrderRefNo.Text = GetText("OrderRefNo")
        lblPOrderDate.Text = GetText("OrderDate")
        lblPOrderPeriod.Text = GetText("OrderPeriod")
        'popup police station
        lblPReportInfo.Text = GetText("PoliceStationInfo")
        lblPRptPoliceStation.Text = GetText("SelfReportPoliceStation")
        lblPRptPSContactNo.Text = GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))
        lblPSDNo.Text = GetText("SDNum")
        lblPOCSName.Text = GetText("OCS")
        lblPOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        lblPReportDay.Text = GetText("SelfReportDay")
        lblPReportTime.Text = GetText("SelfReportTime")
        'popup overseer
        lblPOverseerInfo.Text = GetText("Overseer")
        lblPOverseer.Text = GetText("Overseer")
        lblPOverseerIDNo.Text = GetText("PoliceIDNo")
        lblPOverseerIPK.Text = GetText("Contingent")
        lblPOverseerDept.Text = GetText("Department")
        lblPOverseerContactNo.Text = GetText("ContactNum")
        'popup oversight
        lblPOversightInfo.Text = GetText("Oversight")
        lblPRestrictTime.Text = GetText("ResidenceRestrictionPeriod")
        lblPGeofenceInfo.Text = GetText("Geofence")
        lblPGeofenceMukim.Text = GetText("Township")
        'popup emd
        lblPEMDInstallDate.Text = GetText("InstallationDate")
        lblPEMDDeviceInfo.Text = GetText("EMD")
        lblPEMD.Text = GetText("EMD")
        chkPBeacon.Text = GetText("SmartTag")
        chkPOBC.Text = GetText("OBC")
        chkPBeacon.Text = GetText("Beacon")
        chkPCharger.Text = GetText("Charger")
        chkPStrap.Text = GetText("Strap")
        chkPCable.Text = GetText("Cable")
        'popup file upload
        lblPFileInfo.Text = GetText("OthersInfo")
        lblPPhoto1.Text = GetText("Picture") & " (" & GetText("Face") & ")"
        lblPPhoto2.Text = GetText("Picture") & " (" & GetText("FullBody") & ")"
        lblPAttachment1.Text = GetText("Attachment") & " 1"
        lblPAttachment2.Text = GetText("Attachment") & " 2"
        lblPRemark.Text = GetText("Remark")
        lblPStatus.Text = GetText("Status")
        'Buttons/Message
        btnPApprove.Text = GetText("Approve")
        btnPCancel.Text = GetText("Close")
        btnAdd.Text = GetText("Add")
        btnBack1.Text = GetText("Back")
        btnBack2.Text = GetText("Back")
        btnBack3.Text = GetText("Back")
        btnBack4.Text = GetText("Back")
        btnResetOPP.Text = GetText("Reset")
        btnSubmitOPP.Text = GetText("Update")
        btnResetOverseer.Text = GetText("Reset")
        btnSubmitOverseer.Text = GetText("Update")
        btnResetGeofence.Text = GetText("Reset")
        btnSubmitGeofence.Text = GetText("Update")
        btnGeofenceStatus.Text = GetText("Activate")
        btnResetEMD.Text = GetText("Reset")
        btnSubmitEMD.Text = GetText("Update")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetStatus()
        GetOrderPeriodUnit()
        GetDepartment()
        GetPoliceStation()
        GetState()
        GetGeofenceMukim()
        GetActs()
        GetOrderIssuedBy()
        GetReportDay()
        GetReportTime()
        GetRestrictTime()
        GetDevice(Nothing)
        GetOverseer()
        'GetStatus()
        Dim dateTime As DateTime = UtilityManager.GetServerDateTime()
        hfOrderDate.Text = dateTime.ToString("yyyy-MM-dd")
        hfOrderIssuedDate.Text = dateTime.ToString("yyyy-MM-dd")
        hfEMDInstallDate.Text = dateTime.ToString("yyyy-MM-dd")
        ddlActs_SelectedIndexChanged(Me.ddlActs, EventArgs.Empty)
        ddlState_SelectedIndexChanged(Me.ddlState, EventArgs.Empty)
        BindTable()
    End Sub

    Private Sub GetDepartment()
        Dim datatable As DataTable = PoliceStationManager.GetDepartmentList("Y")
        ddlDepartment.DataSource = datatable
        ddlDepartment.DataTextField = "fldName"
        ddlDepartment.DataValueField = "fldID"
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Department")), -1))
        ddlDepartment.SelectedIndex = 0

        ddlSDepartment.DataSource = datatable
        ddlSDepartment.DataTextField = "fldName"
        ddlSDepartment.DataValueField = "fldID"
        ddlSDepartment.DataBind()
        ddlSDepartment.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlSDepartment.SelectedIndex = 0
    End Sub

    Private Sub GetOrderPeriodUnit()
        ddlOrderPeriodUnit.Items.Add(New ListItem(GetText("Day"), "D"))
        ddlOrderPeriodUnit.Items.Add(New ListItem(GetText("Month"), "M"))
        ddlOrderPeriodUnit.Items.Add(New ListItem(GetText("Year"), "Y"))
        ddlOrderPeriodUnit.SelectedValue = "Y"
    End Sub

    Private Sub GetReportDay()
        ddlReportDay.Items.Add(New ListItem(GetText("Sunday"), "Sunday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Monday"), "Monday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Tuesday"), "Tuesday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Wednesday"), "Wednesday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Thursday"), "Thursday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Friday"), "Friday", True))
        ddlReportDay.Items.Add(New ListItem(GetText("Saturday"), "Saturday", True))
        ddlReportDay.SelectedValue = "Monday"
    End Sub

    Private Sub GetReportTime()
        Dim startTime As DateTime = DateTime.ParseExact("00:00:00", "HH:mm:ss", Nothing)
        Dim endTime As DateTime = DateTime.ParseExact("23:00:00", "HH:mm:ss", Nothing)

        While startTime <= endTime
            ' Add time to dropdown list
            ddlReportFrTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ddlReportToTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ' Increment time by 1 hour
            startTime = startTime.AddHours(1)
        End While
        ddlReportFrTime.SelectedValue = "08:00:00"
        ddlReportToTime.SelectedValue = "12:00:00"
    End Sub

    Private Sub GetRestrictTime()
        Dim startTime As DateTime = DateTime.ParseExact("00:00:00", "HH:mm:ss", Nothing)
        Dim endTime As DateTime = DateTime.ParseExact("23:00:00", "HH:mm:ss", Nothing)

        While startTime <= endTime
            ' Add time to dropdown list
            ddlRestrictFrTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ddlRestrictToTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ' Increment time by 1 hour
            startTime = startTime.AddHours(1)
        End While
        ddlRestrictFrTime.SelectedValue = "20:00:00"
        ddlRestrictToTime.SelectedValue = "06:00:00"
    End Sub

    Private Sub GetStatus()
        ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Pending"), "P", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        If (AdminAuthentication.GetUserData(5) = 3) Then
            ddlSStatus.SelectedValue = "P"
        Else
            ddlSStatus.SelectedIndex = 0
        End If
    End Sub

    Private Sub GetState()
        Dim datatable As DataTable = CountryManager.GetCountryStateList("MY")
        ddlState.DataSource = datatable
        ddlState.DataTextField = "fldState"
        ddlState.DataValueField = "fldState"
        ddlState.DataBind()
        ddlState.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("State")), "", True))
        ddlState.SelectedIndex = 0
    End Sub

    Private Sub GetDistrict(ByVal state As String)
        ddlDistrict.Items.Clear()
        ddlDistrict.DataSource = CountryManager.GetDistrictList(state)
        ddlDistrict.DataTextField = "fldDistrict"
        ddlDistrict.DataValueField = "fldDistrict"
        ddlDistrict.DataBind()
        ddlDistrict.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("District")), "", True))
        ddlDistrict.SelectedIndex = 0
    End Sub

    Private Sub GetMukim(ByVal state As String, ByVal district As String)
        Dim datatable As DataTable = CountryManager.GetMukimList(state, district)
        ddlMukim.Items.Clear()
        ddlMukim.DataSource = datatable
        ddlMukim.DataTextField = "fldMukim"
        ddlMukim.DataValueField = "fldMukim"
        ddlMukim.DataBind()
        ddlMukim.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Township")), "", True))
        ddlMukim.SelectedIndex = 0
    End Sub

    Private Sub GetGeofenceMukim()
        Dim datatable As DataTable = CountryManager.GetMukimList("", "")
        ddlGeofenceMukim.DataSource = datatable
        ddlGeofenceMukim.DataTextField = "fldMukim"
        ddlGeofenceMukim.DataValueField = "fldMukim"
        ddlGeofenceMukim.DataBind()
        ddlGeofenceMukim.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Township")), "", True))
        ddlGeofenceMukim.SelectedIndex = 0
    End Sub

    'Private Sub GetPoliceStation(ByVal state As String)
    '    Dim datatable As DataTable = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "", state, "", "", "Y")
    '    ddlPoliceStation.Items.Clear()
    '    ddlPoliceStation.DataSource = datatable
    '    ddlPoliceStation.DataTextField = "fldName"
    '    ddlPoliceStation.DataValueField = "fldID"
    '    ddlPoliceStation.DataBind()
    '    ddlPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), -1, True))
    '    ddlPoliceStation.SelectedIndex = 0

    '    ddlRptPoliceStation.Items.Clear()
    '    ddlRptPoliceStation.DataSource = datatable
    '    ddlRptPoliceStation.DataTextField = "fldName"
    '    ddlRptPoliceStation.DataValueField = "fldID"
    '    ddlRptPoliceStation.DataBind()
    '    ddlRptPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), -1, True))
    '    ddlRptPoliceStation.SelectedIndex = 0
    'End Sub

    Private Sub GetPoliceStation()
        Dim datatable As DataTable = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "", "", "", "", "Y")
        ddlSPoliceStation.DataSource = datatable
        ddlSPoliceStation.DataTextField = "fldName"
        ddlSPoliceStation.DataValueField = "fldID"
        ddlSPoliceStation.DataBind()
        ddlSPoliceStation.Items.Insert(0, New ListItem(GetText("All"), -1, True))
        ddlSPoliceStation.SelectedIndex = 0

        ddlPoliceStation.DataSource = datatable
        ddlPoliceStation.DataTextField = "fldName"
        ddlPoliceStation.DataValueField = "fldID"
        ddlPoliceStation.DataBind()
        ddlPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), -1, True))
        ddlPoliceStation.SelectedIndex = 0

        ddlRptPoliceStation.DataSource = datatable
        ddlRptPoliceStation.DataTextField = "fldName"
        ddlRptPoliceStation.DataValueField = "fldID"
        ddlRptPoliceStation.DataBind()
        ddlRptPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), -1, True))
        ddlRptPoliceStation.SelectedIndex = 0
    End Sub

    Private Sub GetActs()
        ddlActs.DataSource = ActsManager.GetActsList("Y")
        ddlActs.DataTextField = "fldName"
        ddlActs.DataValueField = "fldID"
        ddlActs.DataBind()
        ddlActs.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Acts")), -1, True))
        ddlActs.SelectedIndex = 0
    End Sub

    Private Sub GetActsSection(ByVal actsid As Long)
        ddlActsSection.Items.Clear()
        ddlActsSection.DataSource = ActsManager.GetActsSectionList(actsid, "Y")
        ddlActsSection.DataTextField = "fldName"
        ddlActsSection.DataValueField = "fldID"
        ddlActsSection.DataBind()
        ddlActsSection.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Section")), -1, True))
        ddlActsSection.SelectedIndex = 0
    End Sub

    Private Sub GetOrderIssuedBy()
        ddlOrderIssuedBy.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Party")), "", True))
        ddlOrderIssuedBy.Items.Add(New ListItem(GetText("Mahkamah"), "Mahkamah", True))
        ddlOrderIssuedBy.Items.Add(New ListItem(GetText("Lembaga"), "Lembaga", True))
        ddlOrderIssuedBy.Items.Add(New ListItem(GetText("Menteri"), "Menteri", True))
        ddlOrderIssuedBy.SelectedIndex = 0
    End Sub

    Private Sub GetDevice(ByVal opp As OPPObj)
        ddlEMD.Items.Clear()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "N")
        ddlEMD.DataTextField = "fldImei"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        If Not opp Is Nothing AndAlso opp.fldEMDDeviceID > 0 Then
            Dim imei As Long = EMDDeviceManager.GetIMEI(opp.fldEMDDeviceID)
            ddlEMD.Items.Add(New ListItem(imei & " (" & GetText("Current") & ")", opp.fldEMDDeviceID))
        End If
        ddlEMD.Items.Insert(0, New ListItem(GetText("Unassign"), 0, True))
        ddlEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOverseer()
        Dim datatable As DataTable = AdminManager.SearchAdminList(-1, 3, -1, "", "", "", "", -1, -1, "", "A", "", "")
        'datatable.Columns.Add("fldNameStatus", GetType(String), "fldName + IIf(fldStatus='D',' (" & GetText("Inactive") & ")','')")
        ddlOverseer.Items.Clear()
        ddlOverseer.DataSource = datatable
        ddlOverseer.DataTextField = "fldName"
        ddlOverseer.DataValueField = "fldID"
        ddlOverseer.DataBind()
        ddlOverseer.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Overseer")), -1, True))
        ddlOverseer.SelectedIndex = 0
    End Sub

    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetDistrict(ddlState.SelectedValue)
        ddlDistrict_SelectedIndexChanged(Me.ddlDistrict, EventArgs.Empty)
    End Sub

    Protected Sub ddlDistrict_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetMukim(ddlState.SelectedValue, ddlDistrict.SelectedValue)
        'GetPoliceStation(ddlState.SelectedValue)
    End Sub

    Protected Sub ddlActs_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetActsSection(ddlActs.SelectedValue)
    End Sub

    Protected Sub ddlRptPoliceStation_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtRptPSContactNo.Text = PoliceStationManager.GetPoliceStationContactNo(ddlRptPoliceStation.SelectedValue)
    End Sub

    Protected Sub ddlOverseer_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim officer As AdminObj = AdminManager.GetAdmin(ddlOverseer.SelectedValue)
        If Not officer Is Nothing Then
            Dim pstype As String = PoliceStationManager.GetPoliceStationType(officer.fldPoliceStationID)
            Dim ipkid As Long = 0
            If pstype.Equals("IPK") Then
                ipkid = officer.fldPoliceStationID
            Else
                ipkid = PoliceStationManager.GetPoliceStationIPK(officer.fldPoliceStationID)
            End If
            txtOverseerIDNo.Text = officer.fldPoliceNo
            txtOverseerIPK.Text = PoliceStationManager.GetPoliceStationName(ipkid)
            txtOverseerDept.Text = PoliceStationManager.GetDepartmentName(officer.fldDeptID)
            txtOverseerContactNo.Text = officer.fldContactNo
        Else
            txtOverseerIDNo.Text = ""
            txtOverseerIPK.Text = ""
            txtOverseerDept.Text = ""
            txtOverseerContactNo.Text = ""
        End If
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = OPPManager.GetOPPList(-1, txtSName.Text, txtSICNo.Text, -1, ddlSDepartment.SelectedValue, ddlSPoliceStation.SelectedValue, txtSOrdRefNo.Text, ddlSStatus.SelectedValue)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        OPPID = 0
        If e.CommandName.Equals("updateopp") Then
            OPPID = e.CommandArgument
            plTable.Visible = False
            plUpdate.Visible = True
            Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
            If Not opp Is Nothing Then
                GetOverseer()
                GetSubjectData(opp)
                GetOverseerData(opp)
                GetGeofenceData(opp)
                GetEMDData(opp)
            End If
        ElseIf e.CommandName.Equals("verify") AndAlso AdminAuthentication.GetUserData(5) = 3 Then
            OPPID = e.CommandArgument
            Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
            GetModalInfo(opp)
            If Not opp Is Nothing Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#plVerify').modal('show');", True)
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

    Protected Function GetLink(ByVal id As Long, ByVal type As String) As String
        If type.Equals("geofence") Then
            Return "OpenPopupWindow('../AddGeofence.aspx?id=" & id & "&i=" & UtilityManager.MD5Encrypt(id & "geopagar") & "',1024,768);"
        ElseIf type.Equals("showmap") Then
            'Return "OpenTabWindow('../admins/TrackingMap.aspx?imei=" & imei & "&i=" & UtilityManager.MD5Encrypt(imei & "trackingmap") & "');"
            Return "OpenTabWindow('../admins/TrackingMap.aspx?opp=" & id & "&i=" & UtilityManager.MD5Encrypt(id & "trackingmap") & "');"
        ElseIf type.Equals("showhist") Then
            Return "OpenTabWindow('../admins/HistoryMap.aspx?opp=" & id & "&i=" & UtilityManager.MD5Encrypt(id & "historymap") & "');"
        End If
        Return "#"
    End Function

    Protected Function GetUserType() As Long
        Return AdminAuthentication.GetUserData(5)
    End Function

    Private Sub GetModalInfo(ByVal opp As OPPObj)
        If Not opp Is Nothing Then
            If Not String.IsNullOrWhiteSpace(opp.fldPhoto1) Then
                imgPPhoto1Preview.ImageUrl = opp.fldPhoto1
            Else
                imgPPhoto1Preview.ImageUrl = "../assets/img/No_Image.png"
            End If
            If Not String.IsNullOrWhiteSpace(opp.fldPhoto2) Then
                imgPPhoto2Preview.ImageUrl = opp.fldPhoto2
            Else
                imgPPhoto2Preview.ImageUrl = "../assets/img/No_Image.png"
            End If
            txtPSubjectName.Text = opp.fldName
            txtPSubjectICNo.Text = opp.fldICNo
            txtPSubjectContactNo.Text = opp.fldContactNo
            txtPAddress.Text = opp.fldAddress
            txtPState.Text = opp.fldState
            txtPDistrict.Text = opp.fldDistrict
            txtPMukim.Text = opp.fldMukim
            txtPPoliceStation.Text = PoliceStationManager.GetPoliceStationName(opp.fldPoliceStationID)
            txtPDepartment.Text = PoliceStationManager.GetDepartmentName(opp.fldDeptID)
            txtPOffenceDesc.Text = opp.fldOffenceDesc
            txtPActs.Text = ActsManager.GetActsName(opp.fldActsID)
            txtPActsSection.Text = ActsManager.GetActsSectionName(opp.fldActsSectionID)
            txtPOrderIssuedBy.Text = opp.fldOrdParty
            txtPOrderIssuedDate.Text = opp.fldOrdIssuedDate.ToString("yyyy-MM-dd")
            txtPOrderRefNo.Text = opp.fldOrdRefNo
            txtPOrderDate.Text = opp.fldOrdFrDate.ToString("yyyy-MM-dd")
            If opp.fldOrdYear > 0 Then
                txtPOrderPeriod.Text = opp.fldOrdYear & " " & GetText("Year")
            ElseIf opp.fldOrdMonth > 0 Then
                txtPOrderPeriod.Text = opp.fldOrdMonth & " " & GetText("Month")
            ElseIf opp.fldOrdDay > 0 Then
                txtPOrderPeriod.Text = opp.fldOrdDay & " " & GetText("Day")
            End If
            txtPRptPoliceStation.Text = PoliceStationManager.GetPoliceStationName(opp.fldRptPoliceStationID)
            txtPRptPSContactNo.Text = PoliceStationManager.GetPoliceStationContactNo(opp.fldRptPoliceStationID)
            txtPSDNo.Text = opp.fldSDNo
            txtPOCSName.Text = opp.fldOCSName
            txtPOCSContactNo.Text = opp.fldOCSTelNo
            txtPReportDay.Text = GetText(opp.fldRptDay)
            txtPReportFrTime.Text = opp.fldRptFrTime
            txtPReportToTime.Text = opp.fldRptToTime
            txtPRestrictFrTime.Text = opp.fldRestrictFrTime
            txtPRestrictToTime.Text = opp.fldRestrictToTime
            Dim officer As AdminObj = AdminManager.GetAdmin(opp.fldOverseerID)
            If Not officer Is Nothing Then
                Dim pstype As String = PoliceStationManager.GetPoliceStationType(officer.fldPoliceStationID)
                Dim ipkid As Long = 0
                If pstype.Equals("IPK") Then
                    ipkid = officer.fldPoliceStationID
                Else
                    ipkid = PoliceStationManager.GetPoliceStationIPK(officer.fldPoliceStationID)
                End If
                txtPOverseer.Text = officer.fldName
                txtPOverseerIDNo.Text = officer.fldPoliceNo
                txtPOverseerIPK.Text = PoliceStationManager.GetPoliceStationName(ipkid)
                txtPOverseerDept.Text = PoliceStationManager.GetDepartmentName(officer.fldDeptID)
                txtPOverseerContactNo.Text = officer.fldContactNo
            Else
                txtPOverseer.Text = ""
                txtPOverseerIDNo.Text = ""
                txtPOverseerIPK.Text = ""
                txtPOverseerDept.Text = ""
                txtPOverseerContactNo.Text = ""
            End If
            txtPGeofenceMukim.Text = opp.fldGeofenceMukim
            txtPEMDInstallDate.Text = opp.fldEMDInstallDate.ToString("yyyy-MM-dd")
            If opp.fldEMDDeviceID > 0 Then
                txtPEMD.Text = EMDDeviceManager.GetIMEI(opp.fldEMDDeviceID)
            Else
                txtPEMD.Text = GetText("Unassign")
            End If
            chkPSmartTag.Checked = opp.fldSmartTag = 1
            chkPOBC.Checked = opp.fldOBC = 1
            chkPBeacon.Checked = opp.fldBeacon = 1
            txtPBeaconCode.Text = opp.fldBeaconCode
            chkPCharger.Checked = opp.fldCharger = 1
            chkPStrap.Checked = opp.fldStrap = 1
            chkPCable.Checked = opp.fldCable = 1
            If Not String.IsNullOrWhiteSpace(opp.fldAttachment1) Then
                lbtPAttachment1.Text = GetText("ClickToView")
                lbtPAttachment1.OnClientClick = "OpenPopupWindow('" & opp.fldAttachment1 & "',1280,800);return false;"
            Else
                lbtPAttachment1.Text = GetText("None")
                lbtPAttachment1.OnClientClick = "return false;"
            End If
            If Not String.IsNullOrWhiteSpace(opp.fldAttachment2) Then
                lbtPAttachment2.Text = GetText("ClickToView")
                lbtPAttachment2.OnClientClick = "OpenPopupWindow('" & opp.fldAttachment2 & "',1280,800);return false;"
            Else
                lbtPAttachment2.Text = GetText("None")
                lbtPAttachment2.OnClientClick = "return false;"
            End If
            txtPRemark.Text = opp.fldRemark
            txtPStatus.Text = If(opp.fldStatus.Equals("Y"), GetText("Active"), If(opp.fldStatus.Equals("N"), GetText("Inactive"), GetText("Pending")))
            txtPStatus.CssClass = If(opp.fldStatus.Equals("Y"), "label label-success", If(opp.fldStatus.Equals("N"), "label label-danger", "label label-warning"))
            btnPApprove.Visible = opp.fldStatus.Equals("P")
        Else
            imgPPhoto1Preview.ImageUrl = "../assets/img/No_Image.png"
            imgPPhoto2Preview.ImageUrl = "../assets/img/No_Image.png"
            txtPSubjectName.Text = ""
            txtPSubjectICNo.Text = ""
            txtPSubjectContactNo.Text = ""
            txtPAddress.Text = ""
            txtPState.Text = ""
            txtPDistrict.Text = ""
            txtPMukim.Text = ""
            txtPPoliceStation.Text = ""
            txtPDepartment.Text = ""
            txtPOffenceDesc.Text = ""
            txtPActs.Text = ""
            txtPActsSection.Text = ""
            txtPOrderIssuedBy.Text = ""
            txtPOrderIssuedDate.Text = ""
            txtPOrderRefNo.Text = ""
            txtPOrderDate.Text = ""
            txtPOrderPeriod.Text = ""
            txtPRptPoliceStation.Text = ""
            txtPSDNo.Text = ""
            txtPOCSName.Text = ""
            txtPOCSContactNo.Text = ""
            txtPReportDay.Text = ""
            txtPReportFrTime.Text = ""
            txtPReportToTime.Text = ""
            txtPRestrictFrTime.Text = ""
            txtPRestrictToTime.Text = ""
            txtPOverseer.Text = ""
            txtPOverseer.Text = ""
            txtPOverseerIDNo.Text = ""
            txtPOverseerIPK.Text = ""
            txtPOverseerDept.Text = ""
            txtPOverseerContactNo.Text = ""
            txtPGeofenceMukim.Text = ""
            txtPEMDInstallDate.Text = ""
            txtPEMD.Text = ""
            chkPSmartTag.Checked = False
            chkPOBC.Checked = False
            chkPBeacon.Checked = False
            txtPBeaconCode.Text = ""
            chkPCharger.Checked = False
            chkPStrap.Checked = False
            chkPCable.Checked = False
            lbtPAttachment1.Text = GetText("None")
            lbtPAttachment1.OnClientClick = "return false;"
            lbtPAttachment2.Text = GetText("None")
            lbtPAttachment2.OnClientClick = "return false;"
            txtPRemark.Text = ""
            txtPStatus.Text = ""
            txtPStatus.CssClass = ""
            btnPApprove.Visible = False
        End If
    End Sub

    Private Sub GetSubjectData(ByVal opp As OPPObj)
        If Not String.IsNullOrWhiteSpace(opp.fldPhoto1) Then
            imgPhoto1Preview.ImageUrl = opp.fldPhoto1
            hfPhoto1Ori.Value = opp.fldPhoto1
        Else
            imgPhoto1Preview.ImageUrl = "../assets/img/No_Image.png"
            hfPhoto1Ori.Value = "../assets/img/No_Image.png"
        End If
        If Not String.IsNullOrWhiteSpace(opp.fldPhoto2) Then
            imgPhoto2Preview.ImageUrl = opp.fldPhoto2
            hfPhoto2Ori.Value = opp.fldPhoto2
        Else
            imgPhoto2Preview.ImageUrl = "../assets/img/No_Image.png"
            hfPhoto2Ori.Value = "../assets/img/No_Image.png"
        End If
        txtSubjectName.Text = opp.fldName
        txtSubjectICNo.Text = opp.fldICNo
        txtSubjectContactNo.Text = opp.fldContactNo
        txtAddress.Text = opp.fldAddress
        Try
            ddlState.SelectedValue = opp.fldState
        Catch ex As Exception
            ddlState.SelectedIndex = 0
        End Try
        ddlState_SelectedIndexChanged(Me.ddlState, EventArgs.Empty)
        Try
            ddlDistrict.SelectedValue = opp.fldDistrict
        Catch ex As Exception
            ddlDistrict.SelectedIndex = 0
        End Try
        ddlDistrict_SelectedIndexChanged(Me.ddlDistrict, EventArgs.Empty)
        Try
            ddlMukim.SelectedValue = opp.fldMukim
        Catch ex As Exception
            ddlMukim.SelectedIndex = 0
        End Try
        Try
            ddlPoliceStation.SelectedValue = opp.fldPoliceStationID
        Catch ex As Exception
            ddlPoliceStation.SelectedIndex = 0
        End Try
        ddlDepartment.SelectedValue = opp.fldDeptID
        txtOffenceDesc.Text = opp.fldOffenceDesc
        Try
            ddlActs.SelectedValue = opp.fldActsID
        Catch ex As Exception
            ddlActs.SelectedIndex = 0
        End Try
        ddlActs_SelectedIndexChanged(Me.ddlActs, EventArgs.Empty)
        Try
            ddlActsSection.SelectedValue = opp.fldActsSectionID
        Catch ex As Exception
            ddlActsSection.SelectedIndex = 0
        End Try
        ddlOrderIssuedBy.SelectedValue = opp.fldOrdParty
        hfOrderIssuedDate.Text = opp.fldOrdIssuedDate.ToString("yyyy-MM-dd")
        txtOrderRefNo.Text = opp.fldOrdRefNo
        hfOrderDate.Text = opp.fldOrdFrDate.ToString("yyyy-MM-dd")
        If opp.fldOrdYear > 0 Then
            txtOrderPeriod.Text = opp.fldOrdYear
            ddlOrderPeriodUnit.SelectedValue = "Y"
        ElseIf opp.fldOrdMonth > 0 Then
            txtOrderPeriod.Text = opp.fldOrdMonth
            ddlOrderPeriodUnit.SelectedValue = "M"
        ElseIf opp.fldOrdDay > 0 Then
            txtOrderPeriod.Text = opp.fldOrdDay
            ddlOrderPeriodUnit.SelectedValue = "D"
        End If
        Try
            ddlRptPoliceStation.SelectedValue = opp.fldRptPoliceStationID
        Catch ex As Exception
            ddlRptPoliceStation.SelectedIndex = 0
        End Try
        ddlRptPoliceStation_SelectedIndexChanged(Me.ddlRptPoliceStation, EventArgs.Empty)
        txtSDNo.Text = opp.fldSDNo
        txtOCSName.Text = opp.fldOCSName
        txtOCSContactNo.Text = opp.fldOCSTelNo
        ddlReportDay.SelectedValue = opp.fldRptDay
        ddlReportFrTime.SelectedValue = opp.fldRptFrTime
        ddlReportToTime.SelectedValue = opp.fldRptToTime
        ddlRestrictFrTime.SelectedValue = opp.fldRestrictFrTime
        ddlRestrictToTime.SelectedValue = opp.fldRestrictToTime
        If Not String.IsNullOrWhiteSpace(opp.fldAttachment1) Then
            lbtAttachment1.Text = GetText("ClickToView")
            lbtAttachment1.OnClientClick = "OpenPopupWindow('" & opp.fldAttachment1 & "',1280,800);return false;"
        Else
            lbtAttachment1.Text = GetText("None")
            lbtAttachment1.OnClientClick = "return false;"
        End If
        If Not String.IsNullOrWhiteSpace(opp.fldAttachment2) Then
            lbtAttachment2.Text = GetText("ClickToView")
            lbtAttachment2.OnClientClick = "OpenPopupWindow('" & opp.fldAttachment2 & "',1280,800);return false;"
        Else
            lbtAttachment2.Text = GetText("None")
            lbtAttachment2.OnClientClick = "return false;"
        End If
        txtRemark.Text = opp.fldRemark
    End Sub

    Private Sub GetOverseerData(ByVal opp As OPPObj)
        Try
            ddlOverseer.SelectedValue = opp.fldOverseerID
        Catch ex As Exception
            If Not opp Is Nothing AndAlso opp.fldOverseerID > 0 Then
                Dim admin As AdminObj = AdminManager.GetAdmin(opp.fldOverseerID)
                ddlOverseer.Items.Add(New ListItem(admin.fldName & If(admin.fldStatus.Equals("D"), " (" & GetText("Inactive") & ")", ""), opp.fldOverseerID))
                ddlOverseer.SelectedValue = opp.fldOverseerID
            Else
                ddlOverseer.SelectedIndex = 0
            End If
        End Try
        ddlOverseer_SelectedIndexChanged(Me.ddlOverseer, EventArgs.Empty)
    End Sub

    Private Sub GetGeofenceData(ByVal opp As OPPObj)
        Try
            ddlGeofenceMukim.SelectedValue = opp.fldGeofenceMukim
        Catch ex As Exception
            ddlGeofenceMukim.SelectedIndex = 0
        End Try
        Dim status As Integer = OPPManager.GetGeofenceStatus(opp.fldID)
        txtGeofenceStatus.Text = GetText(If(status = 1, "Active", "Inactive"))
        txtGeofenceStatus.CssClass = GetText(If(status = 1, "label label-success", "label label-danger"))
        btnGeofenceStatus.Text = GetText(If(status = 1, "Deactivate", "Activate"))
        btnGeofenceStatus.CommandArgument = If(status = 1, "deactive", "active")
    End Sub

    Private Sub GetEMDData(ByVal opp As OPPObj)
        GetDevice(opp)
        hfEMDInstallDate.Text = opp.fldEMDInstallDate.ToString("yyyy-MM-dd")
        Try
            ddlEMD.SelectedValue = opp.fldEMDDeviceID
        Catch ex As Exception
            ddlEMD.SelectedIndex = 0
        End Try
        chkSmartTag.Checked = opp.fldSmartTag = 1
        chkOBC.Checked = opp.fldOBC = 1
        chkBeacon.Checked = opp.fldBeacon = 1
        txtBeaconCode.Text = opp.fldBeaconCode
        chkCharger.Checked = opp.fldCharger = 1
        chkStrap.Checked = opp.fldStrap = 1
        chkCable.Checked = opp.fldCable = 1
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

#Region "Validation"

    Private Function PageValid(group As String) As Boolean
        Dim result As Boolean = ValidatePage(group)
        Dim errorMsg As String = ""
        Dim returnMsg As String = ""
        If group.Equals("opp") Then
            If Not ValidateFileType(fuPhoto1, {".png", ".jpeg", ".jpg", ".bmp"}, 10, False, False, returnMsg) Then
                result = False
                errorMsg &= GetText("ErrorItemRequired").Replace("vITEM", GetText("Picture") & " (" & GetText("Face") & ")") & "\n"
            End If
            If Not ValidateFileType(fuPhoto2, {".png", ".jpeg", ".jpg", ".bmp"}, 10, False, False, returnMsg) Then
                result = False
                errorMsg &= GetText("ErrorItemRequired").Replace("vITEM", GetText("Picture") & " (" & GetText("FullBody") & ")") & "\n"
            End If
        End If
        If group.Equals("attachment") Then
            If Not ValidateFileType(fuAttachment1, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, False, returnMsg) Then
                result = False
                errorMsg &= returnMsg & "\n"
            End If
            If Not ValidateFileType(fuAttachment2, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, False, returnMsg) Then
                result = False
                errorMsg &= returnMsg & "\n"
            End If
        End If
        If Not result Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorPageInvalid") & "\n" & errorMsg & "');", True)
        End If
        Return result
    End Function

#End Region

#Region "Save To DB"

    Protected Sub btnPApprove_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If OPPManager.UpdateStatus(OPPID, "P", "Y", AdminAuthentication.GetUserData(2)) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "VERIFY OPP DATA", "OPP ID: " & OPPID & ", Verify Status: Approved", "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetModalInfo(OPPManager.GetOPP(OPPID))
            BindTable()
        End If
    End Sub

    Protected Sub btnSubmitOPP_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        If Not opp Is Nothing Then
            Dim result As Boolean = True
            opp.fldName = txtSubjectName.Text
            opp.fldICNo = txtSubjectICNo.Text
            opp.fldContactNo = txtSubjectContactNo.Text
            opp.fldAddress = txtAddress.Text
            opp.fldCountryID = "MY"
            opp.fldState = ddlState.SelectedValue
            opp.fldDistrict = ddlDistrict.SelectedValue
            opp.fldMukim = ddlMukim.SelectedValue
            opp.fldPoliceStationID = ddlPoliceStation.SelectedValue
            opp.fldDeptID = ddlDepartment.SelectedValue
            opp.fldOffenceDesc = txtOffenceDesc.Text
            opp.fldActsID = ddlActs.SelectedValue
            opp.fldActsSectionID = ddlActsSection.SelectedValue
            opp.fldOrdParty = ddlOrderIssuedBy.SelectedValue
            opp.fldOrdIssuedDate = hfOrderIssuedDate.Text
            opp.fldOrdRefNo = txtOrderRefNo.Text
            opp.fldOrdFrDate = hfOrderDate.Text
            opp.fldOrdToDate = GetExpDate(hfOrderDate.Text, txtOrderPeriod.Text, ddlOrderPeriodUnit.SelectedValue)
            opp.fldOrdDay = 0
            opp.fldOrdMonth = 0
            opp.fldOrdYear = 0
            If ddlOrderPeriodUnit.SelectedValue.Equals("D") Then
                opp.fldOrdDay = txtOrderPeriod.Text
            ElseIf ddlOrderPeriodUnit.SelectedValue.Equals("M") Then
                opp.fldOrdMonth = txtOrderPeriod.Text
            ElseIf ddlOrderPeriodUnit.SelectedValue.Equals("Y") Then
                opp.fldOrdYear = txtOrderPeriod.Text
            End If
            opp.fldRptPoliceStationID = ddlRptPoliceStation.SelectedValue
            opp.fldSDNo = txtSDNo.Text
            opp.fldOCSName = txtOCSName.Text
            opp.fldOCSTelNo = txtOCSContactNo.Text
            opp.fldRptDay = ddlReportDay.SelectedValue
            opp.fldRptFrTime = ddlReportFrTime.SelectedValue
            opp.fldRptToTime = ddlReportToTime.SelectedValue
            opp.fldRestrictFrTime = ddlRestrictFrTime.SelectedValue
            opp.fldRestrictToTime = ddlRestrictToTime.SelectedValue
            opp.fldRemark = txtRemark.Text
            If result AndAlso Not fuPhoto1.PostedFile Is Nothing AndAlso fuPhoto1.PostedFile.ContentLength > 0 Then
                result = UploadDocument(fuPhoto1, "OPPA", "opp", opp.fldPhoto1)
            End If
            If result AndAlso Not fuPhoto2.PostedFile Is Nothing AndAlso fuPhoto2.PostedFile.ContentLength > 0 Then
                result = UploadDocument(fuPhoto2, "OPPB", "opp", opp.fldPhoto2)
            End If
            If result AndAlso Not fuAttachment1.PostedFile Is Nothing AndAlso fuAttachment1.PostedFile.ContentLength > 0 Then
                result = UploadDocument(fuAttachment1, "Lampiran1", "attachment", opp.fldAttachment1)
            End If
            If result AndAlso Not fuAttachment2.PostedFile Is Nothing AndAlso fuAttachment2.PostedFile.ContentLength > 0 Then
                result = UploadDocument(fuAttachment2, "Lampiran2", "attachment", opp.fldAttachment2)
            End If
            If result Then result = OPPManager.Save(opp, AdminAuthentication.GetUserData(2), 0)
            If result Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP DETAILS", "OPP ID: " & OPPID, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                GetSubjectData(opp)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Protected Sub btnSubmitOverseer_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If OPPManager.UpdateOverseerID(OPPID, ddlOverseer.SelectedValue) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP OVERSEER", "OPP ID: " & OPPID & ", Overseer ID: " & ddlOverseer.SelectedValue, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

    Protected Sub btnSubmitEMD_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If OPPManager.UpdateEMDDevice(OPPID, ddlEMD.SelectedValue, hfEMDInstallDate.Text, txtBeaconCode.Text, If(chkSmartTag.Checked, 1, 0), If(chkOBC.Checked, 1, 0), If(chkBeacon.Checked, 1, 0), If(chkCharger.Checked, 1, 0), If(chkStrap.Checked, 1, 0), If(chkCable.Checked, 1, 0), AdminAuthentication.GetUserData(2)) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP EMD Device", "OPP ID: " & OPPID & ", Device ID: " & ddlEMD.SelectedValue, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

    Protected Sub btnSubmitGeofence_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If OPPManager.UpdateGeofenceMukim(OPPID, ddlGeofenceMukim.SelectedValue) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP GEOFENCE MUKIM", "OPP ID: " & OPPID & ", MUKIM: " & ddlGeofenceMukim.SelectedValue, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

    Protected Sub btnGeofenceStatus_Click(sender As Object, e As EventArgs)
        If sender.CommandArgument.Equals("active") Then
            If OPPManager.UpdateGeofenceStatus(OPPID, 1) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP GEOFENCE STATUS", "OPP ID: " & OPPID & ", Status: Activate", "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                Dim status As Integer = OPPManager.GetGeofenceStatus(OPPID)
                txtGeofenceStatus.Text = GetText(If(status = 1, "Active", "Inactive"))
                txtGeofenceStatus.CssClass = GetText(If(status = 1, "label label-success", "label label-danger"))
                btnGeofenceStatus.Text = GetText(If(status = 1, "Deactivate", "Activate"))
                btnGeofenceStatus.CommandArgument = If(status = 1, "deactive", "active")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        ElseIf sender.CommandArgument.Equals("deactive") Then
            If OPPManager.UpdateGeofenceStatus(OPPID, 0) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE OPP GEOFENCE STATUS", "OPP ID: " & OPPID & ", Status: Deactivate", "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                Dim status As Integer = OPPManager.GetGeofenceStatus(OPPID)
                txtGeofenceStatus.Text = GetText(If(status = 1, "Active", "Inactive"))
                txtGeofenceStatus.CssClass = GetText(If(status = 1, "label label-success", "label label-danger"))
                btnGeofenceStatus.Text = GetText(If(status = 1, "Deactivate", "Activate"))
                btnGeofenceStatus.CommandArgument = If(status = 1, "deactive", "active")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Private Function GetExpDate(ByVal orderdate As String, ByVal period As Integer, ByVal unit As String) As Date
        Dim days As Integer = 0
        Dim months As Integer = 0
        Dim years As Integer = 0
        If unit.Equals("D") Then days = period
        If unit.Equals("M") Then months = period
        If unit.Equals("Y") Then years = period
        Return CDate(orderdate).AddYears(years).AddMonths(months).AddDays(days)
    End Function

    Private Function UploadDocument(ByVal fuFile As FileUpload, ByVal prefix As String, ByVal folder As String, ByRef FilePath As String) As Boolean
        Dim result As Boolean = True
        Dim oldFilePath As String = ""
        Dim datetime As DateTime = UtilityManager.GetServerDateTime
        'Dim newSize As New System.Drawing.Size(500, 500)
        Try
            If Not fuFile.PostedFile Is Nothing AndAlso fuFile.PostedFile.ContentLength > 0 Then
                FilePath = "../" & ValidateFilePath("upload/" & folder & "/", prefix & "_" & datetime.ToString("yyMMddHHmmss") & "_" & AdminAuthentication.GetUserData(2) & UtilityManager.GenerateRandomNumber(3), Path.GetExtension(fuFile.PostedFile.FileName).ToLower())
                If Path.GetExtension(fuFile.PostedFile.FileName).ToLower() <> ".pdf" Then
                    Dim oriImage As System.Drawing.Image = System.Drawing.Image.FromStream(fuFile.PostedFile.InputStream)
                    'newSize = UtilityManager.AspectRatioSize(oriImage.Size, newSize)
                    'Dim resizedImg As System.Drawing.Image = UtilityManager.ResizeImage(oriImage, newSize.Width, newSize.Height)
                    If File.Exists(Server.MapPath(FilePath)) Then
                        File.Delete(Server.MapPath(FilePath))
                    End If
                    'resizedImg.Save(Server.MapPath("../" & FilePath), UtilityManager.GetEncoder(ImageFormat.Jpeg), UtilityManager.GetEncoderParameters(50))
                    oriImage.Save(Server.MapPath(FilePath), UtilityManager.GetEncoder(ImageFormat.Jpeg), UtilityManager.GetEncoderParameters(50))
                Else
                    If File.Exists(Server.MapPath(FilePath)) Then
                        File.Delete(Server.MapPath(FilePath))
                    End If
                    fuFile.PostedFile.SaveAs(Server.MapPath(FilePath))
                End If
                'If Not String.IsNullOrEmpty(oldFilePath) AndAlso File.Exists(Server.MapPath("../" & oldFilePath)) Then
                '    File.Delete(Server.MapPath("../" & oldFilePath))
                'End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUploadFailed") & "');", True)
            result = False
        End Try
        Return result
    End Function

#End Region

#Region "Reset"

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/admins/AddOPP.aspx")
    End Sub

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        txtSName.Text = ""
        txtSICNo.Text = ""
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        plTable.Visible = True
        plUpdate.Visible = False
    End Sub

    Protected Sub btnResetOPP_Click(sender As Object, e As EventArgs)
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        GetSubjectData(opp)
    End Sub

    Protected Sub btnResetOverseer_Click(sender As Object, e As EventArgs)
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        GetOverseerData(opp)
    End Sub

    Protected Sub btnResetGeofence_Click(sender As Object, e As EventArgs)
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        GetGeofenceData(opp)
    End Sub

    Protected Sub btnResetEMD_Click(sender As Object, e As EventArgs)
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        GetEMDData(opp)
    End Sub

    Protected Sub btnPCancel_Click(sender As Object, e As EventArgs)
        GetModalInfo(Nothing)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#plVerify').modal('hide');", True)
    End Sub

#End Region

End Class