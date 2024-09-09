Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAddEMD
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
        lblPageTitle.Text = GetText("AddEMD")
        lblHeader.Text = GetText("AddEMD")
        'details
        lblImei.Text = GetText("Imei")
        rfvImei.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Imei"))
        lblSimNo.Text = GetText("SIMNo")
        rfvSimNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SIMNo"))
        lblSimNo2.Text = GetText("SIMNo") & " 2"
        'rfvSimNo2.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("SIMNo") & " 2")
        lblStatus.Text = GetText("Status")
        'Buttons/Message
        btnSubmit.Text = GetText("Submit")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("AddEMD").ToLower)
        ValidationSummary1.HeaderText = GetText("ErrorPageInvalid")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetStatus()
    End Sub

    Private Sub GetStatus()
        ddlStatus.Items.Add(New ListItem(GetText("Active"), "Y", True))
        ddlStatus.Items.Add(New ListItem(GetText("Inactive"), "N", True))
        ddlStatus.SelectedIndex = 0
    End Sub

#End Region

#Region "validation"

    Private Function VerifyIMEI(ByVal imei As String, ByVal errormsg As Boolean, ByRef msg As String)
        If Not String.IsNullOrWhiteSpace(imei) AndAlso EMDDeviceManager.VerifyIMEI(imei) > 0 Then
            msg = GetText("ErrorDuplicateItem").Replace("vITEM", GetText("IMEI"))
            If errormsg Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & msg & "');", True)
            Return False
        End If
        Return True
    End Function

    'Private Function VerifySIMNo(ByVal simno As String, ByVal errormsg As Boolean, ByRef msg As String)
    '    If Not String.IsNullOrWhiteSpace(simno) AndAlso EMDDeviceManager.VerifySimNo(simno) > 0 Then
    '        msg = GetText("ErrorDuplicateItem").Replace("vITEM", GetText("SIMNo"))
    '        If errormsg Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & msg & "');", True)
    '        Return False
    '    End If
    '    Return True
    'End Function

    Private Function PageValid() As Boolean
        Dim result As Boolean = ValidatePage("add")
        Dim errorMsg As String = ""
        Dim msg As String = ""
        If Not VerifyIMEI(txtImei.Text, False, msg) Then
            result = False
            errorMsg &= msg & "\n"
        End If
        'If Not VerifySIMNo(txtSimNo.Text, False, msg) Then
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
        If PageValid() Then
            Dim device As EMDDeviceObj = New EMDDeviceObj
            device.fldImei = txtImei.Text
            device.fldSimNo = txtSimNo.Text
            device.fldSimNo2 = txtSimNo2.Text
            device.fldStatus = ddlStatus.SelectedValue
            Dim deviceid As Long = EMDDeviceManager.Save(device)
            If deviceid > 0 Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ADD EMD DEVICE", "Device ID: " & deviceid, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgAddItemSuccess").Replace("vITEM", GetText("EMD")) & "');window.location.href='../admins/AddEMD.aspx';", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorAddItemFailed").Replace("vITEM", GetText("EMD")) & "');", True)
            End If
        End If
    End Sub

#End Region

End Class