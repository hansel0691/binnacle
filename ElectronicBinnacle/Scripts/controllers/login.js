angular.module('bitacora.controllers.login', [])
    .controller('LoginCtrl', [
        '$scope', '$routeParams', '$location', 'loginFact',  'loadingFact', '$http',
        function ($scope, $routeParams, $location, loginFact, loadingFact, $http) {
            $('#main-view').removeClass('section');
            $("#welcomeMsg").addClass("hide");
            $scope.credentials = {
                username: "",
                password: "",
                rememberMe: false,
                returnUrl: $routeParams.returnUrl
            };

            $scope.login = function () {
                if (!$scope.credentials.password)
                    $scope.credentials.password = $('#pass').val();
                if (!$scope.credentials.username)
                    $scope.credentials.username = $('#user').val();
                loadingFact.loadingUp();
                var result = loginFact($scope.credentials.username, $scope.credentials.password, $scope.credentials.rememberMe);
                result.then(function(result) {
                    if (result.success) {
                        copyProps($scope.auth, result.user);
                        $.Notify({ style: { background: '#1BA1E2', color: 'white' }, caption: 'Hola...', content: "Bienvenido de vuelta " + result.user.Employee.Name + "." });
                        $scope.logAction.logged = true;
                        $location.path('/home');
                        $("#welcomeMsg").removeClass("hide");

                        $http.get('/Orders/NewNotifications/' + $scope.auth.UserId)
                            .success(function(data) {
                                if (data)
                                    for (var i = 0; i < data.length; i++) {
                                        if (data[i].indexOf('nueva notificación') == -1)
                                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Notificaci&oacute;n del Sistema...', content: data[i] });
                                    }
                            });
                    } else {
                        $scope.credentials.password = "";
                        $.Notify({
                            style: { background: '#9A1616', color: 'white' }, caption: 'Credenciales Incorrectas...',
                            content: "Aseg&uacute;rese de escribir correctamente su nombre de usuario y contrase&ntilde;a."
                        });
                    }
                    loadingFact.loadingReset();
                });
            };
        }
    ]);

//change with ObjForJson("Auth")
function copyProps(auth, logged) {
    auth.UserId = logged.UserId;
    auth.Name = logged.Name;
    auth.Subordinates = logged.Subordinates;
    auth.Employee.Name = logged.Employee.Name;
    auth.Employee.LastName = logged.Employee.LastName;
    auth.Employee.FullName = logged.Employee.FullName;
    auth.Employee.Role.Name = logged.Employee.Role.Name;
    auth.Employee.Role.RoleId = logged.Employee.Role.RoleId;
    auth.Employee.Role.Permissions = logged.Employee.Role.Permissions;
}