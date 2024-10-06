Imports MySql.Data.MySqlClient
Imports System.Web.Services
Imports System.Web.Script.Services
Imports AppCode.BusinessLogic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class GetData
    Inherits System.Web.UI.Page

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'GetNotificationData(-1, -1, 0, "test")
        'GetEMDHistory(-1, 5, "", "")
        'GetMarkers(-1, "", "", "Y")
        'GetEMDDeviceList("-1", -1, "Y", "Y")
    End Sub

#Region "Notification"

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDashboardData() As DashboardData
        Dim data As New DashboardData() With {
            .login_user = SessionManager.CountLoggedInUser(),
            .active_emd = EMDDeviceManager.CountActiveEMD(),
            .inactive_emd = EMDDeviceManager.CountInactiveEMD(),
            .total_alert = AlertManager.CountAlert(-1, -1, -1, 0, "", -1),
            .jenayah_alert = AlertManager.CountAlert(-1, -1, 1, 0, "", -1),
            .komersil_alert = AlertManager.CountAlert(-1, -1, 2, 0, "", -1),
            .narkotik_alert = AlertManager.CountAlert(-1, -1, 3, 0, "", -1),
            .cawangankhas_alert = AlertManager.CountAlert(-1, -1, 4, 0, "", -1)
            }
        Return data
    End Function

    <WebMethod()>
    Public Shared Function GetAlertList(processstatus As Integer, severity As String, limit As Integer) As String
        Dim myDataTable As DataTable = AlertManager.GetAlertList(-1, -1, -1, processstatus, "", severity, "", "", -1, limit, " fldID Desc ")
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
    Public Shared Function GetAlertListCount(processstatus As Integer, severity As String) As Integer
        Return AlertManager.CountAlert(-1, -1, -1, processstatus, severity, -1)
    End Function

    <WebMethod()>
    Public Shared Function GetNotificationData(ByVal deviceid As Long, ByVal oppid As Long, ByVal userid As Long, ByVal page As String) As String 'Notify opp page
        Dim dataTable As DataTable = AlertManager.GetAlertNotification(-1, deviceid, oppid, "", userid, 0, page, 5, True)
        dataTable.Columns.Add("fldMD5", GetType(String))
        For i As Integer = 0 To dataTable.Rows.Count - 1
            dataTable.Rows(i)("fldMD5") = UtilityManager.MD5Encrypt(dataTable.Rows(i)("fldID") & "processalert")
        Next
        ' Convert the DataTable to JSON
        Dim json As String = JsonConvert.SerializeObject(dataTable)
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

    Public Class DashboardData
        Public Property login_user As Integer
        Public Property active_emd As Integer
        Public Property inactive_emd As Integer
        Public Property total_alert As Integer
        Public Property jenayah_alert As Integer
        Public Property komersil_alert As Integer
        Public Property narkotik_alert As Integer
        Public Property cawangankhas_alert As Integer

    End Class

#End Region

