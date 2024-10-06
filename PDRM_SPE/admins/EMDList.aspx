<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="EMDList.aspx.vb" Inherits="PDRM_SPE.AEMDList" %>

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
                    <asp:Panel runat="server" ID="plTable" Style="padding-bottom: 10px">
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Search User</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSImei" CssClass="col-md-3 control-label" Text="User ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSImei" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSName" CssClass="col-md-3 control-label" Text="User ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSName" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSSN" CssClass="col-md-3 control-label" Text="User ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSSN" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSSize" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSSize" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                                <asp:Button runat="server" CssClass="btn " ID="btnSReset" Text="Reset" OnClick="btnSReset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
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
                                                                <%#GetText("Num")%>
                                                            </th>
                                                            <th style="width: 5% !important"></th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Imei")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Marking")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("SerialNum")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Size")%>
                                                            </th>
                                                            <th style="width: 10% !important; display: none;">
                                                                <%#GetText("SIMNo")%>
                                                            </th>
                                                            <th style="width: 10% !important; display: none;">
                                                                <%#GetText("SIMNo") & " 2"%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OPPItem").Replace("vITEM", GetText("Name"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OPPItem").Replace("vITEM", GetText("ICNum"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Status")%>
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
                                                        <asp:Button runat="server" ID="btnEdit" Text='<%#GetText("Update")%>' CommandName="update" CommandArgument='<%#Eval("fldID")%>' CssClass="btn blue btn-xs " />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldImei")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldSN")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("fldSize")%>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%#Eval("fldSImNo")%>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%#Eval("fldSImNo2")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOPPName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOPPICNo")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("Y"), GetText("Active"), GetText("Inactive"))%>' CssClass='<%#If(Eval("fldStatus").Equals("Y"), "label label-success", "label label-danger")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnLockEMD" Text='<%#GetText("Lock")%>' OnClientClick="return confirm(hfConfirm2.value);" CommandName="lock" CommandArgument='<%#Eval("fldID")%>' CssClass="btn purple btn-xs " />
                                                        <asp:Button runat="server" ID="btnUnlockEMD" Text='<%#GetText("Unlock")%>' OnClientClick="return confirm(hfConfirm2.value);" CommandName="unlock" CommandArgument='<%#Eval("fldID")%>' CssClass="btn red btn-xs " />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                        </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <div class=" pull-right">
                                            <asp:Button runat="server" CssClass="btn blue" ID="btnAdd" Text="Search" OnClick="btnAdd_Click" />
                                        </div>
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
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblImei" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtImei" data-temp="1" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblName" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtName" data-temp="1" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                <%--<label style="color: red">*</label>--%>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSN" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSN" data-temp="1" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                <%--<label style="color: red">*</label>--%>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="add"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSize" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSize" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSimNo" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSimNo" CssClass="form-control input-inline input-large" />
                                                <%--<label style="color: red">*</label>--%>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvSimNo" ControlToValidate="txtSimNo" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSimNo2" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSimNo2" CssClass="form-control input-inline input-large" />
                                                <%--<label style="color: red">*</label>--%>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvSimNo" ControlToValidate="txtSimNo" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","update")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn default" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
                                            <asp:Button runat="server" CssClass="btn default" ID="btnBack" Text="Reset" OnClick="btnBack_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- SEARCH -->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfConfirm2" Value="Confirm update details?" ClientIDMode="Static" />
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
            initTable("table1");
        }
    </script>
    <!--Javascript-->
</asp:Content>

