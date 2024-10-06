<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestMap.aspx.vb" Inherits="PDRM_SPE.TestMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #map {
            height: 100%;
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
    <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
    <script src="../assets/js/gmapapi.js"></script>
    <script type="text/javascript">
        let map;
        let markers = [];
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
            //initMarkers(locations)
            fetchAndUpdateMarkers();
            setInterval(fetchAndUpdateMarkers, 1000); // Fetch and update every 10 seconds
        }

        // Fetch marker data from server
        async function fetchAndUpdateMarkers() {
            try {
                const param = {
                    deviceid: document.getElementById("ddlEMD").value,
                    oppid: -1,
                    devicestatus: "Y",
                    oppstatus: "Y"
                };
                const response = await fetch('../GetData.aspx/GetEMDDeviceList', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(param)
                });
                const data = await response.json();
                updateMarkers(data.d); // Assuming response comes under "d"
            } catch (error) {
                console.error('Error fetching marker data:', error);
            }
        }

        // Get the content for the InfoWindow
        function getInfoWindowContent(location) {
            var content = "<div><table>" +
                "<tr><td>Name</td><td>: " + location.name + "</td></tr>" +
                "<tr><td>IMEI</td><td>: " + location.imei + "</td></tr>" +
                "<tr><td>DateTime</td><td>: " + location.datetime + "</td></tr>" +
                "<tr><td>Loc Status</td><td>: " + location.locstatus + "</td></tr>" +
                "<tr><td>Data Status</td><td>: " + location.datastatus + "</td></tr>" +
                "<tr><td>GSM</td><td>: <img src='../assets/img/" + location.gsm + "'/></td></tr>" +
                "<tr><td>GPS Sat</td><td>: " + location.gps + "</td></tr>" +
                "<tr><td>Bds Sat</td><td>: " + location.bds + "</td></tr>" +
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

            // Loop through the new data and update markers
            locations.forEach(location => {
                let existingMarker = markers.find(marker => marker.title === location.imei);

                if (existingMarker) {
                    // If the marker already exists, update its position
                    existingMarker.position = { lat: location.lat, lng: location.lng }; // Update position directly
                    if (existingMarker.name !== location.name) {
                        let glyphLabel = document.createElement("div");
                        glyphLabel.className = 'glyphLabel';
                        glyphLabel.textContent = location.name;

                        const pinElement = new PinElement({
                            background: location.pincolor,
                            glyphColor: location.pinglyphcolor,
                            borderColor: "black",
                            glyph: glyphLabel,
                        });

                        existingMarker.content = pinElement.element;
                    }

                    // If this marker's info window is currently open, update the content
                    if (openMarker === existingMarker) {
                        const newContent = getInfoWindowContent(location);
                        currentInfoWindow.setContent('');
                        currentInfoWindow.setContent(newContent);
                    }
                } else {
                    // If marker doesn't exist, create a new marker
                    createMarker(location, InfoWindow, AdvancedMarkerElement, PinElement);
                }
            });

            markers.forEach(marker => {
                const existingMarker = locations.find(location => location.imei === marker.title);
                if (!existingMarker) {
                    marker.setMap(null);
                    const markerindex = markers.indexOf(marker);
                    if (markerindex > -1) { markers.splice(markerindex, 1); }
                }
            });
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

            const marker = new AdvancedMarkerElement({
                map: map,
                position: { lat: location.lat, lng: location.lng },
                content: pinElement.element,
                title: location.imei
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
                        editable: true,
                    });
                    currentGeofence.setMap(map);
                }
            });

            infoWindow.addListener('closeclick', function () {
                // Remove the polygon from the map when the InfoWindow is closed
                if (currentGeofence) {
                    currentGeofence.setMap(null);
                }
            });

            markers.push(marker);
        }

        async function initMarkers(locations, setcenter = false) {
            const { Map, InfoWindow } = await google.maps.importLibrary("maps");
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
            const infoWindow = new google.maps.InfoWindow();
            if (markers.length > 0) {
                clearMarkers();
            }

            locations.forEach(property => {

                var content = "<table>" +
                    "<tr><td>Name</td><td>: " + property.name + "</td></tr>" +
                    "<tr><td>IMEI</td><td>: " + property.imei + "</td></tr>" +
                    "<tr><td>DateTime</td><td>: " + property.datetime + "</td></tr>" +
                    "<tr><td>Loc Status</td><td>: " + property.locstatus + "</td></tr>" +
                    "<tr><td>Data Status</td><td>: " + property.datastatus + "</td></tr>" +
                    "<tr><td>GSM</td><td>: <img src='../assets/img/" + property.gsm + "'/></td></tr>" +
                    "<tr><td>GPS Sat</td><td>: " + property.gps + "</td></tr>" +
                    "<tr><td>Bds Sat</td><td>: " + property.bds + "</td></tr>" +
                    "<tr><td>Battery</td><td>: " + property.battery + "</td></tr>" +
                    "<tr><td>Belt Status</td><td>: " + property.beltstatus + "</td></tr>" +
                    "<tr><td>Alarm</td><td>: " + property.alarm + "</td></tr>" +
                    "<tr><td>Speed</td><td>: " + property.speed +
                    "</table>"

                let glyphLabel = document.createElement("div");
                // set style and classes as needed
                glyphLabel.className = 'glyphLabel';
                glyphLabel.textContent = property.name;

                // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
                const pinelement = new PinElement({
                    background: property.pincolor,
                    glyphColor: property.pinglyphcolor,
                    borderColor: "black",
                    glyph: glyphLabel,
                });
                const marker = new google.maps.marker.AdvancedMarkerElement({
                    map,
                    position: { lat: property.lat, lng: property.long },
                    content: pinelement.element,
                    title: property.imei
                });

                // markers can only be keyboard focusable when they have click listeners
                // open info window when marker is clicked
                marker.addListener("click", () => {
                    infoWindow.setContent(content);
                    infoWindow.open(map, marker);
                });

                markers.push(marker);
            });

            // Add a marker clusterer to manage the markers.
            //markercluster = new markerClusterer.MarkerClusterer({ map, markers });

            //if (setcenter == true) {
            //    if (markers.length == 1) {
            //        map.setCenter(markers[0].position);
            //    } else {
            //        const bounds = new google.maps.LatLngBounds();
            //        markers.forEach((marker) => {
            //            bounds.extend(marker.position)
            //        })
            //        map.fitBounds(bounds)
            //    }
            //}

        }

        function clearMarkers() {
            markers.forEach(marker => {
                marker.setMap(null);
            });
            //markercluster.clearMarkers();
            markers = [];
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <%--<asp:Timer runat="server" Interval="10000" ID="timer" OnTick="timer_Tick"></asp:Timer>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div>
                    <asp:DropDownList runat="server" ID="ddlEMD" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="timer_Tick"></asp:DropDownList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="map" style="width: 100%; height: 100%;"></div>
    </form>

</body>

</html>
