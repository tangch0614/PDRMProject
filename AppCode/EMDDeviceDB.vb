Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class EMDDeviceDB

#Region "Notification"

        Public Shared Function CountNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal myConnection As MySqlConnection) As Integer
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = " a.fldDateTime >= Date_add(NOW(), interval -5 Minute) And "
            If Not deviceid <= 0 Then query &= " a.fldID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not processstatus < 0 Then query &= " a.fldprocess = @processstatus And "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
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
            Return myCommand.ExecuteScalar
        End Function

        Public Shared Function GetAlertNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal userid As Long, ByVal page As String, ByVal intervalminute As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " a.fldOPPID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not userid < 0 Then query &= " f.fldID is null And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, b.fldimei AS fldemdimei, b.fldRLat, b.fldRLong, b.fldGPSStatus, b.fldBatteryLvl, b.fldDeviceStatus,
                                                                c.fldPhoto1,c.fldPhoto2, c.fldname AS fldOPPName, c.fldicno AS fldOPPICNo, c.fldcontactno AS fldOPPContactNo, ifnull(g.fldName,'') As fldDepartment,
                                                                IFNULL(d.fldName,'') AS fldPSName, IFNULL(d.fldContactNo,'') AS fldPSCOntactNo, 
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
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@userid", userid)
            myCommand.Parameters.AddWithValue("@page", page)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAlertNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer, ByVal limit As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            Dim limitstr As String = ""
            If Not intervalminute < 0 Then query &= " a.fldDateTime >= Date_add(NOW(), interval @intervalminute Minute) And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " a.fldOPPID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(severity) Then query &= " a.fldseverity = @severity And "
            If Not processstatus < 0 Then query &= " a.fldProcess = @processstatus And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            If Not limit <= 0 Then limitstr = " Limit @limit "
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, b.fldimei AS fldemdimei, b.fldRLat, b.fldRLong, b.fldGPSStatus, b.fldBatteryLvl, b.fldDeviceStatus,
                                                                c.fldPhoto1,c.fldPhoto2, c.fldname AS fldOPPName, c.fldicno AS fldOPPICNo, c.fldcontactno AS fldOPPContactNo, ifnull(f.fldName,'') As fldDepartment,
                                                                IFNULL(d.fldName,'') AS fldPSName, IFNULL(d.fldContactNo,'') AS fldPSCOntactNo, 
                                                                IFNULL(e.fldName,'') AS fldOverseerName, IFNULL(e.fldcontactno,'') AS fldOverseerContactNo, IFNULL(e.fldPoliceNo,'') AS fldOverseerPoliceNo
                                                                FROM tblpdrmnotification a 
                                                                JOIN tblemddevice b ON b.fldid=a.fldemddeviceid 
                                                                JOIN tblopp c ON c.fldid=a.fldoppid 
                                                                LEFT JOIN tblpolicestation d ON d.fldID=c.fldPoliceStationID
                                                                LEFT JOIN tbladmin e ON e.fldID=c.fldoverseerid 
                                                                LEFT JOIN tbldepartment f ON f.fldID=c.fldDeptID " _
                                                                & query & " Order by fldSeverity Desc, fldID Desc" & limitstr, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@severity", severity)
            myCommand.Parameters.AddWithValue("@processstatus", processstatus)
            myCommand.Parameters.AddWithValue("@intervalminute", 0 - intervalminute)
            myCommand.Parameters.AddWithValue("@limit", limit)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
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

        Public Shared Function UpdateProcessStatus(ByVal id As Long, ByVal creatorid As Long, ByVal remark As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand
            myCommand.CommandText = "Update tblpdrmnotification Set fldProcess=1, fldProcessUserID=@creatorid, fldremark=@remark, fldProcessDateTime=Now() Where fldID = @id"
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            myCommand.Parameters.AddWithValue("@remark", remark)
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

#End Region

#Region "EMD History"

        'Public Shared Function GetDeviceHistory(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String, ByVal myConnection As MySqlConnection) As DataTable
        '    Dim myDataTable As DataTable = New DataTable()
        '    Dim query As String = " MOD(Minute(fldDeviceDateTime),3)=0 And "
        '    If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
        '    If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
        '    If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
        '    If Not String.IsNullOrEmpty(frdatetime) Then query &= " a.flddevicedatetime >= @frdatetime AND "
        '    If Not String.IsNullOrEmpty(todatetime) Then query &= " a.flddevicedatetime <= @todatetime AND "
        '    If Not String.IsNullOrEmpty(query) Then
        '        query = " Where " & query
        '        query = query.Substring(0, query.Length - 4)
        '    End If
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.* From tblemdhistory a Join tblopp b ON b.fldID=a.fldOppID " & query & " Order by fldDeviceDateTime", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@deviceid", deviceid)
        '    myCommand.Parameters.AddWithValue("@oppid", oppid)
        '    myCommand.Parameters.AddWithValue("@imei", imei)
        '    myCommand.Parameters.AddWithValue("@frdatetime", frdatetime)
        '    myCommand.Parameters.AddWithValue("@todatetime", todatetime)
        '    Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
        '    adapter.Fill(myDataTable)
        '    Return myDataTable
        'End Function

        Public Shared Function GetDeviceHistory(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String, ByVal intervalsec As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = " a.fldDeviceDateTime>='2024-01-01' And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(frdatetime) Then query &= " a.flddevicedatetime >= @frdatetime AND "
            If Not String.IsNullOrEmpty(todatetime) Then query &= " a.flddevicedatetime <= @todatetime AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.* From tblemdhistory a Join tblopp b ON b.fldID=a.fldOppID " & query & " GROUP BY (UNIX_TIMESTAMP(a.fldDeviceDateTime) DIV @intervalsec) Order by a.fldDeviceDateTime", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@frdatetime", frdatetime)
            myCommand.Parameters.AddWithValue("@todatetime", todatetime)
            myCommand.Parameters.AddWithValue("@intervalsec", intervalsec)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetDeviceHistoryFiltered(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(frdatetime) Then query &= " a.flddevicedatetime >= @frdatetime AND "
            If Not String.IsNullOrEmpty(todatetime) Then query &= " a.flddevicedatetime <= @todatetime AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.* From tblemdhistory_filtered a Join tblopp b ON b.fldID=a.fldOppID " & query & " Order by fldDeviceDateTime", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@frdatetime", frdatetime)
            myCommand.Parameters.AddWithValue("@todatetime", todatetime)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

#End Region

#Region "EMD Device"

        Public Shared Function GetOPPID(ByVal id As Long, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldOPPID,0) from tblemddevice where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetIMEI(ByVal id As Long, ByVal myconn As MySqlConnection) As String
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldImei,0) from tblemddevice where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As String = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifyIMEI(ByVal imei As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice where fldimei=@imei", myconn)
            mycmd.Parameters.AddWithValue("@imei",imei)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifySimNo(ByVal simno As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice where fldsimno=@simno", myconn)
            mycmd.Parameters.AddWithValue("@simno", simno)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function CountEMDStatus(ByVal status As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select Count(*) from tblemddevice where fldstatus=@status", myconn)
            mycmd.Parameters.AddWithValue("@status", status)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetDevice(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As EMDDeviceObj
            Dim device As EMDDeviceObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblemddevice Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    device = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return device
        End Function

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal simno As String, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " a.fldID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(simno) Then query &= " a.fldsimno = @simno AND "
            If Not String.IsNullOrEmpty(status) Then query &= " a.fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, ifnull(f.fldgeofence,'') As fldGeofenceMk, ifnull(b.fldGeofence1,'') As fldGeofence1, ifnull(b.fldName,'') As fldOPPName, ifnull(b.fldICNo,'') As fldOPPICNo, ifnull(b.fldContactNo,'') As fldOPPContactNo, 
                                                                ifnull(c.fldColor,'grey') as fldDeptColor, ifnull(c.fldName,'') As fldDepartment, ifnull(d.fldName,'') As fldPSName, 
                                                                ifnull(e.fldName,'') As fldOverseerName, ifnull(e.fldContactNo,'') as fldOverseerContactNo, ifnull(e.fldPoliceNo,'') as fldOverseerPoliceNo
                                                                From tblemddevice a 
                                                                Left Join tblopp b ON b.fldID=a.fldOppID 
                                                                Left Join tbldepartment c On c.fldID=b.fldDeptID
                                                                Left Join tblpolicestation d On d.fldID=b.fldPoliceStationID 
                                                                Left Join tbladmin e On e.fldID=b.fldoverseerid
                                                                Left Join tblcountrymukim f On f.fldMukim=b.fldGeofenceMukim " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@simno", simno)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal imei As String, ByVal simno As String, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " fldID = @deviceid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " fldimei = @imei And "
            If Not String.IsNullOrEmpty(simno) Then query &= " fldsimno = @simno AND "
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblemddevice " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@simno", simno)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function UpdateOPPID(ByVal deviceid As String, ByVal oppid As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemddevice set fldOPPID=@oppid Where fldID=@deviceid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateStatus(ByVal deviceid As String, ByVal status As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemddevice set fldStatus=@status Where fldID=@deviceid", myConnection)
            myCommand.Parameters.AddWithValue("@status", status)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function Save(ByVal device As EMDDeviceObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If device.fldID = Nothing Then
                processExe = "Insert into tblemddevice (fldDateTime, fldName, fldImei, fldSimNo, fldSimNo2, fldStatus, fldWifiData, fldPayloadData, fldGeofenceJson) Values (Now(), @fldName, @fldImei, @fldSimNo, @fldSimNo2, @fldStatus, '', '' ,'')"
                isInsert = True
            Else
                processExe = "Update tblemddevice set fldName=@fldName, fldSimNo = @fldSimNo, fldSimNo2 = @fldSimNo2 Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", device.fldID)
            myCommand.Parameters.AddWithValue("@fldImei", device.fldImei)
            myCommand.Parameters.AddWithValue("@fldName", device.fldName)
            myCommand.Parameters.AddWithValue("@fldSimNo", device.fldSimNo)
            myCommand.Parameters.AddWithValue("@fldSimNo2", device.fldSimNo2)
            myCommand.Parameters.AddWithValue("@fldStatus", device.fldStatus)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblemddevice Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As EMDDeviceObj
            Dim device As EMDDeviceObj = New EMDDeviceObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                device.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                device.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldName"))) Then
                device.fldName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldImei"))) Then
                device.fldImei = myDataRecord.GetString(myDataRecord.GetOrdinal("fldImei"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSimNo"))) Then
                device.fldSimNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSimNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSimNo2"))) Then
                device.fldSimNo2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSimNo2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastPing"))) Then
                device.fldLastPing = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastPing"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLong"))) Then
                device.fldLong = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLong"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLat"))) Then
                device.fldLat = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLat"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                device.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            Return device
        End Function
    End Class

End NameSpace 
