/// <reference path="../../module/inv-module.js" />
/// <reference path="supplier-repository.js" />

'use strict';
invModule.controller('SupplierController',
[
    '$scope', '$location', '$routeParams','supplierRepository','appRepository',
    function ($scope, $location, $routeParams, supplierRepository, appRepository) {

        console.log("supplier dashboard controller");
        //$scope.myname = "yahoo";

        $scope.isBusy = false;

        $scope.loadSupplier = function () {
            $scope.isBusy = true;
            $scope.suppliers = supplierRepository.getAllSuppliers();

            $scope.suppliers.$promise.then(function(response) {
                //alert("success");
                    //console.log(response);
                }, function() {
                    //alert("error");
                })
                .then(function() {

                })
                .then(function () { $scope.isBusy = false; });

            console.log($scope.suppliers);
        };

        

        $scope.save = function (supplier) {

            $scope.errors = [];
            supplierRepository.addSupplier(supplier).$promise.then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/INVPortal/definition/supplier/list');
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.edit = function (supplier) {
            $scope.errors = [];
            supplierRepository.editSupplier(supplier).then(
                function () {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/INVPortal/definition/supplier/list');
                }, function (response) {
                    // failure case
                    console.log("edit - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        //alert($routeParams.id);
        if ($routeParams.id != undefined) {
            $scope.supplier = supplierRepository.getSupplierById($routeParams.id);
        }


    }
]);
