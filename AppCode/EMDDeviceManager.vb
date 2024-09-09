Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class EMDDeviceManager

#Region "Notification"

        Public Shared Function CountNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer) As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Integer = EMDDeviceDB.CountNotification(deviceid, oppid, deptid, processstatus, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetAlertNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal userid As Long, ByVal page As String, ByVal intervalminute As Integer, ByVal updatestatus As Boolean) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = EMDDeviceDB.GetAlertNotification(deviceid, oppid, imei, userid, page, intervalminute, myConnection)
                If updatestatus AndAlso Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                    Dim ids As List(Of Long) = myDataTable.AsEnumerable().Select(Function(row) row.Field(Of Long)("fldID")).ToList()
                    result = EMDDeviceDB.UpdateAlertNotificationSeenUser(ids, userid, page, myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer, ByVal limit As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = EMDDeviceDB.GetAlertNotification(deviceid, oppid, imei, processstatus, severity, intervalminute, limit, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function UpdateProcessStatus(ByVal id As Long, ByVal creatorid As Long, ByVal remark As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = EMDDeviceDB.UpdateProcessStatus(id, creatorid, remark, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

#End Region

#Region "EMD History"

        Public Shared Function GetDeviceHistory(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String, ByVal intervalsec As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceHistory(deviceid, oppid, imei, frdatetime, todatetime, intervalsec, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetDeviceHistoryFiltered(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceHistoryFiltered(deviceid, oppid, imei, frdatetime, todatetime, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

#End Region

#Region "EMD Device"

        Public Shared Function CountEMDStatus(ByVal status As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.CountEMDStatus(status, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetOPPID(ByVal id As Long) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.GetOPPID(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetIMEI(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = EMDDeviceDB.GetIMEI(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifyIMEI(ByVal imei As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.VerifyIMEI(imei, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifySimNo(ByVal simno As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.VerifySimNo(simno, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal simno As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceList(deviceid, oppid, imei, simno, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal imei As String, ByVal simno As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceList(deviceid, imei, simno, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetDevice(ByVal fldID As Long) As EMDDeviceObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim device As EMDDeviceObj = EMDDeviceDB.GetDevice(fldID, myConnection)
                myConnection.Close()
                Return device
            End Using
        End Function

        Public Shared Function Save(ByVal device As EMDDeviceObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = EMDDeviceDB.Save(device, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal device As EMDDeviceObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = EMDDeviceDB.Delete(device.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
