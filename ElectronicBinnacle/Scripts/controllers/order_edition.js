angular.module('bitacora.controllers.order_edition', ['ngRoute'])
    .controller('EditOrderCtrl', ['$scope', '$location', '$compile', 'order', 'authenticationFact', 'paramsFact', 'packagesFact', 'loadingFact', 'employeesFact',
        function ($scope, $location, $compile, order, authenticationFact, paramsFact, packagesFact, loadingFact, employeesFact) {
            var pageIndex = 1;
            $scope.allParameters = [];

            packagesFact.Packages.query(function(data) {
                for (var j = 0; j < data.length; j++)
                    $scope.allParameters.push({ param: data[j], checked: false, type: 0 });
            });
            paramsFact.Parameters.query(function (data) {
                for (var j = 0; j < data.length; j++)
                    $scope.allParameters.push({ param: data[j], checked: false, type: 1 });
            });
            $scope.selectedWorkPackages = [];
            $scope.order = order;
            $scope.samplers = [];
            authenticationFact.authenticatedUser(true).then(function(result) {
                    if (result.data.authenticated) {
                        $scope.order.Creator = {
                            UserId: result.data.user.UserId,
                            SamplingIdentifier: result.data.user.SamplingIdentifier,
                            OrdersCount: result.data.user.OrdersCount
                        }
                        if (!$scope.orderInfo.identifier)
                            $scope.orderInfo.identifier = $scope.order.Creator.SamplingIdentifier + prettyNumberString($scope.order.Creator.OrdersCount);
                    } else {
                        $location.path("/home");
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                    }
                });
            $scope.orderInfo = {
                identifier: "",
                startTime: { minute: "", hour: ""},
                endTime: { minute: "", hour: "" },
                alarmTime: { minute: "", hour: "" },
                selectedDate: "",
                selectedEndDate: "",
                selectedSamplerIndex: -1,
                samplingTypes: [{ name: 'AgP', value: 0 }, { name: 'AgR', value: 1 }, { name: 'AgN', value: 2 }, { name: 'AgS', value: 3 },{ name: 'AgEst', value: 4 }, { name: 'AgMar', value: 5 }],
                selectedType: -1,
                lastStep: false,
                firstStep: true,
                importation: "0",
                binnacleDataSelected: "0"
            };

            $scope.selectedTemplate = {
                path: "PartialRoute/eoclient"
            };
            init();


            function init() {
                if ($scope.order.OrderState == 4 || $scope.order.OrderState == 5)
                    loadSavedOrder();
            }
            $scope.go_previous = function() {
                $scope.orderInfo.lastStep = false;
                if (pageIndex != 1) {
                    pageIndex--;
                    $scope.selectedTemplate.path = getTemplatePath(pageIndex);
                    $('#section-stepper').stepper('prior');
                    if (pageIndex == 1) 
                        $scope.orderInfo.firstStep = true;
                }
            };
            $scope.go_next = function() {
                $scope.orderInfo.firstStep = false;
                switch (pageIndex) {
                    case 1:
                        validateClientData();
                        break;
                    case 2:
                        validateLocationData();
                        break;
                    case 3:
                        validateSamplingData();
                        break;
                    case 4:
                        validateWorkPackages();
                        $scope.samplers = [];
                        employeesFact.Samplers.query({ id: -1, watterMatch: $scope.orderInfo.selectedType }, function (samplers) {
                            for (var i = 0; i < samplers.length; i++) {
                                $scope.samplers.push({ name: samplers[i].Name + " " + samplers[i].LastName, index: i, EmployeeId: samplers[i].EmployeeId });
                            }
                        });
                        break;
                    default:
                } 
                if (pageIndex != 5) {
                    pageIndex++;
                    $scope.selectedTemplate.path = getTemplatePath(pageIndex);
                    $('#section-stepper').stepper('next');
                    if (pageIndex == 5)
                        $scope.orderInfo.lastStep = true;
                } else {
                    $scope.sendOrder();
                }
            };

            $scope.addWorkPackage = function () {
                var newpack = {
                    params: [],
                    SamplesNumber: 0,
                    Type: 0,
                    Period: 0
                }
                $scope.selectedWorkPackages.push(newpack);
            };
            $scope.removeWorkPackage = function (index) {
                $scope.selectedWorkPackages.splice(index, 1);
            }
            $scope.onAddParameter = function (index) {
                $scope.searchParam = {
                    param : {
                        Identifier : ""
                    }
                };
                function isChecked(parameter, selectedParameters) {
                    var property = (parameter.type == 1 ? 'ParameterId' : 'PackageId');
                    for (var i = 0; i < selectedParameters.length; i++) 
                        if (selectedParameters[i][property] == parameter.param[property])
                            return true;
                    return false;
                }
                for (var j = 0; j < $scope.allParameters.length; j++) 
                    $scope.allParameters[j].checked = isChecked($scope.allParameters[j], $scope.selectedWorkPackages[index].params);
                
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Adicionar Par&aacute;metro',
                    width: 600,
                    height: 500,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function () {
                        var content = '<section>' +
                            '<h2>Par&aacute;metros Creados</h2><div class="input-control text"><input type="text" data-ng-model="searchParam.param.Identifier" data-autofocus="" placeholder="Buscar par&aacute;metro" /><button class="btn-search"></button></div>' +
                            '<div id="scroll-section" data-role="scrollbox1" data-scroll="vertical"><table class="table bordered striped hovered" style="margin-bottom: 20px;"><thead><tr><th class="text-left">Identificador del Pr&aacute;metro</th>' +
                            '<th>Utilizado</th></tr></thead><tbody><tr data-ng-repeat="param in allParameters | filter:searchParam" data-ng-click="param.checked = !param.checked"><td class="text-left">{{param.param.Identifier}}</td><td><i class="icon-checkmark fg-lightOlive" data-ng-show="param.checked"></i><i class="icon-cancel-2 fg-dark" data-ng-show="!param.checked"></i></td></tr></tbody></table></div>' +
                            '<div class="right" style="margin-top:25px;"><button class="button primary" data-ng-click="addParameters('+index+')">Adicionar...</button>&nbsp;<button class="button" type="button" onclick="$.Dialog.close()">Cancelar</button></div>' +
                            '</section>';
                        $.Dialog.content(content);
                        $.Metro.initAll();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
                setTimeout(function () { $("#scroll-section").scrollbar({ height: 300, axis: "y" }); }, 100);
            };

            $scope.addParameters = function (index) {
                $scope.selectedWorkPackages[index].params = [];
                for (var j = 0; j < $scope.allParameters.length; j++) 
                    if ($scope.allParameters[j].checked) {
                        $scope.allParameters[j].param.Type = $scope.allParameters[j].type;
                        $scope.selectedWorkPackages[index].params.push($scope.allParameters[j].param);
                    }
                $.Dialog.close();
            }

            $scope.sendOrder = function () {
                if (!validateAll()) return;
                loadingFact.loadingUp();
                completeOrder();
                $scope.order.OrderState = 0;
                _sendOrder(true);
            }
            $scope.increaseHour = function(target) {
                if (target.hour == 23)
                    target.hour = 0;
                else
                    target.hour++;
                target.hour = parsePretyTime(target.hour);
            }
            $scope.increaseMinute = function (target) {
                if (target.minute == 59) {
                    target.minute = 0;
                    $scope.increaseHour(target);
                } else
                    target.minute++;
                target.minute = parsePretyTime(target.minute);
            }
            $scope.decreaseHour = function (target) {
                if (target.hour == 0)
                    target.hour = 23;
                else
                    target.hour--;
                target.hour = parsePretyTime(target.hour);
            }
            $scope.decreaseMinute = function (target) {
                if (target.minute == 0) {
                    target.minute = 59;
                    $scope.decreaseHour(target);
                }
                else
                    target.minute--;
                target.minute = parsePretyTime(target.minute);
            }
            $scope.importData = function() {
                switch ($scope.orderInfo.importation) {
                    case "0":
                        copySamplingPlaceProps($scope.order.LocationData, {});
                        break;
                    case "1":
                        copySamplingPlaceProps($scope.order.LocationData, $scope.order.ClientData);
                        break;
                    case "2":
                        copySamplingPlaceProps($scope.order.LocationData, $scope.order.BillerClient);
                        break;
                    default:
                        console.log("error al importar datos");
                }
            }
            $scope.saveOrder = function () {
                if ($scope.isLoading())return;
                $scope.order.OrderState = 4;
                loadingFact.loadingUp();
                completeOrder();
                if (!$scope.selectedWorkPackages || !$scope.selectedWorkPackages.length) {
                    $scope.order.WorkPackages = [];
                    $scope.order.$save(function (data, headers) {
                        if (data.success && data.success == true) {
                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Orden Creada...', content: "Se ha guardado correctamente la orden de trabajo." });
                            $location.path('/orders/');
                        } else {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        }
                    });}
                else
                    _sendOrder(false);
            }
            $scope.isLoading = function () {
                return loadingFact.isLoading();
            };

            function validateClientData() {
                var result = true;

                if (!$scope.order.ClientData.SocialReason) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Raz&oacute;n social del Cliente." });
                    result = false;
                }
                if (!$scope.order.ClientData.StreetNo) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Calle y No. del Cliente." });
                    result = false;
                }
                if (!$scope.order.ClientData.Colony) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta  la Colonia del Cliente." });
                    result = false;
                }
                if (!$scope.order.ClientData.DelMpio) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Delegaci&oacute;n o Municipio del Cliente." });
                    result = false;
                }
                if (!$scope.order.ClientData.Edo) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Estado del Cliente." });
                    result = false;
                }
                if (!$scope.order.ClientData.CP) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el C&oacute;digo Postal del Cliente." });
                    result = false;
                }
                if ($scope.order.ClientData.BillReport && !$scope.order.ClientData.RFC) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el RFC del Cliente." });
                    result = false;
                }

                if (!$scope.order.ClientData.BillReport) {
                    if (!$scope.order.BillerClient.SocialReason) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Raz&oacute;n social del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.StreetNo) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Calle y No. del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.Colony) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta  la Colonia del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.DelMpio) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Delegaci&oacute;n o Municipio del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.Edo) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Estado del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.CP) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el C&oacute;digo Postal del Cliente a Facturar." });
                        result = false;
                    }
                    if (!$scope.order.BillerClient.RFC) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el RFC del Cliente a Facturar." });
                        result = false;
                    }
                }
                return result;
            }
            function validateLocationData() {
                var result = true;

                if (!$scope.order.LocationData.Place) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Planta o Lugar del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.StreetNo) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Calle y No. del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Colony) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta  la Colonia del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.DelMpio) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Delegaci&oacute;n o Municipio del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Edo) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Estado del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.CP) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el C&oacute;digo Postal del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Contact) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Contacto del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Phone) {

                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Tel&eacute;fono del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Cellphone) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Celular del Sitio de Muestreo." });
                    result = false;
                }
                if (!$scope.order.LocationData.Email) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Correo del Sitio de Muestreo." });
                    result = false;
                }
                return result;
            }
            function validateSamplingData() {
                var result = true;
                if (!$scope.orderInfo.selectedDate) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta la Fecha Inicial de Muestreo." });
                    result = false;
                }
                if (!$scope.orderInfo.selectedEndDate) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta  la Fecha Final de Muestreo." });
                    result = false;
                }

                var startDate = getDateMls($scope.orderInfo.selectedDate) + getTimeMls($scope.orderInfo.startTime.hour + ":" + $scope.orderInfo.startTime.minute);
                var endDate = getDateMls($scope.orderInfo.selectedEndDate) + getTimeMls($scope.orderInfo.endTime.hour + ":" + $scope.orderInfo.endTime.minute);
                if (startDate >= endDate) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "La Fecha y Hora de Inicio no puede ser mayor o igual que la Final." });
                    result = false;
                }
                if (startDate < getNowMls()) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "La Fecha y Hora de Inicio no es v&aacute;lida." });
                    result = false;
                }
                if ($scope.orderInfo.selectedType == -1) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Falta el Tipo en los Datos de Muestreo." });
                    result = false;
                }

                var fromMinutes = parseInt($scope.orderInfo.startTime.minute);
                var fromHours = parseInt($scope.orderInfo.startTime.hour);
                if (Number.isNaN(fromMinutes) || Number.isNaN(fromHours) || fromHours < 0 || fromMinutes < 0 || fromHours >= 24 || fromMinutes >= 60) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "La hora de inicio es incorrecta." });
                    result = false;
                }
                var toMinutes = parseInt($scope.orderInfo.endTime.minute);
                var toHours = parseInt($scope.orderInfo.endTime.hour);
                if (Number.isNaN(toMinutes) || Number.isNaN(toHours) || toHours < 0 || toMinutes < 0 || toHours >= 24 || toMinutes >= 60) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "La hora final es incorrecta." });
                    result = false;
                }
                return result;
            }
            function validateWorkPackages() {
                var result = true;
                if ($scope.selectedWorkPackages.length == 0) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "No tiene creado Grupos de trabajo." });
                    result = false;
                }
                else {
                    if ($scope.selectedWorkPackages.some(function(e) { return e.SamplesNumber == 0; })) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "El N&uacute;mero de Muestras no puede ser 0." });
                        result = false;
                    }
                    if ($scope.orderInfo.selectedType == 3 && $scope.selectedWorkPackages.some(function (e) { return e.Type == 1; })) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "El tipo de Agua es AgS, por favor seleccione Tipo de Muestreo Simple en todos los grupos agregados." });
                        result = false;
                    }
                    if ($scope.selectedWorkPackages.some(function (e) { return e.params.length == 0; })) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "No tiene asignado par&aacute;metros en todos los grupos." });
                        result = false;
                    }
                    if ($scope.selectedWorkPackages.some(function (e) { return $scope.orderInfo.selectedType != 3 && e.Type == 1 && e.Period == 0; })) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "El per&iacute;odo no puede ser igual a 0." });
                        result = false;
                    }
                }
                return result;
            }
            function validateLast() {
                var result = true;
                if ($scope.orderInfo.selectedSamplerIndex == -1) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "No tiene seleccionado un muestreador." });
                    result = false;
                }
                var alarmMinutes = parseInt($scope.orderInfo.alarmTime.minute);
                var alarmHours = parseInt($scope.orderInfo.alarmTime.hour);
                if (Number.isNaN(alarmMinutes) || Number.isNaN(alarmHours) || alarmMinutes >= 60 || alarmHours >= 24) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "El tiempo de alarma es incorrecto." });
                    result = false;
                }
                return result;
            }
            function validateAll() {
                var result = validateClientData();
                result = validateLocationData() && result;
                result = validateSamplingData() && result;
                result = validateWorkPackages() && result;
                return validateLast() && result;

            }
            function parsePretyTime(number) {
                return (number < 10 ? "0" + number : number);
            }
            function completeOrder() {
                if ($scope.orderInfo.selectedType != -1)
                    $scope.order.SamplingData.SamplingKind = parseInt($scope.orderInfo.selectedType);

                if ($scope.orderInfo.binnacleDataSelected != "3") {
                    var fromDataObj = null;
                    switch ($scope.orderInfo.binnacleDataSelected) {
                        case "0":
                            fromDataObj = $scope.order.ClientData;
                            break;
                        case "1":
                            fromDataObj = $scope.order.BillerClient;
                            break;
                        default:
                            fromDataObj = $scope.order.LocationData;
                    }
                    if ($scope.orderInfo.binnacleDataSelected == "2")
                        $scope.order.BinnacleData.SocialReason = fromDataObj.Place;
                    else
                        $scope.order.BinnacleData.SocialReason = fromDataObj.SocialReason;
                    $scope.order.BinnacleData.StreetNo = fromDataObj.StreetNo;
                    $scope.order.BinnacleData.Colony = fromDataObj.Colony;
                    $scope.order.BinnacleData.DelMpio = fromDataObj.DelMpio;
                    $scope.order.BinnacleData.Edo = fromDataObj.Edo;
                    $scope.order.BinnacleData.CP = fromDataObj.CP;
                    $scope.order.SamplingData.Identifier = $scope.orderInfo.identifier;
                }

                if ($scope.orderInfo.selectedSamplerIndex != -1)
                    $scope.order.Sampler = $scope.samplers[$scope.orderInfo.selectedSamplerIndex];
                $scope.order.SamplingData.StartTime = getDateMls($scope.orderInfo.selectedDate) + getTimeMls($scope.orderInfo.startTime.hour + ":" + $scope.orderInfo.startTime.minute);
                $scope.order.SamplingData.EndTime = getDateMls($scope.orderInfo.selectedEndDate) + getTimeMls($scope.orderInfo.endTime.hour + ":" + $scope.orderInfo.endTime.minute);
                $scope.order.SamplingData.Period = getTimeMls($scope.orderInfo.alarmTime.hour + ":" + $scope.orderInfo.alarmTime.minute);

            }
            function loadSavedOrder() {
                $scope.orderInfo.selectedType = $scope.order.SamplingData.SamplingKind.toString();
                for (var i = 0; i < $scope.order.WorkPackages.length; i++) {
                    $scope.selectedWorkPackages.push({
                        params: [],
                        SamplesNumber: $scope.order.WorkPackages[i].SamplesNumber,
                        Type: $scope.order.WorkPackages[i].Type,
                        Period: $scope.order.WorkPackages[i].Period
                    });
                    for (var j = 0; j < $scope.order.WorkPackages[i].Packages.length; j++) {
                        var pack = $scope.order.WorkPackages[i].Packages[j];
                        if (pack.Standard) {
                            pack.Type = 0;
                            $scope.selectedWorkPackages[$scope.selectedWorkPackages.length - 1].params.push(pack);
                        }
                        else
                            for (var k = 0; k < pack.Parameters.length; k++) {
                                pack.Parameters[k].Type = 1;
                                $scope.selectedWorkPackages[$scope.selectedWorkPackages.length - 1].params.push(pack.Parameters[k]);
                            }
                    }
                }

                $scope.orderInfo.selectedDate = getDateStr($scope.order.SamplingData.StartTime);
                $scope.orderInfo.selectedEndDate = getDateStr($scope.order.SamplingData.EndTime);
                
                $scope.orderInfo.startTime = getTimeObj($scope.order.SamplingData.StartTime);
                $scope.orderInfo.endTime = getTimeObj($scope.order.SamplingData.EndTime);
                $scope.orderInfo.alarmTime = getTimeObj($scope.order.SamplingData.Period);

                $scope.orderInfo.identifier = order.SamplingData.Identifier;
                $scope.orderInfo.binnacleDataSelected = "3";
            }
            function _sendOrder(toSend) {
                $scope.order.relatedCount = $scope.selectedWorkPackages.length;

                $scope.order.WorkPackages = [];
                for (var j = 0; j < $scope.selectedWorkPackages.length; j++) {
                    var paramsGroups = makeGroups($scope.selectedWorkPackages[j].params);
                    $scope.order.WorkPackages.push({
                            SamplesNumber: $scope.selectedWorkPackages[j].SamplesNumber,
                            Type: $scope.selectedWorkPackages[j].Type,
                            Period: $scope.selectedWorkPackages[j].Period,
                            Packages: paramsGroups
                        });
                }
                $scope.order.$save(function (data, headers) {
                    if (data.success) {
                        if (toSend)
                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Orden Creada...', content: "Se ha creado correctamente la orden de trabajo y ser&aacute; enviada." });
                        else
                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Orden Creada...', content: "Se ha guardado correctamente la orden de trabajo." });
                        $location.path('/orders/');
                    } else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    }
                }, function() {
                    loadingFact.loadingDown();
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                });
            }
        }
    ]);

