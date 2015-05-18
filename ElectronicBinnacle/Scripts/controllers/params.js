angular.module('bitacora.controllers.params', ['ngRoute'])
    .controller('IndexParamsCtrl', ['$scope', 'params', 'paramsFact', 'FunctionShares', 'loadingFact', '$compile',
        function ($scope, params, paramsFact, FunctionShares, loadingFact, $compile) {
            $scope.params = [];
            $scope.search = {
                Identifier: "",
                Container: "",
                Preserver: "",
                Volume: undefined,
                TMPA: undefined,

                Key: "",
                FieldMeasurement: false
        }
            $scope.new = newParam(paramsFact);
            init();

            function init() {
                if (params.$resolved && params.count == 0)
                    $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No existen par&aacute;metros en la base de datos!" });
                else
                    $scope.params = params.parameters.map(function (e) { return new paramsFact.Parameters(e); });

                $scope.simplePagination = {
                    currentIndex: 0,
                    last: params.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < params.count; i++) $scope.simplePagination.pagination.push(i);
            }

            $scope.goPage = function(paginationInfo, index) {
                loadingFact.loadingUp();
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);

                paramsFact.search($scope.search.Identifier, $scope.search.Container, $scope.search.Preserver, $scope.search.Volume, $scope.search.TMPA, nextIndex + 1)
                    .success(function(session) {
                        $scope.params = session.parameters.map(function(e) { return new paramsFact.Parameters(e); });
                        paginationInfo.currentIndex = nextIndex;
                        var top = Math.min(paginationInfo.currentIndex + 2, paginationInfo.last - 1);
                        var low = Math.max(paginationInfo.currentIndex - 2, 0);
                        paginationInfo.top = top + (2 - (paginationInfo.currentIndex - low));
                        paginationInfo.low = low - (2 - (top - paginationInfo.currentIndex));
                        loadingFact.loadingDown();
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            };
            $scope.onAdvancedFilter = function () {
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Filtrar Par&aacute;metros',
                    width: 500,
                    height: 410,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function () {
                        var content = '<form data-ng-submit="advanceFilter()">' +
                            '<label>Identificador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="par&aacute;metro N" data-ng-model="search.Identifier" /><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Recipiente</label>' +
                            '<div class="input-control text"><input type="text" placeholder="nombre del recipiente" data-ng-model="search.Container"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Preservador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="preservador del par&aacute;metro" data-ng-model="search.Preserver"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<div class="right"><label>Volumen</label>' +
                            '<div class="input-control text size2"><input type="number" placeholder="0" min="0" step="0.1" data-ng-model="search.Volume"></div></div>' +
                            '<div><label>TMPA</label>' +
                            '<div class="input-control text size3"><input type="number" placeholder="TMPA" min="0" step="0.1" data-ng-model="search.TMPA"></div></div><br>' +
                            '<div class="right"><button type="submit" class="button default hover">Filtrar...</button>&nbsp;' +
                            '<button class="button hover" type="button" data-ng-click="cancel_filter()">Limpiar Filtro</button></div>' +
                            '</div></form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            }
            $scope.advanceFilter = function () {
                $scope.searchObj();
                $.Dialog.close();
            }
            $scope.cancel_filter = function () {
                $scope.search = {
                    Identifier: "",
                    Container: "",
                    Preserver: "",
                    Volume: undefined,
                    TMPA: undefined
                }
                $scope.searchObj();
                $.Dialog.close();
            }
            $scope.create_param = function () {
                if ($scope.isLoading()) return;
                $scope.new = newParam(paramsFact);
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Crear Par&aacute;metro',
                    width: 500,
                    height: 410,
                    padding: 10,
                    onShow: function () {
                        var content = '<form data-ng-submit="create()">' +
                            '<label>Identificador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="par&aacute;metro N" data-autofocus="" data-ng-model="new.Identifier" ><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Recipiente</label>' +
                            '<div class="input-control text"><input type="text" placeholder="tipo del recipiente" data-ng-model="new.Container"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Preservador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="preservador del par&aacute;metro" data-ng-model="new.Preserver"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Clave</label>' +
                            '<div class="input-control text"><input type="text" placeholder="clave del par&aacute;metro" data-ng-model="new.Key"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<div class="right"><label>Volumen</label>' +
                            '<div class="input-control text size2"><input type="number" placeholder="0" min="0" step="0.1" data-ng-model="new.Volume"></div></div>' +
                            '<div><label>TMPA</label>' +
                            '<div class="input-control text size2"><input type="number" placeholder="0" min="0" step="0.1" data-ng-model="new.TMPA"></div></div><br>' +
                            '<div class="input-control switch" data-role="input-control"><label>Si es de Campo Presione<input  type="checkbox" data-ng-model="new.FieldMeasurement"><span class="check" style="margin-left: 10px;"></span></label></div>' +
                            '<div class="right"><button type="submit" class="button fg-white bg-darkBlue" data-ng-disabled="isLoading()">Crear...</button>&nbsp;' +
                            '<button class="button" type="button" onclick="$.Dialog.close()" data-ng-disabled="isLoading()">Cancelar</button></div><br />' +
                            '</div></form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            }
            $scope.create = function () {
                if (!validateParam()) return;

                loadingFact.loadingUp();
                var param = newParam(paramsFact);
                setParameter(param, $scope.new);
                $scope.new.$save(function (data, headers) {
                    $.Dialog.close();
                    if (data.success && data.success == true) {
                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Nuevo Par&aacute;metro...', content: param.Identifier + " se ha a&ntilde;adido correctamente." });
                        param.ParameterId = data.id;
                        if ($scope.params.length < 20)
                            $scope.params.push(param);
                        else if (data.overflow)
                            $scope.simplePagination.pagination.push(++$scope.simplePagination.last);
                    } else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    }
                    loadingFact.loadingDown();
                });
            };
            $scope.removeParam = function (id, name, index) {
                loadingFact.loadingUp();
                FunctionShares.removeParam(id, $scope.simplePagination.currentIndex + 1)
                    .success(function(data) {
                        if (data.success && data.success == true) {
                            if (data.notPass) {
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Eliminaci&oacute;n Fallida...', content: data.error });
                                loadingFact.loadingDown();
                                return;
                            }


                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Par&aacute;metro Eliminado...', content: name + " se ha eliminado correctamente." });
                            $scope.params.splice(index, 1);
                            if (data.param)
                                $scope.params.push(data.param);
                            else if (!$scope.params.length && $scope.params.length > 0) {
                                $scope.simplePagination.pagination.length--;
                                $scope.simplePagination.last--;
                                $scope.goPage($scope.simplePagination, $scope.simplePagination.currentIndex - 1);
                            }
                            if (data.last) {
                                $scope.simplePagination.pagination.length--;
                                $scope.simplePagination.last--;
                            }
                        }
                        loadingFact.loadingDown();
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }
            $scope.editParam = function (id, index) {
                if ($scope.isLoading()) return;
                $scope.new = $scope.params.filter(function (element) { return element.ParameterId == id; })[0];
                $scope.backupParam = newParam(paramsFact);
                setParameter($scope.backupParam, $scope.new);
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Editar Par&aacute;metro',
                    width: 500,
                    height: 410,
                    padding: 10,
                    onShow: function () {
                        var content = '<form data-ng-submit="edit(' + index + ')">' +
                            '<label>Identificador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="par&aacute;metro N" data-ng-model="new.Identifier"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Recipiente</label>' +
                            '<div class="input-control text"><input type="text" placeholder="tipo del recipiente" data-ng-model="new.Container"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Preservador</label>' +
                            '<div class="input-control text"><input type="text" placeholder="preservador del par&aacute;metro" data-ng-model="new.Preserver"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<label>Clave</label>' +
                            '<div class="input-control text"><input type="text" placeholder="clave del par&aacute;metro" data-ng-model="new.Key"><button type="button" class="btn-clear" tabindex="-1"></button></div>' +
                            '<div class="right"><label>Volumen</label>' +
                            '<div class="input-control text size2"><input type="number" placeholder="0" min="0" step="0.1" data-ng-model="new.Volume"></div></div>' +
                            '<div><label>TMPA</label>' +
                            '<div class="input-control text size3"><input type="number" placeholder="TMPA" min="0" step="0.1" data-ng-model="new.TMPA"></div></div><br>' +
                            '<div class="input-control switch" data-role="input-control"><label>Si es de Campo Presione<input  type="checkbox" data-ng-model="new.FieldMeasurement"><span class="check" style="margin-left: 10px;"></span></label></div>' +
                            '<div class="right"><button type="submit" class="button fg-white bg-darkBlue" data-ng-disabled="isLoading()">Editar...</button>&nbsp;' +
                            '<button class="button" type="button" data-ng-click="cancelEdition()" data-ng-disabled="isLoading()">Cancelar</button></div><br />' +
                            '</div></form>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            };
            $scope.edit = function (index) {
                if (!validateParam())return;
                loadingFact.loadingUp();
                var param = {};
                setParameter(param, $scope.new);
                $.Dialog.close();
                $scope.new.$save(function (data, headers) {
                    if (data.success && data.success == true) {
                        setParameter($scope.params[index], param);
                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Par&aacute;metro Editado...', content: $scope.params[index].Identifier + " se ha modificado correctamente." });
                    } else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    }
                    loadingFact.loadingDown();
                });
            }
            $scope.cancelEdition = function() {
                setParameter($scope.new, $scope.backupParam);
                $.Dialog.close();
            }
            $scope.hasPermission = function (permissions) {
                if (!$scope.auth)return false;
                for (var i = 0; i < permissions.length; i++) {
                    if (FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissions[i]))
                        return true;
                }
                return false;
            };
            $scope.hasPermissionAnd = function (permissions) {
                if (!$scope.auth) return false;
                for (var i = 0; i < permissions.length; i++) {
                    if (!FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissions[i]))
                        return false;
                }
                return true;
            };
            $scope.isLoading = function () {
                return loadingFact.isLoading();
            };
            $scope.searchObj = function () {
                var page = 1;
                loadingFact.loadingUp();
                paramsFact.search($scope.search.Identifier, $scope.search.Container, $scope.search.Preserver, $scope.search.Volume, $scope.search.TMPA, page)
                    .success(function (data) {
                        params = data;
                        init();
                        loadingFact.loadingReset();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }


            function validateParam() {
                if (!$scope.new.Identifier) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Campo requerido...', content: "El identificador de un par&aacute;metro es requerido para su creaci&oacute;n." });
                    return false;
                }
                if (!$scope.new.Container) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Campo requerido...', content: "El contenedor de un par&aacute;metro es requerido para su creaci&oacute;n." });
                    return false;
                }
                if (!$scope.new.Preserver) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Campo requerido...', content: "El preservador de un par&aacute;metro es requerido para su creaci&oacute;n." });
                    return false;
                }
                if ($scope.new.Volume == undefined) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Campo requerido...', content: "El volumen de un par&aacute;metro debe ser un n&uacute;mero." });
                    return false;
                }
                if ($scope.new.TMPA == undefined) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Campo requerido...', content: "El TMPA de un par&aacute;metro debe ser un n&uacute;mero." });
                    return false;
                }
                return true;
            }
        }
    ]);

function newParam(paramsFact) {
    return new paramsFact.Parameters({
        Identifier: "",
        Container: "",
        Preserver: "",
        Volume: 0,
        TMPA: 0,

        Key: "",
        FieldMeasurement: false
    });
}
function setParameter(param1, param2) {
    param1.ParameterId = param2.ParameterId;
    param1.Identifier = param2.Identifier;
    param1.Container = param2.Container;
    param1.Preserver = param2.Preserver;
    param1.Volume = param2.Volume;
    param1.TMPA = param2.TMPA;
    param1.FieldMeasurement = param2.FieldMeasurement;

    param1.Key = param2.Key;
    param1.FieldMeasurement = param2.FieldMeasurement;
}