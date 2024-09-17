Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class ATrackingMap
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
            UserID = AdminAuthentication.GetUserData(2)
            SetText()
            GetEMDDevice()
            GetOPPList()
            hfOPPID.Value = -1
            If Not Request("opp") Is Nothing Then
                Dim opp As OPPObj = OPPManager.GetOPP(Request("opp"))
                If Not opp Is Nothing AndAlso Not Request("i") Is Nothing AndAlso Request("i").Equals(UtilityManager.MD5Encrypt(Request("opp") & "trackingmap")) Then
                    Try
                        ddlEMD.SelectedValue = opp.fldEMDDeviceID
                        ddlOPP.SelectedValue = opp.fldID
                        hfOPPID.Value = Request("opp")
                    Catch
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorItemNotFound").Replace("vITEM", "OPP") & "')", True)
                        ddlEMD.SelectedIndex = 0
                        ddlOPP.SelectedIndex = 0
                    End Try
                End If
            End If
            GetOPPInfo(hfOPPID.Value)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();initNotifications();", True)
        End If
    End Sub

    Private Sub SetText()
        btnSearch.Text = GetText("Search")
        btnPAcknowledge.Text = GetText("Acknowledge")
        btnPCancel.Text = GetText("Close")
        hfNoResult.Value = GetText("ErrorNoResult")
        hfConfirm.Value = GetText("MsgConfirm")
    End Sub

    Private Sub GetEMDDevice()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlEMD.DataTextField = "fldImei"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        ddlEMD.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("EMD")), -1))
        ddlEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOPPList()
        Dim datatable As DataTable = OPPManager.GetOPPList(-1, "Y")
        datatable.Columns.Add("fldNameIC", GetType(String), "fldName + '-' + fldICNo")
        ddlOPP.DataSource = datatable
        ddlOPP.DataTextField = "fldNameIC"
        ddlOPP.DataValueField = "fldID"
        ddlOPP.DataBind()
        ddlOPP.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("OPP")), -1))
    End Sub

    Private Sub GetOPPInfo(ByVal id As Long)
        imgPhoto.ImageUrl = "../assets/img/No_Image.png"
        txtOPPName.Text = ""
        txtOPPICNo.Text = ""
        txtOverseerName.Text = ""
        txtOverseerContactNo.Text = ""
        txtPoliceNo.Text = ""
        txtPoliceStation.Text = ""
        txtDepartment.Text = ""
        txtPSContactNo.Text = ""
        If id > 0 Then
            Dim myDataTable As DataTable = OPPManager.GetOPPList(id, "", "", -1, -1, -1, "", "")
            If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                imgPhoto.ImageUrl = If(Not String.IsNullOrEmpty(myDataTable.Rows(0)("fldPhoto1")), myDataTable.Rows(0)("fldPhoto1"), "../assets/img/No_Image.png")
                txtOPPName.Text = myDataTable.Rows(0)("fldName")
                txtOPPICNo.Text = myDataTable.Rows(0)("fldICNo")
                txtOverseerName.Text = myDataTable.Rows(0)("fldOverseerName")
                txtOverseerContactNo.Text = myDataTable.Rows(0)("fldOverseerContactNo")
                txtPoliceNo.Text = myDataTable.Rows(0)("fldPoliceNo")
                txtPoliceStation.Text = myDataTable.Rows(0)("fldPSName")
                txtDepartment.Text = myDataTable.Rows(0)("fldDepartment")
                txtPSContactNo.Text = myDataTable.Rows(0)("fldPSContactNo")
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        hfOPPID.Value = 0
        Dim oppid As Long = 0
        If ddlOPP.SelectedIndex > 0 Then
            oppid = ddlOPP.SelectedValue
        ElseIf ddlEMD.SelectedIndex > 0 Then
            oppid = OPPManager.GetOPPID(ddlEMD.SelectedValue, "", "")
        End If
        If oppid > 0 Then
            hfOPPID.Value = oppid
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "cleartoastr();fetchAndUpdateMarkers(true);", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorNoResult") & "');", True)
        End If
        GetOPPInfo(oppid)
    End Sub

    Protected Sub ddlOPP_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlOPP.SelectedIndex > 0 Then
            Dim deviceid As Long = OPPManager.GetEMDDeviceID(ddlOPP.SelectedValue)
            Try
                ddlEMD.SelectedValue = deviceid
            Catch ex As Exception
                ddlEMD.SelectedIndex = 0
            End Try
        End If
    End Sub

    Protected Sub ddlEMD_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlEMD.SelectedValue > 0 Then
            Dim oppid As Long = OPPManager.GetOPPID(ddlEMD.SelectedValue, "", "")
            Try
                ddlOPP.SelectedValue = oppid
            Catch ex As Exception
                ddlOPP.SelectedIndex = 0
            End Try
        End If
    End Sub

    Protected Sub btnPAcknowledge_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If EMDDeviceManager.AcknowledgeAlertNotification(hfAlertID.Value, AdminAuthentication.GetUserData(2), txtPRemark.Text) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ACKNOWLEDGE VIOLATION ALERT", "Alert ID: " & hfAlertID.Value, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');getModalData(" & hfAlertID.Value & ");CloseToastr(" & hfAlertID.Value & ");", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');getModalData(" & hfAlertID.Value & ");", True)
        End If
    End Sub

    Protected Sub btnPCancel_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "$('#plAcknowledge').modal('hide');", True)
    End Sub

End Class