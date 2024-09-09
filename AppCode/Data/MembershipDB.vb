Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class MembershipDB

#Region "Get, Search, Verify"

        Public Shared Function GenerateSponsorLink(ByVal memberID As String, ByVal memberCode As String, ByVal myConnection As MySqlConnection) As String
            Dim result As String = Nothing
            Dim myCommand As New MySqlCommand("select CONCAT(SUBSTRING(MD5(CONCAT(@mid,@mcode,'$L!nk')),1,6),@mid)", myConnection)
            myCommand.Parameters.AddWithValue("@mid", memberID)
            myCommand.Parameters.AddWithValue("@mcode", memberCode)
            result = myCommand.ExecuteScalar()
            Return result
        End Function

        Public Shared Function VerifySponsorLink(ByVal link As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID From tblmembership Where fldSponsorLink = @fldSponsorLink And fldStatus='A'", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldSponsorLink", link)
            result = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetMaxIncomeBalance(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Decimal
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldMaxIncome From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function AutoMaintainList(ByVal memberID As Long, ByVal countryID As String, ByVal yearMonthFrom As String, ByVal yearMonthTo As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = Nothing
            If Not memberID = 0 Then query &= " a.fldMID = @fldMID AND "
            If Not String.IsNullOrEmpty(yearMonthFrom) Then query &= " a.fldYM >= @yearMonthFrom AND "
            If Not String.IsNullOrEmpty(yearMonthTo) Then query &= " a.fldYM <= @yearMonthTo AND "
            If Not String.IsNullOrEmpty(countryID) Then query &= " m.fldCountryID = @fldCountryID AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.*, date_format(a.fldDate,'%Y/%m') as fldYearMonth, m.fldCode, m.fldName, m.fldICNo, m.fldMobileNo, m.fldAddress1, m.fldAddress2, m.fldCity, m.fldState, m.fldPostCode, c.fldName as fldCountryName from tblautomaintain a left join tblmembership m on m.fldID = a.fldMID left join tblcountry c on c.fldID = m.fldCountryID " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@yearMonthFrom", yearMonthFrom)
            myCommand.Parameters.AddWithValue("@yearMonthTo", yearMonthTo)
            myCommand.Parameters.AddWithValue("@fldCountryID", countryID)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetSponsorGroupSales(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal myConnection As MySqlConnection) As Decimal
            Dim result As Decimal = Nothing
            Dim query As String = Nothing
            If Not memberID = 0 Then query &= " fldMID = @fldMID AND "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " fldDate >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " fldDate <= @dateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select IfNull(Sum(fldGPV),0) From tblGroupSales " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            result = myCommand.ExecuteScalar()
            Return result
        End Function

        Public Shared Function GetPersonalSales(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal myConnection As MySqlConnection) As Decimal
            Dim result As Decimal = Nothing
            Dim query As String = Nothing
            If Not memberID = 0 Then query &= " fldMID = @fldMID AND "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " fldDate >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " fldDate <= @dateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select IfNull(Sum(fldPV),0) From tblGroupSales " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMID", memberID)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            result = myCommand.ExecuteScalar()
            Return result
        End Function

        Public Shared Function GetMember(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As MembershipObj
            Dim myMember As MembershipObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembership Where fldCode = @fldCode", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", memberCode)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myMember = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myMember
        End Function

        Public Shared Function GetMember(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As MembershipObj
            Dim myMember As MembershipObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myMember = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myMember
        End Function

        Public Shared Function GetMemberList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembership", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMemberLoginID(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldCode From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetMemberRank(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Long
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldRank From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function GetRootMember(ByVal myConnection As MySqlConnection) As MembershipObj
            Dim myMember As MembershipObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembership Order By fldID Limit 1", myConnection)
            myCommand.CommandType = CommandType.Text

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myMember = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myMember
        End Function

        Public Shared Function GetTreeView(ByVal memberID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " g.fldDate >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " g.fldDate <= @dateTo AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.fldID,a.fldName,a.fldCode,a.fldDateJoin,a.fldSponsorID, ifnull(p.fldPackage,'None') as fldPackage, ifNull(g.fldGPV,0) As fldGPV, ifNull(g.fldPV,0) As fldPV, " _
                                                             & " (SELECT COUNT(*) from tblmembership b where b.fldSponsorID = a.fldID) as tDownline " _
                                                             & " From tblmembership a " _
                                                             & " Left Join tblpackage p On p.fldID = a.fldRank " _
                                                             & " Left Join (Select fldMID, Sum(fldGPV) As fldGPV, Sum(fldPV) As fldPV From tblgroupsales " & query & " Group By fldMID) g On g.fldMID = a.fldID " _
                                                             & " Where a.fldSponsorID = @fldSponsorID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldSponsorID", memberID)
            myCommand.CommandTimeout = 120
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetTreeView(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select a.fldID,a.fldName,a.fldCode,a.fldDateJoin,a.fldSponsorID,(SELECT COUNT(*) from tblmembership b where b.fldSponsorID = a.fldID) as tDownline, (SELECT ifNull(Sum(fldGPV),0) From tblgroupsales where fldMID = a.fldID And fldYM = Date_Format(Current_Date, '%Y%m')) as tGroupSales From tblmembership a", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.CommandTimeout = 120
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function SearchMemberList(ByVal MemberID As Long, ByVal PackageID As Long, ByVal SponsorID As Long, ByVal MemberType As Long, ByVal BankID As Long, ByVal MemberCode As String, ByVal Name As String, _
                                                ByVal ICNo As String, ByVal CountryID As String, ByVal State As String, ByVal Status As String, ByVal Activate As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = Nothing
            If Not MemberID <= 0 Then query &= " M.fldID = @fldID And "
            If Not PackageID <= 0 Then query &= " M.fldRank = @fldRank And "
            If Not SponsorID <= 0 Then query &= " M.fldSponsorID = @fldSponsorID And "
            If Not MemberType <= 0 Then query &= " M.fldType = @fldType And "
            If Not BankID <= 0 Then query &= " M.fldBankID = @fldBankID And "
            If Not String.IsNullOrEmpty(MemberCode) Then query &= " M.fldCode Like @fldCode And "
            If Not String.IsNullOrEmpty(Name) Then query &= " M.fldName Like @fldName AND "
            If Not String.IsNullOrEmpty(ICNo) Then query &= " M.fldICNo Like @fldICNo AND "
            If Not String.IsNullOrEmpty(CountryID) Then query &= " M.fldCountryID = @fldCountryID AND "
            If Not String.IsNullOrEmpty(State) Then query &= " M.fldState = @fldState AND "
            If Not String.IsNullOrEmpty(Status) Then query &= " M.fldStatus = @fldStatus AND "
            If Not String.IsNullOrEmpty(Activate) Then query &= " M.fldActivate = @fldActivate AND "
            If Not String.IsNullOrEmpty(DateFrom) Then query &= " M.fldDateJoin >= @DateFrom AND "
            If Not String.IsNullOrEmpty(DateTo) Then query &= " M.fldDateJoin <= @DateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select M.*, IfNull(S.fldCode,'') As fldSponsorCode, ifNull(P.fldPackage,'') As fldPackage, C.fldName As fldCountry, T.fldRoleName From tblmembership M Left Join tblmembership S On S.fldID = M.fldSponsorID Left Join tblpackage P On P.fldID = M.fldRank Left Join tblcountry C On C.fldID = M.fldCountryID Left Join tblmembertype T On t.fldRoleID = M.fldType " & query & " Order By fldCode", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", MemberID)
            myCommand.Parameters.AddWithValue("@fldRank", PackageID)
            myCommand.Parameters.AddWithValue("@fldSponsorID", SponsorID)
            myCommand.Parameters.AddWithValue("@fldType", MemberType)
            myCommand.Parameters.AddWithValue("@fldBankID", BankID)
            myCommand.Parameters.AddWithValue("@fldCode", "%" & MemberCode & "%")
            myCommand.Parameters.AddWithValue("@fldName", "%" & Name & "%")
            myCommand.Parameters.AddWithValue("@fldICNo", "%" & ICNo & "%")
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            myCommand.Parameters.AddWithValue("@fldState", State)
            myCommand.Parameters.AddWithValue("@fldStatus", Status)
            myCommand.Parameters.AddWithValue("@fldActivate", Activate)
            myCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
            myCommand.Parameters.AddWithValue("@DateTo", DateTo)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Sub SearchMemberListWithPaging(ByVal MemberID As Long, ByVal PackageID As Long, ByVal SponsorID As Long, ByVal MemberType As Long, ByVal BankID As Long, ByVal MemberCode As String, ByVal Name As String, _
                                                ByVal ICNo As String, ByVal CountryID As String, ByVal Status As String, ByVal DateFrom As String, ByVal DateTo As String, ByVal PageNum As Integer, ByVal PageSize As Integer, ByRef PageCount As Integer, ByRef myDataTable As DataTable, ByVal myConnection As MySqlConnection)
            Dim query As String = Nothing
            Dim limiter As String = Nothing
            Dim recordCount As Integer = 0
            'Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand()
            myDataTable = New DataTable
            If Not MemberID <= 0 Then query &= " M.fldID = @fldID And "
            If Not PackageID <= 0 Then query &= " M.fldRank = @fldRank And "
            If Not SponsorID <= 0 Then query &= " M.fldSponsorID = @fldSponsorID And "
            If Not MemberType <= 0 Then query &= " M.fldType = @fldType And "
            If Not BankID <= 0 Then query &= " M.fldBankID = @fldBankID And "
            If Not String.IsNullOrEmpty(MemberCode) Then query &= " M.fldCode Like @fldCode And "
            If Not String.IsNullOrEmpty(Name) Then query &= " M.fldName Like @fldName AND "
            If Not String.IsNullOrEmpty(ICNo) Then query &= " M.fldICNo Like @fldICNo AND "
            If Not String.IsNullOrEmpty(CountryID) Then query &= " M.fldCountryID = @fldCountryID AND "
            If Not String.IsNullOrEmpty(Status) Then query &= " M.fldStatus = @fldStatus AND "
            If Not String.IsNullOrEmpty(DateFrom) Then query &= " M.fldDateJoin >= @DateFrom AND "
            If Not String.IsNullOrEmpty(DateTo) Then query &= " M.fldDateJoin <= @DateTo AND "
            If Not query Is Nothing Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            If Not PageSize = 0 Then limiter &= " Limit " & (PageNum - 1) * PageSize & "," & PageSize
            myCommand.Connection = myConnection
            myCommand.Parameters.AddWithValue("@fldID", MemberID)
            myCommand.Parameters.AddWithValue("@fldRank", PackageID)
            myCommand.Parameters.AddWithValue("@fldSponsorID", SponsorID)
            myCommand.Parameters.AddWithValue("@fldType", MemberType)
            myCommand.Parameters.AddWithValue("@fldBankID", BankID)
            myCommand.Parameters.AddWithValue("@fldCode", "%" & MemberCode & "%")
            myCommand.Parameters.AddWithValue("@fldName", "%" & Name & "%")
            myCommand.Parameters.AddWithValue("@fldICNo", "%" & ICNo & "%")
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            myCommand.Parameters.AddWithValue("@fldStatus", Status)
            myCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
            myCommand.Parameters.AddWithValue("@DateTo", DateTo)
            myCommand.CommandText = "Select Count(*) From tblmembership M " & query
            recordCount = myCommand.ExecuteScalar()
            If recordCount < PageSize Then
                PageCount = 1
            ElseIf recordCount > PageSize Then
                PageCount = recordCount / If(PageSize <= 0, 1, PageSize)
            End If
            myCommand.CommandText = "Select M.*, P.fldPackage, C.fldName As fldCountry From tblmembership M Left Join tblpackage P On P.fldID = M.fldRank Left Join tblcountry C On C.fldID = M.fldCountryID " & query & " Order By fldCode" & limiter
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
        End Sub

        Public Shared Function VerifySponsorMember(ByVal DownlineMID As Long, ByVal UplineMID As Long, ByVal myConnection As MySqlConnection) As Boolean
            If DownlineMID = 0 Then
                Return False
            ElseIf DownlineMID = UplineMID Then
                Return True
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select ifnull(fldSponsorID,0) from tblmembership where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", DownlineMID)
            DownlineMID = myCommand.ExecuteScalar()
            Return VerifySponsorMember(DownlineMID, UplineMID, myConnection)
        End Function

        Public Shared Function VerifyDuplicateICNum(ByVal icnum As String, ByVal rank As Long, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select count(*) From tblmembership Where fldICNo = @fldICNo And fldRank = @fldRank", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldICNo", icnum)
            myCommand.Parameters.AddWithValue("@fldRank", rank)
            result = myCommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function VerifyAccountStatus(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldStatus From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            result = myCommand.ExecuteScalar.Equals("A")
            Return result
        End Function

        Public Shared Function VerifyActivation(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Boolean = False
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldActivate From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            result = myCommand.ExecuteScalar.Equals("Y")
            Return result
        End Function

        Public Shared Function VerifyMemberCode(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID From tblmembership Where fldCode = @fldCode", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCode", memberCode)
            result = myCommand.ExecuteScalar
            Return result
        End Function

#End Region

#Region "INSERT, UPDATE, DELETE"

        Public Shared Function UpdateSponsorLink(ByVal memberID As Long, ByVal link As String, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldSponsorLink = @fldSponsorLink Where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@fldSponsorLink", link)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpdateAccountStatus(ByVal memberID As Long, ByVal currentStatus As String, ByVal newStatus As String, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldStatus = @newStatus Where fldID = @fldID And fldStatus = @currentStatus ", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@currentStatus", currentStatus)
            myCommand.Parameters.AddWithValue("@newStatus", newStatus)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpdateActivateStatus(ByVal memberID As Long, ByVal currentStatus As String, ByVal newStatus As String, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldActivate = @newStatus Where fldID = @fldID And fldActivate = @currentStatus ", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@currentStatus", currentStatus)
            myCommand.Parameters.AddWithValue("@newStatus", newStatus)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function VoidAccount(ByVal memberID As Long, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("UPDATE tblmembership SET fldCode = CONCAT(fldCode,'_DELETED','_',FLOOR(RAND()*999)), fldStatus = 'D' WHERE fldID = @fldID AND fldStatus = 'A'", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function ActivateMember(ByVal memberID As Long, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldActivate = 'Y' Where fldID = @fldID And fldActivate = 'N' ", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpdateMemberRank(ByVal memberID As Long, ByVal Rank As Long, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldRank = @fldRank Where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@fldRank", Rank)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpgradeMemberRank(ByVal memberID As Long, ByVal packageID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldRank = @packageID Where fldID = @fldID and fldRank < @packageID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@packageID", packageID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpdateMemberCode(ByVal memberID As Long, ByVal memberCode As String, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldCode = @fldCode Where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@fldCode", memberCode)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function Maintenance(ByVal memberID As Long, ByVal ExpiryDate As Date, myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldExpiryDate = @fldExpiryDate Where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@fldExpiryDate", ExpiryDate)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function ChangeLoginPassword(ByVal fldID As Long, ByVal fldPassword As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblmembership Set fldPassword = @fldPassword Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            myCommand.Parameters.AddWithValue("@fldPassword", fldPassword)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function ChangeSecurityPassword(ByVal fldID As Long, ByVal fldTransactionPassword As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblmembership Set fldTransactionPassword = @fldTransactionPassword Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            myCommand.Parameters.AddWithValue("@fldTransactionPassword", fldTransactionPassword)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function UpdateMemberType(ByVal memberID As Long, ByVal currentType As Integer, ByVal newType As Integer, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Update tblmembership Set fldType = @newType Where fldID = @fldID And fldType = @currentType", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", memberID)
            myCommand.Parameters.AddWithValue("@currentType", currentType)
            myCommand.Parameters.AddWithValue("@newType", newType)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function Save(ByVal myMembership As MembershipObj, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myMembership.fldID = Nothing Then
                processExe = "Insert into tblmembership (fldCode, fldDateJoin, fldExpiryDate, fldSponsorID, fldTempSponsorID, fldSponsorLevel, fldName, fldICNo, fldDOB, fldSex, fldRace, fldOccupation, fldAddress1, fldAddress2, fldCity, fldPostCode, fldState, fldCountryID, fldCountryZone, fldHouseNo, fldFaxNo, fldMobileNo, fldOfficeNo, fldEMail, fldLanguage, fldBankID, fldBankCity, fldBankState, fldBankCountry, fldBankName, fldBankBranch, fldBankSwiftCode, fldWalletAddrType, fldWalletAddress, fldAccNo, fldAccHolder, fldPassword, fldTransactionPassword, fldStatus, fldRank, fldType, fldLoginAttempt, fldTransactionAttempt, fldLastLoginAttempt, fldLastLogin, fldLastLogout, fldIPAdd, fldRegisterID, fldCreator, fldCreatorType, fldActivate, fldSponsorLink) Values (@fldCode, @fldDateJoin, @fldExpiryDate, @fldSponsorID, @fldTempSponsorID, @fldSponsorLevel, @fldName, @fldICNo, @fldDOB, @fldSex, @fldRace, @fldOccupation, @fldAddress1, @fldAddress2, @fldCity, @fldPostCode, @fldState, @fldCountryID, @fldCountryZone, @fldHouseNo, @fldFaxNo, @fldMobileNo, @fldOfficeNo, @fldEMail, @fldLanguage, @fldBankID, @fldBankCity, @fldBankState, @fldBankCountry, @fldBankName, @fldBankBranch, @fldBankSwiftCode, @fldWalletAddrType, @fldWalletAddress, @fldAccNo, @fldAccHolder, @fldPassword, @fldTransactionPassword, @fldStatus, @fldRank, @fldType, @fldLoginAttempt, @fldTransactionAttempt, @fldLastLoginAttempt, @fldLastLogin, @fldLastLogout, @fldIPAdd, @fldRegisterID, @fldCreator, @fldCreatorType, @fldActivate, @fldSponsorLink)"
                isInsert = True
            Else
                processExe = "Update tblmembership set fldCode = @fldCode, fldDateJoin = @fldDateJoin, fldExpiryDate = @fldExpiryDate, fldSponsorID = @fldSponsorID, fldTempSponsorID = @fldTempSponsorID, fldSponsorLevel = @fldSponsorLevel, fldName = @fldName, fldICNo = @fldICNo, fldDOB = @fldDOB, fldSex = @fldSex, fldRace = @fldRace, fldOccupation = @fldOccupation, fldAddress1 = @fldAddress1, fldAddress2 = @fldAddress2, fldCity = @fldCity, fldPostCode = @fldPostCode, fldState = @fldState, fldCountryID = @fldCountryID, fldCountryZone = @fldCountryZone, fldHouseNo = @fldHouseNo, fldFaxNo = @fldFaxNo, fldMobileNo = @fldMobileNo, fldOfficeNo = @fldOfficeNo, fldEMail = @fldEMail, fldLanguage = @fldLanguage, fldBankID = @fldBankID, fldBankCity = @fldBankCity, fldBankState = @fldBankState, fldBankCountry = @fldBankCountry, fldBankName = @fldBankName, fldBankBranch = @fldBankBranch, fldBankSwiftCode = @fldBankSwiftCode, fldWalletAddrType = @fldWalletAddrType, fldWalletAddress = @fldWalletAddress, fldAccNo = @fldAccNo, fldAccHolder = @fldAccHolder, fldPassword = @fldPassword, fldTransactionPassword = @fldTransactionPassword, fldStatus = @fldStatus, fldRank = @fldRank, fldType = @fldType, fldLoginAttempt = @fldLoginAttempt, fldTransactionAttempt = @fldTransactionAttempt, fldLastLoginAttempt = @fldLastLoginAttempt, fldLastLogin = @fldLastLogin, fldLastLogout = @fldLastLogout, fldIPAdd = @fldIPAdd, fldRegisterID = @fldRegisterID, fldActivate = @fldActivate, fldSponsorLink=@fldSponsorLink Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", myMembership.fldID)

            myCommand.Parameters.AddWithValue("@fldCode", myMembership.fldCode)
            myCommand.Parameters.AddWithValue("@fldDateJoin", myMembership.fldDateJoin)
            myCommand.Parameters.AddWithValue("@fldExpiryDate", myMembership.fldExpiryDate)
            myCommand.Parameters.AddWithValue("@fldSponsorID", myMembership.fldSponsorID)
            myCommand.Parameters.AddWithValue("@fldTempSponsorID", myMembership.fldTempSponsorID)
            myCommand.Parameters.AddWithValue("@fldSponsorLevel", myMembership.fldSponsorLevel)
            myCommand.Parameters.AddWithValue("@fldName", myMembership.fldName)
            myCommand.Parameters.AddWithValue("@fldICNo", myMembership.fldICNo)
            myCommand.Parameters.AddWithValue("@fldDOB", myMembership.fldDOB)
            myCommand.Parameters.AddWithValue("@fldSex", myMembership.fldSex)
            myCommand.Parameters.AddWithValue("@fldRace", myMembership.fldRace)
            myCommand.Parameters.AddWithValue("@fldOccupation", myMembership.fldOccupation)
            myCommand.Parameters.AddWithValue("@fldAddress1", myMembership.fldAddress1)
            myCommand.Parameters.AddWithValue("@fldAddress2", myMembership.fldAddress2)
            myCommand.Parameters.AddWithValue("@fldCity", myMembership.fldCity)
            myCommand.Parameters.AddWithValue("@fldPostCode", myMembership.fldPostCode)
            myCommand.Parameters.AddWithValue("@fldState", myMembership.fldState)
            myCommand.Parameters.AddWithValue("@fldCountryID", myMembership.fldCountryID)
            myCommand.Parameters.AddWithValue("@fldCountryZone", myMembership.fldCountryZone)
            myCommand.Parameters.AddWithValue("@fldHouseNo", myMembership.fldHouseNo)
            myCommand.Parameters.AddWithValue("@fldFaxNo", myMembership.fldFaxNo)
            myCommand.Parameters.AddWithValue("@fldMobileNo", myMembership.fldMobileNo)
            myCommand.Parameters.AddWithValue("@fldOfficeNo", myMembership.fldOfficeNo)
            myCommand.Parameters.AddWithValue("@fldEMail", myMembership.fldEMail)
            myCommand.Parameters.AddWithValue("@fldLanguage", myMembership.fldLanguage)
            myCommand.Parameters.AddWithValue("@fldBankID", myMembership.fldBankID)
            myCommand.Parameters.AddWithValue("@fldBankCity", myMembership.fldBankCity)
            myCommand.Parameters.AddWithValue("@fldBankState", myMembership.fldBankState)
            myCommand.Parameters.AddWithValue("@fldBankCountry", myMembership.fldBankCountry)
            myCommand.Parameters.AddWithValue("@fldBankName", myMembership.fldBankName)
            myCommand.Parameters.AddWithValue("@fldBankBranch", myMembership.fldBankBranch)
            myCommand.Parameters.AddWithValue("@fldBankSwiftCode", myMembership.fldBankSwiftCode)
            myCommand.Parameters.AddWithValue("@fldWalletAddrType", myMembership.fldWalletAddrType)
            myCommand.Parameters.AddWithValue("@fldWalletAddress", myMembership.fldWalletAddress)
            myCommand.Parameters.AddWithValue("@fldAccNo", myMembership.fldAccNo)
            myCommand.Parameters.AddWithValue("@fldAccHolder", myMembership.fldAccHolder)
            myCommand.Parameters.AddWithValue("@fldPassword", myMembership.fldPassword)
            myCommand.Parameters.AddWithValue("@fldTransactionPassword", myMembership.fldTransactionPassword)
            myCommand.Parameters.AddWithValue("@fldStatus", myMembership.fldStatus)
            myCommand.Parameters.AddWithValue("@fldRank", myMembership.fldRank)
            myCommand.Parameters.AddWithValue("@fldType", myMembership.fldType)
            'myCommand.Parameters.AddWithValue("@fldRWallet", myMembership.fldRWallet)
            'myCommand.Parameters.AddWithValue("@fldCWallet", myMembership.fldCWallet)
            'myCommand.Parameters.AddWithValue("@fldSWallet", myMembership.fldSWallet)
            'myCommand.Parameters.AddWithValue("@fldEWallet", myMembership.fldEWallet)
            'myCommand.Parameters.AddWithValue("@fldShare", myMembership.fldShare)
            myCommand.Parameters.AddWithValue("@fldLoginAttempt", myMembership.fldLoginAttempt)
            myCommand.Parameters.AddWithValue("@fldTransactionAttempt", myMembership.fldTransactionAttempt)
            myCommand.Parameters.AddWithValue("@fldLastLoginAttempt", myMembership.fldLastLoginAttempt)
            myCommand.Parameters.AddWithValue("@fldLastLogin", myMembership.fldLastLogin)
            myCommand.Parameters.AddWithValue("@fldLastLogout", myMembership.fldLastLogout)
            myCommand.Parameters.AddWithValue("@fldIPAdd", myMembership.fldIPAdd)
            myCommand.Parameters.AddWithValue("@fldRegisterID", myMembership.fldRegisterID)
            myCommand.Parameters.AddWithValue("@fldCreator", myMembership.fldCreator)
            myCommand.Parameters.AddWithValue("@fldCreatorType", myMembership.fldCreatorType)
            myCommand.Parameters.AddWithValue("@fldActivate", myMembership.fldActivate)
            myCommand.Parameters.AddWithValue("@fldSponsorLink", myMembership.fldSponsorLink)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblmembership Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

#Region "Unused Code"

        'Public Shared Function GetTotalRWalletBalance(ByVal myConnection As MySqlConnection) As Decimal
        '    Return MySqlHelper.ExecuteScalar(myConnection, "Select Sum(fldRWallet) From tblmembership")
        'End Function

        'Public Shared Function GetTotalCWalletBalance(ByVal myConnection As MySqlConnection) As Decimal
        '    Return MySqlHelper.ExecuteScalar(myConnection, "Select Sum(fldCWallet) From tblmembership")
        'End Function

        'Public Shared Function GetTotalSWalletBalance(ByVal myConnection As MySqlConnection) As Decimal
        '    Return MySqlHelper.ExecuteScalar(myConnection, "Select Sum(fldSWallet) From tblmembership")
        'End Function

        'Public Shared Function GetTotalEWalletBalance(ByVal myConnection As MySqlConnection) As Decimal
        '    Return MySqlHelper.ExecuteScalar(myConnection, "Select Sum(fldEWallet) From tblmembership")
        'End Function

        'Public Shared Function GetRWalletBalance(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldRWallet From tblmembership Where fldID = @fldID", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetCWalletBalance(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldCWallet From tblmembership Where fldID = @fldID", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetSWalletBalance(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldSWallet From tblmembership Where fldID = @fldID", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetEWalletBalance(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldEWallet From tblmembership Where fldID = @fldID", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetRWalletBalance(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldRWallet From tblmembership Where fldCode = @fldCode", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldCode", memberCode)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetCWalletBalance(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldCWallet From tblmembership Where fldCode = @fldCode", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldCode", memberCode)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetSWalletBalance(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldSWallet From tblmembership Where fldCode = @fldCode", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldCode", memberCode)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function GetEWalletBalance(ByVal memberCode As String, ByVal myConnection As MySqlConnection) As Decimal
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Select fldEWallet From tblmembership Where fldCode = @fldCode", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldCode", memberCode)
        '    Return myCommand.ExecuteScalar
        'End Function

        'Public Shared Function AdjustRWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    myCommand = New MySqlCommand("Update tblmembership Set fldRWallet = @amount Where fldID = @fldID", myConnection)
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustCWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = @amount Where fldID = @fldID", myConnection)
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustSWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    myCommand = New MySqlCommand("Update tblmembership Set fldSWallet = @amount Where fldID = @fldID", myConnection)
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustEWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    myCommand = New MySqlCommand("Update tblmembership Set fldEWallet = @amount Where fldID = @fldID", myConnection)
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustRWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldRWallet = fldRWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldRWallet = fldRWallet - @amount Where fldID = @fldID", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustCWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet - @amount Where fldID = @fldID", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustSWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldSWallet = fldSWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldSWallet = fldSWallet - @amount Where fldID = @fldID", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function AdjustEWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldEWallet = fldEWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldEWallet = fldEWallet - @amount Where fldID = @fldID", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function UpdateRWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldRWallet = fldRWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldRWallet = fldRWallet - @amount Where fldID = @fldID And fldRWallet >= @amount", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function UpdateCWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet - @amount Where fldID = @fldID And fldCWallet >= @amount", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function UpdateSWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldSWallet = fldSWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldSWallet = fldSWallet - @amount Where fldID = @fldID And fldSWallet >= @amount", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function UpdateEWallet(ByVal memberID As Long, ByVal amount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldEWallet = fldEWallet + @amount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldEWallet = fldEWallet - @amount Where fldID = @fldID And fldEWallet >= @amount", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@amount", amount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function UpdateCSWallet(ByVal memberID As Long, ByVal CWalletAmount As Decimal, ByVal SWalletAmount As Decimal, ByVal operation As String, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = Nothing
        '    If operation.Equals("+") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet + @CWalletAmount, fldSWallet = fldSWallet + @SWalletAmount Where fldID = @fldID", myConnection)
        '    ElseIf operation.Equals("-") Then
        '        myCommand = New MySqlCommand("Update tblmembership Set fldCWallet = fldCWallet - @CWalletAmount, fldSWallet = fldSWallet - @SWalletAmount Where fldID = @fldID And fldCWallet >= @CWalletAmount And fldSWallet >= @SWalletAmount", myConnection)
        '    End If
        '    myCommand.Parameters.AddWithValue("@CWalletAmount", CWalletAmount)
        '    myCommand.Parameters.AddWithValue("@SWalletAmount", SWalletAmount)
        '    myCommand.Parameters.AddWithValue("@fldID", memberID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

        'Public Shared Function SearchMemberList(ByVal MemberCode As String, ByVal Name As String, ByVal ICNo As String, ByVal Sales As String, ByVal OrderBy As String, ByVal Desc As Boolean, ByVal Offset As Integer, ByVal Limit As Integer, ByVal pager As PagingObj, ByVal myConnection As MySqlConnection) As PagingObj
        '    Dim myDataTable As DataTable = New DataTable()
        '    Dim query As String = Nothing
        '    Dim limiter As String = Nothing
        '    Dim myCommand As MySqlCommand = Nothing
        '    Dim adapter As MySqlDataAdapter = Nothing
        '    If Not String.IsNullOrEmpty(Sales) Then
        '        query &= " M.fldSales = @fldSales AND "
        '    End If
        '    If Not String.IsNullOrEmpty(MemberCode) Then
        '        query &= " M.fldCode Like @fldCode And "
        '    End If
        '    If Not String.IsNullOrEmpty(Name) Then
        '        query &= " M.fldName Like @fldName AND "
        '    End If
        '    If Not String.IsNullOrEmpty(ICNo) Then
        '        query &= " M.fldICNo Like @fldICNo AND "
        '    End If
        '    If Not String.IsNullOrEmpty(ICNo) Then
        '        query &= " M.fldICNo Like @fldICNo AND "
        '    End If
        '    If Not query Is Nothing Then
        '        query = " Where " & query
        '        query = query.Substring(0, query.Length - 4)
        '    End If
        '    If Not String.IsNullOrEmpty(OrderBy) Then
        '        query = query & " Order By " & OrderBy
        '        If Desc Then
        '            query = query & " Desc "
        '        End If
        '    Else
        '        query = query & " Order By fldID"
        '    End If
        '    If Not Limit = 0 Then
        '        limiter &= " Limit " & Offset & "," & Limit
        '    End If
        '    myCommand = New MySqlCommand("Select Count(fldID) From tblmembership M " & query, myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldSales", Sales)
        '    myCommand.Parameters.AddWithValue("@fldCode", "%" & MemberCode & "%")
        '    myCommand.Parameters.AddWithValue("@fldName", "%" & Name & "%")
        '    myCommand.Parameters.AddWithValue("@fldICNo", "%" & ICNo & "%")
        '    pager.RecordCount = myCommand.ExecuteScalar()
        '    myCommand = New MySqlCommand("Select M.*, P.fldPackage From tblmembership M Left Join tblpackage P On P.fldID = M.fldRank " & query & limiter, myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldSales", Sales)
        '    myCommand.Parameters.AddWithValue("@fldCode", "%" & MemberCode & "%")
        '    myCommand.Parameters.AddWithValue("@fldName", "%" & Name & "%")
        '    myCommand.Parameters.AddWithValue("@fldICNo", "%" & ICNo & "%")
        '    adapter = New MySqlDataAdapter(myCommand)
        '    adapter.Fill(myDataTable)
        '    pager.DataTable = myDataTable
        '    Return pager
        'End Function

#End Region

#Region "Private Methods"

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As MembershipObj
            Dim myMembership As MembershipObj = New MembershipObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                myMembership.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCode"))) Then
                myMembership.fldCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateJoin"))) Then
                myMembership.fldDateJoin = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateJoin"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldExpiryDate"))) Then
                myMembership.fldExpiryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldExpiryDate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSponsorID"))) Then
                myMembership.fldSponsorID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldSponsorID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldTempSponsorID"))) Then
                myMembership.fldTempSponsorID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldTempSponsorID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSponsorLevel"))) Then
                myMembership.fldSponsorLevel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldSponsorLevel"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldName"))) Then
                myMembership.fldName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldICNo"))) Then
                myMembership.fldICNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldICNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDOB"))) Then
                myMembership.fldDOB = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDOB"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSex"))) Then
                myMembership.fldSex = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSex"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRace"))) Then
                myMembership.fldRace = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRace"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOccupation"))) Then
                myMembership.fldOccupation = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOccupation"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAddress1"))) Then
                myMembership.fldAddress1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAddress1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAddress2"))) Then
                myMembership.fldAddress2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAddress2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCity"))) Then
                myMembership.fldCity = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCity"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPostCode"))) Then
                myMembership.fldPostCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPostCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldState"))) Then
                myMembership.fldState = myDataRecord.GetString(myDataRecord.GetOrdinal("fldState"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCountryID"))) Then
                myMembership.fldCountryID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCountryID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCountryZone"))) Then
                myMembership.fldCountryZone = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCountryZone"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldHouseNo"))) Then
                myMembership.fldHouseNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldHouseNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldFaxNo"))) Then
                myMembership.fldFaxNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldFaxNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMobileNo"))) Then
                myMembership.fldMobileNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMobileNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldOfficeNo"))) Then
                myMembership.fldOfficeNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldOfficeNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldEMail"))) Then
                myMembership.fldEMail = myDataRecord.GetString(myDataRecord.GetOrdinal("fldEMail"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLanguage"))) Then
                myMembership.fldLanguage = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLanguage"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankID"))) Then
                myMembership.fldBankID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldBankID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankCity"))) Then
                myMembership.fldBankCity = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankCity"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankState"))) Then
                myMembership.fldBankState = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankState"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankCountry"))) Then
                myMembership.fldBankCountry = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankCountry"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankName"))) Then
                myMembership.fldBankName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankBranch"))) Then
                myMembership.fldBankBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankBranch"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBankSwiftCode"))) Then
                myMembership.fldBankSwiftCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBankSwiftCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldWalletAddrType"))) Then
                myMembership.fldWalletAddrType = myDataRecord.GetString(myDataRecord.GetOrdinal("fldWalletAddrType"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldWalletAddress"))) Then
                myMembership.fldWalletAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("fldWalletAddress"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAccNo"))) Then
                myMembership.fldAccNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAccNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldAccHolder"))) Then
                myMembership.fldAccHolder = myDataRecord.GetString(myDataRecord.GetOrdinal("fldAccHolder"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPassword"))) Then
                myMembership.fldPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPassword"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldTransactionPassword"))) Then
                myMembership.fldTransactionPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("fldTransactionPassword"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myMembership.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRank"))) Then
                myMembership.fldRank = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldRank"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldType"))) Then
                myMembership.fldType = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldType"))
            End If
            'If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRWallet"))) Then
            '    myMembership.fldRWallet = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("fldRWallet"))
            'End If
            'If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCWallet"))) Then
            '    myMembership.fldCWallet = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("fldCWallet"))
            'End If
            'If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSWallet"))) Then
            '    myMembership.fldSWallet = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("fldSWallet"))
            'End If
            'If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldEWallet"))) Then
            '    myMembership.fldEWallet = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("fldEWallet"))
            'End If
            'If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldShare"))) Then
            '    myMembership.fldShare = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("fldShare"))
            'End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLoginAttempt"))) Then
                myMembership.fldLoginAttempt = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldLoginAttempt"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldTransactionAttempt"))) Then
                myMembership.fldTransactionAttempt = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldTransactionAttempt"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLoginAttempt"))) Then
                myMembership.fldLastLoginAttempt = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLoginAttempt"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLogin"))) Then
                myMembership.fldLastLogin = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLogin"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLastLogout"))) Then
                myMembership.fldLastLogout = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldLastLogout"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldIPAdd"))) Then
                myMembership.fldIPAdd = myDataRecord.GetString(myDataRecord.GetOrdinal("fldIPAdd"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRegisterID"))) Then
                myMembership.fldRegisterID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRegisterID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreator"))) Then
                myMembership.fldCreator = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldCreator"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorType"))) Then
                myMembership.fldCreatorType = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCreatorType"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldActivate"))) Then
                myMembership.fldActivate = myDataRecord.GetString(myDataRecord.GetOrdinal("fldActivate"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSponsorLink"))) Then
                myMembership.fldSponsorLink = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSponsorLink"))
            End If
            Return myMembership
        End Function

#End Region

    End Class

 End NameSpace 
