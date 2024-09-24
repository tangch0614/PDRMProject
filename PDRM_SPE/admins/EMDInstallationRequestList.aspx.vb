Imports System.Drawing.Imaging
Imports System.IO
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AEMDInstallationRequestList
    Inherits ABase

    Private Property RequestID() As Long
        Get
            If Not ViewState("RequestID") Is Nothing Then
                Return CLng(ViewState("RequestID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("RequestID") = value
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
        lblPageTitle.Text = GetText("InstallationRequestList")
        lblHeader.Text = GetText("InstallationRequestList")
        'Search
        lblSDate.Text = GetText("Date")
        lblSPoliceStation.Text = GetText("PoliceStation")
        lblSDepartment.Text = GetText("Department")
        lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'OFFICER department
        lblInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("InstallationRequest"))
        lblDepartmentInfo.Text = GetText("Department")
        lblDepartment.Text = GetText("Department")
        rfvDepartment.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Department"))
        'OFFICER install info
        lblInstallInfo.Text = GetText("InstallationInfo")
        lblDate.Text = GetText("Date")
        lblTime.Text = GetText("Time")
        rfvTime.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Time"))
        'OFFICER install location
        lblInstallLocation.Text = GetText("InstallationLocation")
        lblState.Text = GetText("State")
        'rfvState.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("State"))
        'lblDistrict.Text = GetText("District")
        'rfvDistrict.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("District"))
        lblMukim.Text = GetText("Township")
        rfvMukim.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Township"))
        lblIPK.Text = GetText("Contingent")
        rfvIPK.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Contingent"))
        lblPoliceStation.Text = GetText("PoliceStation")
        rfvPoliceStation.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("PoliceStation"))
        'OFFICER ocs info
        lblOCSInfo.Text = GetText("OCSInfo")
        lblOCSName.Text = GetText("OCSItem").Replace("vITEM", GetText("Name"))
        rfvOCSName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCSItem").Replace("vITEM", GetText("Name")))
        lblOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        rfvOCSContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCSItem").Replace("vITEM", GetText("ContactNum")))
        'OFFICER Others Info
        lblOtherInfo.Text = GetText("OthersInfo")
        lblRemark.Text = GetText("Remark")
        'OFFICER file upload
        lblFileInfo.Text = GetText("Attachment")
        lblAttachment1.Text = GetText("Attachment")
        btnAttachment1.Text = GetText("SelectItem").Replace("vITEM", GetText("File"))
        btnSubmitAttachment1.Text = GetText("Submit")
        'VENDOR department
        lblPInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("InstallationRequest"))
        lblPDepartmentInfo.Text = GetText("Department")
        lblPDepartment.Text = GetText("Department")
        lblPOfficerName.Text = GetText("OfficerItem").Replace("vITEM", GetText("Name"))
        lblPOfficerContactNo.Text = GetText("OfficerItem").Replace("vITEM", GetText("ContactNum"))
        'VENDOR install info
        lblPInstallInfo.Text = GetText("InstallationInfo")
        lblPDate.Text = GetText("Date")
        lblPTime.Text = GetText("Time")
        'VENDOR install location
        lblPInstallLocation.Text = GetText("InstallationLocation")
        lblPMukim.Text = GetText("State") & "/" & GetText("Township")
        lblPIPK.Text = GetText("Contingent")
        lblPPoliceStation.Text = GetText("PoliceStation")
        'VENDOR ocs info
        lblPOCSInfo.Text = GetText("OCSInfo")
        lblPOCSName.Text = GetText("OCSItem").Replace("vITEM", GetText("Name"))
        lblPOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        'VENDOR others info
        lblPOtherInfo.Text = GetText("OthersInfo")
        lblPAttachment1.Text = GetText("Attachment")
        'VENDOR emd info
        lblPEMDDeviceInfo.Text = GetText("EMD")
        lblPOPP.Text = GetText("OPP")
        lblPEMD.Text = GetText("EMD")
        chkPBeacon.Text = GetText("SmartTag")
        chkPOBC.Text = GetText("OBC")
        chkPBeacon.Text = GetText("Beacon")
        chkPCharger.Text = GetText("Charger")
        chkPStrap.Text = GetText("Strap")
        chkPCable.Text = GetText("Cable")
        'VENDOR Status info
        lblPStatusInfo.Text = GetText("Status")
        lblPRemark2.Text = GetText("Remark")
        lblPStatus.Text = GetText("Status")
        'VENDOR file upload
        lblPAttachment2.Text = GetText("Picture")
        lblPFileInfo.Text = GetText("Picture")
        btnPAttachment2.Text = GetText("SelectItem").Replace("vITEM", GetText("MultipleFile"))
        btnPSubmitAttachment2.Text = GetText("Submit")
        'Buttons/Message
        btnPUpdateStatus.Text = GetText("Update")
        btnPUpdateEMD.Text = GetText("Update")
        btnPBackTop.Text = GetText("Back")
        btnPBackBottom.Text = GetText("Back")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        btnBack.Text = GetText("Back")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        hfConfirm2.Value = GetText("MsgConfirm")
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetStatus()
        GetDepartment()
        GetPoliceStation()
        GetState()
        GetIPK("")
        GetTime()
        Dim dateTime As DateTime = UtilityManager.GetServerDateTime()
        hfDateFrom.Text = dateTime.ToString("yyyy-MM-dd")
        hfDateTo.Text = dateTime.AddMonths(6).ToString("yyyy-MM-dd")
        hfDate.Text = dateTime.ToString("yyyy-MM-dd")
        ddlIPK_SelectedIndexChanged(Me.ddlIPK, EventArgs.Empty)
        BindTable()
    End Sub

    Private Sub GetStatus()
        ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Pending"), "P", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Acknowledge"), "A", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Completed"), "Y", True))
        'ddlSStatus.Items.Add(New ListItem(GetText("Rejected"), "N", True))
        If (AdminAuthentication.GetUserData(5) = 1) Then
            ddlSStatus.SelectedValue = "P"
        Else
            ddlSStatus.SelectedIndex = 0
        End If

        ddlPStatus.Items.Add(New ListItem(GetText("Pending"), "P", True))
        ddlPStatus.Items.Add(New ListItem(GetText("Acknowledge"), "A", True))
        ddlPStatus.Items.Add(New ListItem(GetText("Completed"), "Y", True))
        ddlPStatus.SelectedIndex = 0
    End Sub

    Private Sub GetTime()
        Dim startTime As DateTime = DateTime.ParseExact("00:00:00", "HH:mm:ss", Nothing)
        Dim endTime As DateTime = DateTime.ParseExact("23:30:00", "HH:mm:ss", Nothing)

        While startTime <= endTime
            ' Add time to dropdown list
            ddlTime.Items.Add(New ListItem(startTime.ToString("HH:mm"), startTime.ToString("HH:mm:ss")))
            ' Increment time by 5 minutes
            startTime = startTime.AddMinutes(30)
        End While
        ddlTime.SelectedValue = "08:00:00"
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

    'Private Sub GetDistrict(ByVal state As String)
    '    ddlDistrict.Items.Clear()
    '    ddlDistrict.DataSource = CountryManager.GetDistrictList(state)
    '    ddlDistrict.DataTextField = "fldDistrict"
    '    ddlDistrict.DataValueField = "fldDistrict"
    '    ddlDistrict.DataBind()
    '    ddlDistrict.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("District")), "", True))
    '    ddlDistrict.SelectedIndex = 0
    'End Sub

    Private Sub GetMukim(ByVal state As String, ByVal district As String)
        ddlMukim.Items.Clear()
        ddlMukim.DataSource = CountryManager.GetMukimList(state, district)
        ddlMukim.DataTextField = "fldMukim"
        ddlMukim.DataValueField = "fldMukim"
        ddlMukim.DataBind()
        ddlMukim.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Township")), "", True))
        ddlMukim.SelectedIndex = 0
    End Sub

    Private Sub GetIPK(ByVal state As String)
        ddlIPK.Items.Clear()
        ddlIPK.DataSource = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "IPK", state, "", "", "Y")
        ddlIPK.DataTextField = "fldName"
        ddlIPK.DataValueField = "fldID"
        ddlIPK.DataBind()
        ddlIPK.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Contingent")), -1, True))
        ddlIPK.SelectedIndex = 0
    End Sub

    Private Sub GetPoliceStation(ByVal ipkid As Long, ByVal state As String, ByVal district As String, ByVal mukim As String)
        ddlPoliceStation.Items.Clear()
        ddlPoliceStation.DataSource = PoliceStationManager.GetPoliceStationList(-1, ipkid, -1, "Balai", state, district, mukim, "Y")
        ddlPoliceStation.DataTextField = "fldName"
        ddlPoliceStation.DataValueField = "fldID"
        ddlPoliceStation.DataBind()
        ddlPoliceStation.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("PoliceStation")), -1, True))
        ddlPoliceStation.SelectedIndex = 0
    End Sub

    Private Sub GetPoliceStation()
        ddlSPoliceStation.DataSource = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "", "", "", "", "Y")
        ddlSPoliceStation.DataTextField = "fldName"
        ddlSPoliceStation.DataValueField = "fldID"
        ddlSPoliceStation.DataBind()
        ddlSPoliceStation.Items.Insert(0, New ListItem(GetText("All"), -1, True))
        ddlSPoliceStation.SelectedIndex = 0
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

    'Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    GetDistrict(ddlState.SelectedValue)
    '    ddlDistrict_SelectedIndexChanged(Me.ddlDistrict, EventArgs.Empty)
    'End Sub

    'Protected Sub ddlDistrict_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    GetMukim(ddlState.SelectedValue, ddlDistrict.SelectedValue)
    '    GetIPK(ddlState.SelectedValue)
    '    ddlIPK_SelectedIndexChanged(Me.ddlDistrict, EventArgs.Empty)
    'End Sub

    Protected Sub ddlIPK_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetPoliceStation(ddlIPK.SelectedValue, "", "", "")
        Dim state As String = ""
        If ddlIPK.SelectedIndex > 0 Then state = PoliceStationManager.GetState(ddlIPK.SelectedValue)
        Try
            ddlState.SelectedValue = state
        Catch ex As Exception
            ddlState.SelectedIndex = 0
        End Try
        GetMukim(state, "")
    End Sub

    Private Sub GetEMDDevice()
        ddlPEMD.Items.Clear()
        ddlPEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlPEMD.DataTextField = "fldImei"
        ddlPEMD.DataValueField = "fldID"
        ddlPEMD.DataBind()
        ddlPEMD.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("EMD")), 0))
        ddlPEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOPPList()
        ddlPOPP.Items.Clear()
        Dim datatable As DataTable = OPPManager.GetOPPList(-1, "Y")
        datatable.Columns.Add("fldNameIC", GetType(String), "fldName + '-' + fldICNo")
        ddlPOPP.DataSource = datatable
        ddlPOPP.DataTextField = "fldNameIC"
        ddlPOPP.DataValueField = "fldID"
        ddlPOPP.DataBind()
        ddlPOPP.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("OPP")), 0))
    End Sub

