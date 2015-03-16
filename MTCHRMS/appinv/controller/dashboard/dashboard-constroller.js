/// <reference path="../../module/inv-module.js" />


'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$routeParams', 'authRepository',
    function ($scope, $location, $routeParams, authRepository) {

        console.log("inventory dashboard controller");
        //$scope.myname = "yahoo";

        console.log(authRepository.authentication);

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

    }
]);
