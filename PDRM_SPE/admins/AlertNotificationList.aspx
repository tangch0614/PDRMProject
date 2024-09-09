<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AlertNotificationList.aspx.vb" Inherits="PDRM_SPE.AAlertNotificationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .notification{
            min-height:100vh;
            max-height:100vh;
            overflow-y:scroll;
        }
        
        .notification table td {
            padding: 5px 5px !important;
        }
    </style>
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
                    <div class="col-md-4">
                        <div class="portlet box red">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblHighList" CssClass="caption-subject bold uppercase"><%=GetText("High")%></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification">
                                        <!-- TABLE -->
                                        <asp:Repeater runat="server" ID="rptHigh" OnItemCommand="rptHigh_ItemCommand">
                                            <ItemTemplate>
                                                <table class="table table-striped table-hover table-bordered managedTable" id="table1">
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-user"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldOPPName") & " - " & Eval("fldOPPICNO")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-warning"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldMsg")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-clock-o"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button runat="server" ID="btnAcknowledge" CssClass="btn blue btn-xs" CommandArgument='<%#Eval("fldID")%>' CommandName="acknowledge" Text='<%#GetText("Acknowledge")%>'/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="portlet box yellow-gold">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblMediumList" CssClass="caption-subject bold uppercase"><%=GetText("Medium")%></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification">
                                        <!-- TABLE -->
                                        <asp:Repeater runat="server" ID="rptMedium" OnItemCommand="rptMedium_ItemCommand">
                                            <ItemTemplate>
                                                <table class="table table-striped table-hover table-bordered managedTable" id="table1">
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-user"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldOPPName") & " - " & Eval("fldOPPICNO")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-warning"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldMsg")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-clock-o"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button runat="server" ID="btnAcknowledge" CssClass="btn blue btn-xs" CommandArgument='<%#Eval("fldID")%>' CommandName="acknowledge" Text='<%#GetText("Acknowledge")%>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="portlet box yellow-crusta">
                            <div class="portlet-title">
                                <div class="caption">
                                    <asp:Label runat="server" ID="lblLowList" CssClass="caption-subject bold uppercase"><%=GetText("Low")%></asp:Label>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body notification">
                                        <!-- TABLE -->
                                        <asp:Repeater runat="server" ID="rptLow" OnItemCommand="rptLow_ItemCommand">
                                            <ItemTemplate>
                                                <table class="table table-striped table-hover table-bordered managedTable" id="table1">
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-user"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldOPPName") & " - " & Eval("fldOPPICNO")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-warning"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldMsg")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <i class="fa fa-clock-o"></i>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%#Eval("fldDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Button runat="server" ID="btnAcknowledge" CssClass="btn blue btn-xs" CommandArgument='<%#Eval("fldID")%>' CommandName="acknowledge" Text='<%#GetText("Acknowledge")%>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!-- TABLE -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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


</asp:Content>

