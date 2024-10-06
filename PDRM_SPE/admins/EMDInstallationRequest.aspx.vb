Imports System.Drawing.Imaging
Imports System.IO
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AEMDInstallationRequest
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
        lblPageTitle.Text = GetText("InstallationRequest")
        lblHeader.Text = GetText("InstallationRequest")
        'department
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
        'other info
        lblOtherInfo.Text = GetText("OthersInfo")
        lblRemark.Text = GetText("Remark")
        'file upload
        lblFileInfo.Text = GetText("Attachment")
        lblAttachment1.Text = GetText("Attachment")
        btnAttachment1.Text = GetText("UploadDocument")
        'Buttons/Message
        btnSubmit.Text = GetText("Submit")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Submit").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetDepartment()
        GetState()
        GetIPK("")
        GetTime()
        Dim dateTime As DateTime = UtilityManager.GetServerDateTime()
        hfDate.Text = dateTime.ToString("yyyy-MM-dd")
        ddlIPK_SelectedIndexChanged(Me.ddlIPK, EventArgs.Empty)
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

    Private Sub GetDepartment()
        ddlDepartment.DataSource = PoliceStationManager.GetDepartmentList("Y")
        ddlDepartment.DataTextField = "fldName"
        ddlDepartment.DataValueField = "fldID"
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Department")), -1))
        ddlDepartment.SelectedIndex = 0
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
            ddlState.Selectedindex = 0
        End Try
        GetMukim(state, "")
    End Sub

#End Region

#Region "Validation"

    Private Function PageValid() As Boolean
        Dim result As Boolean = ValidatePage("apply")
        Dim errorMsg As String = ""
        Dim returnMsg As String = ""
        If Not ValidateFileType(fuAttachment1, {".png", ".jpeg", ".jpg", ".bmp", ".pdf"}, 10, True, False, returnMsg) Then
            result = False
            errorMsg = returnMsg & "\n"
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
            Dim datetime As DateTime = UtilityManager.GetServerDateTime()
            Dim installrequest As EMDInstallRequestObj = New EMDInstallRequestObj
            If UploadFile(fuAttachment1, "../upload/attachment/", "Lampiran_", "_" & datetime.ToString("yMd_Hms"), False, installrequest.fldAttachment1) Then
                installrequest.fldDateTime = datetime
                installrequest.fldDeptID = ddlDepartment.SelectedValue
                installrequest.fldInstallDateTime = CDate(hfDate.Text & " " & ddlTime.SelectedValue)
                installrequest.fldState = ddlState.SelectedValue
                'installrequest.fldDistrict = ddlDistrict.SelectedValue
                installrequest.fldMukim = ddlMukim.SelectedValue
                installrequest.fldIPKID = ddlIPK.SelectedValue
                installrequest.fldIPDID = 0
                installrequest.fldPSID = ddlPoliceStation.SelectedValue
                installrequest.fldOCSName = txtOCSName.Text
                installrequest.fldOCSTelNo = txtOCSContactNo.Text
                installrequest.fldRemark = txtRemark.Text
                installrequest.fldCreatorID = AdminAuthentication.GetUserData(2)
                installrequest.fldStatus = "P"
                Dim result As Long = EMDInstallRequestManager.Save(installrequest)
                If result > 0 Then
                    UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "SUBMIT EMD INSTALLATION REQUEST", "ID: " & result, "")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgSubmitSuccess") & "');window.location.href='../admins/EMDInstallationRequestList.aspx';", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorSubmitFailed") & "');", True)
                End If
            End If
        End If
    End Sub

#End Region
End Class