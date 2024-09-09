<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="PDRM_SPE.AChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <h3 class="page-title">
        <asp:Label runat="server" ID="lblPageTitle" Text="Change Password"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <!-- Login Password -->
            <div class="portlet light bordered boxShadow">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-key fa-fw"></i>
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Login Password</asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblCurrentLPassword" CssClass="col-md-3 control-label" Text="Current Login Password"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtCurrentLPassword" CssClass="form-control input-inline input-large"  />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvCurrentLPassword" ControlToValidate="txtCurrentLPassword" ErrorMessage="*Please enter current login password" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblLPassword" CssClass="col-md-3 control-label" Text="Login Password"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:TextBox runat="server" TextMode="Password" ID="txtLPassword" CssClass="form-control input-inline input-large" />
                                            <label style="color: red">*</label>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="revLPassword" ControlToValidate="txtLPassword" ErrorMessage="*Password must more than 6 characters" ValidationExpression="[.\S]{6,}" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RegularExpressionValidator>
                                            <asp:CompareValidator runat="server" ID="cvLPassword" ControlToValidate="txtLPassword" ControlToCompare="txtCurrentLPassword" ErrorMessage="*New password cannot same with current password" Display="Dynamic" ForeColor="red" Operator="NotEqual" ValidationGroup="lpassword"></asp:CompareValidator>
                                        </div>
                                    </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblLPasswordConfirm" CssClass="col-md-3 control-label" Text="Confirm Login Password"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" TextMode="Password" ID="txtLPasswordConfirm" CssClass="form-control input-inline input-large"  />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ErrorMessage="*Enter confirm login password" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ID="cvLPasswordConfirm" ControlToValidate="txtLPasswordConfirm" ControlToCompare="txtLPassword" ErrorMessage="*Password not match" Display="Dynamic" ForeColor="red" ValidationGroup="lpassword"></asp:CompareValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button runat="server" CssClass="btn blue " ID="btnChangeLPassword" Text="Change" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfTransactionConfirm","lpassword")){return false};' OnClick="btnChangeLPassword_Click" ClientIDMode="static" />
                                <asp:HiddenField runat="server" ID="hfTransactionConfirm" Value="Confirm change login password?" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
            <!-- Login Password -->
        </div>
    </div>
    <!-- PAGE BODY -->


    <!--Javascript-->
    <script>

    </script>
    <!--Javascript-->

</asp:Content>
