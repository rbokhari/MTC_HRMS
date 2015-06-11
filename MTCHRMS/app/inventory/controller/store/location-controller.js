/// <reference path="../../module/inv-module.js" />
/// <reference path="supplier-repository.js" />

//'use strict';
invModule.controller('LocationController',
[
    '$scope', '$location', '$routeParams','locationRepository', 'itemRepository',
    function ($scope, $location, $routeParams, locationRepository, itemRepository) {

        console.log("location dashboard controller");
        //$scope.myname = "yahoo";

        $scope.isBusy = false;

        $scope.loadLocation = function () {
            $scope.isBusy = true;
            $scope.locations = locationRepository.getAllLocations();

            $scope.locations
                .$promise
                .then(function (response) {

                }, function(error) {
                    //alert("error");
                })
                .then(function() {

                })
                .then(function() { $scope.isBusy = false; });
        };

        $scope.loadItemsLocation = function () {
            $scope.isBusy = true;
            $scope.items = itemRepository.getAllItems();

            $scope.items.$promise.then(function () {
                //alert("success");
                    //console.log(countItems(1));
                }, function () {
                //alert("error");
            })
                .then(function () {

                })
                .then(function () { $scope.isBusy = false; });
        };

        $scope.countItems = function (id) {
            var countlocation = 0;
            angular.forEach($scope.items, function(item) {
                if (item.storeId === id) {
                    countlocation++;
                }
            });
            return countlocation;
        };

        $scope.save = function (location) {
            console.log(location);
            $scope.errors = [];
            locationRepository.addLocation(location).$promise.then(
                function () {
//                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/INVPortal/definition/store/list');
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    //appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.edit = function (location) {
            $scope.errors = [];
            locationRepository.editLocation(location).then(
                function () {
                    // success case
                    console.log("edit done - Successfully !");
                    //appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/INVPortal/definition/store/list');
                }, function (response) {
                    // failure case
                    console.log("edit - Error !");
                    //appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        //alert($routeParams.id);
        if ($routeParams.id != undefined) {
            $scope.store = locationRepository.getLocationById($routeParams.id);
        }


    }
]);
