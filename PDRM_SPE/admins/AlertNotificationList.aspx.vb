Imports System.Web.Services
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class AAlertNotificationList
    Inherits ABase

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("AlertNotificationList")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        SetText()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initAlert();", True)
    End Sub

#End Region

#Region "Table binding"

    <WebMethod()>
    Public Shared Function GetAlertList(severity As String, limit As Integer) As String
        Dim myDataTable As DataTable = EMDDeviceManager.GetAlertNotification(-1, -1, -1, "", 0, severity, -1, limit)
        myDataTable.Columns.Add("fldMD5", GetType(String))
        For i As Integer = 0 To myDataTable.Rows.Count - 1
            myDataTable.Rows(i)("fldMD5") = UtilityManager.MD5Encrypt(myDataTable.Rows(i)("fldID") & "processalert")
        Next
        Dim json As String = JsonConvert.SerializeObject(myDataTable)
        Dim jArray As JArray = JArray.Parse(json)
        Dim newArray As JArray = New JArray()
        For Each jObject As JObject In jArray
            Dim newJObject As New JObject()
            For Each prop As JProperty In jObject.Properties()
                newJObject.Add(prop.Name.ToLower(), prop.Value)
            Next
            newArray.Add(newJObject)
        Next
        Return newArray.ToString
    End Function

    <WebMethod()>
    Public Shared Function GetAlertCount(severity As String) As Integer
        Return EMDDeviceManager.CountNotification(-1, -1, -1, 0, severity, -1)
    End Function

    <WebMethod()>
    Public Shared Function MD5(id As Long) As String
        Return UtilityManager.MD5Encrypt(id & "processalert")
    End Function

    'Protected Sub rptHigh_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    If e.CommandName.Equals("acknowledge") Then
    '        If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
    '            BindHigh()
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
    '        End If
    '    End If
    'End Sub

    'Protected Sub rptMedium_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    If e.CommandName.Equals("acknowledge") Then
    '        If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
    '            BindMedium()
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
    '        End If
    '    End If
    'End Sub

    'Protected Sub rptLow_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
    '    If e.CommandName.Equals("acknowledge") Then
    '        If EMDDeviceManager.AcknowledgeAlertNotification(e.CommandArgument, AdminAuthentication.GetUserData(2), "") Then
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("MsgUpdateSuccess") & "');", True)
    '            BindLow()
    '        Else
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorUpdateFailed") & "');", True)
    '        End If
    '    End If

    'End Sub

#End Region

End Class