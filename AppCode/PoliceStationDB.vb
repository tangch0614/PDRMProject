Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports System.Transactions

Namespace DataAccess
    Public Class PoliceStationDB

#Region "Public Methods"

        Public Shared Function GetPoliceStationType(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldType,0) from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetPoliceStationIPK(ByVal id As Long, ByVal myConnection As MySqlConnection) As Long
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldIPKID,0) from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetPoliceStationIPD(ByVal id As Long, ByVal myConnection As MySqlConnection) As Long
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldIPDID,0) from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetPoliceStationName(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldName,'') from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetPoliceStationContactNo(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldContactNo,'') from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetDepartmentName(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldName,'') from tbldepartment where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetState(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldState,'') from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetDistrict(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldDistrict,'') from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetMukim(ByVal id As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldMukim,'') from tblpolicestation where fldid=@id", myConnection)
            myCommand.Parameters.AddWithValue("@id", id)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetPoliceStationList(ByVal id As Long, ByVal ipkid As Long, ByVal ipdid As Long, ByVal type As String, ByVal state As String, ByVal district As String, ByVal mukim As String, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not id <= 0 Then query &= " a.fldID = @id And "
            If Not ipkid < 0 Then query &= " a.fldIPKID = @ipkid And "
            If Not ipdid < 0 Then query &= " a.fldIPDID = @ipdid And "
            If Not String.IsNullOrEmpty(type) Then query &= " a.fldType = @type And "
            If Not String.IsNullOrEmpty(state) Then query &= " a.fldState = @state And "
            If Not String.IsNullOrEmpty(district) Then query &= " a.fldDistrict = @district AND "
            If Not String.IsNullOrEmpty(mukim) Then query &= " a.fldMukim = @mukim AND "
            If Not String.IsNullOrEmpty(status) Then query &= " a.fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, ifnull(b.fldname,'') as fldIPKName, ifnull(b.fldcontactno,'') as fldIPKContactNo, ifnull(c.fldname,'') as fldIPDName, ifnull(c.fldcontactno,'') as fldIPDContactNo from tblpolicestation a left join tblpolicestation b on b.fldid=a.fldipkid and b.fldtype='IPK' left join tblpolicestation c on c.fldid=a.fldipdid and c.fldtype='IPD' " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@id", id)
            myCommand.Parameters.AddWithValue("@ipkid", ipkid)
            myCommand.Parameters.AddWithValue("@ipdid", ipdid)
            myCommand.Parameters.AddWithValue("@type", type)
            myCommand.Parameters.AddWithValue("@state", state)
            myCommand.Parameters.AddWithValue("@district", district)
            myCommand.Parameters.AddWithValue("@mukim", mukim)
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetDepartmentList(ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrEmpty(status) Then query &= " fldStatus = @status AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * from tbldepartment " & query & " Order by fldName ", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@status", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

#End Region

    End Class

End Namespace
