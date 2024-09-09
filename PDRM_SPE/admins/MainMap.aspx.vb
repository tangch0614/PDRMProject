Imports AppCode.BusinessLogic

Public Class AMainMap
    Inherits ABase

    Protected Property UserID() As Long
        Get
            If Not ViewState("UserID") Is Nothing Then
                Return CLng(ViewState("UserID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("UserID") = value
        End Set
    End Property

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub

#Region "Initialize"

    Private Sub Initialize()
        UserID = AdminAuthentication.GetUserData(2)
        SetDeparmentColor()
        GetEMD()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();initdashboarddata();initNotifications();", True)
    End Sub

    Private Sub SetDeparmentColor()
        dvJenayah.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(1))
        dvKomersil.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(2))
        dvNarkotik.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(3))
        dvCawanganKhas.Attributes.CssStyle.Add("Background-Color", UtilityManager.GetDeptColor(4))
    End Sub

    Private Sub GetEMD()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlEMD.DataTextField = "fldName"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        ddlEMD.Items.Insert(0, New ListItem(GetText("All"), -1))
        ddlEMD.SelectedIndex = 0
    End Sub

    Protected Sub ddlEMD_SelectedIndexChanged(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "fetchAndUpdateMarkers(true);", True)
    End Sub

#End Region
End Class