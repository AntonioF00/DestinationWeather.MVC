var markers[];
var map = L.map('map').setView([41.29, 12.27], 5);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);

/*var marker = L.marker([41.29, 12.27]).addTo(map);*/

/*marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();*/

//var popup = L.popup()
//    .setLatLng([51.513, -0.09])
//    .setContent("I am a standalone popup.")
//    .openOn(map);



function onMapClick(e) {
    alert("You clicked the map at " + e.latlng);
    var marker = L.marker(e.latlng).addTo(map)
    marker.bindPopup("<b>" + e.location + "</b><br>I am a popup.").openPopup();
    markers += marker;
}

map.on('click', onMapClick);

function search() {
    var start = document.forms["modulo"]["start"].value;
    var destination = document.forms["modulo"]["destination"].value;
    $.get(location.protocol + '//nominatim.openstreetmap.org/search?format=json&q=' + start, function (data) {
        console.log(data);
        var marker = L.marker(data.latlng).addTo(map)
        marker.bindPopup("<b>"+start+"</b><br>").openPopup();
    });
    $.get(location.protocol + '//nominatim.openstreetmap.org/search?format=json&q=' + destination, function (data) {
        var marker = L.marker(data.latlng).addTo(map)
        marker.bindPopup("<b>" + destination + "</b><br>").openPopup();
    });
}