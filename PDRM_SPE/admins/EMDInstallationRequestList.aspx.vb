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
        'department
        lblInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("InstallationRequest"))
        lblDepartmentInfo.Text = GetText("Department")
        lblDepartment.Text = GetText("Department")
        rfvDepartment.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Department"))
        'install info
        lblInstallInfo.Text = GetText("InstallationInfo")
        lblDate.Text = GetText("Date")
        lblTime.Text = GetText("Time")
        rfvTime.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Time"))
        'install location
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
        'ocs info
        lblOCSInfo.Text = GetText("OCSInfo")
        lblOCSName.Text = GetText("OCSItem").Replace("vITEM", GetText("Name"))
        rfvOCSName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCSItem").Replace("vITEM", GetText("Name")))
        lblOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        rfvOCSContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("OCSItem").Replace("vITEM", GetText("ContactNum")))
        'file upload
        lblFileInfo.Text = GetText("UploadDocument")
        lblAttachment1.Text = GetText("OversightOrder")
        btnAttachment1.Text = GetText("SelectItem").Replace("vITEM", GetText("Document"))
        lblAttachment2.Text = GetText("Attachment")
        btnAttachment2.Text = GetText("SelectItem").Replace("vITEM", GetText("Document"))
        'popup department
        lblPInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("InstallationRequest"))
        lblPDepartmentInfo.Text = GetText("Department")
        lblPDepartment.Text = GetText("Department")
        'popup install info
        lblPInstallInfo.Text = GetText("InstallationInfo")
        lblPDate.Text = GetText("Date")
        lblPTime.Text = GetText("Time")
        'popup install location
        lblPInstallLocation.Text = GetText("InstallationLocation")
        lblPMukim.Text = GetText("State") & "/" & GetText("Township")
        lblPIPK.Text = GetText("Contingent")
        lblPPoliceStation.Text = GetText("PoliceStation")
        'popup ocs info
        lblPOCSInfo.Text = GetText("OCSInfo")
        lblPOCSName.Text = GetText("OCSItem").Replace("vITEM", GetText("Name"))
        lblPOCSContactNo.Text = GetText("OCSItem").Replace("vITEM", GetText("ContactNum"))
        'popup file upload
        lblPFileInfo.Text = GetText("UploadDocument")
        lblPAttachment1.Text = GetText("OversightOrder")
        lblPAttachment2.Text = GetText("Attachment")
        lblPStatus.Text = GetText("Status")
        'Buttons/Message
        btnPAcknowledge.Text = GetText("Acknowledge")
        btnPCancel.Text = GetText("Close")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        btnBack.Text = GetText("Back")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
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
        ddlSStatus.Items.Add(New ListItem(GetText("Completed"), "Y", True))
        'ddlSStatus.Items.Add(New ListItem(GetText("Rejected"), "N", True))
        If (AdminAuthentication.GetUserData(5) = 1) Then
            ddlSStatus.SelectedValue = "P"
        Else
            ddlSStatus.SelectedIndex = 0
        End If
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

#End Region

