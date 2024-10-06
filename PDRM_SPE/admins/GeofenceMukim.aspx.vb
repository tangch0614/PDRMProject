Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AGeofenceMukim
    Inherits ABase

    Private Property lat() As String
        Get
            If Not ViewState("lat") Is Nothing Then
                Return CStr(ViewState("lat"))
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            ViewState("lat") = value
        End Set
    End Property
    Private Property lng() As String
        Get
            If Not ViewState("lng") Is Nothing Then
                Return CStr(ViewState("lng"))
            Else
                Return ""
            End If
        End Get
        Set(value As String)
            ViewState("lng") = value
        End Set
    End Property
    Private Property zoom() As Integer
        Get
            If Not ViewState("zoom") Is Nothing Then
                Return CInt(ViewState("zoom"))
            Else
                Return 0
            End If
        End Get
        Set(value As Integer)
            ViewState("zoom") = value
        End Set
    End Property
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Initialize()
        End If
    End Sub

    Private Sub Initialize()
        SetText()
        GetState()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();", True)
        ddlState_SelectedIndexChanged(Me.ddlState, EventArgs.Empty)
    End Sub

    Private Sub SetText()
        btnSubmit.Text = GetText("Update")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("Update").ToLower)
    End Sub

    Private Sub GetState()
        ddlState.DataSource = CountryManager.GetCountryStateList("MY")
        ddlState.DataTextField = "fldState"
        ddlState.DataValueField = "fldState"
        ddlState.DataBind()
        ddlState.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("State")), ""))
        ddlState.SelectedIndex = 0
    End Sub

    Private Sub GetDistrict(ByVal state As String)
        ddlDistrict.Items.Clear()
        If Not String.IsNullOrWhiteSpace(state) Then
            ddlDistrict.DataSource = CountryManager.GetDistrictList(state)
            ddlDistrict.DataTextField = "fldDistrict"
            ddlDistrict.DataValueField = "fldDistrict"
            ddlDistrict.DataBind()
        End If
        ddlDistrict.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("District")), ""))
        ddlDistrict.SelectedIndex = 0
    End Sub

    Private Sub GetMukim(ByVal state As String, ByVal district As String)
        ddlMukim.Items.Clear()
        If Not String.IsNullOrWhiteSpace(state) Then
            ddlMukim.DataSource = CountryManager.GetMukimList(state, district)
            ddlMukim.DataTextField = "fldMukim"
            ddlMukim.DataValueField = "fldMukim"
            ddlMukim.DataBind()
        End If
        ddlMukim.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Mukim")), ""))
        ddlMukim.SelectedIndex = 0
    End Sub

    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetDistrict(ddlState.SelectedValue)
        ddlDistrict_SelectedIndexChanged(Me.ddlDistrict, EventArgs.Empty)
    End Sub

    Protected Sub ddlDistrict_SelectedIndexChanged(sender As Object, e As EventArgs)
        GetMukim(ddlState.SelectedValue, ddlDistrict.SelectedValue)
    End Sub

    Protected Sub ddlMukim_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlMukim.SelectedIndex > 0 Then
            Dim mukimobj As DataTable = CountryManager.GetMukim(ddlMukim.SelectedValue)
            lat = mukimobj.Rows(0)("fldlat")
            lng = mukimobj.Rows(0)("fldlng")
            zoom = mukimobj.Rows(0)("fldZoom")
            hfGeofence.Value = mukimobj.Rows(0)("fldGeofence")
            If (String.IsNullOrWhiteSpace(lat) OrElse String.IsNullOrWhiteSpace(lng)) Then
                Dim stateobj As DataTable = CountryManager.GetState(mukimobj.Rows(0)("fldState"))
                lat = stateobj.Rows(0)("fldlat")
                lng = stateobj.Rows(0)("fldlng")
                zoom = stateobj.Rows(0)("fldZoom")
            End If
            If String.IsNullOrWhiteSpace(hfGeofence.Value) Then
                Dim gflat1 As Double = lat - 0.01
                Dim gflng1 As Double = lng - 0.01
                Dim gflat2 As Double = lat + 0.01
                Dim gflng2 As Double = lng - 0.01
                Dim gflat3 As Double = lat + 0.01
                Dim gflng3 As Double = lng + 0.01
                Dim gflat4 As Double = lat - 0.01
                Dim gflng4 As Double = lng + 0.01
                hfGeofence.Value = "[{""lat"":" & gflat1 & ",""lng"":" & gflng1 & "}," &
                 "{""lat"":" & gflat2 & ",""lng"":" & gflng2 & "}," &
                 "{""lat"":" & gflat3 & ",""lng"":" & gflng3 & "}," &
                 "{""lat"":" & gflat4 & ",""lng"":" & gflng4 & "}]"
            End If
        Else
            lat = "4.2105"
            lng = "108.9758"
            zoom = 6
            hfGeofence.Value = ""
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "getGeofence(" & hfGeofence.Value & "," & lat & "," & lng & "," & zoom & ");", True)
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        If Not String.IsNullOrWhiteSpace(hfGeofence.Value) Then
            If CountryManager.UpdateMukimGeofence(ddlMukim.SelectedValue, hfGeofence.Value) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUpdateSuccess") & "');getGeofence(" & hfGeofence.Value & "," & lat & "," & lng & "," & zoom & ");", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & "Sila lukis geo pagar!" & "');", True)
        End If
    End Sub
End Class