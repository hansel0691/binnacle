angular.module('bitacora.controllers.stats', [])
    .controller('StatsCtrl', ['$scope', 'loadingFact', 'employeesFact',
        function ($scope, loadingFact, employeesFact) {
            loadingFact.loadingUp();
            $scope.search = {
                Name: "",
            }
            $scope.subordinates = [];
            employeesFact.subordinate(1)
                .success(function(data) {
                    $scope.subordinates = data.subordinates;
                })
                .error(function() {
                    $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                })
                .then(function() {
                    loadingFact.loadingReset();
                });
        }
    ]);