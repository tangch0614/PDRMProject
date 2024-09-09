Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class UserMenuManager

#Region "Public Methods"
        Public Shared Function Authorization(ByVal UserID As Long, ByVal UserType As String, ByVal MenuPath As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Dim result As Boolean = False
                Dim menuID As Long = 0
                Dim restictedmenu As String = ""
                myConnection.Open()
                restictedmenu = UserMenuDB.GetRestrictedList(UserID, UserType, myConnection)
                If UserType = "A" Then
                    menuID = AdminMenuDB.GetMenuID(MenuPath, myConnection)
                ElseIf UserType = "M" Then
                    menuID = MemberMenuDB.GetMenuID(MenuPath, myConnection)
                End If
                myConnection.Close()
                If Not restictedmenu Is Nothing Then
                    Dim restictedlist As String() = restictedmenu.Split(",")
                    result = Not restictedlist.Contains(menuID)
                Else
                    result = True
                End If
                Return result
            End Using
        End Function

        'Public Shared Function Authorization(ByVal UserID As Long, ByVal UserType As String, ByVal MenuPath As String) As Boolean
        '    Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
        '        Dim result As Boolean = False
        '        Dim menuID As Long = 0
        '        Dim usermenu As String = ""
        '        myConnection.Open()
        '        usermenu = UserMenuDB.GetPermittedList(UserID, UserType, myConnection)
        '        If UserType = "A" Then
        '            menuID = AdminMenuDB.GetMenuID(MenuPath, myConnection)
        '        ElseIf UserType = "M" Then
        '            menuID = MemberMenuDB.GetMenuID(MenuPath, myConnection)
        '        End If
        '        myConnection.Close()
        '        If Not usermenu Is Nothing Then
        '            Dim menulist As String() = usermenu.Split(",")
        '            result = menulist.Contains(menuID)
        '        Else
        '            result = True
        '        End If
        '        Return result
        '    End Using
        'End Function

        Public Shared Function GetPermittedList(ByVal fldUserID As Long, ByVal fldUserType As String) As String()
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim menulist As String = UserMenuDB.GetPermittedList(fldUserID, fldUserType, myConnection)
                myConnection.Close()
                Return If(menulist Is Nothing, menulist, menulist.Split(","))
            End Using
        End Function

        Public Shared Function GetRestrictedList(ByVal fldUserID As Long, ByVal fldUserType As String) As String()
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim restrictedlist As String = UserMenuDB.GetRestrictedList(fldUserID, fldUserType, myConnection)
                myConnection.Close()
                Return If(restrictedlist Is Nothing, restrictedlist, restrictedlist.Split(","))
            End Using
        End Function

        Public Shared Function GetList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = UserMenuDB.GetList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function SavePermit(ByVal userID As Long, ByVal UserType As String, permitList As String()) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = UserMenuDB.SavePermit(userID, UserType, permitList, myConnection)
                    myConnection.Close()
                    If result Then
                        myTransactionScope.Complete()
                    End If
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function SaveRestrict(ByVal userID As Long, ByVal UserType As String, ByVal restrictList As String()) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = UserMenuDB.SaveRestrict(userID, UserType, restrictList, myConnection)
                    myConnection.Close()
                    If result Then
                        myTransactionScope.Complete()
                    End If
                    Return result
                End Using
            End Using
        End Function

#End Region

    End Class

End Namespace
