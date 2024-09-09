<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="IPDList.aspx.vb" Inherits="PDRM_SPE.AIPDList" %>

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
                                            <asp:Label runat="server" ID="lblSName" CssClass="col-md-3 control-label" Text="User ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSName" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSIPK" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSIPK" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblSState" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSState" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSDistrict" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSDistrict" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>--%>
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
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Name")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("ContactNum")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("IPK")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("State")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("District")%>
                                                            </th>
                                                            <%--<th style="width: 10% !important">
                                                                <%#GetText("Status")%>
                                                            </th>--%>
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
                                                        <%#Eval("fldName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldContactNo")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldIPKName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldState")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldDistrict")%>
                                                    </td>
                                                    <%--<td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("Y"), GetText("Active"), GetText("Inactive"))%>' CssClass='<%#If(Eval("fldStatus").Equals("Y"), "label label-success", "label label-danger")%>'></asp:Label>
                                                    </td>--%>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnEdit" Text='<%#GetText("Update")%>' CommandName="updateps" CommandArgument='<%#Eval("fldID")%>' CssClass="btn blue btn-xs " />
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
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblName" CssClass="col-md-3 control-label" Text="Current Login ID"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtName" data-temp="1" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblContactNo" CssClass="col-md-3 control-label" Text="Name"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control input-inline input-large" />
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvContactNo" ControlToValidate="txtContactNo" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblIPK" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlIPK" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvIPK" ControlToValidate="ddlIPK" InitialValue="" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblState" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlState" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddlState" InitialValue="" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblDistrict" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlDistrict" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDistrict" ControlToValidate="ddlDistrict" InitialValue="" ErrorMessage="*Name cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
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
            $('#ddlSState').select2();
            $('#ddlSIPK').select2();
            $('#ddlState').select2();
            $('#ddlDistrict').select2();
            $('#ddlIPK').select2();
        }
    </script>
    <!--Javascript-->
</asp:Content>

