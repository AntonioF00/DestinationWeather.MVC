
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

    marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();

}

map.on('click', onMapClick);

//function search() {
//    var start = document.modulo.start.value;
//    var destination = document.modulo.destination.value;


//}