Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAlertNotificationList
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
        lblPageTitle.Text = GetText("AlertNotificationList")
        lblLowList.Text = GetText("Low")
        lblMediumList.Text = GetText("Medium")
        lblHighList.Text = GetText("High")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        BindLow()
        BindMedium()
        BindHigh()
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindLow()
        Dim myDataTable As DataTable = EMDDeviceManager.GetAlertNotification(-1, -1, -1, "", 0, "low", -1, 10)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptLow.DataSource = myDataTable
            rptLow.DataBind()
        Else
            rptLow.DataSource = ""
            rptLow.DataBind()
        End If
    End Sub

    Private Sub BindMedium()
        Dim myDataTable As DataTable = EMDDeviceManager.GetAlertNotification(-1, -1, -1, "", 0, "medium", -1, 10)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptMedium.DataSource = myDataTable
            rptMedium.DataBind()
        Else
            rptMedium.DataSource = ""
            rptMedium.DataBind()
        End If
    End Sub

    Private Sub BindHigh()
        Dim myDataTable As DataTable = EMDDeviceManager.GetAlertNotification(-1, -1, -1, "", 0, "high", -1, 10)
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptHigh.DataSource = myDataTable
            rptHigh.DataBind()
        Else
            rptHigh.DataSource = ""
            rptHigh.DataBind()
        End If
    End Sub

    Protected Sub rptHigh_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("acknowledge") Then
            If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                BindHigh()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Protected Sub rptMedium_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("acknowledge") Then
            If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                BindMedium()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If
    End Sub

    Protected Sub rptLow_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("acknowledge") Then
            If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
                BindLow()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
            End If
        End If

    End Sub

#End Region

End Class