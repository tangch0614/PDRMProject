Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class SettingHistoryObj

#Region "Private Variables"
        Private _fldID As Long
        Private _fldDateTime As DateTime = DateTime.MinValue
        Private _fldSetting As Long
        Private _fldCurrentValue As String = String.Empty
        Private _fldNewValue As String = String.Empty
        Private _fldRemark As String = String.Empty
        Private _fldUserID As Long

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
        Public Property fldSetting() As Long
            Get
                Return _fldSetting
            End Get
            Set(value As Long)
                _fldSetting = value
            End Set
        End Property
        Public Property fldCurrentValue() As String
            Get
                Return _fldCurrentValue
            End Get
            Set(value As String)
                _fldCurrentValue = Value
            End Set
        End Property
        Public Property fldNewValue() As String
            Get
                Return _fldNewValue
            End Get
            Set(value As String)
                _fldNewValue = Value
            End Set
        End Property
        Public Property fldRemark() As String
            Get
                Return _fldRemark
            End Get
            Set(value As String)
                _fldRemark = Value
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








#End Region

    End Class
End Namespace
