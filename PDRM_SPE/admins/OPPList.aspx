<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="OPPList.aspx.vb" Inherits="PDRM_SPE.AOPPList" %>

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
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSName" CssClass="col-md-3 control-label" Text="Nama"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSName" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSICNo" CssClass="col-md-3 control-label" Text="No. K/P"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSICNo" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSOrdRefNo" CssClass="col-md-3 control-label" Text="No. Rujukan Perintah"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtSOrdRefNo" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSDepartment" CssClass="col-md-3 control-label" Text="Jabatan"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSDepartment" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSPoliceStation" CssClass="col-md-3 control-label" Text="Balai Police"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblSEMD" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSEMD" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblSVerifyStatus" CssClass="col-md-3 control-label" Text="Status"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlSVerifyStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div align="center">
                                                <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                                <asp:Button runat="server" CssClass="btn " ID="btnSReset" Text="Reset" OnClick="btnSReset_Click" />
                                            </div>
                                        </div>
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
                                                    <th style="width: 10% !important">
                                                        <%#GetText("Num")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("Name")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("ICNum")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("EMD")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("OrderRefNo")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("OrderCategory")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("OrderPeriod")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("InstallationDate")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("State") & "/ " & GetText("District") & "/ " & GetText("Township")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("PoliceStation") & "/ " & GetText("Department")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("VerifyStatus")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("Status")%>
                                                    </th>
                                                    <th style="width: 10% !important">
                                                        <%#GetText("Map")%>
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
                                                <%#Eval("fldName")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldICNo")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldImei")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldOrdRefNo")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldActs") & "/ " & Eval("fldActsSection")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldOrdFrDate", "{0:yyyy-MM-dd}") & " - " & Eval("fldOrdToDate", "{0:yyyy-MM-dd}") _
                                                                                                & If(CInt(Eval("fldOrdYear")) > 0, String.Format(" ({0} {1})", Eval("fldOrdYear"), GetText("Year")), "") _
                                                                                                & If(CInt(Eval("fldOrdMonth")) > 0, String.Format(" ({0} {1})", Eval("fldOrdMonth"), GetText("Month")), "") _
                                                                                                & If(CInt(Eval("fldOrdDay")) > 0, String.Format(" ({0} {1})", Eval("fldOrdDay"), GetText("Day")), "")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldEMDInstallDate", "{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldState") & "/ " & Eval("fldDistrict") & "/ " & Eval("fldMukim")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("fldPSName") & " / " & Eval("fldDepartment")%>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label runat="server" ID="txtVerifyStatus" Text='<%#If(Eval("fldVerifyStatus").Equals("Y"), GetText("Approved"), If(Eval("fldVerifyStatus").Equals("N"), GetText("Rejected"), GetText("Pending")))%>' CssClass='<%#If(Eval("fldVerifyStatus").Equals("Y"), "label label-success", If(Eval("fldVerifyStatus").Equals("N"), "label label-danger", "label label-warning"))%>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Label runat="server" ID="txtStatus" Text='<%#If(Eval("fldStatus").Equals("Y"), GetText("Active"), If(Eval("fldStatus").Equals("N"), GetText("Inactive"), GetText("Pending")))%>' CssClass='<%#If(Eval("fldStatus").Equals("Y"), "label label-success", If(Eval("fldStatus").Equals("N"), "label label-danger", "label label-warning"))%>'></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:LinkButton runat="server" ID="lbtShowMap" Text='<%#GetText("ShowMap")%>' OnClientClick='<%#GetLink(Eval("fldID"), "showmap")%>' CommandArgument='<%#Eval("fldID")%>' CssClass="btn blue btn-xs margin-bottom-5" />
                                                <asp:LinkButton runat="server" ID="lbtShowHistory" Text='<%#GetText("HistoryMap")%>' OnClientClick='<%#GetLink(Eval("fldID"), "showhist")%>' CommandArgument='<%#Eval("fldID")%>' CssClass="btn blue btn-xs " />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button runat="server" ID="btnVerify" Text='<%#If(Eval("fldVerifyStatus").Equals("P"), GetText("Verify"), GetText("View"))%>' CommandName="verify" CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 3%>' CssClass="btn blue btn-xs margin-bottom-5" />
                                                <%--<asp:LinkButton runat="server" ID="lbtGeofence" Text='<%#GetText("Geofence")%>' OnClientClick='<%#GetLink(Eval("fldID"), "geofence")%>' CommandArgument='<%#Eval("fldID")%>' Visible='<%#GetUserType() = 1%>' CssClass="btn blue btn-xs margin-bottom-5" />--%>
                                                <%--<asp:LinkButton runat="server" ID="lbtMukim" Text='<%#GetText("SetTownship")%>' OnClientClick="return false;" CssClass="btn blue btn-xs margin-bottom-5" Visible='<%#GetUserType() = 1%>' />--%>
                                                <asp:Button runat="server" ID="btnEdit" Text='<%#GetText("Update")%>' CommandName="updateopp" CommandArgument='<%#Eval("fldID")%>' CssClass="btn blue btn-xs " Visible='<%#GetUserType() = 1%>' />
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
                <!-- Status -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblStatusInfo" CssClass="caption-subject uppercase">Status Info</asp:Label>
                                </div>
                                <div class="actions">
                                    <asp:Button runat="server" CssClass="btn default " ID="btnBackTop" Text="Kembali" OnClick="btnBack_Click" ClientIDMode="static" />
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblVerifyStatus" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Label runat="server" ID="txtVerifyStatus" CssClass="form-control-static"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblStatus" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnOPPStatus" Text="Update" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnOPPStatus_Click" ClientIDMode="static" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblGeofenceStatus" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlGeofenceStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnGeofenceStatus" Text="Update" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnGeofenceStatus_Click" ClientIDMode="static" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnResetStatus" Text="Update" OnClick="btnResetStatus_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnOPPStatus" />
                        <asp:AsyncPostBackTrigger ControlID="btnResetStatus" />
                        <asp:AsyncPostBackTrigger ControlID="btnGeofenceStatus" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- PHOTO -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblPhoto" CssClass="caption-subject uppercase">Subject Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblPhoto1" CssClass="col-md-4 control-label" Text="Subject Photo (Face)"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:Button runat="server" ID="btnPhoto1" Text="Select File" CssClass="btn blue " OnClientClick="fuPhoto1.click();return false;" />
                                                        <asp:Label runat="server" ID="lblPhoto1Validate" ForeColor="red" Visible="false"></asp:Label>
                                                        <asp:Image runat="server" ID="imgPhoto1Preview" ClientIDMode="Static" Style="height: 150px; display: block; margin-top: 10px;" />
                                                        <asp:HiddenField runat="server" ID="hfPhoto1Ori" ClientIDMode="Static" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblPhoto2" CssClass="col-md-4 control-label" Text="Subject Photo (Full Body)"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:Button runat="server" ID="btnPhoto2" Text="Select File" CssClass="btn blue " OnClientClick="fuPhoto2.click();return false;" />
                                                        <asp:Label runat="server" ID="lblPhoto2Validate" ForeColor="red" Visible="false"></asp:Label>
                                                        <asp:Image runat="server" ID="imgPhoto2Preview" ClientIDMode="Static" Style="height: 150px; display: block; margin-top: 10px;" />
                                                        <asp:HiddenField runat="server" ID="hfPhoto2Ori" ClientIDMode="Static" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnSubmitPhoto" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","photo")){return false};' OnClick="btnSubmitPhoto_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnResetPhoto" Text="Update" OnClick="btnResetPhoto_Click" ClientIDMode="static" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnResetPhoto" />
                        <asp:PostBackTrigger ControlID="btnSubmitPhoto" />
                        <asp:PostBackTrigger ControlID="btnBackTop" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- SUBJECT -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblSubject" CssClass="caption-subject uppercase">Subject Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSubjectName" CssClass="col-md-4 control-label" Text="Name Subjek"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSubjectName" CssClass="form-control input-inline input-large"></asp:TextBox>
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvSubjectName" ControlToValidate="txtSubjectName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSubjectICNo" CssClass="col-md-4 control-label" Text="No. K/P"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSubjectICNo" CssClass="form-control input-inline input-large" />
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvSubjectICNo" ControlToValidate="txtSubjectICNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSubjectContactNo" CssClass="col-md-4 control-label" Text="No. Tel."></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtSubjectContactNo" CssClass="form-control input-inline input-large" />
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvSubjectContactNo" ControlToValidate="txtSubjectContactNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblAddress" CssClass="col-md-4 control-label" Text="Alamat Kediaman"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" Rows="5" CssClass="form-control input-inline input-large" />
                                                        <label style="color: red">*</label>
                                                        <div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvAddress" ControlToValidate="txtAddress" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblState" CssClass="col-md-4 control-label" Text="Negeri"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlState" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddlState" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblDistrict" CssClass="col-md-4 control-label" Text="Daerah"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlDistrict" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvDistrict" ControlToValidate="ddlDistrict" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblMukim" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvMukim" ControlToValidate="ddlMukim" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblPoliceStation" CssClass="col-md-4 control-label" Text="Balai Polis"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvPoliceStation" ControlToValidate="ddlPoliceStation" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblDepartment" CssClass="col-md-4 control-label" Text="Jabatan"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvDepartment" ControlToValidate="ddlDepartment" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlState" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOffenceDesc" CssClass="col-md-4 control-label" Text="Kesalahan"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOffenceDesc" TextMode="MultiLine" Rows="5" CssClass="form-control input-inline input-large" />
                                                        <label style="color: red">*</label>
                                                        <div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOffenceDesc" ControlToValidate="txtOffenceDesc" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- ORDER -->
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblOrderInfo" CssClass="caption-subject uppercase">Order Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblActs" CssClass="col-md-4 control-label" Text="Ketegori Perintah"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlActs" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlActs_SelectedIndexChanged"></asp:DropDownList>
                                                                <label style="color: red">*</label>
                                                                <div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvActs" ControlToValidate="ddlActs" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="col-md-8 col-md-offset-4">
                                                                <asp:DropDownList runat="server" ID="ddlActsSection" ClientIDMode="Static" CssClass="form-control input-inline input-large margin-top-10"></asp:DropDownList>
                                                                <label style="color: red">*</label><div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvActsSection" ControlToValidate="ddlActsSection" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlActs" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOrderIssuedBy" CssClass="col-md-4 control-label" Text="Perintah Dikeluarkan"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlOrderIssuedBy" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOrderIssuedBy" ControlToValidate="ddlOrderIssuedBy" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOrderIssuedDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah Dikeluarkan"></asp:Label>
                                                    <div class="col-md-8">
                                                        <input id="txtOrderIssuedDate" name="txtOrderIssuedDate" class="DateFrom form-control input-inline input-large" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                    </div>
                                                    <asp:TextBox runat="server" ID="hfOrderIssuedDate" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <%--<div class="form-group">
                                                <div class="col-md-8 col-md-offset-3">
                                                    <asp:TextBox runat="server" ID="txtOrderIssuedByName" CssClass="form-control input-inline input-large margin-top-10" />
                                                    <label style="color: red">*</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOrderIssuedByName" ControlToValidate="txtOrderIssuedByName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>--%>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOrderRefNo" CssClass="col-md-4 control-label" Text="No. Rujukan Perintah"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOrderRefNo" CssClass="form-control input-inline input-large" />
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOrderRefNo" ControlToValidate="txtOrderRefNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOrderDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah"></asp:Label>
                                                    <div class="col-md-8">
                                                        <input id="txtOrderDate" name="txtOrderDate" class="DateFrom form-control input-inline input-large" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                    </div>
                                                    <asp:TextBox runat="server" ID="hfOrderDate" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOrderPeriod" CssClass="col-md-4 control-label" Text="Tempoh Perintah"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOrderPeriod" CssClass="form-control input-inline input-xsmall" />
                                                        <asp:DropDownList runat="server" ID="ddlOrderPeriodUnit" CssClass="form-control input-inline input-medium"></asp:DropDownList>
                                                        <label style="color: red">*</label><div>
                                                            <asp:RegularExpressionValidator runat="server" ID="revOrderPeriod" ControlToValidate="txtOrderPeriod" ErrorMessage="*invalid value" Display="Dynamic" ForeColor="Red" ValidationGroup="opp" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOrderPeriod" ControlToValidate="txtOrderPeriod" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- REPORT -->
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblReportInfo" CssClass="caption-subject uppercase">Reporting</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblRptPoliceStation" CssClass="col-md-4 control-label" Text="Balai Polis Lapor Diri"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList runat="server" ID="ddlRptPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlRptPoliceStation_SelectedIndexChanged"></asp:DropDownList>
                                                                <label style="color: red">*</label><div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvRptPoliceStation" ControlToValidate="ddlRptPoliceStation" InitialValue="0" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblRptPSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. Balai Polis"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtRptPSContactNo" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblSDNo" CssClass="col-md-4 control-label" Text="No. SD"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtSDNo" CssClass="form-control input-inline input-large" />
                                                                <label style="color: red">*</label><div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvSDNo" ControlToValidate="txtSDNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblOCSName" CssClass="col-md-4 control-label" Text="Ketua Polis Balai"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtOCSName" CssClass="form-control input-inline input-large" />
                                                                <label style="color: red">*</label><div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOCSName" ControlToValidate="txtOCSName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Label runat="server" ID="lblOCSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. KBP"></asp:Label>
                                                            <div class="col-md-8">
                                                                <asp:TextBox runat="server" ID="txtOCSContactNo" CssClass="form-control input-inline input-large" />
                                                                <label style="color: red">*</label><div>
                                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOCSContactNo" ControlToValidate="txtOCSContactNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlRptPoliceStation" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblReportDay" CssClass="col-md-4 control-label" Text="Hari Lapor Diri"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlReportDay" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblReportTime" CssClass="col-md-4 control-label" Text="Masa Lapor Diri"></asp:Label>
                                                    <div class="col-md-8">
                                                        <div class="input-group input-large">
                                                            <div class="input-group-addon">
                                                                <%=GetText("From")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlReportFrTime" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                        <div class="input-group input-large margin-top-10">
                                                            <div class="input-group-addon">
                                                                <%=GetText("To")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlReportToTime" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Oversight -->
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblOversightInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblRestrictTime" CssClass="col-md-4 control-label" Text="Masa Sekatan Kediaman"></asp:Label>
                                                    <div class="col-md-8">
                                                        <div class="input-group input-large">
                                                            <div class="input-group-addon">
                                                                <%=GetText("From")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlRestrictFrTime" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                        <div class="input-group input-large margin-top-10">
                                                            <div class="input-group-addon">
                                                                <%=GetText("To")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlRestrictToTime" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Geofence -->
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblGeofenceInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblGeofenceMukim" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlGeofenceMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                        <label style="color: red">*</label>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvGeofenceMukim" ControlToValidate="ddlGeofenceMukim" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Remarks -->
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblOthersInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblRemark" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="10" Style="resize: none;" CssClass="form-control input-inline input-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="pull-right">
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnSubmitOPP" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","opp")){return false};' OnClick="btnSubmitOPP_Click" ClientIDMode="static" />
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnResetOPP" Text="Update" OnClick="btnResetOPP_Click" ClientIDMode="static" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnResetOPP" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmitOPP" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- Upload Document -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblFileInfo" CssClass="caption-subject uppercase">Upload File</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblAttachment1" CssClass="col-md-4 control-label" Text="Lampiran 1"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Button runat="server" ID="btnAttachment1" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment1.click();return false;" />
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnSubmitAttachment" Text="Update" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnSubmitAttachment_Click" ClientIDMode="static" />
                                                <asp:Label runat="server" ID="lblAttachment1Validate" ForeColor="red" Visible="false"></asp:Label>
                                            </div>
                                            <div class="col-md-offset-4 col-md-8 margin-top-10">
                                                <table class="table table-bordered" style="width: auto">
                                                    <tbody id="dvAttachment1Preview"></tbody>
                                                </table>
                                            </div>
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
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnResetFile" Text="Update" OnClick="btnResetFile_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnResetFile" />
                        <asp:PostBackTrigger ControlID="btnSubmitAttachment" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- Overseer -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblOverseerInfo" CssClass="caption-subject uppercase">Overseer Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseer" CssClass="col-md-4 control-label" Text="Nama Pegawai"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlOverseer" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlOverseer_SelectedIndexChanged"></asp:DropDownList>
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOverseer" ControlToValidate="ddlOverseer" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="officer"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseerIDNo" CssClass="col-md-4 control-label" Text="No. Polis"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOverseerIDNo" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseerContactNo" CssClass="col-md-4 control-label" Text="No. Tel."></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOverseerContactNo" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseerIPK" CssClass="col-md-4 control-label" Text="Kontinjen"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOverseerIPK" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseerDept" CssClass="col-md-4 control-label" Text="Jabatan"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:TextBox runat="server" ID="txtOverseerDept" CssClass="form-control input-inline input-large" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnSubmitOverseer" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","officer")){return false};' OnClick="btnSubmitOverseer_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnResetOverseer" Text="Update" OnClick="btnResetOverseer_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlOverseer" />
                        <asp:AsyncPostBackTrigger ControlID="btnSubmitOverseer" />
                        <asp:AsyncPostBackTrigger ControlID="btnResetOverseer" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- EMD -->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="portlet light">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblEMDDeviceInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblEMDInstallDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah"></asp:Label>
                                                    <div class="col-md-8">
                                                        <input id="txtEMDInstallDate" name="txtEMDInstallDate" class="DateFrom form-control input-inline input-large" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                    </div>
                                                    <asp:TextBox runat="server" ID="hfEMDInstallDate" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblEMD" CssClass="col-md-4 control-label" Text="IMEI"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlEMD" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                        <%--<label style="color: red">*</label>--%>
                                                        <div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvEMD" ControlToValidate="ddlEMD" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="emd" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <div class="col-md-8 col-lg-offset-4">
                                                        <table>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkSmartTag" CssClass="checkbox" Style="margin-left: 20px" Text="SmartTag" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtSmartTagCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkOBC" CssClass="checkbox" Style="margin-left: 20px" Text="OBC" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtOBCCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkBeacon" CssClass="checkbox" Style="margin-left: 20px;" Text="Beacon" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtBeaconCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkCharger" CssClass="checkbox" Style="margin-left: 20px" Text="Charger" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtChargerCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkStrap" CssClass="checkbox" Style="margin-left: 20px" Text="Strap" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtStrapCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:CheckBox runat="server" ID="chkCable" CssClass="checkbox" Style="margin-left: 20px" Text="Cable" /></td>
                                                                <td style="padding: 1px 10px;">
                                                                    <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:TextBox runat="server" ID="txtCableCode" CssClass="form-control input-inline input-small"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="pull-right">
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnSubmitEMD" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","emd")){return false};' OnClick="btnSubmitEMD_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn blue " ID="btnResetEMD" Text="Update" OnClick="btnResetEMD_Click" ClientIDMode="static" />
                                            <asp:Button runat="server" CssClass="btn default " ID="btnBackBottom" Text="Kembali" OnClick="btnBack_Click" ClientIDMode="static" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSubmitEMD" />
                        <asp:AsyncPostBackTrigger ControlID="btnResetEMD" />
                        <asp:PostBackTrigger ControlID="btnBackBottom" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <!-- SEARCH -->
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:FileUpload runat="server" ID="fuPhoto1" Style="display: none" ClientIDMode="static" accept=".jpg, .jpeg, .png, .gif, .bmp" onchange="previewimg(event,'imgPhoto1Preview','hfPhoto1Ori');" />
    <asp:FileUpload runat="server" ID="fuPhoto2" Style="display: none" ClientIDMode="static" accept=".jpg, .jpeg, .png, .gif, .bmp" onchange="previewimg(event,'imgPhoto2Preview','hfPhoto2Ori');" />
    <asp:FileUpload runat="server" ID="fuAttachment1" multiple="multiple" Style="display: none" ClientIDMode="static" onchange="listSelectedFile(this,'dvAttachment1Preview');" />
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfConfirm2" Value="Confirm actions?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="opp" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="officer" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="geofence" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="emd" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <asp:Panel runat="server" ClientIDMode="Static" ID="plVerify" CssClass="modal fade" TabIndex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="portlet light bordered" style="width: 100%; margin: 0 auto">
                    <div class="portlet-title">
                        <button type="button" class="close" aria-hidden="true" onclick="closemodal('plVerify');"></button>
                        <div class="caption">
                            <i class="fa fa-check fa-fw"></i>
                            <asp:Label runat="server" ID="lblPInfo" CssClass="caption-subject bold uppercase" Text="Maklumat Pemohonan"></asp:Label>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="portlet light">
                                <!-- SUBJECT -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPSubject" CssClass="caption-subject uppercase">Subject Info</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPPhoto1" CssClass="col-md-4 control-label" Text="Gambar Subjek (Muka)"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Image runat="server" ID="imgPPhoto1Preview" ClientIDMode="Static" Style="height: 150px; display: block;" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPPhoto2" CssClass="col-md-4 control-label" Text="Gambar Subjek (Badan)"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Image runat="server" ID="imgPPhoto2Preview" ClientIDMode="Static" Style="height: 150px; display: block;" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPSubjectName" CssClass="col-md-4 control-label" Text="Name Subjek"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPSubjectName" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPSubjectICNo" CssClass="col-md-4 control-label" Text="No. K/P"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPSubjectICNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPSubjectContactNo" CssClass="col-md-4 control-label" Text="No. Tel."></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPSubjectContactNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPAddress" CssClass="col-md-4 control-label" Text="Alamat Kediaman"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPAddress" TextMode="MultiLine" Rows="5" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPState" CssClass="col-md-4 control-label" Text="Negeri"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPState" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPDistrict" CssClass="col-md-4 control-label" Text="Daerah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDistrict" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPMukim" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPPoliceStation" CssClass="col-md-4 control-label" Text="Balai Polis"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPDepartment" CssClass="col-md-4 control-label" Text="Jabatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPDepartment" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOffenceDesc" CssClass="col-md-4 control-label" Text="Kesalahan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOffenceDesc" TextMode="MultiLine" Rows="5" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- ORDER -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPOrderInfo" CssClass="caption-subject uppercase">Maklumat Perintah</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPActs" CssClass="col-md-4 control-label" Text="Ketegori Perintah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPActs" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-8 col-md-offset-4">
                                                    <asp:Label runat="server" ID="txtPActsSection" ClientIDMode="Static" CssClass="form-control input-inline input-large margin-top-10"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOrderIssuedBy" CssClass="col-md-4 control-label" Text="Perintah Dikeluarkan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOrderIssuedBy" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOrderIssuedDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah Dikeluarkan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOrderIssuedDate" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                    <div class="col-md-8 col-md-offset-3">
                                        <asp:Label runat="server" ID="txtPOrderIssuedByName" CssClass="form-control input-inline input-large margin-top-10" />
                                        </div>
                                </div>--%>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOrderRefNo" CssClass="col-md-4 control-label" Text="No. Rujukan Perintah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOrderRefNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOrderDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOrderDate" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOrderPeriod" CssClass="col-md-4 control-label" Text="Tempoh Perintah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOrderPeriod" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- REPORT -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPReportInfo" CssClass="caption-subject uppercase">Butir-Butir Balai Polis</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRptPoliceStation" CssClass="col-md-4 control-label" Text="Balai Polis Lapor Diri"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPRptPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRptPSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. Balai Polis"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPRptPSContactNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPSDNo" CssClass="col-md-4 control-label" Text="No. SD"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPSDNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOCSName" CssClass="col-md-4 control-label" Text="Ketua Polis Balai"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSName" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOCSContactNo" CssClass="col-md-4 control-label" Text="No. Tel. KBP"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOCSContactNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPReportDay" CssClass="col-md-4 control-label" Text="Hari Lapor Diri"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPReportDay" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPReportTime" CssClass="col-md-4 control-label" Text="Masa Lapor Diri"></asp:Label>
                                                <div class="col-md-8">
                                                    <div class="input-group input-large">
                                                        <div class="input-group-addon">
                                                            <%=GetText("From")%>
                                                        </div>
                                                        <asp:Label runat="server" ID="txtPReportFrTime" CssClass="form-control"></asp:Label>
                                                    </div>
                                                    <div class="input-group input-large margin-top-10">
                                                        <div class="input-group-addon">
                                                            <%=GetText("To")%>
                                                        </div>
                                                        <asp:Label runat="server" ID="txtPReportToTime" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- OVERSEER -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPOverseerInfo" CssClass="caption-subject uppercase">Pegawai Pegawasan</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOverseer" CssClass="col-md-4 control-label" Text="Nama Pegawai"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOverseer" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOverseerIDNo" CssClass="col-md-4 control-label" Text="No. Polis"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOverseerIDNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOverseerIPK" CssClass="col-md-4 control-label" Text="Kontinjen"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOverseerIPK" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOverseerDept" CssClass="col-md-4 control-label" Text="Jabatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOverseerDept" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPOverseerContactNo" CssClass="col-md-4 control-label" Text="No. Tel."></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPOverseerContactNo" CssClass="form-control input-inline input-large" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Oversight -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPOversightInfo" CssClass="caption-subject uppercase">Oversight Info</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRestrictTime" CssClass="col-md-4 control-label" Text="Masa Sekatan Kediaman"></asp:Label>
                                                <div class="col-md-8">
                                                    <div class="input-group input-large">
                                                        <div class="input-group-addon">
                                                            <%=GetText("From")%>
                                                        </div>
                                                        <asp:Label runat="server" ID="txtPRestrictFrTime" CssClass="form-control"></asp:Label>
                                                    </div>
                                                    <div class="input-group input-large margin-top-10">
                                                        <div class="input-group-addon">
                                                            <%=GetText("To")%>
                                                        </div>
                                                        <asp:Label runat="server" ID="txtPRestrictToTime" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Geofence -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPGeofenceInfo" CssClass="caption-subject uppercase">Geo Pagar</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPGeofenceMukim" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPGeofenceMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- EMD -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPEMDDeviceInfo" CssClass="caption-subject uppercase">EMD</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPEMDInstallDate" CssClass="col-md-4 control-label" Text="Tarikh Perintah"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPEMDInstallDate" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPEMD" CssClass="col-md-4 control-label" Text="IMEI"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPEMD" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-8 col-lg-offset-4">
                                                    <table>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPSmartTag" CssClass="checkbox" Style="margin-left: 20px" Text="SmartTag" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPSmartTagCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPOBC" CssClass="checkbox" Style="margin-left: 20px" Text="OBC" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPOBCCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPBeacon" CssClass="checkbox" Style="margin-left: 20px;" Text="Beacon" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPBeaconCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPCharger" CssClass="checkbox" Style="margin-left: 20px" Text="Charger" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPChargerCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPStrap" CssClass="checkbox" Style="margin-left: 20px" Text="Strap" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPStrapCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:CheckBox runat="server" ID="chkPCable" CssClass="checkbox" Style="margin-left: 20px" Text="Cable" Enabled="false" /></td>
                                                            <td style="padding: 1px 10px;">
                                                                <asp:Label runat="server" Style="margin-right: 5px;" Text="S/N"></asp:Label><asp:Label runat="server" ID="txtPCableCode" CssClass="form-control input-inline input-small" Enabled="false"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- File Upload -->
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblPFileInfo" CssClass="caption-subject uppercase">Butir-Butir Lain</asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPAttachment1" CssClass="col-md-4 control-label" Text="Perintah Pengawasan"></asp:Label>
                                                <div class="col-md-8">
                                                    <table class="table table-bordered table-striped" style="width: auto;">
                                                        <asp:Repeater runat="server" ID="rptPAttachment1">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Eval("FileName") %></td>
                                                                    <td><a href="#" class="btn blue btn-xs" onclick="OpenPopupWindow('<%#Eval("FilePath")%>', 1280, 800); return false;"><%#GetText("Open")%></a></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                                <asp:Label runat="server" ID="lblPAttachment2" CssClass="col-md-4 control-label" Text="Lampiran"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:LinkButton runat="server" ID="lbtPAttachment2" CssClass="form-control-static" Text="Lampiran"></asp:LinkButton>
                                                </div>
                                            </div>--%>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPRemark" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtPRemark" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPStatus" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPStatus" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblPVerifyStatus" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:Label runat="server" ID="txtPVerifyStatus" CssClass="form-control input-inline input-large"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" CssClass="btn blue " ID="btnPApprove" Text="Luluskan" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPApprove_Click" ClientIDMode="static" />
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

    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            initBasicDatePicker("#txtOrderDate", "#hfOrderDate");
            initBasicDatePicker("#txtOrderIssuedDate", "#hfOrderIssuedDate");
            initBasicDatePicker("#txtEMDInstallDate", "#hfEMDInstallDate");
            initTable("table1");
            $('#ddlState').select2();
            $('#ddlDistrict').select2();
            $('#ddlMukim').select2();
            $('#ddlPoliceStation').select2();
            $('#ddlSPoliceStation').select2();
            $('#ddlActs').select2();
            $('#ddlActsSection').select2();
            $('#ddlRptPoliceStation').select2();
            $('#ddlOverseer').select2();
            $('#ddlGeofenceMukim').select2();
            $('#ddlEMD').select2();
        }

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

        function previewimg(event, desimg, oriimg) {
            var output = document.getElementById(desimg);
            if (event && event.target && event.target.files && event.target.files[0]) {
                output.src = URL.createObjectURL(event.target.files[0]);
            } else {
                output.src = document.getElementById(oriimg).value;
            }
            output.onload = function () {
                URL.revokeObjectURL(output.src) // free memory
            }
        };

    </script>
    <!--Javascript-->
</asp:Content>

