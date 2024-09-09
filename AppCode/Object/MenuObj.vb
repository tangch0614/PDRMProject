Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class MenuObj

#Region "Private Variables"
        Private _fldMenuID As Long
        Private _fldMenuPath As String = String.Empty
        Private _fldParentMenuID As Long
        Private _fldStatus As SByte
        Private _fldMenuOrder As Long
        Private _fldMenuTitleText As String = String.Empty
        Private _fldMenuTarget As String = String.Empty
        Private _fldDisplay As SByte

#End Region

#Region "Public Properties"
        Public Property fldMenuID() As Long
            Get
                Return _fldMenuID
            End Get
            Set(value As Long)
                _fldMenuID = Value
            End Set
        End Property
        Public Property fldMenuPath() As String
            Get
                Return _fldMenuPath
            End Get
            Set(value As String)
                _fldMenuPath = Value
            End Set
        End Property
        Public Property fldParentMenuID() As Long
            Get
                Return _fldParentMenuID
            End Get
            Set(value As Long)
                _fldParentMenuID = Value
            End Set
        End Property
        Public Property fldStatus() As SByte
            Get
                Return _fldStatus
            End Get
            Set(value As SByte)
                _fldStatus = Value
            End Set
        End Property
        Public Property fldMenuOrder() As Long
            Get
                Return _fldMenuOrder
            End Get
            Set(value As Long)
                _fldMenuOrder = Value
            End Set
        End Property
        Public Property fldMenuTitleText() As String
            Get
                Return _fldMenuTitleText
            End Get
            Set(value As String)
                _fldMenuTitleText = Value
            End Set
        End Property
        Public Property fldMenuTarget() As String
            Get
                Return _fldMenuTarget
            End Get
            Set(value As String)
                _fldMenuTarget = Value
            End Set
        End Property
        Public Property fldDisplay() As SByte
            Get
                Return _fldDisplay
            End Get
            Set(value As SByte)
                _fldDisplay = value
            End Set
        End Property








#End Region

    End Class
End Namespace
