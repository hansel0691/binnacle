angular.module('bitacora.controllers.employee_stats', [])
    .controller('EmployeeStatsCtrl', ['$scope', 'loadingFact', 'employee', 'employeesFact',
        function ($scope, loadingFact, employee, employeesFact) {
            loadingFact.loadingUp();
            loadingFact.loadingUp();
            $scope.employee = employee;
            $scope.orders = [];
            $scope.boos = "";
            $scope.vertion = "";

            employeesFact.boosAndVertion(employee.EmployeeId)
                .success(function (data) {
                    $scope.boos = data.boos;
                    $scope.vertion = (data.AppVertion ? data.AppVertion : "1");
                })
                .error(function () {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                .then(function () {
                    loadingFact.loadingDown();
                });
            employeesFact.orders(employee.EmployeeId)
                .success(function(data) {
                    $scope.orders = data.orders;
                })
                .error(function() {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                .then(function() {
                    loadingFact.loadingDown();
                });
            $scope.getDateTime  = function (elapsedTime) {
                return getDateTimeStr(elapsedTime, '/');
            }
            $scope.dateDif = function (order) {
                if (!order.Finished) return "No hay datos del Muestro.";
                return dateDif(order.Finished, order.EndTime);
            }
        }
    ]);