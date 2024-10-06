<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TrackingHistoryReport.aspx.vb" Inherits="PDRM_SPE.ATrackingHistoryReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        /* Print specific styles */
        @media print {
            body * {
                visibility: hidden !important;
            }

            .printable, .printable * {
                visibility: visible !important;
            }

            .printable {
                position: absolute !important;
                left: 0 !important;
                top: 0 !important;
                width: 100% !important;
            }

            .report{
                width:100%;
            }

            .report tr {
                page-break-inside: avoid;
            }

            /* Ensure headers/footers on each page can be repeated */
            .report thead {
                display: table-header-group;
            }

            .report tfoot {
                display: table-footer-group;
            }

            .container{
                width:100% !important;
            }

            #map{
                width:auto !important;
            }
        }

        body {
            font-family: "Open Sans",sans-serif;
            font-size: 13.5px;
            color: #333;
            line-height: 1.42857;
        }

        hr {
            height: 2px;
            color: gray;
            background-color: gray;
            margin-left: 50px;
            margin-right: 50px;
        }

        #map {
            height: 600px; /* Set the height of the map */
            width: 90%; /* Set the width of the map */
            margin: auto;
            border: 3px solid green;
            padding: 10px;
        }

        .container {
            width: 90%;
            margin: 25px auto;
        }

        .info td {
            width: auto;
            padding: 2px 15px;
            text-align: left;
            vertical-align: top;
        }

        .report {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
            -webkit-print-color-adjust: exact;
        }

            .report td, .report th {
                border: 1px solid #ddd;
                padding: 8px;
                -webkit-print-color-adjust: exact;
            }

            .report tr:nth-child(even) {
                background-color: #f2f2f2;
                -webkit-print-color-adjust: exact;
            }

            .report th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3c6cac;
                color: white;
                -webkit-print-color-adjust: exact;
            }

        .btn {
            color: #FFF;
            background-color: #3598dc;
            border-color: #3598dc;
            display: inline-block;
            margin-bottom: 0;
            font-weight: 400;
            text-align: center;
            vertical-align: middle;
            touch-action: manipulation;
            cursor: pointer;
            border: 1px solid transparent;
            white-space: nowrap;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857;
            border-radius: 4px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            box-shadow: none !important;
            outline: 0 !important;
            line-height: 1.44;
        }

        .glyphLabel {
            color: #FFFFFF;
            font-size: 10px;
            text-wrap: nowrap;
            position: relative;
            padding: 5px;
        }
    </style>
    
    <script src="../assets/js/gmapapi.js"></script>
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
                //zoomControl: false,
                //gestureHandling: 'none',
            });
        }

        // Fetch marker data from server
        async function fetchAndUpdateMarkers() {
            clearMarkers();
            //var deviceid = document.getElementById("ddlEMD").value;
            var oppid = document.getElementById("hfOPPID").value;
            var frdatetime = document.getElementById("hfFrDate").value;
            var todatetime = document.getElementById("hfToDate").value;
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
                "<tr><td><%=GetText("From") + " " + GetText("DateTime")%></td><td>: " + location.datetime + "</td></tr>" +
                "<tr><td><%=GetText("To") + " " + GetText("DateTime")%></td><td>: " + location.datetimeto + "</td></tr>" +
                "<tr><td><%=GetText("GPSStatus")%></td><td>: " + location.locstatus + "</td></tr>" +
                "<tr><td><%=GetText("GSMSignal")%></td><td>: <img src='../assets/img/" + location.gsm + "'/></td></tr>" +
                "<tr><td><%=GetText("Battery")%></td><td>: " + location.battery + "</td></tr>" +
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

            // Loop through the new data and create markers
            locations.forEach((location, index) => {
                createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement, index + 1, locations.length);
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
        function createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement, index, totalmarkers) {
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
</head>
<body style="background: #fff !important">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container" style="text-align: right">
            <button onclick="printPage()" class="btn"><%=GetText("Save") & " / " & GetText("Print")%></button>
        </div>
        <div id="PanelContents" runat="server" class="printable">
            <div>
                <asp:HiddenField runat="server" ID="hfOPPID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hfFrDate" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hfToDate" ClientIDMode="Static" />
                <div style="margin-bottom: 10px; text-align: center">
                    <img src="#" id="logolable" runat="server" style="width: 150px; padding: 10px;" />
                </div>
                <div style="margin-bottom: 5px; text-align: center">
                    <asp:Label runat="server" ID="lblHeader" Style="text-transform: uppercase" CssClass="caption-subject bold uppercase" Font-Size="X-Large" Text="Laporan EMD - "> </asp:Label>
                    <asp:Label runat="server" ID="txtHeader" CssClass="caption-subject bold uppercase" Font-Size="X-Large" Text=""> </asp:Label>
                </div>
                <div style="margin-bottom: 10px; text-align: center">
                    <asp:Label runat="server" ID="lblHeaderDateTime" CssClass="caption-subject bold uppercase" Font-Size="Small" Text=""> </asp:Label>
                </div>
                <hr />
            </div>
            <table class="container">
                <tr>
                    <td style="width: auto; vertical-align: top;">
                        <table class="info">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblDate" Text="Tarikh"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtDate" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblEMD" Text="EMD"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtEMD" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Nama"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtName" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblIC" Text="No. K/P"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtIC" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPegawaiPengawasan" Text="Nama Pegawai Pengawasan"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtPegawaiPengawasan" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPoliceNo" Text="PoliceNo"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtPoliceNo" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTelPegawai" Text="No. Tel. Pegawai Pengawasan"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtTelPegawai" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblbalai" Text="Balai Polis"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtbalai" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblJabatan" Text="Jabatan"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtJabatan" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTelBalai" Text="No. Tel. Balai Polis"></asp:Label></td>
                                <td>:</td>
                                <td>
                                    <asp:Label runat="server" ID="txtTelBalai" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top; text-align: right;">
                        <asp:Image runat="server" ID="imgPhoto" ImageUrl="#" Style="width: 200px; height: 200px;" />
                    </td>
                </tr>
            </table>
            <div id="map"></div>
            <div class="container" style="page-break-before: always;">
                <div>
                    <asp:Label runat="server" ID="lblAlert" CssClass="caption-subject bold uppercase" Font-Size="X-Large" Text="Alert"> </asp:Label>
                </div>
                <div>
                    <table id="alert" class="report">
                        <tr runat="server" id="dvNoResult" visible="false">
                            <td colspan="3" align="center">
                                <div runat="server" style="text-align: center" class="form-group last">
                                    <p>
                                        <%=GetText("ErrorNoResult")%>
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptAlert">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th>
                                            <%#GetText("num")%>
                                        </th>
                                        <th>
                                            <%#GetText("datetime")%>
                                        </th>
                                        <th>
                                            <%#GetText("Alert")%>
                                        </th>
                                        <th>
                                            <%#GetText("FirstResponder")%>
                                        </th>
                                        <th>
                                            <%#GetText("LastResponder")%>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Container.ItemIndex + 1%>
                                    </td>
                                    <td>
                                        <%#Eval("fldDatetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldmsg").toupper%>
                                    </td>
                                    <td>
                                        <%#Eval("fldprocessbyname")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldlastprocessbyname")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div class="container">
                <div>
                    <asp:Label runat="server" ID="lblhistory" CssClass="caption-subject bold uppercase" Font-Size="X-Large" Text="History"> </asp:Label>
                </div>
                <div>
                    <table id="history" class="report">
                        <tr runat="server" id="dvNoResult2" visible="false">
                            <td colspan="11" align="center">
                                <div runat="server" style="text-align: center" class="form-group last">
                                    <p>
                                        <%=GetText("ErrorNoResult")%>
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptHistory">
                            <HeaderTemplate>
                                <thead>
                                    <tr>
                                        <th>
                                            <%#GetText("Num")%>
                                        </th>
                                        <th>
                                            <%#GetText("DateTime")%>
                                        </th>
                                        <th>
                                            <%#GetText("IMEI")%>
                                        </th>
                                        <th>
                                            <%#GetText("Latitude")%>
                                        </th>
                                        <th>
                                            <%#GetText("Longitude")%>
                                        </th>
                                        <th>
                                            <%#GetText("GPSStatus")%>
                                        </th>
                                        <th>
                                            <%#GetText("GSMSignal") & " (%)"%>
                                        </th>
                                        <th>
                                            <%#GetText("Battery") & " (%)"%>
                                        </th>
                                        <th>
                                            <%#GetText("Charging")%>
                                        </th>
                                        <th>
                                            <%#GetText("BeltStatus")%>
                                        </th>
                                        <th>
                                            <%#GetText("Speed") & " (km/h)"%>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Container.ItemIndex + 1%>
                                    </td>
                                    <td>
                                        <%#Eval("fldDeviceDatetime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldimei")%>
                                    </td>

                                    <td>
                                        <%#CDec(Eval("fldlat")).ToString("0.##########")%>
                                    </td>
                                    <td>
                                        <%#CDec(Eval("fldlong")).ToString("0.##########")%>
                                    </td>
                                    <td>
                                        <%#If(Eval("fldGPSStatus").Equals("V"), GetText("LastGPS"), If(Eval("fldGPSStatus").Equals("A"), GetText("Real-time"), GetText("-")))%>
                                    </td>
                                    <td>
                                        <%#Eval("fldGSMSignalPercent")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldbatteryLvl")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldchargingstatus")%>
                                    </td>
                                    <td>
                                        <%#Eval("fldbeltstatus")%>
                                    </td>
                                    <td>
                                        <%#CDec(Eval("fldSpeedKmh")).ToString("N2")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
        <div class="container" style="text-align: right">
            <button onclick="printPage()" class="btn"><%=GetText("Save") & " / " & GetText("Print")%></button>
        </div>
    </form>

    <script>
        function printPage() {
            window.print(); // Calls the browser's print dialog
        }
    </script>
</body>
</html>
