<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AlertInfo.aspx.vb" Inherits="PDRM_SPE.AAlertInfo" %>

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

        .opp-info td {
            padding: 5px 5px !important;
        }

        .table {
            table-layout: fixed;
        }

            .table tr {
                vertical-align: top;
            }

                .table tr td:first-child {
                    width: 30%;
                    word-break: auto-phrase;
                    overflow: hidden;
                }
    </style>
    <script src="../assets/js/gmapapi.js"></script>
    <!--MAP-->
    <script type="text/javascript">
        let map;
        let marker = null;
        let currentGeofence = null; // To track the currently open polygon
        let currentInfoWindow = null; // To track the currently open info window
        let openMarker = null; // To track the marker associated with the open info window
        let markercluster;
        let focusmarker = true;
        let isUserInteracting = false;

        async function initMap() {
            // Request needed libraries.
            const { Map } = await google.maps.importLibrary("maps");
            map = new Map(document.getElementById("map"), {
                center: { lat: 4.2105, lng: 108.9758 },
                zoom: 6,
                mapId: "4504f8b37365c3d0",
            });

            // Listen for the 'dragstart' and 'idle' events on the map
            map.addListener('dragstart', () => {
                isUserInteracting = true;
                focusmarker = false; // User is interacting with the map, stop centering
            });

            // Listen for idle event - when the map stops moving
            map.addListener('idle', () => {
                if (isUserInteracting) {
                    setTimeout(() => {
                        focusmarker = true;  // Re-enable centering after some time
                        isUserInteracting = false;
                    }, 10000);  // Adjust this duration based on your needs
                }
            });

            //initMarkers(locations)
            fetchAndUpdateMarkers(focusmarker);
            setInterval(() => fetchAndUpdateMarkers(focusmarker), 5000); // Fetch and update every 10 seconds
        }

        // Fetch marker data from server
        async function fetchAndUpdateMarkers(setcenter) {
            //var deviceid = document.getElementById("ddlEMD").value;
            var oppid = document.getElementById("hfOPPID").value;
            if (oppid > 0) {
                try {
                    const param = {
                        deviceid: -1,
                        oppid: oppid,
                    };
                    const response = await fetch('../GetData.aspx/GetEMDDeviceInfo', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(param)
                    });
                    const jsondata = await response.json();
                    const data = jsondata.d // Assuming response comes under "d"
                    if (data != null) {
                        updateMarkers(data, setcenter);
                    }
                } catch (error) {
                    alert('Error fetching EMD data.');
                    console.error('Error fetching EMD data:', error);
                }
            } else {
                clearMarkers();
            }
        }

        // Get the content for the InfoWindow
        function getInfoWindowContent(location) {
            var content = "<div><table>" +
                "<tr><td>IMEI</td><td>: " + location.imei + "</td></tr>" +
                "<tr><td>DateTime</td><td>: " + location.datetime + "</td></tr>" +
                "<tr><td>GPS Status</td><td>: " + location.locstatus + "</td></tr>" +
                "<tr><td>GSM</td><td>: <img src='../assets/img/" + location.gsm + "' /></td></tr>" +
                "<tr><td>Battery</td><td>: " + location.battery + "</td></tr>" +
                "<tr><td>Belt Status</td><td>: " + location.beltstatus + "</td></tr>" +
                "<tr><td>Alarm</td><td>: " + location.alarm + "</td></tr>" +
                "<tr><td>Speed</td><td>: " + location.speed +
                "</table></div>"
            return content;
        }

        // Update markers on the map
        async function updateMarkers(location, setcenter) {
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
            const { InfoWindow } = await google.maps.importLibrary("maps");


            if (marker && marker.title === location.imei) {
                // If the marker already exists, update its position
                marker.position = { lat: location.lat, lng: location.lng }; // Update position directly
                if (marker.name !== location.name) {
                    let glyphLabel = document.createElement("div");
                    glyphLabel.className = 'glyphLabel';
                    glyphLabel.textContent = location.name;

                    const pinElement = new PinElement({
                        background: location.pincolor,
                        glyphColor: location.pinglyphcolor,
                        borderColor: "black",
                        glyph: glyphLabel,
                    });

                    marker.content = pinElement.element;
                }

                updateMarkerClickListener(marker, location, InfoWindow, AdvancedMarkerElement, PinElement);

                // If this marker's info window is currently open, update the content
                if (openMarker === marker) {
                    const newContent = getInfoWindowContent(location);
                    currentInfoWindow.setContent('');
                    currentInfoWindow.setContent(newContent);

                    //show geofence
                    if (currentGeofence) {
                        if (location.geofence != '') {
                            currentGeofence.setPath(JSON.parse(location.geofence));
                        }
                    }
                }

                if (setcenter == true) {
                    map.setCenter(marker.position);
                    //map.setZoom(15);
                }
            } else {
                // If marker doesn't exist, create a new marker
                clearMarkers();
                createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement);
            }
        }

        // Create a new marker with AdvancedMarkerElement
        function createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement) {
            const infoWindow = new InfoWindow();

            let glyphLabel = document.createElement("div");
            glyphLabel.className = 'glyphLabel';
            glyphLabel.textContent = location.name;

            // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
            const pinElement = new PinElement({
                background: location.pincolor,
                glyphColor: location.pinglyphcolor,
                borderColor: "black",
                glyph: glyphLabel,
            });

            marker = new AdvancedMarkerElement({
                map: map,
                position: { lat: location.lat, lng: location.lng },
                content: pinElement.element,
                title: location.imei
            });

            //show geofence
            if (currentGeofence) {
                currentGeofence.setMap(null);
            }
            if (location.geofence != '') {
                currentGeofence = new google.maps.Polygon({
                    paths: JSON.parse(location.geofence),
                    strokeColor: "#FF0000",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#FF0000",
                    fillOpacity: 0.35,
                    editable: false,
                });
                currentGeofence.setMap(map);
            }

            marker.addListener("click", () => {
                const content = getInfoWindowContent(location);
                infoWindow.setContent(content);
                if (currentInfoWindow) {
                    currentInfoWindow.close();
                }
                infoWindow.open(map, marker);
                currentInfoWindow = infoWindow;
                openMarker = marker;

            });

            infoWindow.addListener('closeclick', function () {
                // Remove the polygon from the map when the InfoWindow is closed
                if (currentGeofence) {
                    currentGeofence.setMap(null);
                }
            });

            map.setCenter(marker.position);
            map.setZoom(15);
        }

        // Function to update the click listener for a specific marker
        function updateMarkerClickListener(marker, location, InfoWindow, AdvancedMarkerElement, PinElement) {
            const infoWindow = new InfoWindow();

            // Remove any existing click listener
            google.maps.event.clearListeners(marker, 'click');

            // Set the new click listener
            marker.addListener("click", () => {
                const content = getInfoWindowContent(location);
                infoWindow.setContent(content);
                if (currentInfoWindow) {
                    currentInfoWindow.close();
                }
                infoWindow.open(map, marker);
                currentInfoWindow = infoWindow;
                openMarker = marker;
            });
        }

        function clearMarkers() {
            if (marker) {
                marker.setMap(null);
            }
        }

    </script>
