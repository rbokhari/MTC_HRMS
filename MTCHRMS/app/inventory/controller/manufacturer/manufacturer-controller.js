/// <reference path="../../module/inv-module.js" />
/// <reference path="manufacturer-repository.js" />

'use strict';
invModule.controller('ManufacturerController',
[
    '$scope', '$location', '$routeParams','manufacturerRepository','appRepository','validationRepository','itemRepository', 'ModalService',
    function ($scope, $location, $routeParams, manufacturerRepository, appRepository, validationRepository, itemRepository, ModalService) {

        console.log("manufacturer dashboard controller");
        //$scope.myname = "yahoo";

        $scope.isBusy = false;

        $scope.loadCountries = function () {
            $scope.isLoadCountry = true;
            $scope.countries = validationRepository.getCountries;
            $scope.countries
                .$promise
                .then(function () { }, function () { })
                .then(function() { $scope.isLoadCountry = false; });
        };

        $scope.loadManufacturer = function () {
            $scope.isBusy = true;
            $scope.manufacturers = manufacturerRepository.getAllManufacturers();

            $scope.manufacturers.$promise.then(function (response) {
                //alert("success");
                    //console.log(response);
                }, function() {
                    //alert("error");
                })
                .then(function() {

                })
                .then(function () { $scope.isBusy = false; });

            //console.log($scope.manufacturers);
        };

        $scope.loadItemsManufacturer = function () {
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

        $scope.countItemsManufacturer = function (id) {
            //alert("hello " + id);
            console.log("call countItemsManufacturer");
            var countManufacturer = 0;
            angular.forEach($scope.items, function (item) {
                angular.forEach(item.itemManufacturers, function (manu) {
                    if (manu.manufacturerId === id) {
                        countManufacturer++;
                    }
                });
            });
            return countManufacturer;
        };

        $scope.save = function (manufacturer) {
            
            $scope.errors = [];
            if (angular.isUndefined(manufacturer.statusId) || manufacturer.statusId == 'false' || manufacturer.statusId == 0) {
                manufacturer.statusId = 0;
            } else {
                manufacturer.statusId = 1;
            }

            console.log(manufacturer);
            manufacturerRepository.addManufacturer(manufacturer)
                .$promise
                .then(function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/INVPortal/definition/manufacturer');
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.edit = function (manufacturer) {
            $scope.errors = [];
            if (angular.isUndefined(manufacturer.statusId) || manufacturer.statusId == 'false' || manufacturer.statusId == 0) {
                manufacturer.statusId = 0;
            } else {
                manufacturer.statusId = 1;
            }
            console.log(manufacturer);
            manufacturerRepository.editManufacturer(manufacturer)
                .then(
                function () {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/INVPortal/definition/manufacturer');
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
            
            $scope.manufacturer = manufacturerRepository.getManufacturerById($routeParams.id);
            $scope.manufacturer.$promise
                .then(function() {  }, function(error) { console.log(error); })
                .then(function() { $scope.isBusy = true; });
        }

        $scope.showContact = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/manufacturer/manufacturer-contact.html",
                controller: "ManufacturerModalController",
                inputs: {
                    title: "Add New Contact",
                    parentId: id,
                    manufacturerContact: {},
                    manufacturerContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.manufacturer[0].manufacturerContactPersons.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });

            });
        };

        $scope.editContact = function (contact) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/manufacturer/manufacturer-contact.html",
                controller: "ManufacturerModalController",
                inputs: {
                    title: "Update Contact",
                    parentId: contact.manufacturerId,
                    manufacturerContact: contact,
                    manufacturerContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.showContract = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/manufacturer/manufacturer-contract.html",
                controller: "ManufacturerModalController",
                inputs: {
                    title: "Add New Contract",
                    parentId: id,
                    manufacturerContact: {},
                    manufacturerContract: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.manufacturer[0].manufacturerContracts.push(result.resultData);
                });
            });
        };

        $scope.editContract = function (contract) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/manufacturer/manufacturer-contract.html",
                controller: "ManufacturerModalController",
                inputs: {
                    title: "Update Contract",
                    parentId: contract.manufacturerId,
                    manufacturerContact: {},
                    manufacturerContract: contract,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

    }
]);
