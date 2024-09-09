Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class NewsObj

#Region "Private Variables"
        Private _fldID As Integer = 0
        Private _fldDateTime As DateTime = DateTime.MinValue
        Private _fldSubject As String = String.Empty
        Private _fldContent As String = String.Empty
        Private _fldCreatorID As Integer = 0
        Private _fldCreatorName As String = String.Empty
        Private _fldStatus As String = "Y"
        Private _fldLanguage As String = "EN-US"
        Private _fldPopup As Integer = 0

#End Region

#Region "Public Properties"
        Public Property fldID() As Integer
            Get
                Return _fldID
            End Get
            Set(value As Integer)
                _fldID = Value
            End Set
        End Property
        Public Property fldDateTime() As DateTime
            Get
                Return _fldDateTime
            End Get
            Set(value As DateTime)
                _fldDateTime = Value
            End Set
        End Property
        Public Property fldSubject() As String
            Get
                Return _fldSubject
            End Get
            Set(value As String)
                _fldSubject = Value
            End Set
        End Property
        Public Property fldContent() As String
            Get
                Return _fldContent
            End Get
            Set(value As String)
                _fldContent = Value
            End Set
        End Property
        Public Property fldCreatorID() As Integer
            Get
                Return _fldCreatorID
            End Get
            Set(value As Integer)
                _fldCreatorID = Value
            End Set
        End Property
        Public Property fldCreatorName() As String
            Get
                Return _fldCreatorName
            End Get
            Set(value As String)
                _fldCreatorName = Value
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
        Public Property fldLanguage() As String
            Get
                Return _fldLanguage
            End Get
            Set(value As String)
                _fldLanguage = value
            End Set
        End Property
        Public Property fldPopup() As Integer
            Get
                Return _fldPopup
            End Get
            Set(value As Integer)
                _fldPopup = value
            End Set
        End Property

#End Region

    End Class
End Namespace
