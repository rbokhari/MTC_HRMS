/// <reference path="../../module/hrms-module.js" />

'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$routeParams',
    function($scope, $location, $routeParams) {

        console.log("inventory dashboard controller");
        //$scope.myname = "yahoo";
      

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
