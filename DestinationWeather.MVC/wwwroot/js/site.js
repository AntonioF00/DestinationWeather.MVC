var marker1;
var marker2;
var map = L.map('map').setView([41.29, 12.27], 5);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

function onMapClick(e) {
    var marker = L.marker(e.latlng).addTo(map)
    marker.bindPopup("<b>" + e.location + "</b><br>I am a popup.").openPopup();
    markers.push(marker);
}

map.on('click', onMapClick);


function search() {

    //var start = document.forms["modulo"]["start"].value;
    //var destination = document.forms["modulo"]["destination"].value;

    var marker1 = L.marker1([data.StartData.lat, data.StartData.lon]).addTo(map)
    marker1.bindPopup("<b></b><br>I am a popup.").openPopup();

    var marker2 = L.marker2([data.DestinationData.lat, data.DestinationData.lon]).addTo(map)
    marker2.bindPopup("<b></b><br>I am a popup.").openPopup();
}


