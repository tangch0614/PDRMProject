Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class EMDInstallRequestManager

#Region "Public Methods"

        Public Shared Function GetInstallRequestList(id As Long, creatorid As Long, deptid As Long, ipkid As Long, policestationid As Long, status As String, frinsdate As String, toinsdate As String, frdate As String, todate As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = EMDInstallRequestDB.GetInstallRequestList(id, creatorid, deptid, ipkid, policestationid, status, frinsdate, toinsdate, frdate, todate, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetInstallRequest(ByVal fldID As Long) As EMDInstallRequestObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim installrequest As EMDInstallRequestObj = EMDInstallRequestDB.GetInstallRequest(fldID, myConnection)
                myConnection.Close()
                Return installrequest
            End Using
        End Function

        Public Shared Function UpdateAttachment1(ByVal requestid As Long, ByVal attachment As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = EMDInstallRequestDB.UpdateAttachment1(requestid, attachment, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateAttachment2(ByVal requestid As Long, ByVal attachment As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = EMDInstallRequestDB.UpdateAttachment2(requestid, attachment, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateEMDAcessories(ByVal requestid As Long, ByVal oppid As Long, ByVal emddeviceid As Long, ByVal smarttagcode As String, ByVal obccode As String, ByVal beaconcode As String, ByVal chargercode As String, ByVal strapcode As String, ByVal cablecode As String, ByVal smarttag As Integer, ByVal obc As Integer, ByVal beacon As Integer, ByVal charger As Integer, ByVal strap As Integer, ByVal cable As Integer) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim opp As OPPObj = OPPDB.GetOPP(oppid, myConnection)
                    Dim result As Boolean = True
                    If result Then EMDInstallRequestDB.UpdateEMDAccessories(requestid, oppid, emddeviceid, smarttagcode, obccode, beaconcode, chargercode, strapcode, cablecode, smarttag, obc, beacon, charger, strap, cable, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Save(ByVal installrequest As EMDInstallRequestObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    installrequest.fldRefID = StoredProcDB.spGenerateCode("INST", "", myConnection)
                    Dim result As Long = EMDInstallRequestDB.Save(installrequest, myConnection)
                    myConnection.Close()
                    If result > 0 Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateStatus(ByVal id As Long, ByVal creatorid As Long, ByVal status As String, ByVal remark As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = EMDInstallRequestDB.UpdateStatus(id, creatorid, status, remark, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

    End Class

End Namespace
