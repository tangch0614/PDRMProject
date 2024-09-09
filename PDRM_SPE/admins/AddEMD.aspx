<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AddEMD.aspx.vb" Inherits="PDRM_SPE.AAddEMD" %>

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
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">UserDetail</asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblImei" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtImei" data-temp="1" CssClass="form-control input-inline input-large"></asp:TextBox>
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvImei" ControlToValidate="txtImei" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblSimNo" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtSimNo" CssClass="form-control input-inline input-large" />
                                    <label style="color: red">*</label>
                                    <asp:RequiredFieldValidator runat="server" ID="rfvSimNo" ControlToValidate="txtSimNo" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="add"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblSimNo2" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                <div class="col-md-8">
                                    <asp:TextBox runat="server" ID="txtSimNo2" CssClass="form-control input-inline input-large" />
                                    <%--<label style="color: red">*</label>--%>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="rfvSimNo" ControlToValidate="txtSimNo" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="add"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                <div class="col-md-8">
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="pull-right">
                                <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","add")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="update" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            initTable("table1");
        }
    </script>
    <!--Javascript-->
</asp:Content>

