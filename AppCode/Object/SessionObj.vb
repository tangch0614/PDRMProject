Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class SessionObj

#Region "Private Variables"
        Private _fldSessionID As String = String.Empty
        Private _fldUserID As Long
        Private _fldUserType As String = String.Empty
        Private _fldLoginDateTime As DateTime = DateTime.MinValue
        Private _fldUserCode As String = String.Empty
        Private _fldIPAddress As String = String.Empty

#End Region

#Region "Public Properties"
        Public Property fldSessionID() As String
            Get
                Return _fldSessionID
            End Get
            Set(value As String)
                _fldSessionID = Value
            End Set
        End Property
        Public Property fldUserID() As Long
            Get
                Return _fldUserID
            End Get
            Set(value As Long)
                _fldUserID = Value
            End Set
        End Property
        Public Property fldUserType() As String
            Get
                Return _fldUserType
            End Get
            Set(value As String)
                _fldUserType = Value
            End Set
        End Property
        Public Property fldLoginDateTime() As DateTime
            Get
                Return _fldLoginDateTime
            End Get
            Set(value As DateTime)
                _fldLoginDateTime = Value
            End Set
        End Property
        Public Property fldUserCode() As String
            Get
                Return _fldUserCode
            End Get
            Set(value As String)
                _fldUserCode = Value
            End Set
        End Property
        Public Property fldIPAddress() As String
            Get
                Return _fldIPAddress
            End Get
            Set(value As String)
                _fldIPAddress = Value
            End Set
        End Property







#End Region

    End Class
End Namespace
