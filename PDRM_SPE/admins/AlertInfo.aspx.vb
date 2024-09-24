Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAlertInfo
    Inherits Base

    Protected Property alertID() As Long
        Get
            If Not ViewState("alertID") Is Nothing Then
                Return CLng(ViewState("alertID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("alertID") = value
        End Set
    End Property

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            If Not Request("id") Is Nothing AndAlso Not Request("i") Is Nothing AndAlso Request("i").Equals(UtilityManager.MD5Encrypt(Request("id") & "processalert")) Then
                alertID = Request("id")
                GetInfo(alertID, True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "alert('" & GetText("ErrorNoResult") & "');window.top.close();", True)
            End If
        End If
    End Sub

    Private Sub SetText()
        btnAcknowledge.Text = GetText("Acknowledge")
        btnCancel.Text = GetText("Close")
        hfConfirm.Value = GetText("MsgConfirm")
    End Sub

    Private Sub GetInfo(ByVal alertid As Long, init As Boolean)
        Dim alert As DataTable = EMDDeviceManager.GetAlertNotification(alertid, -1, -1, "", -1, "", -1, -1)
        If alert.Rows.Count > 0 Then
            Dim row As DataRow = alert.Rows(0)
            If row("fldseverity").Equals("high") Then
                plViolateTerms.CssClass = "portlet box red"
            ElseIf row("fldseverity").Equals("medium") Then
                plViolateTerms.CssClass = "portlet box yellow-gold"
            Else
                plViolateTerms.CssClass = "portlet box yellow-crusta"
            End If
            lblViolateTermsInfo.Text = GetText("ViolateTerms") & " - " & row("fldmsg")
            imgPhoto1Preview.ImageUrl = If(String.IsNullOrWhiteSpace(row("fldPhoto1")), "../assets/img/No_Image.png", row("fldPhoto1"))
            txtImei.Text = row("fldimei")
            txtDateTime.Text = CDate(row("flddatetime")).ToString("yyyy-MM-dd hh:mm:ss tt")
            txtViolateTerms.Text = row("fldmsg").ToUpper()
            txtSubjectName.Text = row("fldoppname")
            txtSubjectICNo.Text = row("fldoppicno")
            txtSubjectContactNo.Text = row("fldoppcontactno")
            txtPoliceStation.Text = row("fldpsname")
            txtPSContactNo.Text = row("fldpscontactno")
            txtDepartment.Text = row("flddepartment")
            txtOverseer.Text = row("fldoverseername")
            txtOverseerIDNo.Text = row("fldoverseerpoliceno")
            txtOverseerContactNo.Text = row("fldoverseercontactno")
            If CInt(row("fldprocess")) = 1 Then
                btnAcknowledge.Visible = False
                txtRemark.Enabled = False
                txtRemark.Text = row("fldremark")
                txtAcknowledgeByID.Text = row("fldprocessbyname")
                txtAcknowledgeDateTime.Text = CDate(row("fldprocessdatetime")).ToString("yyyy-MM-dd hh:mm:ss tt")
            Else
                btnAcknowledge.Visible = True
                txtRemark.Enabled = True
                txtRemark.Text = ""
                txtAcknowledgeByID.Text = ""
                txtAcknowledgeDateTime.Text = ""
            End If
            If init Then
                hfOPPID.Value = row("fldoppid")
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "alert('" & GetText("ErrorNoResult") & "');window.top.close();", True)
        End If
    End Sub

    Protected Sub btnAcknowledge_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If EMDDeviceManager.AcknowledgeAlertNotification(alertID, AdminAuthentication.GetUserData(2), txtRemark.Text) Then
            UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ACKNOWLEDGE VIOLATION ALERT", "Alert ID: " & alertID, "")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
            GetInfo(alertID, False)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "window.top.close();", True)
    End Sub

End Class