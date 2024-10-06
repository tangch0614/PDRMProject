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

        Public Shared Function CountActiveEMDList_ByDept() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As DataTable = EMDDeviceDB.CountActiveEMDList_ByDept(myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountInactiveEMDList_ByDept() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As DataTable = EMDDeviceDB.CountInactiveEMDList_ByDept(myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountEMDStatus(ByVal status As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.CountEMDStatus(status, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountActiveEMD() As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.CountActiveEMD(myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function CountInactiveEMD() As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.CountInactiveEMD(myConnection)
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

        Public Shared Function GetEMDDeviceID(ByVal oppid As Long, ByVal name As String, ByVal imei As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.GetEMDDeviceID(oppid, name, imei, myConnection)
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

        Public Shared Function VerifySerialNum(ByVal serialno As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = EMDDeviceDB.VerifySerialNum(serialno, myConnection)
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

        Public Shared Function GetActiveDeviceList(ByVal deptid As Long) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetActiveDeviceList(deptid, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetInactiveDeviceList(ByVal deptid As Long) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetInactiveDeviceList(deptid, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetDeviceList(ByVal deviceid As Long, ByVal oppid As Long, ByVal deptid As Long, ByVal imei As String, ByVal name As String, ByVal serialno As String, ByVal size As String, ByVal simno1 As String, ByVal simno2 As String, ByVal devicestatus As String, ByVal oppstatus As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDDeviceDB.GetDeviceList(deviceid, oppid, deptid, imei, name, serialno, size, simno1, simno2, devicestatus, oppstatus, myConnection)
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
