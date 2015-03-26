/// <reference path="../../module/inv-module.js" />
/// <reference path="item-repository.js" />


'use strict';
invModule.controller('ItemController',
[
    '$scope', '$location', '$routeParams', 'itemRepository', 'validationRepository','appRepository', 'locationRepository','departmentRepository', 'ModalService',
    function ($scope, $location, $routeParams, itemRepository, validationRepository, appRepository, locationRepository, departmentRepository, ModalService) {

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

            $scope.itemTypes = validationRepository.getItemTypes; 
            $scope.itemCategories = validationRepository.getItemCategories;
            $scope.itemTechnicians = validationRepository.getItemTechnicians;
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

            if (angular.isUndefined(item.isIT) || item.isIT == 'false') {
                item.isIT = 0;
            } else {
                item.isIT = 1;
            }

            if (angular.isUndefined(item.isCallibration) || item.isCallibration == 'false') {
                item.isCallibration = 0;
            } else {
                item.isCallibration = 1;
            }

            if (angular.isUndefined(item.isMaintenance) || item.isMaintenance == 'false') {
                item.isMaintenance = 0;
            } else {
                item.isMaintenance = 1;
            }
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


        // Modal service start ----------------
        $scope.showDepartment = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-department.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Add Department",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.item[0].itemDepartments.push(result.resultData);
                });
            });
        };

        $scope.editDepartment = function (dept) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-department.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Update Department",
                    parentId: dept.itemId,
                    itemDepartment: dept,
                    itemYear: {},
                    itemSupplier: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteDepartment = function (dept) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemDepartment(dept)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemDepartments.pop(dept);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };


        $scope.showYear = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-year.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Add Year",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.item[0].itemYears.push(result.resultData);
                });

            });
        };

        $scope.deleteYear = function (year) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemYear(year)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemYears.pop(year);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };


        $scope.showSupplier = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-supplier.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Add Supplier",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.item[0].itemSuppliers.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });

            });
        };

        $scope.deleteSupplier = function (supplier) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemSupplier(supplier)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemSuppliers.pop(supplier);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.showImageForm = function (id) {

            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-image.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Update Picture",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //console.log(result);
                    $scope.item[0].itemPicture = result.resultData.itemPicture;
                });

            });

        };


    }
]);
