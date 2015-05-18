angular.module('bitacora.controllers.users', [])
    .controller('IndexUsersCtrl', [
        '$scope', 'users', 'FunctionShares', 'usersFact', 'loadingFact',
        function ($scope, users, FunctionShares, usersFact, loadingFact) {
            $scope.search = { Name: "" }
            init();

            function init() {
                $scope.users = users.users;
                $scope.simplePagination = {
                    currentIndex: 0,
                    last: users.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < users.count; i++)
                    $scope.simplePagination.pagination.push(i);
            }
            $scope.goPage = function (paginationInfo, index) {
                loadingFact.loadingUp();
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);

                usersFact.search($scope.search.Name, nextIndex + 1)
                    .success(function(session) {
                        $scope.users = session.users;
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
            $scope.hasPermission = function (permissionId) {
                if (!$scope.auth) return false;
                return FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissionId);
            };
            $scope.searchOj = function() {
                var page = 1;
                loadingFact.loadingUp();
                usersFact.search($scope.search.Name, page )
                    .success(function(data) {
                        users = data;
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