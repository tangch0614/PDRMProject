Imports System.Drawing.Imaging
Imports System.IO
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAddOPP
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
        lblPageTitle.Text = GetText("AddOPP")
        lblHeader.Text = GetText("AddOPP")
        'case
        'lblCaseFile.Text = GetText("Fail Kes")
        'lblCaseFileNo.Text = GetText("No. Fail Kes")
        'lblOfficerName.Text = GetText("Pengawai Fail Kes")
        'lblOfficerContactNo.Text = GetText("ContactNum")
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
        'Buttons/Message
        btnSubmit.Text = GetText("Add")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("AddOPP").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
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
        GetDevice()
        GetOverseer()
        'GetStatus()
        Dim dateTime As DateTime = UtilityManager.GetServerDateTime()
        hfOrderDate.Text = dateTime.ToString("yyyy-MM-dd")
        hfOrderIssuedDate.Text = dateTime.ToString("yyyy-MM-dd")
        hfEMDInstallDate.Text = dateTime.ToString("yyyy-MM-dd")
        ddlActs_SelectedIndexChanged(Me.ddlActs, EventArgs.Empty)
        ddlState_SelectedIndexChanged(Me.ddlState, EventArgs.Empty)
    End Sub

    Private Sub GetDepartment()
        Dim datatable As DataTable = PoliceStationManager.GetDepartmentList("Y")
        ddlDepartment.DataSource = datatable
        ddlDepartment.DataTextField = "fldName"
        ddlDepartment.DataValueField = "fldID"
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Department")), -1))
        ddlDepartment.SelectedIndex = 0
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

    'Private Sub GetStatus()
    '    ddlStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
    '    ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
    '    ddlStatus.SelectedIndex = 0
    'End Sub

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

    Private Sub GetDevice()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "N")
        ddlEMD.DataTextField = "fldImei"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        ddlEMD.Items.Insert(0, New ListItem(GetText("Unassign"), 0, True))
        ddlEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOverseer()
        ddlOverseer.DataSource = AdminManager.SearchAdminList(-1, 3, -1, "", "", "", "", -1, -1, "", "A", "", "")
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

#Region "Validation"

    Private Function PageValid() As Boolean
        Dim result As Boolean = ValidatePage("opp")
        Dim errorMsg As String = ""
        Dim returnMsg As String = ""
        If Not ValidateFileType(fuPhoto1, {".png", ".jpeg", ".jpg", ".bmp"}, 10, False, False, returnMsg) Then
            result = False
            errorMsg &= GetText("ErrorItemRequired").Replace("vITEM", GetText("Picture") & " (" & GetText("Face") & ")") & "\n"
        End If
        If Not ValidateFileType(fuPhoto2, {".png", ".jpeg", ".jpg", ".bmp"}, 10, False, False, returnMsg) Then
            result = False
            errorMsg &= GetText("ErrorItemRequired").Replace("vITEM", GetText("Picture") & " (" & GetText("FullBody") & ")") & "\n"
        End If
        If Not ValidateFileType(fuAttachment1, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, False, returnMsg) Then
            result = False
            errorMsg &= returnMsg & "\n"
        End If
        If Not ValidateFileType(fuAttachment2, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, False, returnMsg) Then
            result = False
            errorMsg &= returnMsg & "\n"
        End If
        If Not result Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorPageInvalid") & "\n" & errorMsg & "');", True)
        End If
        Return result
    End Function

#End Region

#Region "Save To DB"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If PageValid() Then
            Dim opp As OPPObj = New OPPObj
            Dim attachment1 As String = ""
            Dim attachment2 As String = ""
            Dim attactment As List(Of String) = New List(Of String)
            If UploadDocument(fuPhoto1, "OPPA_", "_" & AdminAuthentication.GetUserData(2) & UtilityManager.GenerateRandomNumber(3), "opp", True, opp.fldPhoto1) _
                AndAlso UploadDocument(fuPhoto2, "OPPB_", "_" & AdminAuthentication.GetUserData(2) & UtilityManager.GenerateRandomNumber(3), "opp", True, opp.fldPhoto2) _
                AndAlso UploadDocument(fuAttachment1, "", "_" & txtSubjectName.Text, "attachment", False, attachment1) _
                AndAlso UploadDocument(fuAttachment2, "", "_" & txtSubjectName.Text, "attachment", False, attachment2) Then
                Dim datetime As DateTime = UtilityManager.GetServerDateTime()
                If Not String.IsNullOrWhiteSpace(attachment1) Then attactment.Add(attachment1)
                If Not String.IsNullOrWhiteSpace(attachment2) Then attactment.Add(attachment2)
                opp.fldAttachment1 = String.Join(",", attactment)
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
                opp.fldOverseerID = ddlOverseer.SelectedValue
                opp.fldRestrictFrTime = ddlRestrictFrTime.SelectedValue
                opp.fldRestrictToTime = ddlRestrictToTime.SelectedValue
                opp.fldGeofenceMukim = ddlGeofenceMukim.SelectedValue
                opp.fldEMDInstallDate = hfEMDInstallDate.Text
                opp.fldEMDDeviceID = ddlEMD.SelectedValue
                opp.fldSmartTag = If(chkSmartTag.Checked, 1, 0)
                opp.fldOBC = If(chkOBC.Checked, 1, 0)
                opp.fldBeacon = If(chkBeacon.Checked, 1, 0)
                opp.fldBeaconCode = If(chkBeacon.Checked, txtBeaconCode.Text, "")
                opp.fldCharger = If(chkCharger.Checked, 1, 0)
                opp.fldStrap = If(chkStrap.Checked, 1, 0)
                opp.fldCable = If(chkCable.Checked, 1, 0)
                opp.fldRemark = txtRemark.Text
                opp.fldStatus = "P"
                If OPPManager.Save(opp, AdminAuthentication.GetUserData(2), opp.fldID) Then
                    UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ADD OPP", "OPP ID: " & opp.fldID & ", OPP Name: " & opp.fldName, "")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgSubmitSuccess") & "');window.location.href='../admins/OPPList.aspx'", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorSubmitFailed") & "');", True)
                End If
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

    Private Function UploadDocument(ByVal fuFile As FileUpload, ByVal prefix As String, ByVal suffix As String, ByVal folder As String, ByVal genfilename As Boolean, ByRef FilePath As String) As Boolean
        Dim result As Boolean = True
        Dim oldFilePath As String = ""
        Dim datetime As DateTime = UtilityManager.GetServerDateTime
        'Dim newSize As New System.Drawing.Size(500, 500)
        Try
            If Not fuFile.PostedFile Is Nothing AndAlso fuFile.PostedFile.ContentLength > 0 Then
                If genfilename Then
                    FilePath = ValidateFilePath("../" & "upload/" & folder & "/", UtilityManager.EscapeFileName(prefix & datetime.ToString("yyMMddHHmmss") & suffix).Replace(" ", "_"), Path.GetExtension(fuFile.PostedFile.FileName).ToLower())
                Else
                    FilePath = ValidateFilePath("../" & "upload/" & folder & "/", UtilityManager.EscapeFileName(prefix & Path.GetFileNameWithoutExtension(fuFile.PostedFile.FileName) & suffix).Replace(" ", "_"), Path.GetExtension(fuFile.PostedFile.FileName).ToLower())
                End If
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
End Class