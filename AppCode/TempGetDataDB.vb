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

    End Class

End Namespace