#End Region

#Region "Table binding"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        BindTable()
        plOfficer.Visible = False
        plVendor.Visible = False
        plTable.Visible = True
    End Sub

    Private Sub BindTable()
        Dim creatorid As Long = -1
        If AdminAuthentication.GetUserData(5) = 3 Then
            creatorid = AdminAuthentication.GetUserData(2)
        End If
        Dim myDataTable As DataTable = EMDInstallRequestManager.GetInstallRequestList(-1, creatorid, ddlSDepartment.SelectedValue, -1, ddlSPoliceStation.SelectedValue, ddlSStatus.SelectedValue, hfDateFrom.Text, hfDateTo.Text, "", "")
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        RequestID = 0
        plOfficer.Visible = False
        plVendor.Visible = False
        plTable.Visible = True
        If e.CommandName.Equals("updaterequest") Then 'AndAlso AdminAuthentication.GetUserData(5) = 3 
            RequestID = e.CommandArgument
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            GetOfficerFormInfo(installrequest)
            GetOfficerFileData(installrequest)
        ElseIf e.CommandName.Equals("processrequest") Then 'AndAlso AdminAuthentication.GetUserData(5) = 1
            RequestID = e.CommandArgument
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            GetEMDDevice()
            GetOPPList()
            GetVendorFormInfo(installrequest)
            GetVendorFileData(installrequest)
        End If
    End Sub

    Protected Function GetUserType() As Long
        Return AdminAuthentication.GetUserData(5)
    End Function

