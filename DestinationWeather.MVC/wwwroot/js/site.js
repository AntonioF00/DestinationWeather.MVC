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

    //add marker to map 
    var marker1 = L.marker1([start.lat,start.lon]).addTo(map)
    marker1.bindPopup("<b></b><br>I am a popup.").openPopup();

    var marker2 = L.marker2([destination.lat, destination.lon]).addTo(map)
    marker2.bindPopup("<b></b><br>I am a popup.").openPopup();
}

async function geocode($address) {

    var res;

    const apiUrl = "http://nominatim.openstreetmap.org/?format=json&addressdetails=1&q=" + $address +"&format=json&limit=1";

    const productValue = new Headers();
    productValue.append("User-Agent", "ScraperBot/1.0");

    const commentValue = new Headers();
    commentValue.append("User-Agent", "(+http://www.API.com/ScraperBot.html)");

    const options = {
        method: "GET",
        headers: new Headers(),
    };

    options.headers.append("User-Agent", productValue);
    options.headers.append("User-Agent", commentValue);

    fetch(apiUrl, options)
        .then((response) => response.json())
        .then((data) => console.log(data))
        .catch((error) => console.error(error));

    return array(res.lat, res.lon);
}


