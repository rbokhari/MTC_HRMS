/// <reference path="../../module/inv-module.js" />


'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$window','$routeParams', 'authRepository','supplierRepository', 'itemRepository','locationRepository','manufacturerRepository',
    function ($scope, $location, $window, $routeParams, authRepository, supplierRepository, itemRepository, locationRepository, manufacturerRepository) {

        console.log("inventory dashboard controller");

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

        $scope.loadDasboard = function() {
            $scope.items = itemRepository.getAllItems();
            $scope.suppliers = supplierRepository.getAllSuppliers();
            $scope.locations = locationRepository.getAllLocations();
            $scope.manufacturers = manufacturerRepository.getAllManufacturers();
        };
    }
]);
