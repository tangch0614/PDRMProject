Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


Namespace DataAccess
    Public Class AdminDB

#Region "Public Methods"

        Public Shared Function SearchAdminList(ByVal UserID As Long, ByVal Level As Integer, ByVal CreatorID As Long, ByVal UserCode As String, ByVal Name As String,
                                                ByVal ICNo As String, ByVal PoliceNo As String, ByVal DeptID As Long, ByVal PoliceStationID As Long, ByVal CountryID As String, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = Nothing
            If Not UserID <= 0 Then query &= " a.fldID = @fldID And "
            If Not Level <= -1 Then query &= " a.fldLevel = @fldLevel And "
            If Not CreatorID <= 0 Then query &= " a.fldCreator = @fldCreator And "
            If Not String.IsNullOrEmpty(UserCode) Then query &= " a.fldCode Like @fldCode And "
            If Not String.IsNullOrEmpty(Name) Then query &= " a.fldName Like @fldName AND "
            If Not String.IsNullOrEmpty(ICNo) Then query &= " a.fldICNo Like @fldICNo AND "
            If Not String.IsNullOrEmpty(PoliceNo) Then query &= " a.fldPoliceNo Like @fldPoliceNo AND "
            If Not DeptID < 0 Then query &= " a.fldDeptID = @fldDeptID AND "
            If Not PoliceStationID < 0 Then query &= " a.fldPoliceStationID = @fldPoliceStationID AND "
            If Not String.IsNullOrEmpty(CountryID) Then query &= " a.fldCountryID = @fldCountryID AND "
            If Not String.IsNullOrEmpty(Status) Then query &= " a.fldStatus = @fldStatus AND "
            If Not String.IsNullOrEmpty(DateFrom) Then query &= " a.fldCreatorDateTime >= @DateFrom AND "
            If Not String.IsNullOrEmpty(DateTo) Then query &= " a.fldCreatorDateTime <= @DateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, b.fldrolename, ifnull(c.fldname,'') as fldDepartment, ifnull(d.fldname,'') as fldPSName
                                                                From tbladmin a 
                                                                Join tbladmintype b on b.fldroleid=a.fldlevel 
                                                                left Join tbldepartment c on c.fldid=a.flddeptid 
                                                                left join tblpolicestation d on d.fldid=a.fldpolicestationid" & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", UserID)
            myCommand.Parameters.AddWithValue("@fldCreator", CreatorID)
            myCommand.Parameters.AddWithValue("@fldLevel", Level)
            myCommand.Parameters.AddWithValue("@fldCode", "%" & UserCode & "%")
            myCommand.Parameters.AddWithValue("@fldName", "%" & Name & "%")
            myCommand.Parameters.AddWithValue("@fldICNo", "%" & ICNo & "%")
            myCommand.Parameters.AddWithValue("@fldPoliceNo", "%" & PoliceNo & "%")
            myCommand.Parameters.AddWithValue("@fldPoliceStationID", PoliceStationID)
            myCommand.Parameters.AddWithValue("@fldDeptID", DeptID)
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            myCommand.Parameters.AddWithValue("@fldStatus", Status)
            myCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
            myCommand.Parameters.AddWithValue("@DateTo", DateTo)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function VerifyAdminCode(ByVal adminCode As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID From tbladmin Where fldCode = @fldCode", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", adminCode)
            result = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifyLoginID(ByVal loginID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select Count(*) From tbladmin Where fldCode = @fldCode", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", loginID)
            result = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function ChangeLoginPassword(ByVal fldID As Long, ByVal fldPassword As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tbladmin Set fldPassword = @fldPassword Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            myCommand.Parameters.AddWithValue("@fldPassword", fldPassword)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function GetAdminLoginID(ByVal userID As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldCode From tbladmin Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", userID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetAdminName(ByVal userID As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldName From tbladmin Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", userID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetAdmin(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As AdminObj
            Dim myAdmin As AdminObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tbladmin Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myAdmin = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myAdmin
        End Function

        Public Shared Function GetAdminList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tbladmin", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal myAdmin As AdminObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myAdmin.fldID = Nothing Then
                processExe = "Insert into tbladmin (fldCode, fldName, fldICNo, fldPoliceNo, fldContactNo, fldDeptID, fldPoliceStationID, fldLanguage, fldCountryID, fldLevel, fldPassword, fldTransactionPassword, fldStatus, fldLastLoginAttempt, fldLastLogin, fldLastLogout, fldLoginAttempt, fldCreatorDateTime, fldCreator) Values (@fldCode, @fldName, @fldICNo, @fldPoliceNo, @fldContactNo, @fldDeptID, @fldPoliceStationID, @fldLanguage, @fldCountryID, @fldLevel, @fldPassword, @fldTransactionPassword, @fldStatus, @fldLastLoginAttempt, @fldLastLogin, @fldLastLogout, @fldLoginAttempt, @fldCreatorDateTime, @fldCreator)"
                isInsert = True
            Else
                processExe = "Update tbladmin set fldCode = @fldCode, fldName = @fldName, fldICNo = @fldICNo, fldPoliceNo=@fldPoliceNo, fldContactNo=@fldContactNo, fldDeptID=@fldDeptID, fldPoliceStationID=@fldPoliceStationID, fldLanguage = @fldLanguage, fldCountryID = @fldCountryID, fldLevel = @fldLevel, fldPassword = @fldPassword, fldTransactionPassword = @fldTransactionPassword, fldStatus = @fldStatus, fldLastLoginAttempt = @fldLastLoginAttempt, fldLastLogin = @fldLastLogin, fldLastLogout = @fldLastLogout, fldLoginAttempt = @fldLoginAttempt, fldCreatorDateTime = @fldCreatorDateTime, fldCreator = @fldCreator Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", myAdmin.fldID)

            myCommand.Parameters.AddWithValue("@fldCode", myAdmin.fldCode)
            myCommand.Parameters.AddWithValue("@fldName", myAdmin.fldName)
            myCommand.Parameters.AddWithValue("@fldICNo", myAdmin.fldICNo)
            myCommand.Parameters.AddWithValue("@fldPoliceNo", myAdmin.fldPoliceNo)
            myCommand.Parameters.AddWithValue("@fldContactNo", myAdmin.fldContactNo)
            myCommand.Parameters.AddWithValue("@fldDeptID", myAdmin.fldDeptID)
            myCommand.Parameters.AddWithValue("@fldPoliceStationID", myAdmin.fldPoliceStationID)
            myCommand.Parameters.AddWithValue("@fldLanguage", myAdmin.fldLanguage)
            myCommand.Parameters.AddWithValue("@fldCountryID", myAdmin.fldCountryID)
            myCommand.Parameters.AddWithValue("@fldLevel", myAdmin.fldLevel)
            myCommand.Parameters.AddWithValue("@fldPassword", myAdmin.fldPassword)
            myCommand.Parameters.AddWithValue("@fldTransactionPassword", myAdmin.fldTransactionPassword)
            myCommand.Parameters.AddWithValue("@fldStatus", myAdmin.fldStatus)
            myCommand.Parameters.AddWithValue("@fldLastLoginAttempt", myAdmin.fldLastLoginAttempt)
            myCommand.Parameters.AddWithValue("@fldLastLogin", myAdmin.fldLastLogin)
            myCommand.Parameters.AddWithValue("@fldLastLogout", myAdmin.fldLastLogout)
            myCommand.Parameters.AddWithValue("@fldLoginAttempt", myAdmin.fldLoginAttempt)
            myCommand.Parameters.AddWithValue("@fldCreatorDateTime", myAdmin.fldCreatorDateTime)
            myCommand.Parameters.AddWithValue("@fldCreator", myAdmin.fldCreator)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tbladmin Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As AdminObj
            Dim myAdmin As AdminObj = New AdminObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                myAdmin.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCode"))) Then
                myAdmin.fldCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldName"))) Then
                myAdmin.fldName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldICNo"))) Then
                myAdmin.fldICNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldICNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPoliceNo"))) Then
                myAdmin.fldPoliceNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPoliceNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldContactNo"))) Then
                myAdmin.fldContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldContactNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDeptID"))) Then
                myAdmin.fldDeptID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldDeptID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPoliceStationID"))) Then
                myAdmin.fldPoliceStationID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldPoliceStationID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLanguage"))) Then
                myAdmin.fldLanguage = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLanguage"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCountryID"))) Then
                myAdmin.fldCountryID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCountryID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLevel"))) Then
                myAdmin.fldLevel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldLevel"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPassword"))) Then
                myAdmin.fldPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPassword"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldTransactionPassword"))) Then
                myAdmin.fldTransactionPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("fldTransactionPassword"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myAdmin.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLoginAttempt"))) Then
                myAdmin.fldLastLoginAttempt = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLoginAttempt"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLogin"))) Then
                myAdmin.fldLastLogin = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLogin"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLogout"))) Then
                myAdmin.fldLastLogout = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLogout"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLoginAttempt"))) Then
                myAdmin.fldLoginAttempt = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldLoginAttempt"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorDateTime"))) Then
                myAdmin.fldCreatorDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldCreatorDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreator"))) Then
                myAdmin.fldCreator = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCreator"))
            End If
            Return myAdmin
        End Function
    End Class

End Namespace
