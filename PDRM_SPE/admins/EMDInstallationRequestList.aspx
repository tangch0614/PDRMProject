<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="EMDInstallationRequestList.aspx.vb" Inherits="PDRM_SPE.AEMDInstallationRequestList" %>

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
                                            <asp:Label runat="server" ID="lblSDate" CssClass="col-md-4 control-label" Text="Transaction Date"></asp:Label>
                                            <div class="col-md-8">
                                                <div class="input-group input-large">
                                                    <div class="input-group-addon">
                                                        <asp:Label runat="server" ID="lblFrom"><%=gettext("From")%></asp:Label>
                                                    </div>
                                                    <input id="txtDateFrom" name="txtDateFrom" class="DateFrom form-control" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                </div>
                                                <div class="input-group input-large margin-top-10">
                                                    <div class="input-group-addon">
                                                        <asp:Label runat="server" ID="lblTo"><%=gettext("To")%></asp:Label>
                                                    </div>
                                                    <input id="txtDateTo" name="txtDateTo" class="DateTo form-control" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                </div>
                                            </div>
                                            <asp:TextBox runat="server" ID="hfDateFrom" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="hfDateTo" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSPoliceStation" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSDepartment" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSDepartment" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSStatus" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-4 col-md-8">
                                                <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                                <asp:Button runat="server" CssClass="btn default" ID="btnSReset" Text="Reset" OnClick="btnSReset_Click" />
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
                                                                <%#GetText("DateTime")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("InstallationDate")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OfficerItem").replace("vITEM", gettext("Name"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OfficerItem").replace("vITEM", gettext("ContactNum"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Department")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("PoliceStation")%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OCSItem").replace("vITEM", gettext("Name"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("OCSItem").replace("vITEM", gettext("ContactNum"))%>
                                                            </th>
                                                            <th style="width: 10% !important">
                                                                <%#GetText("State") & "/" & GetText("Township")%>
                                                            </th>
                                                            <th style="width: 5% !important">
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
                                                    <td style="text-align: center">
                                                        <%#Eval("fldDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("fldInstallDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOffName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOffContactNo")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("fldDepartment")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("fldPSName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOCSName")%>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <%#Eval("fldOCSTelNo")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("fldState") & " / " & Eval("fldMukim")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("Y"), GetText("Completed"), If(Eval("fldStatus").Equals("A"), GetText("Acknowledge"), If(Eval("fldStatus").Equals("N"), GetText("Rejected"), GetText("Pending"))))%>' CssClass='<%#If(Eval("fldStatus").Equals("Y"), "label label-success", If(Eval("fldStatus").Equals("A"), "label label-info", If(Eval("fldStatus").Equals("N"), "label label-danger", "label label-warning")))%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnProcess" Text='<%#If(Eval("fldStatus").Equals("P"), GetText("Process"), GetText("View"))%>' CommandName="processrequest" CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 1%>' CssClass="btn blue btn-xs margin-bottom-5" />
                                                        <asp:Button runat="server" ID="btnUpdate" Text='<%#If(Eval("fldStatus").Equals("P"), GetText("Update"), GetText("View"))%>' CommandName="updaterequest" CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 3%>' CssClass="btn blue btn-xs margin-bottom-5" />
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
                    <!--OFFICER-->
                    <asp:Panel runat="server" ID="plOfficer" Visible="false">
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblInfo" CssClass="caption-subject bold uppercase">UserDetail</asp:Label>
                                </div>
                            </div>
                            <div class="portlet light">
                                <!-- DEPARTMENT INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblDepartmentInfo" CssClass="caption-subject uppercase" Text="Department"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblDepartment" CssClass="col-md-4 control-label" Text="Department"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                    <label style="color: red">*</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvDepartment" ControlToValidate="ddlDepartment" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- INSTALL INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblInstallInfo" CssClass="caption-subject uppercase" Text="Butir-Butir Pemasangan"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblDate" CssClass="col-md-4 control-label" Text="Tarikh"></asp:Label>
                                                <div class="col-md-8">
                                                    <input id="txtDate" name="txtDate" class="DateFrom form-control input-inline input-large" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                </div>
                                                <asp:TextBox runat="server" ID="hfDate" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblTime" CssClass="col-md-4 control-label" Text="Masa"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlTime" class="form-control input-inline input-large" ClientIDMode="Static"></asp:DropDownList>
                                                    <label style="color: red">*</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvTime" ControlToValidate="ddlTime" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- INSTALL LOCATION -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblInstallLocation" CssClass="caption-subject uppercase" Text="Tempat Pemasangan"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblIPK" CssClass="col-md-4 control-label" Text="Kontinjen"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlIPK" CssClass="form-control input-inline input-large" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlIPK_SelectedIndexChanged"></asp:DropDownList>
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPK" ControlToValidate="ddlIPK" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblPoliceStation" CssClass="col-md-4 control-label" Text="Balai Police"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvPoliceStation" ControlToValidate="ddlPoliceStation" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" visible="false">
                                                        <asp:Label runat="server" ID="lblState" CssClass="col-md-4 control-label" Text="State"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                            <%--<label style="color: red">*</label>--%>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddlState" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblDistrict" CssClass="col-md-4 control-label" Text="District"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDistrict" ControlToValidate="ddlDistrict" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblMukim" CssClass="col-md-4 control-label" Text="Sub-District"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvMukim" ControlToValidate="ddlMukim" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlIPK" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <!-- OCS INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblOCSInfo" CssClass="caption-subject uppercase" Text="Maklumat Ketua Polis Balai"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblOCSName" CssClass="col-md-4 control-label" Text="Nama KPB"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtOCSName" CssClass="form-control input-inline input-large" />
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOCSName" ControlToValidate="txtOCSName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblOCSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. KBP"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtOCSContactNo" CssClass="form-control input-inline input-large" />
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOCSContactNo" ControlToValidate="txtOCSContactNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <!-- OTHER INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblOtherInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblRemark" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Kemaskini" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","update")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                                                    <asp:Button runat="server" CssClass="btn default " ID="btnReset" Text="Set Semula" OnClick="btnReset_Click" ClientIDMode="static" />
                                                    <asp:Button runat="server" CssClass="btn default " ID="btnBack" Text="Kembali" OnClick="btnBack_Click" ClientIDMode="static" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- File Upload -->
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblFileInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblAttachment1" CssClass="col-md-4 control-label" Text="Perintah Pengawasan"></asp:Label>
                                            <asp:Panel runat="server" ID="plAttachment1">
                                                <div class="col-md-8">
                                                    <asp:Button runat="server" ID="btnAttachment1" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment1.click();return false;" />
                                                    <asp:Button runat="server" ID="btnSubmitAttachment1" Text="Update" CssClass="btn blue " OnClientClick='return confirm(hfConfirm.value);' OnClick="btnSubmitAttachment1_Click" ClientIDMode="static" />
                                                    <asp:Label runat="server" ID="lblAttachment1Validate" ForeColor="red" Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-offset-4 col-md-8 margin-top-10">
                                                    <table class="table table-bordered" style="width: auto">
                                                        <tbody id="dvAttachment1Preview"></tbody>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-8 col-md-offset-4">
                                                <table class="table table-bordered table-striped" style="width: auto;">
                                                    <asp:Repeater runat="server" ID="rptAttachment1" OnItemCommand="rptAttachment1_ItemCommand" OnItemCreated="rptAttachment1_ItemCreated">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("FileName") %></td>
                                                                <td>
                                                                    <a href="#" class="btn blue btn-sm" onclick="OpenPopupWindow('<%#Eval("FilePath")%>', 1280, 800); return false;">
                                                                        <i class="fa fa-folder-open"></i>
                                                                    </a>
                                                                    <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn red btn-sm" OnClientClick='return confirm(hfConfirm2.value);' CommandName="deletefile" CommandArgument='<%#Container.ItemIndex%>'>
                                                                        <i class="fa fa-trash-o"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!--VENDOR-->
                    <asp:Panel runat="server" ID="plVendor" Visible="false">
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="fa fa-check fa-fw"></i>
                                    <asp:Label runat="server" ID="lblPInfo" CssClass="caption-subject bold uppercase" Text="Maklumat Pemohonan"></asp:Label>
                                </div>
                                <div class="actions">
                                    <asp:Button runat="server" CssClass="btn default " ID="btnPBackTop" Text="Close" OnClick="btnPBack_Click" ClientIDMode="static" />
                                </div>
                            </div>
                            <div class="portlet light">
                                <!-- DEPARTMENT INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPDepartmentInfo" CssClass="caption-subject uppercase" Text="Department"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPDepartment" CssClass="col-md-4 control-label" Text="Department"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDepartment" CssClass="form-control input-inline input-large" readonly="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOfficerName" CssClass="col-md-4 control-label" Text="Nama KPB"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOfficerName" CssClass="form-control input-inline input-large" readonly="true" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOfficerContactNo" CssClass="col-md-4 control-label" Text="No. Tel. KBP"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOfficerContactNo" CssClass="form-control input-inline input-large" readonly="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- INSTALL INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPInstallInfo" CssClass="caption-subject uppercase" Text="Butir-Butir Pemasangan"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPDate" CssClass="col-md-4 control-label" Text="Tarikh"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDate" name="txtDate" class="form-control input-inline input-large" readonly="true" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPTime" CssClass="col-md-4 control-label" Text="Masa"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPTime" class="form-control input-inline input-large" ClientIDMode="Static" readonly="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- INSTALL LOCATION -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPInstallLocation" CssClass="caption-subject uppercase" Text="Tempat Pemasangan"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPIPK" CssClass="col-md-4 control-label" Text="Kontinjen"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPIPK" CssClass="form-control input-inline input-large" ClientIDMode="Static" readonly="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPPoliceStation" CssClass="col-md-4 control-label" Text="Balai Police"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large" readonly="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPMukim" CssClass="col-md-4 control-label" Text="Sub-District"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large" readonly="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- OCS INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPOCSInfo" CssClass="caption-subject uppercase" Text="Maklumat Ketua Polis Balai"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOCSName" CssClass="col-md-4 control-label" Text="Nama KPB"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSName" CssClass="form-control input-inline input-large" readonly="true" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOCSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. KBP"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSContactNo" CssClass="form-control input-inline input-large" readonly="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- OTHER INFO -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPOtherInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPAttachment1" CssClass="col-md-4 control-label" Text="Lampiran"></asp:Label>
                                                <div class="col-md-8">
                                                    <table class="table table-bordered table-striped" style="width: auto;">
                                                        <asp:Repeater runat="server" ID="rptPAttachment1">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Eval("FileName") %></td>
                                                                    <td>
                                                                        <a href="#" class="btn blue btn-sm" onclick="OpenPopupWindow('<%#Eval("FilePath")%>', 1280, 800); return false;">
                                                                            <i class="fa fa-folder-open"></i>
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRemark" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtPRemark" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- EMD INFO -->
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblPEMDDeviceInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPOPP" CssClass="col-md-4 control-label" Text="OPP"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlPOPP" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <%--<label style="color: red">*</label>--%>
                                                <div>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvPOPP" ControlToValidate="ddlPOPP" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="acknowledge" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPEMD" CssClass="col-md-4 control-label" Text="IMEI"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlPEMD" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <%--<label style="color: red">*</label>--%>
                                                <div>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvPEMD" ControlToValidate="ddlPEMD" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="acknowledge" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-8 col-lg-offset-4">
                                                <table>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPSmartTag" CssClass="checkbox" Style="margin-left: 20px" Text="SmartTag" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPSmartTagCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPOBC" CssClass="checkbox" Style="margin-left: 20px" Text="OBC" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPOBCCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPBeacon" CssClass="checkbox" Style="margin-left: 20px;" Text="Beacon" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPBeaconCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPCharger" CssClass="checkbox" Style="margin-left: 20px" Text="Charger" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPChargerCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPStrap" CssClass="checkbox" Style="margin-left: 20px" Text="Strap" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPStrapCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:CheckBox runat="server" ID="chkPCable" CssClass="checkbox" Style="margin-left: 20px" Text="Cable" /></td>
                                                        <td style="padding: 1px 10px;">
                                                            <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtPCableCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnPUpdateEMD" Text="Maklum Terima" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPUpdateEMD_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- STATUS INFO -->
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblPStatusInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPRemark2" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtPRemark2" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPStatus" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlPStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnPUpdateStatus" Text="Maklum Terima" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPUpdateStatus_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn default " ID="btnPBackBottom" Text="Close" OnClick="btnPBack_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- File Upload -->
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblPFileInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPAttachment2" CssClass="col-md-4 control-label" Text="Lampiran"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Button runat="server" ID="btnPAttachment2" Text="Select File" CssClass="btn blue " OnClientClick="fuPAttachment2.click();return false;" />
                                                <asp:Button runat="server" ID="btnPSubmitAttachment2" Text="Update" CssClass="btn blue " OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPSubmitAttachment2_Click" ClientIDMode="static" />
                                                <asp:Label runat="server" ID="lblPAttachment2Validate" ForeColor="red" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-offset-4 col-md-8 margin-top-10">
                                                <div id="dvPAttachment2Preview"></div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-8 col-md-offset-4">
                                                <div>
                                                    <asp:Repeater runat="server" ID="rptPAttachment2" OnItemCommand="rptPAttachment2_ItemCommand" OnItemCreated="rptPAttachment2_ItemCreated">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; margin: 5px; border: 1px solid #ccc">
                                                                <div style='position: relative; background-image: url(<%# Eval("filePath") %>); width: 200px; height: 200px; background-size: cover; background-repeat: no-repeat; background-position: center'>
                                                                    <div style="display: block; padding: 5px; position: absolute; bottom: 0; width: 100%; text-align: right; background: rgba(0,0,0,0.5);">
                                                                        <a href="#" class="btn blue btn-sm" onclick="OpenPopupWindow('<%#Eval("FilePath")%>', 1280, 800); return false;">
                                                                            <i class="fa fa-folder-open"></i>
                                                                        </a>
                                                                        <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn red btn-sm" OnClientClick='return confirm(hfConfirm2.value);' CommandName="deletefile" CommandArgument='<%#Container.ItemIndex%>'>
                                                                        <i class="fa fa-trash-o"></i>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPUpdateEMD" />
                    <asp:AsyncPostBackTrigger ControlID="btnPUpdateStatus" />
                    <asp:PostBackTrigger ControlID="btnPSubmitAttachment2" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                    <asp:PostBackTrigger ControlID="btnSubmitAttachment1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:FileUpload runat="server" ID="fuAttachment1" multiple="multiple" Style="display: none" ClientIDMode="static" onchange="listSelectedFile(this,'dvAttachment1Preview');" />
    <asp:FileUpload runat="server" ID="fuPAttachment2" multiple="multiple" accept=".jpg, .jpeg, .png, .gif, .bmp" Style="display: none" ClientIDMode="static" onchange="listSelectedImg(this,'dvPAttachment2Preview');" />
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfConfirm2" Value="Confirm action?" ClientIDMode="Static" />
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
            initBasicDatePicker("#txtDate", "#hfDate");
            initDatePickerNoMax("#txtDateFrom", "#hfDateFrom", "#txtDateTo", "#hfDateTo");
            initTable("table1");
            $('#ddlSPoliceStation').select2();
            $('#ddlIPK').select2();
            $('#ddlPoliceStation').select2();
            $('#ddlMukim').select2();
            $('#ddlPOPP').select2();
            $('#ddlPEMD').select2();
            if (document.getElementById("txtAttachment1") != null) {
                document.getElementById("txtAttachment1").value = filename(document.getElementById("fuAttachment1"));
            };
            if (document.getElementById("txtPAttachment2") != null) {
                document.getElementById("txtPAttachment2").value = filename(document.getElementById("fuPAttachment2"));
            };
        }

        function filename(fileupload) {
            if (fileupload.value != "") { return fileupload.files[0].name; }
            else { return ""; };
        }

        function previewimg(event, desimg) {
            var output = document.getElementById(desimg);
            output.src = URL.createObjectURL(event.target.files[0]);
            output.onload = function () {
                URL.revokeObjectURL(output.src) // free memory
            }
        };
    </script>

    <script type="text/javascript">

        function listSelectedFile(input, previewid) {
            var selectedFiles = [];
            var files = input.files;
            for (var i = 0; i < files.length; i++) {
                selectedFiles.push(files[i]);
            }

            var fileList = document.getElementById(previewid);
            fileList.innerHTML = ""; // Clear the list

            // Display each file with a remove button
            selectedFiles.forEach(function (file, index) {
                var tr = document.createElement("tr");

                //var tdPreview = document.createElement("td");
                //if (file.type.startsWith("image/")) {
                //    var img = document.createElement("img");
                //    img.src = URL.createObjectURL(file);
                //    //img.style.width = "50px";
                //    img.style.height = "50px";
                //    img.onload = function () {
                //        URL.revokeObjectURL(img.src); // Free memory
                //    };
                //    tdPreview.appendChild(img);
                //} else {
                //    var icon = document.createElement("img");
                //    icon.src = "../assets/img/textfile.png";
                //    icon.style.width = "50px";
                //    icon.style.height = "50px";
                //    tdPreview.appendChild(icon);
                //}
                //tr.appendChild(tdPreview);

                // Create a cell for the file name
                var tdFileName = document.createElement("td");
                tdFileName.textContent = file.name;
                tr.appendChild(tdFileName);

                // Create a cell for the file size
                var trfileSize = document.createElement("td");
                trfileSize.textContent = (file.size / 1024 / 1024).toFixed(2) + " MB";
                tr.appendChild(trfileSize);

                fileList.appendChild(tr);
            });
        }


        function listSelectedImg(input, previewid) {
            var selectedFiles = [];
            var files = input.files;
            for (var i = 0; i < files.length; i++) {
                selectedFiles.push(files[i]);
            }

            var fileList = document.getElementById(previewid);
            fileList.innerHTML = ""; // Clear the list

            // Display each file with a remove button
            selectedFiles.forEach(function (file, index) {
                var div = document.createElement("div");
                div.style.display = "inline-block";
                div.style.padding = "5px";

                var divPreview = document.createElement("div");
                divPreview.style.display = "block";
                if (file.type.startsWith("image/")) {
                    var img = document.createElement("img");
                    img.src = URL.createObjectURL(file);
                    //img.style.width = "50px";
                    img.style.height = "100px";
                    img.onload = function () {
                        URL.revokeObjectURL(img.src); // Free memory
                    };
                    divPreview.appendChild(img);
                } else {
                    var icon = document.createElement("img");
                    icon.src = "../assets/img/textfile.png";
                    //icon.style.width = "50px";
                    icon.style.height = "100px";
                    divPreview.appendChild(icon);
                }
                div.appendChild(divPreview);

                // Create a cell for the file name
                var divFileName = document.createElement("div");
                divFileName.style.display = "block";
                divFileName.textContent = (file.size / 1024 / 1024).toFixed(2) + " MB";
                div.appendChild(divFileName);

                fileList.appendChild(div);
            });
        }
    </script>
    <!--Javascript-->
</asp:Content>

