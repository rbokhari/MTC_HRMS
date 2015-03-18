/// <reference path="../../module/inv-module.js" />


'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$window','$routeParams', 'authRepository',
    function ($scope, $location, $window, $routeParams, authRepository) {

        console.log("inventory dashboard controller");
        //$scope.myname = "yahoo";
        console.log($scope.authentication);

        $scope.isActiveNavigation = function (viewLocation) {
            //alert(viewLocation === $location.path());
            var active = (viewLocation === $location.path());
            return active;
        };

        $scope.isActiveParentNavigation = function (viewLocation) {
            //alert($location.path().indexOf(viewLocation));
            var active = ($location.path().indexOf(viewLocation) > -1);
            return active;
        };

        if ($scope.authentication.moduleId == 1 && $scope.authentication.roleId > 1) {
            $window.location.href = '/HRMSPortal';
        }

    }
]);
