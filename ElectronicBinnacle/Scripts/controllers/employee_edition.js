angular.module('bitacora.controllers.employee_edition', ['ngRoute'])
    .controller('EditEmployeeCtrl', ['$scope', "$location", '$routeParams', 'employee', 'roles', 'FunctionShares', 'loadingFact', '$http',
        function ($scope, $location, $routeParams, employee, roles, FunctionShares, loadingFact, $http) {
            $scope.pageInfo = {};
            $scope.employee = employee;
            $scope.password = employee.User.Password;
            $scope.repeat_password = employee.User.Password;
            $scope.roles = roles;
            $scope.defaultSign = "iVBORw0KGgoAAAANSUhEUgAAAPAAAACGCAIAAABsTJAZAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAWhJREFUeF7t0gENAAAMw6D7FzsN99GABm4QIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQpQpMiNClCkyI0KUKTIjQh2wOgVnTDHGFlHwAAAABJRU5ErkJggg==";
            $scope.employeeInfo = {
                samplingTypes: [{ name: 'AgP', value: false, kind: 0 }, { name: 'AgR', value: false, kind: 1 }, { name: 'AgN', value: false, kind: 2 }, { name: 'AgS', value: false, kind: 3 }, { name: 'AgEst', value: false, kind: 4 }, { name: 'AgMar', value: false, kind: 5 }],
            }
            init();
            

            var id = $routeParams.id;
            if (id == undefined) {
                $scope.pageInfo.headerTitle = "Crear";
                $scope.employee.Role.RoleId = 2;
            }
            else
                $scope.pageInfo.headerTitle = "Editar";


            $scope.accept = function () {
                $http.get('/Employees/CheckImei?imei=' + $scope.employee.User.IMEI + '&id=' + $scope.employee.User.UserId)
                        .success(function (data) {
                            if (!data || !data.result) {
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "El imei esta siendo usado por otro usuario activo del sistema." });
                                return;
                            }
                            if (!validate()) return;

                            loadingFact.loadingUp();
                            var types = $scope.employeeInfo.samplingTypes;

                            $scope.employee.User.WatterTypes = [];
                            for (var i = 0; i < types.length; i++) {
                                if (types[i].value)
                                    $scope.employee.User.WatterTypes.push({ SampleKind: types[i].kind });
                            }
                            var e = jQuery.extend({}, $scope.employee);
                            e.$save(function (data, headers) {
                                if (data.success && data.success == true) {
                                    if (id == undefined)
                                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Nuevo Empleado...', content: "Se ha a&ntilde;adido un usuario satisfactoriamente." });
                                    else
                                        $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Empleado Editado...', content: "Se ha editado un usuario satisfactoriamente." });
                                    $location.path('/employees');
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
            }
            $scope.isRelatedData = function (array, others) {
                if ($scope.employee.Role == undefined) return false;
                return FunctionShares.isRelatedData($scope.employee.Role.RoleId, array, others);
            };
            $scope.hasPermission = function (permissionId) {
                if ($scope.employee.Role == undefined) return false;
                var role = $scope.roles.filter(function (r) { return r.RoleId == $scope.employee.Role.RoleId; });
                if (role.length == 0 || role[0] == undefined) return false;
                return FunctionShares.hasPermission(role[0].Permissions, permissionId);
            };
            $scope.isLoading = function () {
                return loadingFact.isLoading();
            }
            $scope.onSignSelect = function () {
                $("#signature").click();
            }
            $scope.uploadFile = function (files) {
                var fd = new FormData();
                fd.append("file", files[0]);

                $http.post("Employees/UploadSign", fd, {
                    withCredentials: true,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                }).success(function (data) {
                    if (data.success)
                        $scope.employee.Signature = data.image;
                    else
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    });
            }

            function validate() {
                if ($scope.employee.Role.RoleId == undefined) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Estacoja el role del empleado." });
                    return false;
                }
                if (!$scope.employee.Name || !$scope.employee.LastName) {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Nombre y Apellido del empleado." });
                    return false;
                }
                if ($scope.employee.Role.RoleId != 5) {
                    if (!$scope.employee.User.Name) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Nombre de Usuario del empleado" });
                        return false;
                    }
                    if ($scope.password && $scope.repeat_password && $scope.password == $scope.repeat_password)
                        $scope.employee.User.Password = $scope.password;
                    else {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Error...', content: "Las contrase&ntilde;as no coinciden." });
                        $scope.password = "";
                        $scope.repeat_password = "";
                        return false;
                    }
                    if (($scope.employee.Role.RoleId == 3 || $scope.employee.Role.RoleId == 4) && !$scope.employee.User.SamplingIdentifier) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Identificador de Muestreo del empleado" });
                        return false;
                    }
                }
                else {
                    if (!$scope.employee.User.IMEI) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el IMEI del empleado." });
                        return false;
                    }
                    if (!$scope.employee.User.BinnacleIdentifier) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Identificador de Bit&aacute;cora del empleado" });
                        return false;
                    }
                    if (!$scope.employee.User.Job) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca el Puesto del empleado." });
                        return false;
                    }
                    if (!$scope.employee.User.Category) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca la Categor&iacute;a del empleado." });
                        return false;
                    }
                    if (!$scope.employee.User.Subsidiary) {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Informaci&oacute;n...', content: "Introduzca la Sucursal del empleado." });
                        return false;
                    }
                }
                return true;
            }
            function init() {
                if ($scope.employee.User.WatterTypes) {
                    var types = $scope.employee.User.WatterTypes;
                    for (var i = 0; i < types.length; i++) {
                        $scope.employeeInfo.samplingTypes.filter(function (e) { return e.kind == types[i].SampleKind; })[0].value = true;
                    }
                }
                //loadingFact signature   
                if ($scope.employee.EmployeeId)
                    $http.get('Employees/DownloadSign?employeeId=' + $scope.employee.EmployeeId )
                        .success(function(data) {
                            if (data.success)
                                $scope.employee.Signature = data.Signature;
                            else
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                        })
                        .error(function(data) {
                            $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });

                        });
            }
        }
    ]);