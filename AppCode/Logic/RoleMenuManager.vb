Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class RoleMenuManager

#Region "Public Methods"

        Public Shared Function GetList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = RoleMenuDB.GetList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetItem(ByVal fldRoleID As Long, ByVal fldUserType As String) As RoleMenuObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myRoleMenu As RoleMenuObj = RoleMenuDB.GetItem(fldRoleID, fldUserType, myConnection)
                myConnection.Close()
                Return myRoleMenu
            End Using
        End Function

        Public Shared Function Save(ByVal myRoleMenu As RoleMenuObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = RoleMenuDB.Save(myRoleMenu, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myRoleMenu As RoleMenuObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = RoleMenuDB.Delete(myRoleMenu.fldRoleID, myRoleMenu.fldUserType, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
