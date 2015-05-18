angular.module('bitacora.controllers.package_edition', ['ngRoute'])
    .controller('EditPackageCtrl', ['$scope', "$location", '$routeParams', '$compile', 'pack', 'params', 'loadingFact',
        function ($scope, $location, $routeParams, $compile, pack, params, loadingFact) {
            $scope.package = pack;
            $scope.all_params = [];
            for (var i = 0; i < params.length; i++) {
                $scope.all_params.push({ param: params[i], checked: $scope.package.Parameters.some(function (element) { return element.ParameterId == params[i].ParameterId; }) });
            }


            var id = $routeParams.id;
            if (id == undefined) 
                $scope.headerTitle = "Crear";
            else 
                $scope.headerTitle = "Editar";
            

            $scope.add_param = function () {
                $scope.searchParam = "";
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
                            '<h2>Par&aacute;metros Creados</h2><div class="input-control text"><input type="text" data-ng-model="searchParam" data-autofocus="" placeholder="Buscar par&aacute;metro" /><button class="btn-search"></button></div>' +
                            '<div id="scroll-section" data-role="scrollbox1" data-scroll="vertical"><table class="table bordered hovered" style="margin-bottom: 20px;"><thead><tr><th class="text-left">Identificador del Pr&aacute;metro</th>' +
                            '<th>Utilizado</th></tr></thead><tbody><tr data-ng-repeat="param in all_params | filter:searchParam" data-ng-click="toogleParam(param.param.ParameterId)"><td class="text-left">{{param.param.Identifier}}</td><td><i class="icon-checkmark fg-lightOlive" data-ng-show="param.checked"></i><i class="icon-cancel-2 fg-dark" data-ng-show="!param.checked"></i></td></tr></tbody></table></div>' +
                            '<div class="right" style="margin-top:25px;"><button class="button primary" data-ng-click="add()">Adicionar...</button>&nbsp;<button class="button" type="button" onclick="$.Dialog.close()">Cancelar</button></div>'+
                            '</section>';
                        $.Dialog.content(content);
                        $.Metro.initAll();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
                setTimeout(function() { $("#scroll-section").scrollbar({ height: 300, axis: "y" }); }, 100);
            };
            $scope.toogleParam = function(parameterId) {
                var index = myIndexOf($scope.all_params, parameterId, idEquality);
                $scope.all_params[index].checked = !$scope.all_params[index].checked;
            }
            $scope.add = function() {
                var selectedList = selectedParams($scope.all_params);
                if (!selectedList || selectedList.length == 0) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No tiene seleccionado ning&uacute;n par&aacute;metro." });
                    return;
                }
                $scope.package.Parameters = [];
                for (var k = 0; k < selectedList.length; k++) 
                    $scope.package.Parameters.push(selectedList[k]);
                $.Dialog.close();
            }
            $scope.remove_param = function (index) {
                $scope.all_params[myIndexOf($scope.all_params, $scope.package.Parameters[index].ParameterId, idEquality)].checked = false;
                $scope.package.Parameters.splice(index, 1);
            };
            $scope.accept = function () {
                if (!$scope.package.Identifier) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "En identificador del grupo es vacio." });
                    return;
                }
                if (!$scope.package.Parameters || !$scope.package.Parameters.length) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Datos Faltantes...', content: "Debe asignar al menos un par&aacute;metro al grupo." });
                    return;
                }

                loadingFact.loadingUp();
                $scope.package.$save(function (data, headers) {
                    if (data.success && data.success == true) {
                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Nuevo Grupo de Trabajo...', content: "Se ha a&ntilde;adido un grupo satifactoriamente." });
                        $location.path('/packages');
                    } else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        setTimeout(function () { loadingFact.loadingDown(); }, 100);
                    }
                });
            };
            $scope.isLoading = function() {
                return loadingFact.isLoading();
            };
        }
    ]);

function myIndexOf(array, element, equalityFunc) {
    for (var i = 0; i < array.length; i++) {
        if (equalityFunc(array[i].param, element))
            return i;
    }
    return -1;
}
function idEquality(e1, e2) { return e1.ParameterId == e2; }
function selectedParams(array) {
    var result = [];
    for (var i = 0; i < array.length; i++) {
        if (array[i].checked)
            result.push(array[i].param);
    }
    return result;
}