angular.module('bitacora.controllers.map', [])
    .controller('MapCtrl', [
        '$scope',
        function($scope) {
            var map = null;
            var markers = [];
            var infowindows = [];
            var notificationWindow = '<div class="gmaps-notification">' +
                '<span class="subheader-secondary">Pepe Antonio</span>' +
                '<div class="body">' +
                '<span>30/12/2014, 9:54 AM</span><br />' +
                '<span>Velocidad: 85.6 <span class="measurement-unit">Km/h</span></span><br />' +
                '<span>Precisión: 5.8 <span class="measurement-unit">m</span></span><br /><br />' +
                '<p>Llamada realizada a: +546421546. Nombre del contacto: Karla Rodriguez. Duración 21 segundos.</p>' +
                '</div>' +
                '</div>';
//            $(document).ready(function () { initialize(); });


            function initialize() {
                var mapOptions = {
                    center: new google.maps.LatLng(17.998867, -92.954385),
                    zoom: 15,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

                setTimeout(google.maps.event.addListenerOnce(map, 'idle', function() { google.maps.event.trigger(map, 'resize');}), 100);
            }
            $scope.showNotification = function (notificationId) {
                var myLatlng = new google.maps.LatLng(-25.363882, 131.044922);

                var infowindow = new google.maps.InfoWindow({
                    content: notificationWindow
                });
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    icon: 'images/msg.png'
                });
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });

            }
        }
    ]);