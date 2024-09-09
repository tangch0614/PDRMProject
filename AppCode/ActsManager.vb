Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class ActsManager

#Region "Public Methods"

        Public Shared Function GetActsName(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = ActsDB.GetActsName(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetActsSectionName(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = ActsDB.GetActsSectionName(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetActsList(ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = ActsDB.GetActsList(status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetActsSectionList(ByVal actsid As Long, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = ActsDB.GetActsSectionList(actsid, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function
#End Region

    End Class

End Namespace
