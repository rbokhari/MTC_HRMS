/// <reference path="../../module/inv-module.js" />


'use strict';
invModule.controller('DashboardController',
[
    '$scope', '$location', '$window', '$routeParams', 'authRepository', 'supplierRepository', 'itemRepository',
        'locationRepository', 'manufacturerRepository', 'validationRepository','departmentRepository',

    function ($scope, $location, $window, $routeParams, authRepository, supplierRepository, itemRepository,
        locationRepository, manufacturerRepository, validationRepository, departmentRepository) {

        $scope.isItemType1 = false;

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

        $scope.serialItem = "";
        $scope.getItemBySerial = function (event) {
            console.log(event.which);
            if ($scope.serialItem !== "" && (event.which === 13 || event.which===1 )) {
                itemRepository.getItemBySerialNo($scope.serialItem).query()
                    .$promise
                    .then(function (response) {

                        console.log(response);
                        console.log(response[0].itemId);
                        if (response.length===1) {
                            $location.url('/INVPortal/item/detail/' + response[0].itemId + '/distribution/' + response[0].stockSerialId);
                        }
                        else {
                            $location.url('/INVPortal/distribution/serials/' + $scope.serialItem);
                        }

                    }, function (err) {

                    });
            }
        };


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
