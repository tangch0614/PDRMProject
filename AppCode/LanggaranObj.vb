Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

Namespace BusinessObject

    Public Class LanggaranObj

#Region "Private Variables"
		Private _fldID As Long
		Private _fldDateTime As DateTime = DateTime.MinValue
		Private _fldoppID As Long
		Private _fldoppName As String = String.Empty
		Private _fldoppContactNo As String = String.Empty
		Private _fldoppJabatan As String = String.Empty
		Private _fldoppBalai As String = String.Empty
		Private _fldPolisID As Long
		Private _fldemdID As Long
		Private _fldpolisNo As String = String.Empty
		Private _fldpolisContactNo As String = String.Empty
		Private _fldBalaiName As String = String.Empty
		Private _fldBalaiContactNo1 As String = String.Empty
		Private _fldBalaiContactNo2 As String = String.Empty

		Private _fldIMEI As String = String.Empty
		Private _fldGPS As String = String.Empty
		Private _fldLocation As String = String.Empty
		Private _fldBatt As String = String.Empty
		Private _fldStatus As String = String.Empty

#End Region

#Region "Public Properties"
		Public Property fldpolisNo() As String
			Get
				Return _fldpolisNo
			End Get
			Set
				_fldpolisNo = Value
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
		Public Property fldBatt() As String
			Get
				Return _fldBatt
			End Get
			Set
				_fldBatt = Value
			End Set
		End Property
		Public Property fldLocation() As String
			Get
				Return _fldLocation
			End Get
			Set
				_fldLocation = Value
			End Set
		End Property
		Public Property fldGPS() As String
			Get
				Return _fldGPS
			End Get
			Set
				_fldGPS = Value
			End Set
		End Property
		Public Property fldIMEI() As String
			Get
				Return _fldIMEI
			End Get
			Set
				_fldIMEI = Value
			End Set
		End Property



		Public Property fldBalaiContactNo2() As String
			Get
				Return _fldBalaiContactNo2
			End Get
			Set
				_fldBalaiContactNo2 = Value
			End Set
		End Property
		Public Property fldBalaiContactNo1() As String
			Get
				Return _fldBalaiContactNo1
			End Get
			Set
				_fldBalaiContactNo1 = Value
			End Set
		End Property
		Public Property fldBalaiName() As String
			Get
				Return _fldBalaiName
			End Get
			Set
				_fldBalaiName = Value
			End Set
		End Property
		Public Property fldpolisContactNo() As String
			Get
				Return _fldpolisContactNo
			End Get
			Set
				_fldpolisContactNo = Value
			End Set
		End Property


		Public Property fldemdID() As Long
			Get
				Return _fldemdID
			End Get
			Set
				_fldemdID = Value
			End Set
		End Property

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
		Public Property fldoppID() As Long
			Get
				Return _fldoppID
			End Get
			Set
				_fldoppID = Value
			End Set
		End Property
		Public Property fldoppName() As String
			Get
				Return _fldoppName
			End Get
			Set
				_fldoppName = Value
			End Set
		End Property
		Public Property fldoppContactNo() As String
			Get
				Return _fldoppContactNo
			End Get
			Set
				_fldoppContactNo = Value
			End Set
		End Property
		Public Property fldPolisID() As Long
			Get
				Return _fldPolisID
			End Get
			Set
				_fldPolisID = Value
			End Set
		End Property
		Public Property fldoppJabatan() As String
			Get
				Return _fldoppJabatan
			End Get
			Set
				_fldoppJabatan = Value
			End Set
		End Property
		Public Property fldoppBalai() As String
			Get
				Return _fldoppBalai
			End Get
			Set
				_fldoppBalai = Value
			End Set
		End Property









#End Region

	End Class

End Namespace