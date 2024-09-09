Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class NewsManager

#Region "Public Methods"

        Public Shared Function SearchNewsList(ByVal newsID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal creator As String, ByVal subject As String, ByVal status As String, ByVal language As String, ByVal popup As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = NewsDB.SearchNewsList(newsID, dateFrom, dateTo, creator, subject, status, language, popup, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function SearchNewsList(ByVal newsID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal creator As String, ByVal subject As String, ByVal status As String, ByVal language As String, ByVal popup As Integer, ByVal PageNum As Integer, ByVal PageSize As Integer, ByRef PageCount As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = NewsDB.SearchNewsList(newsID, dateFrom, dateTo, creator, subject, status, language, popup, PageNum, PageSize, PageCount, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetNewsList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = NewsDB.GetNewsList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetNewsList(ByVal Limit As Integer, ByVal language As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = NewsDB.GetNewsList(Limit, language, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetNewsList(ByVal Limit As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = NewsDB.GetNewsList(Limit, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetNews(ByVal fldID As Integer) As NewsObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myNews As NewsObj = NewsDB.GetNews(fldID, myConnection)
                myConnection.Close()
                Return myNews
            End Using
        End Function

        Public Shared Function Save(ByVal myNews As NewsObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Integer = NewsDB.Save(myNews, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal NewsID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = NewsDB.Delete(NewsID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function Delete(ByVal myNews As NewsObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = NewsDB.Delete(myNews.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
