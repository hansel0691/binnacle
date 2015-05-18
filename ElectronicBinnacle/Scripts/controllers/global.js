angular.module('bitacora.controllers.global', [])
    .controller('GlobalCtr', [
        '$scope', 'authenticationFact', '$location', 'FunctionShares', 'loadingFact', '$http', '$rootScope', '$cookies',
        function ($scope, authenticationFact, $location, FunctionShares, loadingFact, $http, $rootScope, $cookies) {
            loadingFact.loadingUp();
            $scope.auth = { UserId: 0 };
            $scope.minHeight = Math.max(window.innerHeight, 839) - 39;
            $rootScope.newNotifications = 0;
            

            setInterval(function() {
                if ($scope.auth.UserId)
                    $http.get('/Orders/NewNotifications/' + $scope.auth.UserId)
                        .success(function(data) {
                            if (data)
                                for (var i = 0; i < data.length; i++) {
                                    $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Notificaci&oacute;n del Sistema...', content: data[i] });
                                    if (data[i].indexOf('nueva notificación') != -1)
                                        $rootScope.newNotifications++;
                                }
                        });
            }, 10000);

            authenticationFact.authenticatedUser().
                then(function(result) {
                    if (result.data.authenticated) {
                        $scope.auth = result.data.user;
                        $scope.logAction = {
                            logged: true
                        };
                    } else {
                        $scope.auth = { Employee: { Role: { Permissions: [] } } };
                        $scope.logAction = {
                            logged: false
                        };
                    }
                loadingFact.loadingDown();
            });
            $scope.logout = function() {
                authenticationFact.logout();
                $scope.auth = { Employee: { Role: { Permissions: [] } } };
                $scope.logAction = {
                    logged: false
                };
                $.Notify({ style: { background: '#1BA1E2', color: 'white' }, caption: 'Adi&oacute;s...', content: "Que tenga un buen d&iacute;a."});
                $location.path('/');
            }
            $scope.hasPermission = function (permissions) {
                if (!$scope.logAction || !$scope.logAction.logged)
                    return false;
                for (var i = 0; i < permissions.length; i++) {
                    if (FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissions[i]))
                        return true;
                }
                return false;
            }
            $scope.isLoading = function() {
                return loadingFact.isLoading();
            };
            $scope.goToUrl = function (url) {
                $location.path(url);
            }
            $scope.getDate = function (elapsedTime, separator) {
                return getDateStr(elapsedTime, '/');
            }
            $scope.getTime = function (elapsedTime) {
                return getTimeStr(elapsedTime);
            }
            $scope.getDateTime = function (elapsedTime, separator) {
                return getDateTimeStr(elapsedTime, separator);
            }

            $scope.goBack = function () {
                window.history.back();
            }
        }
    ]);