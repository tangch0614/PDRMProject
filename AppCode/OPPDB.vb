Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class OPPDB

#Region "Public Methods"

        Public Shared Function GetOPPID(ByVal deviceid As Long, ByVal name As String, ByVal icno As String, ByVal myconn As MySqlConnection) As Long
            Dim query As String = ""
            If Not deviceid <= 0 Then query &= " fldEMDDeviceID = @deviceid And "
            If Not String.IsNullOrEmpty(name) Then query &= " fldname = @name And "
            If Not String.IsNullOrEmpty(icno) Then query &= " fldicno = @icno AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldID,0) from tblopp " & query, myconn)
            mycmd.Parameters.AddWithValue("@deviceid", deviceid)
            mycmd.Parameters.AddWithValue("@name", name)
            mycmd.Parameters.AddWithValue("@icno", icno)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetGeofenceStatus(ByVal id As Long, ByVal myconn As MySqlConnection) As Integer
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldGeoFenceActive,0) from tblopp where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As Integer = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetEMDDeviceID(ByVal id As Long, ByVal myconn As MySqlConnection) As Long
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldEMDDeviceID,0) from tblopp where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As Long = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetOPP(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As OPPObj
            Dim myopp As OPPObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblopp Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myopp = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myopp
        End Function

        Public Shared Function GetOPPList(ByVal id As Long, ByVal name As String, ByVal icno As String, ByVal deviceid As Long, ByVal deptid As Long, ByVal policestationid As Long, ByVal orderrefno As String, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not id <= 0 Then query &= " a.fldID = @id And "
            If Not deviceid < 0 Then query &= " a.fldEMDDeviceID = @deviceid And "
            If Not String.IsNullOrEmpty(name) Then query &= " a.fldname = @name And "
            If Not String.IsNullOrEmpty(icno) Then query &= " a.fldicno = @icno AND "
            If Not deptid < 0 Then query &= " a.flddeptid = @deptid AND "
            If Not policestationid < 0 Then query &= " a.fldpolicestationid = @policestationid AND "
            If Not String.IsNullOrEmpty(orderrefno) Then query &= " a.fldordrefno = @orderrefno AND "
            If Not String.IsNullOrEmpty(status) Then query &= " a.fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, ifnull(b.fldName,'') As fldPSName, ifnull(b.fldContactNo,'') As fldPSContactNo, ifnull(c.fldName,'') as fldActs, ifnull(d.fldName,'') as fldActsSection, ifnull(e.fldName,'') as fldEMDName, ifnull(e.fldImei,'') As fldImei,
                                                                ifnull(f.fldName,'') as fldOverseerName, ifnull(f.fldICNo,'') as fldOverseerICNo, ifnull(f.fldContactNo,'') as fldOverseerContactNo, ifnull(f.fldPoliceNo,'') as fldPoliceNo, ifnull(g.fldName,'') as fldDepartment
                                                                From tblopp a 
                                                                left Join tblpolicestation b On b.fldID=a.fldPoliceStationID 
                                                                left Join tblacts c On c.fldID=a.fldActsID 
                                                                left Join tblActsSection d On d.fldID=a.fldActsSectionID 
                                                                left Join tblemddevice e On e.fldID=a.fldEMDDeviceID
                                                                left Join tbladmin f On f.fldID=a.fldOverseerID  
                                                                LEFT JOIN tbldepartment g ON g.fldID=a.fldDeptID " & query & " Order by a.fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            myCommand.Parameters.AddWithValue("@name", name)
            myCommand.Parameters.AddWithValue("@icno", icno)
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            myCommand.Parameters.AddWithValue("@policestationid", policestationid)
            myCommand.Parameters.AddWithValue("@orderrefno", orderrefno)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetOPPList(ByVal id As Long, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not id <= 0 Then query &= " fldID = @id And "
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblopp " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function UpdateStatus(ByVal oppid As Long, ByVal oldstatus As String, ByVal newstatus As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldStatus=@newstatus Where fldID=@oppid and fldStatus=@oldstatus", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@oldstatus", oldstatus)
            myCommand.Parameters.AddWithValue("@newstatus", newstatus)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateOverseerID(ByVal oppid As Long, ByVal overseerid As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldOverseerID=@overseerid Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@overseerid", overseerid)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateEMDDeviceID(ByVal oppid As Long, ByVal deviceid As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldEMDDeviceID=@deviceid Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@deviceid", deviceid)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateEMDInstallDate(ByVal oppid As Long, ByVal installdate As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldEMDInstallDate=@installdate Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@installdate", installdate)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateEMDAccessories(ByVal oppid As Long, ByVal BeaconCode As String, ByVal SmartTag As Integer, ByVal OBC As Integer, ByVal Beacon As Integer, ByVal Charger As Integer, ByVal Strap As Integer, ByVal Cable As Integer, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldBeaconCode=@BeaconCode, fldSmartTag=@SmartTag, fldOBC=@OBC, fldBeacon=@Beacon, fldCharger=@Charger, fldStrap=@Strap, fldCable=@Cable Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@BeaconCode", BeaconCode)
            myCommand.Parameters.AddWithValue("@SmartTag", SmartTag)
            myCommand.Parameters.AddWithValue("@OBC", OBC)
            myCommand.Parameters.AddWithValue("@Beacon", Beacon)
            myCommand.Parameters.AddWithValue("@Charger", Charger)
            myCommand.Parameters.AddWithValue("@Strap", Strap)
            myCommand.Parameters.AddWithValue("@Cable", Cable)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateRestrictionTime(ByVal oppid As Long, ByVal frtime As String, ByVal totime As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldRestrictFrTime=@status, fldRestrictToTime=@totime  Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@frtime", frtime)
            myCommand.Parameters.AddWithValue("@totime", totime)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateGeofenceStatus(ByVal oppid As Long, ByVal status As Integer, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldGeofenceActive=@status Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateGeofence1(ByVal oppid As Long, ByVal geofence As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldGeofence1=@geofence Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@geofence", geofence)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateGeofence2(ByVal oppid As Long, ByVal geofence As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldGeofence2=@geofence Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@geofence", geofence)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateGeofenceMukim(ByVal oppid As Long, ByVal mukim As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblopp set fldGeofenceMukim=@mukim Where fldID=@oppid", myConnection)
            myCommand.Parameters.AddWithValue("@oppid", oppid)
            myCommand.Parameters.AddWithValue("@mukim", mukim)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function SaveOPPStatusHist(oppid As Long, oldstatus As String, newstatus As String, remark As String, creatorid As Long, myConnection As MySqlConnection) As Boolean
            Dim mycommand As MySqlCommand = New MySqlCommand("Insert into tbloppstatushist (fldDateTime,fldOPPID,fldCurrentStatus,fldStatus,fldRemark,fldCreatorID) Values (Now(),@oppid,@oldstatus,@newstatus,@remark,@creatorid)", myConnection)
            mycommand.Parameters.AddWithValue("@oppid", oppid)
            mycommand.Parameters.AddWithValue("@oldstatus", oldstatus)
            mycommand.Parameters.AddWithValue("@newstatus", newstatus)
            mycommand.Parameters.AddWithValue("@remark", remark)
            mycommand.Parameters.AddWithValue("@creatorid", creatorid)
            Dim result As Boolean = mycommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function SaveOPPEMDeviceHist(oppid As Long, deviceid As Long, status As String, remark As String, creatorid As Long, myConnection As MySqlConnection) As Boolean
            Dim mycommand As MySqlCommand = New MySqlCommand("Insert into tbloppemdhist (fldDateTime,fldOPPID,fldEMDDeviceID,fldStatus,fldRemark,fldCreatorID) Values (Now(),@oppid,@deviceid,@status,@remark,@creatorid)", myConnection)
            mycommand.Parameters.AddWithValue("@oppid", oppid)
            mycommand.Parameters.AddWithValue("@deviceid", deviceid)
            mycommand.Parameters.AddWithValue("@status", status)
            mycommand.Parameters.AddWithValue("@remark", remark)
            mycommand.Parameters.AddWithValue("@creatorid", creatorid)
            Dim result As Boolean = mycommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function Save(ByVal myopp As OPPObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myopp.fldID = Nothing Then
                processExe = "Insert into tblopp (fldDateTime, fldRefID, fldName, fldICNo, fldContactNo, fldPhoto1, fldPhoto2, fldAddress, fldCountryID, fldState, fldDistrict, fldMukim, fldPoliceStationID, fldDeptID, fldOffenceDesc, fldCaseFileNo, fldCaseHandlerName, fldCaseHandlerTelNo, fldActsID, fldActsSectionID, fldOrdParty, fldOrdPartyName, fldOrdIssuedDate, fldOrdRefNo, fldOrdDay, fldOrdMonth, fldOrdYear, fldOrdFrDate, fldOrdToDate, fldRptPoliceStationID, fldSDNo, fldOCSName, fldOCSTelNo, fldRptDay, fldRptFrTime, fldRptToTime, fldOverseerID, fldEMDDeviceID, fldEMDInstallDate, fldBeaconCode, fldSmartTag, fldOBC, fldBeacon, fldCharger, fldStrap, fldCable, fldRestrictFrTime, fldRestrictToTime, fldGeofence1, fldGeofence1FrTime, fldGeofence1ToTime, fldGeofence2, fldGeofence2FrTime, fldGeofence2ToTime, fldGeofenceMukim, fldAttachment1, fldAttachment2, fldRemark, fldStatus) Values (Now(), @fldRefID, @fldName, @fldICNo, @fldContactNo, @fldPhoto1, @fldPhoto2, @fldAddress, @fldCountryID, @fldState, @fldDistrict, @fldMukim, @fldPoliceStationID, @fldDeptID, @fldOffenceDesc, @fldCaseFileNo, @fldCaseHandlerName, @fldCaseHandlerTelNo, @fldActsID, @fldActsSectionID, @fldOrdParty, @fldOrdPartyName, @fldOrdIssuedDate, @fldOrdRefNo, @fldOrdDay, @fldOrdMonth, @fldOrdYear, @fldOrdFrDate, @fldOrdToDate, @fldRptPoliceStationID, @fldSDNo, @fldOCSName, @fldOCSTelNo, @fldRptDay, @fldRptFrTime, @fldRptToTime, @fldOverseerID, @fldEMDDeviceID, @fldEMDInstallDate, @fldBeaconCode, @fldSmartTag, @fldOBC, @fldBeacon, @fldCharger, @fldStrap, @fldCable, @fldRestrictFrTime, @fldRestrictToTime, @fldGeofence1, @fldGeofence1FrTime, @fldGeofence1ToTime, @fldGeofence2, @fldGeofence2FrTime, @fldGeofence2ToTime, @fldGeofenceMukim, @fldAttachment1, @fldAttachment2, @fldRemark, @fldStatus)"
                isInsert = True
            Else
                'processExe = "Update tblopp set fldName = @fldName, fldICNo = @fldICNo, fldContactNo=@fldContactNo, fldPhoto1 = @fldPhoto1, fldPhoto2 = @fldPhoto2, fldAddress = @fldAddress, fldCountryID = @fldCountryID, fldState = @fldState, fldDistrict = @fldDistrict, fldMukim = @fldMukim, fldPoliceStationID = @fldPoliceStationID, fldDeptID = @fldDeptID, fldOffenceDesc = @fldOffenceDesc, fldCaseFileNo = @fldCaseFileNo, fldCaseHandlerName = @fldCaseHandlerName, fldCaseHandlerTelNo = @fldCaseHandlerTelNo, fldActsID = @fldActsID, fldActsSectionID = @fldActsSectionID, fldOrdParty = @fldOrdParty, fldOrdPartyName = @fldOrdPartyName, fldOrdIssuedDate=@fldOrdIssuedDate, fldOrdRefNo=@fldOrdRefNo, fldOrdDay=@fldOrdDay, fldOrdMonth=@fldOrdMonth, fldOrdYear = @fldOrdYear, fldOrdFrDate = @fldOrdFrDate, fldOrdToDate = @fldOrdToDate, fldRptPoliceStationID = @fldRptPoliceStationID, fldSDNo = @fldSDNo, fldOCSName = @fldOCSName, fldOCSTelNo = @fldOCSTelNo, fldRptDay = @fldRptDay, fldRptFrTime = @fldRptFrTime, fldRptToTime = @fldRptToTime, fldOverseerID = @fldOverseerID, fldEMDDeviceID = @fldEMDDeviceID, fldEMDInstallDate=@fldEMDInstallDate, fldBeaconCode=@fldBeaconCode, fldSmartTag=@fldSmartTag, fldOBC=@fldOBC, fldBeacon=@fldBeacon, fldCharger=@fldCharger, fldStrap=@fldStrap, fldCable=@fldCable, fldRestrictFrTime=@fldRestrictFrTime, fldRestrictToTime=@fldRestrictToTime, fldGeofenceMukim = @fldGeofenceMukim, fldAttachment1 = @fldAttachment1, fldAttachment2 = @fldAttachment2, fldRemark = @fldRemark Where fldID = @fldID"
                processExe = "Update tblopp set fldName = @fldName, fldICNo = @fldICNo, fldContactNo=@fldContactNo, fldPhoto1 = @fldPhoto1, fldPhoto2 = @fldPhoto2, fldAddress = @fldAddress, fldCountryID = @fldCountryID, fldState = @fldState, fldDistrict = @fldDistrict, fldMukim = @fldMukim, fldPoliceStationID = @fldPoliceStationID, fldDeptID = @fldDeptID, fldOffenceDesc = @fldOffenceDesc, fldCaseFileNo = @fldCaseFileNo, fldCaseHandlerName = @fldCaseHandlerName, fldCaseHandlerTelNo = @fldCaseHandlerTelNo, fldActsID = @fldActsID, fldActsSectionID = @fldActsSectionID, fldOrdParty = @fldOrdParty, fldOrdPartyName = @fldOrdPartyName, fldOrdIssuedDate=@fldOrdIssuedDate, fldOrdRefNo=@fldOrdRefNo, fldOrdDay=@fldOrdDay, fldOrdMonth=@fldOrdMonth, fldOrdYear = @fldOrdYear, fldOrdFrDate = @fldOrdFrDate, fldOrdToDate = @fldOrdToDate, fldRptPoliceStationID = @fldRptPoliceStationID, fldSDNo = @fldSDNo, fldOCSName = @fldOCSName, fldOCSTelNo = @fldOCSTelNo, fldRptDay = @fldRptDay, fldRptFrTime = @fldRptFrTime, fldRptToTime = @fldRptToTime, fldRestrictFrTime=@fldRestrictFrTime, fldRestrictToTime=@fldRestrictToTime, fldAttachment1 = @fldAttachment1, fldAttachment2 = @fldAttachment2, fldRemark = @fldRemark Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", myopp.fldID)

            myCommand.Parameters.AddWithValue("@fldRefID", myopp.fldRefID)
            myCommand.Parameters.AddWithValue("@fldName", myopp.fldName)
            myCommand.Parameters.AddWithValue("@fldICNo", myopp.fldICNo)
            myCommand.Parameters.AddWithValue("@fldContactNo", myopp.fldContactNo)
            myCommand.Parameters.AddWithValue("@fldPhoto1", myopp.fldPhoto1)
            myCommand.Parameters.AddWithValue("@fldPhoto2", myopp.fldPhoto2)
            myCommand.Parameters.AddWithValue("@fldAddress", myopp.fldAddress)
            myCommand.Parameters.AddWithValue("@fldCountryID", myopp.fldCountryID)
            myCommand.Parameters.AddWithValue("@fldState", myopp.fldState)
            myCommand.Parameters.AddWithValue("@fldDistrict", myopp.fldDistrict)
            myCommand.Parameters.AddWithValue("@fldMukim", myopp.fldMukim)
            myCommand.Parameters.AddWithValue("@fldPoliceStationID", myopp.fldPoliceStationID)
            myCommand.Parameters.AddWithValue("@fldDeptID", myopp.fldDeptID)
            myCommand.Parameters.AddWithValue("@fldOffenceDesc", myopp.fldOffenceDesc)
            myCommand.Parameters.AddWithValue("@fldCaseFileNo", myopp.fldCaseFileNo)
            myCommand.Parameters.AddWithValue("@fldCaseHandlerName", myopp.fldCaseHandlerName)
            myCommand.Parameters.AddWithValue("@fldCaseHandlerTelNo", myopp.fldCaseHandlerTelNo)
            myCommand.Parameters.AddWithValue("@fldActsID", myopp.fldActsID)
            myCommand.Parameters.AddWithValue("@fldActsSectionID", myopp.fldActsSectionID)
            myCommand.Parameters.AddWithValue("@fldOrdParty", myopp.fldOrdParty)
            myCommand.Parameters.AddWithValue("@fldOrdIssuedDate", myopp.fldOrdIssuedDate)
            myCommand.Parameters.AddWithValue("@fldOrdPartyName", myopp.fldOrdPartyName)
            myCommand.Parameters.AddWithValue("@fldOrdRefNo", myopp.fldOrdRefNo)
            myCommand.Parameters.AddWithValue("@fldOrdDay", myopp.fldOrdDay)
            myCommand.Parameters.AddWithValue("@fldOrdMonth", myopp.fldOrdMonth)
            myCommand.Parameters.AddWithValue("@fldOrdYear", myopp.fldOrdYear)
            myCommand.Parameters.AddWithValue("@fldOrdFrDate", myopp.fldOrdFrDate)
            myCommand.Parameters.AddWithValue("@fldOrdToDate", myopp.fldOrdToDate)
            myCommand.Parameters.AddWithValue("@fldRptPoliceStationID", myopp.fldRptPoliceStationID)
            myCommand.Parameters.AddWithValue("@fldSDNo", myopp.fldSDNo)
            myCommand.Parameters.AddWithValue("@fldOCSName", myopp.fldOCSName)
            myCommand.Parameters.AddWithValue("@fldOCSTelNo", myopp.fldOCSTelNo)
            myCommand.Parameters.AddWithValue("@fldRptDay", myopp.fldRptDay)
            myCommand.Parameters.AddWithValue("@fldRptFrTime", myopp.fldRptFrTime)
            myCommand.Parameters.AddWithValue("@fldRptToTime", myopp.fldRptToTime)
            myCommand.Parameters.AddWithValue("@fldOverseerID", myopp.fldOverseerID)
            myCommand.Parameters.AddWithValue("@fldEMDDeviceID", myopp.fldEMDDeviceID)
            myCommand.Parameters.AddWithValue("@fldEMDInstallDate", myopp.fldEMDInstallDate)
            myCommand.Parameters.AddWithValue("@fldBeaconCode", myopp.fldBeaconCode)
            myCommand.Parameters.AddWithValue("@fldSmartTag", myopp.fldSmartTag)
            myCommand.Parameters.AddWithValue("@fldOBC", myopp.fldOBC)
            myCommand.Parameters.AddWithValue("@fldBeacon", myopp.fldBeacon)
            myCommand.Parameters.AddWithValue("@fldCharger", myopp.fldCharger)
            myCommand.Parameters.AddWithValue("@fldStrap", myopp.fldStrap)
            myCommand.Parameters.AddWithValue("@fldCable", myopp.fldCable)
            myCommand.Parameters.AddWithValue("@fldRestrictFrTime", myopp.fldRestrictFrTime)
            myCommand.Parameters.AddWithValue("@fldRestrictToTime", myopp.fldRestrictToTime)
            myCommand.Parameters.AddWithValue("@fldGeofence1", myopp.fldGeofence1)
            myCommand.Parameters.AddWithValue("@fldGeofence1FrTime", myopp.fldGeofence1FrTime)
            myCommand.Parameters.AddWithValue("@fldGeofence1ToTime", myopp.fldGeofence1ToTime)
            myCommand.Parameters.AddWithValue("@fldGeofence2", myopp.fldGeofence2)
            myCommand.Parameters.AddWithValue("@fldGeofence2FrTime", myopp.fldGeofence2FrTime)
            myCommand.Parameters.AddWithValue("@fldGeofence2ToTime", myopp.fldGeofence2ToTime)
            myCommand.Parameters.AddWithValue("@fldGeofenceMukim", myopp.fldGeofenceMukim)
            myCommand.Parameters.AddWithValue("@fldAttachment1", myopp.fldAttachment1)
            myCommand.Parameters.AddWithValue("@fldAttachment2", myopp.fldAttachment2)
            myCommand.Parameters.AddWithValue("@fldRemark", myopp.fldRemark)
            myCommand.Parameters.AddWithValue("@fldStatus", myopp.fldStatus)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblopp Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As OPPObj
            Dim myopp As OPPObj = New OPPObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                myopp.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRefID"))) Then
                myopp.fldRefID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRefID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldName"))) Then
                myopp.fldName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldICNo"))) Then
                myopp.fldICNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldICNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldContactNo"))) Then
                myopp.fldContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldContactNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPhoto1"))) Then
                myopp.fldPhoto1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPhoto1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPhoto2"))) Then
                myopp.fldPhoto2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPhoto2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAddress"))) Then
                myopp.fldAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAddress"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCountryID"))) Then
                myopp.fldCountryID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCountryID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldState"))) Then
                myopp.fldState = myDataRecord.GetString(myDataRecord.GetOrdinal("fldState"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDistrict"))) Then
                myopp.fldDistrict = myDataRecord.GetString(myDataRecord.GetOrdinal("fldDistrict"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMukim"))) Then
                myopp.fldMukim = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMukim"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPoliceStationID"))) Then
                myopp.fldPoliceStationID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldPoliceStationID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDeptID"))) Then
                myopp.fldDeptID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldDeptID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOffenceDesc"))) Then
                myopp.fldOffenceDesc = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOffenceDesc"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCaseFileNo"))) Then
                myopp.fldCaseFileNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCaseFileNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCaseHandlerName"))) Then
                myopp.fldCaseHandlerName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCaseHandlerName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCaseHandlerTelNo"))) Then
                myopp.fldCaseHandlerTelNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCaseHandlerTelNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldActsID"))) Then
                myopp.fldActsID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldActsID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldActsSectionID"))) Then
                myopp.fldActsSectionID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldActsSectionID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdParty"))) Then
                myopp.fldOrdParty = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOrdParty"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdPartyName"))) Then
                myopp.fldOrdPartyName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOrdPartyName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdIssuedDate"))) Then
                myopp.fldOrdIssuedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldOrdIssuedDate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdRefNo"))) Then
                myopp.fldOrdRefNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOrdRefNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdDay"))) Then
                myopp.fldOrdDay = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldOrdDay"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdMonth"))) Then
                myopp.fldOrdMonth = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldOrdMonth"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdYear"))) Then
                myopp.fldOrdYear = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldOrdYear"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdFrDate"))) Then
                myopp.fldOrdFrDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldOrdFrDate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOrdToDate"))) Then
                myopp.fldOrdToDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldOrdToDate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRptPoliceStationID"))) Then
                myopp.fldRptPoliceStationID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldRptPoliceStationID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSDNo"))) Then
                myopp.fldSDNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSDNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOCSName"))) Then
                myopp.fldOCSName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOCSName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOCSTelNo"))) Then
                myopp.fldOCSTelNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOCSTelNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRptDay"))) Then
                myopp.fldRptDay = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRptDay"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRptFrTime"))) Then
                myopp.fldRptFrTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRptFrTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRptToTime"))) Then
                myopp.fldRptToTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRptToTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOverseerID"))) Then
                myopp.fldOverseerID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldOverseerID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldEMDDeviceID"))) Then
                myopp.fldEMDDeviceID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldEMDDeviceID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldEMDInstallDate"))) Then
                myopp.fldEMDInstallDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldEMDInstallDate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBeaconCode"))) Then
                myopp.fldBeaconCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBeaconCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSmartTag"))) Then
                myopp.fldSmartTag = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldSmartTag"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOBC"))) Then
                myopp.fldOBC = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldOBC"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBeacon"))) Then
                myopp.fldBeacon = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldBeacon"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCharger"))) Then
                myopp.fldCharger = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCharger"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStrap"))) Then
                myopp.fldStrap = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldStrap"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCable"))) Then
                myopp.fldCable = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCable"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRestrictFrTime"))) Then
                myopp.fldRestrictFrTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRestrictFrTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRestrictToTime"))) Then
                myopp.fldRestrictToTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRestrictToTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence1"))) Then
                myopp.fldGeofence1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence1FrTime"))) Then
                myopp.fldGeofence1FrTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence1FrTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence1ToTime"))) Then
                myopp.fldGeofence1ToTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence1ToTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence2"))) Then
                myopp.fldGeofence2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence2FrTime"))) Then
                myopp.fldGeofence2FrTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence2FrTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofence2ToTime"))) Then
                myopp.fldGeofence2ToTime = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofence2ToTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGeofenceMukim"))) Then
                myopp.fldGeofenceMukim = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGeofenceMukim"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAttachment1"))) Then
                myopp.fldAttachment1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAttachment1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAttachment2"))) Then
                myopp.fldAttachment2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAttachment2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRemark"))) Then
                myopp.fldRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRemark"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myopp.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            Return myopp
        End Function
    End Class

End NameSpace 
