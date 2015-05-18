angular.module('bitacora.controllers.user_edition', ['ngRoute'])
    .controller('EditUserCtrl', [
        '$q', '$scope', '$routeParams', "$location", 'user', 'FunctionShares', 'employeesFact', 'loadingFact', '$http',
        function ($q, $scope, $routeParams, $location, user, FunctionShares, employeesFact, loadingFact, $http) {
            $scope.user = user;
            $scope.password = user.Password;
            $scope.repeat_password = user.Password;
            $scope.subordinates = [];
            init();


            function init() {
                if ($scope.user.Employee.Role.RoleId == 1 || $scope.user.Employee.Role.RoleId == 2 || $scope.user.Employee.Role.RoleId == 5) return;
                var employees = [];
                var resource = {};
                if ($scope.user.Employee.Role.RoleId == 4)
                    resource = employeesFact.Samplers;
                if ($scope.user.Employee.Role.RoleId == 3)
                    resource = employeesFact.Coordinators;
                if ($scope.user.Employee.Role.RoleId >= 6)
                    resource = employeesFact.AllEmployees;

                employees = resource.query(function() {
                    for (var i = 0; i < employees.length; i++) {
                        var e = employees[i];
                        $scope.subordinates.push({
                            employee: {
                                EmployeeId : e.EmployeeId,
                                Name: e.Name,
                                LastName: e.LastName,
                                Role: { RoleId: e.Role.RoleId, Name: e.Role.Name },
                                User: { Name: e.User.Name }
                            },
                            isSub: $scope.user.Subordinates.some(function (element) { return element.EmployeeId == e.EmployeeId; })
                        });
                    }
                });
                
            }

            $scope.edit_user = function () {
                $http.get('/Employees/CheckImei?imei=' + $scope.user.IMEI + '&id=' + $scope.user.UserId)
                    .success(function (data) {
                        if (!data || !data.result) {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "El imei esta siendo usado por otro usuario activo del sistema." });
                            return;
                        }
                        if (!validateUser()) return;

                        loadingFact.loadingUp();
                        $scope.user.Subordinates = $scope.subordinates.filter(function (element) { return element.isSub; }).map(function (element) { return element.employee; });
                        var u = jQuery.extend({}, $scope.user);
                        u.$save(function (data, headers) {
                            if (data.success && data.success == true) {
                                $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Usuario Editado...', content: "Se ha actualizado un usuario satifactoriamente." });
                                $location.path('/users');
                            } else {
                                if (!data.error)
                                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                                else
                                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Nombre de Usuario Incorrecto...', content: data.error });
                                loadingFact.loadingReset();
                            }
                        });
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        result = false;
                    });
            };
            $scope.isRelatedData = function (array, others) {
                if (!$scope.user || !$scope.user.Employee || !$scope.user.Employee.Role) return false;
                return FunctionShares.isRelatedData($scope.user.Employee.Role.RoleId, array, others);
            };
            $scope.hasPermission = function (permissionId) {
                if (!$scope.user || !$scope.user.Employee || !$scope.user.Employee.Role) return false;
                return FunctionShares.hasPermission($scope.user.Employee.Role.Permissions, permissionId);
            };
            $scope.isLoading = function () {
                return loadingFact.isLoading();
            }
            function validateUser() {
                if ($scope.user.Employee.Role.RoleId != 5)
                {
                    if (!$scope.user.Name) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Nombre de Usuario del empleado" });
                        return false;
                    }
                    if ($scope.password == $scope.repeat_password)
                        $scope.user.Password = $scope.password;
                    else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Error...', content: "Las contrase&ntilde;as no coinciden." });
                        $scope.password = user.Password;
                        $scope.repeat_password = user.Password;
                        return false;
                    }
                    if (($scope.user.Employee.Role.RoleId == 3 || $scope.user.Employee.Role.RoleId == 4) && !$scope.user.SamplingIdentifier) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Identificador de Muestreo del empleado" });
                        return false;
                    }
                }
                else{
                    if (!$scope.user.IMEI) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el IMEI del usuario." });
                        return false;
                    }
                    if (!$scope.user.BinnacleIdentifier) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Identificador de Bit&aacute;cora del usuario" });
                        return false;
                    }
                    if (!$scope.user.Job) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Puesto del usuario." });
                        return false;
                    }
                    if (!$scope.user.Category) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca la Categor&iacute;a del usuario." });
                        return false;
                    }
                    if (!$scope.user.Subsidiary) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca la Sucursal del usuario." });
                        return false;
                    }
                }
                return true;
            }
        }
    ]);