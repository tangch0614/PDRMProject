<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="OfficerList.aspx.vb" Inherits="PDRM_SPE.AOfficerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <h3 class="page-title" style="display: none;">
        <asp:Label runat="server" ID="lblPageTitle" Text="User List"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel runat="server" ID="upTable">
                <ContentTemplate>
                    <!-- SEARCH -->
                    <asp:Panel runat="server" ID="plTable">
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Search User</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSUserID" CssClass="col-md-3 control-label" Text="User ID"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSUserID" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSName" CssClass="col-md-3 control-label" Text="User Name"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSName" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSPoliceNo" CssClass="col-md-3 control-label" Text="I/C Number"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSPoliceNo" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSDepartment" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlSDepartment" runat="server" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSPoliceStation" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList ID="ddlSPoliceStation" runat="server" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlSStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div align="center">
                                                        <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                                        <asp:Button runat="server" CssClass="btn " ID="btnSReset" Text="Reset" OnClick="btnSReset_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-body">
                                        <!-- TABLE -->
                                        <asp:Repeater runat="server" ID="rptTable" OnItemCommand="rptTable_ItemCommand">
                                            <HeaderTemplate>
                                                <asp:HiddenField runat="server" ID="jqSearch" ClientIDMode="Static" Value='<%#GetText("Search")%>' />
                                                <asp:HiddenField runat="server" ID="jqShow" ClientIDMode="Static" Value='<%#GetText("Show")%>' />
                                                <asp:HiddenField runat="server" ID="jqEntries" ClientIDMode="Static" Value='<%#GetText("Entries")%>' />
                                                <asp:HiddenField runat="server" ID="jqTo" ClientIDMode="Static" Value='<%#GetText("To")%>' />
                                                <asp:HiddenField runat="server" ID="jqOf" ClientIDMode="Static" Value='<%#GetText("Of")%>' />
                                                <asp:HiddenField runat="server" ID="jqNoData" ClientIDMode="Static" Value='<%#GetText("ErrorNoData")%>' />
                                                <asp:HiddenField runat="server" ID="jqEmpty" ClientIDMode="Static" Value='<%#GetText("ErrorNoResult")%>' />
                                                <asp:HiddenField runat="server" ID="jqMatching" ClientIDMode="Static" Value='<%#GetText("ErrorNoMatchFound")%>' />
                                                <asp:HiddenField runat="server" ID="jqAll" ClientIDMode="Static" Value='<%#GetText("All")%>' />
                                                <asp:HiddenField runat="server" ID="jqExcel" ClientIDMode="Static" Value='<%#GetText("Excel")%>' />
                                                <asp:HiddenField runat="server" ID="jqPrint" ClientIDMode="Static" Value='<%#GetText("Print")%>' />
                                                <asp:HiddenField runat="server" ID="jqPrintInfo" ClientIDMode="Static" Value='<%#GetText("MsgPrintInstruction")%>' />
                                                <table class="table table-striped table-hover table-bordered managedTable" id="table1">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 5% !important">
                                                                <asp:Label runat="server" ID="lblHNo" Text='<%#GetText("Num")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHAdminID" Text='<%#GetText("Username")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHName" Text='<%#GetText("Name")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHPoliceNo" Text='<%#GetText("PoliceIDNo")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHContactNo" Text='<%#GetText("ContactNum")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHPoliceStation" ClientIDMode="Static"  Text='<%#GetText("PoliceStation")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHDepartment" Text='<%#GetText("Department")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 5% !important">
                                                                <asp:Label runat="server" ID="lblHStatus" Text='<%#GetText("Status")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <asp:Label runat="server" ID="lblHLastLogin" Text='<%#GetText("LastLogin")%>'></asp:Label>
                                                            </th>
                                                            <th style="width: 10% !important"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.ItemIndex + 1%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label runat="server" ID="txtAdminID" Text='<%#Eval("fldCode")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label runat="server" ID="txtName" Text='<%#Eval("fldName")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtPoliceNo" Text='<%#Eval("fldPoliceNo")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtContactNo" Text='<%#Eval("fldContactNo")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtPoliceStation" Text='<%#Eval("fldPSName")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtDepartment" Text='<%#Eval("fldDepartment")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("A"), GetText("Active"), GetText("Inactive"))%>' CssClass='<%#If(Eval("fldStatus").Equals("A"), "label label-success", "label label-danger")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtLastLogin" Text='<%#Eval("fldLastLogin")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnEdit" Text='<%#GetText("Update")%>' CommandName="editUser" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-sm " />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                        </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="plUpdate" Visible="false">
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblInfo" CssClass="caption-subject bold uppercase">UserDetail</asp:Label>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-body">
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblUserID" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:Label runat="server" ID="txtUserID" data-temp="1" CssClass="form-control input-inline input-large" Enabled="False" ReadOnly="True" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblName" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control input-inline input-large" />
                                            <label style="color: red">*</label>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblIC" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtIC" CssClass="form-control input-inline input-large" />
                                            <label style="color: red">*</label>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvIC" ControlToValidate="txtIC" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblContactNo" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control input-inline input-large" />
                                            <label style="color: red">*</label>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvContactNo" ControlToValidate="txtContactNo" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblPoliceNo" CssClass="col-md-3 control-label" Text="No. Polis"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtPoliceNo" CssClass="form-control input-inline input-large" />
                                            <%--<label style="color: red">*</label>--%>
                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvPoliceNo" ControlToValidate="txtPoliceNo" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblDepartment" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblPoliceStation" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlPoliceStation" runat="server" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblLevel" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control input-inline input-large" Enabled="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--<div class="form-group">
                                                        <asp:Label runat="server" ID="lblCountry" CssClass="col-md-3 control-label" Text="Country"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control input-inline input-large">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblLanguage" CssClass="col-md-3 control-label" Text="Preferred Language"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlLanguage" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","update")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn default" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
                                            <asp:Button runat="server" CssClass="btn default" ID="btnBack" Text="Reset" OnClick="btnBack_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Login Password -->
                        <div class="portlet light" runat="server" visible="false">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblLPasswordHeader" CssClass="caption-subject bold uppercase">Login Password</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblLPassword" CssClass="col-md-3 control-label" Text="Login Password"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" TextMode="Password" ID="txtLPassword" CssClass="form-control input-inline input-large" />
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ID="revLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password must more than 6 characters" ValidationExpression="[.\S]{6,}" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblLPasswordConfirm" CssClass="col-md-3 control-label" Text="Confirm Login Password"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" TextMode="Password" ID="txtLPasswordConfirm" CssClass="form-control input-inline input-large" />
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ErrorMessage="*Enter confirm login password" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator runat="server" ID="cvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ControlToCompare="txtLPassword" ErrorMessage="*Password not match" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnChangeLPassword" Text="Change" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","lpassword")){return false};' OnClick="btnChangeLPassword_Click" ClientIDMode="static" />
                                                <asp:Button runat="server" CssClass="btn default" ID="btnBack2" Text="Reset" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- END FORM-->
                            </div>
                        </div>
                        <!-- Login Password -->
                    </asp:Panel>
                    <!-- SEARCH -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="update" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <!--LOADING POPUP-->
    <asp:UpdateProgress runat="server" ID="upPopup" AssociatedUpdatePanelID="upTable">
        <ProgressTemplate>
            <div class="loadingPage">
                <img src="../assets/img/loadingCircle.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--LOADING POPUP-->

    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            $('#ddlSPoliceStation').select2();
            $('#ddlPoliceStation').select2();
            initTable("table1");
        }
    </script>
    <!--Javascript-->
</asp:Content>

