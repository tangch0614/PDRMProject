Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class EMDDeviceManager

#Region "Command"

        Public Shared Function InsertGPRSCommand(ByVal DeviceID As Long, ByVal MsgType As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    If DeviceID > 0 AndAlso Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = EMDDeviceDB.GetConnectionID(IMEI, myConnection)
                        If ConnectionID > 0 Then
                            If MsgType.Equals("alarmon") Then
                                MsgContent = "$GPRS," & IMEI & ";W043,1,1,1;!"
                            ElseIf MsgType.Equals("alarmoff") Then
                                MsgContent = "$GPRS," & IMEI & ";W043,1,1,0;!"
                            ElseIf MsgType.Equals("unlock") Then
                                MsgContent = "$,unlock;221088;!"
                            ElseIf MsgType.Equals("lock") Then
                                MsgContent = "$,lock;000000;!"
                            ElseIf MsgType.Equals("vibrate") Then
                                MsgContent = "$GPRS," & IMEI & ";W036,20;!"
                            End If
                            result = EMDDeviceDB.InsertGPRSCommand(ConnectionID, IMEI, MsgContent, myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertGPRSCommand_Unlock(ByVal DeviceID As Long, ByVal password As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    If DeviceID > 0 AndAlso Not String.IsNullOrEmpty(password) AndAlso Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = EMDDeviceDB.GetConnectionID(IMEI, myConnection)
                        If ConnectionID > 0 Then
                            result = EMDDeviceDB.InsertGPRSCommand(ConnectionID, IMEI, "$,unlock;" & password & ";!", myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertGPRSCommand_Lock(ByVal DeviceID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    If DeviceID > 0 AndAlso Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = EMDDeviceDB.GetConnectionID(IMEI, myConnection)
                        If ConnectionID > 0 Then
                            result = EMDDeviceDB.InsertGPRSCommand(ConnectionID, IMEI, "$,lock;000000;!", myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertGPRSCommand_Vibrate(ByVal DeviceID As Long, ByVal VibrateCount As Integer) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    If DeviceID > 0 AndAlso Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = EMDDeviceDB.GetConnectionID(IMEI, myConnection)
                        If ConnectionID > 0 Then
                            result = EMDDeviceDB.InsertGPRSCommand(ConnectionID, IMEI, "$GPRS," & IMEI & ";W036," & VibrateCount & ";!", myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertGPRSCommand_Alarm(ByVal DeviceID As Long, ByVal OthersAlarm As Integer, ByVal BeltAlarm As Integer, ByVal PlayAlarm As Integer) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    If DeviceID > 0 AndAlso Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = EMDDeviceDB.GetConnectionID(IMEI, myConnection)
                        If ConnectionID > 0 Then
                            result = EMDDeviceDB.InsertGPRSCommand(ConnectionID, IMEI, "$GPRS," & IMEI & ";W043," & OthersAlarm & "," & BeltAlarm & "," & PlayAlarm & ";!", myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function InsertNotification(ByVal DeviceID As Long, ByVal MsgType As String, ByVal Level As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = EMDDeviceDB.GetIMEI(DeviceID, myConnection)
                    Dim oppid As String = EMDDeviceDB.GetOPPID(DeviceID, myConnection)
                    If Not String.IsNullOrEmpty(IMEI) Then
                        result = EMDDeviceDB.InsertNotification(IMEI, oppid, DeviceID, MsgType, Level, myConnection)
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

#Region "Notification"

        Public Shared Function CountNotification(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer) As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Integer = EMDDeviceDB.CountNotification(deviceid, oppid, deptid, processstatus, severity, intervalminute, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetAlertNotification(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal userid As Long, ByVal processstatus As Integer, ByVal page As String, ByVal intervalminute As Integer, ByVal updatestatus As Boolean) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = EMDDeviceDB.GetAlertNotification(alertid, deviceid, oppid, imei, userid, processstatus, page, intervalminute, myConnection)
                If updatestatus AndAlso Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                    Dim ids As List(Of Long) = myDataTable.AsEnumerable().Select(Function(row) row.Field(Of Long)("fldID")).ToList()
                    result = EMDDeviceDB.UpdateAlertNotificationSeenUser(ids, userid, page, myConnection)
                End If
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertNotification(ByVal alertid As Long, ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal processstatus As Integer, ByVal severity As String, ByVal intervalminute As Integer, ByVal limit As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = False
                Dim myDataTable As DataTable = EMDDeviceDB.GetAlertNotification(alertid, deviceid, oppid, imei, processstatus, severity, intervalminute, limit, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetAlertNotificationList(ByVal emdID As Long, ByVal oppID As Long, ByVal dateFrom As String, ByVal dateTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetAlertNotificationList(emdID, oppID, dateFrom, dateTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function AcknowledgeAlertNotification(ByVal id As Long, ByVal creatorid As Long, ByVal remark As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = EMDDeviceDB.AcknowledgeAlertNotification(id, creatorid, remark, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

#End Region

#Region "EMD History"

        Public Shared Function GetDeviceHistoryRAW(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal frdatetime As String, ByVal todatetime As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceHistoryRAW(deviceid, oppid, imei, frdatetime, todatetime, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

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

        Public Shared Function GetDeviceHistory_PDF(ByVal deviceid As Long, ByVal oppid As Long, ByVal frdatetime As String, ByVal todatetime As String, ByVal intervalsec As Integer) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceHistory_PDF(deviceid, oppid, frdatetime, todatetime, intervalsec, myConnection)
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

        Public Shared Function VerifyName(ByVal name As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.VerifyName(name, myConnection)
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

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal imei As String, ByVal name As String, ByVal simno1 As String, ByVal simno2 As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceList(deviceid, oppid, imei, name, simno1, simno2, status, myConnection)
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
