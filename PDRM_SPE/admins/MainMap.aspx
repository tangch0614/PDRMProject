<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MainMap.aspx.vb" Inherits="PDRM_SPE.AMainMap" %>

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
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-toastr/toastr.min.css" rel="stylesheet" type="text/css" />
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

        .dataTable tr {
            vertical-align: top;
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
                    deviceid: document.getElementById("ddlEMD").value,
                    imei: "",
                    simno: "",
                    status: "Y"
                };
                const response = await fetch('../GetData.aspx/GetMarkers', {
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
                page: "mainmap"
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
                            tableHTML += "<tr><td><%=GetText("PoliceStation")%></td><td class='bold'>" + notification.fldpsname + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("Department")%></td><td class='bold'>" + notification.fldDepartment + "</td></tr>";
                            tableHTML += "<tr><td><%=GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))%></td><td class='bold'>" + notification.fldpscontactno + "</td></tr>";
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
                                    //getModalData(notification.fldid);
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

        //function getModalData(alertid) {
        //    document.getElementById('hfAlertID').value = 0;
        //    $.ajax({
        //        type: "POST",
        //        url: "../GetData.aspx/GetNotificationDetail", // Replace with the actual server-side URL
        //        data: JSON.stringify({ alertid: alertid }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            var alerts = JSON.parse(response.d);
        //            if (alerts.length > 0) {
        //                var alert = alerts[0];
        //                if (alert && Object.keys(alert).length > 0) {
        //                    if (alert.fldPhoto1 && alert.fldPhoto1.trim() !== "") {
        //                        document.getElementById('imgPPhoto1Preview').src = alert.fldphoto1;
        //                    } else {
        //                        document.getElementById('imgPPhoto1Preview').src = "../assets/img/No_Image.png";
        //                    }
        //                    document.getElementById('hfAlertID').value = alert.fldid;
        //                    document.getElementById('txtPImei').innerText = alert.fldimei;
        //                    document.getElementById('txtPDateTime').innerText = alert.flddatetime.replace("T", " ");
        //                    document.getElementById('txtPViolateTerms').innerText = alert.fldmsg.toUpperCase();
        //                    document.getElementById('txtPSubjectName').innerText = alert.fldoppname;
        //                    document.getElementById('txtPSubjectICNo').innerText = alert.fldoppicno;
        //                    document.getElementById('txtPSubjectContactNo').innerText = alert.fldoppcontactno;
        //                    document.getElementById('txtPPoliceStation').innerText = alert.fldpsname;
        //                    document.getElementById('txtPPSContactNo').innerText = alert.fldpscontactno;
        //                    document.getElementById('txtPDepartment').innerText = alert.flddepartment;
        //                    document.getElementById('txtPOverseer').innerText = alert.fldoverseername;
        //                    document.getElementById('txtPOverseerIDNo').innerText = alert.fldoverseerpoliceno;
        //                    document.getElementById('txtPOverseerContactNo').innerText = alert.fldoverseercontactno;

        //                    if (alert.fldprocess == 1) {
        //                        document.getElementById('btnPAcknowledge').style.display = "none"; // Hide the button
        //                        document.getElementById('txtPRemark').value = alert.fldremark;
        //                        document.getElementById('txtPAcknowledgeByID').innerText = alert.fldprocessbyname;
        //                        document.getElementById('txtPAcknowledgeDateTime').innerText = alert.fldprocessdatetime.replace("T", " ");
        //                    } else {
        //                        document.getElementById('btnPAcknowledge').style.display = "inline-block"; // Show the button
        //                        document.getElementById('txtPRemark').value = "";
        //                        document.getElementById('txtPAcknowledgeByID').innerText = "";
        //                        document.getElementById('txtPAcknowledgeDateTime').innerText = "";
        //                    }
        //                    $('#plAcknowledge').modal('show');
        //                } else {
        //                    $('#plAcknowledge').modal('hide');
        //                }
        //            } else {
        //                $('#plAcknowledge').modal('hide');
        //            }
        //        },
        //        error: function (error) {
        //            console.error("Error: ", error);
        //        }
        //    });
        //}

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
</head>
<body style="background-color: #f1f1f1 !important;">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="map-container">
            <div style="display: flex; flex-direction: row; height: 100%">
                <div style="width: 80%; height: 100%; padding: 10px; display: flex; flex-direction: column;">
                    <div class="col-md-12" style="height: 70%; padding: 10px;">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:DropDownList runat="server" ID="ddlEMD" Style="display: none; position: absolute; z-index: 100; top: 20px; left: 200px; padding: 10px;" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlEMD_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div id="map" style="width: 100%; height: 100%; padding: 10px;"></div>
                    </div>
                    <div class="row" style="height: 30%; padding: 10px;">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
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
                        </div>
                    </div>
                </div>
                <div id="notification" style="overflow-y: hidden; height: 100%; width: 20%; padding: 10px;">
                </div>
            </div>
        </div>

        <asp:HiddenField runat="server" ID="hfAlertID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfConfirm" ClientIDMode="Static" />

        <!-- The Modal -->
        <div class="modal fade" id="initmodal" tabindex="-1" role="dialog" aria-labelledby="confirmationModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Peta telah berjaya dimuatkan.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="confirmBtn" onclick="$('#initmodal').modal('hide');">OK</button>
                    </div>
                </div>
            </div>
        </div>

        <%--<asp:Panel runat="server" ClientIDMode="Static" ID="plAcknowledge" CssClass="modal fade" TabIndex="-1" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="portlet light bordered" style="width: 100%; margin: 0 auto">
                        <div class="portlet-title">
                            <button type="button" class="close" aria-hidden="true" onclick="$('#plAcknowledge').modal('hide');"></button>
                            <div class="caption">
                                <i class="fa fa-check fa-fw"></i>
                                <label class="caption-subject bold uppercase"><%=GetText("ViolateTerms")%></label>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group margin-bottom-5">
                                                        <div style="text-align: center">
                                                            <asp:Image runat="server" ID="imgPPhoto1Preview" ClientIDMode="Static" Style="height: 200px;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <table class="dataTable table-bordered table-striped">
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ViolateTerms")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPViolateTerms" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("DateTime")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPDateTime" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("IMEI")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPImei" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Name")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPSubjectName"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ICNum")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPSubjectICNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("ContactNum")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPSubjectContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceStation")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPPoliceStation" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Department")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPDepartment"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceStationItem").Replace("vITEM", GetText("ContactNum"))%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPPSContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Overseer")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPOverseer" ClientIDMode="Static"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("PoliceIDNo")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPOverseerIDNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("OfficerItem").Replace("vITEM", GetText("ContactNum"))%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPOverseerContactNo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-sm-6">
                                                    <table class="dataTable table-bordered table-striped">
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("Remark")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtPRemark" TextMode="MultiLine" Rows="15" Style="width: 100%"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("AcknowledgeBy")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPAcknowledgeByID"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label><%=GetText("AcknowledgeDateTime")%></label>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="txtPAcknowledgeDateTime"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="form-actions">
                                                <div class="pull-right">
                                                    <asp:Button runat="server" CssClass="btn blue " ID="btnPAcknowledge" Text="Maklum Terima" OnClientClick='return confirm(hfConfirm.value);' OnClick="btnPAcknowledge_Click" ClientIDMode="static" />
                                                    <asp:Button runat="server" CssClass="btn default " ID="btnPCancel" Text="Tutup" OnClick="btnPCancel_Click" ClientIDMode="static" />
                                                </div>
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>--%>

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
