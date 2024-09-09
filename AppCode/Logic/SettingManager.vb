Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class SettingManager

#Region "Public Methods"

        Public Shared Function GetSettingList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = SettingDB.GetSettingList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetSettingValue(ByVal fldSetting As String) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim value As String = SettingDB.GetSettingValue(fldSetting, myConnection)
                myConnection.Close()
                Return value
            End Using
        End Function

        Public Shared Function GetSetting(ByVal fldID As Long) As SettingObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim mySetting As SettingObj = SettingDB.GetSetting(fldID, myConnection)
                myConnection.Close()
                Return mySetting
            End Using
        End Function

        Public Shared Function GetSetting(ByVal fldSetting As String) As SettingObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim mySetting As SettingObj = SettingDB.GetSetting(fldSetting, myConnection)
                myConnection.Close()
                Return mySetting
            End Using
        End Function

        Public Shared Function Update(ByVal value As String, ByVal settingID As Long, ByVal settingHistory As SettingHistoryObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim dateTime As DateTime = UtilityDB.GetServerDateTime(myConnection)
                    Dim result As Long = SettingDB.Update(value, settingID, myConnection)
                    If result Then
                        settingHistory.fldDateTime = dateTime
                        result = SettingHistoryDB.Save(settingHistory, myConnection)
                        myConnection.Close()
                        myTransactionScope.Complete()
                    End If
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Save(ByVal mySetting As SettingObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = SettingDB.Save(mySetting, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal mySetting As SettingObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = SettingDB.Delete(mySetting.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
