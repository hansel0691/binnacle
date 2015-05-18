angular.module('bitacora.controllers.routes', [])
    .controller('RoutesCtrl', ['$scope', 'notificationFact', 'loadingFact', 'samplers', '$rootScope', 'usersFact', '$compile',
        function ($scope, notificationFact, loadingFact, samplers, $rootScope, usersFact, $compile) {
            loadingFact.loadingUp();
            var map = null;
            var notifications = [];
            var positions = [];

            $scope.notifications = [];
            $scope.samplers = samplers;
            //if (samplers && samplers.length)
            //    $scope.samplers = [{ EmployeeId: 0, FullName: 'Todos' }].concat(samplers);

            $scope.search = {
                NotificationCategory: 0,
                selectedSampler: 0
            }
            function initialize() {
                if (checkForGoogle()) {
                    var mapOptions = {
                        center: new google.maps.LatLng(17.998867, -92.954385),
                        zoom: 15,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    };
                    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                    setTimeout(google.maps.event.addListenerOnce(map, 'idle', function () { google.maps.event.trigger(map, 'resize'); }), 200);
                }
                for (var i = 0; i < $scope.samplers.length; i++)
                    $scope.samplers[i].visible = true;
                getPositions();
                $scope.getNotifications();
                loadingFact.loadingReset();
            }


            $scope.onFilterRoute = function () {
                $scope.searchEmployee = {
                    Name: ""
                };

                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Filtrar Empleados',
                    width: 500,
                    height: 520,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function () {
                        var content = '<form data-ng-submit="filter()">' +
                            '<h2>Todos sus Subordinados</h2>' +
                            '<div class="input-control text"><input type="text" data-autofocus="" data-ng-model="searchEmployee.Name" placeholder="Buscar empleados" /><button class="btn-search"></button></div>' +
                            '<div style="height: 340px;overflow-y: auto; overflow-x: hidden;margin: 5px 0;"><table class="table bordered hovered"><thead><tr><th class="text-left">Nombre</th><th style="width: 100px;">Ver</th></tr></thead></div>' +
                            '<tbody><tr data-ng-if="employee.EmployeeId != 0" data-ng-repeat="employee in samplers| filter:searchEmployee.Name" data-ng-click="employee.visible = !employee.visible;"><td class="text-left">{{employee.Name}} {{employee.LastName}}</td><td style="width: 90px;">' +
                            '<i class="fg-lightGreen icon-checkmark" data-ng-if="employee.visible"></i><i class="icon-cancel-2" data-ng-if="!employee.visible"></i></td></tr></tbody></table>' +
                            '</div><div class="right"><button type="submit" class="button default hover">Filtrar...</button>&nbsp;' +
                            '<button class="button hover" type="button" data-ng-click="selectionAllEmployees()"><span data-ng-if="!SelectedAll()">Marcar Todos</span><span data-ng-if="SelectedAll()">Desmarcar Todos</span></button></div>' +
                            '</form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            };
            $scope.selectionAllEmployees = function () {
                var value = !$scope.SelectedAll();
                for (var i = 0; i < $scope.samplers.length; i++)
                    if ($scope.samplers[i].EmployeeId != 0)
                        $scope.samplers[i].visible = value;
            };
            $scope.filter = function () {
                getPositions();
                filterNotifications();
                $.Dialog.close();
            };
            $scope.getHeaderText = function (notifyType) {
                switch (notifyType) {
                    case 1:
                        return "Estado del GPS";
                    case 2:
                        return "SMS";
                    case 3:
                        return "MMS";
                    case 4:
                        return "Llamadas";
                    case 5:
                        return "Apagado/Encendido";
                    case 6:
                        return "Actualización de ISY SAMPLER";
                    case 7:
                        return "Batería Baja";
                    case 8:
                        return "ISY SAMPLER Detenido";
                    case 9:
                        return "Paquete Instalado";
                    case 10:
                        return "Perfil de Sonido";
                    case 11:
                        return "Orden Enviada";
                    case 12:
                        return "Orden Recibida";
                    default:
                }
            };
            $scope.visibleEmployees = function () {
                var result = [{ EmployeeId: 0, FullName: 'Todos' }];
                for (var i = 0; i < $scope.samplers.length; i++)
                    if ($scope.samplers[i].EmployeeId == 0 || $scope.samplers[i].visible)
                        result.push($scope.samplers[i]);
                return result;
            };
            $scope.SelectedAll = function () {
                for (var i = 0; i < $scope.samplers.length; i++)
                    if ($scope.samplers[i].EmployeeId && !$scope.samplers[i].visible)
                        return false;
                return true;
            }

            function getPositions() {
                if (!checkForGoogle()) return;
                clearPositions();

                for (var i = 0; i < $scope.samplers.length; i++)
                {
                    var employeeId = $scope.samplers[i].EmployeeId;
                    if ($scope.samplers[i].visible) {
                        usersFact.currentPosition(employeeId)
                            .success(function (position) {
                                if (!position) {
                                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                                    return;
                                }
                                if (position.empty) {
                                    $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: position.name + "No tiene datos de posicionamiento." });
                                    return;
                                }
                                showPosition(position);
                            })
                            .error(function () {
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                            });
                    }
                    else {
                        hidePosition(employeeId);
                    }
                }
            };
            function showPosition(position) {
                var myLatlng = new google.maps.LatLng(position.Latitude, position.Longitude);

                var infowindow = new google.maps.InfoWindow({
                    content: '<span>' + position.Employee.FullName + '</span><br /><br />' +
                        '<span>' + $scope.getDateTime(position.DateTime, '/') + '</span><br />' +
                        '<span>Velocidad: ' + round(position.Speed, 2) + ' <span class="measurement-unit">Km/h</span></span><br />' +
                        '<span>Precisión: ' + position.Accuracy + ' <span class="measurement-unit">m</span></span><br /><br />'
                });
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                });
                if ($scope.search.selectedSampler == position.Employee.EmployeeId)
                    centerPosition(position);
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
                positions.push({ marker: marker, infoWindow: infowindow, employeeId: position.Employee.EmployeeId });
                
            };
            function hidePosition(employeeId) {
                var index = indexOf(positions, function (p) { return p.employeeId == employeeId; });
                if (index < 0 || index >= positions.length) return;
                positions[index].marker.setMap(null);
                positions.splice(index, 1);
            };
            function clearPositions() {
                for (var i = 0; i < positions.length; i++)
                    positions[i].marker.setMap(null);
                positions = [];
            }
            function centerPosition(position) {
                map.setCenter(new google.maps.LatLng(position.Latitude, position.Longitude));
            }

            $scope.getNotifications = function () {
                loadingFact.loadingUp();
                notificationFact.Notifications()
                    .success(function (notifications) {
                        if (!notifications || !notifications.length)
                            $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No tiene notificaciones." });

                        $rootScope.newNotifications = 0;
                        $scope.notifications = [];
                        if (notifications)
                            $scope.notifications = notifications.sort(function (n1, n2) { return (n2.NotificationId < n1.NotificationId ? -1 : 1); });
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    })
                    .then(function () {
                        loadingFact.loadingDown();
                    });
            }
            $scope.toogleNotification = function (notificationId) {
                if (!$scope.hasGpsData(notificationId)) return;

                var index = indexOf(notifications, function (n) { return n.notificationId == notificationId; });
                if (index != -1)
                    hideNotification(index);
                else
                    showNotification(notificationId);
            }
            function showNotification(notificationId) {
                var notification = null;
                for (var i = 0; i < $scope.notifications.length; i++)
                    if ($scope.notifications[i].NotificationId == notificationId)
                        notification = $scope.notifications[i];

                if (!notification) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    return;
                }
                if (!checkForGoogle()) return;
                var myLatlng = new google.maps.LatLng(notification.LATITUDE, notification.LONGITUDE);
                var infowindow = new google.maps.InfoWindow({
                    content: '<div class="gmaps-notification">' +
                        '<span class="subheader-secondary">' + notification.SamplerName + '</span>' +
                        '<div class="body">' +
                        '<span>' + $scope.getDateTime(notification.DATETIME, '/') + '</span><br />' +
                        '<span>Velocidad: ' + round(notification.SPEED, 2) + ' <span class="measurement-unit">Km/h</span></span><br />' +
                        '<span>Precisión: ' + notification.ACCURACY + ' <span class="measurement-unit">m</span></span><br /><br />' +
                        '<p>' + notification.NOTIFICATION_MSG + '</p>' +
                        '</div>' +
                        '</div>'
                });
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    icon: '/Images/icon 32x32.png'
                });
                map.setCenter(myLatlng);
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
                notifications.push({ marker: marker, notificationId: notification.NotificationId });
            }
            function hideNotification(index) {
                if (index < 0 || index >= notifications.length) return;
                notifications[index].marker.setMap(null);
                notifications.splice(index, 1);
            }
            $scope.clearNotifications = function () {
                for (var i = 0; i < notifications.length; i++)
                    notifications[i].marker.setMap(null);
                notifications = [];
            }
            $scope.hasGpsData = function (notificationId) {
                var index = indexOf($scope.notifications, function (n) { return n.NotificationId == notificationId; });
                if (index == -1) return false;
                var notification = $scope.notifications[index];
                return notification.LATITUDE && notification.LONGITUDE;
            }
            function filterNotifications() {
                loadingFact.loadingUp();
                var today = new Date();
                notificationFact.search("", $scope.search.selectedSampler, $scope.search.NotificationCategory, getDateMls(prettyNumberString(today.getDate(), 2) + '.' + prettyNumberString(today.getMonth() + 1, 2) + '.' + today.getFullYear()))
                    .success(function (data) {
                        $scope.notifications = [];
                        if (!data || data.length == 0)
                            return;

                        $rootScope.newNotifications = 0;
                        $scope.notifications = data.sort(function (n1, n2) { return (n2.NotificationId < n1.NotificationId ? -1 : 1); });;

                        //change this in update
                        //for (var i = 0; i < $scope.notifications.length; i++) {
                        //    if (!$scope.samplers[i].visibility) {

                        //    }
                        //}

                        loadingFact.loadingReset();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    })
                    .then(function () {
                        loadingFact.loadingDown();
                    });
            }

            $(document).ready(function () { initialize(); });
        }
    ]);

function checkForGoogle() {
    try {
        google;
        return true;
    } catch (e) {
        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "No hay conexion con google map." });
        return false;
    }
}
function indexOf(array, predicate) {
    for (var i = 0; i < array.length; i++) {
        if (predicate(array[i]))
            return i;
    }
    return -1;
}

    