function copySamplingPlaceProps(obj1, obj2) {
    obj1.Place = (obj2.SocialReason ? obj2.SocialReason : "");
    obj1.StreetNo = (obj2.StreetNo ? obj2.StreetNo : "");
    obj1.Colony = (obj2.Colony ? obj2.Colony : "");
    obj1.DelMpio = (obj2.DelMpio ? obj2.DelMpio : "");
    obj1.Edo = (obj2.Edo ? obj2.Edo : "");
    obj1.CP = (obj2.CP ? obj2.CP : "");

    obj1.RequirementsTechnical = (obj2.RequirementsTechnical ? obj2.RequirementsTechnical : "");
    obj1.ObservationsForSafety = (obj2.ObservationsForSafety ? obj2.ObservationsForSafety : "");
}
function getTemplatePath(index) {
    var url = "";
    switch (index) {
        case 1:
            url += 'PartialRoute/eoclient';
            break;
        case 2:
            url += 'PartialRoute/eosamplingplace';
            break;
        case 3:
            url += 'PartialRoute/eosamplingdata';
            break;
        case 4:
            url += 'PartialRoute/eopackage';
            break;
        case 5:
            url += 'PartialRoute/eofinalphase';
            break;
    }
    return url;
}
function makeGroups(paramsArray) {
    var result = [];
    var params = [];
    for (var i = 0; i < paramsArray.length; i++) {
        if (paramsArray[i].Type == 0)
            result.push({ PackageId: paramsArray[i].PackageId });
        else
            params.push(paramsArray[i]);
    }
    if (params.length)
        result.push({
            Identifier: "",
            Standard: false,
            Parameters: params
        });
    return result;
}
function prettyNumberString(number, length) {
    if (!length)
        length = 6;
    var stringNumber = number.toString();
    for (var i = stringNumber.length; i < length; i++)
        stringNumber = "0" + stringNumber;
    return stringNumber;
}
