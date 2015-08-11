

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

        $scope.selectItem = function (item) {
            $scope.resultData = item;
            $scope.close();
            $('#confirmModal').modal('hide');
        }

        $scope.cancelClick = function () {
            $scope.resultData = 0;

            $scope.close();
            $('#confirmModal').modal('hide');
        }


        $scope.saveItemSerials = function (parentId, itemSerials, isprint) {
            var ctrl = $("#cmdSaveSerial");
            appRepository.setControlDisabled(ctrl);
            $scope.errors = [];
            //itemDepartment.itemId = parentId;

            itemRepository.addItemStockSerials(itemSerials)
                .$promise
                .then(function (response) {
                    console.log("parentId : : " + parentId);
                    console.log("response", response);
                    appRepository.showAddSuccessGritterNotification();

                    if (isprint) {
                        var address = $location.protocol() + "://" + location.host;
                        location.href = address + "/api/item/BarcodeDataItemId/" + parentId;
                    };

                    $scope.close();
                    $('#dvStockSerial').modal('hide');
                }, function(error) {
                    console.log("error", error);
                    
                })
                .then(function() {
                 appRepository.setControlEnabled(ctrl);
            });
            //console.log(itemSerials);
        };

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 300); // close, but give 500ms for bootstrap to animate

        };

    }
]);