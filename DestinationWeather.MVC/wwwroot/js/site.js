var res;
var start;
var destination;
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

async function search() {
    start = document.forms["modulo"]["start"].value;
    destination = document.forms["modulo"]["destination"].value;

    window.sayHello2 = (res, start, destination) => {
        return DotNet.invokeMethodAsync('DestinationWeather.MVC', 'Search', start, destination);
    };

    //add marker to map 
    var marker1 = L.marker1([res.StartDatas.lat, res.StartDatas.lon]).addTo(map)
    marker1.bindPopup("<b></b><br>I am a popup.").openPopup();

    var marker2 = L.marker2([res.DestinationDatas.lat, res.DestinationDatas.lon]).addTo(map)
    marker2.bindPopup("<b></b><br>I am a popup.").openPopup();
}



