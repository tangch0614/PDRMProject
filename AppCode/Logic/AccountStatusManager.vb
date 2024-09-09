Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class AccountStatusManager

#Region "Public Methods"

        Public Shared Function UpdateAccountStatus(ByVal MemberID As Long, ByVal CurrentStatus As String, ByVal NewStatus As String, ByVal remark As String, ByVal creatorID As Long, ByVal creatorType As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim datetime As DateTime = UtilityDB.GetServerDateTime(myConnection)
                    Dim result As Boolean = MembershipDB.UpdateAccountStatus(MemberID, CurrentStatus, NewStatus, myConnection)
                    If result Then
                        Dim history As AccountStatusHistObj = New AccountStatusHistObj
                        history.fldDateTime = datetime
                        history.fldMID = MemberID
                        history.fldCurrentStatus = CurrentStatus
                        history.fldStatus = NewStatus
                        history.fldRemark = remark
                        history.fldCreatorID = creatorID
                        history.fldCreatorType = creatorType
                        result = AccountStatusHistDB.Save(history, myConnection) > 0
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function GetAccountStatusHistory(ByVal MemberID As Long, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal CreatorID As Long) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AccountStatusHistDB.GetAccountStatusHistory(MemberID, Status, DateFrom, DateTo, CreatorID, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAccountStatusHistList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AccountStatusHistDB.GetAccountStatusHistList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAccountStatusHist(ByVal fldID As Long) As AccountStatusHistObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim accountStatusHist As AccountStatusHistObj = AccountStatusHistDB.GetAccountStatusHist(fldID, myConnection)
                myConnection.Close()
                Return accountStatusHist
            End Using
        End Function

        Public Shared Function Save(ByVal accountStatusHist As AccountStatusHistObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = AccountStatusHistDB.Save(accountStatusHist, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal accountStatusHist As AccountStatusHistObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AccountStatusHistDB.Delete(accountStatusHist.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
