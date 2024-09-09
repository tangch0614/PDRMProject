Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports System.Transactions

Namespace DataAccess
    Public Class ActsDB

#Region "Public Methods"

        Public Shared Function GetActsName(ByVal id As Long, ByVal myconn As MySqlConnection) As String
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldName,'') from tblacts where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As String = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetActsSectionName(ByVal id As Long, ByVal myconn As MySqlConnection) As String
            Dim mycmd As MySqlCommand = New MySqlCommand("Select ifnull(fldName,'') from tblactsSection where fldID=@id", myconn)
            mycmd.Parameters.AddWithValue("@id", id)
            Dim result As String = mycmd.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetActsList(ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select *, concat(fldName,' ','fldDesc') as fldNameDesc from tblacts " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetActsSectionList(ByVal actsid As Long, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not actsid <= 0 Then query &= " fldActsID = @actsid And "
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select *, concat(fldName,' ','fldDesc') as fldNameDesc from tblactssection " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@actsid", actsid)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

#End Region

    End Class

End Namespace
