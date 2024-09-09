Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class RoleMenuDB

#Region "Public Methods"

        Public Shared Function GetItem(ByVal fldRoleID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As RoleMenuObj
            Dim myRoleMenu As RoleMenuObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblrolemenu Where fldRoleID = @fldRoleID And fldUserType = @fldUserType", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myRoleMenu = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myRoleMenu
        End Function

        Public Shared Function GetList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblrolemenu", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal myRoleMenu As RoleMenuObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myRoleMenu.fldRoleID = Nothing Then
                processExe = "Insert into tblrolemenu (fldRoleID, fldMenuID, fldUserType) Values (@fldRoleID, @fldMenuID, @fldUserType)"
                isInsert = True
            Else
                processExe = "Update tblrolemenu set fldRoleID = @fldRoleID, fldMenuID = @fldMenuID, fldUserType = @fldUserType Where fldRoleID = @fldRoleID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldRoleID", myRoleMenu.fldRoleID)

            myCommand.Parameters.AddWithValue("@fldRoleID", myRoleMenu.fldRoleID)
            myCommand.Parameters.AddWithValue("@fldMenuID", myRoleMenu.fldMenuID)
            myCommand.Parameters.AddWithValue("@fldUserType", myRoleMenu.fldUserType)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldRoleID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblrolemenu Where fldRoleID = @fldRoleID And fldUserType = @fldUserType", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As RoleMenuObj
            Dim myRoleMenu As RoleMenuObj = New RoleMenuObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRoleID"))) Then
                myRoleMenu.fldRoleID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldRoleID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuID"))) Then
                myRoleMenu.fldMenuID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldMenuID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldUserType"))) Then
                myRoleMenu.fldUserType = myDataRecord.GetString(myDataRecord.GetOrdinal("fldUserType"))
            End If
            Return myRoleMenu
        End Function
    End Class

 End NameSpace 
