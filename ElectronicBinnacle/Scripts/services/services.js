var services = angular.module('bitacora.services', ['ngResource']);

var baseUrl = 'http://localhost:2453/Home/';


services.factory('packagesFact', ['$resource', '$http', function ($resource, $http) {
    return {
        Packages: $resource('/Packages/Package/:id', { id: '@id', page: '@page' }),
        search: function (identifier, page) {
            return $http.get('/Packages/Package?searchName=' + identifier + '&page=' + page);
        }
    };
}]);

services.factory('paramsFact', ['$resource', '$http', function ($resource, $http) {
    return {
        Parameters: $resource('/Packages/Parameter/:id', { id: '@id', page: '@page' }),
        search: function (identifier, container, preserver, volume, tmpa, page) {
            var url = "/Packages/Parameter?";
            if (identifier)
                url += 'searchName=' + identifier + '&';
            if (preserver)
                url += 'preserver=' + preserver + '&';
            if (container)
                url += 'container=' + container + '&';
            if (volume != undefined)
                url += 'volume=' + volume + '&';
            if (tmpa != undefined)
                url += 'tmpa=' + tmpa + '&';
            url += '&page=' + page;
            return $http.get(url);
        }
    }
}]);

services.factory('employeesFact', ['$resource', '$http', function ($resource, $http) {
    return {
        AllEmployees: $resource("Employees/Employee", { id: '@id', page: '@page' }),
        Samplers: $resource("Employees/Sampler", { id: '@id', watterMatch: '@watterMatch' }),
        Coordinators: $resource("Employees/Coordinator", { id: '@id' }),
        ToogleActive: function (id, page, active, name) {
            return $http.post('/Employees/ToogleActiveEmployee/' + id + "/?page=" + page + '&active=' + active + '&searchName=' + name);
        },
        search: function(employeeName, active, page) {
            return $http.get('/Employees/Employee?searchName=' + employeeName + '&page=' + page + '&active=' + active);
        },
        subordinate: function (page) {
            return $http.get('/Employees/Subordinates?page=' + page);
        },
        orders : function(employeeId) {
            return $http.get('/Employees/Orders?employeeId=' + employeeId);
        },
        boosAndVertion : function(employeeId) {
            return $http.get('/Employees/BoosAndVertion?employeeId=' + employeeId);
        },
        searchPath : function(employeeId, searchDate) {
            return $http.get('/Employees/Path?employeeId=' + employeeId + '&date=' + searchDate);
        },
        getLastPositions: function (employeeId, searchDate, lastId) {
            return $http.get('/Employees/LastPositions?employeeId=' + employeeId + '&lastId=' + lastId + '&date=' + searchDate);
        }
    };
}]);

services.factory('rolesFact', ['$resource', '$http', function ($resource, $http) {
    return {
        Roles: $resource('/Employees/Role/:id', { id: '@id', page: '@page', active : '@Active' }),
        Permissions: $http.get('/Employees/Permissions'),
        ActiveRoles: $http.get('/Employees/ActiveRoles?active'),
        ToogleActive: function(id, page, active, name) {
            return $http.post('/Employees/ToogleActiveRole/' + id + "/?page=" + page + '&active=' + active + '&searchName=' + name);
        },
        search: function (roleName, active, page) {
            return $http.get('/Employees/Role?searchName=' + roleName + '&page=' + page  + '&active=' + active);
        }
    };
}]);

services.factory('usersFact', ['$resource', '$http', function ($resource, $http) {
    return {
        AllUsers: $resource('/Employees/User/:id', { id: '@id', page: '@page', searchName: null }),
        search : function(userName, page) {
            return $http.get('/Employees/User?searchName=' + userName + '&page=' + page);
        },
        currentPositions : function() {
            return $http.get('/Orders/UserPosition');
        },
        currentPosition: function (employeeId) {
            return $http.get('/Orders/CurrentPosition?employeeId=' + employeeId);
        }
    };
}]);

