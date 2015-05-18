angular.module('bitacora.controllers.generalmap', [])
    .controller('GeneralMapCtrl', ['$scope', '$http', '$compile', 'employeesFact', 'loadingFact', '$cookieStore',
        function ($scope, $http, $compile, employeesFact, loadingFact, $cookieStore) {
            var today = new Date();
            var map = null;
            $scope.search = {
                Name : "",
                Date: prettyNumberString(today.getDate(), 2) + '.' + prettyNumberString(today.getMonth() + 1, 2) + '.' + today.getFullYear(),
                activeEmployees: [],
                SelectedEmployee: 0,
            }
            $scope.subordinates = [];
            $scope.fillScreen = false;
            var wasLoaded = false;
            $scope.employeeList = [{ EmployeeId: 0, FullName: 'Todas' }];

            var paths = {};
            var markers = {};
            employeesFact.subordinate(1)
                .success(function (data) {
                    if (data.subordinates.length) {
                        $scope.subordinates = data.subordinates.map(function (e) {
                            e.path = null;
                            e.visible = $scope.search && $scope.search.activeEmployees && $scope.search.activeEmployees.some(function (eId) { return e.EmployeeId == eId; });
                            return e;
                        });
                        $scope.employeeList = $scope.employeeList.concat($scope.subordinates.map(function (s) { return { EmployeeId: s.EmployeeId, FullName: s.Name + ' ' + s.LastName }; }));
                        if (wasLoaded) {
                            $scope.searchObj();
                            wasLoaded = false;
                        }
                    }
                })
                .error(function () {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                .then(function () {
                    loadingFact.loadingReset();
                });

            setInterval(function () {
                if (map)
                    $scope.searchObj(true);
            }, 5*60000);

            $scope.onFilterRoute = function () {
                $scope.searchEmployee = {
                    Name : ""
                };  
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Filtrar Rutas de Empleados',
                    width: 500,
                    height: 520,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function () {
                        var content = '<form data-ng-submit="advanceFilter()">' +
                            '<h2>Todos sus Subordinados</h2>' +
                            '<div class="input-control text"><input type="text" data-autofocus="" data-ng-model="searchEmployee.Name" placeholder="Buscar empleados" /><button class="btn-search"></button></div>' +
                            '<div style="height: 340px;overflow-y: auto; overflow-x: hidden;margin: 5px 0;"><table class="table bordered hovered"><thead><tr><th class="text-left">Nombre</th><th style="width: 100px;">Ver</th></tr></thead></div>' +
                            '<tbody><tr data-ng-repeat="employee in subordinates| filter:searchEmployee" data-ng-click="employee.visible = !employee.visible;"><td class="text-left">{{employee.Name}} {{employee.LastName}}</td><td style="width: 90px;">' +
                            '<i class="fg-lightGreen icon-checkmark" data-ng-if="employee.visible"></i><i class="icon-cancel-2" data-ng-if="!employee.visible"></i></td></tr></tbody></table>' +
                            '</div><div class="right"><button type="submit" class="button default hover">Aceptar...</button>&nbsp;' +
                            '<button class="button hover" type="button" data-ng-click="showAllEmployees()">Marcar Todos</button></div>' +
                            '</form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            }
            $scope.advanceFilter = function () {
                $.Dialog.close();
            };
            $scope.showAllEmployees = function() {
                for (var i = 0; i < $scope.subordinates.length; i++) 
                    $scope.subordinates[i].visible = true;
            }
            $scope.fullScreen = function () {
                $scope.fillScreen = !$scope.fillScreen;
                if ($scope.fillScreen) {
                    $('#navigation-header').css('visibility', 'collapse');
                    $('#main-section').css('visibility', 'collapse');
                    $('#main-view').css('min-height', '0');
                } else {
                    $('#navigation-header').css('visibility', 'visible');
                    $('#main-section').css('visibility', 'visible');
                    $('#main-view').css('min-height', $scope.minHeight.toString());
                }
            }
            $scope.searchObj = function (donMoveToLast) {
                saveSearch();
                filterByName();
                if (!checkForGoogle()) return;
                loadingFact.loadingUp();

                for (var i = 0; i < $scope.subordinates.length; i++) {
                    var employee = $scope.subordinates[i];
                    removeLines(employee.EmployeeId);
                    removeMarkers(employee.EmployeeId);
                    if (employee.visible)
                        paintEmployeePath(employee, donMoveToLast);
                }
                google.maps.event.addDomListener(window, 'load', initialize);
                loadingFact.loadingReset();
            }
            
            function initialize() {
                loadSearch();
                if (!checkForGoogle()) return;

                var mapOptions = {
                    center: new google.maps.LatLng(17.998867, -92.954385),
                    zoom: 15,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                setTimeout(google.maps.event.addListenerOnce(map, 'idle', function () { google.maps.event.trigger(map, 'resize'); }), 100);
            }
            function filterByName() {
                if ($scope.search.SelectedEmployee)
                    for (var i = 0; i < $scope.subordinates.length; i++) {
                        if ($scope.subordinates[i].EmployeeId == $scope.search.SelectedEmployee) {
                            $scope.subordinates[i].visible = true;
                            return;
                        }
                    }
            }

            function paintEmployeePath(employee, donMoveToLast) {
                employeesFact.searchPath(employee.EmployeeId, getDateMls($scope.search.Date))
                    .success(function(data) {
                        if (!data.path) return; 
                        var pathCoordinates = [];
                        var dataPaths = [];
                        
                        for (var i = 0; i < data.path.length - 1; i++) {
                            var currentPosition = { lat: data.path[i].Latitude, lng: data.path[i].Longitude };
                            var nextPosition = { lat: data.path[i + 1].Latitude, lng: data.path[i + 1].Longitude };
                            dataPaths.push(data.path[i]);
                            pathCoordinates.push(currentPosition);
                            if (coordinatesDistance(currentPosition.lat, currentPosition.lng, nextPosition.lat, nextPosition.lng) > 1) {
                                addLine(pathCoordinates, employee.EmployeeId, dataPaths);
                                pathCoordinates = [];
                                dataPaths = [];
                            }
                        }
                        if (pathCoordinates.length)
                            addLine(pathCoordinates, employee.EmployeeId, dataPaths);

                        if (data.path.length)
                            addSpetialMarker(employee.EmployeeId, data.path[0], 0);
                        if (data.path.length > 1) {
                            var last = data.path[data.path.length - 1];
                            addSpetialMarker(employee.EmployeeId, last, 1);
                            if (!donMoveToLast && employee.EmployeeId == $scope.search.SelectedEmployee)
                                map.setCenter(new google.maps.LatLng(last.Latitude, last.Longitude));
                        }
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    });
            }
            function addMarker(lat, lng, employeeId, data) {
                var gicon = {
                    path: google.maps.SymbolPath.CIRCLE,
                    fillColor: "#f03",
                    fillOpacity: 0.5,
                    strokeColor: "red",
                    strokeWeight: 5,
                    strokeOpacity: 0.5,
                    scale: 1,
                };
//                var gicon = new google.maps.MarkerImage('images/Ball-green-128.png', null, null, null);
                var latLng = new google.maps.LatLng(lat, lng);
                var marker = new google.maps.Marker({
                    'position': latLng,
                    'icon': gicon,
                });
                var infowindow = new google.maps.InfoWindow({
                    content: '<span>' + data.Employee.FullName + '</span><br />' +
                        '<span>Latitud: ' + data.Latitude + '</span><span style="margin-left:10px;">Longitud: ' + data.Longitude + '</span><br />' +
                        '<span>Precisión: ' + data.Accuracy + ' <span class="measurement-unit">m</span></span><span style="margin-left:10px;">Velocidad: ' + round(data.Speed, 2) + ' <span class="measurement-unit">m/s</span></span><span style="margin-left:10px;">Fecha: ' + $scope.getDateTime(data.DateTime) + '</span><br />'
                });
                marker.setMap(map);
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
                if (!markers[employeeId])
                    markers[employeeId] = [];
                markers[employeeId].push(marker);
            }
            function removeMarkers(employeeId) {
                if (markers[employeeId]) {
                    for (var j = 0; j < markers[employeeId].length; j++)
                        markers[employeeId][j].setMap(null);
                    markers[employeeId] = null;
                }
            }
            function addLine(pathCoordinates, employeeId, dataPaths) {
                var mapCoordinates = [];
                for (var i = 0; i < pathCoordinates.length; i++) {
                    addMarker(pathCoordinates[i].lat, pathCoordinates[i].lng, employeeId, dataPaths[i]);
                    mapCoordinates.push(new google.maps.LatLng(pathCoordinates[i].lat, pathCoordinates[i].lng));
                }
                var path = new google.maps.Polyline({
                    path: mapCoordinates,
                    geodesic: true,
                    strokeColor: '#FF2D19',
                    strokeOpacity: 0.7,
                    strokeWeight: 3
                });
                path.setMap(map);

                if (!paths[employeeId])
                    paths[employeeId] = [];
                paths[employeeId].push(path);
            }
            function removeLines(employeeId) {
                if (paths[employeeId]) {
                    for (var j = 0; j < paths[employeeId].length; j++)
                        paths[employeeId][j].setMap(null);
                    paths[employeeId] = null;
                }
            }
            function coordinatesDistance(lat1, lng1, lat2, lng2) {
                var earthRadius = 6371; //kilometers
                var dLat = toRadians(lat2 - lat1);
                var dLng = toRadians(lng2 - lng1);
                var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(toRadians(lat1)) * Math.cos(toRadians(lat2)) * Math.sin(dLng / 2) * Math.sin(dLng / 2);
                var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
                var dist = (earthRadius * c);
                return dist;
            }
            function toRadians(number) {
                return ((number * 1.0) * Math.PI) / (180 * 1.0);
            }
            
            function addSpetialMarker(employeeId, data, type) {
                var text = (type == 0 ? '<span>Primera Posición</span>' : '<span>Última Posición</span>');

                var latLng = new google.maps.LatLng(data.Latitude, data.Longitude);
                var marker = new google.maps.Marker({
                    'position': latLng,
                });
                var infowindow = new google.maps.InfoWindow({
                    content: text + '<br /><br />' +
                        '<span>' + data.Employee.FullName + '</span><br />' +
                        '<span>Latitud: ' + data.Latitude + '</span><span style="margin-left:10px;">Longitud: ' + data.Longitude + '</span><br />' +
                        '<span>Precisión: ' + data.Accuracy + ' <span class="measurement-unit">m</span></span><span style="margin-left:10px;">Velocidad: ' + round(data.Speed, 2) + ' <span class="measurement-unit">m/s</span></span></span><span style="margin-left:10px;">Fecha: ' + $scope.getDateTime(data.DateTime) + '</span><br />'
                });
                if (type == 0)
                    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
                else
                    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

                marker.setMap(map);
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
                if (!markers[employeeId])
                    markers[employeeId] = [];
                markers[employeeId].push(marker);
            }

            function saveSearch() {
                $scope.search.activeEmployees = [];
                for (var i = 0; i < $scope.subordinates.length; i++)
                    if ($scope.subordinates[i].visible)
                        $scope.search.activeEmployees.push($scope.subordinates[i].EmployeeId);
                $cookieStore.put('generalmap', $scope.search);
            }
            function loadSearch() {
                var storeSearchObj = $cookieStore.get('generalmap');
                if (storeSearchObj) {
                    $scope.search = storeSearchObj;
                    wasLoaded = true;
                }
            }


            $(document).ready(function () { initialize(); });
        }
    ]);