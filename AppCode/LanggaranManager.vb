Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class LanggaranManager

#Region "Public Methods"

        Public Shared Function GetWebContentList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = LanggaranDB.GetWebContentList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetWebContentList(ByVal ID As Long, ByVal subject As String, ByVal categoryID As Long, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = LanggaranDB.GetWebContentList(ID, subject, categoryID, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetWebContentCategory(ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = LanggaranDB.GetWebContentCategory(status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetWebContent(ByVal fldID As Long) As LanggaranObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim webcontent As LanggaranObj = LanggaranDB.GetWebContent(fldID, myConnection)
                myConnection.Close()
                Return webcontent
            End Using
        End Function

        'Public Shared Function Save(ByVal webcontent As LanggaranObj) As Long
        '    Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
        '        Using myTransactionScope As TransactionScope = New TransactionScope()
        '            myConnection.Open()
        '            Dim result As Long = LanggaranDB.Save(webcontent, myConnection)
        '            myConnection.Close()
        '            myTransactionScope.Complete()
        '            Return result
        '        End Using
        '    End Using
        'End Function

        Public Shared Function Delete(ByVal webcontent As LanggaranObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = LanggaranDB.Delete(webcontent.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace