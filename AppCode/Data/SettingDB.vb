Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


Namespace DataAccess
    Public Class SettingDB

#Region "Public Methods"

        Public Shared Function GetSettingValue(ByVal fldSetting As String, ByVal myConnection As MySqlConnection) As String
            Dim value As String = "-1"
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldValue From tblsetting Where fldSetting = @fldSetting", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldSetting", fldSetting)
            If Not myCommand.ExecuteScalar.Equals(DBNull.Value) Then
                value = myCommand.ExecuteScalar
            End If
            Return value
        End Function

        Public Shared Function GetSetting(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As SettingObj
            Dim mySetting As SettingObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsetting Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    mySetting = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return mySetting
        End Function

        Public Shared Function GetSetting(ByVal fldSetting As String, ByVal myConnection As MySqlConnection) As SettingObj
            Dim mySetting As SettingObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsetting Where fldSetting = @fldSetting", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldSetting", fldSetting)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    mySetting = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return mySetting
        End Function

        Public Shared Function GetSettingList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsetting", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Update(ByVal Value As String, ByVal settingID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblSetting Set fldValue = @fldValue Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldValue", Value)
            myCommand.Parameters.AddWithValue("@fldID", settingID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function Update(ByVal Value As String, ByVal settingName As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblSetting Set fldValue = @fldValue Where fldSetting = @fldSetting", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldValue", Value)
            myCommand.Parameters.AddWithValue("@fldSetting", settingName)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function Save(ByVal mySetting As SettingObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If mySetting.fldID = Nothing Then
                processExe = "Insert into tblsetting (fldSetting, fldValue, fldDescription) Values (@fldSetting, @fldValue, @fldDescription)"
                isInsert = True
            Else
                processExe = "Update tblsetting set fldSetting = @fldSetting, fldValue = @fldValue, fldDescription = @fldDescription Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", mySetting.fldID)

            myCommand.Parameters.AddWithValue("@fldSetting", mySetting.fldSetting)
            myCommand.Parameters.AddWithValue("@fldValue", mySetting.fldValue)
            myCommand.Parameters.AddWithValue("@fldDescription", mySetting.fldDescription)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblsetting Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As SettingObj
            Dim mySetting As SettingObj = New SettingObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                mySetting.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSetting"))) Then
                mySetting.fldSetting = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSetting"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldValue"))) Then
                mySetting.fldValue = myDataRecord.GetString(myDataRecord.GetOrdinal("fldValue"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDescription"))) Then
                mySetting.fldDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("fldDescription"))
            End If
            Return mySetting
        End Function
    End Class

End Namespace
