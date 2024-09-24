Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class OPPManager

#Region "Public Methods"

        Public Shared Function GetOPPID(ByVal deviceid As Long, ByVal name As String, ByVal icno As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = OPPDB.GetOPPID(deviceid, name, icno, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetGeofenceStatus(ByVal id As Long) As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Integer = OPPDB.GetGeofenceStatus(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetEMDDeviceID(ByVal id As Long) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = OPPDB.GetEMDDeviceID(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetOPPList(ByVal id As Long, ByVal name As String, ByVal icno As String, ByVal deviceid As Long, ByVal deptid As Long, ByVal policestationid As Long, ByVal orderrefno As String, ByVal status As String, ByVal verifystatus As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = OPPDB.GetOPPList(id, name, icno, deviceid, deptid, policestationid, orderrefno, status, verifystatus, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetOPPList(ByVal id As Long, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = OPPDB.GetOPPList(id, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetOPP(ByVal fldID As Long) As OPPObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myopp As OPPObj = OPPDB.GetOPP(fldID, myConnection)
                myConnection.Close()
                Return myopp
            End Using
        End Function

        Public Shared Function UpdateEMDDeviceID(ByVal oppid As Long, ByVal deviceid As Long, ByVal creatorid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim oriemdid As Long = OPPDB.GetEMDDeviceID(oppid, myConnection)
                    Dim result As Boolean = False
                    If oriemdid <> deviceid Then
                        result = OPPDB.UpdateEMDDeviceID(oppid, deviceid, myConnection)
                        If deviceid > 0 Then
                            If result Then result = EMDDeviceDB.UpdateOPPID(deviceid, oppid, myConnection)
                            If result Then result = EMDDeviceDB.UpdateStatus(deviceid, "Y", myConnection)
                            If result Then result = OPPDB.SaveOPPEMDeviceHist(oppid, deviceid, "ASSIGN", "Assign device to OPP.", creatorid, myConnection)
                        End If
                        If oriemdid > 0 Then
                            If result Then result = EMDDeviceDB.UpdateOPPID(oriemdid, 0, myConnection)
                            If result Then result = EMDDeviceDB.UpdateStatus(oriemdid, "N", myConnection)
                            If result Then result = OPPDB.SaveOPPEMDeviceHist(oppid, oriemdid, "UNASSIGN", "Unassign device from OPP.", creatorid, myConnection)
                        End If
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateEMDDevice(ByVal oppid As Long, ByVal deviceid As Long, ByVal installdate As String, ByVal smarttagcode As String, ByVal obccode As String, ByVal beaconcode As String, ByVal chargercode As String, ByVal strapcode As String, ByVal cablecode As String, ByVal smarttag As Integer, ByVal obc As Integer, ByVal beacon As Integer, ByVal charger As Integer, ByVal strap As Integer, ByVal cable As Integer, ByVal creatorid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim opp As OPPObj = OPPDB.GetOPP(oppid, myConnection)
                    Dim result As Boolean = True
                    If opp.fldEMDDeviceID <> deviceid Then
                        result = OPPDB.UpdateEMDDeviceID(oppid, deviceid, myConnection)
                        If deviceid > 0 Then
                            If result Then result = EMDDeviceDB.UpdateOPPID(deviceid, oppid, myConnection)
                            If result Then result = EMDDeviceDB.UpdateStatus(deviceid, "Y", myConnection)
                            If result Then result = OPPDB.SaveOPPEMDeviceHist(oppid, deviceid, "ASSIGN", "Assign device to OPP.", creatorid, myConnection)
                        End If
                        If opp.fldEMDDeviceID > 0 Then
                            If result Then result = EMDDeviceDB.UpdateOPPID(opp.fldEMDDeviceID, 0, myConnection)
                            If result Then result = EMDDeviceDB.UpdateStatus(opp.fldEMDDeviceID, "N", myConnection)
                            If result Then result = OPPDB.SaveOPPEMDeviceHist(oppid, opp.fldEMDDeviceID, "UNASSIGN", "Unassign device from OPP.", creatorid, myConnection)
                        End If
                    End If
                    If Date.Compare(opp.fldEMDInstallDate, CDate(installdate)) <> 0 AndAlso result Then result = OPPDB.UpdateEMDInstallDate(oppid, installdate, myConnection)
                    If result Then OPPDB.UpdateEMDAccessories(oppid, smarttagcode, obccode, beaconcode, chargercode, strapcode, cablecode, smarttag, obc, beacon, charger, strap, cable, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateOverseerID(ByVal oppid As Long, ByVal overseerid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateOverseerID(oppid, overseerid, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateGeofence1(ByVal oppid As Long, ByVal geofence As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateGeofence1(oppid, geofence, myConnection)
                    'If result Then result = OPPDB.UpdateGeofenceStatus(oppid, 1, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateGeofenceMukim(ByVal oppid As Long, ByVal mukim As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateGeofenceMukim(oppid, mukim, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateGeofenceStatus(ByVal oppid As Long, ByVal status As Integer) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateGeofenceStatus(oppid, status, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateAttachment(ByVal oppid As Long, ByVal attachment As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateAttachment(oppid, attachment, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateVerifyStatus(ByVal oppid As Long, ByVal oldstatus As String, ByVal newstatus As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateVerifyStatus(oppid, oldstatus, newstatus, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateVerifyStatus(ByVal oppid As Long, ByVal oldstatus As String, ByVal newstatus As String, ByVal creatorid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.UpdateVerifyStatus(oppid, oldstatus, newstatus, creatorid, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateStatus(ByVal oppid As Long, ByVal newstatus As String, ByVal creatorid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim curstatus As String = OPPDB.GetStatus(oppid, myConnection)
                    Dim result As Boolean = OPPDB.UpdateStatus(oppid, curstatus, newstatus, myConnection)
                    If result Then result = OPPDB.SaveOPPStatusHist(oppid, curstatus, newstatus, "Update OPP Status", creatorid, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Save(ByVal myopp As OPPObj, ByVal creatorid As Long, ByRef oppid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    myopp.fldRefID = StoredProcDB.spGenerateCode("OPP", "", myConnection)
                    oppid = OPPDB.Save(myopp, myConnection)
                    Dim result As Boolean = oppid > 0
                    If result Then result = OPPDB.SaveOPPStatusHist(oppid, "N", myopp.fldStatus, "Add new OPP", creatorid, myConnection)
                    If myopp.fldEMDDeviceID > 0 Then
                        If result Then result = EMDDeviceDB.UpdateOPPID(myopp.fldEMDDeviceID, oppid, myConnection)
                        If result Then result = EMDDeviceDB.UpdateStatus(myopp.fldEMDDeviceID, "Y", myConnection)
                        If result Then result = OPPDB.SaveOPPEMDeviceHist(oppid, myopp.fldEMDDeviceID, "ASSIGN", "Assign device to New OPP." & If(Not String.IsNullOrWhiteSpace(myopp.fldRemark), " Remark:" & myopp.fldRemark, ""), creatorid, myConnection)
                    End If
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Update(ByVal myopp As OPPObj, ByVal creatorid As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = OPPDB.Save(myopp, myConnection) > 0
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myopp As OPPObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = OPPDB.Delete(myopp.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