#Region "Table binding"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        BindTable()
        plUpdate.Visible = False
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
        If e.CommandName.Equals("updaterequest") AndAlso AdminAuthentication.GetUserData(5) = 3 Then
            RequestID = e.CommandArgument
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            GetInfo(installrequest)
        ElseIf e.CommandName.Equals("acknowledge") AndAlso AdminAuthentication.GetUserData(5) = 1 Then
            RequestID = e.CommandArgument
            Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
            GetModalInfo(installrequest)
            If Not installrequest Is Nothing Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#plAcknowledge').modal('show');", True)
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

    Protected Function GetUserType() As Long
        Return AdminAuthentication.GetUserData(5)
    End Function

    Private Sub GetInfo(ByVal installrequest As EMDInstallRequestObj)
        If Not installrequest Is Nothing Then
            plTable.Visible = False
            plUpdate.Visible = True
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
            If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment1) Then
                lbtAttachment1.Text = GetText("ClickToView")
                lbtAttachment1.OnClientClick = "OpenPopupWindow('" & installrequest.fldAttachment1 & "',1280,800);return false;"
            Else
                lbtAttachment1.Text = GetText("None")
                lbtAttachment1.OnClientClick = "return false;"
            End If
            If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment2) Then
                lbtAttachment2.Text = GetText("ClickToView")
                lbtAttachment2.OnClientClick = "OpenPopupWindow('" & installrequest.fldAttachment2 & "',1280,800);return false;"
            Else
                lbtAttachment2.Text = GetText("None")
                lbtAttachment2.OnClientClick = "return false;"
            End If
            txtRemark.Text = installrequest.fldRemark
        Else
            plTable.Visible = True
            plUpdate.Visible = False
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
            lbtAttachment1.Text = GetText("None")
            lbtAttachment1.OnClientClick = "return false;"
            lbtAttachment2.Text = GetText("None")
            lbtAttachment2.OnClientClick = "return false;"
            txtRemark.Text = ""
        End If
    End Sub

    Private Sub GetModalInfo(ByVal installrequest As EMDInstallRequestObj)
        If Not installrequest Is Nothing Then
            txtPDepartment.Text = PoliceStationManager.GetDepartmentName(installrequest.fldDeptID)
            txtPDate.Text = installrequest.fldInstallDateTime.ToString("yyyy-MM-dd")
            txtPTime.Text = installrequest.fldInstallDateTime.ToString("HH:mm")
            txtPIPK.Text = PoliceStationManager.GetPoliceStationName(installrequest.fldIPKID)
            txtPPoliceStation.Text = PoliceStationManager.GetPoliceStationName(installrequest.fldPSID)
            txtPMukim.Text = installrequest.fldState & " / " & installrequest.fldMukim
            txtPOCSName.Text = installrequest.fldOCSName
            txtPOCSContactNo.Text = installrequest.fldOCSTelNo
            If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment1) Then
                lbtPAttachment1.Text = GetText("ClickToView")
                lbtPAttachment1.OnClientClick = "OpenPopupWindow('" & installrequest.fldAttachment1 & "',1280,800);return false;"
            Else
                lbtPAttachment1.Text = GetText("None")
                lbtPAttachment1.OnClientClick = "return false;"
            End If
            If Not String.IsNullOrWhiteSpace(installrequest.fldAttachment2) Then
                lbtPAttachment2.Text = GetText("ClickToView")
                lbtPAttachment2.OnClientClick = "OpenPopupWindow('" & installrequest.fldAttachment2 & "',1280,800);return false;"
            Else
                lbtPAttachment2.Text = GetText("None")
                lbtPAttachment2.OnClientClick = "return false;"
            End If
            txtPRemark.Text = installrequest.fldRemark
            txtPStatus.Text = If(installrequest.fldStatus.Equals("Y"), GetText("Completed"), If(installrequest.fldStatus.Equals("N"), GetText("Rejected"), GetText("Pending")))
            txtPStatus.CssClass = If(installrequest.fldStatus.Equals("Y"), "label label-success", If(installrequest.fldStatus.Equals("N"), "label label-danger", "label label-warning"))
            Dim officer As AdminObj = AdminManager.GetAdmin(installrequest.fldCreatorID)
            txtPOfficerName.Text = officer.fldName
            txtPOfficerContactNo.Text = officer.fldContactNo
            btnPAcknowledge.Visible = installrequest.fldStatus.Equals("P")
        Else
            txtPDepartment.Text = ""
            txtPDate.Text = ""
            txtPTime.Text = ""
            txtPIPK.Text = ""
            txtPPoliceStation.Text = ""
            txtPMukim.Text = ""
            txtPOCSName.Text = ""
            txtPOCSContactNo.Text = ""
            lbtPAttachment2.Text = GetText("NotAvailable")
            lbtPAttachment2.OnClientClick = "return false;"
            txtPRemark.Text = ""
            txtPStatus.Text = ""
            txtPOfficerName.Text = ""
            txtPOfficerContactNo.Text = ""
            btnPAcknowledge.Visible = False
        End If
    End Sub

#End Region

#Region "Save To DB"

    Protected Sub btnPAcknowledge_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If EMDInstallRequestManager.UpdateStatus(RequestID, AdminAuthentication.GetUserData(2), "P", "Y", "") Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ACKNOWLEDGE EMD INSTALLATION REQUEST", "ID: " & RequestID, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetModalInfo(EMDInstallRequestManager.GetInstallRequest(RequestID))
            BindTable()
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
        If Not installrequest Is Nothing Then
            If UpdateDetails(installrequest) Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD INSTALLATION REQUEST", "ID: " & RequestID, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                GetInfo(EMDInstallRequestManager.GetInstallRequest(RequestID))
                BindTable()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Private Function UpdateDetails(ByVal installrequest As EMDInstallRequestObj) As Boolean
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
        If Not fuAttachment1.PostedFile Is Nothing AndAlso fuAttachment1.PostedFile.ContentLength > 0 Then
            result = UploadDocument(fuAttachment1, "Perintah", installrequest.fldAttachment1)
        End If
        If result AndAlso Not fuAttachment2.PostedFile Is Nothing AndAlso fuAttachment2.PostedFile.ContentLength > 0 Then
            result = UploadDocument(fuAttachment2, "Lampiran", installrequest.fldAttachment2)
        End If
        If result Then result = EMDInstallRequestManager.Save(installrequest) > 0
        Return result
    End Function

    Private Function UploadDocument(ByVal fuFile As FileUpload, ByVal prefix As String, ByRef FilePath As String) As Boolean
        Dim result As Boolean = True
        Dim oldFilePath As String = ""
        Dim datetime As DateTime = UtilityManager.GetServerDateTime
        'Dim newSize As New System.Drawing.Size(500, 500)
        Try
            If Not fuFile.PostedFile Is Nothing AndAlso fuFile.PostedFile.ContentLength > 0 Then
                FilePath = "../" & ValidateFilePath("upload/attachment/", prefix & "_" & datetime.ToString("yyMMddHHmmss") & "_" & AdminAuthentication.GetUserData(2) & UtilityManager.GenerateRandomNumber(3), Path.GetExtension(fuFile.PostedFile.FileName).ToLower())
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

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        ddlSDepartment.SelectedIndex = 0
        ddlSPoliceStation.SelectedIndex = 0
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim installrequest As EMDInstallRequestObj = EMDInstallRequestManager.GetInstallRequest(RequestID)
        GetInfo(installrequest)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        GetInfo(Nothing)
    End Sub

    Protected Sub btnPCancel_Click(sender As Object, e As EventArgs)
        GetModalInfo(Nothing)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#plAcknowledge').modal('hide');", True)
    End Sub

#End Region

End Class