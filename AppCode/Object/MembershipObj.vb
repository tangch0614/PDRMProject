Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class MembershipObj
#Region "Private Variables"
        Private _fldID As Long
        Private _fldCode As String = String.Empty
        Private _fldDateJoin As DateTime = DateTime.MinValue
        Private _fldExpiryDate As DateTime = DateTime.MinValue
        Private _fldSponsorID As Long
        Private _fldTempSponsorID As Long
        Private _fldSponsorLevel As Integer = 0
        Private _fldName As String = String.Empty
        Private _fldICNo As String = String.Empty
        Private _fldDOB As DateTime = DateTime.MinValue
        Private _fldSex As String = "M"
        Private _fldRace As String = "O"
        Private _fldOccupation As String = String.Empty
        Private _fldAddress1 As String = String.Empty
        Private _fldAddress2 As String = String.Empty
        Private _fldCity As String = String.Empty
        Private _fldPostCode As String = String.Empty
        Private _fldState As String = String.Empty
        Private _fldCountryID As String = String.Empty
        Private _fldCountryZone As String = "NONE"
        Private _fldHouseNo As String = String.Empty
        Private _fldFaxNo As String = String.Empty
        Private _fldMobileNo As String = String.Empty
        Private _fldOfficeNo As String = String.Empty
        Private _fldEMail As String = String.Empty
        Private _fldLanguage As String = "EN-US"
        Private _fldBankID As Integer = 0
        Private _fldBankCity As String = String.Empty
        Private _fldBankState As String = String.Empty
        Private _fldBankCountry As String = String.Empty
        Private _fldBankName As String = String.Empty
        Private _fldBankBranch As String = String.Empty
        Private _fldBankSwiftCode As String = String.Empty
        Private _fldWalletAddrType As String = String.Empty
        Private _fldWalletAddress As String = String.Empty
        Private _fldAccNo As String = String.Empty
        Private _fldAccHolder As String = String.Empty
        Private _fldPassword As String = String.Empty
        Private _fldTransactionPassword As String = String.Empty
        Private _fldStatus As String = String.Empty
        Private _fldRank As Integer = 0
        Private _fldType As Integer = 0
        Private _fldLoginAttempt As Integer = 0
        Private _fldTransactionAttempt As Integer = 0
        Private _fldLastLoginAttempt As DateTime = DateTime.MinValue
        Private _fldLastLogin As DateTime = DateTime.MinValue
        Private _fldLastLogout As DateTime = DateTime.MinValue
        Private _fldIPAdd As String = String.Empty
        Private _fldRegisterID As String = String.Empty
        Private _fldCreator As Long
        Private _fldCreatorType As String = String.Empty
        Private _fldActivate As String = "N"
        Private _fldSponsorLink As String = ""

#End Region

