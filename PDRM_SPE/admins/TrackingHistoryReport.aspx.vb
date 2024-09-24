Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject


Public Class ATrackingHistoryReport
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            If Not Request("opp") Is Nothing _
                AndAlso Not Request("emd") Is Nothing _
                AndAlso Not Request("fr") Is Nothing _
                AndAlso Not Request("to") Is Nothing _
                AndAlso Not Request("i") Is Nothing AndAlso Request("i").Equals(UtilityManager.MD5Encrypt(Request("opp") & Request("emd") & Request("fr") & Request("to") & "historyreport")) Then
                Dim oppid As Long = 0
                If Request("opp") > 0 Then
                    oppid = Request("opp")
                ElseIf Request("emd") > 0 Then
                    oppid = OPPManager.GetOPPID(Request("emd"), "", "")
                End If
                If oppid > 0 Then
                    Dim opp As OPPObj = OPPManager.GetOPP(oppid)
                    If Not opp Is Nothing Then
                        hfOPPID.Value = oppid
                        hfFrDate.Value = Request("fr")
                        hfToDate.Value = Request("to")
                        GetInfo(opp, hfFrDate.Value, hfToDate.Value)
                        BindAlertList(-1, hfOPPID.Value, hfFrDate.Value, hfToDate.Value)
                        BindHistoryList(-1, hfOPPID.Value, hfFrDate.Value, hfToDate.Value)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "javascript", "initMap().then(() => fetchAndUpdateMarkers());", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorNoResult") & "');window.top.close();", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorNoResult") & "');window.top.close();", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorNoResult") & "');window.top.close();", True)
            End If
        End If

    End Sub

    Private Sub SetText()
        lblHeader.Text = GetText("emdReport") & " - "
        lblDate.Text = GetText("Date")
        lblEMD.Text = GetText("EMD")
        lblName.Text = GetText("Name")
        lblIC.Text = GetText("ICNum")
        lblPegawaiPengawasan.Text = GetText("Overseer")
        lblPoliceNo.Text = GetText("PoliceIDNo")
        lblTelPegawai.Text = GetText("OverseerItem").Replace("vITEM", GetText("ContactNum"))
        lblbalai.Text = GetText("PoliceStation")
        lblJabatan.Text = GetText("Department")
        lblTelBalai.Text = GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))
        lblAlert.Text = GetText("Alert")
        lblhistory.Text = GetText("history")
    End Sub

    Private Sub GetInfo(opp As OPPObj, frdatetime As DateTime, todatetime As DateTime)
        Dim emd As EMDDeviceObj = EMDDeviceManager.GetDevice(opp.fldEMDDeviceID)
        Dim pegawaiDtl As AdminObj = AdminManager.GetAdmin(opp.fldOverseerID)
        txtHeader.Text = opp.fldName
        lblHeaderDateTime.Text = frdatetime.ToString("dd-MMMM-yyyy hh:mm:ss tt") & " - " & todatetime.ToString("dd-MMMM-yyyy hh:mm:ss tt")
        txtDate.Text = UtilityManager.GetServerDateTime.ToString("dd-MMMM-yyyy")
        txtEMD.Text = emd.fldImei
        txtName.Text = opp.fldName
        txtIC.Text = opp.fldICNo
        txtPegawaiPengawasan.Text = pegawaiDtl.fldName
        txtPoliceNo.Text = pegawaiDtl.fldPoliceNo
        txtTelPegawai.Text = pegawaiDtl.fldContactNo
        txtbalai.Text = PoliceStationManager.GetPoliceStationName(opp.fldPoliceStationID)
        txtJabatan.Text = PoliceStationManager.GetDepartmentName(opp.fldDeptID)
        txtTelBalai.Text = PoliceStationManager.GetPoliceStationContactNo(opp.fldPoliceStationID)
        'Dim baseUrl As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
        logolable.Src = "../assets/img/companylogo.png"
        If Not String.IsNullOrEmpty(opp.fldPhoto1) Then
            imgPhoto.ImageUrl = opp.fldPhoto1
        Else
            imgPhoto.ImageUrl = "../assets/img/No_Image.png"
        End If
    End Sub

    Private Sub BindAlertList(deviceid As Long, oppid As Long, frdatetime As String, todatetime As String)
        Dim myDataTable As DataTable = EMDDeviceManager.GetAlertNotificationList(deviceid, oppid, frdatetime, todatetime)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptAlert.DataSource = myDataTable
            rptAlert.DataBind()
            dvNoResult.Visible = False
        Else
            rptAlert.DataSource = ""
            rptAlert.DataBind()
            dvNoResult.Visible = True
        End If
    End Sub

    Private Sub BindHistoryList(deviceid As Long, oppid As Long, frdatetime As String, todatetime As String)
        Dim myDataTable As DataTable = EMDDeviceManager.GetDeviceHistory_PDF(deviceid, oppid, frdatetime, todatetime, 180)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptHistory.DataSource = myDataTable
            rptHistory.DataBind()
            dvNoResult2.Visible = False
        Else
            dvNoResult2.Visible = True
            rptHistory.DataSource = ""
            rptHistory.DataBind()
        End If
    End Sub

End Class