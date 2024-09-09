Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class TempGetDataManager

#Region "Public Methods"

        Public Shared Function GetOfficerList(ByVal id As Long, ByVal name As String, ByVal icno As String, ByVal policeno As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = TempGetDataDB.GetOfficerList(id, name, icno, policeno, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

#End Region

        Public Shared Function InsertGPRSMode(ByVal DeviceID As Long, ByVal MsgType As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = False
                    Dim MsgContent As String = ""
                    Dim IMEI As String = TempGetDataDB.GetIMEINo(DeviceID, myConnection)

                    If Not String.IsNullOrEmpty(IMEI) Then
                        Dim ConnectionID As Long = TempGetDataDB.GetConnectionID(IMEI, myConnection)

                        If ConnectionID > 0 Then
                            If MsgType.Equals("Alarm") Then
                                MsgContent = "$GPRS," & IMEI & ";W043,1,1,1;!"
                            ElseIf MsgType.Equals("AlarmOff") Then
                                MsgContent = "$GPRS," & IMEI & ";W043,1,1,0;!"
                            ElseIf MsgType.Equals("Unlock") Then
                                MsgContent = "$,unlock;221088;!"
                            ElseIf MsgType.Equals("Lock") Then
                                MsgContent = "$,lock;000000;!"
                            ElseIf MsgType.Equals("Vibrate") Then
                                MsgContent = "$GPRS," & IMEI & ";W036,20;!"
                            End If

                            result = TempGetDataDB.InsertGPRSMode(ConnectionID, IMEI, MsgContent, myConnection)
                        End If
                    End If

                    myConnection.Close()
                    If result Then
                        myTransactionScope.Complete()
                    End If
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
                    Dim IMEI As String = TempGetDataDB.GetIMEINo(DeviceID, myConnection)
                    Dim oppid As String = EMDDeviceDB.GetOPPID(DeviceID, myConnection)

                    If Not String.IsNullOrEmpty(IMEI) Then
                        result = TempGetDataDB.InsertNotification(IMEI, oppid, DeviceID, MsgType, Level, myConnection)
                    End If

                    myConnection.Close()
                    If result Then
                        myTransactionScope.Complete()
                    End If
                    Return result
                End Using
            End Using
        End Function


    End Class

End Namespace
