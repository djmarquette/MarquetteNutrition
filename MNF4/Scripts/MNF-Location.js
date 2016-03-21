/*!
* mnfLocation.js
* jQuery and JavaScript functions for Location page
* http://www.marquettenutrition.com/
* 
* Date: Feb 20, 2013
*/
$(document).ready(function() {
		function initialize() {
			var location = new google.maps.LatLng(30.212322, -97.830884);  // New Location - Brodie/Wm. Cannon -- 30.212322, -97.830884
			var mapDiv = document.getElementById('map_canvas');			
			var streetViewOptions = {
				enableCloseButton: true,
				visible: false
			};
			var street = new google.maps.StreetViewPanorama(mapDiv, streetViewOptions);
			var mapOptions = {
				center: location,
				zoom: 14,
				mapTypeId: google.maps.MapTypeId.ROADMAP,
				streetView: street
			};
			var map = new google.maps.Map(mapDiv, mapOptions);
			var marker = new google.maps.Marker({
				position: location,
				map: map,
				title: "Marquette Nutrition & Fitness"
			});
		}
		google.maps.event.addDomListener(window, 'load', initialize);
	});