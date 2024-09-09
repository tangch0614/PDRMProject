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

    Protected Sub rpt1List_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName.Equals("Alarm") Then
            Dim result As Boolean = TempGetDataManager.InsertGPRSMode(e.CommandArgument, "Alarm")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("AlarmOff") Then
            Dim result As Boolean = TempGetDataManager.InsertGPRSMode(e.CommandArgument, "AlarmOff")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Lock") Then
            Dim result As Boolean = TempGetDataManager.InsertGPRSMode(e.CommandArgument, "Lock")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Unlock") Then
            Dim result As Boolean = TempGetDataManager.InsertGPRSMode(e.CommandArgument, "Unlock")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("Vibrate") Then
            Dim result As Boolean = TempGetDataManager.InsertGPRSMode(e.CommandArgument, "Vibrate")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        ElseIf e.CommandName.Equals("LowBattery") Then
            Dim result As Boolean = TempGetDataManager.InsertNotification(e.CommandArgument, "device low battery", "medium")

            If result Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('Failed');", True)
            End If
        End If
    End Sub
End Class