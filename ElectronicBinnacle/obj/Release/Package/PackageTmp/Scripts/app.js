angular.module('bitacora.controllers', ['bitacora.controllers.employee_edition', 'bitacora.controllers.employees',
    'bitacora.controllers.home', 'bitacora.controllers.login', 'bitacora.controllers.role_edition', 'bitacora.controllers.roles',
    'bitacora.controllers.samples', 'bitacora.controllers.user_edition', 'bitacora.controllers.users',
    'bitacora.controllers.packages', 'bitacora.controllers.params', 'bitacora.controllers.orders', 'bitacora.controllers.order_edition',
    'bitacora.controllers.package_edition', 'bitacora.controllers.global', 'bitacora.controllers.package_view', 'bitacora.controllers.routes',
    'bitacora.controllers.stats', 'bitacora.controllers.employee_stats', 'bitacora.controllers.status', 'bitacora.controllers.SvEvidence', 'bitacora.controllers.SvString'
    , 'bitacora.controllers.map', 'bitacora.controllers.generalmap'
]);
var app = angular.module('bitacora', ['bitacora.controllers', 'bitacora.directives', 'bitacora.services', 'ngRoute', 'ngCookies']);


app.config([
    '$routeProvider', '$httpProvider',
    function ($routeProvider, $httpProvider) {
        $routeProvider
            .when('/home', {
                controller: 'HomeCtrl',
                templateUrl: 'partialroute/home'
            })
            .when('/login/:returnUrl', {
                controller: 'LoginCtrl',
                templateUrl: 'account/login'
            })
            .when('/users', {
                controller: 'IndexUsersCtrl',
                resolve: {
                    users: [
                        'usersFact', function (usersFact) {
                            return usersFact.AllUsers.get({ page: 1 }).$promise;
                        }
                    ]
                },
                templateUrl: 'partialroute/users'
            })
            .when('/edit/user/:id', {
                templateUrl: 'partialroute/edituser',
                controller: 'EditUserCtrl',
                resolve: {
                    user: [
                        '$q', 'usersFact', '$route', function ($q, usersFact, $route) {
                            var d = $q.defer();
                            usersFact.AllUsers.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ]
                }
            })
            .when('/roles', {
                controller: 'IndexRolesCtrl',
                resolve: {
                    roles: [
                        'rolesFact', function (rolesFact) {
                            return rolesFact.Roles.get({ page: 1, active: true }).$promise;
                        }
                    ]
                },
                templateUrl: 'partialroute/roles'
            })
            .when('/new/role', {
                controller: 'EditRoleCtrl',
                templateUrl: 'partialroute/editrole',
                resolve: {
                    role: [
                        'rolesFact', function (rolesFact) {
                            return new rolesFact.Roles({ Name: "", Active: true, Permissions: [] });
                        }
                    ],
                    permissions: [
                        'rolesFact', function (rolesFact) {
                            return rolesFact.Permissions;
                        }
                    ]
                }
            })
            .when('/edit/role/:id', {
                controller: 'EditRoleCtrl',
                templateUrl: 'partialroute/editrole',
                resolve: {
                    role: [
                        '$q', 'rolesFact', '$route', function ($q, rolesFact, $route) {
                            var d = $q.defer();
                            rolesFact.Roles.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                    permissions: [
                        'rolesFact', function (rolesFact) {
                            return rolesFact.Permissions;
                        }
                    ]
                }
            })
            .when('/employees', {
                controller: 'IndexEmployeesCtrl',
                resolve: {
                    employees: [
                        'employeesFact', function (employeesFact) {
                            return employeesFact.AllEmployees.get({page : 1}).$promise;
                        }
                    ]
                },
                templateUrl: 'partialroute/employees'
            })
            .when('/new/employee', {
                controller: 'EditEmployeeCtrl',
                templateUrl: 'partialroute/editemployee',
                resolve: {
                    employee: [
                        'employeesFact', function (employeesFact) {
                            return new employeesFact.AllEmployees(
                            {
                                Name: "",
                                LastName: "",
                                PhoneNumber: "",
                                Email: "",
                                Signature: "",
                                Speciality: "",
                                Degree: 0,
                                DropDown: "",
                                User: { IMEI: "", Name: "", Password: "", },
                                Role: { Name: "", Active: true, Permissions: [] }
                            });
                        }
                    ],
                    roles: [
                        '$q', 'rolesFact', function ($q, rolesFact) {
                            var d = $q.defer();
                            rolesFact.ActiveRoles.then(function (data) {
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    ]
                }
            })
            .when('/edit/employee/:id', {
                controller: 'EditEmployeeCtrl',
                templateUrl: 'partialroute/editemployee',
                resolve: {
                    employee: [
                        '$q', 'employeesFact', '$route', function ($q, employeesFact, $route) {
                            var d = $q.defer();
                            employeesFact.AllEmployees.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                    roles: [
                        '$q', 'rolesFact', function ($q, rolesFact) {
                            var d = $q.defer();
                            rolesFact.ActiveRoles.then(function(data) {
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    ]
                }
            })
            .when('/orders/:state', {
                controller: 'IndexOrdersCtrl',
                resolve: {
                    orders: ['ordersFact', '$route', function (ordersFact, $route) {
                        if (!$route.current.params.state)
                            return ordersFact.Orders.get({ page: 1 }).$promise;
                        return (
                            $route.current.params.state == 'sended' ? ordersFact.SendedOrders
                            : ($route.current.params.state == 'unsended' ? ordersFact.UnSendedOrders
                            : ($route.current.params.state == 'evaluated' ? ordersFact.EvaluatedOrders
                            : ordersFact.UnEvaluatedOrders))).get({ page: 1 }).$promise;
                    }]
                },
                templateUrl: 'partialroute/orders'
            })
            .when('/order/:id/samples', {
                controller: 'IndexSamplesCtrl',
                resolve: {
                    sampleInfo: [
                        '$q', 'samplesFact', '$route', function ($q, samplesFact, $route) {
                            var d = $q.defer();
                            samplesFact.SamplingPlan.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                },
                templateUrl: 'partialroute/samplesview'
            })
            .when('/new/order', {
                controller: 'EditOrderCtrl',
                templateUrl: 'partialroute/editorder',
                resolve: {
                    order: [
                        'ordersFact', function (ordersFact) {
                            return new ordersFact.Orders({
                                ClientData: {
                                    SocialReason: "",
                                    StreetNo: "",
                                    Colony: "",
                                    DelMpio: "",
                                    Edo: "",
                                    CP: "",
                                    RFC: "",
                                    BillReport: true
                                },
                                BillerClient: {
                                    SocialReason: "",
                                    StreetNo: "",
                                    Colony: "",
                                    DelMpio: "",
                                    Edo: "",
                                    CP: "",
                                    RFC: ""
                                },
                                LocationData: {
                                    Place: "",
                                    StreetNo: "",
                                    Colony: "",
                                    DelMpio: "",
                                    Edo: "",
                                    CP: "",
                                    Contact: "",
                                    Phone: "",
                                    Cellphone: "",
                                    Email: ""
                                },
                                SamplingData: {
                                    Identifier: "",
                                    StartTime: 0,
                                    EndTime: 0,
                                    Period: 0,
                                    SamplingKind: -1,
                                },
                                BinnacleData: {
                                    SocialReason: "",
                                    StreetNo: "",
                                    Colony: "",
                                    DelMpio: "",
                                    Edo: "",
                                    CP: ""
                                },
                                WorkPackages: [],
                                OrderState: 0,
                                Sampler: {},
                                Creator: {}
                            });
                        }
                    ],
                    packages: [
                        'packagesFact', function (packagesFact) {
                            return packagesFact.Packages.query().$promise;
                        }
                    ],
                    samplers: [
                        'employeesFact', function (employeesFact) {
                            return employeesFact.Samplers.query({id : -1}).$promise;
                        }
                    ]
                }
            })
            .when('/edit/order/:id', {
                controller: 'EditOrderCtrl',
                templateUrl: 'partialroute/editorder',
                resolve: {
                    order: [
                        '$q', 'ordersFact', '$route', function ($q, ordersFact, $route) {
                            var d = $q.defer();
                            ordersFact.Orders.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ]
                }
            })
            .when('/packages', {
                controller: 'IndexPackagesCtrl',
                resolve: {
                    packages: ['packagesFact', function (packagesFact) {
                        return packagesFact.Packages.get({ page: 1 }).$promise;
                    }]
                },
                templateUrl: 'partialroute/packages'
            })
            .when('/new/package', {
                controller: 'EditPackageCtrl',
                templateUrl: 'partialroute/editpackage',
                resolve: {
                    pack: [
                        'packagesFact', function (packagesFact) {
                            return new packagesFact.Packages({ Identifier: "", Parameters: [] });
                        }
                    ],
                    params: [
                        'paramsFact', function (paramsFact) {
                            return paramsFact.Parameters.query().$promise;
                        }
                    ]
                }
            })
            .when('/edit/package/:id', {
                controller: 'EditPackageCtrl',
                templateUrl: 'partialroute/editpackage',
                resolve: {
                    pack: [
                        '$q', 'packagesFact', '$route', function ($q, packagesFact, $route) {
                            var d = $q.defer();
                            packagesFact.Packages.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                    params: [
                        'paramsFact', function (paramsFact) {
                            return paramsFact.Parameters.query().$promise;
                        }
                    ]
                }
            })
            .when('/package/:id', {
                controller: 'ViewPackageCtrl',
                templateUrl: 'partialroute/package',
                resolve: {
                    pack: [
                        '$q', 'packagesFact', '$route', function ($q, packagesFact, $route) {
                            var d = $q.defer();
                            packagesFact.Packages.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                    params: [
                        'paramsFact', function (paramsFact) {
                            return paramsFact.Parameters.query().$promise;
                        }
                    ]
                }
            })
            .when('/params', {
                controller: 'IndexParamsCtrl',
                resolve: {
                    params: ['paramsFact', function (paramsFact) {
                        return paramsFact.Parameters.get({ page: 1 }).$promise;
                    }]
                },
                templateUrl: 'partialroute/params'
            })
            .when('/routes', {
                controller: 'RoutesCtrl',
                resolve: {
                    samplers: [
                        'employeesFact', function (employeesFact) {
                            return employeesFact.Samplers.query({ id: -1 }).$promise;
                        }
                    ]
                },
                templateUrl: 'partialroute/routes'
            })
            .when('/stats/', {
                controller: 'StatsCtrl',
                templateUrl: 'partialroute/stats'
            })
            .when('/stats/:id', {
                controller: 'EmployeeStatsCtrl',
                resolve: {
                    employee: [
                        '$q', 'employeesFact', '$route', function ($q, employeesFact, $route) {
                            var d = $q.defer();
                            employeesFact.AllEmployees.get({ id: $route.current.params.id }, function (session) {
                                d.resolve(session);
                            }, function (err) {
                                d.reject(err);
                            });
                            return d.promise;
                        }
                    ],
                },
                templateUrl: 'partialroute/employeestats'
            })
            .when('/generalmap/', {
                controller: 'GeneralMapCtrl',
                templateUrl: 'partialroute/generalmap'
            })
            .otherwise({
                redirectTo: '/home'
            });
        $httpProvider.interceptors.push('authResponseInterceptor');
    }
]);