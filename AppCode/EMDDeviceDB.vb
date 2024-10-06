Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class EMDDeviceDB
#Region "Command"

        Public Shared Function GetConnectionID(ByVal fldIMEI As String, ByVal myConnection As MySqlConnection) As Long
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT fldConnectionID FROM tblpdrmdeviceconn WHERE fldIMEI = @fldIMEI order by fldDatetime desc limit 1;", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)
            Dim result As Long = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function InsertGPRSCommand(ByVal fldConnectionID As Long, ByVal fldIMEI As String, ByVal fldMsg As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("insert into tblpdrmoutmsg (fldDatetime,fldConnectionID,fldIMEI,fldMsg) values(now(),@fldConnectionID,@fldIMEI, @fldMsg)", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldConnectionID", fldConnectionID)
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)
            myCommand.Parameters.AddWithValue("@fldMsg", fldMsg)
            Dim result As Boolean = myCommand.ExecuteNonQuery()
            Return result
        End Function

#End Region

#Region "EMD History"

        Public Shared Function GetDeviceHistoryRAW(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = "" '" MOD(Minute(fldDeviceDateTime),3)=0 And "
            If Not deviceid <= 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(frdatetime) Then query &= " a.flddevicedatetime >= @frdatetime AND "
            If Not String.IsNullOrEmpty(todatetime) Then query &= " a.flddevicedatetime <= @todatetime AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.*, ifnull(c.fldGeofence,'') as fldGeofenceMK, a.fldSpeed*1.852 as fldSpeedKmh, 
                                                                if(a.fldGSMSignal>0,fldGSMSignal/31*100,0) As fldGSMSignalPercent,
                                                                if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, 
                                                                if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus 
                                                                From tblemdhistory a Join tblopp b ON b.fldID=a.fldOppID 
                                                                Left Join tblcountrymukim c on c.fldMukim=b.fldGeofenceMukim " & query & " Order by fldDeviceDateTime", myConnection)
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

        Public Shared Function GetDeviceHistory_PDF(ByVal deviceid As Long, ByVal oppid As Long, ByVal frdatetime As String, ByVal todatetime As String, ByVal intervalsec As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " fldEMDDeviceID = @deviceid And "
            If Not oppid <= 0 Then query &= " fldoppid = @oppid And "
            If Not String.IsNullOrEmpty(frdatetime) Then query &= " flddevicedatetime >= @frdatetime AND "
            If Not String.IsNullOrEmpty(todatetime) Then query &= " flddevicedatetime <= @todatetime AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT *, fldSpeed*1.852 as fldSpeedKmh, if(fldGSMSignal>0,fldGSMSignal/31*100,0) As fldGSMSignalPercent,if(flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, if(flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus FROM tblemdhistory" & query & " GROUP BY (UNIX_TIMESTAMP(fldDeviceDateTime) DIV @intervalsec) Order by fldDeviceDateTime", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@frdatetime", frdatetime)
            myCommand.Parameters.AddWithValue("@todatetime", todatetime)
            myCommand.Parameters.AddWithValue("@intervalsec", intervalsec)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.*, ifnull(c.fldGeofence,'') as fldGeofenceMK, fldSpeed*1.852 as fldSpeedKmh, 
                                                                if(a.fldGSMSignal>0,a.fldGSMSignal/31*100,0) As fldGSMSignalPercent,
                                                                if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, 
                                                                if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus 
                                                                From tblemdhistory a 
                                                                Join tblopp b ON b.fldID=a.fldOppID
                                                                Left Join tblcountrymukim c on c.fldMukim=b.fldGeofenceMukim " & query & " GROUP BY (UNIX_TIMESTAMP(a.fldDeviceDateTime) DIV @intervalsec) Order by a.fldDeviceDateTime", myConnection)
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
            If Not String.IsNullOrEmpty(frdatetime) Then query &= " (a.flddevicedatetime >= @frdatetime Or a.flddevicedatetimeto >= @frdatetime) AND "
            If Not String.IsNullOrEmpty(todatetime) Then query &= " (a.flddevicedatetime <= @todatetime Or a.flddevicedatetimeto <= @todatetime) AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.*, ifnull(c.fldGeofence,'') as fldGeofenceMK, a.fldSpeed*1.852 as fldSpeedKmh, 
                                                                if(a.fldGSMSignal>0,a.fldGSMSignal/31*100,0) As fldGSMSignalPercent,
                                                                if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, 
                                                                if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus 
                                                                From tblemdhistory_filtered a Join tblopp b ON b.fldID=a.fldOppID 
                                                                Left Join tblcountrymukim c on c.fldMukim=b.fldGeofenceMukim " & query & " Order by fldDeviceDateTime", myConnection)
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

        Public Shared Function GetEMDDeviceID(ByVal oppid As Long, ByVal name As String, ByVal imei As String, ByVal myconn As MySqlConnection) As Long
            Dim query As String = ""
            If Not oppid <= 0 Then query &= " fldoppid = @oppid And "
            If Not String.IsNullOrEmpty(name) Then query &= " fldname = @name And "
            If Not String.IsNullOrEmpty(imei) Then query &= " fldimei = @imei AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice " & query, myconn)
            mycmd.Parameters.AddWithValue("@oppid", oppid)
            mycmd.Parameters.AddWithValue("@name", name)
            mycmd.Parameters.AddWithValue("@imei", imei)
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

        Public Shared Function VerifyName(ByVal name As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice where fldName=@name", myconn)
            mycmd.Parameters.AddWithValue("@name", name)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifySerialNum(ByVal serialno As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice where fldSN=@serialno", myconn)
            mycmd.Parameters.AddWithValue("@serialno", serialno)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifySimNo(ByVal simno As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblemddevice where fldsimno=@simno", myconn)
            mycmd.Parameters.AddWithValue("@simno", simno)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function CountActiveEMDList_ByDept(ByVal myconn As MySqlConnection) As DataTable
            Dim datatable As DataTable = New DataTable
            Dim mycmd As MySqlCommand = New MySqlCommand("SELECT a.fldID, a.fldName As fldDepartment, IFNULL(d.fldCount,0) as fldCount FROM tbldepartment a 
                                                            LEFT JOIN (SELECT c.fldDeptID, COUNT(*) AS fldCount 
                                                                        FROM tblemddevice b 
                                                                        JOIN tblopp c ON c.fldid=b.fldoppid 
                                                                        WHERE b.fldstatus='Y' and c.fldstatus='Y' GROUP BY c.fldDeptID) d ON d.fldDeptID=a.fldID
                                                            WHERE a.fldStatus='Y'", myconn)
            Dim adp As MySqlDataAdapter = New MySqlDataAdapter(mycmd)
            adp.Fill(datatable)
            Return datatable
        End Function

        Public Shared Function CountInactiveEMDList_ByDept(ByVal myconn As MySqlConnection) As DataTable
            Dim datatable As DataTable = New DataTable
            Dim mycmd As MySqlCommand = New MySqlCommand("SELECT a.fldID, a.fldName As fldDepartment, IFNULL(d.fldCount,0) as fldCount FROM tbldepartment a 
                                                            LEFT JOIN (SELECT c.fldDeptID, COUNT(*) AS fldCount 
                                                                        FROM tblemddevice b 
                                                                        JOIN tblopp c ON c.fldid=b.fldoppid 
                                                                        WHERE b.fldstatus='N' Or c.fldstatus='N' GROUP BY c.fldDeptID) d ON d.fldDeptID=a.fldID
                                                            WHERE a.fldStatus='Y'", myconn)
            Dim adp As MySqlDataAdapter = New MySqlDataAdapter(mycmd)
            adp.Fill(datatable)
            Return datatable
        End Function

        Public Shared Function CountEMDStatus(ByVal status As String, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select Count(*) from tblemddevice where fldstatus=@status", myconn)
            mycmd.Parameters.AddWithValue("@status", status)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function CountActiveEMD(ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select Count(*) from tblemddevice a Left Join tblopp b on b.fldid=a.fldoppid Where a.fldStatus='Y' And b.fldStatus='Y'", myconn)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function CountInactiveEMD(ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select Count(*) from tblemddevice a Left Join tblopp b on b.fldid=a.fldoppid Where a.fldStatus='N' Or b.fldStatus='N'", myconn)
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

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal imei As String, ByVal name As String, ByVal serialno As String, ByVal size As String, ByVal simno1 As String, ByVal simno2 As String, ByVal devicestatus As String, ByVal oppstatus As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " a.fldID = @deviceid And "
            If Not oppid <= 0 Then query &= " b.fldID = @oppid And "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
            If Not String.IsNullOrEmpty(imei) Then query &= " a.fldimei = @imei And "
            If Not String.IsNullOrEmpty(name) Then query &= " a.fldName = @name And "
            If Not String.IsNullOrEmpty(serialno) Then query &= " a.fldSN = @serialno And "
            If Not String.IsNullOrEmpty(size) Then query &= " a.fldSize = @size And "
            If Not String.IsNullOrEmpty(simno1) Then query &= " a.fldsimno1 = @simno1 AND "
            If Not String.IsNullOrEmpty(simno2) Then query &= " a.fldsimno2 = @simno2 AND "
            If Not String.IsNullOrEmpty(devicestatus) Then query &= " a.fldStatus = @devicestatus AND "
            If Not String.IsNullOrEmpty(oppstatus) Then query &= " b.fldStatus = @oppstatus AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, a.fldSpeed*1.852 as fldSpeedKmh, if(a.fldGSMSignal>0,a.fldGSMSignal/31*100,0) As fldGSMSignalPercent, if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus, 
                                                                ifnull(f.fldgeofence,'') As fldGeofenceMk, ifnull(b.fldGeofence1,'') As fldGeofence1, ifnull(b.fldName,'') As fldOPPName, ifnull(b.fldICNo,'') As fldOPPICNo, ifnull(b.fldContactNo,'') As fldOPPContactNo, 
                                                                ifnull(c.fldColor,'grey') as fldDeptColor, ifnull(c.fldName,'') As fldDepartment, ifnull(d.fldName,'') As fldPSName, ifnull(d.fldContactNo,'') As fldPSContactNo, 
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
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@name", name)
            myCommand.Parameters.AddWithValue("@serialno", serialno)
            myCommand.Parameters.AddWithValue("@size", size)
            myCommand.Parameters.AddWithValue("@simno1", simno1)
            myCommand.Parameters.AddWithValue("@simno2", simno2)
            myCommand.Parameters.AddWithValue("@devicestatus", devicestatus)
            myCommand.Parameters.AddWithValue("@oppstatus", oppstatus)
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
            Dim myCommand As MySqlCommand = New MySqlCommand("Select *, fldSpeed*1.852 as fldSpeedKmh, if(fldGSMSignal>0,fldGSMSignal/31*100,0) As fldGSMSignalPercent, if(flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, if(flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus From tblemddevice " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@imei", imei)
            myCommand.Parameters.AddWithValue("@simno", simno)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetActiveDeviceList(ByVal deptid As Long, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = " a.fldStatus = 'Y' AND  b.fldStatus = 'Y' AND "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, a.fldSpeed*1.852 as fldSpeedKmh, if(a.fldGSMSignal>0,a.fldGSMSignal/31*100,0) As fldGSMSignalPercent, if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus, 
                                                                ifnull(b.fldName,'') As fldOPPName, ifnull(b.fldICNo,'') As fldOPPICNo, ifnull(b.fldContactNo,'') As fldOPPContactNo, 
                                                                ifnull(c.fldColor,'grey') as fldDeptColor, ifnull(c.fldName,'') As fldDepartment
                                                                From tblemddevice a 
                                                                Left Join tblopp b ON b.fldID=a.fldOppID 
                                                                Left Join tbldepartment c On c.fldID=b.fldDeptID" & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetInactiveDeviceList(ByVal deptid As Long, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = " a.fldStatus ='N' Or  b.fldStatus = 'N' AND "
            If Not deptid <= 0 Then query &= " b.fldDeptID = @deptid And "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, a.fldSpeed*1.852 as fldSpeedKmh, if(a.fldGSMSignal>0,a.fldGSMSignal/31*100,0) As fldGSMSignalPercent, if(a.flddevicestatus IN ('10', '11'),'Yes','No') AS fldChargingStatus, if(a.flddevicestatus IN ('01', '11'),'BeltOn','BeltOff') AS fldBeltStatus, 
                                                                ifnull(b.fldName,'') As fldOPPName, ifnull(b.fldICNo,'') As fldOPPICNo, ifnull(b.fldContactNo,'') As fldOPPContactNo, 
                                                                ifnull(c.fldColor,'grey') as fldDeptColor, ifnull(c.fldName,'') As fldDepartment
                                                                From tblemddevice a 
                                                                Left Join tblopp b ON b.fldID=a.fldOppID 
                                                                Left Join tbldepartment c On c.fldID=b.fldDeptID" & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@deptid", deptid)
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
                processExe = "Insert into tblemddevice (fldDateTime, fldName, fldImei, fldSN, fldSize, fldSimNo, fldSimNo2, fldStatus, fldWifiData, fldPayloadData, fldGeofenceJson) Values (Now(), @fldName, @fldImei, @fldSN, @fldSize, @fldSimNo, @fldSimNo2, @fldStatus, '', '' ,'')"
                isInsert = True
            Else
                processExe = "Update tblemddevice set fldSN=@fldSN, fldSize=@fldSize, fldName=@fldName, fldSimNo = @fldSimNo, fldSimNo2 = @fldSimNo2 Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", device.fldID)
            myCommand.Parameters.AddWithValue("@fldImei", device.fldImei)
            myCommand.Parameters.AddWithValue("@fldName", device.fldName)
            myCommand.Parameters.AddWithValue("@fldSN", device.fldSN)
            myCommand.Parameters.AddWithValue("@fldSize", device.fldSize)
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
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSN"))) Then
                device.fldSN = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSN"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSize"))) Then
                device.fldSize = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSize"))
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
