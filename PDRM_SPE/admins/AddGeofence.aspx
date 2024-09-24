<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddGeofence.aspx.vb" Inherits="PDRM_SPE.AddGeofence" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #map {
            position: fixed !important;
        }

        #button-container {
            position: fixed;
            bottom: 0;
            z-index: 999;
            justify-content: center;
            align-items: center;
            width: 100%;
            background: rgba(255, 255, 255, 0.8);
            padding: 10px;
            margin: 0 -62px;
        }
    </style>
    <!-- BEGIN PRIOIRITY PLUGINS -->
    <script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/js/modalpopup.js"></script>
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
    <link href="../assets/layouts/layout3/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout3/css/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout3/css/custom.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <!-- NON TEMPLATE-->
    <link href="../assets/css/general.css?v=1.1" rel="stylesheet" />
    <link href="../assets/css/layout3.css" rel="stylesheet" />
    <link href="../assets/jquery-ui-1.11.1/jquery-ui.css" rel="stylesheet" />
    <!-- NON TEMPLATE-->
    <link rel="shortcut icon" href="../assets/img/favicon.ico" />
    <link href="../assets/img/companylogo.png" rel="icon" sizes="32x32" />
    <link href="../assets/img/companylogo.png" rel="icon" sizes="192x192" />
    <link href="../assets/img/companylogo.png" rel="apple-touch-icon-precomposed" />
    <meta content="../assets/img/companylogo.png" name="msapplication-TileImage" />
    <script src="../assets/js/gmapapi.js"></script>
    <script type="text/javascript">

        let map;
        let polygon = null;

        async function initMap(deflat, deflng, geofenceL) {
            // Request needed libraries.
            const { Map } = await google.maps.importLibrary("maps");
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
            map = new Map(document.getElementById("map"), {
                center: { lat: deflat, lng: deflng },
                zoom: 14,
                mapId: "4504f8b37365c3d0",
            });

            const pinelement = new PinElement({
                background: "blue",
                glyphColor: "black",
            });

            const marker = new AdvancedMarkerElement({
                map: map,
                position: { lat: deflat, lng: deflng },
                content: pinelement.element
            });

            polygon = new google.maps.Polygon({
                paths: JSON.parse(geofenceL),
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
            document.getElementById("hfCordinates").value = storeDBString;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div id="button-container">
            <div class="pull-right">
            <asp:Button runat="server" CssClass="btn custom-color" Style="box-shadow: 0 2px 6px rgba(0,0,0,.3) !important;" ID="btnSubmit" Text="Kemas Kini" OnClientClick="return processCoordinates();" OnClick="btnSubmit_Click" ClientIDMode="static" />
            <asp:Button runat="server" CssClass="btn default" Style="box-shadow: 0 2px 6px rgba(0,0,0,.3) !important;" ID="btnReset" Text="Tetapkan semula" OnClick="btnReset_Click" ClientIDMode="static" />
        </div></div>
        <div id="map" style="width: 100%; height: 100%;"></div>
        <asp:HiddenField ID="hfCordinates" runat="server" ClientIDMode="Static" />
    </form>

    <!--[if lt IE 9]>
<script src="../assets/global/plugins/respond.min.js"></script>
<script src="../assets/global/plugins/excanvas.min.js"></script> 
<script src="../assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
    <!-- BEGIN CORE PLUGINS -->
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
    <script src="../assets/layouts/layout3/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout3/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <!-- END THEME LAYOUT SCRIPTS -->
</body>
</html>
