Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class AccountStatusHistObj

#Region "Private Variables"
        Private _fldID As Long
        Private _fldDateTime As DateTime = DateTime.MinValue
        Private _fldMID As Long
        Private _fldCurrentStatus As String = String.Empty
        Private _fldStatus As String = String.Empty
        Private _fldRemark As String = String.Empty
        Private _fldCreatorID As Long
        Private _fldCreatorType As String = String.Empty

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
        Public Property fldDateTime() As DateTime
            Get
                Return _fldDateTime
            End Get
            Set(value As DateTime)
                _fldDateTime = Value
            End Set
        End Property
        Public Property fldMID() As Long
            Get
                Return _fldMID
            End Get
            Set(value As Long)
                _fldMID = Value
            End Set
        End Property
        Public Property fldCurrentStatus() As String
            Get
                Return _fldCurrentStatus
            End Get
            Set(value As String)
                _fldCurrentStatus = Value
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
        Public Property fldRemark() As String
            Get
                Return _fldRemark
            End Get
            Set(value As String)
                _fldRemark = value
            End Set
        End Property
        Public Property fldCreatorID() As Long
            Get
                Return _fldCreatorID
            End Get
            Set(value As Long)
                _fldCreatorID = value
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









#End Region

    End Class
End Namespace