services.factory('samplesFact', ['$resource', '$http', function ($resource, $http) {
    return {
        //Samples: $resource('/Orders/Sample/:id', { id: '@id' }),
        SamplingOrder: function(id) { return $http.get('/Orders/SamplingOrder/' + id);},
        SamplingPlan: $resource('/Orders/SamplingPlan/:id', { id: '@id' }),
        SimpleSample: function(id, page) { return $http.get('/Orders/SimpleSample/' + id + '?page=' + page);},
        ComplexSample: function (id, page) { return $http.get('/Orders/ComplexSample/' + id + '?page=' + page); },
        SamplingString: function (id) { return $http.get('/Orders/SamplingString/' + id); },
        Croquis: function(sampleId, croquisId) {
            return $http.get('/Orders/Croquis?sampleId=' + sampleId + '&croquisId=' + croquisId);
        },
        Photos: function (id, photoId) {
            return $http.get('/Orders/Photos/' + id + '?photo=' + photoId);
        },
        setSamplingState: function (id, state) { return $http.post('/Orders/SetSamplingState/', { id: id, state: state }); },
        setReceivedAmount: function (id, samplingIdentifierId, amount, samplingType) { return $http.post('/Orders/SetReceivedAmount/', { id: id, samplingIdentifierId: samplingIdentifierId, amount: amount, samplingType: samplingType }); },
        setLabNo: function (id, samplingIdentifierId, amount, samplingType) { return $http.post('/Orders/SetLabNo/', { id: id, samplingIdentifierId: samplingIdentifierId, amount: amount, samplingType: samplingType }); },
        removePhoto : function (photoId) {
            return $http.post('/Orders/RemovePhoto/', { photoId: photoId });
        }
    };
}]);

services.factory('ordersFact', ['$resource', '$http', function ($resource, $http) {
    return {
        Orders: $resource('/Orders/Order/:id', { id: '@id', relatedCount: '@relatedCount', currentCount: '@currentCount' }),
        SendedOrders: $resource('/Orders/Order/:id', { id: '@id', relatedCount: '@relatedCount', currentCount: '@currentCount', sended : true, unsended : false, evaluated : false, unevaluated : false, unfinished : false, uncomplete : false}),
        UnSendedOrders: $resource('/Orders/Order/:id', { id: '@id', relatedCount: '@relatedCount', currentCount: '@currentCount', sended: false, unsended: true, evaluated: false, unevaluated: false, unfinished: false, uncomplete: false }),
        EvaluatedOrders: $resource('/Orders/Order/:id', { id: '@id', relatedCount: '@relatedCount', currentCount: '@currentCount', sended: false, unsended: false, evaluated: true, unevaluated: false, unfinished: false, uncomplete: false }),
        UnEvaluatedOrders: $resource('/Orders/Order/:id', { id: '@id', relatedCount: '@relatedCount', currentCount: '@currentCount', sended: false, unsended: false, evaluated: false, unevaluated: true, unfinished: false, uncomplete: false }),
        search :  function(identifier, sended, unsended, evaluated, unevaluated, unfinished, uncomplete, socialReason, place, rfc, startDate, endDate, page) {
            var url = "/Orders/Order?";
            if (identifier)
                url += 'identifier=' + identifier + '&';
            url += 'sended=' + sended + '&';
            url += 'unsended=' + unsended + '&';
            url += 'evaluated=' + evaluated + '&';
            url += 'unevaluated=' + unevaluated + '&';
            url += 'unfinished=' + unfinished + '&';
            url += 'uncomplete=' + uncomplete + '&';

            if (socialReason)
                url += 'socialReason=' + socialReason + '&';
            if (place)
                url += 'place=' + place + '&';
            if (rfc)
                url += 'rfc=' + rfc + '&';
            if (startDate)
                url += 'startDate=' + startDate + '&';
            if (endDate)
                url += 'endDate=' + endDate + '&';

            url += '&page=' + page;
            return $http.get(url);
        },
        generateExel: function (orderId, exportTo) {
            return $http.post('/Orders/GenerateExel/' + orderId + '?exportTo=' + exportTo);
        },
        newOrders : []
    }
}]);

