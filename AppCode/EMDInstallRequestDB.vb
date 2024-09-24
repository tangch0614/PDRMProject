Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class EMDInstallRequestDB

#Region "Public Methods"

        Public Shared Function GetInstallRequest(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As EMDInstallRequestObj
            Dim installrequest As EMDInstallRequestObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblemdinstallrequest Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    installrequest = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return installrequest
        End Function

        Public Shared Function GetInstallRequestList(id As Long, creatorid As Long, deptid As Long, ipkid As Long, policestationid As Long, status As String, frinsdate As String, toinsdate As String, frdate As String, todate As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not id <= 0 Then query &= " a.fldID = @id And "
            If Not creatorid <= 0 Then query &= " a.fldCreatorID = @creatorid And "
            If Not deptid < 0 Then query &= " a.fldDeptID = @deptid And "
            If Not ipkid < 0 Then query &= " a.fldIPKID = @ipkid And "
            If Not policestationid < 0 Then query &= " a.fldPSID = @policestationid And "
            If Not String.IsNullOrEmpty(frinsdate) Then query &= " a.fldInstallDateTime >= @frinsdate And "
            If Not String.IsNullOrEmpty(toinsdate) Then query &= " a.fldInstallDateTime < Date_add(@toinsdate,interval 1 day) And "
            If Not String.IsNullOrEmpty(frdate) Then query &= " a.fldDateTime >= @frdate AND "
            If Not String.IsNullOrEmpty(todate) Then query &= " a.fldDateTime < Date_add(@todate,interval 1 day) AND "
            If Not String.IsNullOrEmpty(status) Then query &= " a.fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT a.*, IFNULL(b.fldname,'') AS fldDepartment, IFNULL(c.fldname,'') AS fldIPKName, IFNULL(d.fldname,'') AS fldPSName, IFNULL(e.fldname,'') AS fldOffName, IFNULL(e.fldContactNo,'') AS fldOffContactNo FROM tblemdinstallrequest a
                                                                LEFT JOIN tbldepartment b ON b.fldid=a.fldDeptID
                                                                LEFT JOIN tblpolicestation c ON c.fldid=a.fldipkid
                                                                LEFT JOIN tblpolicestation d ON d.fldid=a.fldpsid
                                                                LEFT JOIN tbladmin e ON e.fldid=a.fldcreatorid " & query & " Order by fldInstallDateTime", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            myCommand.Parameters.AddWithValue("@ipkid", ipkid)
            myCommand.Parameters.AddWithValue("@policestationid", policestationid)
            myCommand.Parameters.AddWithValue("@frinsdate", frinsdate)
            myCommand.Parameters.AddWithValue("@toinsdate", toinsdate)
            myCommand.Parameters.AddWithValue("@frdate", frdate)
            myCommand.Parameters.AddWithValue("@todate", todate)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function UpdateAttachment1(ByVal requestid As Long, ByVal attachment As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemdinstallrequest set fldAttachment1=@attachment Where fldID=@requestid", myConnection)
            myCommand.Parameters.AddWithValue("@requestid", requestid)
            myCommand.Parameters.AddWithValue("@attachment", attachment)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateAttachment2(ByVal requestid As Long, ByVal attachment As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemdinstallrequest set fldAttachment2=@attachment Where fldID=@requestid", myConnection)
            myCommand.Parameters.AddWithValue("@requestid", requestid)
            myCommand.Parameters.AddWithValue("@attachment", attachment)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateEMDAccessories(ByVal requestid As Long, ByVal OPPID As Long, ByVal EMDDeviceID As Long, ByVal SmartTagCode As String, ByVal OBCCode As String, ByVal BeaconCode As String, ByVal ChargerCode As String, ByVal StrapCode As String, ByVal CableCode As String, ByVal SmartTag As Integer, ByVal OBC As Integer, ByVal Beacon As Integer, ByVal Charger As Integer, ByVal Strap As Integer, ByVal Cable As Integer, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemdinstallrequest set fldOPPID=@OPPID, fldEMDDeviceID=@EMDDeviceID, fldSmartTagCode=@SmartTagCode, fldOBCCode=@OBCCode, fldBeaconCode=@BeaconCode, fldChargerCode=@ChargerCode, fldStrapCode=@StrapCode, fldCableCode=@CableCode, fldSmartTag=@SmartTag, fldOBC=@OBC, fldBeacon=@Beacon, fldCharger=@Charger, fldStrap=@Strap, fldCable=@Cable Where fldID=@requestid", myConnection)
            myCommand.Parameters.AddWithValue("@requestid", requestid)
            myCommand.Parameters.AddWithValue("@OPPID", OPPID)
            myCommand.Parameters.AddWithValue("@EMDDeviceID", EMDDeviceID)
            myCommand.Parameters.AddWithValue("@SmartTagCode", SmartTagCode)
            myCommand.Parameters.AddWithValue("@OBCCode", OBCCode)
            myCommand.Parameters.AddWithValue("@BeaconCode", BeaconCode)
            myCommand.Parameters.AddWithValue("@ChargerCode", ChargerCode)
            myCommand.Parameters.AddWithValue("@StrapCode", StrapCode)
            myCommand.Parameters.AddWithValue("@CableCode", CableCode)
            myCommand.Parameters.AddWithValue("@SmartTag", SmartTag)
            myCommand.Parameters.AddWithValue("@OBC", OBC)
            myCommand.Parameters.AddWithValue("@Beacon", Beacon)
            myCommand.Parameters.AddWithValue("@Charger", Charger)
            myCommand.Parameters.AddWithValue("@Strap", Strap)
            myCommand.Parameters.AddWithValue("@Cable", Cable)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function Save(ByVal installrequest As EMDInstallRequestObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If installrequest.fldID = Nothing Then
                processExe = "Insert into tblemdinstallrequest (fldDateTime, fldRefID, fldDeptID, fldInstallDateTime, fldState, fldDistrict, fldMukim, fldIPKID, fldIPDID, fldPSID, fldOCSName, fldOCSTelNo, fldAttachment1, fldAttachment2, fldRemark, fldCreatorID, fldStatus, fldProcessByID, fldProcessDateTime, fldProcessRemark) Values (@fldDateTime, @fldRefID, @fldDeptID, @fldInstallDateTime, @fldState, @fldDistrict, @fldMukim, @fldIPKID, @fldIPDID, @fldPSID, @fldOCSName, @fldOCSTelNo, @fldAttachment1, @fldAttachment2, @fldRemark, @fldCreatorID, @fldStatus, @fldProcessByID, @fldProcessDateTime, @fldProcessRemark)"
                isInsert = True
            Else
                processExe = "Update tblemdinstallrequest set fldDeptID = @fldDeptID, fldInstallDateTime = @fldInstallDateTime, fldState = @fldState, fldDistrict = @fldDistrict, fldMukim = @fldMukim, fldIPKID = @fldIPKID, fldIPDID = @fldIPDID, fldPSID = @fldPSID, fldOCSName = @fldOCSName, fldOCSTelNo = @fldOCSTelNo, fldAttachment1 = @fldAttachment1, fldAttachment2 = @fldAttachment2, fldRemark = @fldRemark Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", installrequest.fldID)

            myCommand.Parameters.AddWithValue("@fldDateTime", installrequest.fldDateTime)
            myCommand.Parameters.AddWithValue("@fldRefID", installrequest.fldRefID)
            myCommand.Parameters.AddWithValue("@fldDeptID", installrequest.fldDeptID)
            myCommand.Parameters.AddWithValue("@fldInstallDateTime", installrequest.fldInstallDateTime)
            myCommand.Parameters.AddWithValue("@fldState", installrequest.fldState)
            myCommand.Parameters.AddWithValue("@fldDistrict", installrequest.fldDistrict)
            myCommand.Parameters.AddWithValue("@fldMukim", installrequest.fldMukim)
            myCommand.Parameters.AddWithValue("@fldIPKID", installrequest.fldIPKID)
            myCommand.Parameters.AddWithValue("@fldIPDID", installrequest.fldIPDID)
            myCommand.Parameters.AddWithValue("@fldPSID", installrequest.fldPSID)
            myCommand.Parameters.AddWithValue("@fldOCSName", installrequest.fldOCSName)
            myCommand.Parameters.AddWithValue("@fldOCSTelNo", installrequest.fldOCSTelNo)
            myCommand.Parameters.AddWithValue("@fldAttachment1", installrequest.fldAttachment1)
            myCommand.Parameters.AddWithValue("@fldAttachment2", installrequest.fldAttachment2)
            myCommand.Parameters.AddWithValue("@fldRemark", installrequest.fldRemark)
            myCommand.Parameters.AddWithValue("@fldCreatorID", installrequest.fldCreatorID)
            myCommand.Parameters.AddWithValue("@fldStatus", installrequest.fldStatus)
            myCommand.Parameters.AddWithValue("@fldProcessByID", installrequest.fldProcessByID)
            myCommand.Parameters.AddWithValue("@fldProcessDateTime", installrequest.fldProcessDateTime)
            myCommand.Parameters.AddWithValue("@fldProcessRemark", installrequest.fldProcessRemark)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function UpdateStatus(ByVal id As Long, ByVal creatorid As Long, ByVal newstatus As String, ByVal remark As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblemdinstallrequest Set fldStatus=@newstatus, fldProcessByID=@creatorid, fldProcessDateTime=Now(), fldProcessRemark=@remark Where fldID = @id", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@newstatus", newstatus)
            myCommand.Parameters.AddWithValue("@creatorid", creatorid)
            myCommand.Parameters.AddWithValue("@remark", remark)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As EMDInstallRequestObj
            Dim installrequest As EMDInstallRequestObj = New EMDInstallRequestObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                installrequest.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRefID"))) Then
                installrequest.fldRefID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRefID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                installrequest.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDeptID"))) Then
                installrequest.fldDeptID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldDeptID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldInstallDateTime"))) Then
                installrequest.fldInstallDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldInstallDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldState"))) Then
                installrequest.fldState = myDataRecord.GetString(myDataRecord.GetOrdinal("fldState"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDistrict"))) Then
                installrequest.fldDistrict = myDataRecord.GetString(myDataRecord.GetOrdinal("fldDistrict"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMukim"))) Then
                installrequest.fldMukim = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMukim"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldIPKID"))) Then
                installrequest.fldIPKID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldIPKID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldIPDID"))) Then
                installrequest.fldIPDID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldIPDID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPSID"))) Then
                installrequest.fldPSID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldPSID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOCSName"))) Then
                installrequest.fldOCSName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOCSName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOCSTelNo"))) Then
                installrequest.fldOCSTelNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOCSTelNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldEMDDeviceID"))) Then
                installrequest.fldEMDDeviceID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldEMDDeviceID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOPPID"))) Then
                installrequest.fldOPPID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldOPPID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSmartTagCode"))) Then
                installrequest.fldSmartTagCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSmartTagCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOBCCode"))) Then
                installrequest.fldOBCCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOBCCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBeaconCode"))) Then
                installrequest.fldBeaconCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBeaconCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldChargerCode"))) Then
                installrequest.fldChargerCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldChargerCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStrapCode"))) Then
                installrequest.fldStrapCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStrapCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCableCode"))) Then
                installrequest.fldCableCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCableCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSmartTag"))) Then
                installrequest.fldSmartTag = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldSmartTag"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOBC"))) Then
                installrequest.fldOBC = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldOBC"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBeacon"))) Then
                installrequest.fldBeacon = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldBeacon"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCharger"))) Then
                installrequest.fldCharger = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCharger"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStrap"))) Then
                installrequest.fldStrap = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldStrap"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCable"))) Then
                installrequest.fldCable = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCable"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAttachment1"))) Then
                installrequest.fldAttachment1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAttachment1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAttachment2"))) Then
                installrequest.fldAttachment2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAttachment2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRemark"))) Then
                installrequest.fldRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRemark"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorID"))) Then
                installrequest.fldCreatorID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldCreatorID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                installrequest.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldProcessByID"))) Then
                installrequest.fldProcessByID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldProcessByID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldProcessDateTime"))) Then
                installrequest.fldProcessDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldProcessDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldProcessRemark"))) Then
                installrequest.fldProcessRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("fldProcessRemark"))
            End If
            Return installrequest
        End Function
    End Class

End NameSpace 
