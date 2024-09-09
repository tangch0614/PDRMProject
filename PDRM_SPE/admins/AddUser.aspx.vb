Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject

Public Class AAddUser
    Inherits ABase
    Private Property LoginIDValid() As Boolean
        Get
            If Not ViewState("LoginIDValid") Is Nothing Then
                Return CBool(ViewState("LoginIDValid"))
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            ViewState("LoginIDValid") = value
        End Set
    End Property
    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
            Initialize()
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("AddAdmin")
        lblHeader.Text = GetText("AddAdmin")
        lblName.Text = GetText("Name")
        rfvName.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Name"))
        lblIC.Text = GetText("ICNum")
        rfvIC.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ICNum"))
        lblContactNo.Text = GetText("ContactNum")
        rfvContactNo.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ContactNum"))
        lblPoliceNo.Text = GetText("PoliceIDNo")
        lblDepartment.Text = GetText("Department")
        lblPoliceStation.Text = GetText("PoliceStation")
        lblLoginID.Text = GetText("Username")
        rfvLoginID.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("Username"))
        lblLPassword.Text = GetText("NewPassword")
        rfvLPassword.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("NewPassword"))
        revLPassword.ErrorMessage = GetText("ErrorPasswordLength")
        lblLPasswordConfirm.Text = GetText("ConfirmPassword")
        rfvLPasswordConfirm.ErrorMessage = GetText("ErrorItemRequired").Replace("vITEM", GetText("ConfirmPassword"))
        cvLPasswordConfirm.ErrorMessage = GetText("ErrorConfirmPasswordCompare")
        'lblCountry.Text = GetText("Country")
        lblRole.Text = GetText("RoleLevel")
        'Buttons/Message
        btnAdd.Text = GetText("AddAdmin")
        hfConfirm.Value = GetText("MsgConfirmItem").Replace("vITEM", GetText("AddAdmin").ToLower)
    End Sub
#End Region

#Region "Initialize"

    Private Sub Initialize()
        GetRole()
        GetPoliceStation()
        GetDepartment()
        'GetCountry()
    End Sub

    Private Sub GetRole()
        ddlRole.DataSource = RoleManager.GetRoleList("A")
        ddlRole.DataTextField = "fldRoleName"
        ddlRole.DataValueField = "fldRoleID"
        ddlRole.DataBind()
        ddlRole.SelectedIndex = 0
    End Sub

    'Private Sub GetCountry()
    '    ddlCountry.DataSource = CountryManager.GetCountryList()
    '    ddlCountry.DataTextField = "fldName"
    '    ddlCountry.DataValueField = "fldID"
    '    ddlCountry.DataBind()
    '    ddlCountry.SelectedValue = Application("DefaultCountry")
    'End Sub

    Private Sub GetPoliceStation()
        ddlPoliceStation.DataSource = PoliceStationManager.GetPoliceStationList(-1, -1, -1, "", "", "", "", "Y")
        ddlPoliceStation.DataTextField = "fldName"
        ddlPoliceStation.DataValueField = "fldID"
        ddlPoliceStation.DataBind()
        ddlPoliceStation.Items.Insert(0, New ListItem(GetText("All"), 0, True))
        ddlPoliceStation.SelectedIndex = 0
    End Sub

    Private Sub GetDepartment()
        Dim datatable As DataTable = PoliceStationManager.GetDepartmentList("Y")
        ddlDepartment.DataSource = datatable
        ddlDepartment.DataTextField = "fldName"
        ddlDepartment.DataValueField = "fldID"
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, New ListItem(GetText("SelectItem").Replace("vITEM", GetText("Department")), 0))
        ddlDepartment.SelectedIndex = 0
    End Sub

#End Region


#Region "Save"

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        If LoginIDValid Then
            If AddUser() Then
                UtilityManager.SaveLog(0, AdminAuthentication.GetUserData(2), "ADD ADMIN", "Admin Code: " & txtLoginID.Text, "")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgAddItemSuccess").Replace("vITEM", GetText("User")) & "');window.location.href='../admins/UserList.aspx'", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorAddItemFailed").Replace("vITEM", GetText("User")) & "');", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorPageInvalid") & "');", True)
        End If
    End Sub

    Private Function AddUser() As Boolean
        Dim admin As AdminObj = New AdminObj()
        admin.fldName = txtName.Text
        admin.fldCode = txtLoginID.Text
        admin.fldICNo = txtIC.Text
        admin.fldPoliceNo = txtPoliceNo.Text
        admin.fldContactNo = txtContactNo.Text
        admin.fldDeptID = ddlDepartment.SelectedValue
        admin.fldPoliceStationID = ddlPoliceStation.SelectedValue
        admin.fldPassword = txtLPassword.Text
        admin.fldTransactionPassword = txtLPassword.Text
        'admin.fldLanguage = Application("DefaultLanguage")
        'admin.fldCountryID = ddlCountry.SelectedValue
        admin.fldLevel = ddlRole.SelectedValue
        admin.fldStatus = "A"
        admin.fldCreatorDateTime = UtilityManager.GetServerDateTime()
        admin.fldCreator = AdminAuthentication.GetUserData(2)
        Dim result As Long = AdminManager.Save(admin)
        Return result > 0
    End Function
#End Region

    Protected Sub txtLoginID_TextChanged(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(txtLoginID.Text) Then
            lblLoginIDValidate.Visible = True
            If UtilityManager.ValidateString(txtLoginID.Text) AndAlso Not txtLoginID.Text.Length < 6 Then
                Dim duplicateLoginID As Boolean = AdminManager.VerifyLoginID(txtLoginID.Text)
                If duplicateLoginID Then
                    lblLoginIDValidate.Text = GetText("ErrorLoginIDUnavailable")
                    lblLoginIDValidate.ForeColor = Drawing.Color.Red
                    LoginIDValid = False
                Else
                    lblLoginIDValidate.Text = GetText("MsgLoginIDAvailable")
                    lblLoginIDValidate.ForeColor = Drawing.Color.Blue
                    LoginIDValid = True
                End If
            Else
                lblLoginIDValidate.Text = GetText("ErrorLoginIDSpecialChar")
                lblLoginIDValidate.ForeColor = Drawing.Color.Red
                LoginIDValid = False
            End If
        Else
            lblLoginIDValidate.Visible = False
            LoginIDValid = False
        End If
    End Sub
End Class