
var marker1;
var marker2;
var map = L.map('map').setView([41.29, 12.27], 5);



L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

function onMapClick(e) {
    /*alert("You clicked the map at " + e.latlng);*/
    var marker = L.marker(e.latlng).addTo(map)
    marker.bindPopup("<b>" + e.location + "</b><br>I am a popup.").openPopup();
    markers.push(marker);
}

map.on('click', onMapClick);

function search() {
    var start = document.forms["modulo"]["start"].value;
    var destination = document.forms["modulo"]["destination"].value;
    address1 = jQuery(start).val();
    address2 = jQuery(destination).val();

}

// 1. Initialize GeoCoder
const geocoder = new google.maps.Geocoder();

// 2. The text address that you want to convert to coordinates
let address = "FANO";

// 3. Obtain coordinates from the API
geocoder.geocode({ address: address }, (results, status) => {
    if (status === "OK") {
        // Display response in the console
        console.log(results);
    } else {
        alert("Geocode error: " + status);
    }
});