<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SelectImage.aspx.vb" Inherits="PDRM_SPE.ASelectImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <!-- NON TEMPLATE-->
    <link href="../assets/css/general.css?v=1" rel="stylesheet" />
    <link href="../assets/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet" />
    <!-- NON TEMPLATE-->
    <link rel="shortcut icon" href="../assets/img/favicon.ico" />
    <link href="../assets/img/companylogo.png" rel="icon" sizes="32x32" />
    <link href="../assets/img/companylogo.png" rel="icon" sizes="192x192" />
    <link href="../assets/img/companylogo.png" rel="apple-touch-icon-precomposed" />
    <meta content="../assets/img/companylogo.png" name="msapplication-TileImage" />
</head>
<body style="background: #fff !important">
    <form id="form1" style="padding: 20px" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hfControlID" ClientIDMode="Static" />
        <!-- PAGE HEADER -->
        <h3 class="page-title">
            <asp:Label runat="server" ID="lblPageTitle" Text="Browse Image"></asp:Label></h3>
        <!-- PAGE HEADER -->
        <div class="clearfix">
        </div>
        <%--<div style="padding: 20px 0; margin: 0 20px; font-size: x-large; color: #666; border-bottom: 1px solid #eee">
            <i class="fa fa-search-plus fa-fw" style="font-size: x-large"></i>
            <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Browse Image</asp:Label>
        </div>--%>
        <div class="row">
            <div class="col-md-12">
                <!-- INFORMATION -->
                <div class="portlet light bordered boxShadow">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-search-plus fa-fw"></i>
                            <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase">Browse Image</asp:Label>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <!-- BEGIN FORM-->
                        <div class="form-horizontal">
                            <div class="form-body">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblImg" CssClass="col-md-3 control-label" Text="New Image"></asp:Label>
                                    <asp:FileUpload runat="server" ID="fuImg" Style="display: none" ClientIDMode="static" onchange="txtImg.value = filename(this)" />
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtImg" CssClass="form-control input-inline input-large" onkeydown="return false;" onpaste="return false;" autocomplete="off" ClientIDMode="static"></asp:TextBox>
                                        <label style="color: red">*</label>
                                        <asp:Button runat="server" ID="btnImg" Text="Select File" CssClass="btn purple " OnClientClick="fuImg.click();return false;" />
                                        <asp:Label runat="server" ID="lblImgValidate" ForeColor="red" Visible="false"></asp:Label>
                                        <asp:Button runat="server" CssClass="btn blue " ID="btnSubmit" Text="Upload" OnClientClick="return confirm(document.getElementById('hfConfirm').value);" OnClick="btnSubmit_Click" ClientIDMode="static" />
                                        <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm transaction?" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upSearch">
                                <ContentTemplate>
                                    <div class="form-actions">
                                        <!-- TABLE -->
                                        <asp:HiddenField runat="server" ID="hfConfirmDelete" ClientIDMode="Static" Value="delete?" />
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
                                                            <th style="width: 5px !important">
                                                                <asp:Label runat="server" ID="lblHNo" Text='<%#GetText("Num")%>'></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblHPreview" Text='<%#GetText("Preview")%>'></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblHName" Text='<%#GetText("FileName")%>'></asp:Label>
                                                            </th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblHOperation" Text='<%#GetText("Operation")%>'></asp:Label>
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
                                                        <asp:Image runat="server" ID="imgPreview" ImageUrl='<%#Eval("filePath")%>' Style="max-height: 100px" />
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:Label runat="server" Style="display: block;" ID="txtName" Text='<%#Eval("fileName")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button runat="server" ID="btnDelete" Text='<%#GetText("Delete")%>' OnClientClick="return confirm(document.getElementById('hfConfirmDelete').value);" CommandName="deleteImg" CommandArgument='<%#Eval("filePath")%>' class="btn red btn-sm" />
                                                        <asp:Button runat="server" ID="btnSelect" Text='<%#GetText("SelectItem").Replace("vITEM", "")%>' data-image='<%#"<img alt=""" & Eval("fileName") & """ src=""" & Eval("filePath") & """ style=""width:100%"" />"%>' class="btn blue btn-sm" OnClientClick="f2(this);" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--LOADING POPUP-->
        <%--<asp:UpdateProgress runat="server" ID="upgSearch" AssociatedUpdatePanelID="upSearch">
            <ProgressTemplate>
                <div class="loadingPage">
                    <img src="../assets/img/loadingCircle.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        <!--LOADING POPUP-->

    </form>
    <!--[if lt IE 9]>
<script src="../assets/global/plugins/respond.min.js"></script>
<script src="../assets/global/plugins/excanvas.min.js"></script> 
<script src="../assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN GLOBAL PLUGINS -->
        <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
        <!-- END GLOBAL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/table-datatables-managed.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="../assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
        <script src="../assets/layouts/layout/scripts/demo.min.js" type="text/javascript"></script>
        <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
        <!-- NON TEMPLATE-->
        <script src="../assets/tinymce3.x/jscripts/tiny_mce/tiny_mce.js"></script>
        <script src="../assets/jquery-ui-1.11.1/jquery-ui.min.js"></script>
        <script src="../assets/jquery-ui-1.11.1/jquery-ui.js"></script>
        <script src="../assets/js/moment.min.js"></script>
        <script src="../assets/js/moment.js"></script>
        <script src="../assets/js/JQueryDialog.js"></script>
        <script src="../assets/js/validate.js"></script>
        <script src="../assets/js/browserPopup.js"></script>
        <script src="../assets/js/datepicker.js"></script>
        <script src="../assets/js/datatable.js?v=1"></script>
        <script src="../assets/js/tinymce.js"></script>
        <!-- NON TEMPLATE-->
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        });

        function filename(fileupload) {
            if (fileupload.value != "") { return fileupload.files[0].name; }
            else { return ""; };
        }

        function pageLoad(sender, args) { /*execute on page load*/
            initTable("table1");
        }
        function f2(lnk) {
            if (window.opener != null && !window.opener.closed) {
                var controlid = document.getElementById("hfControlID").value
                var txtControl = opener.document.getElementById(controlid);
                txtControl.value += lnk.getAttribute('data-image');
                window.opener.__doPostBack(controlid, '');
                //window.opener.location.href = "/Admin/AdminRegister.aspx?mCode=" + lnk.getAttribute('name')
            }
            window.close();
        }

    </script>
    <!-- END JAVASCRIPTS -->
</body>
</html>
