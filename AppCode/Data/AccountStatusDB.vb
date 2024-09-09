Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class AccountStatusHistDB

#Region "Public Methods"

        Public Shared Function GetAccountStatusHistory(ByVal MemberID As Long, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal CreatorID As Long, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = Nothing
            If Not MemberID <= 0 Then query &= " h.fldMID = @fldMID And "
            If Not CreatorID <= -1 Then query &= " h.fldCreatorID = @fldCreatorID And "
            If Not String.IsNullOrEmpty(Status) Then query &= " h.fldStatus = @fldStatus AND "
            If Not String.IsNullOrEmpty(DateFrom) Then query &= " date(h.fldDateTime) >= @DateFrom AND "
            If Not String.IsNullOrEmpty(DateTo) Then query &= " date(h.fldDateTime) <= @DateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select h.*, m.fldCode, m.fldName, m.fldICNo, a.fldCode as fldProcessBy From tblaccountstatushist h Join tblmembership m On m.fldID = h.fldMID Join tbladmin a On a.fldID = h.fldCreatorID " & query & " Order By fldDateTime Desc, fldID Desc", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMID", MemberID)
            myCommand.Parameters.AddWithValue("@fldCreatorID", CreatorID)
            myCommand.Parameters.AddWithValue("@fldStatus", Status)
            myCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
            myCommand.Parameters.AddWithValue("@DateTo", DateTo)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetAccountStatusHist(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As AccountStatusHistObj
            Dim accountStatusHist As AccountStatusHistObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblaccountstatushist Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    accountStatusHist = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return accountStatusHist
        End Function

        Public Shared Function GetAccountStatusHistList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblaccountstatushist", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal accountStatusHist As AccountStatusHistObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If accountStatusHist.fldID = Nothing Then
                processExe = "Insert into tblaccountstatushist (fldDateTime, fldMID, fldCurrentStatus, fldStatus, fldRemark, fldCreatorID, fldCreatorType) Values (@fldDateTime, @fldMID, @fldCurrentStatus, @fldStatus, @fldRemark, @fldCreatorID, @fldCreatorType)"
                isInsert = True
            Else
                processExe = "Update tblaccountstatushist set fldDateTime = @fldDateTime, fldMID = @fldMID, fldCurrentStatus = @fldCurrentStatus, fldStatus = @fldStatus, fldRemark = @fldRemark, fldCreatorID = @fldCreatorID, fldCreatorType = @fldCreatorType Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", accountStatusHist.fldID)

            myCommand.Parameters.AddWithValue("@fldDateTime", accountStatusHist.fldDateTime)
            myCommand.Parameters.AddWithValue("@fldMID", accountStatusHist.fldMID)
            myCommand.Parameters.AddWithValue("@fldCurrentStatus", accountStatusHist.fldCurrentStatus)
            myCommand.Parameters.AddWithValue("@fldStatus", accountStatusHist.fldStatus)
            myCommand.Parameters.AddWithValue("@fldRemark", accountStatusHist.fldRemark)
            myCommand.Parameters.AddWithValue("@fldCreatorID", accountStatusHist.fldCreatorID)
            myCommand.Parameters.AddWithValue("@fldCreatorType", accountStatusHist.fldCreatorType)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblaccountstatushist Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AccountStatusHistObj
            Dim accountStatusHist As AccountStatusHistObj = New AccountStatusHistObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                accountStatusHist.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                accountStatusHist.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMID"))) Then
                accountStatusHist.fldMID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldMID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCurrentStatus"))) Then
                accountStatusHist.fldCurrentStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCurrentStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                accountStatusHist.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRemark"))) Then
                accountStatusHist.fldRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRemark"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorID"))) Then
                accountStatusHist.fldCreatorID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldCreatorID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorType"))) Then
                accountStatusHist.fldCreatorType = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCreatorType"))
            End If
            Return accountStatusHist
        End Function
    End Class

 End NameSpace 
