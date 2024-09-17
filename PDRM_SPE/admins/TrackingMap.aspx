<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TrackingMap.aspx.vb" Inherits="PDRM_SPE.ATrackingMap" %>

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
            background-color: #d91e18; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-medium {
            background-color: #e87e04; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-low {
            background-color: #f3c200; /* Custom background color */
            color: #000 !important; /* Custom text color */
        }

        .toast-title {
            text-transform: uppercase;
            padding-left: 30px;
            margin-bottom: 5px;
        }

        .toast-message table td {
            padding: 5px 5px !important;
        }

        #toast-container {
            max-height: 100vh; /* Set the max height for the container */
            overflow-y: auto; /* Enable vertical scrolling */
        }

            #toast-container > .toast {
                margin-bottom: 10px; /* Add some space between toasts */
            }

            #toast-container > div {
                margin: 0 0 6px;
                padding: 10px;
                width: 100%;
                background-position: 10px 7px;
                opacity: 1;
            }

        .opp-info td {
            padding: 5px 5px !important;
        }

        .dataTable tr {
            vertical-align: top;
        }
    </style>
    <script>(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })
            ({ key: "AIzaSyA9AQTXBVGEnr8xB2k3chP1Ek5Yxk6gePU", v: "weekly" });</script>

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
                        // Parse the JSON response
                        document.getElementById("txtIMEI").innerHTML = data.imei;
                        document.getElementById("imgGSMStatus").src = '../assets/img/' + data.gsm;
                        document.getElementById("txtGPSStatus").innerHTML = data.locstatus;
                        document.getElementById("txtBatteryStatus").innerHTML = data.battery;
                        document.getElementById("txtBeltStatus").innerHTML = data.beltstatus;
                        document.getElementById("txtSpeed").innerHTML = data.speed;
                    } else {
                        document.getElementById("txtIMEI").innerHTML = "";
                        document.getElementById("imgGSMStatus").innerHTML = "";
                        document.getElementById("txtGPSStatus").innerHTML = "";
                        document.getElementById("txtBatteryStatus").innerHTML = "";
                        document.getElementById("txtBeltStatus").innerHTML = "";
                        document.getElementById("txtSpeed").innerHTML = "";
                    }
                } catch (error) {
                    alert('Error fetching EMD data.');
                    console.error('Error fetching EMD data:', error);
                }
            } else {
                document.getElementById("txtIMEI").innerHTML = "";
                document.getElementById("imgGSMStatus").innerHTML = "";
                document.getElementById("txtGPSStatus").innerHTML = "";
                document.getElementById("txtBatteryStatus").innerHTML = "";
                document.getElementById("txtBeltStatus").innerHTML = "";
                document.getElementById("txtSpeed").innerHTML = "";
                clearMarkers();
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

    <!--NOTIFICATION-->
    <script type="text/javascript">
        var priorityMap = {
            'high': 3,
            'medium': 2,
            'low': 1
        };

        function fetchNotifications() {
            //var deviceid = document.getElementById("ddlEMD").value;
            var oppid = document.getElementById("hfOPPID").value;
            var userid = <%=UserID%>;
            if (oppid > 0) {
                const param = {
                    deviceid: -1,
                    oppid: oppid,
                    userid: userid,
                    page: "trackingmap"
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
                                var tableHTML = '<table style="width: 100%;" class="dataTable table-bordered table-striped">';
                                tableHTML += '<tr><td><%=GetText("DateTime")%></td><td class="bold">' + notification.flddatetime.replace('T', ' ') + '</td></tr>';
                                tableHTML += '</table>';
                                tableHTML += '<div align="center"><button class="btn default" id="btnAcknowledge">Acknowledge</button></div>';

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
                                    "positionClass": "toast-bottom-right",
                                    "newestOnTop": true,
                                    "escapeHtml": false, // Allow HTML in the content
                                    "tapToDismiss": false
                                };

                                var $toast = toastr.warning(tableHTML, notification.fldmsg).attr('id', "toastr" + notification.fldid);
                                $('#toast-container').appendTo('#notification');

                                if ($toast && $toast.find('#btnAcknowledge').length) {
                                    $toast.find('#btnAcknowledge').on('click', function () {
                                        event.preventDefault(); // Prevent any default action when clicking the button
                                        getModalData(notification.fldid);
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
        }

        function getModalData(alertid) {
            document.getElementById('hfAlertID').value = 0;
            $.ajax({
                type: "POST",
                url: "../GetData.aspx/GetNotificationDetail", // Replace with the actual server-side URL
                data: JSON.stringify({ alertid: alertid }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var alerts = JSON.parse(response.d);
                    if (alerts.length > 0) {
                        var alert = alerts[0];
                        if (alert && Object.keys(alert).length > 0) {
                            if (alert.fldPhoto1 && alert.fldPhoto1.trim() !== "") {
                                document.getElementById('imgPPhoto1Preview').src = alert.fldphoto1;
                            } else {
                                document.getElementById('imgPPhoto1Preview').src = "../assets/img/No_Image.png";
                            }
                            document.getElementById('hfAlertID').value = alert.fldid;
                            document.getElementById('txtPImei').innerText = alert.fldimei;
                            document.getElementById('txtPDateTime').innerText = alert.flddatetime.replace("T", " ");
                            document.getElementById('txtPViolateTerms').innerText = alert.fldmsg.toUpperCase();
                            document.getElementById('txtPSubjectName').innerText = alert.fldoppname;
                            document.getElementById('txtPSubjectICNo').innerText = alert.fldoppicno;
                            document.getElementById('txtPSubjectContactNo').innerText = alert.fldoppcontactno;
                            document.getElementById('txtPPoliceStation').innerText = alert.fldpsname;
                            document.getElementById('txtPPSContactNo').innerText = alert.fldpscontactno;
                            document.getElementById('txtPDepartment').innerText = alert.flddepartment;
                            document.getElementById('txtPOverseer').innerText = alert.fldoverseername;
                            document.getElementById('txtPOverseerIDNo').innerText = alert.fldoverseerpoliceno;
                            document.getElementById('txtPOverseerContactNo').innerText = alert.fldoverseercontactno;

                            if (alert.fldprocess == 1) {
                                document.getElementById('btnPAcknowledge').style.display = "none"; // Hide the button
                                document.getElementById('txtPRemark').value = alert.fldremark;
                                document.getElementById('txtPAcknowledgeByID').innerText = alert.fldprocessbyname;
                                document.getElementById('txtPAcknowledgeDateTime').innerText = alert.fldprocessdatetime.replace("T", " ");
                            } else {
                                document.getElementById('btnPAcknowledge').style.display = "inline-block"; // Show the button
                                document.getElementById('txtPRemark').value = "";
                                document.getElementById('txtPAcknowledgeByID').innerText = "";
                                document.getElementById('txtPAcknowledgeDateTime').innerText = "";
                            }
                            $('#plAcknowledge').modal('show');
                        } else {
                            $('#plAcknowledge').modal('hide');
                        }
                    } else {
                        $('#plAcknowledge').modal('hide');
                    }
                },
                error: function (error) {
                    console.error("Error: ", error);
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

        function cleartoastr() {
            toastr.clear();
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
        function pageLoad(sender, args) { /*execute on page load*/
            $('#ddlEMD').select2();
            $('#ddlOPP').select2();
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
                                                    <td colspan="2" align="right">
                                                        <asp:Button runat="server" CssClass="btn blue" ID="btnSearch" Text='Search' OnClick="btnSearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="plOPPInfo" CssClass="portlet light bordered">
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
                                                    <td><%=GetText("PoliceStation").Replace("vITEM", GetText("ContactNum")) & " 1"%></td>
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
                                                <tr>
                                                    <td><%=GetText("GSM")%></td>
                                                    <td class="bold">
                                                        <asp:Image runat="server" ID="imgGSMStatus" ClientIDMode="Static"></asp:Image>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("GPS")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtGPSStatus" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Battery")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtBatteryStatus" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("BeltStatus")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtBeltStatus" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td><%=GetText("Speed")%></td>
                                                    <td class="bold">
                                                        <asp:Label runat="server" ID="txtSpeed" ClientIDMode="Static"></asp:Label>
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
        <asp:HiddenField runat="server" ID="hfAlertID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfConfirm" ClientIDMode="Static" />

        <asp:Panel runat="server" ClientIDMode="Static" ID="plAcknowledge" CssClass="modal fade" TabIndex="-1" data-backdrop="static" data-keyboard="false">
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
        </asp:Panel>


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
