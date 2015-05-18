var directives = angular.module('bitacora.directives', [])
    .directive('autofocus',
        function() {
            return {
                link: function(scope, element, attrs) {
                    element[0].focus();
                }
            };
        })
    .directive('parseDate',
        function() {
            return {
                link: function (scope, element, attrs) {
                    var number = attrs.parseDate;
                    if (number == "0") {
                        element.text("00/00/0000");
                        return;
                    }
                    var pDate = new Date(parseInt(number));
                    element.text(pDate.getDate() + '/' + (pDate.getMonth() + 1) + '/' + pDate.getFullYear());
                }
            };
        })
    .directive('datePicker',
        function () {
            return {
                restrict: 'A',
                require: '?ngModel',
                link: function(scope, element, attrs, ngModel) {
                    if (!ngModel) {
                        console.log('no model, returning');
                        return;
                    }
                    $('.calendar').bind('dateChangeEvent', function () {
                        console.log('datetime changed: ', $('#datepicker-section input').val());
                        scope.$apply(read);
                    });
                    function read() {
                        ngModel.$setViewValue(element.val());
                    }
                }
            };
        })
    .directive('butterbar', [ '$rootScope',
        function($rootScope) {
            return {
                link: function(scope, element, attrs) {
                    $rootScope.$on('$routeChangeStart', function() {
                        $rootScope.isLoading++;
                    });
                    $rootScope.$on('$routeChangeSuccess', function() {
                        $rootScope.isLoading = 0;
                    });
                }
            };
        }
    ])
    .directive('ngBlur',
        function() {
            return function (scope, elem, attrs) {
                elem.bind('blur', function () {
                    scope.$apply(attrs.ngBlur);
                });
            };
        })
    .directive('back',
        function() {
            return {
                link: function(scope, element, attrs) {
                    element.on('click', function() {
                        $window.history.back(1);
                    });
                }
            }
        });