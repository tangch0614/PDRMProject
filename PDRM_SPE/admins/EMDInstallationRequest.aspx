<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="EMDInstallationRequest.aspx.vb" Inherits="PDRM_SPE.AEMDInstallationRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .input-inline + .select2-container--bootstrap{
            display:inline-block;
        }
    </style>
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
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase" Text="Pemohonan Pemasangan"></asp:Label>
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
                                        <asp:RequiredFieldValidator runat="server" ID="rfvDepartment" ControlToValidate="ddlDepartment" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator runat="server" ID="rfvTime" ControlToValidate="ddlTime" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
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
                                                <asp:RequiredFieldValidator runat="server" ID="rfvIPK" ControlToValidate="ddlIPK" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblPoliceStation" CssClass="col-md-3 control-label" Text="Balai Police"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlPoliceStation" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPoliceStation" ControlToValidate="ddlPoliceStation" InitialValue="-1" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group" runat="server" visible="false">
                                            <asp:Label runat="server" ID="lblState" CssClass="col-md-3 control-label" Text="State"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlState" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <%--<label style="color: red">*</label>--%>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddlState" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <%--<div class="form-group">
                                            <asp:Label runat="server" ID="lblDistrict" CssClass="col-md-3 control-label" Text="District"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDistrict" ControlToValidate="ddlDistrict" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblMukim" CssClass="col-md-3 control-label" Text="Sub-District"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:DropDownList runat="server" ID="ddlMukim" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvMukim" ControlToValidate="ddlMukim" InitialValue="" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlIPK"/>
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
                                                <asp:RequiredFieldValidator runat="server" ID="rfvOCSName" ControlToValidate="txtOCSName" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblOCSContactNo" CssClass="col-md-3 control-label" Text="No. Tel. KBP"></asp:Label>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="txtOCSContactNo" CssClass="form-control input-inline input-large" />
                                                <label style="color: red">*</label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvOCSContactNo" ControlToValidate="txtOCSContactNo" ErrorMessage="*cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="apply"></asp:RequiredFieldValidator>
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
                                        <asp:TextBox runat="server" ID="txtAttachment1" CssClass="form-control input-inline input-large" onkeydown="return false;" onpaste="return false;" autocomplete="off" ClientIDMode="static"></asp:TextBox>
                                        <%--<label style="color: red">*</label>--%>
                                        <asp:Button runat="server" ID="btnAttachment1" Text="Select File" CssClass="btn blue " OnClientClick="fuAttachment1.click();return false;" />
                                        <asp:Label runat="server" ID="lblAttachment1Validate" ForeColor="red" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblAttachment2" CssClass="col-md-3 control-label" Text="Lampiran"></asp:Label>
                                    <div class="col-md-8">
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
                                        <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Serahkan" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","apply")){return false};' OnClick="btnSubmit_Click" ClientIDMode="static" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    </div>
    <!-- PAGE BODY -->
    <asp:FileUpload runat="server" ID="fuAttachment1" Style="display: none" ClientIDMode="static" onchange="txtAttachment1.value = filename(this);" />
    <asp:FileUpload runat="server" ID="fuAttachment2" Style="display: none" ClientIDMode="static" onchange="txtAttachment2.value = filename(this);" />
    <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm update details?" ClientIDMode="Static" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="apply" DisplayMode="BulletList" HeaderText="Please make sure all field are entered correctly" ShowMessageBox="true" ShowValidationErrors="true" ShowSummary="false" />

    <!--Javascript-->
    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            initBasicDatePicker("#txtDate", "#hfDate");
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

