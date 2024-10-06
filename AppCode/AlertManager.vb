Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class AlertManager

#Region "Notification"

        Public Shared Function GetViolateTermsList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As DataTable = AlertDB.GetViolateTermsList(myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetProcessStatus(ByVal alertid As Long) As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Integer = AlertDB.GetProcessStatus(alertid, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetAlertRemarkHist(ByVal alertid As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = AlertDB.GetAlertRemarkHist(alertid, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountAlert(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer) As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Integer = AlertDB.CountAlert(deviceid, oppid, deptid, processstatus, severity, intervalminute, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountAlertGroup(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim datatable As DataTable = AlertDB.CountAlertGroup(deviceid, oppid, deptid, processstatus, severity, intervalminute, myConnection)
                myConnection.Close()
                Return datatable
            End Using
        End Function

        Public Shared Function GetAlertNotification(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal userid As Long, ByVal processstatus As Integer, ByVal page As String, ByVal intervalminute As Integer, ByVal updatestatus As Boolean) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = AlertDB.GetAlertNotification(alertid, deviceid, oppid, imei, userid, processstatus, page, intervalminute, myConnection)
                If updatestatus AndAlso Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                    Dim ids As List(Of Long) = myDataTable.AsEnumerable().Select(Function(row) row.Field(Of Long)("fldID")).ToList()
                    result = AlertDB.UpdateAlertNotificationSeenUser(ids, userid, page, myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertInfoList(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal overseerid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal intervalminute As Integer, ByVal limit As Integer, ByVal order As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = AlertDB.GetAlertInfoList(alertid, deviceid, oppid, overseerid, processstatus, severity, dateFrom, dateTo, intervalminute, limit, UtilityManager.EscapeSQLString(order), myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertList(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppID As Long, ByVal processstatus As Integer, ByVal alerttype As String, ByVal severity As String, ByVal dateFrom As String, ByVal dateTo As String, ByVal intervalminute As Integer, ByVal limit As Integer, ByVal order As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AlertDB.GetAlertList(alertid, deviceid, oppID, processstatus, alerttype, severity, dateFrom, dateTo, intervalminute, limit, UtilityManager.EscapeSQLString(order), myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertReport(ByVal deviceid As Long, ByVal oppID As Long, ByVal actsid As Long, ByVal state As String, ByVal mukim As String, ByVal alerttype As String, ByVal processstatus As Integer, ByVal severity As String, ByVal dateTimeFrom As String, ByVal dateTimeTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = AlertDB.GetAlertReport(deviceid, oppID, actsid, state, mukim, alerttype, processstatus, severity, dateTimeFrom, dateTimeTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function UpdateProcessStatus(ByVal id As Long, ByVal newstatus As Integer, ByVal creatorid As Long, ByVal remark As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim oldstatus As Integer = AlertDB.GetProcessStatus(id, myConnection)
                    Dim result As Boolean = AlertDB.UpdateProcessStatus(id, newstatus, oldstatus, myConnection)
                    If result Then result = AlertDB.SaveProcessLog(id, newstatus, creatorid, remark, myConnection)
                    If result AndAlso oldstatus = 0 Then result = AlertDB.UpdateProcessUserID(id, creatorid, myConnection)
                    If result Then result = AlertDB.UpdateLastProcessUserID(id, creatorid, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertAlert(ByVal DeviceID As Long, ByVal MsgType As String, ByVal Level As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    Dim oppid As String = EMDDeviceDB.GetOPPID(DeviceID, myConnection)
                    If Not String.IsNullOrEmpty(IMEI) Then
                        result = AlertDB.InsertAlert(IMEI, oppid, DeviceID, MsgType, Level, myConnection)
                    End If
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
