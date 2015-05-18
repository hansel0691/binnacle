angular.module('bitacora.controllers.orders', [])
    .controller('IndexOrdersCtrl', ['$scope', 'orders', '$routeParams', 'ordersFact', '$compile', 'loadingFact', 
        function ($scope, orders, $routeParams, ordersFact, $compile, loadingFact) {
            var topStatusVisible = true;
            var bootomStatusVisible = true;
            $scope.search = {
                identifier: '',
                unsended: true,
                sended: true,
                evaluated: true,
                unevaluated: true,
                unfinished: true,
                uncomplete : true,
                socialReason : '',
                place : '',
                rfc: '',
                startDate: "",
                endDate : ""
            }
            $scope.generatingReport = false;
            $scope.generationMsg = false;
            $scope.generationBttmMsg = false;
            init();
            
            function init() {
                if ($routeParams.state) {
                    $scope.search.sended = false;
                    $scope.search.unsended = false;
                    $scope.search.evaluated = false;
                    $scope.search.unevaluated = false;
                    $scope.search.unfinished = false;
                    $scope.search.uncomplete = false;
                    $scope.search[$routeParams.state] = !$scope.search[$routeParams.state];
                }

                $scope.simplePagination = {
                    currentIndex: 0,
                    last: orders.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < orders.count; i++)
                    $scope.simplePagination.pagination.push(i);
                initOrders();
            }
            function initOrders() {
                $scope.selectedIndex = -1;
                $scope.order = undefined;
                $scope.orders = [];

                $scope.orders = orders.orders.sort(function (a, b) {
                    if (a.SamplingData.StartTime < b.SamplingData.StartTime)
                        return 1;
                    else if (b.SamplingData.StartTime < a.SamplingData.StartTime)
                        return -1;
                    else {
                        if (a.SamplingData.Identifier < b.SamplingData.Identifier)
                            return 1;
                        else
                            return 0;
                    }
                });
                if ($scope.orders.length) {
                    $scope.selectedIndex = 0;
                    getAndUpdateOrder($scope.selectedIndex, -1,  $scope.orders);
                }
                else
                    $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No existen &oacute;rdenes de muestreo que cumplan este filtro!" });
            }

            $scope.dayFilter = function(index) {
                return function (order) {
                    var today = getNowMls();
                    var startDate = order.SamplingData.StartTime;
                    var elapsedDays = daydiff(today, startDate);
                    if (index == 0 && elapsedDays < 1)
                        return true;
                    else if (index == 1 && elapsedDays >= 1 && elapsedDays < 2)
                        return true;
                    else if (index == 2 && elapsedDays >= 2 && elapsedDays < 8)
                        return true;
                    else if (index == 3 && elapsedDays >= 8)
                        return true;
                    return false;
                }
            }

            $scope.goPage = function (paginationInfo, index) {
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);

                loadingFact.loadingUp();
                ordersFact.search($scope.search.identifier, $scope.search.sended, $scope.search.unsended, $scope.search.evaluated, $scope.search.unevaluated, $scope.search.unfinished, $scope.search.uncomplete,
                    $scope.search.socialReason, $scope.search.place, $scope.search.rfc, getDateMls($scope.search.startDate), getDateMls($scope.search.endDate), nextIndex + 1)
                    .success(function (session) {
                        orders = session;
                        paginationInfo.currentIndex = nextIndex;
                        var top = Math.min(paginationInfo.currentIndex + 2, paginationInfo.last - 1);
                        var low = Math.max(paginationInfo.currentIndex - 2, 0);
                        paginationInfo.top = top + (2 - (paginationInfo.currentIndex - low));
                        paginationInfo.low = low - (2 - (top - paginationInfo.currentIndex));
                        initOrders();
                        loadingFact.loadingReset();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            };
            $scope.viewOrder = function (id) {
                var index = getIndexFromId(id, $scope.orders);
                
                if ($scope.selectedIndex != index) {
                    getAndUpdateOrder(index, $scope.selectedIndex, $scope.orders);
                    $scope.selectedIndex = index;
                }
            }
            $scope.getDate = function (elapsedTime) {
                return getDateStr(elapsedTime, '/');
            }
            $scope.getHours = function(elapsedTime) {
                return getTimeStr(elapsedTime);
            }
            $scope.getSamplingKind = function (type) {
                switch (type) {
                    case 0:
                        return "AgP";
                    case 1:
                        return "AgR";
                    case 2:
                        return "AgN";
                    case 3:
                        return "AgS";
                    case 4:
                        return "AgEst";
                    case 5:
                        return "AgMar";
                    default:
                        return "";
                }
            }
            $scope.onFilter = function () {
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Filtrar &Oacute;rdenes',
                    width: 570,
                    height: 470,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function() {
                        var content = '<form data-ng-submit="filterAdvance()">' +
                            '<div class="right" style="width: 260px"><label>Fecha Final del Intervalo</label><div id="datepicker-section" class="input-control text" data-role="datepicker" data-effect="fade" data-format="d/m/yyyy" data-locale="es" style="width:250px !important;"><input type="text" placeholder="dd/mm/aaaa" data-ng-model="search.endDate" data-date-picker="" /><button class="btn-date"></button></div></div>' +
                            '<div style="width: 260px"><label>Fecha Inicial del Intervalo</label><div id="datepicker-section" class="input-control text" data-role="datepicker" data-effect="fade" data-format="d/m/yyyy" data-locale="es" style="width:250px !important;"><input type="text" placeholder="dd/mm/aaaa" data-ng-model="search.startDate" data-date-picker="" /><button class="btn-date"></button></div></div>' +
                            '<label>Identificador de la Muestra</label>' +
                            '<div class="input-control text"><input type="text" placeholder="Identificador de la Muestra" data-ng-model="search.identifier" /><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Raz&oacute;n Social</label>' +
                            '<div class="input-control text"><input type="text" placeholder="Raz&oacute;n Social" data-ng-model="search.socialReason"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Planta o Lugar</label>' +
                            '<div class="input-control text"><input type="text" placeholder="Planta o Lugar" data-ng-model="search.place"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<div><label>RFC del Cliente Facturador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="RFC del Cliente Facturador" data-ng-model="search.rfc"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<div class="right" style="margin-top: 10px;"><button type="submit" class="button default hover">Filtrar...</button>&nbsp;' +
                            '<button class="button hover" type="button" data-ng-click="cancel_filter()">Limpiar Filtro</button></div>' +
                            '</div></form>';
                        $.Dialog.content(content);
                        $.Metro.initAll();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            };
            $scope.filterAdvance = function ()
            {
                $.Dialog.close();
                $scope.searchObj();
            }
            $scope.cancel_filter = function() {
                $.Dialog.close();
                $scope.search.identifier = '';
                $scope.search.socialReason = '';
                $scope.search.place = '';
                $scope.search.rfc = '';
                $scope.search.startDate = "";
                $scope.search.endDate = "";
                $scope.searchObj();
            }
            $scope.toogleOrderState = function (prop) {
                switch (prop) {
                    case 0:
                        $scope.search.uncomplete = !$scope.search.uncomplete;
                        break;
                    case 1:
                        $scope.search.unfinished = !$scope.search.unfinished;
                        break;
                    case 2:
                        $scope.search.unsended = !$scope.search.unsended;
                        break;
                    case 3:
                        $scope.search.sended = !$scope.search.sended;
                        break;
                    case 4:
                        $scope.search.unevaluated = !$scope.search.unevaluated;
                        break;
                    case 5:
                        $scope.search.evaluated = !$scope.search.evaluated;
                        break;
                    default:
                }
                $scope.searchObj();
            }
            $scope.toogleSatus = function (statusBar) {
                if (statusBar == 1)
                    topStatusVisible = !topStatusVisible;
                else if (statusBar == 2)
                    bootomStatusVisible = !bootomStatusVisible;
                else {
                    topStatusVisible = !(topStatusVisible || bootomStatusVisible);
                    bootomStatusVisible = topStatusVisible;
                }

                if (topStatusVisible)
                    $('#top-status-bar').fadeIn('slow');
                else
                    $('#top-status-bar').fadeOut('slow');

                if (bootomStatusVisible)
                    $('#bootom-status-bar').fadeIn('slow');
                else
                    $('#bootom-status-bar').fadeOut('slow');
            }
            $scope.reportGeneration = function (exportTo, exportFrom) {
                $scope.generationMsg = false;
                $scope.generationBttmMsg = false;
                window.open('/Orders/GenerateExel/' + $scope.order.Id + '?exportTo=' + exportTo + '&exportFrom=' + exportFrom, '_newtab');
            }
            $scope.getState = function (id)
            {
                var index = getIndexFromId(id, $scope.orders);
                switch ($scope.orders[index].OrderState) {
                    case 0:
                        return 'Orden de Trabajo Sin Enviar';
                    case 1:
                        return 'Orden de Trabajo Enviada';
                    case 2:
                        return 'Orden de Trabajo Sin Evaluar';
                    case 3:
                        return 'Orden de Trabajo Evaluada';
                    case 4:
                        return 'Orden de Trabajo Sin Terminar';
                    case 5:
                        return 'Orden de Trabajo Incumplida';
                }
            }



            $scope.isLoading = function() {
                return loadingFact.isLoading();
            }
            $scope.searchObj = function () {
                var page = 1;
                loadingFact.loadingUp();
                ordersFact.search($scope.search.identifier, $scope.search.sended, $scope.search.unsended, $scope.search.evaluated, $scope.search.unevaluated, $scope.search.unfinished, $scope.search.uncomplete,
                    $scope.search.socialReason, $scope.search.place, $scope.search.rfc, getDateMls($scope.search.startDate), getDateMls($scope.search.endDate), page)
                    .success(function (data) {
                        orders = data;
                        init();
                        loadingFact.loadingReset();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }

            function getAndUpdateOrder(index, oldIndex, orders) {
                //                loadingFact.loadingUp();
                if ($scope.order && orders[index].Id == $scope.order.Id) return;
                ordersFact.Orders.get({ id: orders[index].Id }, function (data) {
                    $scope.order = data;
                    if (oldIndex != -1) $scope.orders[oldIndex].selected = false;
                    $scope.orders[index].selected = true;
                    $scope.order.parameters = allParameters();
//                    loadingFact.loadingReset();
                });
            }
            function allParameters() {
                if (!$scope.order) return [];
                var params = [];
                for (var i = 0; i < $scope.order.WorkPackages.length; i++) {
                    var wp = $scope.order.WorkPackages[i];
                    for (var j = 0; j < wp.Packages.length; j++) {
                        var pack = wp.Packages[j];
                        if (pack.Standard)
                            params.push({ identifier: pack.Identifier, samplesNo: wp.SamplesNumber, sampleType: wp.Type, period: wp.Period, type : 0, Id : pack.PackageId});
                        else
                            for (var k = 0; k < pack.Parameters.length; k++) {
                                var param = pack.Parameters[k];
                                params.push({ identifier: param.Identifier, samplesNo: wp.SamplesNumber, sampleType: wp.Type, period: wp.Period, type: 1});
                            }
                    }
                }
                return params;
            }
        }
    ]);

function daydiff(first, second) {
    var a = new Date(first);
    var b = new Date(second);
    a.setHours(0);
    a.setMinutes(0);
    b.setHours(0);
    b.setMinutes(0);

    return Math.floor((a.getTime() - b.getTime()) / (1000 * 60 * 60 * 24));
}
function getIndexFromId(id, enumerable, key) {
    if (!key) key = 'Id';
    for (var i = 0; i < enumerable.length; i++)
        if (enumerable[i][key] == id) {
            return i;
        }
    return -1;
}