#Region "Public Properties"
        Public Property fldID() As Long
            Get
                Return _fldID
            End Get
            Set(value As Long)
                _fldID = Value
            End Set
        End Property
        Public Property fldCode() As String
            Get
                Return _fldCode
            End Get
            Set(value As String)
                _fldCode = Value
            End Set
        End Property
        Public Property fldDateJoin() As DateTime
            Get
                Return _fldDateJoin
            End Get
            Set(value As DateTime)
                _fldDateJoin = Value
            End Set
        End Property
        Public Property fldExpiryDate() As DateTime
            Get
                Return _fldExpiryDate
            End Get
            Set(value As DateTime)
                _fldExpiryDate = Value
            End Set
        End Property
        Public Property fldSponsorID() As Long
            Get
                Return _fldSponsorID
            End Get
            Set(value As Long)
                _fldSponsorID = Value
            End Set
        End Property
        Public Property fldTempSponsorID() As Long
            Get
                Return _fldTempSponsorID
            End Get
            Set(value As Long)
                _fldTempSponsorID = value
            End Set
        End Property
        Public Property fldSponsorLevel() As Integer
            Get
                Return _fldSponsorLevel
            End Get
            Set(value As Integer)
                _fldSponsorLevel = Value
            End Set
        End Property
        Public Property fldName() As String
            Get
                Return _fldName
            End Get
            Set(value As String)
                _fldName = Value
            End Set
        End Property
        Public Property fldICNo() As String
            Get
                Return _fldICNo
            End Get
            Set(value As String)
                _fldICNo = Value
            End Set
        End Property
        Public Property fldDOB() As DateTime
            Get
                Return _fldDOB
            End Get
            Set(value As DateTime)
                _fldDOB = Value
            End Set
        End Property
        Public Property fldSex() As String
            Get
                Return _fldSex
            End Get
            Set(value As String)
                _fldSex = Value
            End Set
        End Property
        Public Property fldRace() As String
            Get
                Return _fldRace
            End Get
            Set(value As String)
                _fldRace = Value
            End Set
        End Property
        Public Property fldOccupation() As String
            Get
                Return _fldOccupation
            End Get
            Set(value As String)
                _fldOccupation = Value
            End Set
        End Property
        Public Property fldAddress1() As String
            Get
                Return _fldAddress1
            End Get
            Set(value As String)
                _fldAddress1 = Value
            End Set
        End Property
        Public Property fldAddress2() As String
            Get
                Return _fldAddress2
            End Get
            Set(value As String)
                _fldAddress2 = Value
            End Set
        End Property
        Public Property fldCity() As String
            Get
                Return _fldCity
            End Get
            Set(value As String)
                _fldCity = Value
            End Set
        End Property
        Public Property fldPostCode() As String
            Get
                Return _fldPostCode
            End Get
            Set(value As String)
                _fldPostCode = Value
            End Set
        End Property
        Public Property fldState() As String
            Get
                Return _fldState
            End Get
            Set(value As String)
                _fldState = Value
            End Set
        End Property
        Public Property fldCountryID() As String
            Get
                Return _fldCountryID
            End Get
            Set(value As String)
                _fldCountryID = Value
            End Set
        End Property
        Public Property fldCountryZone() As String
            Get
                Return _fldCountryZone
            End Get
            Set(value As String)
                _fldCountryZone = value
            End Set
        End Property
        Public Property fldHouseNo() As String
            Get
                Return _fldHouseNo
            End Get
            Set(value As String)
                _fldHouseNo = Value
            End Set
        End Property
        Public Property fldFaxNo() As String
            Get
                Return _fldFaxNo
            End Get
            Set(value As String)
                _fldFaxNo = Value
            End Set
        End Property
        Public Property fldMobileNo() As String
            Get
                Return _fldMobileNo
            End Get
            Set(value As String)
                _fldMobileNo = Value
            End Set
        End Property
        Public Property fldOfficeNo() As String
            Get
                Return _fldOfficeNo
            End Get
            Set(value As String)
                _fldOfficeNo = Value
            End Set
        End Property
        Public Property fldEMail() As String
            Get
                Return _fldEMail
            End Get
            Set(value As String)
                _fldEMail = Value
            End Set
        End Property
        Public Property fldLanguage() As String
            Get
                Return _fldLanguage
            End Get
            Set(value As String)
                _fldLanguage = Value
            End Set
        End Property
        Public Property fldBankID() As Integer
            Get
                Return _fldBankID
            End Get
            Set(value As Integer)
                _fldBankID = Value
            End Set
        End Property
        Public Property fldBankCity() As String
            Get
                Return _fldBankCity
            End Get
            Set(value As String)
                _fldBankCity = Value
            End Set
        End Property
        Public Property fldBankState() As String
            Get
                Return _fldBankState
            End Get
            Set(value As String)
                _fldBankState = Value
            End Set
        End Property
        Public Property fldBankCountry() As String
            Get
                Return _fldBankCountry
            End Get
            Set(value As String)
                _fldBankCountry = Value
            End Set
        End Property
        Public Property fldBankName() As String
            Get
                Return _fldBankName
            End Get
            Set(value As String)
                _fldBankName = value
            End Set
        End Property
        Public Property fldBankBranch() As String
            Get
                Return _fldBankBranch
            End Get
            Set(value As String)
                _fldBankBranch = value
            End Set
        End Property
        Public Property fldBankSwiftCode() As String
            Get
                Return _fldBankSwiftCode
            End Get
            Set(value As String)
                _fldBankSwiftCode = value
            End Set
        End Property
        Public Property fldWalletAddrType() As String
            Get
                Return _fldWalletAddrType
            End Get
            Set(value As String)
                _fldWalletAddrType = value
            End Set
        End Property
        Public Property fldWalletAddress() As String
            Get
                Return _fldWalletAddress
            End Get
            Set(value As String)
                _fldWalletAddress = value
            End Set
        End Property
        Public Property fldAccNo() As String
            Get
                Return _fldAccNo
            End Get
            Set(value As String)
                _fldAccNo = value
            End Set
        End Property
        Public Property fldAccHolder() As String
            Get
                Return _fldAccHolder
            End Get
            Set(value As String)
                _fldAccHolder = value
            End Set
        End Property
        Public Property fldPassword() As String
            Get
                Return _fldPassword
            End Get
            Set(value As String)
                _fldPassword = value
            End Set
        End Property
        Public Property fldTransactionPassword() As String
            Get
                Return _fldTransactionPassword
            End Get
            Set(value As String)
                _fldTransactionPassword = value
            End Set
        End Property
        Public Property fldStatus() As String
            Get
                Return _fldStatus
            End Get
            Set(value As String)
                _fldStatus = value
            End Set
        End Property
        Public Property fldRank() As Integer
            Get
                Return _fldRank
            End Get
            Set(value As Integer)
                _fldRank = value
            End Set
        End Property
        Public Property fldType() As Integer
            Get
                Return _fldType
            End Get
            Set(value As Integer)
                _fldType = value
            End Set
        End Property
        Public Property fldLoginAttempt() As Integer
            Get
                Return _fldLoginAttempt
            End Get
            Set(value As Integer)
                _fldLoginAttempt = value
            End Set
        End Property
        Public Property fldTransactionAttempt() As Integer
            Get
                Return _fldTransactionAttempt
            End Get
            Set(value As Integer)
                _fldTransactionAttempt = value
            End Set
        End Property
        Public Property fldLastLoginAttempt() As DateTime
            Get
                Return _fldLastLoginAttempt
            End Get
            Set(value As DateTime)
                _fldLastLoginAttempt = value
            End Set
        End Property
        Public Property fldLastLogin() As DateTime
            Get
                Return _fldLastLogin
            End Get
            Set(value As DateTime)
                _fldLastLogin = value
            End Set
        End Property
        Public Property fldLastLogout() As DateTime
            Get
                Return _fldLastLogout
            End Get
            Set(value As DateTime)
                _fldLastLogout = value
            End Set
        End Property
        Public Property fldIPAdd() As String
            Get
                Return _fldIPAdd
            End Get
            Set(value As String)
                _fldIPAdd = value
            End Set
        End Property
        Public Property fldRegisterID() As String
            Get
                Return _fldRegisterID
            End Get
            Set(value As String)
                _fldRegisterID = value
            End Set
        End Property
        Public Property fldCreator() As Long
            Get
                Return _fldCreator
            End Get
            Set(value As Long)
                _fldCreator = value
            End Set
        End Property
        Public Property fldCreatorType() As String
            Get
                Return _fldCreatorType
            End Get
            Set(value As String)
                _fldCreatorType = value
            End Set
        End Property
        Public Property fldActivate() As String
            Get
                Return _fldActivate
            End Get
            Set(value As String)
                _fldActivate = value
            End Set
        End Property
        Public Property fldSponsorLink() As String
            Get
                Return _fldSponsorLink
            End Get
            Set(value As String)
                _fldSponsorLink = value
            End Set
        End Property

#End Region

    End Class
End Namespace
