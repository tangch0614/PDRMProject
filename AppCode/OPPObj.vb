Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

	Public Class OPPObj

#Region "Private Variables"
		Private _fldID As Long
		Private _fldRefID As String = String.Empty
		Private _fldName As String = String.Empty
		Private _fldGender As String = String.Empty
		Private _fldICNo As String = String.Empty
		Private _fldContactNo As String = String.Empty
		Private _fldPhoto1 As String = String.Empty
		Private _fldPhoto2 As String = String.Empty
		Private _fldAddress As String = String.Empty
		Private _fldCountryID As String = String.Empty
		Private _fldState As String = String.Empty
		Private _fldDistrict As String = String.Empty
		Private _fldMukim As String = String.Empty
		Private _fldIPKID As Long
		Private _fldIPDID As Long
		Private _fldPoliceStationID As Long
		Private _fldDeptID As String = String.Empty
		Private _fldOffenceDesc As String = String.Empty
		Private _fldCaseFileNo As String = String.Empty
		Private _fldCaseHandlerName As String = String.Empty
		Private _fldCaseHandlerTelNo As String = String.Empty
		Private _fldActsID As Long
		Private _fldActsSectionID As Long
		Private _fldOrdParty As String = String.Empty
		Private _fldOrdPartyName As String = String.Empty
		Private _fldOrdIssuedDate As DateTime = DateTime.MinValue
		Private _fldOrdRefNo As String = String.Empty
		Private _fldOrdDay As Integer = 0
		Private _fldOrdMonth As Integer = 0
		Private _fldOrdYear As Integer = 0
		Private _fldOrdFrDate As DateTime = DateTime.MinValue
		Private _fldOrdToDate As DateTime = DateTime.MinValue
		Private _fldRptPoliceStationID As Long
		Private _fldSDNo As String = String.Empty
		Private _fldOCSName As String = String.Empty
		Private _fldOCSTelNo As String = String.Empty
		Private _fldRptDay As String = "Monday"
		Private _fldRptFrTime As String = "08:00:00"
		Private _fldRptToTime As String = "12:00:00"
		Private _fldOverseerID As Long
		Private _fldEMDDeviceID As Long
		Private _fldEMDInstallDate As DateTime = DateTime.MinValue
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
		Private _fldRestrictFrTime As String = String.Empty
		Private _fldRestrictToTime As String = String.Empty
		Private _fldGeofence1 As String = String.Empty
		Private _fldGeofence1FrTime As String = String.Empty
		Private _fldGeofence1ToTime As String = String.Empty
		Private _fldGeofence2 As String = String.Empty
		Private _fldGeofence2FrTime As String = String.Empty
		Private _fldGeofence2ToTime As String = String.Empty
		Private _fldGeofenceDistrict As String = String.Empty
		Private _fldGeofenceMukim As String = String.Empty
		Private _fldGeofenceMukimID As Integer = 0
		Private _fldAttachment1 As String = String.Empty
		Private _fldAttachment2 As String = String.Empty
		Private _fldRemark As String = String.Empty
		Private _fldStatus As String = "Y"
		Private _fldActivateDateTime As DateTime = DateTime.MinValue
		Private _fldVerifyStatus As String = "P"
		Private _fldVerifyDateTime As DateTime = DateTime.MinValue
		Private _fldVerifyBy As Long = 0
		Private _fldGeoFenceActive As Integer = 0

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
		Public Property fldName() As String
			Get
				Return _fldName
			End Get
			Set
				_fldName = Value
			End Set
		End Property
		Public Property fldGender As String
			Get
				Return _fldGender
			End Get
			Set(value As String)
				_fldGender = value
			End Set
		End Property
		Public Property fldICNo() As String
			Get
				Return _fldICNo
			End Get
			Set
				_fldICNo = Value
			End Set
		End Property
		Public Property fldPhoto1() As String
			Get
				Return _fldPhoto1
			End Get
			Set
				_fldPhoto1 = Value
			End Set
		End Property
		Public Property fldPhoto2() As String
			Get
				Return _fldPhoto2
			End Get
			Set
				_fldPhoto2 = Value
			End Set
		End Property
		Public Property fldAddress() As String
			Get
				Return _fldAddress
			End Get
			Set
				_fldAddress = Value
			End Set
		End Property
		Public Property fldCountryID() As String
			Get
				Return _fldCountryID
			End Get
			Set
				_fldCountryID = Value
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
		Public Property fldPoliceStationID() As Long
			Get
				Return _fldPoliceStationID
			End Get
			Set
				_fldPoliceStationID = Value
			End Set
		End Property
		Public Property fldDeptID() As String
			Get
				Return _fldDeptID
			End Get
			Set
				_fldDeptID = Value
			End Set
		End Property
		Public Property fldOffenceDesc() As String
			Get
				Return _fldOffenceDesc
			End Get
			Set
				_fldOffenceDesc = Value
			End Set
		End Property
		Public Property fldCaseFileNo() As String
			Get
				Return _fldCaseFileNo
			End Get
			Set
				_fldCaseFileNo = Value
			End Set
		End Property
		Public Property fldCaseHandlerName() As String
			Get
				Return _fldCaseHandlerName
			End Get
			Set
				_fldCaseHandlerName = Value
			End Set
		End Property
		Public Property fldCaseHandlerTelNo() As String
			Get
				Return _fldCaseHandlerTelNo
			End Get
			Set
				_fldCaseHandlerTelNo = Value
			End Set
		End Property
		Public Property fldActsID() As Long
			Get
				Return _fldActsID
			End Get
			Set
				_fldActsID = Value
			End Set
		End Property
		Public Property fldActsSectionID() As Long
			Get
				Return _fldActsSectionID
			End Get
			Set
				_fldActsSectionID = Value
			End Set
		End Property
		Public Property fldOrdParty() As String
			Get
				Return _fldOrdParty
			End Get
			Set
				_fldOrdParty = Value
			End Set
		End Property
		Public Property fldOrdPartyName() As String
			Get
				Return _fldOrdPartyName
			End Get
			Set
				_fldOrdPartyName = Value
			End Set
		End Property
		Public Property fldOrdRefNo() As String
			Get
				Return _fldOrdRefNo
			End Get
			Set
				_fldOrdRefNo = Value
			End Set
		End Property
		Public Property fldOrdYear() As Integer
			Get
				Return _fldOrdYear
			End Get
			Set
				_fldOrdYear = Value
			End Set
		End Property
		Public Property fldOrdFrDate() As DateTime
			Get
				Return _fldOrdFrDate
			End Get
			Set
				_fldOrdFrDate = Value
			End Set
		End Property
		Public Property fldOrdToDate() As DateTime
			Get
				Return _fldOrdToDate
			End Get
			Set
				_fldOrdToDate = Value
			End Set
		End Property
		Public Property fldRptPoliceStationID() As Long
			Get
				Return _fldRptPoliceStationID
			End Get
			Set
				_fldRptPoliceStationID = Value
			End Set
		End Property
		Public Property fldSDNo() As String
			Get
				Return _fldSDNo
			End Get
			Set
				_fldSDNo = Value
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
		Public Property fldRptDay() As String
			Get
				Return _fldRptDay
			End Get
			Set
				_fldRptDay = Value
			End Set
		End Property
		Public Property fldRptFrTime() As String
			Get
				Return _fldRptFrTime
			End Get
			Set
				_fldRptFrTime = Value
			End Set
		End Property
		Public Property fldRptToTime() As String
			Get
				Return _fldRptToTime
			End Get
			Set
				_fldRptToTime = Value
			End Set
		End Property
		Public Property fldOverseerID() As Long
			Get
				Return _fldOverseerID
			End Get
			Set
				_fldOverseerID = Value
			End Set
		End Property
		Public Property fldEMDDeviceID() As Long
			Get
				Return _fldEMDDeviceID
			End Get
			Set
				_fldEMDDeviceID = Value
			End Set
		End Property
		Public Property fldGeofence1() As String
			Get
				Return _fldGeofence1
			End Get
			Set
				_fldGeofence1 = Value
			End Set
		End Property
		Public Property fldGeofence1FrTime() As String
			Get
				Return _fldGeofence1FrTime
			End Get
			Set
				_fldGeofence1FrTime = Value
			End Set
		End Property
		Public Property fldGeofence1ToTime() As String
			Get
				Return _fldGeofence1ToTime
			End Get
			Set
				_fldGeofence1ToTime = Value
			End Set
		End Property
		Public Property fldGeofence2() As String
			Get
				Return _fldGeofence2
			End Get
			Set
				_fldGeofence2 = Value
			End Set
		End Property
		Public Property fldGeofence2FrTime() As String
			Get
				Return _fldGeofence2FrTime
			End Get
			Set
				_fldGeofence2FrTime = Value
			End Set
		End Property
		Public Property fldGeofence2ToTime() As String
			Get
				Return _fldGeofence2ToTime
			End Get
			Set
				_fldGeofence2ToTime = Value
			End Set
		End Property
		Public Property fldGeofenceMukim() As String
			Get
				Return _fldGeofenceMukim
			End Get
			Set
				_fldGeofenceMukim = Value
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
		Public Property fldStatus() As String
			Get
				Return _fldStatus
			End Get
			Set
				_fldStatus = Value
			End Set
		End Property
		Public Property fldOrdDay As Integer
			Get
				Return _fldOrdDay
			End Get
			Set(value As Integer)
				_fldOrdDay = value
			End Set
		End Property
		Public Property fldOrdMonth As Integer
			Get
				Return _fldOrdMonth
			End Get
			Set(value As Integer)
				_fldOrdMonth = value
			End Set
		End Property
		Public Property fldOrdIssuedDate As Date
			Get
				Return _fldOrdIssuedDate
			End Get
			Set(value As Date)
				_fldOrdIssuedDate = value
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
		Public Property fldRestrictFrTime As String
			Get
				Return _fldRestrictFrTime
			End Get
			Set(value As String)
				_fldRestrictFrTime = value
			End Set
		End Property
		Public Property fldRestrictToTime As String
			Get
				Return _fldRestrictToTime
			End Get
			Set(value As String)
				_fldRestrictToTime = value
			End Set
		End Property
		Public Property fldRefID As String
			Get
				Return _fldRefID
			End Get
			Set(value As String)
				_fldRefID = value
			End Set
		End Property
		Public Property fldEMDInstallDate As Date
			Get
				Return _fldEMDInstallDate
			End Get
			Set(value As Date)
				_fldEMDInstallDate = value
			End Set
		End Property
		Public Property fldContactNo As String
			Get
				Return _fldContactNo
			End Get
			Set(value As String)
				_fldContactNo = value
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

		Public Property fldVerifyStatus As String
			Get
				Return _fldVerifyStatus
			End Get
			Set(value As String)
				_fldVerifyStatus = value
			End Set
		End Property

		Public Property fldVerifyDateTime As Date
			Get
				Return _fldVerifyDateTime
			End Get
			Set(value As Date)
				_fldVerifyDateTime = value
			End Set
		End Property

		Public Property fldVerifyBy As Long
			Get
				Return _fldVerifyBy
			End Get
			Set(value As Long)
				_fldVerifyBy = value
			End Set
		End Property

		Public Property fldGeoFenceActive As Integer
			Get
				Return _fldGeoFenceActive
			End Get
			Set(value As Integer)
				_fldGeoFenceActive = value
			End Set
		End Property

		Public Property fldGeofenceDistrict As String
			Get
				Return _fldGeofenceDistrict
			End Get
			Set(value As String)
				_fldGeofenceDistrict = value
			End Set
		End Property

		Public Property fldGeofenceMukimID As Integer
			Get
				Return _fldGeofenceMukimID
			End Get
			Set(value As Integer)
				_fldGeofenceMukimID = value
			End Set
		End Property

		Public Property fldActivateDateTime As Date
			Get
				Return _fldActivateDateTime
			End Get
			Set(value As Date)
				_fldActivateDateTime = value
			End Set
		End Property


#End Region

	End Class
End Namespace
