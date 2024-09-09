Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class SettingHistoryManager

#Region "Public Methods"

        Public Shared Function GetSettingHistoryList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = SettingHistoryDB.GetSettingHistoryList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetSettingHistory(ByVal fldID As Long) As SettingHistoryObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim mySettingHistory As SettingHistoryObj = SettingHistoryDB.GetSettingHistory(fldID, myConnection)
                myConnection.Close()
                Return mySettingHistory
            End Using
        End Function

        Public Shared Function Save(ByVal mySettingHistory As SettingHistoryObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = SettingHistoryDB.Save(mySettingHistory, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal mySettingHistory As SettingHistoryObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = SettingHistoryDB.Delete(mySettingHistory.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
