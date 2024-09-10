Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class TempGetDataManager

#Region "Public Methods"

        Public Shared Function GetOfficerList(ByVal id As Long, ByVal name As String, ByVal icno As String, ByVal policeno As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = TempGetDataDB.GetOfficerList(id, name, icno, policeno, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

#End Region

    End Class

End Namespace
