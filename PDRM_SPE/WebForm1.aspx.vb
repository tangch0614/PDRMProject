Imports AppCode.BusinessLogic

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim myDataTable As DataTable = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
            If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                rptTable.DataSource = myDataTable
                rptTable.DataBind()
            Else
                rptTable.DataSource = ""
                rptTable.DataBind()
            End If
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("Alarm") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand(e.CommandArgument, "alarmon")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("AlarmOff") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand(e.CommandArgument, "alarmoff")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Lock") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand(e.CommandArgument, "lock")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Unlock") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand(e.CommandArgument, "unlock")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Vibrate") Then
            Dim result As Boolean = EMDDeviceManager.InsertGPRSCommand(e.CommandArgument, "vibrate")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("LowBattery") Then
            Dim result As Boolean = EMDDeviceManager.InsertNotification(e.CommandArgument, "device low battery", "medium")
            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        End If
    End Sub
End Class