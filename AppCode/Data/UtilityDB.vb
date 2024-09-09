Imports MySql.Data.MySqlClient
Imports System.Data
Imports AppCode.BusinessObject

Namespace DataAccess

    Public Class UtilityDB

        Public Shared Function GetDeptColor(ByVal deptid As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldColor,'grey') from tbldepartment where fldID=@deptid", myConnection)
            myCommand.Parameters.AddWithValue("@deptid", deptid)
            Return myCommand.ExecuteScalar
        End Function

        Public Shared Function InsertEmail(ByVal DateTime As DateTime, ByVal memberID As Long, ByVal senderID As Long, ByVal email As String, ByVal title As String, ByVal content As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Insert Into tblemail (fldDateTime, fldMID, fldSenderID, fldEmail, fldTitle, fldContent) Values (@fldDateTime, @fldMID, @fldSenderID, @fldEmail, @fldTitle, @fldContent)", myConnection)
            myCommand.Parameters.AddWithValue("@fldDateTime", DateTime)
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@fldSenderID", senderID)
            myCommand.Parameters.AddWithValue("@fldEmail", email)
            myCommand.Parameters.AddWithValue("@fldTitle", title)
            myCommand.Parameters.AddWithValue("@fldContent", content)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function InsertSMS(ByVal DateTime As DateTime, ByVal memberID As Long, ByVal contactNo As String, ByVal message As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Insert Into tblsms (fldDateTime, fldMID, fldHP, fldMsg) Values (@fldDateTime, @fldMID, @fldHP, @fldMsg)", myConnection)
            myCommand.Parameters.AddWithValue("@fldDateTime", DateTime)
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@fldHP", contactNo)
            myCommand.Parameters.AddWithValue("@fldMsg", message)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Sub SaveLog(ByVal memberID As Long, ByVal adminID As Long, ByVal type As String, ByVal description As String, ByVal reference As String, ByVal myConnection As MySqlConnection)
            Dim myCommand As MySqlCommand = New MySqlCommand("Insert Into tbladmin_log (fldDateTime, fldMID, fldAID, fldType, fldDesc, fldReference) Values (now(), @fldMID, @fldAID, @fldType, @fldDesc, @fldReference)", myConnection)
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@fldAID", adminID)
            myCommand.Parameters.AddWithValue("@fldType", type)
            myCommand.Parameters.AddWithValue("@fldDesc", description)
            myCommand.Parameters.AddWithValue("@fldReference", reference)
            myCommand.ExecuteNonQuery()
        End Sub

        Public Shared Function MD5Encrypt(ByVal STR As String, ByVal myConnection As MySqlConnection) As String
            Return MySqlHelper.ExecuteScalar(AppConfiguration.ConnectionString, "select cast(md5(?Str) as char)", New MySqlParameter("?Str", STR))
        End Function

        Public Shared Function GetMemberType(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembertype", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMemberType(ByVal ID As Long, ByVal myConnection As MySqlConnection) As String
            Return MySqlHelper.ExecuteScalar(AppConfiguration.ConnectionString, "Select fldDesc From tblmembertype Where fldID = ?fldID", New MySqlParameter("?fldID", ID))
        End Function

        Public Shared Function GetTransactionType(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tbltransactiontype", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountryList(ByVal Connection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As New MySqlCommand("Select * from tblcountry", Connection)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            adapter.Dispose()
            Return myDataTable
        End Function

        Public Shared Function GetServerDateTime(ByVal Connection As MySqlConnection) As DateTime
            Dim datetime As DateTime = Nothing
            Dim cmdDateTime As New MySqlCommand("Select Now() As ServerTime", Connection)
            'Dim cmdDateTime As New MySqlCommand("Select UTC_TIMESTAMP()", Connection)
            datetime = cmdDateTime.ExecuteScalar()
            Return datetime
        End Function

        Public Shared Function GetServerDayOfWeek(ByVal Connection As MySqlConnection) As Integer
            Dim dayOfWeek As Integer = Nothing
            Dim cmdDateTime As New MySqlCommand("Select DAYOFWEEK(Now()) As DayOfWeek", Connection)
            dayOfWeek = cmdDateTime.ExecuteScalar()
            Return dayOfWeek
        End Function

        Public Shared Function GetServerDayNameOfWeek(ByVal Connection As MySqlConnection) As String
            Dim dayOfWeek As String = Nothing
            Dim cmdDateTime As New MySqlCommand("Select DAYNAME(Now()) As DayOfWeek", Connection)
            dayOfWeek = cmdDateTime.ExecuteScalar()
            Return dayOfWeek
        End Function

        Public Shared Function GetLanguageList(ByVal Connection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As New MySqlCommand("Select * from tbllanguage", Connection)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            adapter.Dispose()
            Return myDataTable
        End Function

        Public Shared Sub test()
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Dim myCommand As MySqlCommand = New MySqlCommand("Insert into tbltest (fldContent) values (@fldContent)", myConnection)
                myCommand.Parameters.AddWithValue("@fldContent", "test")
                myConnection.Open()
                myCommand.ExecuteNonQuery()
                myConnection.Close()
            End Using
        End Sub

    End Class
End Namespace
