<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="AlertNotificationReport.aspx.vb" Inherits="PDRM_SPE.AAlertNotificationReport" %>

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
                                                <asp:Label runat="server" ID="lblSTime" CssClass="col-md-4 control-label" Text="Masa Lapor Diri"></asp:Label>
                                                <div class="col-md-8">
                                                    <div class="input-group input-large">
                                                        <div class="input-group-addon">
                                                            <%=GetText("From")%>
                                                        </div>
                                                        <asp:DropDownList runat="server" ID="ddlFrTime" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="input-group input-large margin-top-10">
                                                        <div class="input-group-addon">
                                                            <%=GetText("To")%>
                                                        </div>
                                                        <asp:DropDownList runat="server" ID="ddlToTime" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblSEMD" CssClass="col-md-4 control-label" Text="User ID"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlSEMD" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblSOPP" CssClass="col-md-4 control-label" Text="User ID"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlSOPP" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblSState" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlSState" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblSProcessStatus" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" ID="ddlSProcessStatus" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                        <asp:Label runat="server" ID="lblSViolateTerms" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlSViolateTerms" ClientIDMode="Static" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblSSeverity" CssClass="col-md-4 control-label" Text="Status"></asp:Label>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlSSeverity" CssClass="form-control input-inline input-large"></asp:DropDownList>
                                        </div>
                                    </div>--%>
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
                                    <asp:Button runat="server" Style="float: right" ID="btnExport" Text="Export To Excel" CssClass="btn green btn-sm" OnClick="btnExport_Click" />
                                    <asp:Repeater runat="server" ID="rptTable">
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
                                                            <%#GetText("OPPItem").replace("vITEM", Gettext("Name")) & " / " & Gettext("ICNum")%>
                                                        </th>
                                                        <th style="width: 10% !important;">
                                                            <%#GetText("EMD")%>
                                                        </th>
                                                        <th style="width: 10% !important;">
                                                            <%#GetText("Acts")%>
                                                        </th>
                                                        <th style="width: 10% !important;">
                                                            <%#GetText("State") & " / " & GetText("Township")%>
                                                        </th>
                                                        <th style="width: 10% !important;">
                                                            <%#GetText("ViolateTerms")%>
                                                        </th>
                                                        <th style="width: 10% !important">
                                                            <%#GetText("AlertDateTime")%>
                                                        </th>
                                                        <th style="width: 10% !important">
                                                            <%#GetText("Status")%>
                                                        </th>
                                                        <th style="width: 10% !important">
                                                            <%#GetText("CompleteDateTime")%>
                                                        </th>
                                                        <th style="width: 10% !important">
                                                            <%#GetText("ActionTaken")%>
                                                        </th>
                                                        <th style="width: 10% !important">
                                                            <%#GetText("ClosingBy")%>
                                                        </th>
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
                                                    <%#Eval("fldOPPName") & " - " & Eval("fldOPPICNo")%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%#If(String.IsNullOrWhiteSpace(Eval("fldEMDName")), Eval("fldEMDImei"), Eval("fldEMDName"))%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%#Eval("fldActs") & " - " & Eval("fldActsSection")%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%#Eval("fldState") & " - " & Eval("fldMukim")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Gettext(Eval("fldMsg")).ToUpper%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <%#Eval("fldDateTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label runat="server" Text='<%#If(CInt(Eval("fldProcessStatus")) = 1, Gettext("Acknowledge") & "/" & GetText("Hold"), If(CInt(Eval("fldProcessStatus")) = 2, Gettext("Completed"), Gettext("Pending")))%>' CssClass='<%#If(CInt(Eval("fldProcessStatus")) = 1, "label label-info", If(CInt(Eval("fldProcessStatus")) = 2, "label label-success", "label label-warning"))%>' />
                                                </td>
                                                <td style="text-align: center;">
                                                    <%#If(Eval("fldProcessStatus") = 2, Eval("fldLastProcessDateTime", "{0:yyyy-MM-dd HH:mm:ss}"), "-")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox runat="server" Text='<%#If(Eval("fldProcessStatus") = 2, Eval("fldProcessRemark"), "")%>' TextMode="MultiLine" Rows="5" Style="width: 100%; resize: none;" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Eval("fldLastProcessByName")%>
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
                    <!-- SEARCH -->
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- PAGE BODY -->

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
            initDatePickerNoMax("#txtDateFrom", "#hfDateFrom", "#txtDateTo", "#hfDateTo");
            initTable("table1");
            $("#ddlSEMD").select2();
            $("#ddlSOPP").select2();
            $("#ddlSState").select2();
        }
    </script>
    <!--Javascript-->
</asp:Content>

