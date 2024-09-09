<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AddUser.aspx.vb" Inherits="PDRM_SPE.AAddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <h3 class="page-title" style="display: none;">
        <asp:Label runat="server" ID="lblPageTitle" Text="Change Password"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Login Password</asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal">
                        <div class="form-body">
                            <asp:UpdatePanel runat="server" ID="upLoginID">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblLoginID" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" ID="txtLoginID" CssClass="form-control input-inline input-large" AutoPostBack="true" OnTextChanged="txtLoginID_TextChanged" />
                                            <label style="color: red">*</label>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvLoginID" ControlToValidate="txtLoginID" ErrorMessage="*Please enter login ID" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                            <asp:Label runat="server" ID="lblLoginIDValidate" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblName" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtName" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblIC" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtIC" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvIC" ControlToValidate="txtIC" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblContactNo" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvContactNo" ControlToValidate="txtContactNo" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblPoliceNo" CssClass="col-md-3 control-label" Text="No. Polis"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtPoliceNo" CssClass="form-control input-inline input-large" />
                                    <%--<label style="color: red">*</label>--%>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="rfvPoliceNo" ControlToValidate="txtPoliceNo" ErrorMessage="*IC number cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblRole" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control input-inline input-large"></asp:DropDownList>
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
                                <asp:Label runat="server" ID="lblLPassword" CssClass="col-md-3 control-label" Text="Login Password"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtLPassword" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="revLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password must more than 6 characters" ValidationExpression="[.\S]{6,}" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblLPasswordConfirm" CssClass="col-md-3 control-label" Text="Confirm Login Password"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtLPasswordConfirm" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ErrorMessage="*Enter confirm login password" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ControlToCompare="txtLPassword" ErrorMessage="*Password not match" Display="Dynamic" ForeColor="red" ValidationGroup="adduser"></asp:CompareValidator>
                                </div>
                            </div>
                            <%--<div class="form-group last">
                                <asp:Label runat="server" ID="lblCountry" CssClass="col-md-3 control-label" Text="Country"></asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                </div>
                            </div>--%>
                        </div>
                        <div class="form-actions">
                            <div class="pull-right">
                                <asp:Button runat="server" CssClass="btn blue" ID="btnAdd" Text="Add" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","adduser")){return false};' OnClick="btnAdd_Click" ClientIDMode="static" />
                                <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm add user?" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
    </div>
    <!-- PAGE BODY -->


    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            $('#ddlPoliceStation').select2();
        }
    </script>
    <!--Javascript-->

</asp:Content>
