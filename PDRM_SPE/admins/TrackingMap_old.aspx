<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TrackingMap_old.aspx.vb" Inherits="PDRM_SPE.ATrackingMap_old" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #map{
            position:unset !important;
        }
        </style>
    <script
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA9AQTXBVGEnr8xB2k3chP1Ek5Yxk6gePU&loading=async&libraries=marker&v=beta&solution_channel=GMP_CCS_complexmarkers_v3"
        defer></script>

</head>
<body onload="initialize()">
    <form id="form1" runat="server">
    <div id="map" style="width: 100%; height: 100%; position:fixed;"></div>
    </form>

</body>

<script type="text/javascript">

    var locations = [
        ['867255079755544', 3.11161, 101.5785, '867255079755544', '2024-07-08 11:54:10', 'GPS', 'Real-Time', '11', '3', '7%(Charging)', 'Belt Off', 'Offline', '0(km/h)'],
        ['867255079747483', 2.91179, 101.73555, '867255079747483', '2024-07-08 11:54:10', 'GPS', 'Real-Time', '11', '12', '0%', 'Belt Off', 'Offline', '0(km/h)'],
        ['867255079776666', 3.134922, 101.713302, '867255079776666', '2024-08-07 11:54:10', 'GPS', 'Real-Time', '11', '3', '0%', 'Belt Off', 'Offline', '0(km/h)']
    ];

    function initialize() {
        var myOptions = {
            center: new google.maps.LatLng(4.2105, 108.9758),
            zoom: 7,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            mapId: "DEMO_MAP_ID"
        };
        var map = new google.maps.Map(document.getElementById("map"),
            myOptions);
        setMarkers(map, locations)
    }

    function setMarkers(map, locations) {
        var marker, i
        for (i = 0; i < locations.length; i++) {
            var loan = locations[i][0]
            var lat = locations[i][1]
            var long = locations[i][2]
            var add = locations[i][3]
            var datetime = locations[i][4]
            var locStatus = locations[i][5]
            var DataStatus = locations[i][6]
            var GPS = locations[i][7]
            var bds = locations[i][8]
            var battery = locations[i][9]
            var beltStatus = locations[i][10]
            var alarm = locations[i][11]
            var speed = locations[i][12]

            latlngset = new google.maps.LatLng(lat, long);
            var marker = new google.maps.marker.AdvancedMarkerElement({
                map: map, title: loan, position: latlngset
            });
            //map.setCenter(marker.getPosition())
            var content = "<h2> " + loan + '</h2></br>' + "Name: " + loan + '</br>' + "ID(IMEI): " + add + '</br>' + "DateTime: " + datetime + '</br>' + "Loc Status: " + locStatus + '</br>' + "Data Status: " + DataStatus + '</br>' + "GSM: " + '0' + '</br>' + "GPS Sat: " + GPS + '</br>' + "Bds Sat: " + bds + '</br>' + "Battery: " + battery + '</br>' + "Belt Status: " + beltStatus + '</br>' + "Alarm: " + alarm + '</br>' + "Speed: " + speed
            var infowindow = new google.maps.InfoWindow()
            google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                return function () {
                    infowindow.setContent(content);
                    infowindow.open(map, marker);
                };
            })(marker, content, infowindow));
        }
    }
</script>
</html>
