Imports AppCode.BusinessLogic

Public Class TestMap
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim location As String = "[{name:'867255079755544', lat:3.11161, long:101.5785, imei:'867255079755544', datetime:'2024-07-08 11:54:10', locstatus:'GPS', datastatus:'Real-Time', gps:'11', bds:'3', battery:'7%(Charging)', beltstatus:'Belt Off', alarm:'Offline', speed:'0(km/h)', pincolor:'blue', pinglyphcolor:'white'},
                                    {name:'867255079747483', lat:2.91179, long:101.73555, imei:'867255079747483', datetime:'2024-07-08 11:54:10', locstatus:'GPS', datastatus:'Real-Time', gps:'11', bds:'12', battery:'0%', beltstatus:'Belt Off', alarm:'Offline', speed:'0(km/h)', pincolor:'red', pinglyphcolor:'brown'},
                                    {name:'867255079776666', lat:3.134922, long:101.713302, imei:'867255079776666', datetime:'2024-08-07 11:54:10', locstatus:'GPS', datastatus:'Real-Time', gps:'11', bds:'3', battery:'0%', beltstatus:'Belt Off', alarm:'Offline', speed:'0(km/h)', pincolor:'green', pinglyphcolor:'yellow'}]"
        If Not IsPostBack Then
            GetEMD()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();", True)
        End If
    End Sub

    Private Sub GetEMD()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlEMD.DataTextField = "fldName"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        ddlEMD.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlEMD.SelectedIndex = 0
    End Sub

    Private Function GetDeviceInfo() As String
        Dim sb As StringBuilder = New StringBuilder
        Dim datatable As DataTable = EMDDeviceManager.GetDeviceList(ddlEMD.SelectedValue, "", "", "Y")
        sb.Append("[")
        For i As Integer = 0 To datatable.Rows.Count - 1
            Dim statusarr As Array = CStr(datatable.Rows(i)("fldDeviceStatus")).ToCharArray
            sb.Append("{")
            sb.AppendFormat("id:'{0}',", i + 1)
            sb.AppendFormat("datetime:'{0}',", CDate(datatable.Rows(i)("fldLastPing")).ToString("yyyy-MM-dd HH:mm:ss"))
            sb.AppendFormat("name:'{0}',", datatable.Rows(i)("fldName"))
            sb.AppendFormat("imei:'{0}',", datatable.Rows(i)("fldImei"))
            sb.AppendFormat("lat:{0},", ConvertPos(datatable.Rows(i)("fldLat"), datatable.Rows(i)("fldNS"), "lat"))
            sb.AppendFormat("long:{0},", ConvertPos(datatable.Rows(i)("fldLong"), datatable.Rows(i)("fldWE"), "long"))
            sb.AppendFormat("locstatus:'{0}',", GetText(datatable.Rows(i)("fldLocStatus")))
            sb.AppendFormat("datastatus:'{0}',", GetText("Real-time"))
            sb.AppendFormat("gsm:'{0}',", GetGSMSignalImg(CInt(datatable.Rows(i)("fldGSMSignal"))))
            sb.AppendFormat("gps:{0:N0},", CInt(datatable.Rows(i)("fldGPSSat")))
            sb.AppendFormat("bds:{0:N0},", CInt(datatable.Rows(i)("fldBDSat")))
            sb.AppendFormat("battery:'{0:N0}%{1}',", CDec(datatable.Rows(i)("fldBatteryLvl")), If(statusarr(0).ToString.Equals("1"), "(" & GetText("Charging") & ")", ""))
            sb.AppendFormat("beltstatus:'{0}',", If(statusarr(1).ToString.Equals("1"), GetText("BeltOn"), GetText("BeltOff")))
            sb.AppendFormat("alarm:'{0}',", datatable.Rows(i)("fldAlarmEvent"))
            sb.AppendFormat("speed:'{0:N0}km/h',", CDec(datatable.Rows(i)("fldSpeed")) * 1.852)
            sb.AppendFormat("pincolor:'{0}',", "blue")
            sb.AppendFormat("pinglyphcolor:'{0}'", "black")
            If i = datatable.Rows.Count - 1 Then
                sb.Append("}")
            Else
                sb.Append("},")
            End If
        Next
        sb.Append("]")
        Return sb.ToString
    End Function

    Private Function ConvertPos(ByVal pos As String, ByVal posdir As String, ByVal postype As String) As Decimal
        Dim convert As Decimal = 0
        If postype.ToLower.Equals("lat") Then
            Dim deg As Decimal = pos.Substring(0, 2)
            Dim min As Decimal = pos.Substring(2)
            convert = deg + (min / 60)
            If posdir.ToLower.Equals("s") Then convert = 0 - convert 'south negative lat
        Else
            Dim deg As Decimal = pos.Substring(0, 3)
            Dim min As Decimal = pos.Substring(3)
            convert = deg + (min / 60)
            If posdir.ToLower.Equals("w") Then convert = 0 - convert 'west negative long
        End If
        Return convert
    End Function

    Private Function GetGSMSignalImg(ByVal gsm As Integer) As String
        Dim percent As Decimal = gsm / 31 * 100
        If percent >= 80 Then
            Return "signal_1.png"
        ElseIf percent >= 60 Then
            Return "signal_2.png"
        ElseIf percent >= 40 Then
            Return "signal_3.png"
        ElseIf percent >= 20 Then
            Return "signal_4.png"
        Else
            Return "signal_5.png"
        End If
    End Function

    Protected Sub timer_Tick(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMarkers(" & GetDeviceInfo() & ");", True)
    End Sub

    Protected Sub ddlEMD_SelectedIndexChanged(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "fetchAndUpdateMarkers();", True)
    End Sub

End Class