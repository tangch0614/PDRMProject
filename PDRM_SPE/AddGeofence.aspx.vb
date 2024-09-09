Imports System.Drawing
Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AddGeofence
    Inherits System.Web.UI.Page
    Private Property OPPID() As Long
        Get
            If Not ViewState("OPPID") Is Nothing Then
                Return CLng(ViewState("OPPID"))
            Else
                Return 0
            End If
        End Get
        Set(value As Long)
            ViewState("OPPID") = value
        End Set
    End Property
    Private Property deflat() As String
        Get
            If Not ViewState("deflat") Is Nothing Then
                Return CStr(ViewState("deflat"))
            Else
                Return 0
            End If
        End Get
        Set(value As String)
            ViewState("deflat") = value
        End Set
    End Property
    Private Property deflng() As String
        Get
            If Not ViewState("deflng") Is Nothing Then
                Return CStr(ViewState("deflng"))
            Else
                Return 0
            End If
        End Get
        Set(value As String)
            ViewState("deflng") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request("id") Is Nothing AndAlso Not Request("i") Is Nothing AndAlso Request("i").Equals(UtilityManager.MD5Encrypt(Request("id") & "geopagar")) Then
                OPPID = Request("id")
                Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
                If Not opp Is Nothing Then
                    Dim emd As DataTable = EMDDeviceManager.GetDeviceList(opp.fldEMDDeviceID, "", "", "")
                    If emd.Rows.Count > 0 Then
                        deflat = emd.Rows(0)("fldRLat")
                        deflng = emd.Rows(0)("fldRLong")
                        hfCordinates.Value = ""
                        If Not String.IsNullOrWhiteSpace(opp.fldGeofence1) Then
                            hfCordinates.Value = opp.fldGeofence1
                        Else
                            Dim setDefaultLat1 As Double = deflat - 0.01
                            Dim setDefaultLng1 As Double = deflng - 0.01

                            Dim setDefaultLat2 As Double = deflat + 0.01
                            Dim setDefaultLng2 As Double = deflng - 0.01

                            Dim setDefaultLat3 As Double = deflat + 0.01
                            Dim setDefaultLng3 As Double = deflng + 0.01

                            Dim setDefaultLat4 As Double = deflat - 0.01
                            Dim setDefaultLng4 As Double = deflng + 0.01

                            hfCordinates.Value = "[{""lat"":" & setDefaultLat1 & ",""lng"":" & setDefaultLng1 & "}," &
                             "{""lat"":" & setDefaultLat2 & ",""lng"":" & setDefaultLng2 & "}," &
                             "{""lat"":" & setDefaultLat3 & ",""lng"":" & setDefaultLng3 & "}," &
                             "{""lat"":" & setDefaultLat4 & ",""lng"":" & setDefaultLng4 & "}]"
                        End If
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap(" & deflat & "," & deflng & ",'" & hfCordinates.Value & "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "alert('Data EMD tidak dapat dijumpai!');window.top.close();", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "alert('Data OPP tidak dapat dijumpai!');window.top.close();", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "_blank", "alert('Error');window.top.close();", True)
            End If
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        If Not String.IsNullOrWhiteSpace(hfCordinates.Value) Then
            If OPPManager.UpdateGeofence1(OPPID, hfCordinates.Value) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & "Kemas kini berjaya!" & "');initMap(" & deflat & "," & deflng & ",'" & hfCordinates.Value & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & "Sila lukis geo pagar!" & "');", True)
        End If
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Dim opp As OPPObj = OPPManager.GetOPP(OPPID)
        If Not opp Is Nothing Then
            Dim emd As DataTable = EMDDeviceManager.GetDeviceList(opp.fldEMDDeviceID, "", "", "")
            If emd.Rows.Count > 0 Then
                deflat = emd.Rows(0)("fldRLat")
                deflng = emd.Rows(0)("fldRLong")
                Dim setDefaultLat1 As Double = deflat - 0.01
                Dim setDefaultLng1 As Double = deflng - 0.01

                Dim setDefaultLat2 As Double = deflat + 0.01
                Dim setDefaultLng2 As Double = deflng - 0.01

                Dim setDefaultLat3 As Double = deflat + 0.01
                Dim setDefaultLng3 As Double = deflng + 0.01

                Dim setDefaultLat4 As Double = deflat - 0.01
                Dim setDefaultLng4 As Double = deflng + 0.01

                hfCordinates.Value = "[{""lat"":" & setDefaultLat1 & ",""lng"":" & setDefaultLng1 & "}," &
                 "{""lat"":" & setDefaultLat2 & ",""lng"":" & setDefaultLng2 & "}," &
                 "{""lat"":" & setDefaultLat3 & ",""lng"":" & setDefaultLng3 & "}," &
                 "{""lat"":" & setDefaultLat4 & ",""lng"":" & setDefaultLng4 & "}]"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap(" & deflat & "," & deflng & ",'" & hfCordinates.Value & "');", True)
            End If
        End If
    End Sub
End Class