Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

	Public Class EMDDeviceObj

#Region "Private Variables"
		Private _fldID As Long
		Private _fldDateTime As DateTime = DateTime.MinValue
		Private _fldName As String = String.Empty
		Private _fldImei As String = String.Empty
		Private _fldSimNo As String = String.Empty
		Private _fldSimNo2 As String = String.Empty
		Private _fldLastPing As DateTime = DateTime.MinValue
		Private _fldLong As String = String.Empty
		Private _fldLat As String = String.Empty
		Private _fldStatus As String = String.Empty

#End Region

#Region "Public Properties"
		Public Property fldID() As Long
			Get
				Return _fldID
			End Get
			Set
				_fldID = Value
			End Set
		End Property
		Public Property fldDateTime() As DateTime
			Get
				Return _fldDateTime
			End Get
			Set
				_fldDateTime = Value
			End Set
		End Property
		Public Property fldName() As String
			Get
				Return _fldName
			End Get
			Set
				_fldName = Value
			End Set
		End Property
		Public Property fldImei() As String
			Get
				Return _fldImei
			End Get
			Set
				_fldImei = Value
			End Set
		End Property
		Public Property fldSimNo() As String
			Get
				Return _fldSimNo
			End Get
			Set
				_fldSimNo = Value
			End Set
		End Property
		Public Property fldLastPing() As DateTime
			Get
				Return _fldLastPing
			End Get
			Set
				_fldLastPing = Value
			End Set
		End Property
		Public Property fldLong() As String
			Get
				Return _fldLong
			End Get
			Set
				_fldLong = Value
			End Set
		End Property
		Public Property fldLat() As String
			Get
				Return _fldLat
			End Get
			Set
				_fldLat = Value
			End Set
		End Property
		Public Property fldStatus() As String
			Get
				Return _fldStatus
			End Get
			Set
				_fldStatus = Value
			End Set
		End Property

		Public Property fldSimNo2 As String
			Get
				Return _fldSimNo2
			End Get
			Set(value As String)
				_fldSimNo2 = value
			End Set
		End Property









#End Region

	End Class
End Namespace
