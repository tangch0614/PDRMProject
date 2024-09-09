Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class MenuManager

#Region "Public Methods"


        Public Shared Function GetMemberMenuList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MemberMenuDB.GetMenuList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetMemberMenuList(ByVal MemberType As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = Nothing
                If MemberType > 0 Then
                    myDataTable = MemberMenuDB.GetMenuList(MemberType, myConnection)
                Else
                    myDataTable = MemberMenuDB.GetMenuList(True, myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAdminMenuList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AdminMenuDB.GetMenuList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetPermanentMenuList(ByVal UserType As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = New DataTable()
                If UserType.Equals("M") Then
                    myDataTable = MemberMenuDB.GetPermanentMenuList(myConnection)
                ElseIf UserType.Equals("A") Then
                    myDataTable = AdminMenuDB.GetPermanentMenuList(myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAdminMenuList(ByVal RoleID As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = Nothing
                If RoleID > 0 Then
                    myDataTable = AdminMenuDB.GetMenuList(RoleID, myConnection)
                Else
                    myDataTable = AdminMenuDB.GetMenuList(True, myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AdminMenuDB.GetList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetItem(ByVal fldMenuID As Long) As MenuObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myMenu As MenuObj = AdminMenuDB.GetItem(fldMenuID, myConnection)
                myConnection.Close()
                Return myMenu
            End Using
        End Function

        Public Shared Function Save(ByVal myMenu As MenuObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = AdminMenuDB.Save(myMenu, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myMenu As MenuObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AdminMenuDB.Delete(myMenu.fldMenuID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
