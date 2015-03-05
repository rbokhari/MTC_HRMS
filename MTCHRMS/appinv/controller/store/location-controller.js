/// <reference path="../../module/inv-module.js" />
/// <reference path="supplier-repository.js" />

'use strict';
invModule.controller('LocationController',
[
    '$scope', '$location', '$routeParams','locationRepository',
    function ($scope, $location, $routeParams, locationRepository) {

        console.log("location dashboard controller");
        //$scope.myname = "yahoo";

        $scope.isBusy = false;

        $scope.loadLocation = function () {
            $scope.isBusy = true;
            $scope.locations = locationRepository.getAllLocations();

            $scope.locations.$promise.then(function () {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() {

                })
                .then(function() { $scope.isBusy = false; });
        };

        $scope.save = function (location) {

            $scope.errors = [];
            locationRepository.addLocation(location).$promise.then(
                function () {
//                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/INVPortal/store/list');
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

                    $location.url('/INVPortal/store/list');
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
            $scope.location = locationRepository.getLocationById($routeParams.id);
        }


    }
]);
