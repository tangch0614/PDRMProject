Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

	Public Class EMDInstallRequestObj

#Region "Private Variables"
		Private _fldID As Long
		Private _fldDateTime As DateTime = DateTime.MinValue
		Private _fldRefID As String = String.Empty
		Private _fldDeptID As Long
		Private _fldInstallDateTime As DateTime = DateTime.MinValue
		Private _fldState As String = String.Empty
		Private _fldDistrict As String = String.Empty
		Private _fldMukim As String = String.Empty
		Private _fldIPKID As Long
		Private _fldIPDID As Long
		Private _fldPSID As Long
		Private _fldOCSName As String = String.Empty
		Private _fldOCSTelNo As String = String.Empty
		Private _fldEMDDeviceID As Long
		Private _fldOPPID As Long
		Private _fldSmartTagCode As String = String.Empty
		Private _fldOBCCode As String = String.Empty
		Private _fldChargerCode As String = String.Empty
		Private _fldStrapCode As String = String.Empty
		Private _fldCableCode As String = String.Empty
		Private _fldBeaconCode As String = String.Empty
		Private _fldSmartTag As Integer = 0
		Private _fldOBC As Integer = 0
		Private _fldBeacon As Integer = 0
		Private _fldCharger As Integer = 0
		Private _fldStrap As Integer = 0
		Private _fldCable As Integer = 0
		Private _fldAttachment1 As String = String.Empty
		Private _fldAttachment2 As String = String.Empty
		Private _fldRemark As String = String.Empty
		Private _fldCreatorID As Long
		Private _fldStatus As String = "P"
		Private _fldProcessByID As Long
		Private _fldProcessDateTime As DateTime = DateTime.MinValue
		Private _fldProcessRemark As String = String.Empty

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
		Public Property fldRefID() As String
			Get
				Return _fldRefID
			End Get
			Set
				_fldRefID = Value
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
		Public Property fldDeptID() As Long
			Get
				Return _fldDeptID
			End Get
			Set
				_fldDeptID = Value
			End Set
		End Property
		Public Property fldInstallDateTime() As DateTime
			Get
				Return _fldInstallDateTime
			End Get
			Set
				_fldInstallDateTime = Value
			End Set
		End Property
		Public Property fldState() As String
			Get
				Return _fldState
			End Get
			Set
				_fldState = Value
			End Set
		End Property
		Public Property fldDistrict() As String
			Get
				Return _fldDistrict
			End Get
			Set
				_fldDistrict = Value
			End Set
		End Property
		Public Property fldMukim() As String
			Get
				Return _fldMukim
			End Get
			Set
				_fldMukim = Value
			End Set
		End Property
		Public Property fldIPKID() As Long
			Get
				Return _fldIPKID
			End Get
			Set
				_fldIPKID = Value
			End Set
		End Property
		Public Property fldIPDID() As Long
			Get
				Return _fldIPDID
			End Get
			Set
				_fldIPDID = Value
			End Set
		End Property
		Public Property fldPSID() As Long
			Get
				Return _fldPSID
			End Get
			Set
				_fldPSID = Value
			End Set
		End Property
		Public Property fldOCSName() As String
			Get
				Return _fldOCSName
			End Get
			Set
				_fldOCSName = Value
			End Set
		End Property
		Public Property fldOCSTelNo() As String
			Get
				Return _fldOCSTelNo
			End Get
			Set
				_fldOCSTelNo = Value
			End Set
		End Property
		Public Property fldAttachment1() As String
			Get
				Return _fldAttachment1
			End Get
			Set
				_fldAttachment1 = Value
			End Set
		End Property
		Public Property fldAttachment2() As String
			Get
				Return _fldAttachment2
			End Get
			Set
				_fldAttachment2 = Value
			End Set
		End Property
		Public Property fldRemark() As String
			Get
				Return _fldRemark
			End Get
			Set
				_fldRemark = Value
			End Set
		End Property
		Public Property fldCreatorID() As Long
			Get
				Return _fldCreatorID
			End Get
			Set
				_fldCreatorID = Value
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
		Public Property fldProcessByID() As Long
			Get
				Return _fldProcessByID
			End Get
			Set
				_fldProcessByID = Value
			End Set
		End Property
		Public Property fldProcessDateTime() As DateTime
			Get
				Return _fldProcessDateTime
			End Get
			Set
				_fldProcessDateTime = Value
			End Set
		End Property
		Public Property fldProcessRemark() As String
			Get
				Return _fldProcessRemark
			End Get
			Set
				_fldProcessRemark = Value
			End Set
		End Property

		Public Property fldEMDDeviceID As Long
			Get
				Return _fldEMDDeviceID
			End Get
			Set(value As Long)
				_fldEMDDeviceID = value
			End Set
		End Property

		Public Property fldOPPID As Long
			Get
				Return _fldOPPID
			End Get
			Set(value As Long)
				_fldOPPID = value
			End Set
		End Property

		Public Property fldSmartTagCode As String
			Get
				Return _fldSmartTagCode
			End Get
			Set(value As String)
				_fldSmartTagCode = value
			End Set
		End Property

		Public Property fldOBCCode As String
			Get
				Return _fldOBCCode
			End Get
			Set(value As String)
				_fldOBCCode = value
			End Set
		End Property

		Public Property fldChargerCode As String
			Get
				Return _fldChargerCode
			End Get
			Set(value As String)
				_fldChargerCode = value
			End Set
		End Property

		Public Property fldStrapCode As String
			Get
				Return _fldStrapCode
			End Get
			Set(value As String)
				_fldStrapCode = value
			End Set
		End Property

		Public Property fldCableCode As String
			Get
				Return _fldCableCode
			End Get
			Set(value As String)
				_fldCableCode = value
			End Set
		End Property

		Public Property fldBeaconCode As String
			Get
				Return _fldBeaconCode
			End Get
			Set(value As String)
				_fldBeaconCode = value
			End Set
		End Property

		Public Property fldSmartTag As Integer
			Get
				Return _fldSmartTag
			End Get
			Set(value As Integer)
				_fldSmartTag = value
			End Set
		End Property

		Public Property fldOBC As Integer
			Get
				Return _fldOBC
			End Get
			Set(value As Integer)
				_fldOBC = value
			End Set
		End Property

		Public Property fldBeacon As Integer
			Get
				Return _fldBeacon
			End Get
			Set(value As Integer)
				_fldBeacon = value
			End Set
		End Property

		Public Property fldCharger As Integer
			Get
				Return _fldCharger
			End Get
			Set(value As Integer)
				_fldCharger = value
			End Set
		End Property

		Public Property fldStrap As Integer
			Get
				Return _fldStrap
			End Get
			Set(value As Integer)
				_fldStrap = value
			End Set
		End Property

		Public Property fldCable As Integer
			Get
				Return _fldCable
			End Get
			Set(value As Integer)
				_fldCable = value
			End Set
		End Property

#End Region

	End Class
End Namespace
