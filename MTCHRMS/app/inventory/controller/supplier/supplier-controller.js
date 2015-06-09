/// <reference path="../../module/inv-module.js" />
/// <reference path="supplier-repository.js" />

'use strict';
invModule.controller('SupplierController',
[
    '$scope', '$location', '$routeParams','supplierRepository','appRepository','validationRepository','ModalService',
    function ($scope, $location, $routeParams, supplierRepository, appRepository, validationRepository, ModalService) {

        console.log("supplier dashboard controller");
        //$scope.myname = "yahoo";

        $scope.isBusy = false;

        $scope.loadCountries = function() {
            $scope.countries = validationRepository.getAllDetailsByValidationId(3); 
        };

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

            //console.log($scope.suppliers);
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
            $scope.supplier.$promise
                .then(function() {}, function() {})
                .then(function() { $scope.isBusy = true; });
        }

        // Modal service start ----------------
        $scope.showContact = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/supplier/supplier-contact.html",
                controller: "SupplierModalController",
                inputs: {
                    title: "Add New Contact",
                    parentId: id,
                    supplierContact: {},
                    supplierContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.supplier[0].supplierContactPersons.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });

            });
        };

        $scope.editContact = function (contact) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/supplier/supplier-contact.html",
                controller: "SupplierModalController",
                inputs: {
                    title: "Update Contact",
                    parentId: contact.supplierId,
                    supplierContact: contact,
                    supplierContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.showContract = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/supplier/supplier-contract.html",
                controller: "SupplierModalController",
                inputs: {
                    title: "Add New Contract",
                    parentId: id,
                    supplierContact: {},
                    supplierContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.supplier[0].supplierContracts.push(result.resultData);
                });
            });
        };

        $scope.editContract = function (contract) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/supplier/supplier-contract.html",
                controller: "SupplierModalController",
                inputs: {
                    title: "Update Contract",
                    parentId: contract.supplierId,
                    supplierContact: {},
                    supplierContract: contract,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };


    }
]);
