

moduleModal.controller('ItemLookupModalController',
[
    '$scope', '$location', 'appRepository', 'itemRepository', 'title', 'close',
    'parentId','resultData', '$timeout', 
    function($scope, $location, appRepository, itemRepository, title, close,
        parentId, resultData, $timeout) {

        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        //$scope.pitems = pItems;
        
        //console.log("ItemLookupModalController " + $scope.pitems);

        if ($scope.parentId === -1) {
            $scope.pitems = itemRepository.getAllItems();
            $scope.pitems
                .$promise
                .then(function () {
                    //alert("success");
                }, function () {
                    //alert("error");
                })
                .then(function () {
                });
        }

        if ($scope.parentId > 0) {
            $scope.itemSerials = itemRepository.getAllSerialsByItemId($scope.parentId);
            $scope.itemSerials.$promise
                .then(function (){;}, function () {})
                .then(function () {});
        }

        $scope.refreshItems = function() {
            $scope.pitems = itemRepository.getAllItems();

            $scope.pitems
                .$promise
                .then(function () {
                    //alert("success");
                }, function () {
                    //alert("error");
                })
                .then(function () {

                });
        }

        $scope.selectItem = function(item) {
            $scope.resultData = { "itemId": item.itemId,"itemPicture": item.itemPicture, "itemCode": item.itemCode, "itemName": item.itemName, "stockinHand": item.itemStock };
            $scope.close();
            $('#dvItemLookup').modal('hide');
        }

        $scope.selectSerials = function (serials) {
            var selectedSerials = [];
            angular.forEach(serials, function (value, key) {
                angular.forEach(value, function (val, ke) {
                    if (ke === "isSelected" && val === true) {
                        //console.log("found >" + val + " > serial " + value["serialNo"]);
                        selectedSerials.push(value);
                    }
                });
            });
            //alert(selectedSerials);
            $scope.resultData = selectedSerials;
            $scope.close();
            $('#dvItemSerialLookup').modal('hide');
        }

        $scope.cancelClick = function () {
            $scope.resultData = 0;

            $scope.close();
            $('#confirmModal').modal('hide');
        }

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 300); // close, but give 500ms for bootstrap to animate

        };

    }
]);