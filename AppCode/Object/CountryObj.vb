Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

Namespace BusinessObject

    Public Class CountryObj

#Region "Private Variables"
        Private _fldID As String = String.Empty
        Private _fldName As String = String.Empty
        Private _fldStatus As String = String.Empty
        Private _fldHQ As String = String.Empty
        Private _fldPhoneCode As String = String.Empty
        Private _fldCurrency As String = String.Empty

#End Region

#Region "Public Properties"
        Public Property fldID() As String
            Get
                Return _fldID
            End Get
            Set(value As String)
                _fldID = Value
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
        Public Property fldStatus() As String
            Get
                Return _fldStatus
            End Get
            Set(value As String)
                _fldStatus = Value
            End Set
        End Property
        Public Property fldHQ() As String
            Get
                Return _fldHQ
            End Get
            Set(value As String)
                _fldHQ = Value
            End Set
        End Property
        Public Property fldPhoneCode() As String
            Get
                Return _fldPhoneCode
            End Get
            Set(value As String)
                _fldPhoneCode = value
            End Set
        End Property
        Public Property fldCurrency() As String
            Get
                Return _fldCurrency
            End Get
            Set(value As String)
                _fldCurrency = value
            End Set
        End Property

#End Region

    End Class
End Namespace
