/// <reference path="../../module/inv-module.js" />
/// <reference path="item-repository.js" />


'use strict';
invModule.controller('ItemController',
[
    '$scope', '$location', '$routeParams', 'itemRepository', 'validationRepository', 'locationRepository','departmentRepository',
    function ($scope, $location, $routeParams, itemRepository, validationRepository, locationRepository, departmentRepository) {

        console.log("item controller");

        $scope.isBusy = false;

        $scope.tab = 1; // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function(setTab) {
            this.tab = setTab;
        };

        // verify if tab is selected or not, use for ng-class 
        $scope.isTabSelected = function(checkTab) {
            return this.tab === checkTab;
        };

        $scope.loadDefinition = function() {

            $scope.itemTypes = validationRepository.getAllDetailsByValidationId(7);
            $scope.itemCategories = validationRepository.getAllDetailsByValidationId(8);
            $scope.storeLocations = locationRepository.getAllLocations();
            $scope.departments = departmentRepository.getAllDepartment();
            console.log($scope.storeLocations);
        };

        $scope.loadItems = function() {
            $scope.isBusy = true;
            $scope.items = itemRepository.getAllItems();

            $scope.items.$promise.then(function() {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() {

                })
                .then(function() { $scope.isBusy = false; });
        };

        $scope.save = function(item) {
            console.log(item);
            $scope.errors = [];
            itemRepository.addItem(item).$promise.then(
                function() {
//                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/INVPortal/item/list');
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    //appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.edit = function(item) {
            $scope.errors = [];
            itemRepository.editItem(item).then(
                function() {
                    // success case
                    console.log("edit done - Successfully !");
                    //appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/INVPortal/item/list');
                }, function(response) {
                    // failure case
                    console.log("edit - Error !");
                    //appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        //alert($routeParams.id);
        $scope.loadItem = function() {
            if ($routeParams.id != undefined) {
                $scope.item = itemRepository.getItemById($routeParams.id);
            }
        };


    }
]);
