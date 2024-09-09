Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class AdminObj

#Region "Private Variables"
        Private _fldID As Long
        Private _fldCode As String = String.Empty
        Private _fldName As String = String.Empty
        Private _fldICNo As String = String.Empty
        Private _fldPoliceNo As String = String.Empty
        Private _fldContactNo As String = String.Empty
        Private _fldDeptID As Long
        Private _fldPoliceStationID As Long
        Private _fldLanguage As String = String.Empty
        Private _fldCountryID As String = String.Empty
        Private _fldLevel As Integer = 0
        Private _fldPassword As String = String.Empty
        Private _fldTransactionPassword As String = String.Empty
        Private _fldStatus As String = String.Empty
        Private _fldLastLoginAttempt As DateTime = DateTime.MinValue
        Private _fldLastLogin As DateTime = DateTime.MinValue
        Private _fldLastLogout As DateTime = DateTime.MinValue
        Private _fldLoginAttempt As Integer = 0
        Private _fldCreatorDateTime As DateTime = DateTime.MinValue
        Private _fldCreator As Integer = 0

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
        Public Property fldLanguage() As String
            Get
                Return _fldLanguage
            End Get
            Set(value As String)
                _fldLanguage = Value
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
        Public Property fldLevel() As Integer
            Get
                Return _fldLevel
            End Get
            Set(value As Integer)
                _fldLevel = Value
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
        Public Property fldPassword() As String
            Get
                Return _fldPassword
            End Get
            Set(value As String)
                _fldPassword = Value
            End Set
        End Property
        Public Property fldStatus() As String
            Get
                Return _fldStatus
            End Get
            Set(value As String)
                _fldStatus = Value
            End Set
        End Property
        Public Property fldLastLoginAttempt() As DateTime
            Get
                Return _fldLastLoginAttempt
            End Get
            Set(value As DateTime)
                _fldLastLoginAttempt = Value
            End Set
        End Property
        Public Property fldLastLogin() As DateTime
            Get
                Return _fldLastLogin
            End Get
            Set(value As DateTime)
                _fldLastLogin = Value
            End Set
        End Property
        Public Property fldLastLogout() As DateTime
            Get
                Return _fldLastLogout
            End Get
            Set(value As DateTime)
                _fldLastLogout = Value
            End Set
        End Property
        Public Property fldLoginAttempt() As Integer
            Get
                Return _fldLoginAttempt
            End Get
            Set(value As Integer)
                _fldLoginAttempt = Value
            End Set
        End Property
        Public Property fldCreatorDateTime() As DateTime
            Get
                Return _fldCreatorDateTime
            End Get
            Set(value As DateTime)
                _fldCreatorDateTime = Value
            End Set
        End Property
        Public Property fldCreator() As Integer
            Get
                Return _fldCreator
            End Get
            Set(value As Integer)
                _fldCreator = Value
            End Set
        End Property

        Public Property fldPoliceNo As String
            Get
                Return _fldPoliceNo
            End Get
            Set(value As String)
                _fldPoliceNo = value
            End Set
        End Property

        Public Property fldContactNo As String
            Get
                Return _fldContactNo
            End Get
            Set(value As String)
                _fldContactNo = value
            End Set
        End Property

        Public Property fldDeptID As Long
            Get
                Return _fldDeptID
            End Get
            Set(value As Long)
                _fldDeptID = value
            End Set
        End Property

        Public Property fldPoliceStationID As Long
            Get
                Return _fldPoliceStationID
            End Get
            Set(value As Long)
                _fldPoliceStationID = value
            End Set
        End Property

#End Region

    End Class
End Namespace
