<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeofenceMukim.aspx.vb" Inherits="PDRM_SPE.AGeofenceMukim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
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
    <link href="../assets/global/plugins/bootstrap-toastr/toastr.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
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
    <link href="../assets/css/general.css?v=1.1" rel="stylesheet" />
    <link href="../assets/css/layout1.css" rel="stylesheet" />
    <link href="../assets/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet" />
    <!-- NON TEMPLATE-->

    <style type="text/css">
        #map {
            height: 100%;
        }

        .map-container {
            height: 100vh; /* Full height of the viewport */
            width: 100vw; /* Full width of the viewport */
        }

        html, body, form {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        .glyphLabel {
            background-color: #808080;
            border-radius: 8px;
            color: #FFFFFF;
            font-size: 12px;
            text-wrap: nowrap;
            position: relative;
            padding: 5px;
            margin-bottom: 50px;
        }
    </style>
    <script src="../assets/js/gmapapi.js"></script>
    <!--MAP-->
    <script type="text/javascript">
        let map;
        let polygon = null;

        async function initMap() {
            // Request needed libraries.
            const { Map } = await google.maps.importLibrary("maps");
            map = new Map(document.getElementById("map"), {
                center: { lat: 4.2105, lng: 108.9758 },
                zoom: 6,
                mapId: "4504f8b37365c3d0",
            });
        }

        // Fetch marker data from server
        function getGeofence(geofence, lat, lng, zoom) {
            //if (geofence) {
            //    const bounds = new google.maps.LatLngBounds();
            //    geofence.forEach((coord) => {
            //        bounds.extend(coord);
            //    });
            //    map.fitBounds(bounds);
            //} 
            map.setCenter({ lat: lat, lng: lng });
            map.setZoom(zoom);

            if (polygon) {
                polygon.setMap(null);
            }
            if (geofence) {
                polygon = new google.maps.Polygon({
                    paths: geofence,
                    strokeColor: "#FF0000",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#FF0000",
                    fillOpacity: 0.35,
                    editable: true,
                    draggable: true,
                });
                polygon.setMap(map);
            }
        }

        function SetMapCenter(lat, lng, zoom) {
            if (map) {
                map.setCenter({ lat: lat, lng: lng });
                map.setZoom(zoom);
            }
        }

        function processCoordinates() {
            // Construct the polygon.
            const vertices = polygon.getPath();
            let storeDBString = "[";
            // Iterate over the vertices.
            for (let i = 0; i < vertices.getLength(); i++) {
                const xy = vertices.getAt(i);
                storeDBString += "{" + '"' + "lat" + '":' + xy.lat() + "," + '"' + "lng" + '":' + xy.lng() + "}";
                if (i < vertices.length - 1) {
                    storeDBString += ",";
                }
            }
            storeDBString += "]";
            // Update hidden fields
            document.getElementById("hfGeofence").value = storeDBString;
        }
    </script>

    <script type="text/javascript">
        function pageLoad(sender, args) { /*execute on page load*/
            $('#ddlState').select2();
            $('#ddlDistrict').select2();
            $('#ddlMukim').select2();
        }
    </script>

</head>
<body style="background-color: #f1f1f1 !important;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="map-container">
            <div style="display: flex; flex-direction: row; height: 100%">
                <div style="width: 70%; height: 100%; padding: 10px; display: flex; flex-direction: column;">
                    <div class="col-md-12" style="height: 100%; padding: 10px;">
                        <div id="map" style="width: 100%; height: 100%; padding: 10px;"></div>
                    </div>
                </div>
                <div id="notification" style="overflow-y: scroll; height: 100%; width: 30%; padding: 10px;">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="portlet light bordered">
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <table style="width: 100%" class="opp-info dataTable">
                                                <tr>
                                                    <td><%=GetText("State")%></td>
                                                    <td>
                                                        <asp:DropDownList runat="server" CssClass="form-control input-medium" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:HiddenField runat="server" ID="hfOPPID" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("District")%></td>
                                                    <td>
                                                        <asp:DropDownList runat="server" CssClass="form-control input-medium" ID="ddlDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Township")%></td>
                                                    <td>
                                                        <asp:DropDownList runat="server" CssClass="form-control input-medium" ID="ddlMukim" AutoPostBack="true" OnSelectedIndexChanged="ddlMukim_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right">
                                                        <asp:Button runat="server" CssClass="btn blue" ID="btnSubmit" Text='Search' OnClientClick="if(!confirm(hfConfirm.value)){return false;}else{return processCoordinates();}" OnClick="btnSubmit_Click" />
                                                        <asp:HiddenField runat="server" ID="hfGeofence" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfConfirm" ClientIDMode="Static" />

        <!-- BEGIN CORE PLUGINS -->
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
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="../assets/global/plugins/bootstrap-toastr/toastr.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/ui-toastr.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- NON TEMPLATE-->
        <script src="../assets/tinymce3.x/jscripts/tiny_mce/tiny_mce.js"></script>
        <script src="../assets/jquery-ui-1.11.1/jquery-ui.min.js"></script>
        <script src="../assets/jquery-ui-1.11.1/jquery-ui.js"></script>
        <script src="../assets/js/moment.min.js"></script>
        <script src="../assets/js/moment.js"></script>
        <script src="../assets/js/JQueryDialog.js"></script>
        <script src="../assets/js/validate.js?v=1.00"></script>
        <script src="../assets/js/browserPopup.js"></script>
        <script src="../assets/js/datepicker.js"></script>
        <script src="../assets/js/datatable.js?v=1"></script>
        <script src="../assets/js/tinymce.js"></script>
        <script src="../assets/js/modalpopup.js"></script>
        <!-- NON TEMPLATE-->
        <script>
            jQuery(document).ready(function () {
                $('#clickmewow').click(function () {
                    $('#radio1003').attr('checked', 'checked');
                });
                $.datepicker.setDefaults($.datepicker.regional[getLanguage()]);
            });

            function getCookie(cname) {
                var name = cname + "=";
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1);
                    if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
                }
                return "";
            }

            function getLanguage() {
                var language = getCookie("LanguageCookie")
                if (language == 'ZH-CN') {
                    return "zh-CN";
                }
                else {
                    return "";
                }
            }
        </script>
    </form>
</body>

</html>
