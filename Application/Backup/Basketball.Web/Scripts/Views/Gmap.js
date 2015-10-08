/*
* gmap.js
*
* Contains methods that use the Google maps API
*/

var map;
var localSearch = new GlocalSearch();

var icon = new GIcon();
icon.image = "http://www.google.com/mapfiles/marker.png";
icon.shadow = "http://www.google.com/mapfiles/shadow50.png";
icon.iconSize = new GSize(20, 34);
icon.shadowSize = new GSize(37, 34);
icon.iconAnchor = new GPoint(10, 34);

/**
* Searches for the specified post code
*/
function usePointFromPostcode(postcode, callbackFunction) {
    // Don't init map if post code is blank
    if (postcode == "")
        $("#map").hide();

    localSearch.setSearchCompleteCallback(null,
		function () {

		    if (localSearch.results[0]) {
		        var resultLat = localSearch.results[0].lat;
		        var resultLng = localSearch.results[0].lng;
		        var point = new GLatLng(resultLat, resultLng);
		        callbackFunction(point);
		    } else {
		        $("#directions").html("Post code not found");
		        //$("#map").hide();
		        //$("#directions").hide();
		    }
		});

    localSearch.execute(postcode + ", UK");
}

/**
* Adds a marker to the map at the specified point
*/
function placeMarkerAtPoint(point) {
    var marker = new GMarker(point, icon);
    map.addOverlay(marker);
}

/**
* Centers the map on the specified point
*/
function setCenterToPoint(point) {

    map.setCenter(point, 16, G_NORMAL_MAP); //G_NORMAL_MAP G_HYBRID_MAP

    // Set directions link
    $("#directions").html('<a href="http://maps.google.co.uk/maps?saddr=&daddr=' + point.toUrlValue() + '" rel="external" title="Click here to get directions to the venue">Get directions to venue<\/a>');
    setExternalLinks();
}

function showPointLatLng(point) {
    alert("Latitude: " + point.lat() + "\nLongitude: " + point.lng());
}

/*
* Initialise the map object
*/
function mapLoad() {
    if (GBrowserIsCompatible()) {
        map = new GMap2(document.getElementById("map"));

        //map.addControl(new GLargeMapControl());
        //map.addControl(new GSmallMapControl());
        //map.addControl(new GMapTypeControl());
        map.setUIToDefault();
        //map.setCenter(new GLatLng(54.622978,-2.592773), 5, G_HYBRID_MAP);
    }
}


//addLoadEvent(mapLoad);
//addUnLoadEvent(GUnload);

// Upload the map object to prevent memory leaks
$(window).unload(GUnload);

