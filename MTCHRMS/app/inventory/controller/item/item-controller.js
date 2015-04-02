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

            if (angular.isUndefined(item.isIt) || item.isIt == 'false' || item.isIt == 0) {
                item.isIt = 0;
            } else {
                item.isIt = 1;
            }

            if (angular.isUndefined(item.isCallibration) || item.isCallibration == 'false' || item.isCallibration == 0) {
                item.isCallibration = 0;
            } else {
                item.isCallibration = 1;
            }

            if (angular.isUndefined(item.isMaintenance) || item.isMaintenance == 'false' || item.isMaintenance == 0) {
                item.isMaintenance = 0;
            } else {
                item.isMaintenance = 1;
            }

            console.log(item);
            $scope.errors = [];
            itemRepository.addItem(item)
                .$promise
                .then(
                function(response) {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    console.log(response);
                    $location.url('/INVPortal/item/detail/' + response.itemId);
                }, function(error) {
                    // failure case
                    console.log("save - Error !");
                    if (error.status == 302) {
                        appRepository.showDuplicateGritterNotification();
                    } else {
                        appRepository.showErrorGritterNotification();
                    }
                    console.log(error);
                    $scope.errors = error.data;
                }
            );
        };

        $scope.edit = function (item) {
            if (angular.isUndefined(item.isIt) || item.isIt == 'false' || item.isIt == 0) {
                item.isIt = 0;
            } else {
                item.isIt = 1;
            }

            if (angular.isUndefined(item.isCallibration) || item.isCallibration == 'false' || item.isCallibration == 0) {
                item.isCallibration = 0;
            } else {
                item.isCallibration = 1;
            }

            if (angular.isUndefined(item.isMaintenance) || item.isMaintenance == 'false' || item.isMaintenance == 0) {
                item.isMaintenance = 0;
            } else {
                item.isMaintenance = 1;
            }

            $scope.errors = [];
            console.log(item);
            itemRepository.addItem(item).$promise.then(
                function() {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/INVPortal/item/detail/' + item.itemId);
                }, function(response) {
                    // failure case
                    console.log("edit - Error !");
                    appRepository.showErrorGritterNotification();
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
                    itemManufacturer: {},
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
                    itemManufacturer: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteDepartment = function (index, dept) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemDepartment(dept)
                    .$promise
                    .then(function() {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemDepartments.splice(index, 1);
                    }, function(error) {
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
                    itemManufacturer: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.item[0].itemYears.push(result.resultData);
                });

            });
        };

        $scope.deleteYear = function (index, year) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemYear(year)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemYears.splice(index, 1);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.showSupplier = function (id) {
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-supplier.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Add Supplier",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    itemManufacturer: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.item[0].itemSuppliers.push(result.resultData);
                });
            });
        };

        $scope.deleteSupplier = function (index, supplier) {
            var x;
            alert(index);
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemSupplier(supplier)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        //$scope.item[0].itemSuppliers.pop(supplier);
                    $scope.item[0].itemSuppliers.splice(index, 1);
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
                    itemManufacturer: {},
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


        $scope.showManufacturer = function (id) {
            console.log(id);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-manufacturer.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Add Manufacturer",
                    parentId: id,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    itemManufacturer: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.item[0].itemManufacturers.push(result.resultData);
                }); 
            });
        };

        $scope.editManufacturer = function (manufacturer) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-manufacturer.html",
                controller: "ItemModalController",
                inputs: {
                    title: "Update Manufacturer",
                    parentId: manufacturer.itemId,
                    itemDepartment: {},
                    itemYear: {},
                    itemSupplier: {},
                    itemManufacturer: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteManufacturer = function (index, manufacturer) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemManufacturer(manufacturer)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemDepartments.splice(index, 1);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

    }
]);
