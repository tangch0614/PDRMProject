Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Data

NameSpace BusinessObject

    Public Class RoleObj

#Region "Private Variables"
        Private _fldRoleID As Long
        Private _fldRoleName As String = String.Empty

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
        Public Property fldRoleName() As String
            Get
                Return _fldRoleName
            End Get
            Set(value As String)
                _fldRoleName = Value
            End Set
        End Property



#End Region

    End Class
End Namespace
