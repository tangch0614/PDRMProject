<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HistoryMap.aspx.vb" Inherits="PDRM_SPE.AHistoryMap" %>

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
    <link href="../assets/css/general.css?v=1" rel="stylesheet" />
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
            color: #FFFFFF;
            font-size: 10px;
            text-wrap: nowrap;
            position: relative;
            padding: 5px;
        }

        .opp-info td {
            padding: 5px 5px !important;
        }
    </style>
    <script>(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })
            ({ key: "AIzaSyA9AQTXBVGEnr8xB2k3chP1Ek5Yxk6gePU", v: "weekly" });</script>
    <script src="../assets/js/oms.min.js"></script>
    <!--MAP-->
    <script type="text/javascript">
        let map;
        let markers = [];
        let currentPolyline = null;
        let currentGeofence = null; // To track the currently open polygon
        let currentInfoWindow = null; // To track the currently open info window
        let openMarker = null; // To track the marker associated with the open info window
        let markercluster;

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
        async function fetchAndUpdateMarkers() {
            clearMarkers();
            //var deviceid = document.getElementById("ddlEMD").value;
            var oppid = document.getElementById("hfOPPID").value;
            var frdate = document.getElementById("hfFrDate").value;
            var todate = document.getElementById("hfToDate").value;
            var frtime = document.getElementById("ddlFrTime").value;
            var totime = document.getElementById("ddlToTime").value;
            var frdatetime = frdate + " " + frtime
            var todatetime = todate + " " + totime
            if (frdatetime == "") { frdatetime = getcurrentdate() + " 00:00:00" }
            if (todatetime == "") { todatetime = getcurrentdate() + " 23:59:59" }
            if (oppid > 0) {
                try {
                    const param = {
                        deviceid: -1,
                        oppid: oppid,
                        imei: "",
                        frdatetime: frdatetime,
                        todatetime: todatetime
                    };
                    const response = await fetch('../GetData.aspx/GetEMDHistory', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(param)
                    });
                    const jsonstr = await response.json();
                    const data = jsonstr.d // Assuming response comes under "d"
                    if (data.length > 0) {
                        updateMarkers(data);
                    } else {
                        alert(document.getElementById("hfNoResult").value);
                    }
                } catch (error) {
                    console.error('Error fetching marker data:', error);
                }
            }
        }

        // Get the content for the InfoWindow
        function getInfoWindowContent(location) {
            var content = "<div><table>" +
                "<tr><td>IMEI</td><td>: " + location.imei + "</td></tr>" +
                "<tr><td>DateTime</td><td>: " + location.datetime + "</td></tr>" +
                "<tr><td>GPS Status</td><td>: " + location.locstatus + "</td></tr>" +
                "<tr><td>GSM</td><td>: <img src='../assets/img/" + location.gsm + "'/></td></tr>" +
                "<tr><td>Battery</td><td>: " + location.battery + "</td></tr>" +
                "<tr><td>Belt Status</td><td>: " + location.beltstatus + "</td></tr>" +
                "<tr><td>Alarm</td><td>: " + location.alarm + "</td></tr>" +
                "<tr><td>Speed</td><td>: " + location.speed +
                "</table></div>"
            return content;
        }

        // Update markers on the map
        async function updateMarkers(locations) {
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
            const { InfoWindow } = await google.maps.importLibrary("maps");
            // Initialize OverlappingMarkerSpiderfier
            const oms = new OverlappingMarkerSpiderfier(map, {
                keepSpiderfied: true // Optional: Keep spiderfied markers expanded when clicked
            });

            // Loop through the new data and create markers
            locations.forEach((location, index) => {
                createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement, index + 1, locations.length, oms);
            });

            const arrowSymbol = {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW,
                scale: 1, // Adjust the size of the arrow
                strokeColor: '#000000', // Arrow color
                fillColor: '#000000',
                fillOpacity: 1,
            };

            if (currentPolyline) {
                currentPolyline.setMap(null);
            }
            currentPolyline = new google.maps.Polyline({
                path: locations, // Array of LatLng points
                geodesic: true,
                strokeColor: "#AF38EB",
                strokeOpacity: 1,
                strokeWeight: 5,
                icons: [
                    {
                        icon: arrowSymbol,
                        offset: '0',
                        repeat: '50px' // Place an arrow every 50 pixels along the line
                    }
                ],
            });
            currentPolyline.setMap(map);

            //show geofence
            if (currentGeofence) {
                currentGeofence.setMap(null);
            }
            if (locations[0].geofence != '') {
                currentGeofence = new google.maps.Polygon({
                    paths: JSON.parse(locations[0].geofence),
                    strokeColor: "#FF0000",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#FF0000",
                    fillOpacity: 0.35,
                    editable: false,
                });
                currentGeofence.setMap(map);
            }

            const bounds = new google.maps.LatLngBounds();
            markers.forEach((marker) => {
                bounds.extend(marker.position)
            })
            map.fitBounds(bounds);
        }

        // Create a new marker with AdvancedMarkerElement
        function createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement, index, totalmarkers, oms) {
            const infoWindow = new InfoWindow();

            // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
            let glyphLabel = document.createElement("div");
            glyphLabel.className = 'glyphLabel';
            glyphLabel.textContent = index;

            let pinColor = "blue";
            let glyph = glyphLabel; // Default to showing the index number

            // Customize the first and last markers
            if (index === 1) {
                pinColor = "red";  // First marker is blue
            } else if (index === totalmarkers) {
                pinColor = "red";  // Last marker white
                glyph = "🏁";
            }

            const pinElement = new PinElement({
                background: pinColor,
                glyphColor: location.pinglyphcolor,
                borderColor: "black",
                glyph: glyph,
            });

            const marker = new AdvancedMarkerElement({
                map: map,
                position: { lat: location.lat, lng: location.lng },
                content: pinElement.element,
                title: location.fldID,
                zIndex: index === totalmarkers ? totalmarkers : -index
            });

            // Add marker to OverlappingMarkerSpiderfier instance
            oms.addMarker(marker);

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

            markers.push(marker);
        }

        function clearMarkers() {
            markers.forEach(marker => {
                marker.setMap(null);
            });
            //markercluster.clearMarkers();
            markers = [];
            if (currentGeofence) {
                currentGeofence.setMap(null);
            }
            if (currentPolyline) {
                currentPolyline.setMap(null);
            }
        }

        function SetMapCenter(lat, lng) {
            map.setCenter({ lat: lat, lng: lng });
            map.setZoom(20);
        }

        function getcurrentdate() {
            const now = new Date();
            const year = now.getFullYear();
            const month = String(now.getMonth() + 1).padStart(2, '0');
            const day = String(now.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        }
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) { /*execute on page load*/
            initDatePicker("#txtFrDate", "#hfFrDate", "#txtToDate", "#hfToDate");
            $('#ddlEMD').select2();
            $('#ddlOPP').select2();
            $('#ddlFrTime').select2();
            $('#ddlToTime').select2();
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
                                <div class="portlet-title">
                                    <div class="caption">
                                        <asp:Label runat="server" ID="lblHeader" CssClass="caption-subject bold uppercase"><%=GetText("HistoryMap")%></asp:Label>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <table style="width: 100%" class="opp-info dataTable">
                                                <tr>
                                                    <td><%=GetText("EMD")%></td>
                                                    <td>
                                                        <asp:DropDownList runat="server" CssClass="form-control input-medium" ID="ddlEMD" AutoPostBack="true" OnSelectedIndexChanged="ddlEMD_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:HiddenField runat="server" ID="hfOPPID" ClientIDMode="Static" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Name") & " / " & GetText("ICNum")%></td>
                                                    <td>
                                                        <asp:DropDownList runat="server" CssClass="form-control input-medium" ID="ddlOPP" AutoPostBack="true" OnSelectedIndexChanged="ddlOPP_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Date")%></td>
                                                    <td>
                                                        <div class="input-group input-medium">
                                                            <div class="input-group-addon">
                                                                <%=GetText("From")%>
                                                            </div>
                                                            <input id="txtFrDate" name="txtFrDate" class="DateFrom form-control" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                        </div>
                                                        <asp:TextBox runat="server" ID="hfFrDate" ClientIDMode="Static" Style="display: none" />
                                                        <div class="input-group input-medium margin-top-10">
                                                            <div class="input-group-addon">
                                                                <%=GetText("To")%>
                                                            </div>
                                                            <input id="txtToDate" name="txtToDate" class="DateTo form-control" type="text" onkeydown="return false;" onpaste="return false;" autocomplete="off" readonly="true" />
                                                        </div>
                                                        <asp:TextBox runat="server" ID="hfToDate" ClientIDMode="Static" Style="display: none" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Time")%></td>
                                                    <td>
                                                        <div class="input-group input-medium select2-bootstrap-prepend">
                                                            <div class="input-group-addon">
                                                                <%=GetText("From")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlFrTime" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                        <div class="input-group input-medium select2-bootstrap-prepend margin-top-10">
                                                            <div class="input-group-addon">
                                                                <%=GetText("To")%>
                                                            </div>
                                                            <asp:DropDownList runat="server" ID="ddlToTime" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right">
                                                        <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text='Search' OnClick="btnSearch_Click" />
                                                        <asp:Button runat="server" CssClass="btn blue" ID="btnpdf" Text='Pdf' OnClick="btnpdf_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="plOPPInfo" CssClass="portlet light bordered" Visible="false">
                                <div class="portlet-body">
                                    <!-- BEGIN FORM-->
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <table style="width: 100%" class="opp-info dataTable table-bordered table-striped">
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Image runat="server" ID="imgPhoto" ImageUrl="../assets/img/No_Image.png" Style="width: 100px; height: 100px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Name")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtOPPName"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("ICNum")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtOPPICNo"></asp:Label>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td><%=GetText("OverseerItem").Replace("vITEM", GetText("Name"))%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtOverseerName"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("PoliceIDNo")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtPoliceNo"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("OverseerItem").Replace("vITEM", GetText("ContactNum"))%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtOverseerContactNo"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("PoliceStation")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtPoliceStation"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Department")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtDepartment"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtPSContactNo"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("IMEI")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtIMEI" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfNoResult" ClientIDMode="Static" />

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
