Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports System.Transactions

Namespace DataAccess
    Public Class AlertDB

#Region "Alert"

        Public Shared Function GetViolateTermsList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select Distinct Upper(fldmsg) As fldAlert From tblpdrmnotification", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetProcessStatus(ByVal alertid As Long, ByVal myConnection As MySqlConnection) As Integer
            Dim mycommand As MySqlCommand = New MySqlCommand("Select ifnull(fldProcessStatus,0) from tblpdrmnotification where fldid=@alertid", myConnection)
            mycommand.Parameters.AddWithValue("@alertid", alertid)
            Dim result As Integer = mycommand.ExecuteScalar()
            Return result
        End Function

        Public Shared Function CountAlert(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer, ByVal myConnection As MySqlConnection) As Integer
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not deviceid <= 0 Then query &= " a.fldID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT count(*) FROM tblpdrmnotification a JOIN tblopp b ON b.fldid=a.fldoppid " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            Return myCommand.ExecuteScalar
        End Function

        Public Shared Function CountAlertGroup(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim datatable As DataTable = New DataTable()
            Dim query As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not deviceid <= 0 Then query &= " a.fldID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT Upper(fldMsg) As fldMsg, count(*) as fldCount, fldseverity FROM tblpdrmnotification a JOIN tblopp b ON b.fldid=a.fldoppid " & query & " group by fldMsg order by FIELD(fldSeverity,'high','medium','low','curfew'), fldMsg", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            Dim adp As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adp.Fill(datatable)
            Return datatable
        End Function

        Public Shared Function GetAlertNotification(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal userid As Long, ByVal processstatus As Integer, ByVal page As String, ByVal intervalminute As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not alertid <= 0 Then query &= " a.fldID = @alertid And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " a.fldOPPID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not userid < 0 Then query &= " f.fldID is null And "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, b.fldimei AS fldemdimei, b.fldRLat, b.fldRLong, b.fldGPSStatus, b.fldBatteryLvl, b.fldDeviceStatus,
                                                                c.fldPhoto1,c.fldPhoto2, c.fldname AS fldOPPName, c.fldicno AS fldOPPICNo, c.fldcontactno AS fldOPPContactNo, c.fldMukim, ifnull(g.fldName,'') As fldDepartment,
                                                                IFNULL(d.fldName,'') AS fldPSName, IFNULL(d.fldContactNo,'') AS fldPSContactNo, 
                                                                IFNULL(e.fldName,'') AS fldOverseerName, IFNULL(e.fldcontactno,'') AS fldOverseerContactNo, IFNULL(e.fldPoliceNo,'') AS fldOverseerPoliceNo
                                                                FROM tblpdrmnotification a 
                                                                JOIN tblemddevice b ON b.fldid=a.fldemddeviceid 
                                                                JOIN tblopp c ON c.fldid=a.fldoppid 
                                                                LEFT JOIN tblpolicestation d ON d.fldID=c.fldPoliceStationID
                                                                LEFT JOIN tbladmin e ON e.fldID=c.fldoverseerid 
                                                                LEFT JOIN tblpdrmnotification_seen f ON f.fldNoticeID=a.fldID and f.fldUserID=@userid and f.fldPage=@page
                                                                LEFT JOIN tbldepartment g ON g.fldID=c.fldDeptID " _
                                                                & query & " Order by fldSeverity Desc, fldID Desc", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@alertid", alertid)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@userid", userid)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@page", page)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAlertInfoList(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal overseerid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal intervalminute As Integer, ByVal limit As Integer, ByVal orderstr As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            Dim limitstr As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not alertid <= 0 Then query &= " a.fldID = @alertid And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " a.fldOPPID = @oppid And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " a.fldDateTime >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " a.fldDateTime < date_add(@dateTo,interval 1 day) AND "
            If Not overseerid < 0 Then query &= " c.fldOverseerID = @overseerid And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            If Not String.IsNullOrWhiteSpace(orderstr) Then orderstr = " Order By " & orderstr
            If Not limit <= 0 Then limitstr = " Limit @limit "
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, b.fldimei AS fldemdimei, b.fldRLat, b.fldRLong, b.fldGPSStatus, b.fldBatteryLvl, b.fldDeviceStatus,
                                                                c.fldPhoto1,c.fldPhoto2, c.fldname AS fldOPPName, c.fldicno AS fldOPPICNo, c.fldcontactno AS fldOPPContactNo, c.fldMukim, ifnull(f.fldName,'') As fldDepartment,
                                                                IFNULL(d.fldName,'') AS fldPSName, IFNULL(d.fldContactNo,'') AS fldPSContactNo, 
                                                                IFNULL(e.fldName,'') AS fldOverseerName, IFNULL(e.fldcontactno,'') AS fldOverseerContactNo, IFNULL(e.fldPoliceNo,'') AS fldOverseerPoliceNo,
                                                                IFNULL(g.fldName,'') As fldProcessByName, IFNULL(h.fldName,'') As fldLastProcessByName                                                                    
                                                                FROM tblpdrmnotification a 
                                                                JOIN tblemddevice b ON b.fldid=a.fldemddeviceid 
                                                                JOIN tblopp c ON c.fldid=a.fldoppid 
                                                                LEFT JOIN tblpolicestation d ON d.fldID=c.fldPoliceStationID
                                                                LEFT JOIN tbladmin e ON e.fldID=c.fldoverseerid 
                                                                LEFT JOIN tbldepartment f ON f.fldID=c.fldDeptID 
                                                                Left JOIN tbladmin g ON g.fldID=a.fldProcessUserID and a.fldProcessUserID>0 
                                                                Left JOIN tbladmin h ON h.fldID=a.fldLastProcessUserID and a.fldLastProcessUserID>0 " _
                                                                & query & orderstr & limitstr, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@alertid", alertid)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@overseerid", overseerid)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            myCommand.Parameters.AddWithValue("@limit", limit)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAlertList(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppID As Long, ByVal processstatus As Integer, ByVal alerttype As String, ByVal severity As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal intervalminute As Integer, ByVal limit As Integer, ByVal orderstr As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = ""
            Dim limitstr As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not alertid <= 0 Then query &= " a.fldID = @alertid And "
            If Not oppID <= 0 Then query &= " a.fldoppID = @oppID AND "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid AND "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not String.IsNullOrEmpty(alerttype) Then query &= " a.fldmsg = @alerttype And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " a.fldDateTime >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " a.fldDateTime < date_add(@dateTo,interval 1 day) AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            If Not String.IsNullOrWhiteSpace(orderstr) Then orderstr = " Order By " & orderstr
            If Not limit <= 0 Then limitstr = " Limit @limit "
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, IFNULL(b.fldimei,'') AS fldemdimei, c.fldname AS fldOPPName, c.fldicno AS fldOPPICNo, c.fldMukim,
                                                                IFNULL(d.fldName,'') As fldProcessByName, IFNULL(e.fldName,'') As fldLastProcessByName 
                                                                FROM tblpdrmnotification a 
                                                                JOIN tblemddevice b ON b.fldid=a.fldemddeviceid 
                                                                JOIN tblopp c ON c.fldid=a.fldoppid 
                                                                Left JOIN tbladmin d ON d.fldID=a.fldProcessUserID and a.fldProcessUserID>0 
                                                                Left JOIN tbladmin e ON e.fldID=a.fldLastProcessUserID and a.fldLastProcessUserID>0  " & query & orderstr & limitstr, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@alertid", alertid)
            myCommand.Parameters.AddWithValue("@oppID", oppID)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@alerttype", alerttype)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            myCommand.Parameters.AddWithValue("@limit", limit)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAlertReport(ByVal deviceid As Long, ByVal oppID As Long, ByVal actsid As Long, ByVal state As String, ByVal mukim As String, ByVal alerttype As String, ByVal processstatus As Integer, ByVal severity As String, ByVal dateTimeFrom As String, ByVal dateTimeTo As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = ""
            If Not oppID <= 0 Then query &= " a.fldoppID = @oppID AND "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid AND "
            If Not actsid <= 0 Then query &= " a.fldActsID = @actsid AND "
            If Not processstatus < 0 Then query &= " a.fldProcessStatus = @processstatus And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not String.IsNullOrEmpty(state) Then query &= " c.fldstate = @state And "
            If Not String.IsNullOrEmpty(mukim) Then query &= " c.fldmukim = @mukim And "
            If Not String.IsNullOrEmpty(alerttype) Then query &= " a.fldmsg = @alerttype And "
            If Not String.IsNullOrEmpty(dateTimeFrom) Then query &= " a.fldDateTime >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTimeTo) Then query &= " a.fldDateTime < @dateTo AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, IFNULL(b.fldname,'') AS fldemdname, IFNULL(b.fldimei,'') AS fldemdimei, ifnull(c.fldname,'') AS fldOPPName, ifnull(c.fldicno,'') AS fldOPPICNo,
                                                                ifnull(c.fldState,'') AS fldState, ifnull(c.fldMukim,'') AS fldMukim,
                                                                IFNULL(d.fldName,'') AS fldProcessByName, IFNULL(e.fldName,'') AS fldLastProcessByName,
								                                IFNULL(f.fldname,'') AS fldacts, IFNULL(g.fldname,'') AS fldactssection, IFNULL(h.fldremark,'') AS fldprocessremark
                                                                FROM tblpdrmnotification a 
                                                                JOIN tblemddevice b ON b.fldid=a.fldemddeviceid 
                                                                JOIN tblopp c ON c.fldid=a.fldoppid 
                                                                LEFT JOIN tbladmin d ON d.fldID=a.fldProcessUserID AND a.fldProcessUserID>0 
                                                                LEFT JOIN tbladmin e ON e.fldID=a.fldLastProcessUserID AND a.fldLastProcessUserID>0 
                                                                LEFT JOIN tblacts f ON f.fldID=c.fldActsID LEFT JOIN tblactssection g ON g.fldid=c.fldActsSectionID 
                                                                LEFT JOIN tblpdrmnotification_response h ON h.fldalertid=a.fldid AND h.fldProcessStatus=2 " & query & " Order by fldDateTime Desc ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@oppID", oppID)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@actsid", actsid)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@state", state)
            myCommand.Parameters.AddWithValue("@mukim", mukim)
            myCommand.Parameters.AddWithValue("@alerttype", alerttype)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@dateFrom", dateTimeFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTimeTo)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAlertRemarkHist(ByVal alertid As Long, ByVal myconnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select Group_Concat(Concat(Date_format(a.fldDateTime, '%Y-%m-%d %H:%i:%s'),' [',ifnull(c.fldrolename,''),' ',ifnull(b.fldname,''),']: ',a.fldRemark) SEPARATOR '\n\n') from tblpdrmnotification_response a left Join tbladmin b on b.fldID=a.fldProcessUserID left join tbladmintype c on c.fldroleid=b.fldlevel where fldalertid=@alertid group by fldalertid", myconnection)
            myCommand.Parameters.AddWithValue("@alertid", alertid)
            Dim result As String = myCommand.ExecuteScalar()
            Return result
        End Function

        Public Shared Function UpdateAlertNotificationSeenUser(ByVal ids As List(Of Long), ByVal userid As Long, ByVal page As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            Dim inquery As String = ""
            For i As Integer = 0 To ids.Count - 1
                inquery &= String.Format("(@noticeid{0},@userid,@page,Now()){1}", i, If(i < ids.Count - 1, ",", ""))
                myCommand.Parameters.AddWithValue("@noticeid" & i, ids(i))
            Next
            myCommand.Parameters.AddWithValue("@userid", userid)
            myCommand.Parameters.AddWithValue("@page", page)
            myCommand.CommandText = "Insert Into tblpdrmnotification_seen (fldNoticeID,fldUserID,fldPage,fldDateTime) Values " & inquery
            myCommand.Connection = myConnection
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

        Public Shared Function UpdateProcessStatus(ByVal id As Long, ByVal newstatus As String, ByVal oldstatus As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            myCommand.CommandText = "Update tblpdrmnotification Set fldProcessStatus=@newstatus Where fldID = @id and fldProcessStatus=@oldstatus"
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@newstatus", newstatus)
            myCommand.Parameters.AddWithValue("@oldstatus", oldstatus)
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

        Public Shared Function SaveProcessLog(ByVal id As Long, ByVal status As String, ByVal creatorid As Long, ByVal remark As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            myCommand.CommandText = "Insert into tblpdrmnotification_response (fldAlertID,fldDateTime,fldProcessUserID,fldProcessStatus,fldRemark) Values (@id,Now(),@creatorid,@status,@remark)"
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            myCommand.Parameters.AddWithValue("@status", status)
            myCommand.Parameters.AddWithValue("@remark", remark)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateProcessUserID(ByVal id As Long, ByVal creatorid As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            myCommand.CommandText = "Update tblpdrmnotification Set fldProcessUserID=@creatorid, fldProcessDateTime=Now() Where fldID = @id"
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

        Public Shared Function UpdateLastProcessUserID(ByVal id As Long, ByVal creatorid As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            myCommand.CommandText = "Update tblpdrmnotification Set fldLastProcessUserID=@creatorid, fldLastProcessDateTime=Now() Where fldID = @id"
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

        Public Shared Function InsertAlert(ByVal fldIMEI As String, ByVal oppid As Long, ByVal EMDDeviceID As Long, ByVal fldMsg As String, ByVal Level As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("insert into tblpdrmnotification (fldDatetime,fldIMEI,fldoppid,fldEMDDeviceID,fldMsg,fldSeverity) values(now(),@fldIMEI,@fldoppid,@fldEMDDeviceID, @fldMsg, @fldSeverity)", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldEMDDeviceID", EMDDeviceID)
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)
            myCommand.Parameters.AddWithValue("@fldMsg", fldMsg)
            myCommand.Parameters.AddWithValue("@fldSeverity", Level)
            myCommand.Parameters.AddWithValue("@fldoppid", oppid)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

#End Region

    End Class

End Namespace
