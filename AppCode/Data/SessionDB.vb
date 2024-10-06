Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class SessionDB

#Region "Public Methods"

        Public Shared Function CountLoggedInUser(ByVal myConnection As MySqlConnection) As Integer
            Dim myCommand As MySqlCommand = New MySqlCommand("Select count(*) from tblsession Where fldUserID>0", myConnection)
            Return myCommand.ExecuteScalar
        End Function

        Public Shared Function GetItem(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As SessionObj
            Dim mySession As SessionObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsession Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    mySession = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return mySession
        End Function

        Public Shared Function GetList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsession", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal mySession As SessionObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If mySession.fldUserID = Nothing Then
                processExe = "Insert into tblsession (fldSessionID, fldUserID, fldUserType, fldLoginDateTime, fldUserCode, fldIPAddress) Values (@fldSessionID, @fldUserID, @fldUserType, @fldLoginDateTime, @fldUserCode, @fldIPAddress)"
                isInsert = True
            Else
                processExe = "Update tblsession set fldSessionID = @fldSessionID, fldUserID = @fldUserID, fldUserType = @fldUserType, fldLoginDateTime = @fldLoginDateTime, fldUserCode = @fldUserCode, fldIPAddress = @fldIPAddress Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", mySession.fldUserID)

            myCommand.Parameters.AddWithValue("@fldSessionID", mySession.fldSessionID)
            myCommand.Parameters.AddWithValue("@fldUserID", mySession.fldUserID)
            myCommand.Parameters.AddWithValue("@fldUserType", mySession.fldUserType)
            myCommand.Parameters.AddWithValue("@fldLoginDateTime", mySession.fldLoginDateTime)
            myCommand.Parameters.AddWithValue("@fldUserCode", mySession.fldUserCode)
            myCommand.Parameters.AddWithValue("@fldIPAddress", mySession.fldIPAddress)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblsession Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As SessionObj
            Dim mySession As SessionObj = New SessionObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSessionID"))) Then
                mySession.fldSessionID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSessionID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldUserID"))) Then
                mySession.fldUserID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldUserID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldUserType"))) Then
                mySession.fldUserType = myDataRecord.GetString(myDataRecord.GetOrdinal("fldUserType"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLoginDateTime"))) Then
                mySession.fldLoginDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLoginDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldUserCode"))) Then
                mySession.fldUserCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldUserCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldIPAddress"))) Then
                mySession.fldIPAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("fldIPAddress"))
            End If
            Return mySession
        End Function
    End Class

 End NameSpace 
