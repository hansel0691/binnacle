angular.module('bitacora.controllers.role_edition', [])
    .controller('EditRoleCtrl', ['$scope', "$location", '$routeParams', '$compile', 'role', 'permissions', 'loadingFact',
        function ($scope, $location, $routeParams, $compile, role, permissions, loadingFact) {
            var pageInfo = {};
            $scope.role = role;
            $scope.all_permissons = permissions.data;
            $scope.search = "";

            var id = $routeParams.id;
            if (id == undefined) {
                pageInfo.headerTitle = "Crear";
                pageInfo.state = false;
            } else {
                pageInfo.headerTitle = "Editar";
                pageInfo.state = true;
            }
            $scope.pageInfo = pageInfo;

            $scope.add_permisson = function () {
                $.Dialog({
                    shadow: true,
                    overlay: true,
                    draggable: true,
                    icon: '<span class="icon-droplet"></span>',
                    title: 'Agregar Permiso',
                    width: 500,
                    padding: 10,
                    content: 'This Window is draggable by caption.',
                    onShow: function() {
                        var content = '<div>' +
                            '<div class="size2 right" style="margin-top: -5px;"><label>Valor</label>' +
                            '<div class="input-control select"><select data-ng-model="selectedValue"><option value="0">Ninguno</option><option value="1">Asignado</option><option value="2">Completo</option></select></div></div>' +
                            '<div class="size4"><label>Permiso</label>' +
                            '<div class="input-control select"><select data-ng-model="selectedPermisson" ng-options="permission for permission in all_permissons"></select></div></div>' +
                            '<div class="text-right" style="margin:25px 0;">' +
                            '<button class="button primary" data-ng-click="add_perm()">Adicionar...</button>&nbsp;' +
                            '<button class="button" type="button" onclick="$.Dialog.close()">Cancelar</button>' +
                            '</div></div></div>';
                        $.Dialog.content(content);
                        $.Metro.initInputs();
                    }
                });
                $compile($(".metro.window-overlay"))($scope);
            }
            $scope.add_perm = function () {
                if ($scope.selectedPermisson == undefined) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Seleccione un permiso." });
                    return;
                }
                if ($scope.selectedValue == undefined) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Seleccione un valor" });
                    return;
                }
                var permission = { Identifier: permissionId($scope.selectedPermisson), Value: $scope.selectedValue };
                for (var i = 0; i < $scope.role.Permissions.length; i++) {
                    if ($scope.role.Permissions[i].Identifier == permission.Identifier) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Actualmente tiene a&ntilde;adido este permiso." });
                        return;
                    }
                }
                $scope.role.Permissions.push(permission);
                $.Dialog.close();
                $scope.selectedPermisson = undefined;
                $scope.selectedValue = undefined;
            }
            $scope.accept = function () {
                if (!validateRole())return;
                loadingFact.loadingUp();
                $scope.role.$save(function (data, headers) {
                    if (data.success && data.success == true) {
                        if (id == undefined)
                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Nuevo Role...', content: "Se ha a&ntilde;adido un role al sistema." });
                        else
                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Edici&oacute;n de Role...', content: "Se ha modificado un role del sistema." });
                        $location.path('/roles');
                    } else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    }
                });
            };
            $scope.getPermissionFromId = function(permissionId) {
                switch (permissionId) {
                    case 0:
                        return "Ver lista de Usuarios";
                    case 1:
                        return "Ver Usuario";
                    case 2:
                        return "Crear Usuario";
                    case 3:
                        return "Editar Usuario";
                    case 4:
                        return "Eliminar Usuario";
                    case 5:
                        return "Ver lista de Roles";
                    case 6:
                        return "Ver Role";
                    case 7:
                        return "Crear Role";
                    case 8:
                        return "Editar Role";
                    case 9:
                        return "Eliminar Role";
                    case 10:
                        return "Ver lista de Empleados";
                    case 11:
                        return "Ver Empleado";
                    case 12:
                        return "Crear Empleado";
                    case 13:
                        return "Editar Empleado";
                    case 14:
                        return "Eliminar Empleado";
                    case 15:
                        return "Ver lista de Parametros";
                    case 16:
                        return "Ver Parametro";
                    case 17:
                        return "Crear Parametro";
                    case 18:
                        return "Editar Parametro";
                    case 19:
                        return "Eliminar Parametro";
                    case 20:
                        return "Ver lista de Grupos";
                    case 21:
                        return "Ver Grupo";
                    case 22:
                        return "Crear Grupo";
                    case 23:
                        return "Editar Grupo";
                    case 24:
                        return "Eliminar Grupo";
                    case 25:
                        return "Ver lista de \u00D3rdenes de Trabajo";
                    case 26:
                        return "Ver Orden de Trabajo";
                    case 27:
                        return "Crear Orden de Trabajo";
                    case 28:
                        return "Editar Orden de Trabajo";
                    case 29:
                        return "Eliminar Orden de Trabajo";
                    case 30:
                        return "Ver Muestra";
                    case 31:
                        return "Editar Muestra";
                    case 32:
                        return "Ver Informe de Usuario";
                    case 33:
                        return "Ver Rutas";
                    default:
                        return "error";
                }
            }
            $scope.isLoading = function() {
                return loadingFact.isLoading();
            }
            function validateRole() {
                if (!$scope.role.Name) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Identificador del role." });
                    return false;
                }
                if ($scope.role.Permissions.every(function (e) { return e.Value == "0"; })) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "El role debe tener al menos un Permiso asignado." });
                    return false;
                }
                return true;
            }
        }
    ]);

function permissionId(permission) {
    switch (permission) {
        case "Ver lista de Usuarios":
            return 0;
        case "Ver Usuario":
            return 1;
        case "Crear Usuario":
            return 2;
        case "Editar Usuario":
            return 3;
        case "Eliminar Usuario":
            return 4;
        case "Ver lista de Roles":
            return 5;
        case "Ver Role":
            return 6;
        case "Crear Role":
            return 7;
        case "Editar Role":
            return 8;
        case "Eliminar Role":
            return 9;
        case "Ver lista de Empleados":
            return 10;
        case "Ver Empleado":
            return 11;
        case "Crear Empleado":
            return 12;
        case "Editar Empleado":
            return 13;
        case "Eliminar Empleado":
            return 14;
        case "Ver lista de Parametros":
            return 15;
        case "Ver Parametro":
            return 16;
        case "Crear Parametro":
            return 17;
        case "Editar Parametro":
            return 18;
        case "Eliminar Parametro":
            return 19;
        case "Ver lista de Grupos":
            return 20;
        case "Ver Grupo":
            return 21;
        case "Crear Grupo":
            return 22;
        case "Editar Grupo":
            return 23;
        case "Eliminar Grupo":
            return 24;
        case "Ver lista de Ordenes de Trabajo":
            return 25;
        case "Ver Orden de Trabajo":
            return 26;
        case "Crear Orden de Trabajo":
            return 27;
        case "Editar Orden de Trabajo":
            return 28;
        case "Eliminar Orden de Trabajo":
            return 29;
        case "Ver Muestra":
            return 30;
        case "Editar Muestra":
            return 31;
        case "Ver Informe de Usuario":
            return 32;
        case "Ver Rutas":
            return 33;
        default:
            return "error";
    }
}
