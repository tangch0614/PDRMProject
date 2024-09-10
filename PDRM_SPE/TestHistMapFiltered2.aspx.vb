Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class TestHistMapFiltered2
    Inherits Base

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            GetEMDDevice()
            GetOPPList()
            GetTime()
            hfOPPID.Value = -1
            Dim dateTime As DateTime = UtilityManager.GetServerDateTime()
            hfFrDate.Text = dateTime.ToString("yyyy-MM-dd")
            hfToDate.Text = dateTime.ToString("yyyy-MM-dd")
            If Not Request("opp") Is Nothing Then
                Dim opp As OPPObj = OPPManager.GetOPP(Request("opp"))
                If Not opp Is Nothing AndAlso Not Request("i") Is Nothing AndAlso Request("i").Equals(UtilityManager.MD5Encrypt(Request("opp") & "historymap")) Then
                    Try
                        ddlEMD.SelectedValue = opp.fldEMDDeviceID
                        ddlOPP.SelectedValue = opp.fldID
                        hfOPPID.Value = Request("opp")
                    Catch
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorItemNotFound").Replace("vITEM", "OPP") & "')", True)
                        ddlEMD.SelectedIndex = 0
                        ddlOPP.SelectedIndex = 0
                    End Try
                End If
            End If
            GetOPPInfo(hfOPPID.Value)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "initMap();fetchAndUpdateMarkers();", True)
        End If
    End Sub

    Private Sub SetText()
        btnSearch.Text = GetText("Search")
        hfNoResult.Value = GetText("ErrorNoResult")
    End Sub

    Private Sub GetTime()
        Dim startTime As DateTime = DateTime.ParseExact("00:00:00", "HH:mm:ss", Nothing)
        Dim endTime As DateTime = DateTime.ParseExact("23:55:00", "HH:mm:ss", Nothing)

        While startTime <= endTime
            ' Add time to dropdown list
            ddlFrTime.Items.Add(New ListItem(startTime.ToString("HH:mm:ss"), startTime.ToString("HH:mm:ss")))
            ddlToTime.Items.Add(New ListItem(startTime.ToString("HH:mm:ss"), startTime.ToString("HH:mm:ss")))
            ' Increment time by 5 minutes
            startTime = startTime.AddMinutes(5)
        End While

        ddlFrTime.SelectedIndex = 0
        ddlToTime.SelectedIndex = ddlToTime.Items.Count - 1
    End Sub

    Private Sub GetEMDDevice()
        ddlEMD.DataSource = EMDDeviceManager.GetDeviceList(-1, "", "", "Y")
        ddlEMD.DataTextField = "fldImei"
        ddlEMD.DataValueField = "fldID"
        ddlEMD.DataBind()
        ddlEMD.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("EMD")), -1))
        ddlEMD.SelectedIndex = 0
    End Sub

    Private Sub GetOPPList()
        Dim datatable As DataTable = OPPManager.GetOPPList(-1, "Y")
        datatable.Columns.Add("fldNameIC", GetType(String), "fldName + '-' + fldICNo")
        ddlOPP.DataSource = datatable
        ddlOPP.DataTextField = "fldNameIC"
        ddlOPP.DataValueField = "fldID"
        ddlOPP.DataBind()
        ddlOPP.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("OPP")), -1))
    End Sub

    Private Sub GetOPPInfo(ByVal id As Long)
        imgPhoto.ImageUrl = "../assets/img/No_Image.png"
        txtOPPName.Text = ""
        txtOPPICNo.Text = ""
        txtOverseerName.Text = ""
        txtOverseerContactNo.Text = ""
        txtPoliceNo.Text = ""
        txtPoliceStation.Text = ""
        txtDepartment.Text = ""
        txtPSContactNo.Text = ""
        txtIMEI.Text = ""
        plOPPInfo.Visible = False
        If id > 0 Then
            Dim myDataTable As DataTable = OPPManager.GetOPPList(id, "", "", -1, -1, -1, "", "")
            If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
                imgPhoto.ImageUrl = If(Not String.IsNullOrEmpty(myDataTable.Rows(0)("fldPhoto1")), myDataTable.Rows(0)("fldPhoto1"), "../assets/img/No_Image.png")
                txtOPPName.Text = myDataTable.Rows(0)("fldName")
                txtOPPICNo.Text = myDataTable.Rows(0)("fldICNo")
                txtOverseerName.Text = myDataTable.Rows(0)("fldOverseerName")
                txtOverseerContactNo.Text = myDataTable.Rows(0)("fldOverseerContactNo")
                txtPoliceNo.Text = myDataTable.Rows(0)("fldPoliceNo")
                txtPoliceStation.Text = myDataTable.Rows(0)("fldPSName")
                txtDepartment.Text = myDataTable.Rows(0)("fldDepartment")
                txtPSContactNo.Text = myDataTable.Rows(0)("fldPSContactNo")
                txtIMEI.Text = myDataTable.Rows(0)("fldIMEI")
                plOPPInfo.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        hfOPPID.Value = 0
        Dim oppid As Long = 0
        If ddlOPP.SelectedIndex > 0 Then
            oppid = ddlOPP.SelectedValue
        ElseIf ddlEMD.SelectedIndex > 0 Then
            oppid = OPPManager.GetOPPID(ddlEMD.SelectedValue, "", "")
        End If
        If oppid > 0 Then
            hfOPPID.Value = oppid
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "fetchAndUpdateMarkers();", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & GetText("ErrorNoResult") & "');clearMarkers();", True)
        End If
        GetOPPInfo(hfOPPID.Value)
    End Sub

    Protected Sub ddlOPP_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlOPP.SelectedIndex > 0 Then
            Dim deviceid As Long = OPPManager.GetEMDDeviceID(ddlOPP.SelectedValue)
            Try
                ddlEMD.SelectedValue = deviceid
            Catch ex As Exception
                ddlEMD.SelectedIndex = 0
            End Try
        End If
    End Sub

    Protected Sub ddlEMD_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlEMD.SelectedValue > 0 Then
            Dim oppid As Long = OPPManager.GetOPPID(ddlEMD.SelectedValue, "", "")
            Try
                ddlOPP.SelectedValue = oppid
            Catch ex As Exception
                ddlOPP.SelectedIndex = 0
            End Try
        End If
    End Sub

End Class