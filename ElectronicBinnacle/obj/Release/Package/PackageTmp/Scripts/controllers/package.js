angular.module('bitacora.controllers.package_view', ['ngRoute'])
    .controller('ViewPackageCtrl', [
        '$scope', 'pack', 'params', '$location',
        function ($scope, pack, params, $location) {
            $scope.package = pack;
            $scope.all_params = params;

            $scope.edit_package = function() {
                $location.path('edit/package/' + $scope.package.PackageId);
            }
        }
    ]);