#Region "Map"

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEMDHistory(ByVal deviceid As Long, ByVal oppid As Long, ByVal frdatetime As String, ByVal todatetime As String) As List(Of EMDDeviceInfo)
        Dim markers As New List(Of EMDDeviceInfo)()
        Dim dataTable As DataTable = EMDDeviceManager.GetDeviceHistoryFiltered(deviceid, oppid, "", frdatetime, todatetime)
        'If dataTable Is Nothing OrElse dataTable.Rows.Count = 0 Then
        '    dataTable = EMDDeviceManager.GetDeviceHistory(deviceid, oppid, "", frdatetime, todatetime, 180)
        'End If
        ' Convert the DataTable to JSON
        Dim base As New Base
        For i As Integer = 0 To dataTable.Rows.Count - 1
            Dim statusarr As Array = CStr(dataTable.Rows(i)("fldDeviceStatus")).ToCharArray
            Dim marker As New EMDDeviceInfo() With {
                .emdid = dataTable.Rows(i)("fldEMDDeviceID"),
                .datetime = CDate(dataTable.Rows(i)("fldDeviceDateTime")).ToString("yyyy-MM-dd HH:mm:ss"),
                .datetimeto = If(dataTable.Columns.Contains("fldDeviceDateTimeTo"), CDate(dataTable.Rows(i)("fldDeviceDateTimeTo")).ToString("yyyy-MM-dd HH:mm:ss"), ""),
                .name = dataTable.Rows(i)("fldName"),
                .imei = dataTable.Rows(i)("fldImei"),
                .lat = dataTable.Rows(i)("fldLat"),
                .lng = dataTable.Rows(i)("fldLong"),
                .locstatus = If(dataTable.Rows(i)("fldGPSStatus").Equals("V"), base.GetText("LastGPS"), base.GetText("GPS")),
                .datastatus = base.GetText("Real-time"),
                .gsm = GetGSMSignalImg(CInt(dataTable.Rows(i)("fldGSMSignalPercent"))),
                .battery = String.Format("{0}%{1}", CDec(dataTable.Rows(i)("fldBatteryLvl")), If(statusarr(0).ToString.Equals("1"), "(" & base.GetText("Charging") & ")", "")),
                .beltstatus = If(statusarr(1).ToString.Equals("1"), base.GetText("BeltOn"), base.GetText("BeltOff")),
                .alarm = dataTable.Rows(i)("fldAlarmEvent"),
                .speed = CDec(dataTable.Rows(i)("fldSpeedkmh")).ToString("N2") & "km/h",
                .geofence = dataTable.Rows(i)("fldGeofenceMk"),'datatable.Rows(i)("fldGeofence1"),
                .pincolor = "blue",
                .pinglyphcolor = "black"
            }
            markers.Add(marker)
        Next
        Return markers
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEMDHistoryFilter(ByVal deviceid As Long, ByVal oppid As Long, ByVal frdatetime As String, ByVal todatetime As String, ByVal filtertype As Integer) As List(Of EMDDeviceInfo)
        Dim markers As New List(Of EMDDeviceInfo)()
        Dim dataTable As DataTable = New DataTable
        If filtertype = 0 Then
            dataTable = EMDDeviceManager.GetDeviceHistoryRAW(deviceid, oppid, "", frdatetime, todatetime)
        ElseIf filtertype = 1 Then
            dataTable = EMDDeviceManager.GetDeviceHistory(deviceid, oppid, "", frdatetime, todatetime, 180)
        ElseIf filtertype = 2 Then
            dataTable = EMDDeviceManager.GetDeviceHistoryFiltered(deviceid, oppid, "", frdatetime, todatetime)
        End If
        ' Convert the DataTable to JSON
        Dim base As New Base
        For i As Integer = 0 To dataTable.Rows.Count - 1
            Dim statusarr As Array = CStr(dataTable.Rows(i)("fldDeviceStatus")).ToCharArray
            Dim marker As New EMDDeviceInfo() With {
                .emdid = dataTable.Rows(i)("fldEMDDeviceID"),
                .datetime = CDate(dataTable.Rows(i)("fldDeviceDateTime")).ToString("yyyy-MM-dd HH:mm:ss"),
                .datetimeto = If(dataTable.Columns.Contains("fldDeviceDateTimeTo"), CDate(dataTable.Rows(i)("fldDeviceDateTimeTo")).ToString("yyyy-MM-dd HH:mm:ss"), ""),
                .name = dataTable.Rows(i)("fldName"),
                .imei = dataTable.Rows(i)("fldImei"),
                .lat = dataTable.Rows(i)("fldLat"),
                .lng = dataTable.Rows(i)("fldLong"),
                .locstatus = If(dataTable.Rows(i)("fldGPSStatus").Equals("V"), base.GetText("LastGPS"), base.GetText("GPS")),
                .datastatus = base.GetText("Real-time"),
                .gsm = GetGSMSignalImg(CInt(dataTable.Rows(i)("fldGSMSignalPercent"))),
                .battery = String.Format("{0}%{1}", CDec(dataTable.Rows(i)("fldBatteryLvl")), If(statusarr(0).ToString.Equals("1"), "(" & base.GetText("Charging") & ")", "")),
                .beltstatus = If(statusarr(1).ToString.Equals("1"), base.GetText("BeltOn"), base.GetText("BeltOff")),
                .alarm = dataTable.Rows(i)("fldAlarmEvent"),
                .speed = CDec(dataTable.Rows(i)("fldSpeedkmh")).ToString("N2") & "km/h",
                .geofence = dataTable.Rows(i)("fldGeofenceMk"),'datatable.Rows(i)("fldGeofence1"),
                .pincolor = "blue",
                .pinglyphcolor = "black"
            }
            markers.Add(marker)
        Next
        Return markers
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEMDDeviceInfo(ByVal deviceid As Long, ByVal oppid As Long) As EMDDeviceInfo
        Dim datatable As DataTable = EMDDeviceManager.GetDeviceList(deviceid, oppid, -1, "", "", "", "", "", "", "", "")
        Dim base As New Base
        If Not datatable Is Nothing AndAlso datatable.Rows.Count > 0 Then
            If Not String.IsNullOrWhiteSpace(datatable.Rows(0)("fldRLat")) AndAlso Not String.IsNullOrWhiteSpace(datatable.Rows(0)("fldRLong")) AndAlso CInt(datatable.Rows(0)("fldInit")) = 1 Then
                Dim statusarr As Array = CStr(datatable.Rows(0)("fldDeviceStatus")).ToCharArray
                Dim deviceinfo As New EMDDeviceInfo() With {
                    .emdid = datatable.Rows(0)("fldID"),
                    .datetime = CDate(datatable.Rows(0)("fldLastPing")).ToString("yyyy-MM-dd HH:mm:ss"),
                    .name = If(String.IsNullOrWhiteSpace(datatable.Rows(0)("fldName")), datatable.Rows(0)("fldImei"), datatable.Rows(0)("fldName")),
                    .imei = datatable.Rows(0)("fldImei"),
                    .lat = datatable.Rows(0)("fldRLat"),
                    .lng = datatable.Rows(0)("fldRLong"),
                    .locstatus = If(datatable.Rows(0)("fldGPSStatus").Equals("V"), base.GetText("LastGPS"), base.GetText("GPS")),
                    .datastatus = base.GetText("Real-time"),
                    .gsm = GetGSMSignalImg(CInt(datatable.Rows(0)("fldGSMSignalPercent"))),
                    .gps = CInt(datatable.Rows(0)("fldGPSSat")),
                    .bds = CInt(datatable.Rows(0)("fldBDSat")),
                    .battery = String.Format("{0}%{1}", CDec(datatable.Rows(0)("fldBatteryLvl")), If(statusarr(0).ToString.Equals("1"), "(" & base.GetText("Charging") & ")", "")),
                    .beltstatus = If(statusarr(1).ToString.Equals("1"), base.GetText("BeltOn"), base.GetText("BeltOff")),
                    .alarm = datatable.Rows(0)("fldAlarmEvent"),
                    .speed = CDec(datatable.Rows(0)("fldSpeedkmh")).ToString("N2") & "km/h",
                    .geofence = datatable.Rows(0)("fldGeofenceMk"),'datatable.Rows(i)("fldGeofence1"),
                    .pincolor = datatable.Rows(0)("fldDeptColor"),
                    .pinglyphcolor = "black"
                }
                Return deviceinfo
            End If
        End If
        Return Nothing
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetEMDDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal devicestatus As String, ByVal oppstatus As String) As List(Of EMDDeviceInfo)
        Dim markers As New List(Of EMDDeviceInfo)()
        Dim datatable As DataTable = EMDDeviceManager.GetDeviceList(deviceid, oppid, -1, "", "", "", "", "", "", devicestatus, oppstatus)
        Dim base As New Base
        For i As Integer = 0 To datatable.Rows.Count - 1
            If Not String.IsNullOrWhiteSpace(datatable.Rows(i)("fldRLat")) AndAlso Not String.IsNullOrWhiteSpace(datatable.Rows(i)("fldRLong")) AndAlso CInt(datatable.Rows(i)("fldInit")) = 1 Then
                Dim statusarr As Array = CStr(datatable.Rows(i)("fldDeviceStatus")).ToCharArray
                Dim marker As New EMDDeviceInfo() With {
                    .emdid = datatable.Rows(i)("fldID"),
                    .datetime = CDate(datatable.Rows(i)("fldLastPing")).ToString("yyyy-MM-dd HH:mm:ss"),
                    .name = If(String.IsNullOrWhiteSpace(datatable.Rows(i)("fldName")), datatable.Rows(i)("fldImei"), datatable.Rows(i)("fldName")),
                    .imei = datatable.Rows(i)("fldImei"),
                    .lat = datatable.Rows(i)("fldRLat"),
                    .lng = datatable.Rows(i)("fldRLong"),
                    .locstatus = If(datatable.Rows(i)("fldGPSStatus").Equals("V"), base.GetText("LastGPS"), base.GetText("GPS")),
                    .datastatus = base.GetText("Real-time"),
                    .gsm = GetGSMSignalImg(CInt(datatable.Rows(i)("fldGSMSignalPercent"))),
                    .gps = CInt(datatable.Rows(i)("fldGPSSat")),
                    .bds = CInt(datatable.Rows(i)("fldBDSat")),
                    .battery = String.Format("{0}%{1}", CDec(datatable.Rows(i)("fldBatteryLvl")), If(statusarr(0).ToString.Equals("1"), "(" & base.GetText("Charging") & ")", "")),
                    .beltstatus = If(statusarr(1).ToString.Equals("1"), base.GetText("BeltOn"), base.GetText("BeltOff")),
                    .alarm = datatable.Rows(i)("fldAlarmEvent"),
                    .speed = CDec(datatable.Rows(i)("fldSpeedkmh")).ToString("N2") & "km/h",
                    .geofence = datatable.Rows(i)("fldGeofenceMK"),'datatable.Rows(i)("fldGeofence1"),
                    .pincolor = datatable.Rows(i)("fldDeptColor"),
                    .pinglyphcolor = "black",
                    .oppname = datatable.Rows(i)("fldOPPName"),
                    .oppicno = datatable.Rows(i)("fldOPPICNo"),
                    .oppcontactno = datatable.Rows(i)("fldOPPContactNo"),
                    .department = datatable.Rows(i)("fldDepartment"),
                    .psname = datatable.Rows(i)("fldPSName"),
                    .pscontactno = datatable.Rows(i)("fldPSContactNo"),
                    .offname = datatable.Rows(i)("fldOverseerName"),
                    .offcontactno = datatable.Rows(i)("fldOverseerContactNo"),
                    .offpoliceno = datatable.Rows(i)("fldOverseerPoliceNo")
                }
                markers.Add(marker)
            End If
        Next
        Return markers
    End Function

    'Private Shared Function PinColor(ByVal dept As String) As String
    '    If dept.Equals("Jenayah") Then
    '        Return "#AF38EB"
    '    ElseIf dept.Equals("Komersil") Then
    '        Return "#808080"
    '    ElseIf dept.Equals("Narkotik") Then
    '        Return "#14AAF5"
    '    ElseIf dept.Equals("Cawangan Khas") Then
    '        Return "#E05194"
    '    Else
    '        Return "blue"
    '    End If
    'End Function

    Private Shared Function ConvertPos(ByVal pos As String, ByVal posdir As String, ByVal postype As String) As Decimal
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

    Private Shared Function GetGSMSignalImg(ByVal gsm As Decimal) As String
        Dim percent As Decimal = gsm 'If(gsm > 0, gsm / 31 * 100, 0)
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


    Public Class EMDDeviceInfo
        Public Property emdid As Integer
        Public Property datetime As String
        Public Property datetimeto As String
        Public Property name As String
        Public Property imei As String
        Public Property lat As Decimal
        Public Property lng As Decimal
        Public Property locstatus As String
        Public Property datastatus As String
        Public Property gsm As String
        Public Property gps As Integer
        Public Property bds As Integer
        Public Property battery As String
        Public Property beltstatus As String
        Public Property alarm As String
        Public Property speed As String
        Public Property geofence As String
        Public Property pincolor As String
        Public Property pinglyphcolor As String
        Public Property oppname As String
        Public Property oppicno As String
        Public Property oppcontactno As String
        Public Property department As String
        Public Property psname As String
        Public Property pscontactno As String
        Public Property offname As String
        Public Property offcontactno As String
        Public Property offpoliceno As String

    End Class
#End Region
End Class
