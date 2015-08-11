

'use strict';
invModule.controller('ItemDistributionController',
[
    '$scope', '$location', '$routeParams', 'itemRepository', 'ModalService',

    function ($scope, $location, $routeParams, itemRepository, ModalService) {

        //$scope.items = itemRepository.getAllItems();

        //$scope.items
        //    .$promise
        //    .then(function () {
        //        console.log("items", $scope.items);
        //    }, function () {
        //        //alert("error");
        //    })
        //    .then(function () {

        //    });

        $scope.showItemLookup = function () {
            ModalService.showModal({
                templateUrl: "/app/inventory/templates/distribution/item-lookup.html",
                controller: "ItemLookupModalController",
                inputs: {
                    title: "Select Item",
                    parentId: 1,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('.modal-backdrop').remove();
                    console.log(result.resultData);
                });
            });
        };


    }
]);