</head>
<body style="background-color: #f1f1f1 !important;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="map-container">
            <div style="display: flex; flex-direction: row; height: 100%">
                <div style="width: 50%; height: 100%; padding: 10px; display: flex; flex-direction: column;">
                    <div class="col-md-12" style="height: 100%; padding: 10px;">
                        <div id="map" style="width: 100%; height: 100%; padding: 10px;"></div>
                    </div>
                </div>
                <div id="notification" style="overflow-y: scroll; height: 100%; width: 50%; padding: 10px;">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="plViolateTerms" CssClass="portlet box red" Style="width: 100%; margin: 0 auto">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-check fa-fw"></i>
                                        <asp:Label runat="server" ID="lblViolateTermsInfo" CssClass="caption-subject bold uppercase"></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group margin-bottom-5">
                                                        <div style="text-align: center">
                                                            <asp:Image runat="server" ID="imgPhoto1Preview" ClientIDMode="Static" Style="height: 150px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <table class="opp-info table table-bordered table-striped">
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("IMEI")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtImei" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Name")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtSubjectName"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ICNum")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtSubjectICNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ContactNum")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtSubjectContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Department")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtDepartment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceStation")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPoliceStation" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPSContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Township")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtMukim"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Overseer")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtOverseer" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceIDNo")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtOverseerIDNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("OfficerItem").Replace("vITEM", GetText("ContactNum"))%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtOverseerContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-md-6">
                                                    <table class="opp-info table table-bordered table-striped">
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ViolateTerms")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtViolateTerms" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("DateTime")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtDateTime" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trRemarkHist">
                                                            <td>
                                                                <label><%=GetText("RemarkHistory")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtRemarkHist" TextMode="MultiLine" Rows="10" Style="width: 100%" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trRemark">
                                                            <td>
                                                                <label><%=GetText("Remark")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Rows="5" Style="width: 100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("AcknowledgeBy")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtAcknowledgeByID"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("AcknowledgeDateTime")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtAcknowledgeDateTime"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trCompleteBy">
                                                            <td>
                                                                <label><%=GetText("CompleteBy")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtCompleteByID"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trCompleteDateTime">
                                                            <td>
                                                                <label><%=GetText("CompleteDateTime")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtCompleteDateTime"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="pull-right">
                                                <asp:Button runat="server" CssClass="btn purple " ID="btnAcknowledge" Text="Maklum Terima" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnAcknowledge_Click" ClientIDMode="static" />
                                                <asp:Button runat="server" CssClass="btn blue " ID="btnCompleted" Text="Selesai" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnCompleted_Click" ClientIDMode="static" />
                                                <asp:Button runat="server" CssClass="btn default " ID="btnCancel" Text="Tutup" OnClick="btnCancel_Click" ClientIDMode="static" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfOPPID" ClientIDMode="Static" />
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
