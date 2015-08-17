

'use strict';
invModule.controller('ItemDistributionController',
[
    '$scope', '$location', '$routeParams',
    'authRepository', 'itemRepository', 'departmentRepository', 'employeeRepository', 'ModalService',

    function ($scope, $location, $routeParams,
        authRepository, itemRepository, departmentRepository, employeeRepository, ModalService) {

        $scope.departments = departmentRepository.getAllDepartment();

        $scope.auth = authRepository.authentication;
        console.log($scope.auth);

        $scope.loadEmployee = function(id) {
            $scope.employees = employeeRepository.getEmployeesByDepartmentId(id);
        }

        //$scope.selectedItemSerials = [];

        $scope.distribution = {
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
                                    $scope.distribution.distributionItems.push({
                                        itemId: value["itemId"],
                                        distributionId:0,
                                        ItemStockSerialId : 0,
                                        itemImage: itemData.itemPicture,
                                        itemCode: itemData.itemCode,
                                        itemName: itemData.itemName,
                                        stockInHand: itemData.stockinHand,
                                        serial: value["serialNo"]
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
                    console.log(result);
                }, function () {
                
            });
        };

        $scope.removeItemDistribution = function(item) {
            $scope.selectedItemSerials.splice($scope.selectedItemSerials.indexOf(item),1);
        }


    }
]);