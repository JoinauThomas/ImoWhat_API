

// LES VARIABLES


var latit = $('#latitude').val();
var longit = $('#longitude').val();
var home = { lat: Number(latit), lng: Number(longit) };
var mapPoiCommune;
var infowindow;
var hopitalTab = [];
var parkTab = [];
var ecoleTab = [];
var barTab = [];
var restoTab = [];
var stationTab = [];

var iconSchool = 'https://maps.gstatic.com/mapfiles/place_api/icons/school-71.png';
var iconBar = 'https://maps.gstatic.com/mapfiles/place_api/icons/bar-71.png ';
var iconRestaurant = 'https://maps.gstatic.com/mapfiles/place_api/icons/restaurant-71.png ';
var iconStation = 'https://maps.gstatic.com/mapfiles/place_api/icons/bus-71.png';
var iconPark = 'https://maps.gstatic.com/mapfiles/place_api/icons/generic_recreational-71.png ';
var iconHospital = 'https://maps.gstatic.com/mapfiles/place_api/icons/doctor-71.png';


//CREATION DE LA CARTE

function initMap() {


    mapPoiCommune = new google.maps.Map(document.getElementById('mapPoiCommune'), {
        center: home,
        zoom: 14
    });

    infowindow = new google.maps.InfoWindow();
    var service = new google.maps.places.PlacesService(mapPoiCommune);

    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['bar']
    }, callbackBar);

    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['school']
    }, callbackSchool);
    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['park']
    }, callbackPark);
    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['hospital']
    }, callbackHospital);
    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['restaurant']
    }, callbackResto);
    service.nearbySearch({
        location: home,
        radius: 2000,
        type: ['bus_station']
    }, callbackStation);
}


// PARTIE QUI GERE LES CHECKBOX


$('#checkboxHopital').click(function () {
    var i = 0;
    if (document.getElementById("checkboxHopital").checked === true) {
        for (i; i < hopitalTab.length; i++) {
            var marker = hopitalTab[i];
            marker.setVisible(true);
        }
    }
    else {
        for (i; i < hopitalTab.length; i++) {
            var marker = hopitalTab[i];
            marker.setVisible(false);
        }
    }
});

$('#checkboxBar').click(function () {
    if (document.getElementById("checkboxBar").checked === true) {
        for (var i = 0; i < barTab.length; i++) {
            var marker = barTab[i];
            marker.setVisible(true);
        }
    }

    else {
        for (var i = 0; i < barTab.length; i++) {
            var marker = barTab[i];
            marker.setVisible(false);
        }
    }
});

$('#checkboxEcoles').click(function () {
    if (document.getElementById("checkboxEcoles").checked === true) {

        for (var i = 0; i < ecoleTab.length; i++) {
            var marker = ecoleTab[i];
            marker.setVisible(true);
        }

    }
    else {
        for (var i = 0; i < ecoleTab.length; i++) {
            var marker = ecoleTab[i];
            marker.setVisible(false);
        }

    }
});

$('#checkboxRestaurent').click(function () {
    if (document.getElementById("checkboxRestaurent").checked === true) {
        for (var i = 0; i < restoTab.length; i++) {
            var marker = restoTab[i];
            marker.setVisible(true);
        }
    }
    else {
        for (var i = 0; i < restoTab.length; i++) {
            var marker = restoTab[i];
            marker.setVisible(false);
        }
    }
});

$('#checkboxStation').click(function () {
    var i = 0
    if (document.getElementById("checkboxStation").checked === true) {
        for (i; i < stationTab.length; i++) {
            var marker = stationTab[i];
            marker.setVisible(true);
        }
    }
    else {
        ;
        for (i; i < stationTab.length; i++) {
            var marker = stationTab[i];
            marker.setVisible(false);
        }
    }
});

$('#checkboxPark').click(function () {
    if (document.getElementById("checkboxPark").checked === true) {
        for (var i = 0; i < parkTab.length; i++) {
            var marker = parkTab[i];
            marker.setVisible(true);
        }
    }
    else {
        for (var i = 0; i < parkTab.length; i++) {
            var marker = parkTab[i];
            marker.setVisible(false);
        }
    }
});


// FONCTION QUI GERENT LA RECUPERATION DES MARKER ET QUI CREE LA LISTE DES POI

// pour les bars
function callbackBar(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerBar(results[i]);
        }
    }
}

function createMarkerBar(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('bar');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconBar,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "bar",
        visible: false
    });
    barTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + place.icon + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}

// pour les parcs
function callbackPark(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerPark(results[i]);
        }
    }
}

function createMarkerPark(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('park');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconPark,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "park",
        visible: false
    });
    parkTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + iconPark + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}

// pour les ecoles
function callbackSchool(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerSchool(results[i]);
        }
    }
}

function createMarkerSchool(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('ecole');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconSchool,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "school",
        visible: false
    });
    ecoleTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + iconSchool + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}

// pour les restaurants
function callbackResto(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerResto(results[i]);
        }
    }
}

function createMarkerResto(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('resto');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconRestaurant,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "restaurant",
        visible: false
    });
    restoTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + iconRestaurant + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}

// pour les hopitaux
function callbackHospital(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerHospital(results[i]);
        }
    }
}

function createMarkerHospital(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('hopital');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconHospital,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "hospital",
        visible: false
    });
    hopitalTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + iconHospital + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}

// pour les stations de bus
function callbackStation(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarkerStation(results[i]);
        }
    }
}

function createMarkerStation(place) {
    var placeLoc = place.geometry.location;
    var placesList = document.getElementById('station');
    var marker = new google.maps.Marker({
        map: mapPoiCommune,
        position: place.geometry.location,
        icon: {
            url: iconStation,
            scaledSize: new google.maps.Size(20, 20)
        },
        type: "bus-station",
        visible: false
    });
    stationTab.push(marker);

    placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center">' + place.name + ' <span class="badge badge-primary badge-pill"><img src="' + iconStation + '" style="width:20px"></span></li>';


    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });
}
