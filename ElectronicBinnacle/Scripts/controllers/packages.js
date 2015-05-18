angular.module('bitacora.controllers.packages', ['ngRoute'])
    .controller('IndexPackagesCtrl', [
        '$scope', "$location", 'packages', 'packagesFact', 'FunctionShares', 'loadingFact',
        function ($scope, $location, packages, packagesFact, FunctionShares, loadingFact) {
            $scope.search = {
                Identifier: ""
            }
            init();

            function init() {
                if (packages.$resolved && !packages.packs.length)
                    $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No existen grupos en la base de datos!" });

                $scope.packages = packages.packs;
                $scope.simplePagination = {
                    currentIndex: 0,
                    last: packages.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < packages.count; i++)
                    $scope.simplePagination.pagination.push(i);
            }
            $scope.goPage = function(paginationInfo, index) {
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);

                packagesFact.search($scope.search.Identifier, nextIndex + 1)
                    .success(function (session) {
                        $scope.packages = session.packs;
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
            $scope.create_package = function() {
                $location.path('/new/package');
            };
            $scope.remove_pack = function (id, name, index) {
                //if (index >= $scope.packages.length || !$scope.isEnable) return;
                loadingFact.loadingUp($scope);
                FunctionShares.removePack(id, $scope.simplePagination.currentIndex + 1)
                    .success(function(data) {
                        if (data.success && data.success == true) {
                            if (data.notPass) {
                                $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Eliminaci&oacute;n Fallida...', content: data.error });
                                loadingFact.loadingDown();
                                return;
                            }

                            $.Notify({ style: { background: '#78AA1C', color: 'white' }, caption: 'Grupo Eliminado...', content: name + " se ha eliminado correctamente." });
                            $scope.packages.splice(index, 1);
                            
                            if (data.pack)
                                $scope.packages.push(data.pack);
                            else if (!$scope.packages.length) {
                                $scope.simplePagination.pagination.length--;
                                $scope.simplePagination.last--;
                                $scope.goPage($scope.simplePagination, $scope.simplePagination.currentIndex - 1);
                            }
                            if (data.last) {
                                $scope.simplePagination.pagination.length--;
                                $scope.simplePagination.last--;
                            }
                        }
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                    })
                    .then(function() {
                        loadingFact.loadingDown();
                        if (!$scope.packages.length)
                            $.Notify({ style: { background: '#1ba1e2', color: 'white' }, caption: 'Informaci&oacute;n...', content: "No existen grupos en la base de datos!" });
                    });
            }
            $scope.hasPermissionAnd = function(permissions) {
                if (!$scope.auth) return false;
                for (var i = 0; i < permissions.length; i++) {
                    if (!FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissions[i]))
                        return false;
                }
                return true;
            };
            $scope.editPack = function (id, index) {
                if (index >= $scope.packages.length || loadingFact.isLoading()) return;
                $location.path('/edit/package/' + id);
            }
            $scope.isLoading = function () {
                return loadingFact.isLoading();
            }
            $scope.searchObj = function () {
                var page = 1;
                loadingFact.loadingUp();
                packagesFact.search($scope.search.Identifier, page)
                    .success(function (data) {
                        packages = data;
                        init();
                        loadingFact.loadingReset();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }
        }
    ]);