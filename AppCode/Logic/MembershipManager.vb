Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class MembershipManager

#Region "Public Methods"

#Region "DETAILS GET, VERIFICATION"

        Public Shared Function VerifySponsorLink(ByVal link As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = MembershipDB.VerifySponsorLink(link, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetMaxIncomeBalance(ByVal memberID As Long) As Decimal
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Decimal = MembershipDB.GetMaxIncomeBalance(memberID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function AutoMaintainList(ByVal memberID As Long, ByVal countryID As String, ByVal yearMonthFrom As String, ByVal yearMonthTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MembershipDB.AutoMaintainList(memberID, countryID, yearMonthFrom, yearMonthTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetSponsorGroupSales(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String) As Decimal
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myBonus As Decimal = MembershipDB.GetSponsorGroupSales(memberID, dateFrom, dateTo, myConnection)
                myConnection.Close()
                Return myBonus
            End Using
        End Function

        Public Shared Function GetPersonalSales(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String) As Decimal
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myBonus As Decimal = MembershipDB.GetPersonalSales(memberID, dateFrom, dateTo, myConnection)
                myConnection.Close()
                Return myBonus
            End Using
        End Function

        Public Shared Function GetMemberList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MembershipDB.GetMemberList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function


        Public Shared Function GetMember(ByVal memberCode As String) As MembershipObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myMember As MembershipObj = MembershipDB.GetMember(memberCode, myConnection)
                myConnection.Close()
                Return myMember
            End Using
        End Function

        Public Shared Function GetMember(ByVal memberID As Long) As MembershipObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myMember As MembershipObj = MembershipDB.GetMember(memberID, myConnection)
                myConnection.Close()
                Return myMember
            End Using
        End Function

        Public Shared Function GetRootMember() As MembershipObj
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myMember As MembershipObj = MembershipDB.GetRootMember(myConnection)
                myConnection.Close()
                Return myMember
            End Using
        End Function

        Public Shared Function GetMemberLoginID(ByVal memberID As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim memberCode As String = MembershipDB.GetMemberLoginID(memberID, myConnection)
                myConnection.Close()
                Return memberCode
            End Using
        End Function

        Public Shared Function GetMemberRank(ByVal memberID As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim memberCode As String = MembershipDB.GetMemberRank(memberID, myConnection)
                myConnection.Close()
                Return memberCode
            End Using
        End Function

        Public Shared Function GetTreeView(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MembershipDB.GetTreeView(memberID, dateFrom, dateTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetTreeView() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MembershipDB.GetTreeView(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function VerifyDuplicateICNum(ByVal icNum As String, ByVal rank As Long) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = MembershipDB.VerifyDuplicateICNum(icNum, rank, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifyMemberCode(ByVal memberCode As String) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = MembershipDB.VerifyMemberCode(memberCode, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifySponsorMember(ByVal DownlineMID As Long, ByVal UplineMID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = MembershipDB.VerifySponsorMember(DownlineMID, UplineMID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifySponsorGroupMember(ByVal SearchMID As Long, ByVal MemberID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = MembershipDB.VerifySponsorMember(SearchMID, MemberID, myConnection)
                If Not result Then result = MembershipDB.VerifySponsorMember(MemberID, SearchMID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifyAccountStatus(ByVal memberID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = MembershipDB.VerifyAccountStatus(memberID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function VerifyActivation(ByVal memberID As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = MembershipDB.VerifyActivation(memberID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function SearchMemberList(ByVal MemberID As Long, ByVal PackageID As Long, ByVal SponsorID As Long, ByVal MemberType As Long, ByVal BankID As Long, ByVal MemberCode As String, ByVal Name As String, _
                                                ByVal ICNo As String, ByVal CountryID As String, ByVal State As String, ByVal Status As String, ByVal Activate As String, ByVal DateFrom As String, ByVal DateTo As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MembershipDB.SearchMemberList(MemberID, PackageID, SponsorID, MemberType, BankID, MemberCode, Name, ICNo, CountryID, State, Status, Activate, DateFrom, DateTo, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Sub SearchMemberListWithPaging(ByVal MemberID As Long, ByVal PackageID As Long, ByVal SponsorID As Long, ByVal MemberType As Long, ByVal BankID As Long, ByVal MemberCode As String, ByVal Name As String, _
                                                ByVal ICNo As String, ByVal CountryID As String, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal PageNum As Integer, ByVal PageSize As Integer, ByRef PageCount As Integer, ByRef myDataTable As DataTable)
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                MembershipDB.SearchMemberListWithPaging(MemberID, PackageID, SponsorID, MemberType, BankID, MemberCode, Name, ICNo, CountryID, Status, DateFrom, DateTo, PageNum, PageSize, PageCount, myDataTable, myConnection)
                myConnection.Close()
            End Using
        End Sub
#End Region

#Region "DETAILS UPDATE, SAVE, DELETE"

        Public Shared Function UpdateSponsorLink(ByVal MemberID As Long, ByVal Link As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.UpdateSponsorLink(MemberID, Link, myConnection)
                    myConnection.Close()
                    If result Then myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateMemberType(ByVal MemberID As Long, ByVal CurrentType As Long, ByVal NewType As Long) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.UpdateMemberType(MemberID, CurrentType, NewType, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateAccountStatus(ByVal MemberID As Long, ByVal CurrentStatus As String, ByVal NewStatus As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.UpdateAccountStatus(MemberID, CurrentStatus, NewStatus, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateActivateStatus(ByVal MemberID As Long, ByVal CurrentStatus As String, ByVal NewStatus As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.UpdateActivateStatus(MemberID, CurrentStatus, NewStatus, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function UpdateMemberCode(ByVal MemberID As Long, ByVal MemberCode As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.UpdateMemberCode(MemberID, MemberCode, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function ChangeLoginPassword(ByVal fldUserID As Long, ByVal fldPassword As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.ChangeLoginPassword(fldUserID, fldPassword, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function ChangeSecurityPassword(ByVal fldUserID As Long, ByVal fldTransactionPassword As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Boolean = MembershipDB.ChangeSecurityPassword(fldUserID, fldTransactionPassword, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Save(ByVal myMember As MembershipObj) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                Using myTransactionScope As TransactionScope = New TransactionScope()
                    myConnection.Open()
                    Dim result As Long = MembershipDB.Save(myMember, myConnection)
                    myConnection.Close()
                    myTransactionScope.Complete()
                    Return result
                End Using
            End Using
        End Function

        Public Shared Function Delete(ByVal myMember As MembershipObj) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = MembershipDB.Delete(myMember.fldID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

#End Region

#End Region

    End Class

End Namespace
