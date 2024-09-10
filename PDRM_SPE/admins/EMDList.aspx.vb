Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AEMDList
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
        lblPageTitle.Text = GetText("EMDList")
        lblHeader.Text = GetText("EMDList")
        lblInfo.Text = GetText("Information").Replace("vINFOTYPE", GetText("EMD"))
        'Search
        lblSImei.Text = GetText("Imei")
        lblSStatus.Text = GetText("Status")
        btnSearch.Text = GetText("Search")
        btnSReset.Text = GetText("Reset")
        'details
        lblImei.Text = GetText("Imei")
        'lblName.Text = GetText("Name")
        'rfvName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Name"))
        lblSimNo.Text = GetText("SIMNo")
        'rfvSimNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SIMNo"))
        lblSimNo2.Text = GetText("SIMNo") & " 2"
        'rfvSimNo2.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SIMNo") & " 2")
        'lblStatus.Text = GetText("Status")
        'Buttons/Message
        btnAdd.Text = GetText("Add")
        btnBack.Text = GetText("Back")
        btnReset.Text = GetText("Reset")
        btnSubmit.Text = GetText("Update")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetStatus()
        BindTable()
    End Sub

    Private Sub GetStatus()
        'ddlStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        'ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        'ddlStatus.SelectedIndex = 0

        ddlSStatus.Items.Add(New ListItem(GetText("All"), "", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        ddlSStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        ddlSStatus.SelectedIndex = 0
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = EMDDeviceManager.GetDeviceList(-1, -1, txtSImei.Text, "", ddlSStatus.SelectedValue)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        DeviceID = 0
        If e.CommandName.Equals("update") Then
            DeviceID = e.CommandArgument
            plTable.Visible = False
            plUpdate.Visible = True
            Dim device As EMDDeviceObj = EMDDeviceManager.GetDevice(DeviceID)
            If Not device Is Nothing Then
                GetDeviceData(device)
            End If
        ElseIf e.CommandName.Equals("lock") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand_Lock(e.CommandArgument)
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgCommandSendSuccess") & "');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorCommandSendFailed") & "');", True)
            End If
        ElseIf e.CommandName.Equals("unlock") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand_Unlock(e.CommandArgument, "221088")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgCommandSendSuccess") & "');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorCommandSendFailed") & "');", True)
            End If
        Else
            plUpdate.Visible = False
            plTable.Visible = True
        End If
    End Sub

    Private Sub GetDeviceData(ByVal device As EMDDeviceObj)
        txtImei.Text = device.fldImei
        txtSimNo.Text = device.fldSimNo
        txtSimNo2.Text = device.fldSimNo2
        'txtName.Text = device.fldName
        'ddlStatus.SelectedValue = device.fldStatus
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

    Private Function VerifyIMEI(ByVal device As EMDDeviceObj, ByVal imei As String, ByVal errormsg As Boolean, ByRef msg As String)
        If Not String.IsNullOrWhiteSpace(imei) AndAlso Not device.fldImei.Equals(imei) AndAlso EMDDeviceManager.VerifyIMEI(imei) > 0 Then
            msg = GetText("ErrorDuplicateItem").Replace("vITEM", GetText("IMEI"))
            If errormsg Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & msg & "');", True)
            Return False
        End If
        Return True
    End Function

    Private Function VerifySIMNo(ByVal device As EMDDeviceObj, ByVal simno As String, ByVal errormsg As Boolean, ByRef msg As String)
        If Not String.IsNullOrWhiteSpace(simno) AndAlso Not device.fldSimNo.Equals(simno) AndAlso EMDDeviceManager.VerifySimNo(simno) > 0 Then
            msg = GetText("ErrorDuplicateItem").Replace("vITEM", GetText("SIMNo"))
            If errormsg Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & msg & "');", True)
            Return False
        End If
        Return True
    End Function

    Private Function PageValid(ByVal device As EMDDeviceObj) As Boolean
        Dim result As Boolean = ValidatePage("update")
        Dim errorMsg As String = ""
        Dim msg As String = ""
        'If Not VerifyIMEI(device, txtImei.Text, False, msg) Then
        '    result = False
        '    errorMsg &= msg & "\n"
        'End If
        'If Not VerifySIMNo(device, txtSimNo.Text, False, msg) Then
        '    result = False
        '    errorMsg &= msg & "\n"
        'End If
        If Not result Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorPageInvalid") & "\n" & errorMsg & "');", True)
        End If
        Return result
    End Function

#End Region

#Region "Save To DB"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim device As EMDDeviceObj = EMDDeviceManager.GetDevice(DeviceID)
        If Not device Is Nothing AndAlso PageValid(device) Then
            device.fldSimNo = txtSimNo.Text
            device.fldSimNo2 = txtSimNo2.Text
            'device.fldName = txtName.Text
            'device.fldStatus = ddlStatus.SelectedValue
            If EMDDeviceManager.Save(device) > 0 Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "UPDATE EMD DEVICE", "Device ID: " & device.fldID, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                BindTable()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

#End Region

#Region "Reset"

    Protected Sub btnSReset_Click(sender As Object, e As EventArgs)
        txtSImei.Text = ""
        ddlSStatus.SelectedIndex = 0
        rptTable.DataSource = ""
        rptTable.DataBind()
        plUpdate.Visible = False
        plTable.Visible = True
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim device As EMDDeviceObj = EMDDeviceManager.GetDevice(DeviceID)
        GetDeviceData(device)
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/admins/AddEMD.aspx")
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        plTable.Visible = True
        plUpdate.Visible = False
        DeviceID = 0
    End Sub

#End Region

End Class