Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class AdminManager

#Region "Public Methods"

        Public Shared Function SearchAdminList(ByVal UserID As Long, ByVal Level As Integer, ByVal CreatorID As Long, ByVal UserCode As String, ByVal Name As String,
                                                 ByVal ICNo As String, ByVal PoliceNo As String, ByVal DeptID As Long, ByVal PoliceStationID As Long, ByVal CountryID As String, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AdminDB.SearchAdminList(UserID, Level, CreatorID, UserCode, Name, ICNo, PoliceNo, DeptID, PoliceStationID, CountryID, Status, DateFrom, DateTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function VerifyLoginID(ByVal loginID As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AdminDB.VerifyLoginID(loginID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifyAdminCode(ByVal adminCode As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = AdminDB.VerifyAdminCode(adminCode, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function ChangeLoginPassword(ByVal fldUserID As Long, ByVal fldPassword As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = AdminDB.ChangeLoginPassword(fldUserID, fldPassword, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        'Public Shared Function GetAdminLoginID(ByVal fldID As Long) As String
        '    Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
        '        myConnection.Open()
        '        Dim myAdmin As String = AdminDB.GetAdminLoginID(fldID, myConnection)
        '        myConnection.Close()
        '        Return myAdmin
        '    End Using
        'End Function

        Public Shared Function GetAdminName(ByVal fldID As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myAdmin As String = AdminDB.GetAdminName(fldID, myConnection)
                myConnection.Close()
                Return myAdmin
            End Using
        End Function

        Public Shared Function GetAdminList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AdminDB.GetAdminList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAdmin(ByVal fldID As Long) As AdminObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myAdmin As AdminObj = AdminDB.GetAdmin(fldID, myConnection)
                myConnection.Close()
                Return myAdmin
            End Using
        End Function

        Public Shared Function Save(ByVal myAdmin As AdminObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = AdminDB.Save(myAdmin, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myAdmin As AdminObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = AdminDB.Delete(myAdmin.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
