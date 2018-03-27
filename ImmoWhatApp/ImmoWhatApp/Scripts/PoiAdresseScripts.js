

// LES VARIABLES


var latit = $('#latitude').val();
var longit = $('#longitude').val();
var home = { lat: Number(latit), lng: Number(longit) };
var mapPoiCommune;
var infowindow;
var radius = 1000;
var radiusHopital = 2000;
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
        zoom: 16
    });
    var markerHome = new google.maps.Marker({
        map: mapPoiCommune,
        position: home,
        icon: {
            url: '/assets/svg/maps/home.png',
            scaledSize: new google.maps.Size(25, 25)
        }

    });

    infowindow = new google.maps.InfoWindow();
    var service = new google.maps.places.PlacesService(mapPoiCommune);

    service.nearbySearch({
        location: home,
        radius: radius,
        type: ['bar']
    }, callbackBar);

    service.nearbySearch({
        location: home,
        radius: radius,
        type: ['school']
    }, callbackSchool);
    service.nearbySearch({
        location: home,
        radius: radius,
        type: ['park']
    }, callbackPark);
    service.nearbySearch({
        location: home,
        radius: radiusHopital,
        type: ['hospital']
    }, callbackHospital);
    service.nearbySearch({
        location: home,
        radius: radius,
        type: ['restaurant']
    }, callbackResto);
    service.nearbySearch({
        location: home,
        radius: radius,
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
        $('#nbBars').html(barTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#barListPOI').hide();
        document.getElementById("checkboxBar").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconBar + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }

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
        $('#nbParks').html(parkTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#ParkListPOI').hide();
        document.getElementById("checkboxPark").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconPark + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }

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
        $('#nbSchools').html(ecoleTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#SchoolListPOI').hide();
        document.getElementById("checkboxSchool").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconSchool + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }

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
        $('#nbRestos').html(restoTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#RestaurantListPOI').hide();
        document.getElementById("checkboxRestaurent").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconRestaurant + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }

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
        $('#nbHospitals').html(hopitalTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#HospitalListPOI').hide();
        document.getElementById("checkboxHopital").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconHospital + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }

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
        $('#nbStations').html(stationTab.length);
    }
    else if (status == "ZERO_RESULTS") {
        $('#StationListPOI').hide();
        document.getElementById("checkboxStation").disabled = true;
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

    var end = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

    var service = new google.maps.DistanceMatrixService();

    service.getDistanceMatrix({
        origins: [home],
        destinations: [end],
        travelMode: 'DRIVING',
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, callBackDistance);

    function callBackDistance(response, status) {
        if (status !== 'OK') {
            alert('Error was: ' + status);
        } else {
            var distance = response.rows[0].elements[0].distance.text;
            placesList.innerHTML += '<li class="list-group-item d-flex justify-content-between align-items-center"><span class="badge"><img src="' + iconStation + '" style="width:20px"></span>' + place.name + ' <span class="badge badge-primary badge-pill">' + distance + '</span></li>';
        }
    }




    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(place.name);
        infowindow.open(mapPoiCommune, this);
    });

    function getDistanceBetweenTwoPoints(start, end) {


        var service = new google.maps.DistanceMatrixService();

        service.getDistanceMatrix({
            origins: [start],
            destinations: [end],
            travelMode: 'DRIVING',
            unitSystem: google.maps.UnitSystem.METRIC,
            avoidHighways: false,
            avoidTolls: false
        }, callBackDistance);

        function callBackDistance(response, status) {
            if (status !== 'OK') {
                alert('Error was: ' + status);
            } else {
                var distance = response.rows[0].elements[0].distance.text;
            }
        }
    }
}
