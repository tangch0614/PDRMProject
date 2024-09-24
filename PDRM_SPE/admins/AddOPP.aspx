<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AddOPP.aspx.vb" Inherits="PDRM_SPE.AAddOPP" %>

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
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">AddOPP</asp:Label>
                    </div>
                </div>
                <div class="portlet light">
                    <!-- CASE -->
                    <%--<div class="portlet-title">
                        <div class="caption">
                            <asp:Label runat="server" ID="lblCaseFile" CssClass="caption-subject uppercase">CaseFileInfo</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCaseFileNo" CssClass="col-md-4 control-label" Text="Current Login ID"></asp:Label>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtCaseFileNo" data-temp="1" CssClass="form-control input-inline input-large"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblOfficerName" CssClass="col-md-4 control-label" Text="Name"></asp:Label>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtOfficerName" CssClass="form-control input-inline input-large" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblOfficerContactNo" CssClass="col-md-4 control-label" Text="Name"></asp:Label>
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtOfficerContactNo" CssClass="form-control input-inline input-large" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <!-- SUBJECT -->
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
                                            <asp:Label runat="server" ID="lblSubjectContactNo" CssClass="col-md-4 control-label" Text="No. K/P"></asp:Label>
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
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPhoto1" CssClass="col-md-4 control-label" Text="Gambar Subjek (Muka)"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Button runat="server" ID="btnPhoto1" Text="Select File" CssClass="btn blue " OnClientClick="fuPhoto1.click();return false;" />
                                                <label style="color: red">*</label>
                                                <asp:Label runat="server" ID="lblPhoto1Validate" ForeColor="red" Visible="false"></asp:Label>
                                                <asp:Image runat="server" ID="imgPhoto1Preview" src="../assets/img/No_Image.png" ClientIDMode="Static" Style="height: 150px; display: block; margin-top: 10px;" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPhoto2" CssClass="col-md-4 control-label" Text="Gambar Subjek (Badan)"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Button runat="server" ID="btnPhoto2" Text="Select File" CssClass="btn blue " OnClientClick="fuPhoto2.click();return false;" />
                                                <label style="color: red">*</label>
                                                <asp:Label runat="server" ID="lblPhoto2Validate" ForeColor="red" Visible="false"></asp:Label>
                                                <asp:Image runat="server" ID="imgPhoto2Preview" src="../assets/img/No_Image.png" ClientIDMode="Static" Style="height: 150px; display: block; margin-top: 10px;" />
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
                            <asp:Label runat="server" ID="lblOrderInfo" CssClass="caption-subject uppercase">Maklumat Perintah</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class=" col-md-6">
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
                            <asp:Label runat="server" ID="lblReportInfo" CssClass="caption-subject uppercase">Butir-Butir Balai Polis</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class=" col-md-6">
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
                                    <div class=" col-md-6">
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
                    <!-- OVERSEER -->
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Label runat="server" ID="lblOverseerInfo" CssClass="caption-subject uppercase">Pegawai Pegawasan</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class=" col-md-6">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblOverseer" CssClass="col-md-4 control-label" Text="Nama Pegawai"></asp:Label>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlOverseer" ClientIDMode="Static" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlOverseer_SelectedIndexChanged"></asp:DropDownList>
                                                        <label style="color: red">*</label><div>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvOverseer" ControlToValidate="ddlOverseer" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
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
                                            <div class=" col-md-6">
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
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlOverseer" />
                                    </Triggers>
                                </asp:UpdatePanel>
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
                                    <div class=" col-md-6">
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
                            <asp:Label runat="server" ID="lblGeofenceInfo" CssClass="caption-subject uppercase">Geo Pagar</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblGeofenceMukim" CssClass="col-md-4 control-label" Text="Mukim"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlGeofenceMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <label style="color: red">*</label><div>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvGeofenceMukim" ControlToValidate="ddlGeofenceMukim" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- EMD -->
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Label runat="server" ID="lblEMDDeviceInfo" CssClass="caption-subject uppercase">EMD</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class=" col-md-6">
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
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvEMD" ControlToValidate="ddlEMD" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="opp" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
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
                        </div>
                    </div>
                    <!-- File Upload -->
                    <div class="portlet-title">
                        <div class="caption">
                            <asp:Label runat="server" ID="lblFileInfo" CssClass="caption-subject uppercase">Butir-Butir Lain</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblAttachment1" CssClass="col-md-4 control-label" Text="Lampiran 1"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:Button runat="server" ID="btnAttachment1" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment1.click();return false;" />
                                                <asp:Label runat="server" ID="lblAttachment1Validate" ForeColor="red" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-offset-4 col-md-8">
                                                <table class="table table-bordered" style="width: auto">
                                                    <tbody id="dvAttachment1Preview"></tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=" col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblRemark" CssClass="col-md-4 control-label" Text="Catatan"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="5" Style="resize: none;" CssClass="form-control input-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                    <asp:Label runat="server" ID="lblStatus" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                    <div class="col-md-8">
                                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                    </div>
                                </div>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="pull-right">
                                    <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","opp")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- PAGE BODY -->
    <asp:FileUpload runat="server" ID="fuPhoto1" Style="display: none" ClientIDMode="static" accept=".jpg, .jpeg, .png, .gif, .bmp" onchange="previewimg(event,'imgPhoto1Preview');" />
    <asp:FileUpload runat="server" ID="fuPhoto2" Style="display: none" ClientIDMode="static" accept=".jpg, .jpeg, .png, .gif, .bmp" onchange="previewimg(event,'imgPhoto2Preview');" />
    <asp:FileUpload runat="server" ID="fuAttachment1" multiple="multiple" Style="display: none" ClientIDMode="static" onchange="listSelectedFile(this,'dvAttachment1Preview');" />
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="opp" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            initBasicDatePicker("#txtOrderDate", "#hfOrderDate");
            initBasicDatePicker("#txtOrderIssuedDate", "#hfOrderIssuedDate");
            initBasicDatePicker("#txtEMDInstallDate", "#txtEMDInstallDate");
            $('#ddlState').select2();
            $('#ddlDistrict').select2();
            $('#ddlMukim').select2();
            $('#ddlPoliceStation').select2();
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

        function previewimg(event, desimg) {
            var output = document.getElementById(desimg);
            if (event && event.target && event.target.files && event.target.files[0]) {
                output.src = URL.createObjectURL(event.target.files[0]);
            } else {
                output.src = "../assets/img/No_Image.png";
            }
            output.onload = function () {
                URL.revokeObjectURL(output.src) // free memory
            }
        };

    </script>
    <!--Javascript-->
</asp:Content>