services.factory('FunctionShares', ['$http', function ($http) {
    return {
        isRelatedData: function(roleId, array, others) {
            return array.indexOf(roleId) != -1 || (others && roleId >= 6);
        },
        hasPermission: function(rolePermissions, permission) {
            if (!rolePermissions)return false;
            for (var i = 0; i < rolePermissions.length; i++) {
                if (rolePermissions[i].Identifier == permission)
                    return true;
            }
            return false;
        },
        removeParam: function (parameterId, page) {
            //this is here because i don wanna change the param controller. To lazy to do that.
            return $http.post('/Packages/RemoveParameter/' + parameterId + '/?page=' + page);
        },
        removePack: function (packId, page) {
            //this is here because i don wanna change the param controller. To lazy to do that.
            return $http.post('/Packages/RemovePackage/' + packId + '/?page=' + page);
        }
    };
}]);

services.factory('notificationFact', ['$http', function($http) {
        return {
            Notifications: function (notificationId, page) {
                if (!notificationId)
                    return $http.get('/Orders/Notification?date=' + todayDate() + '&page=' + page);
                return $http.get('/Orders/Notification?id=' + notificationId);
            },
            search: function (notificationText, samplerId, type, date, page) {
                var url = '/Orders/Notification?';
                if (notificationText)
                    url += 'notificationText=' + notificationText + '&';
                if (samplerId != 0)
                    url += 'samplerId=' + samplerId + '&';
                if (type != 0)
                    url += 'type=' + type + '&';
                if (date != 0 && date != 0)
                    url += 'date=' + date + '&';
                else
                    url += 'date=' + todayDate() + '&';
                url += 'page=' + page;
                return $http.get(url);
            }
        };
    }]);

services.factory('authResponseInterceptor', [ '$q', '$location', function($q, $location) {
        return {
            response: function (response) {
                if (response.status === 401) {
                    console.log("Response 401");
                }
                return response || $q.when(response);
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    $location.path('/login' + $location.path());
                }
                return $q.reject(rejection);
            }
        }
    }]);

services.factory('loginFact', ['$http', '$q', function ($http, $q) {
        return function (userName, password, rememberMe) {
            var deferredObject = $q.defer();

            $http.post(
                '/Account/Login', {
                    UserName: userName,
                    Password: password,
                    RememberMe: rememberMe
                }
            ).
            success(function (data) {
                if (data.success == true) {
                    deferredObject.resolve({ success: true, user : data.user });
                } else {
                    deferredObject.resolve({ success: false });
                }
            }).
            error(function () {
                deferredObject.resolve({ success: false });
            });

            return deferredObject.promise;
        }
    }]);

services.factory('authenticationFact', ['$http', '$q', function($http, $q) {
        return {
            authenticatedUser: function (withOrderCount) {
                return $http.get('/Account/AuthUser?withOrderCount=' + withOrderCount);
            },
            logout: function() {
                $http.post('/Account/LogOff');
            },
            login: function(userName, password, rememberMe) {
                var deferredObject = $q.defer();

                $http.post(
                        '/Account/Login', {
                            UserName: userName,
                            Password: password,
                            RememberMe: rememberMe
                        }
                    ).
                    success(function(data) {
                        if (data.success == true) {
                            deferredObject.resolve({ success: true, user: data.user });
                        } else {
                            deferredObject.resolve({ success: false });
                        }
                    }).
                    error(function() {
                        deferredObject.resolve({ success: false });
                    });

                return deferredObject.promise;
            }
        }
    }]);

services.factory('loadingFact', [
    '$rootScope', function($rootScope) {
        return {
            loadingReset: function() {
                $rootScope.isLoading = 0;
            },
            loadingUp: function() {
                $rootScope.isLoading++;
            },
            loadingDown: function() {
                $rootScope.isLoading--;
                if ($rootScope.isLoading < 0)
                    $rootScope.isLoading = 0;
            },
            isLoading: function() {
                return $rootScope.isLoading > 0;
            }
        };
    }
]);

function todayDate() {
    var now = new Date(Date.now());
    var day = now.getDate();
    var month = now.getMonth();
    var year = now.getFullYear();
    return new Date(year, month, day).getTime();
}