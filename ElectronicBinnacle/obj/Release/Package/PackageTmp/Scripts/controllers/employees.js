angular.module('bitacora.controllers.employees', [])
    .controller('IndexEmployeesCtrl', ['$scope', "$location", 'employees', 'FunctionShares', 'employeesFact', 'loadingFact',
        function ($scope, $location, employees, FunctionShares, employeesFact, loadingFact) {
            $scope.search = {
                Name: '',
                DropDown: false
            }
            init();

            
            function init() {
                $scope.employees = employees.employees;
                $scope.simplePagination = {
                    currentIndex: 0,
                    last: employees.count,
                    pagination: [],
                    top: 5,
                    low: 0
                }
                for (var i = 0; i < employees.count; i++)
                    $scope.simplePagination.pagination.push(i);
            }
            $scope.parseDate = function(mls) {
                return getDateStr(mls, '/');
            }
            $scope.goPage = function (paginationInfo, index) {
                if (paginationInfo.currentIndex == Math.min(Math.max(index, 0), paginationInfo.last - 1))
                    return;
                var nextIndex = Math.min(Math.max(index, 0), paginationInfo.last - 1);

                employeesFact.search($scope.search.Name, !$scope.search.DropDown, nextIndex + 1)
                    .success(function (session) {
                        $scope.employees = session.employees;
                        paginationInfo.currentIndex = nextIndex;
                        var top = Math.min(paginationInfo.currentIndex + 2, paginationInfo.last - 1);
                        var low = Math.max(paginationInfo.currentIndex - 2, 0);
                        paginationInfo.top = top + (2 - (paginationInfo.currentIndex - low));
                        paginationInfo.low = low - (2 - (top - paginationInfo.currentIndex));
                        loadingFact.loadingDown();
                    })
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            }
            $scope.create_employee = function() {
                $location.path('/new/employee');
            };
            $scope.filterName = function(search) {
                return function(item) {
                    var search_text = search.pattern;
                    var active = search.dropDown;
                    if (item.DropDown != active) return false;

                    var full_name = (item.Name + item.LastName).toLowerCase();
                    var words = search_text.split(' ');

                    for (var i = 0; i < words.length; i++) {
                        var word = words[i].toLowerCase();
                        if ((full_name.indexOf(word) == -1) && (full_name.indexOf(word) == -1))
                            return false;
                    };
                    return true;
                };
            };
            $scope.toogle_dropDown = function (employeeId, index) {
                if (loadingFact.isLoading()) return;
                loadingFact.loadingUp();
                employeesFact.ToogleActive(employeeId, $scope.simplePagination.currentIndex + 1, !$scope.search.DropDown, $scope.search.Name)
                    .success(function (data) {
                        $scope.employees.splice(index, 1);
                        if (data.employee)
                            $scope.employees.push(data.employee);
                        else if (!$scope.employees.length) {
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
                    .error(function () {
                        $.Notify({ style: { background: '#9A1616', color: 'white' }, caption: 'Lo sentimos...', content: "Ha ocurrido un error en el sistema." });
                        loadingFact.loadingDown();
                    });
            };
            $scope.seeToogleActive = function () {
                $scope.search.DropDown = !$scope.search.DropDown;
                $scope.searchObj();
            }
            $scope.hasPermission = function (permissionId) {
                if (!$scope.auth) return false;
                return FunctionShares.hasPermission($scope.auth.Employee.Role.Permissions, permissionId);
            };
            $scope.searchObj = function () {
                var page = 1;
                loadingFact.loadingUp();
                employeesFact.search($scope.search.Name, !$scope.search.DropDown, page)
                    .success(function (data) {
                        employees = data;
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