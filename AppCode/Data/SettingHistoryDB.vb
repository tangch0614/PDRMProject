Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class SettingHistoryDB

#Region "Public Methods"

        Public Shared Function GetSettingHistory(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As SettingHistoryObj
            Dim mySettingHistory As SettingHistoryObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsettinghistory Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    mySettingHistory = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return mySettingHistory
        End Function

        Public Shared Function GetSettingHistoryList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblsettinghistory", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal mySettingHistory As SettingHistoryObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If mySettingHistory.fldID = Nothing Then
                processExe = "Insert into tblsettinghistory (fldDateTime, fldSetting, fldCurrentValue, fldNewValue, fldRemark, fldUserID) Values (@fldDateTime, @fldSetting, @fldCurrentValue, @fldNewValue, @fldRemark, @fldUserID)"
                isInsert = True
            Else
                processExe = "Update tblsettinghistory set fldDateTime = @fldDateTime, fldSetting = @fldSetting, fldCurrentValue = @fldCurrentValue, fldNewValue = @fldNewValue, fldRemark = @fldRemark, fldUserID = @fldUserID Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", mySettingHistory.fldID)

            myCommand.Parameters.AddWithValue("@fldDateTime", mySettingHistory.fldDateTime)
            myCommand.Parameters.AddWithValue("@fldSetting", mySettingHistory.fldSetting)
            myCommand.Parameters.AddWithValue("@fldCurrentValue", mySettingHistory.fldCurrentValue)
            myCommand.Parameters.AddWithValue("@fldNewValue", mySettingHistory.fldNewValue)
            myCommand.Parameters.AddWithValue("@fldRemark", mySettingHistory.fldRemark)
            myCommand.Parameters.AddWithValue("@fldUserID", mySettingHistory.fldUserID)

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
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblsettinghistory Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As SettingHistoryObj
            Dim mySettingHistory As SettingHistoryObj = New SettingHistoryObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                mySettingHistory.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                mySettingHistory.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSetting"))) Then
                mySettingHistory.fldSetting = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldSetting"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCurrentValue"))) Then
                mySettingHistory.fldCurrentValue = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCurrentValue"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldNewValue"))) Then
                mySettingHistory.fldNewValue = myDataRecord.GetString(myDataRecord.GetOrdinal("fldNewValue"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRemark"))) Then
                mySettingHistory.fldRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRemark"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldUserID"))) Then
                mySettingHistory.fldUserID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldUserID"))
            End If
            Return mySettingHistory
        End Function
    End Class

 End NameSpace 
