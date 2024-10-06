<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="Home.aspx.vb" Inherits="PDRM_SPE.AHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        #map {
            height: 400px !important;
        }

        .map-container {
            height: 78vh; /* Full height of the viewport */
            width: 100%;
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

        .toast-high {
            background-color: #d91e18 !important; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-medium {
            background-color: #e87e04 !important; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-low {
            background-color: #f3c200 !important; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-title {
            text-transform: uppercase !important;
            padding-left: 30px !important;
            margin-bottom: 5px !important;
        }

        .toast-message table td {
            padding: 5px 5px !important;
        }

        #toast-container {
            max-height: 100vh !important; /* Set the max height for the container */
            overflow-y: auto !important; /* Enable vertical scrolling */
            position: relative !important;
        }

            #toast-container > .toast {
                margin-bottom: 10px !important; /* Add some space between toasts */
            }

            #toast-container > div {
                margin: 0 0 6px !important;
                padding: 10px !important;
                width: 100% !important;
                background-position: 10px 7px !important;
                opacity: 1 !important;
            }
    </style>
    <script src="../assets/js/gmapapi.js"></script>
    <!--MAP-->
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
            fetchAndUpdateMarkers(false);
            //$('#initmodal').modal('show');
            setInterval(fetchAndUpdateMarkers, 10000); // Fetch and update every 10 seconds
        }

        // Fetch marker data from server
        async function fetchAndUpdateMarkers(setcenter) {
            try {
                const param = {
                    deviceid: -1,
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
                updateMarkers(data.d, setcenter); // Assuming response comes under "d"
            } catch (error) {
                console.error('Error fetching marker data:', error);
            }
        }

        // Get the content for the InfoWindow
        function getInfoWindowContent(location) {
            var content = "<div><table>" +
                "<tr><td colspan=2 style='font-weight:bold;padding-top:5px;border-bottom:1px solid #ccc;'><%=GetText("OPP")%></td></tr>" +
                "<tr><td><%=GetText("Name")%></td><td>: " + location.oppname + "</td></tr>" +
                "<tr><td><%=GetText("ICNum")%></td><td>: " + location.oppicno + "</td></tr>" +
                "<tr><td><%=GetText("ContactNum")%></td><td>: " + location.oppcontactno + "</td></tr>" +
                "<tr><td><%=GetText("Department")%></td><td>: " + location.department + "</td></tr>" +
                "<tr><td><%=GetText("PoliceStation")%></td><td>: " + location.psname + "</td></tr>" +
                "<tr><td colspan=2 style='font-weight:bold;padding-top:5px;border-bottom:1px solid #ccc;'><%=GetText("Overseer")%></td></tr>" +
                "<tr><td><%=GetText("Name")%></td><td>: " + location.offname + "</td></tr>" +
                "<tr><td><%=GetText("ContactNum")%></td><td>: " + location.offcontactno + "</td></tr>" +
                "<tr><td><%=GetText("PoliceIDNo")%></td><td>: " + location.offpoliceno + "</td></tr>" +
                "<tr><td colspan=2 style='font-weight:bold;padding-top:5px;border-bottom:1px solid #ccc;'>EMD</td></tr>" +
                "<tr><td>IMEI</td><td>: " + location.imei + "</td></tr>" +
                "<tr><td><%=GetText("Date")%></td><td>: " + location.datetime + "</td></tr>" +
                "<tr><td><%=GetText("Latitude")%></td><td>: " + location.lat + "</td></tr>" +
                "<tr><td><%=GetText("Longitude")%></td><td>: " + location.lng + "</td></tr>" +
                "<tr><td><%=GetText("GPSStatus")%></td><td>: " + location.locstatus + "</td></tr>" +
                "<tr><td><%=GetText("GSMSignal")%></td><td>: <img src='../assets/img/" + location.gsm + "'/></td></tr>" +
                "<tr><td><%=GetText("Battery")%></td><td>: " + location.battery + "</td></tr>" +
                "<tr><td><%=GetText("BeltStatus")%></td><td>: " + location.beltstatus + "</td></tr>" +
                "<tr><td><%=GetText("Alarm")%></td><td>: " + location.alarm + "</td></tr>" +
                "<tr><td><%=GetText("Speed")%></td><td>: " + location.speed +
                "</table></div>"
            return content;
        }

        // Update markers on the map
        async function updateMarkers(locations, setcenter) {
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

                    updateMarkerClickListener(existingMarker, location, InfoWindow, AdvancedMarkerElement, PinElement);

                    // If this marker's info window is currently open, update the content
                    if (openMarker === existingMarker) {
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

            if (setcenter == true) {
                if (markers.length == 1) {
                    map.setCenter(markers[0].position);
                } else {
                    const bounds = new google.maps.LatLngBounds();
                    markers.forEach((marker) => {
                        bounds.extend(marker.position)
                    })
                    map.fitBounds(bounds)
                }
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
                        editable: false,
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
            });
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

        function SetMapCenter(lat, lng, zoom) {
            map.setCenter({ lat: lat, lng: lng });
            map.setZoom(zoom);
        }

    </script>

    <!--Notification-->
    <script type="text/javascript">
        var priorityMap = {
            'high': 3,
            'medium': 2,
            'low': 1
        };

        function fetchNotifications() {
            var userid = <%=UserID%>;
            const param = {
                deviceid: -1,
                oppid: -1,
                userid: userid,
                page: "homepage"
            };
            $.ajax({
                type: "POST",
                url: "../GetData.aspx/GetNotificationData",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var notifications = JSON.parse(response.d);
                    if (notifications.length > 0) {
                        notifications.sort(function (a, b) {
                            return priorityMap[a.fldseverity] - priorityMap[b.fldseverity];
                        });
                        notifications.forEach(notification => {
                            var img = notification.fldphoto1
                            if (img == '') {
                                img = "../assets/img/No_Image.png";
                            };
                            var tableHTML = "<div align='center'><img src='" + img + "' style='width:100px;height:100px;'/></div>";
                            tableHTML += "<table style='width: 100%;' class='dataTable table-bordered table-striped'>";
                            tableHTML += "<tr><td><%=GetText("IMEI")%></td><td class='bold'>" + notification.fldimei + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("Name")%></td><td class='bold'>" + notification.fldoppname + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("ICNum")%></td><td class='bold'>" + notification.fldoppicno + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("ContactNum")%></td><td class='bold'>" + notification.fldoppcontactno + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("OfficerItem").Replace("vITEM", GetText("Name"))%></td><td class='bold'>" + notification.fldoverseername + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("PoliceIDNo")%></td><td class='bold'>" + notification.fldoverseerpoliceno + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("OfficerItem").Replace("vITEM", GetText("ContactNum"))%></td><td class='bold'>" + notification.fldoverseercontactno + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("Department")%></td><td class='bold'>" + notification.flddepartment + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("PoliceStation")%></td><td class='bold'>" + notification.fldpsname + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))%></td><td class='bold'>" + notification.fldpscontactno + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("Township")%></td><td class='bold'>" + notification.fldmukim + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("DateTime")%></td><td class='bold'>" + notification.flddatetime.replace("T", " ") + "</td></tr>";
                            tableHTML += "<tr><td colspan='2' align='center'><button class='btn blue btn-xs' id='btnAcknowledge' onclick=\"OpenPopupWindow('../admins/AlertInfo.aspx?id=" + notification.fldid + "&i=" + notification.fldmd5 + "',1280,800);return false;\"><%=GetText("Acknowledge")%></button></td></tr>"
                            tableHTML += "</table>";
                            //tableHTML += "<div align='center'><button class='btn default' id='btnAcknowledge'><%=GetText("Acknowledge")%></button></div>";
                            //tableHTML += "<button class='btn default' onclick='SetMapCenter(" + notification.fldRLat + "," + notification.fldRLong + ");return false;" > Show Location</button > "

                            var toastrclass;
                            if (notification.fldseverity == "low") {
                                toastrclass = "toast toast-low"
                            };
                            if (notification.fldseverity == "medium") {
                                toastrclass = "toast toast-medium"
                            };
                            if (notification.fldseverity == "high") {
                                toastrclass = "toast toast-high"
                            };
                            toastr.options = {
                                "toastClass": toastrclass,
                                "closeButton": false,
                                "progressBar": true,
                                "timeOut": "30000", // Show for 10 seconds
                                "extendedTimeOut": "30000",
                                "positionClass": "toast-top-right",
                                "newestOnTop": true,
                                "escapeHtml": false, // Allow HTML in the content
                                "tapToDismiss": false,
                            };

                            var $toast = toastr.warning(tableHTML, notification.fldmsg).attr('id', "toastr" + notification.fldid);
                            $('#toast-container').appendTo('#notification');

                            if ($toast && $toast.find('#btnAcknowledge').length) {
                                $toast.find('#btnAcknowledge').on('click', function () {
                                    event.preventDefault(); // Prevent any default action when clicking the button
                                    CloseToastr(notification.fldid);
                                });
                            }
                        });
                        playNotificationSound(1);
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Failed to fetch notifications: " + error);
                }
            });
        }

        var currentAudio = null;
        function playNotificationSound() {
            if (currentAudio) {
                currentAudio.pause();
                currentAudio.currentTime = 0;  // Reset the audio to the start
            }
            // Create a new audio object and play the new sound
            currentAudio = new Audio('../assets/alertsound4.mp3');
            currentAudio.play();
        }

        function CloseToastr(alertid) {
            if (document.getElementById('toastr' + alertid)) {
                $("#toastr" + alertid).fadeOut(300, function () {
                    $(this).remove();
                });
            }
        }

        function initNotifications() {
            fetchNotifications();
            setInterval(fetchNotifications, 5000); // 10 seconds
        }
    </script>

    <!--Dashboard-->
    <script type="text/javascript">
        function getdashboardata() {
            $.ajax({
                type: "POST",
                url: "../GetData.aspx/GetDashboardData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Parse the JSON response
                    var data = response.d;
                    document.getElementById("txtOnlineCount").innerHTML = data.login_user;
                    document.getElementById("txtActiveEMD").innerHTML = data.active_emd;
                    document.getElementById("txtInactiveEMD").innerHTML = data.inactive_emd;
                    document.getElementById("txtTotal_Alert").innerHTML = data.total_alert;
                    document.getElementById("txtJenayah_Alert").innerHTML = data.jenayah_alert;
                    document.getElementById("txtNarkotik_Alert").innerHTML = data.narkotik_alert;
                    document.getElementById("txtKomersil_Alert").innerHTML = data.komersil_alert;
                    document.getElementById("txtCawanganKhas_Alert").innerHTML = data.cawangankhas_alert;
                },
                error: function (xhr, status, error) {
                    console.error("Failed to fetch dashboard data: " + error);
                    alert("Failed to fetch dashboard data: " + error);
                }
            });
        }

        function initdashboarddata() {
            getdashboardata();
            setInterval(getdashboardata, 5000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- PAGE HEADER -->
    <div class="page-head" style="display: none;">
        <div class="container">
            <div class="page-title">
                <h1>
                    <asp:Label runat="server" CssClass="uppercase" ID="lblPageTitle" Text="Home"></asp:Label></h1>
            </div>
        </div>
    </div>
    <!-- PAGE HEADER -->
    <!-- PAGE BODY -->
    <div style="display: flex; flex-direction: row; height: 100%">
        <div style="width: 80%; height: 100%; padding: 10px; display: flex; flex-direction: column;">
            <div class="col-md-12" style="height: 70%; padding: 10px;">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div>
                            <a class="btn default" style="position: absolute; z-index: 100; top: 20px; left: 200px; padding: 9px; box-shadow: 0px 0px 3px #4a4a4a !important; -moz-box-shadow: 0px 0px 3px #4a4a4a !important; -webkit-box-shadow: 0px 0px 3px #4a4a4a !important;" onclick="SetMapCenter(4.2105,108.9758,6)"><%=GetText("Reset")%></a>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="map" style="width: 100%; height: 100%; padding: 10px;"></div>
            </div>
            <div class="row" style="height: 30%; padding: 10px;">
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtActiveEMD" OnClick="lbtActiveEMD_Click">
                        <div class="dashboard-stat custom-color" style="background-color: #1f1d59">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtActiveEMD" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblActiveEMD" ClientIDMode="Static" Text="EMD Aktif"><%=GetText("ActiveEMD")%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtInactiveEMD" OnClick="lbtInactiveEMD_Click">
                        <div class="dashboard-stat custom-color" style="background-color: #1f1d59">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtInactiveEMD" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblInactiveEMD" ClientIDMode="Static" Text="EMD Tidak Aktif"><%=GetText("InactiveEMD")%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtTotal_Alert" CommandArgument="total_alert" OnClick="lbtViolateTermsList_Click">
                        <div class="dashboard-stat custom-color" style="background-color: #1f1d59">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtTotal_Alert" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblTotal_Alert" ClientIDMode="Static" Text="OPP Langgar Syarat"><%=GetText("OPPViolateTerms")%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <div class="dashboard-stat custom-color" style="background-color: #1f1d59">
                        <div class="visual">
                        </div>
                        <div class="details">
                            <div class="number">
                                <asp:Label runat="server" ID="txtOnlineCount" ClientIDMode="Static" Text="0"></asp:Label>
                            </div>
                            <div class="desc">
                                <asp:Label runat="server" ID="lblOnlineCount" ClientIDMode="Static" Text="Operator Dlm.Talian"><%=GetText("OperatorOnline")%></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtJenayah_Alert" CommandArgument="jenayah_alert" OnClick="lbtViolateTermsList_Click">
                        <div class="dashboard-stat custom-color" runat="server" id="dvJenayah">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtJenayah_Alert" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblJenayah_Alert" ClientIDMode="Static" Text="Langgar Syarat (Jenayah)"><%=String.Format("{0} ({1})", GetText("ViolateTerms"), GetText("Jenayah"))%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtNarkotik_Alert" CommandArgument="narkotik_alert" OnClick="lbtViolateTermsList_Click">
                        <div class="dashboard-stat custom-color" runat="server" id="dvNarkotik">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtNarkotik_Alert" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblNarkotik_Alert" ClientIDMode="Static" Text="Langgar Syarat (Narkotik)"><%=String.Format("{0} ({1})", GetText("ViolateTerms"), GetText("Narkotik"))%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtKomersil_Alert" CommandArgument="komersil_alert" OnClick="lbtViolateTermsList_Click">
                        <div class="dashboard-stat custom-color" runat="server" id="dvKomersil">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtKomersil_Alert" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblKomersil_Alert" ClientIDMode="Static" Text="Langgar Syarat (Komersil)"><%=String.Format("{0} ({1})", GetText("ViolateTerms"), GetText("Komersil"))%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-6">
                    <asp:LinkButton runat="server" ID="lbtCawanganKhas_Alert" CommandArgument="cawangankhas_alert" OnClick="lbtViolateTermsList_Click">
                        <div class="dashboard-stat custom-color" runat="server" id="dvCawanganKhas">
                            <div class="visual">
                            </div>
                            <div class="details">
                                <div class="number">
                                    <asp:Label runat="server" ID="txtCawanganKhas_Alert" ClientIDMode="Static" Text="0"></asp:Label>
                                </div>
                                <div class="desc">
                                    <asp:Label runat="server" ID="lblCawanganKhas_Alert" ClientIDMode="Static" Text="Langgar Syarat (Cawangan Khas)"><%=String.Format("{0} ({1})", GetText("ViolateTerms"), GetText("Cawangan Khas"))%></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="notification" style="overflow-y: hidden; height: 100%; width: 20%; padding: 10px;">
        </div>
    </div>
    <!-- PAGE BODY -->

    <!-- Active EMD By Department -->
    <div class="modal fade" id="dvActiveEMD" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%=GetText("ActiveEMD")%></h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rptActiveEMD" OnItemCommand="rptActiveEMD_ItemCommand" OnItemCreated="rptActiveEMD_ItemCreated">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th><%#GetText("Department")%></th>
                                                <th><%#GetText("Unit")%></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("fldDepartment")%></td>
                                        <td style="text-align: center;"><%#Eval("fldCount")%></td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton runat="server" CssClass="btn blue btn-xs" ID="lbtEMDList" CommandName="showlist" CommandArgument='<%#Eval("fldID")%>'><%#GetText("EMDList")%></asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtActiveEMD" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" CssClass="btn default" OnClientClick="$('#dvActiveEMD').modal('hide');return false;"><%=GetText("Close")%></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <!-- Active EMD List -->
    <div class="modal fade" id="dvActiveEMDList" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="lblActiveEMDListTitle"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: scroll;max-height: 70vh;">
                            <asp:Repeater runat="server" ID="rptActiveEMDList">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th><%#GetText("Marking")%></th>
                                                <th><%#GetText("IMEI")%></th>
                                                <th><%#GetText("OPP")%></th>
                                                <th><%#GetText("Show")%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("fldName")%></td>
                                        <td><%#Eval("fldImei")%></td>
                                        <td><%#Eval("fldOPPName") & " - " & Eval("fldOPPICNo")%></td>
                                        <td style="text-align: center;">
                                            <a class="btn blue btn-xs" onclick='SetMapCenter(<%#Eval("fldRLat")%>,<%#Eval("fldRLong")%>,15);$("#dvActiveEMDList").modal("hide");$("#dvActiveEMD").modal("hide");'><%#GetText("Show")%></a></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" CssClass="btn default" OnClientClick="$('#dvActiveEMDList').modal('hide');return false;"><%=GetText("Close")%></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Inactive EMD List -->
    <div class="modal fade" id="dvInactiveEMDList" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title"><%=GetText("InactiveEMD")%></h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;max-height: 70vh;">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Repeater runat="server" ID="rptInactiveEMDList">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th><%#GetText("Marking")%></th>
                                                <th><%#GetText("IMEI")%></th>
                                                <th><%#GetText("OPP")%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("fldName")%></td>
                                        <td><%#Eval("fldImei")%></td>
                                        <td><%#Eval("fldOPPName") & " - " & Eval("fldOPPICNo")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtInactiveEMD" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" CssClass="btn default" OnClientClick="$('#dvInactiveEMDList').modal('hide');return false;"><%=GetText("Close")%></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <!-- Total Alert List -->
    <div class="modal fade" id="dvViolateTermsList" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title">
                                <asp:Label runat="server" ID="lblViolateTermsTitle"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Repeater runat="server" ID="rptTotal_Alert">
                                <HeaderTemplate>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th><%#GetText("ViolateTerms")%></th>
                                                <th><%#GetText("Total")%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" OnClientClick="return false;" CssClass='<%#If(Eval("fldSeverity").Equals("high"), "btn red btn-xs", If(Eval("fldSeverity").Equals("medium"), "btn yellow-gold btn-xs", "btn yellow-crusta btn-xs"))%>' Text='<%#Eval("fldMsg")%>' />
                                        </td>
                                        <td style="text-align: center;"><%#Eval("fldCount")%></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                            </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" CssClass="btn default" data-dismiss="modal" aria-label="Close"><%=GetText("Close")%></asp:LinkButton>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbtTotal_Alert" />
                        <asp:AsyncPostBackTrigger ControlID="lbtJenayah_Alert" />
                        <asp:AsyncPostBackTrigger ControlID="lbtNarkotik_Alert" />
                        <asp:AsyncPostBackTrigger ControlID="lbtKomersil_Alert" />
                        <asp:AsyncPostBackTrigger ControlID="lbtCawanganKhas_Alert" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
