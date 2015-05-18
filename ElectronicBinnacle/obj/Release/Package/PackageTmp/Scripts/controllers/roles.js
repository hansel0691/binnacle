angular.module('bitacora.controllers.roles', ['ngRoute'])
    .controller('IndexRolesCtrl', [
        '$scope', "$location", 'roles', 'FunctionShares', 'rolesFact', 'loadingFact',
        function ($scope, $location, roles, FunctionShares, rolesFact, loadingFact) {
            $scope.search = { Name: "", Active: true };
            init();

            function init() {
                $scope.roles = roles.roles;
                $scope.simplePagination = {
                    currentIndex: 0,
                    last: roles.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < roles.count; i++)
                    $scope.simplePagination.pagination.push(i);
            }
            $scope.goPage = function (paginationInfo, index) {
                loadingFact.loadingUp();
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);
                rolesFact.search($scope.search.Name, $scope.search.Active, nextIndex + 1)
                    .success(function(session) {
                        $scope.roles = session.roles;
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
            }
            $scope.create_role = function() {
                if (loadingFact.isLoading()) return;
                $location.path('/new/role');
            };
            $scope.toogle_role = function(roleId) {
                if (loadingFact.isLoading()) return;
                loadingFact.loadingUp();
                rolesFact.ToogleActive(roleId, $scope.simplePagination.currentIndex + 1, $scope.search.Active, $scope.search.Name)
                    .success(function(data) {
                        var index = findIndex($scope.roles, roleId);
                        $scope.roles.splice(index, 1);
                        if (data.role)
                            $scope.roles.push(data.role);
                        else if (!$scope.roles.length) {
                            $scope.simplePagination.pagination.length--;
                            $scope.simplePagination.last--;
                            $scope.goPage($scope.simplePagination, $scope.simplePagination.currentIndex - 1);
                        }
                        if (data.last) {
                            $scope.simplePagination.pagination.length--;
                            $scope.simplePagination.last--;
                        }
                        
                        loadingFact.loadingDown();
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            };
            $scope.seeToogleActive = function() {
                $scope.search.Active = !$scope.search.Active;
                $scope.searchObj();
            }
            $scope.hasPermission = function (permissionId) {
                if (!$scope.auth) return false;
                return FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissionId);
            };
            $scope.isLoading = function() {
                return loadingFact.isLoading();
            }
            $scope.searchObj = function () {
                var page = 1;
                loadingFact.loadingUp();
                rolesFact.search($scope.search.Name, $scope.search.Active, page)
                    .success(function(data) {
                        roles = data;
                        init();
                        loadingFact.loadingReset();
                    })
                    .error(function() {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }
        }
    ]);

function findIndex(array, roleId) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].RoleId == roleId)
            return  i;
    }
    return -1;
}