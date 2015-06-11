/// <reference path="../../module/inv-module.js" />


'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$window', '$routeParams', 'authRepository', 'supplierRepository', 'itemRepository',
        'locationRepository', 'manufacturerRepository', 'validationRepository','departmentRepository',

    function ($scope, $location, $window, $routeParams, authRepository, supplierRepository, itemRepository,
        locationRepository, manufacturerRepository, validationRepository, departmentRepository) {

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

        $scope.loadDasboard = function () {

            $scope.itemSuppliers = itemRepository.getAllItemSuppliers();
            $scope.itemManufacturers = itemRepository.getAllItemManufactuers();
            $scope.itemYears = itemRepository.getAllItemYears();
            $scope.itemDepartments = itemRepository.getAllItemDepartments();

            $scope.items = itemRepository.getAllItems();
            $scope.suppliers = supplierRepository.getAllSuppliers();
            $scope.locations = locationRepository.getAllLocations();
            $scope.manufacturers = manufacturerRepository.getAllManufacturers();
            $scope.itemTypes = validationRepository.getItemTypes;
            $scope.itemCategories = validationRepository.getItemCategories;
            $scope.departments = departmentRepository.getAllDepartment();


            //$scope.itemTechnicians = validationRepository.getItemTechnicians;
            
        };
    }
]);
