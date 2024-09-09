import { MarkerClusterer } from "@googlemaps/markerclusterer";

async function initMap(locations) {
    // Request needed libraries.
    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary("marker");
    const map = new Map(document.getElementById("map"), {
        center: { lat: 4.2105, lng: 108.9758 },
        zoom: 7,
        mapId: "4504f8b37365c3d0",
    });
    // Create an info window to share between markers.

    const infoWindow = new google.maps.InfoWindow({
        disableAutoPan: true,
    });

    const markers = locations.map((position, i) => {
        var content = "Name: " + position.imei + '</br>' + "ID(IMEI): " + position.imei + '</br>' + "DateTime: " + position.datetime + '</br>' + "Loc Status: " + position.locstatus + '</br>' + "Data Status: " + position.datastatus + '</br>' + "GSM: " + '0' + '</br>' + "GPS Sat: " + position.gps + '</br>' + "Bds Sat: " + position.bds + '</br>' + "Battery: " + position.battery + '</br>' + "Belt Status: " + position.beltstatus + '</br>' + "Alarm: " + position.alarm + '</br>' + "Speed: " + position.speed

        // Each PinElement is paired with a MarkerView to demonstrate setting each parameter.
        const pinelement = new PinElement({
            background: position.pincolor,
            glyphColor: position.pinglyphcolor,
        });
        const marker = new google.maps.marker.AdvancedMarkerElement({
            position: { lat: position.lat, lng: position.long },
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
    new MarkerClusterer({ map, markers });
}