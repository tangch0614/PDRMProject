Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class RoleMenuObj

#Region "Private Variables"
        Private _fldRoleID As Long
        Private _fldMenuID As Long
        Private _fldUserType As String = "A"
#End Region

#Region "Public Properties"
        Public Property fldRoleID() As Long
            Get
                Return _fldRoleID
            End Get
            Set(value As Long)
                _fldRoleID = Value
            End Set
        End Property
        Public Property fldMenuID() As Long
            Get
                Return _fldMenuID
            End Get
            Set(value As Long)
                _fldMenuID = Value
            End Set
        End Property
        Public Property fldUserType() As String
            Get
                Return _fldUserType
            End Get
            Set(value As String)
                _fldUserType = value
            End Set
        End Property



#End Region

    End Class
End Namespace
