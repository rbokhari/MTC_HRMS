

'use strict';
invModule.controller('ItemDistributionController',
[
    '$scope', '$location', '$routeParams',
    'authRepository', 'itemRepository', 'appRepository', 'departmentRepository', 'employeeRepository', 'ModalService',

    function ($scope, $location, $routeParams,
        authRepository, itemRepository, appRepository, departmentRepository, employeeRepository, ModalService) {

        $scope.departments = departmentRepository.getAllDepartment();

        $scope.auth = authRepository.authentication;

        $scope.loadEmployee = function(id) {
            $scope.employees = employeeRepository.getEmployeesByDepartmentId(id);
        }

        //$scope.selectedItemSerials = [];

        $scope.distribution = {
            authorizedByName: $scope.auth.fullName,
            authorizedBy: $scope.auth.employeeId,
            authorizedDesignation: $scope.auth.designation,
            distributionItems:[]
        };

        $scope.showItemLookup = function () {
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/distribution/item-lookup.html",
                controller: "ItemLookupModalController",
                inputs: {
                    title: "Select Item",
                    parentId: -1,   // to load item values
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('.modal-backdrop').remove();
                    var itemData = result.resultData;
                    ModalService.showModal({
                        templateUrl: "/app/inventory/templates/distribution/item-serial-lookup.html",
                        controller: "ItemLookupModalController",
                        inputs: {
                            title: "Select Item : " + result.resultData.itemCode + ", " + result.resultData.itemName,
                            parentId: result.resultData.itemId,    // to load selected item serials
                            resultData: {}
                        }
                    }).then(function (modal) {
                        modal.element.modal();
                        modal.close.then(function (result) {
                            $('.modal-backdrop').remove();

                            if (result.resultData === "back") {
                                console.log("back");
                                $scope.showItemLookup();
                                return;
                            }
                            else {
                                angular.forEach(result.resultData, function (value, key) {
                                    console.log("value", value);
                                    $scope.distribution.distributionItems.push({
                                        itemId: value["itemId"],
                                        distributionId:0,
                                        itemStockSerialId: value["itemStockSerialId"],
                                        itemImage: itemData.itemPicture,
                                        itemCode: itemData.itemCode,
                                        itemName: itemData.itemName,
                                        stockInHand: itemData.stockinHand,
                                        serialNo: value["serialNo"],
                                        createdBy: 5,
                                        createdOn: new Date()
                                    });
                                });
                            }
                        });
                    });
                });
            });
        };


        $scope.saveDistribution = function (distribution) {

            itemRepository.addItemDistribution(distribution)
                .$promise
                .then(function (result) {
                    
                    appRepository.showAddSuccessGritterNotification();
                    $location.url('/INVPortal');

                }, function (error) {
                    appRepository.showErrorGritterNotification();
            });
        };

        $scope.removeItemDistribution = function(item) {
            $scope.selectedItemSerials.splice($scope.selectedItemSerials.indexOf(item),1);
        }


    }
]);