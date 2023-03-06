var start = [];
var destination = [];
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
    start = geocode(document.forms["modulo"]["start"].value);
    destination = geocode(document.forms["modulo"]["destination"].value);
}

function geocode($address) {

    $url = "http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q=" + $address +"&format=json&limit=1";

    $resp_json = funcName($url);

    $resp = JSON.parse($resp_json);

    return array($resp.lat, $resp.lon);
}

async function funcName(url) {
    const response = await fetch(url);
    var data = await response.json();
    return data;
}

