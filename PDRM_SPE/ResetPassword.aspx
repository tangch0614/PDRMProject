<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ResetPassword.aspx.vb" Inherits="PDRM_SPE.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet"
        type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../assets/global/css/components-rounded.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../assets/pages/css/login-3.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
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
<body class="login">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <!-- BEGIN LOGIN -->
        <div class="content">
            <div class="login-form">
                <h3 class="form-title">
                    <i class="fa fa-key fa-fw"></i>
                    <asp:Label runat="server" ID="lblPageTitle">Reset Password</asp:Label>
                </h3>
                <asp:UpdatePanel runat="server" ID="upReset" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblLanguage" CssClass="control-label visible-ie8 visible-ie9" Text="Language"></asp:Label>
                            <div class="input-icon">
                                <i class="fa fa-globe fa-fw"></i>
                                <asp:DropDownList runat="server" ID="ddlLanguage" CssClass="form-control placeholder-no-fix" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblLoginID" CssClass="control-label visible-ie8 visible-ie9" Text="Login ID"></asp:Label>
                            <div class="input-icon">
                                <i class="fa fa-user fa-fw"></i>
                                <asp:TextBox runat="server" ID="txtLoginID" CssClass="form-control placeholder-no-fix"  />
                                <asp:RequiredFieldValidator runat="server" ID="rfvLoginID" ControlToValidate="txtLoginID" ErrorMessage="*Login ID cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="resetpass"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblEmail" CssClass="control-label visible-ie8 visible-ie9" Text="E-Mail"></asp:Label>
                            <div class="input-icon">
                                <i class="fa fa-envelope fa-fw"></i>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control placeholder-no-fix"  />
                                <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ErrorMessage="*Invalid e-mail address" Display="Dynamic" ForeColor="red" ValidationGroup="resetpass" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="*Email address cannot be blank" Display="Dynamic" ForeColor="red" ValidationGroup="resetpass"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblCaptcha" CssClass="control-label visible-ie8 visible-ie9" Text="Captcha Code"></asp:Label>
                            <div class="input-icon">
                                <i class="fa fa-lock fa-fw"></i>
                                <asp:TextBox runat="server" ID="txtCaptcha" CssClass="form-control placeholder-no-fix"  ClientIDMode="Static" AutoComplete="off" />
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCaptcha" ControlToValidate="txtCaptcha" ErrorMessage="*Please enter captcha code" Display="Dynamic" ForeColor="red" ValidationGroup="resetpass"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" ID="lblCaptchaValidate" Text="*Incorrect captcha code" ForeColor="red" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <div class="input-icon">
                                <asp:Image runat="server" CssClass="captcha" ID="imgCaptcha" ImageUrl="~/CaptchaCode.aspx" /><a href="javascript:void(0);" style="vertical-align: sub" onclick="RefreshImage('imgCaptcha','txtCaptcha');"><i class="fa fa-refresh fa-2x"></i></a>
                            </div>
                        </div>

                        <div class="form-actions">
                            <asp:Button runat="server" CssClass="btn red" ID="btnReset" Text="Reset Password" UseSubmitBehavior="false" OnClientClick='if(!validate2(this,"hfConfirm","resetpass")){return false};' OnClick="btnReset_Click" ValidationGroup="resetpass" />
                            <asp:Button runat="server" CssClass="btn blue" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:HiddenField runat="server" ID="hfConfirm" Value="Confirm register member?" ClientIDMode="Static" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- BEGIN COPYRIGHT -->
                <div class="copyright">
                    <asp:Label runat="server" ID="lblFooter"></asp:Label>
                </div>
                <!-- END COPYRIGHT -->
            </div>
        </div>
        <!-- END LOGIN -->


        <!--LOADING POPUP-->
        <asp:UpdateProgress runat="server" ID="upgReset" AssociatedUpdatePanelID="upReset">
            <ProgressTemplate>
                <div class="loadingPage">
                    <img src="../assets/img/loadingCircle.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../assets/pages/scripts/login.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script type="text/javascript">
        function RefreshImage(valImageId, valCaptcha) {
            var objImage = document.getElementById(valImageId)
            var txtCaptcha = document.getElementById(valCaptcha)
            if (objImage == undefined) {
                return;
            }
            var now = new Date();
            objImage.src = objImage.src.split('?')[0] + '?x=' + now.toUTCString();
            txtCaptcha.value = "";
        }
    </script>
    <script>
        jQuery(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
            // init background slide images
            //$.backstretch([
            // "../assets/img/bg1.png",
            // "../assets/img/bg2.jpg",
            // "../assets/img/bg3.jpg",
            // "../assets/img/bg4.jpg"
            //], {
            //    fade: 1000,
            //    duration: 8000
            //}
            //);
        });
    </script>
    <!-- END JAVASCRIPTS -->
</body>
</html>
