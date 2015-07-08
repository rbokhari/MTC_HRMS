/// <reference path="../../module/inv-module.js" />
/// <reference path="item-repository.js" />


'use strict';
invModule.controller('ItemController',
[
    '$scope', '$location', '$routeParams', 'itemRepository', 'validationRepository', 'appRepository',
        'locationRepository', 'departmentRepository', 'supplierRepository', 'manufacturerRepository', 'ModalService',

    function ($scope, $location, $routeParams, itemRepository, validationRepository, appRepository,
        locationRepository, departmentRepository, supplierRepository, manufacturerRepository, ModalService) {

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
            //console.log($scope.storeLocations);
        };

        $scope.loadStockAdd = function() {
            $scope.maintenanceTypes = validationRepository.getMaintenanceType;
        }

        $scope.loadSupplier = function() {
            $scope.suppliers = supplierRepository.getAllSuppliers();

            $scope.suppliers.$promise.then(function (response) {}, function () {})
                .then(function () { $scope.isBusy = false; });
        };

        $scope.loadManufacturer = function () {
            $scope.isBusy = true;
            $scope.manufacturers = manufacturerRepository.getAllManufacturers();

            $scope.manufacturers.$promise.then(function (response) {
                //alert("success");
                //console.log(response);
            }, function () {
                //alert("error");
            })
                .then(function () {

                })
                .then(function () { $scope.isBusy = false; });

            //console.log($scope.manufacturers);
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

        $scope.saveStock = function (stock, itemId) {

            stock.itemId = itemId;
            if (angular.isUndefined(stock.isWarranty) || stock.isWarranty == 'false' || stock.isWarranty == 0) {
                stock.isWarranty = 0;
            } else {
                stock.isWarranty = 1;
            }

            if (angular.isUndefined(stock.isMaintenance) || stock.isMaintenance == 'false' || stock.isMaintenance == 0) {
                stock.isMaintenance = 0;
            } else {
                stock.isMaintenance = 1;
            }

            $scope.errors = [];
            itemRepository.addItemStock(stock)
                .$promise
                .then(
                function (response) {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    console.log(response);
                    console.log(response.itemId + ":::" + response.itemStockAddId);
                    $scope.showSerialConfirmModal(response.itemId, response.itemStockAddId);
                    //$location.url('/INVPortal/item/detail/' + response.itemId);
                }, function (error) {
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



        //alert($routeParams.id);
        $scope.loadItem = function() {
            if ($routeParams.id != undefined) {
                $scope.isBusy = true;
                $scope.item = itemRepository.getItemById($routeParams.id);
                $scope.item.$promise.then(function () {
                    //alert("success");
                }, function () {
                    //alert("error");
                })
                .then(function () {

                })
                .then(function () {
                    $scope.isBusy = false;
                });
            }
        };


        // Serial Modal Start ----

        $scope.showSerialConfirmModal = function (id, stockId) {
            //console.log(id + ":" + serialCount);
            ModalService.showModal({
                templateUrl: "/app/common/templates/modal/confirm-modal.html",
                controller: "ItemSerialModalController",
                inputs: {
                    title: "Add Serial",
                    messagebody: "Would you like to add Serial Numbers now ?",
                    parentId: stockId,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('.modal-backdrop').remove();
                    if (result.resultData === 1) {
                        $scope.showSerial(id, stockId);
                    } else {
                        $location.url('/INVPortal/item/detail/' + id);
                    }
                    console.log(result.resultData);
                    //$scope.item[0].itemDepartments.push(result.resultData);
                });
            });
        };

        $scope.showSerial = function (itemId,id) {
            //console.log(id + ":" + serialCount);
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/item/item-serial.html",
                controller: "ItemSerialModalController",
                inputs: {
                    title: "Add Serial",
                    parentId: id,
                    messagebody:'',
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('.modal-backdrop').remove();
                    //$scope.item[0].itemDepartments.push(result.resultData);
                    $location.url('/INVPortal/item/detail/' + itemId);
                });
            });
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
                    $('.modal-backdrop').remove();
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
                    $('.modal-backdrop').remove();
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
                    $('.modal-backdrop').remove();
                });
            });
        };

        $scope.deleteSupplier = function (index, supplier) {
            var x;
            
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
                    $('.modal-backdrop').remove();
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
                    $('.modal-backdrop').remove();
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
            console.log(manufacturer);
            if (confirm("Are you sure to delete this record ?") == true) {
                itemRepository.deleteItemManufacturer(manufacturer)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.item[0].itemManufacturers.splice(index, 1);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.editSingleSerial = function (serialid) {
            console.log(serialid);
            $('#tr' + serialid).addClass('info');
            $('#tEditSerial' + serialid).removeAttr("disabled");
            $('#tEditSerial' + serialid).focus();
            $('#cEdit' + serialid).hide();
            $('#cPrintBarcode' + serialid).hide();
            $('#cSave' + serialid).show();
            $('#cUndo' + serialid).show();
        };

        $scope.undoSingleSerial = function (serialid) {
            console.log(serialid);
            $('#tr' + serialid).removeClass('info', 1000, 'fade');
            $('#tEditSerial' + serialid).attr('disabled', 'disabled');
            $('#cEdit' + serialid).show();
            $('#cPrintBarcode' + serialid).show();
            $('#cSave' + serialid).hide();
            $('#cUndo' + serialid).hide();
        };

        $scope.saveSingleSerial = function (serial) {
            console.log(serial);

            itemRepository.updateItemSerial(serial)
                .$promise
                .then(function() {
                    appRepository.showUpdateSuccessGritterNotification();
                    $('#tr' + serial.itemStockSerialId).removeClass('info', 1000, 'fade');
                    $('#tEditSerial' + serial.itemStockSerialId).attr('disabled', 'disabled');
                    $('#cEdit' + serial.itemStockSerialId).show();
                    $('#cPrintBarcode' + serial.itemStockSerialId).show();
                    $('#cSave' + serial.itemStockSerialId).hide();
                    $('#cUndo' + serial.itemStockSerialId).hide();

                    //$('#tEditSerial' + serial.itemStockSerialId).hide();
                }, function() {
                    appRepository.showErrorGritterNotification();
                });
        };


        $scope.item = {};

        $scope.clearSearch = function () {
            $scope.errors = [];

            $scope.item.itemCode = "";
            $scope.item.itemName = "";
            $scope.item.typeId = 0;
            $scope.item.categoryId = 0;
            $scope.item.supplierId = 0;
            $scope.item.condition = 0;
            $scope.item.manufacturerId = 0;

        };

        $scope.clearSearch();

        //console.log($routeParams);
        function getSearchData(item) {
            $scope.items = itemRepository.getItemSearchList(item);
            $scope.items
                .$promise
                .then(function (response) {
                    console.log(response);

                }, function (error) {

                })
                .then(function () {
                    $scope.isBusy = false;
                });
        }

        //console.log(Object.keys($routeParams).length);

        if ($routeParams.param01 != undefined) {
            $scope.clearSearch();
            $scope.item.typeId = $routeParams.param01;
            getSearchData($scope.item);
            console.log("first param reach");
        }

        if ($routeParams.param02 != undefined) {
            $scope.clearSearch();
            $scope.item.categoryId = $routeParams.param02;
            getSearchData($scope.item);
            console.log("second param reach");
        }

        if ($routeParams.param03 != undefined) {
            $scope.clearSearch();
            $scope.item.storeId = $routeParams.param03;
            getSearchData($scope.item);
            console.log("third param reach");
        }

        if ($routeParams.param04 != undefined) {
            $scope.clearSearch();
            $scope.item.departmentId = $routeParams.param04;
            getSearchData($scope.item);
            console.log("fourth param reach");
        }

        if ($routeParams.param05 != undefined) {
            $scope.clearSearch();
            $scope.item.supplierId = $routeParams.param05;
            getSearchData($scope.item);
            console.log("fourth param reach");
        }


        if ($routeParams.param06 != undefined) {
            $scope.clearSearch();
            $scope.item.manufacturerId = $routeParams.param06;
            getSearchData($scope.item);
            console.log("fourth param reach");
        }


        $scope.itemSearch = function (item) {
            $scope.errors = [];
            $scope.isBusy = true;

            getSearchData(item);
        };

        if (Object.keys($routeParams).length === 0) {
            $scope.loadItems();
        }

    }
]);
