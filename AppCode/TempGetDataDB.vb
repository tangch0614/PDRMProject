Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports System.Transactions

Namespace DataAccess
    Public Class TempGetDataDB

#Region "Public Methods"

        Public Shared Function GetOfficerList(ByVal id As Long, ByVal name As String, ByVal icno As String, ByVal policeno As String, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not id <= 0 Then query &= " a.fldID = @id And "
            If Not String.IsNullOrEmpty(name) Then query &= " fldName = @name And "
            If Not String.IsNullOrEmpty(icno) Then query &= " fldICNo = @icno AND "
            If Not String.IsNullOrEmpty(policeno) Then query &= " fldPoliceNo = @policeno AND "
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, ifnull(b.fldName,'') as fldPSName from tbladmin a Left Join tblpolicestation b ON b.fldID=a.fldPoliceStationID " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@name", name)
            myCommand.Parameters.AddWithValue("@icno", icno)
            myCommand.Parameters.AddWithValue("@policeno", policeno)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

#End Region

        Public Shared Function GetConnectionID(ByVal fldIMEI As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT fldConnectionID FROM tblpdrmdeviceconn WHERE fldIMEI = @fldIMEI order by fldDatetime desc limit 1;", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)

            result = myCommand.ExecuteScalar
            Return result
        End Function
        Public Shared Function GetIMEINo(ByVal fldDeviceID As Long, ByVal myConnection As MySqlConnection) As String
            Dim result As String = ""
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT fldIMEI FROM tblemddevice WHERE fldID = @fldID;", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldDeviceID)

            result = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function InsertGPRSMode(ByVal fldConnectionID As Long, ByVal fldIMEI As String, ByVal fldMsg As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim processExe As String = ""
            processExe = "insert into tblpdrmoutmsg (fldDatetime,fldConnectionID,fldIMEI,fldMsg) values(now(),@fldConnectionID,@fldIMEI, @fldMsg)"
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldConnectionID", fldConnectionID)
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)
            myCommand.Parameters.AddWithValue("@fldMsg", fldMsg)

            result = myCommand.ExecuteNonQuery()

            Return result
        End Function
        Public Shared Function InsertNotification(ByVal fldIMEI As String, ByVal oppid As Long, ByVal EMDDeviceID As Long, ByVal fldMsg As String, ByVal Level As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim processExe As String = ""
            processExe = "insert into tblpdrmnotification (fldDatetime,fldIMEI,fldoppid,fldEMDDeviceID,fldMsg,fldSeverity) values(now(),@fldIMEI,@fldoppid,@fldEMDDeviceID, @fldMsg, @fldSeverity)"
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldEMDDeviceID", EMDDeviceID)
            myCommand.Parameters.AddWithValue("@fldIMEI", fldIMEI)
            myCommand.Parameters.AddWithValue("@fldMsg", fldMsg)
            myCommand.Parameters.AddWithValue("@fldSeverity", Level)
            myCommand.Parameters.AddWithValue("@fldoppid", oppid)

            result = myCommand.ExecuteNonQuery()

            Return result
        End Function
    End Class

End Namespace
