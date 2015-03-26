angular.module('toggle-switch', ['ng']).directive('toggleSwitch', ['$compile', function($compile) {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            model: '=',
            isDisabled: '@',
            onLabel: '@',
            offLabel: '@',
            knobLabel: '@',
            html: '=',
            onChange: '&'
        },
        template: '<div class="ats-switch" ng-click="toggle()" ng-class="{ \'disabled\': isDisabled }"><div class="switch-animate" ng-class="{\'switch-off\': !model, \'switch-on\': model}"><span class="switch-left"></span><span class="knob"></span><span class="switch-right"></span></div></div>',
        controller: ['$scope', function($scope) {
            $scope.toggle = function toggle() {
                if (angular.isUndefined($scope.model)) {
                    $scope.model = false;
                }
                if (!$scope.isDisabled) {
                    $scope.model = !$scope.model;
                    //$scope.model = $scope.model ? "1" : "0";
                    //alert($scope.model);
                }
                $scope.onChange();
            };
        }],
        compile: function(element, attrs) {
            if (angular.isUndefined(attrs.onLabel)) {
                attrs.onLabel = 'On';
            }
            if (angular.isUndefined(attrs.offLabel)) {
                attrs.offLabel = 'Off';
            }
            if (angular.isUndefined(attrs.knobLabel)) {
                attrs.knobLabel = '\u00a0';
            }
            if (angular.isUndefined(attrs.isDisabled)) {
                attrs.isDisabled = false;
            }
            if (angular.isUndefined(attrs.html)) {
                attrs.html = false;
            }

            return function postLink(scope, iElement, iAttrs, controller) {

                var bindSpan = function(span, html) {
                    span = angular.element(span);
                    var bindAttributeName = (html === true) ? 'ng-bind-html' : 'ng-bind';

                    // remove old ng-bind attributes
                    span.removeAttr('ng-bind-html');
                    span.removeAttr('ng-bind');

                    if (angular.element(span).hasClass("switch-left"))
                        span.attr(bindAttributeName, 'onLabel');
                    if (span.hasClass("knob"))
                        span.attr(bindAttributeName, 'knobLabel');
                    if (span.hasClass("switch-right"))
                        span.attr(bindAttributeName, 'offLabel');

                    $compile(span)(scope, function(cloned, scope) {
                        span.replaceWith(cloned);
                    });
                };
                //alert(iAttrs.ngFalseValue);
                // add ng-bind attribute to each span element.
                // NOTE: you need angular-sanitize to use ng-bind-html
                var bindSwitch = function (iElement, html) {
                    //console.log(iElement);
                    //alert(html);
                    angular.forEach(iElement[0].children[0].children, function(span, index) {
                        bindSpan(span, html);
                    });
                };

                scope.$watch('html', function (newValue) {
                    //alert(newValue);
                    bindSwitch(iElement, newValue);
                });
            };
        }
    };
}]);