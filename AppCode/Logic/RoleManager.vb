Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class RoleManager

#Region "Public Methods"

        Public Shared Function GetRoleName(ByVal fldRoleID As Long, ByVal UserType As String) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myRole As String = RoleDB.GetRoleName(fldRoleID, UserType, myConnection)
                myConnection.Close()
                Return myRole
            End Using
        End Function

        Public Shared Function Authorization(ByVal roleID As Long, ByVal UserType As String, ByVal menuPath As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Dim result As Boolean = False
                If roleID > 0 Then
                    myConnection.Open()
                    result = RoleDB.Authorization(roleID, UserType, menuPath, myConnection)
                    myConnection.Close()
                Else
                    result = True
                End If
                Return result
            End Using
        End Function

        Public Shared Function GetRoleMenu(ByVal fldRoleID As Long, ByVal fldUserType As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = RoleDB.GetRoleMenu(fldRoleID, fldUserType, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetRoleMenuList(ByVal fldRoleID As Long, ByVal fldUserType As String) As String()
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim menulist As String() = RoleDB.GetRoleMenuList(fldRoleID, fldUserType, myConnection)
                myConnection.Close()
                Return menulist
            End Using
        End Function

        Public Shared Function GetRoleList(ByVal UserType As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = RoleDB.GetRoleList(UserType, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetRole(ByVal fldRoleID As Long) As RoleObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myRole As RoleObj = RoleDB.GetRole(fldRoleID, myConnection)
                myConnection.Close()
                Return myRole
            End Using
        End Function

        Public Shared Function Save(ByVal roleID As Long, ByVal UserType As String, menuList As String()) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = RoleDB.Save(roleID, UserType, menuList, myConnection)
                    myConnection.Close()
                    If result Then
                        myTransactionScope.Complete()
                    End If
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Save(ByVal myRole As RoleObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = RoleDB.Save(myRole, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myRole As RoleObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = RoleDB.Delete(myRole.fldRoleID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
