<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="admins/Admin.Master" CodeBehind="AccessDenied.aspx.vb" Inherits="PDRM_SPE.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <h3 class="page-title">
        <asp:Label runat="server" ID="lblPageTitle" Text="Account Activation Receipt"></asp:Label></h3>
    <!-- PAGE HEADER -->
    <div class="clearfix">
    </div>
    <!-- PAGE BODY -->
    <div class="row">
        <div class="col-md-12">
            <div class="portlet light bordered boxShadow">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-ban fa-fw"></i>
                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Access Denied</asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="text-align:center">
                            <h1><b><asp:Label runat="server" ID="lblAccessDenied" Text="You are not authorized"></asp:Label></b></h1>
                            <img src="assets/img/accessdenied.jpg" />
                        </div>
                    </div>
                    <!-- END FORM-->
                </div>
            </div>
            <!-- NETWORK INFORMATION -->
        </div>

    </div>
    <!-- PAGE BODY -->
</asp:Content>
