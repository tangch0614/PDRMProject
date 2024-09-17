Imports AppCode.BusinessLogic

Public Class ABase
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Authorization() Then
                Response.Redirect("~/AccessDenied.aspx")
            End If
        End If
    End Sub

    Private Function Authorization() As Boolean
        Dim result As Boolean = False
        If Not Page.Request.Url.AbsolutePath.ToLower.EndsWith("AccessDenied.aspx") AndAlso Page.Request.Url.AbsolutePath.ToLower.StartsWith("/admins") Then
            result = RoleManager.Authorization(CLng(AdminAuthentication.GetUserData(5)), "A", ".." & Page.Request.Url.AbsolutePath) AndAlso UserMenuManager.Authorization(CLng(AdminAuthentication.GetUserData(2)), "A", ".." & Page.Request.Url.AbsolutePath)
        Else
            result = True
        End If
        Return result
    End Function

    Public Sub UserIsAuthenticated()
        Dim result As Boolean = Page.Request.IsAuthenticated AndAlso AdminAuthentication.ValidateSession(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0)))
        If Not result Then
            AdminAuthentication.Logout(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0)))
            Response.Redirect("~/secure/Login_a.aspx")
        End If
    End Sub

    Public Function AuthorizedAdmin(ByVal adminID As Long()) As Boolean
        Dim result As Boolean = False
        If adminID.Contains(AdminAuthentication.GetUserData(2)) Then
            result = True
        End If
        Return result
    End Function

    Public Function RestrictedAdmin(ByVal adminID As Long()) As Boolean
        Dim result As Boolean = True
        If adminID.Contains(AdminAuthentication.GetUserData(2)) Then
            result = False
        End If
        Return result
    End Function

    Public Function WalletActionAuthorization(ByVal adminID As Long, ByVal walletTable As DataTable, ByVal actionType As String) As DataTable
        Dim filterTable As DataTable = walletTable.Clone
        Dim authorizeCol As String = ""
        Dim restrictCol As String = ""

        If actionType.ToUpper.Equals("ADJUST") Then
            authorizeCol = "fldAdjustAuthorize"
            restrictCol = "fldAdjustRestrict"
        ElseIf actionType.ToUpper.Equals("TOPUP") Then
            authorizeCol = "fldTopupAuthorize"
            restrictCol = "fldTopupRestrict"
        End If

        For i As Integer = 0 To walletTable.Rows.Count - 1
            Dim authorizeIDList As String = walletTable.Rows(i)(authorizeCol)
            Dim restrictIDList As String = walletTable.Rows(i)(restrictCol)
            Dim authorized As Boolean = True
            Dim restricted As Boolean = False
            If Not String.IsNullOrEmpty(authorizeIDList) Then
                If Not authorizeIDList.Split(",").Contains(adminID) Then
                    authorized = False
                End If
            End If
            If Not String.IsNullOrEmpty(restrictIDList) Then
                If restrictIDList.Split(",").Contains(adminID) Then
                    restricted = True
                End If
            End If
            If authorized AndAlso Not restricted Then
                filterTable.ImportRow(walletTable.Rows(i))
            End If
        Next

        Return filterTable
    End Function

End Class