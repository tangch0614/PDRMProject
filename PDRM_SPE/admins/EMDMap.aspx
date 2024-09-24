<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admins/Admin.Master" CodeBehind="EMDMap.aspx.vb" Inherits="PDRM_SPE.AEMDMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #map {
            position: fixed !important;
        }
    </style>
    <script src="https://unpkg.com/@googlemaps/markerclusterer/dist/index.min.js"></script>
    <script src="../assets/js/gmapapi.js"></script>
    <script type="text/javascript">

        async function initMap(locations) {
            // Request needed libraries.
            const { Map, InfoWindow } = await google.maps.importLibrary("maps");
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
            const map = new Map(document.getElementById("map"), {
                center: { lat: 4.2105, lng: 108.9758 },
                zoom: 6,
                mapId: "4504f8b37365c3d0",
            });
            // Create an info window to share between markers.

            const infoWindow = new google.maps.InfoWindow();

            const markers = locations.map((position, i) => {
                var content = "Name: " + property.name + '</br>' + "ID(IMEI): " + property.imei + '</br>' + "DateTime: " + property.datetime + '</br>' + "Lat: " + property.lat + '</br>' + "Long: " + property.long + '</br>' + "GSM: " + property.gsm + '</br>' + "GPS Sat: " + property.gps + '</br>' + "Bds Sat: " + property.bds + '</br>' + "Battery: " + property.battery + '</br>' + "Belt Status: " + property.beltstatus + '</br>' + "Alarm: " + property.alarm + '</br>' + "Speed: " + property.speed

                // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
                const pinelement = new PinElement({
                    background: property.pincolor,
                    glyphColor: property.pinglyphcolor,
                    glyph: property.id
                });
                const marker = new google.maps.marker.AdvancedMarkerElement({
                    position: { lat: property.lat, lng: property.long },
                    content: pinelement.element,
                });

                // markers can only be keyboard focusable when they have click listeners
                // open info window when marker is clicked
                marker.addListener("click", () => {
                    infoWindow.setContent(content);
                    infoWindow.open(map, marker);
                });
                return marker;
            });

            // Add a marker clusterer to manage the markers.
            new markerClusterer.MarkerClusterer({ map, markers });

            //for (i = 0; i < locations.length; i++) {
            //    var loan = locations[i][0]
            //    var lat = locations[i][1]
            //    var long = locations[i][2]
            //    var add = locations[i][3]
            //    var datetime = locations[i][4]
            //    var locStatus = locations[i][5]
            //    var DataStatus = locations[i][6]
            //    var GPS = locations[i][7]
            //    var bds = locations[i][8]
            //    var battery = locations[i][9]
            //    var beltStatus = locations[i][10]
            //    var alarm = locations[i][11]
            //    var speed = locations[i][12]
            //    var pincolor = locations[i][13]
            //    var pinglyphcolor = locations[i][14]
            //    var content = "<h2> " + loan + '</h2></br>' + "Name: " + loan + '</br>' + "ID(IMEI): " + add + '</br>' + "DateTime: " + datetime + '</br>' + "Loc Status: " + locStatus + '</br>' + "Data Status: " + DataStatus + '</br>' + "GSM: " + '0' + '</br>' + "GPS Sat: " + GPS + '</br>' + "Bds Sat: " + bds + '</br>' + "Battery: " + battery + '</br>' + "Belt Status: " + beltStatus + '</br>' + "Alarm: " + alarm + '</br>' + "Speed: " + speed

            //    // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
            //    const pinelement = new PinElement({
            //        background: pincolor,
            //        glyphColor: pinglyphcolor,
            //    });
            //    const marker = new AdvancedMarkerElement({
            //        map,
            //        position: { lat: lat, lng: long },
            //        content: pinelement.element
            //    });

            //    // Add a click listener for each marker, and set up the info window.
            //    marker.addListener("click", ({ domEvent, latLng }) => {
            //        const { target } = domEvent;

            //        infoWindow.close();
            //        infoWindow.setContent(content);
            //        infoWindow.open(marker.map, marker);
            //    });
            //};

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
    <div class="page-content">
        <div class="container">
            <div class="page-content-inner">
                <div class="row">
                    <div id="map" style="width: 100%; height: 100%;"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- PAGE BODY -->

    <script>
        function pageLoad(sender, args) { /*execute on page load*/
            //initTable("table1");
            //initDatePicker("#txtDateFrom", "#hfDateFrom", "#txtDateTo", "#hfDateTo");
        }
    </script>
</asp:Content>
