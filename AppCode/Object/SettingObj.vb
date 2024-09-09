Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class SettingObj

#Region "Private Variables"
        Private _fldID As Long
        Private _fldSetting As String = String.Empty
        Private _fldValue As String = String.Empty
        Private _fldDescription As String = String.Empty

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
        Public Property fldSetting() As String
            Get
                Return _fldSetting
            End Get
            Set(value As String)
                _fldSetting = Value
            End Set
        End Property
        Public Property fldValue() As String
            Get
                Return _fldValue
            End Get
            Set(value As String)
                _fldValue = Value
            End Set
        End Property
        Public Property fldDescription() As String
            Get
                Return _fldDescription
            End Get
            Set(value As String)
                _fldDescription = Value
            End Set
        End Property





#End Region

    End Class
End Namespace
