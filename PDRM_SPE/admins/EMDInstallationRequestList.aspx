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
                                            <asp:Label runat="server" ID="lblSDate" CssClass="col-md-3 control-label" Text="Transaction Date"></asp:Label>
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
                                            <asp:Label runat="server" ID="lblSPoliceStation" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSDepartment" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSDepartment" CssClass="form-control input-inline input-large"></asp:DropDownList>
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
                                                            <th style="width: 10% !important">
                                                                <%#GetText("Document")%>
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
                                                        <asp:LinkButton runat="server" Text='<%#GetText("OversightOrder")%>' OnClientClick='<%#"OpenPopupWindow(""" & Eval("fldAttachment1") & """,1280,800);return false;"%>' Visible='<%#Not String.IsNullOrWhiteSpace(Eval("fldAttachment1"))%>'></asp:LinkButton>
                                                        <asp:LinkButton runat="server" Text='<%#GetText("Attachment")%>' OnClientClick='<%#"OpenPopupWindow(""" & Eval("fldAttachment2") & """,1280,800);return false;"%>' Visible='<%#Not String.IsNullOrWhiteSpace(Eval("fldAttachment2"))%>'></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("Y"), GetText("Completed"), If(Eval("fldStatus").Equals("N"), GetText("Rejected"), GetText("Pending")))%>' CssClass='<%#If(Eval("fldStatus").Equals("Y"), "label label-success", If(Eval("fldStatus").Equals("N"), "label label-danger", "label label-warning"))%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnAcknowledge" Text='<%#If(Eval("fldStatus").Equals("P"), GetText("Acknowledge"), GetText("View"))%>' CommandName="acknowledge" CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 1%>' CssClass="btn blue btn-xs margin-bottom-5" />
                                                        <asp:Button runat="server" ID="btnEdit" Text='<%#GetText("Update")%>' CommandName="updaterequest" CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 3 AndAlso Eval("fldStatus").Equals("P")%>' CssClass="btn blue btn-xs margin-bottom-5" />
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
                                                <asp:Label runat="server" ID="lblDepartment" CssClass="col-md-3 control-label" Text="Department"></asp:Label>
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
                                                <asp:Label runat="server" ID="lblDate" CssClass="col-md-3 control-label" Text="Tarikh"></asp:Label>
                                                <div class="col-md-8">
                                                    <input id="txtDate" name="txtDate" class="DateFrom form-control input-inline input-large" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                </div>
                                                <asp:TextBox runat="server" ID="hfDate" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblTime" CssClass="col-md-3 control-label" Text="Masa"></asp:Label>
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
                                                        <asp:Label runat="server" ID="lblIPK" CssClass="col-md-3 control-label" Text="Kontinjen"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlIPK" CssClass="form-control input-inline input-large" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlIPK_SelectedIndexChanged"></asp:DropDownList>
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvIPK" ControlToValidate="ddlIPK" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblPoliceStation" CssClass="col-md-3 control-label" Text="Balai Police"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvPoliceStation" ControlToValidate="ddlPoliceStation" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" visible="false">
                                                        <asp:Label runat="server" ID="lblState" CssClass="col-md-3 control-label" Text="State"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                            <%--<label style="color: red">*</label>--%>
                                                            <%--<asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddlState" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblDistrict" CssClass="col-md-3 control-label" Text="District"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDistrict" ControlToValidate="ddlDistrict" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblMukim" CssClass="col-md-3 control-label" Text="Sub-District"></asp:Label>
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
                                                        <asp:Label runat="server" ID="lblOCSName" CssClass="col-md-3 control-label" Text="Nama KPB"></asp:Label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox runat="server" ID="txtOCSName" CssClass="form-control input-inline input-large" />
                                                            <label style="color: red">*</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOCSName" ControlToValidate="txtOCSName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="update"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ID="lblOCSContactNo" CssClass="col-md-3 control-label" Text="No. Tel. KBP"></asp:Label>
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
                                <!-- File Upload -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblFileInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblAttachment1" CssClass="col-md-3 control-label" Text="Perintah Pengawasan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:LinkButton runat="server" ID="lbtAttachment1" Text="Perintah Pegawasan"></asp:LinkButton>
                                                </div>
                                                <div class="col-md-8 col-md-offset-3">
                                                    <asp:TextBox runat="server" ID="txtAttachment1" CssClass="form-control input-inline input-large" onkeydown="return false;" onpaste="return false;" autocomplete="off" ClientIDMode="static"></asp:TextBox>
                                                    <%--<label style="color: red">*</label>--%>
                                                    <asp:Button runat="server" ID="btnAttachment1" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment1.click();return false;" />
                                                    <asp:Label runat="server" ID="lblAttachment1Validate" ForeColor="red" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblAttachment2" CssClass="col-md-3 control-label" Text="Lampiran"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:LinkButton runat="server" ID="lbtAttachment2" Text="Lampiran"></asp:LinkButton>
                                                </div>
                                                <div class="col-md-8 col-md-offset-3">
                                                    <asp:TextBox runat="server" ID="txtAttachment2" CssClass="form-control input-inline input-large" onkeydown="return false;" onpaste="return false;" autocomplete="off" ClientIDMode="static"></asp:TextBox>
                                                    <%--<label style="color: red">*</label>--%>
                                                    <asp:Button runat="server" ID="btnAttachment2" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment2.click();return false;" />
                                                    <asp:Label runat="server" ID="lblAttachment2Validate" ForeColor="red" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblRemark" CssClass="col-md-3 control-label" Text="Catatan"></asp:Label>
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
                    </asp:Panel>
                    <!-- SEARCH -->
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:FileUpload runat="server" ID="fuAttachment1" Style="display: none" ClientIDMode="static" onchange="txtAttachment1.value = filename(this);" />
    <asp:FileUpload runat="server" ID="fuAttachment2" Style="display: none" ClientIDMode="static" onchange="txtAttachment2.value = filename(this);" />
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="update" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <asp:Panel runat="server" ClientIDMode="Static" ID="plAcknowledge" CssClass="modal fade" TabIndex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="portlet light bordered" style="width: 100%; margin: 0 auto">
                    <div class="portlet-title">
                        <button type="button" class="close" aria-hidden="true" onclick="closemodal('popupform');"></button>
                        <div class="caption">
                            <i class="fa fa-check fa-fw"></i>
                            <asp:Label runat="server" ID="lblPInfo" CssClass="caption-subject bold uppercase" Text="Maklumat Pemohonan"></asp:Label>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
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
                                                <asp:Label runat="server" ID="lblPDepartment" CssClass="col-md-3 control-label" Text="Department"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDepartment" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOfficerName" CssClass="col-md-3 control-label" Text="Nama KPB"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOfficerName" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOfficerContactNo" CssClass="col-md-3 control-label" Text="No. Tel. KBP"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOfficerContactNo" CssClass="form-control input-inline input-large" />
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
                                                <asp:Label runat="server" ID="lblPDate" CssClass="col-md-3 control-label" Text="Tarikh"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDate" name="txtDate" class="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPTime" CssClass="col-md-3 control-label" Text="Masa"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPTime" class="form-control input-inline input-large" ClientIDMode="Static"></asp:Label>
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
                                                <asp:Label runat="server" ID="lblPIPK" CssClass="col-md-3 control-label" Text="Kontinjen"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPIPK" CssClass="form-control input-inline input-large" ClientIDMode="Static"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPPoliceStation" CssClass="col-md-3 control-label" Text="Balai Police"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPMukim" CssClass="col-md-3 control-label" Text="Sub-District"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
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
                                                <asp:Label runat="server" ID="lblPOCSName" CssClass="col-md-3 control-label" Text="Nama KPB"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSName" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOCSContactNo" CssClass="col-md-3 control-label" Text="No. Tel. KBP"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSContactNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- File Upload -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPFileInfo" CssClass="caption-subject uppercase" Text="Muat Naik Dokumen"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPAttachment1" CssClass="col-md-3 control-label" Text="Perintah Pengawasan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:LinkButton runat="server" ID="lbtPAttachment1" CssClass="form-control-static" Text="Perintah Pegawasan"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPAttachment2" CssClass="col-md-3 control-label" Text="Lampiran"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:LinkButton runat="server" ID="lbtPAttachment2" CssClass="form-control-static" Text="Lampiran"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRemark" CssClass="col-md-3 control-label" Text="Catatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtPRemark" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large"  Enabled="false"/>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPStatus" CssClass="col-md-3 control-label" Text="Catatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:label runat="server" ID="txtPStatus" CssClass="form-control-static" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" CssClass="btn blue " ID="btnPAcknowledge" Text="Maklum Terima" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPAcknowledge_Click" ClientIDMode="static" />
                                                    <asp:Button runat="server" CssClass="btn default " ID="btnPCancel" Text="Close" OnClick="btnPCancel_Click" ClientIDMode="static" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>

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
            if (document.getElementById("txtAttachment1") != null) {
                document.getElementById("txtAttachment1").value = filename(document.getElementById("fuAttachment1"));
            };
            if (document.getElementById("txtAttachment2") != null) {
                document.getElementById("txtAttachment2").value = filename(document.getElementById("fuAttachment2"));
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
    <!--Javascript-->
</asp:Content>