#End Region

#Region "Officer Update Form"

    Private Sub GetOfficerFormInfo(ByVal installrequest As EMDInstallRequestObj)
        If Not installrequest Is Nothing Then
            plTable.Visible = False
            plOfficer.Visible = True
            ddlDepartment.SelectedValue = installrequest.fldDeptID
            hfDate.Text = installrequest.fldInstallDateTime.ToString("yyyy-MM-dd")
            ddlTime.SelectedValue = installrequest.fldInstallDateTime.ToString("HH:mm:ss")
            Try
                ddlIPK.SelectedValue = installrequest.fldIPKID
            Catch ex As Exception
                ddlIPK.SelectedIndex = 0
            End Try
            ddlIPK_SelectedIndexChanged(Me.ddlIPK, EventArgs.Empty)
            Try
                ddlPoliceStation.SelectedValue = installrequest.fldPSID
            Catch ex As Exception
                ddlPoliceStation.SelectedIndex = 0
            End Try
            Try
                ddlState.SelectedValue = installrequest.fldState
            Catch ex As Exception
                ddlState.SelectedIndex = 0
            End Try
            Try
                ddlMukim.SelectedValue = installrequest.fldMukim
            Catch ex As Exception
                ddlMukim.SelectedIndex = 0
            End Try
            txtOCSName.Text = installrequest.fldOCSName
            txtOCSContactNo.Text = installrequest.fldOCSTelNo
            txtRemark.Text = installrequest.fldRemark
            btnSubmit.Visible = installrequest.fldStatus.Equals("P")
            plAttachment1.Visible = installrequest.fldStatus.Equals("P")
        Else
            plTable.Visible = True
            plOfficer.Visible = False
            ddlDepartment.SelectedIndex = 0
            hfDate.Text = UtilityManager.GetServerDateTime.ToString("yyyy-MM-dd")
            ddlTime.SelectedValue = "08:00:00"
            ddlIPK.SelectedIndex = 0
            ddlIPK_SelectedIndexChanged(Me.ddlIPK, EventArgs.Empty)
            ddlPoliceStation.SelectedIndex = 0
            ddlState.SelectedIndex = 0
            ddlMukim.SelectedIndex = 0
            txtOCSName.Text = ""
            txtOCSContactNo.Text = ""
            txtRemark.Text = ""
        End If
    End Sub

    Private Sub GetOfficerFileData(ByVal installrequest As EMDInstallRequestObj)
        rptAttachment1.DataSource = ""
        rptAttachment1.DataBind()
        If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment1) Then
            Dim fileList As String() = installrequest.fldAttachment1.Split(",")
            Dim fileDetails As New List(Of Object)
            For Each filePath As String In fileList
                fileDetails.Add(New With {
                    .FileName = Path.GetFileName(filePath),
                    .FileType = Path.GetExtension(filePath).Replace(".", "").ToLower(),
                    .FilePath = filePath
                })
            Next
            rptAttachment1.DataSource = fileDetails
            rptAttachment1.DataBind()
        End If
    End Sub

    Protected Sub rptAttachment1_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me)
            Dim btnDelete As LinkButton = e.Item.FindControl("btnDelete")
            scriptManager.RegisterAsyncPostBackControl(btnDelete)
        End If
    End Sub

    Protected Sub rptAttachment1_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("deletefile") Then
            Try
                Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
                Dim fileList As List(Of String) = installrequest.fldAttachment1.Split(",").ToList
                Dim filepath As String = fileList(e.CommandArgument)

                Dim fullServerPath As String = Server.MapPath(filepath)
                If File.Exists(fullServerPath) Then
                    File.Delete(fullServerPath)
                End If
                fileList.RemoveAt(e.CommandArgument)
                Dim fileListString As String = ""
                If fileList.Count > 0 Then fileListString = String.Join(",", fileList)
                If EMDInstallRequestManager.UpdateAttachment1(installrequest.fldID, fileListString) Then
                    UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "DELETE EMD INSTALLATION REQUEST ATTACHMENT 1", "Request ID: " & installrequest.fldID & ", Removed Attachment Name: " & Path.GetFileNameWithoutExtension(filepath), "")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgDeleteSuccess") & "');", True)
                    GetOfficerFileData(EMDInstallRequestManager.GetInstallRequest(RequestID))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorDeleteFailed") & "');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorDeleteFailed") & "');", True)
            End Try
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
        If Not installrequest Is Nothing Then
            If UpdateOfficerRequestInfo(installrequest) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST", "ID: " & RequestID, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                GetOfficerFormInfo(EMDInstallRequestManager.GetInstallRequest(RequestID))
                BindTable()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Private Function UpdateOfficerRequestInfo(ByVal installrequest As EMDInstallRequestObj) As Boolean
        Dim result As Boolean = True
        installrequest.fldDeptID = ddlDepartment.SelectedValue
        installrequest.fldInstallDateTime = CDate(hfDate.Text & " " & ddlTime.SelectedValue)
        installrequest.fldIPKID = ddlIPK.SelectedValue
        installrequest.fldPSID = ddlPoliceStation.SelectedValue
        installrequest.fldState = ddlState.SelectedValue
        installrequest.fldMukim = ddlMukim.SelectedValue
        installrequest.fldOCSName = txtOCSName.Text
        installrequest.fldOCSTelNo = txtOCSContactNo.Text
        installrequest.fldRemark = txtRemark.Text
        If result Then result = EMDInstallRequestManager.Save(installrequest) > 0
        Return result
    End Function

    Protected Sub btnSubmitAttachment1_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If ValidateFileType(fuAttachment1, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, True, "") Then
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            If Not installrequest Is Nothing Then
                Dim attachment As String = ""
                If fuAttachment1.HasFiles Then
                    If UploadFile(fuAttachment1, "../upload/attachment/", "Lampiran_", "_" & UtilityManager.GetServerDateTime.ToString("yMd_Hms"), False, attachment) Then
                        If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment1) Then attachment = installrequest.fldAttachment1 & "," & attachment
                        If EMDInstallRequestManager.UpdateAttachment1(RequestID, attachment) Then
                            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST ATTACHMENT 1", "Request ID: " & RequestID, "")
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                            GetOfficerFileData(EMDInstallRequestManager.GetInstallRequest(RequestID))
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "Vendor Update Form"

    Private Sub GetVendorFormInfo(ByVal installrequest As EMDInstallRequestObj)
        If Not installrequest Is Nothing Then
            plTable.Visible = False
            plVendor.Visible = True
            txtPDepartment.Text = PoliceStationManager.GetDepartmentName(installrequest.fldDeptID)
            txtPDate.Text = installrequest.fldInstallDateTime.ToString("yyyy-MM-dd")
            txtPTime.Text = installrequest.fldInstallDateTime.ToString("HH:mm")
            txtPIPK.Text = PoliceStationManager.GetPoliceStationName(installrequest.fldIPKID)
            txtPPoliceStation.Text = PoliceStationManager.GetPoliceStationName(installrequest.fldPSID)
            txtPMukim.Text = installrequest.fldState & " / " & installrequest.fldMukim
            txtPOCSName.Text = installrequest.fldOCSName
            txtPOCSContactNo.Text = installrequest.fldOCSTelNo
            If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment1) Then
                Dim fileList As String() = installrequest.fldAttachment1.Split(",")
                Dim fileDetails As New List(Of Object)
                For Each filePath As String In fileList
                    fileDetails.Add(New With {
                        .FileName = Path.GetFileName(filePath),
                        .FileType = Path.GetExtension(filePath).Replace(".", "").ToLower(),
                        .FilePath = filePath
                        })
                Next
                rptPAttachment1.DataSource = fileDetails
                rptPAttachment1.DataBind()
            Else
                rptPAttachment1.DataSource = ""
                rptPAttachment1.DataBind()
            End If
            txtPRemark.Text = installrequest.fldRemark
            Dim officer As AdminObj = AdminManager.GetAdmin(installrequest.fldCreatorID)
            txtPOfficerName.Text = officer.fldName
            txtPOfficerContactNo.Text = officer.fldContactNo
            txtPRemark2.Text = installrequest.fldProcessRemark
            ddlPStatus.SelectedValue = installrequest.fldStatus
            'emd data
            Try
                ddlPOPP.SelectedValue = installrequest.fldOPPID
            Catch ex As Exception
                ddlPOPP.SelectedIndex = 0
            End Try
            Try
                ddlPEMD.SelectedValue = installrequest.fldEMDDeviceID
            Catch ex As Exception
                ddlPEMD.SelectedIndex = 0
            End Try
            chkPSmartTag.Checked = installrequest.fldSmartTag = 1
            txtPSmartTagCode.Text = installrequest.fldSmartTagCode
            chkPOBC.Checked = installrequest.fldOBC = 1
            txtPOBCCode.Text = installrequest.fldOBCCode
            chkPBeacon.Checked = installrequest.fldBeacon = 1
            txtPBeaconCode.Text = installrequest.fldBeaconCode
            chkPCharger.Checked = installrequest.fldCharger = 1
            txtPChargerCode.Text = installrequest.fldChargerCode
            chkPStrap.Checked = installrequest.fldStrap = 1
            txtPStrapCode.Text = installrequest.fldStrapCode
            chkPCable.Checked = installrequest.fldCable = 1
            txtPCableCode.Text = installrequest.fldCableCode
        Else
            plTable.Visible = True
            plVendor.Visible = False
            txtPDepartment.Text = ""
            txtPDate.Text = ""
            txtPTime.Text = ""
            txtPIPK.Text = ""
            txtPPoliceStation.Text = ""
            txtPMukim.Text = ""
            txtPOCSName.Text = ""
            txtPOCSContactNo.Text = ""
            rptPAttachment1.DataSource = ""
            rptPAttachment1.DataBind()
            txtPRemark.Text = ""
            txtPOfficerName.Text = ""
            txtPOfficerContactNo.Text = ""
            txtPRemark2.Text = ""
            ddlPStatus.SelectedIndex = 0
            ddlPOPP.SelectedIndex = 0
            ddlPEMD.SelectedIndex = 0
            chkPSmartTag.Checked = False
            txtPSmartTagCode.Text = ""
            chkPOBC.Checked = False
            txtPOBCCode.Text = ""
            chkPBeacon.Checked = False
            txtPBeaconCode.Text = ""
            chkPCharger.Checked = False
            txtPChargerCode.Text = ""
            chkPStrap.Checked = False
            txtPStrapCode.Text = ""
            chkPCable.Checked = False
            txtPCableCode.Text = ""
        End If
    End Sub

    Private Sub GetVendorFileData(ByVal installrequest As EMDInstallRequestObj)
        rptPAttachment2.DataSource = ""
        rptPAttachment2.DataBind()
        If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment2) Then
            Dim fileList As String() = installrequest.fldAttachment2.Split(",")
            Dim fileDetails As New List(Of Object)
            For Each filePath As String In fileList
                fileDetails.Add(New With {
                    .FileName = Path.GetFileName(filePath),
                    .FileType = Path.GetExtension(filePath).Replace(".", "").ToLower(),
                    .FilePath = filePath
                })
            Next
            rptPAttachment2.DataSource = fileDetails
            rptPAttachment2.DataBind()
        End If
    End Sub

    Protected Sub rptPAttachment2_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me)
            Dim btnDelete As LinkButton = e.Item.FindControl("btnDelete")
            scriptManager.RegisterAsyncPostBackControl(btnDelete)
        End If
    End Sub

    Protected Sub rptPAttachment2_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("deletefile") Then
            Try
                Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
                Dim fileList As List(Of String) = installrequest.fldAttachment2.Split(",").ToList
                Dim filepath As String = fileList(e.CommandArgument)

                Dim fullServerPath As String = Server.MapPath(filepath)
                If File.Exists(fullServerPath) Then
                    File.Delete(fullServerPath)
                End If
                fileList.RemoveAt(e.CommandArgument)
                Dim fileListString As String = ""
                If fileList.Count > 0 Then fileListString = String.Join(",", fileList)
                If EMDInstallRequestManager.UpdateAttachment2(installrequest.fldID, fileListString) Then
                    UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "DELETE EMD INSTALLATION REQUEST ATTACHMENT 2", "Request ID: " & installrequest.fldID & ", Removed Attachment Name: " & Path.GetFileNameWithoutExtension(filepath), "")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgDeleteSuccess") & "');", True)
                    GetVendorFileData(EMDInstallRequestManager.GetInstallRequest(RequestID))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorDeleteFailed") & "');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorDeleteFailed") & "');", True)
            End Try
        End If
    End Sub

    Protected Sub btnPSubmitAttachment2_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If ValidateFileType(fuPAttachment2, {".png", ".jpeg", ".jpg", ".bmp"}, 10, True, True, "") Then
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            If Not installrequest Is Nothing Then
                Dim attachment As String = ""
                If fuPAttachment2.HasFiles Then
                    If UploadFile(fuPAttachment2, "../upload/attachment/", "Gambar_", "_" & installrequest.fldRefID.ToLower, True, attachment) Then
                        If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment2) Then attachment = installrequest.fldAttachment2 & "," & attachment
                        If EMDInstallRequestManager.UpdateAttachment2(RequestID, attachment) Then
                            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST ATTACHMENT 2", "Request ID: " & RequestID, "")
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                            GetVendorFileData(EMDInstallRequestManager.GetInstallRequest(RequestID))
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnPUpdateEMD_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If EMDInstallRequestManager.UpdateEMDAcessories(RequestID, ddlPOPP.SelectedValue, ddlPEMD.SelectedValue, If(chkPSmartTag.Checked, txtPSmartTagCode.Text, ""), If(chkPOBC.Checked, txtPOBCCode.Text, ""), If(chkPBeacon.Checked, txtPBeaconCode.Text, ""), If(chkPCharger.Checked, txtPChargerCode.Text, ""), If(chkPStrap.Checked, txtPStrapCode.Text, ""), If(chkPCable.Checked, txtPCableCode.Text, ""), If(chkPSmartTag.Checked, 1, 0), If(chkPOBC.Checked, 1, 0), If(chkPBeacon.Checked, 1, 0), If(chkPCharger.Checked, 1, 0), If(chkPStrap.Checked, 1, 0), If(chkPCable.Checked, 1, 0)) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST ACCESSORIES", "Request ID: " & RequestID, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetVendorFormInfo(EMDInstallRequestManager.GetInstallRequest(RequestID))
        End If
    End Sub

    Protected Sub btnPUpdateStatus_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If EMDInstallRequestManager.UpdateStatus(RequestID, AdminAuthentication.GetUserData(2), ddlPStatus.SelectedValue, txtPRemark2.Text) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST STATUS", "Request ID: " & RequestID & ", Update Status: " & ddlPStatus.SelectedValue, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetVendorFormInfo(EMDInstallRequestManager.GetInstallRequest(RequestID))
            BindTable()
        End If
    End Sub

#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        ddlSDepartment.SelectedIndex = 0
        ddlSPoliceStation.SelectedIndex = 0
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plOfficer.Visible = False
        plVendor.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
        GetOfficerFormInfo(installrequest)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        GetOfficerFormInfo(Nothing)
    End Sub

    Protected Sub btnPBack_Click(sender As Object, e As EventArgs)
        GetVendorFormInfo(Nothing)
    End Sub

#End Region

End Class