<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AccessControl.aspx.vb" Inherits="PDRM_SPE.AAccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <h3 class="page-title" style="display: none;">
        <asp:Label runat="server" ID="lblPageTitle" Text="Sponsor Tree"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <div class="portlet light">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Privilege Setting</asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="form-actions top">
                                    <div class="form-horizontal" style="padding: 20px">
                                        <div class="form-body">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblRole" CssClass="col-md-3 control-label" Text="Role"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control input-inline input-large" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-body">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="privilegeTree form-group">
                                                <div class="col-md-12">
                                                    <div class="portlet light bordered boxShadow">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="fa fa-list fa-fw"></i>
                                                                <asp:Label runat="server" ID="lblInfoHeader" CssClass="caption-subject bold uppercase">Member Wallet Information</asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body form">
                                                            <div class="form-horizontal form-bordered">
                                                                <div class="form-body">
                                                                    <div class="form-group">
                                                                        <ul>
                                                                            <asp:Repeater ID="rpMain" runat="server">
                                                                                <ItemTemplate>
                                                                                    <li runat="server" id="liMain">
                                                                                        <a runat="server" id="lbtExpand" class="expand fa fa-plus-square fw"></a>
                                                                                        <asp:CheckBox runat="server" ID="chkMain" />
                                                                                        <asp:LinkButton runat="server" ID="lbtMain" href="#">
                                                                                            <asp:HiddenField runat="server" ID="hfMainID" Value='<%#Eval("fldMenuID") %>' />
                                                                                            <asp:Label ID="lblMainTitle" runat="server" Font-Bold="true" Text='<%#Eval("fldMenuTitleText")%>'></asp:Label>
                                                                                        </asp:LinkButton>
                                                                                        <asp:Repeater ID="rpSub" runat="server">
                                                                                            <HeaderTemplate>
                                                                                                <ul>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <li runat="server" id="liSub">
                                                                                                    <asp:LinkButton runat="server" ID="lbtSub" href="#">
                                                                                                        <asp:CheckBox runat="server" ID="chkSub" />
                                                                                                        <asp:HiddenField runat="server" ID="hfSubID" Value='<%#Eval("fldMenuID") %>' />
                                                                                                        <label>--</label>
                                                                                                        <asp:Label ID="lblSubTitle" runat="server" Text='<%#Eval("fldMenuTitleText")%>'></asp:Label>
                                                                                                    </asp:LinkButton>
                                                                                                </li>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                </ul>
                                                                                            </FooterTemplate>
                                                                                        </asp:Repeater>
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <div class="form-actions">
                                                                    <div class="row">
                                                                        <div class="pull-right">
                                                                            <asp:Button runat="server" CssClass="btn custom-color" ID="btnUpdate" Text="Update" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","")){return false};' OnClick="btnUpdate_Click" />
                                                                            <asp:Button runat="server" CssClass="btn" ID="btnReset" Text="Reset" OnClick="btnReset_Click" />
                                                                            <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm edit privilege?" ClientIDMode="Static" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlRole" />
                                <asp:AsyncPostBackTrigger ControlID="btnReset" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <!-- END FORM -->
                </div>
            </div>
        </div>
    </div>

    <!--LOADING POPUP-->
    <asp:UpdateProgress runat="server" ID="upgSponsorTree" AssociatedUpdatePanelID="UpdatePanel3">
        <ProgressTemplate>
            <div class="loadingPage">
                <img src="../assets/img/loadingCircle.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--LOADING POPUP-->

    <script type="text/javascript">
        function pageLoad(sender, args) {
            jQuery(document).ready(function () {

                //$('.privilegeTree li').each(function () {
                //    if ($(this).children('ul').length > 0) {
                //        $(this).addClass('parent');
                //    }
                //});

                $('.privilegeTree li.parent > a.expand').click(function () {
                    $(this).parent().toggleClass('active');
                    $(this).parent().children('ul').slideToggle('fast');
                });


                $('.privilegeTree li').each(function () {
                    $(this).toggleClass('active');
                    $(this).children('ul').show();
                });
            });
        }

    </script>
</asp:Content>
