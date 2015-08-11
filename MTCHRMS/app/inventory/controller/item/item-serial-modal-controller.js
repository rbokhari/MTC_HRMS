﻿

moduleModal.controller('ItemSerialModalController',
[
    '$scope', '$location', 'appRepository', 'itemRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', 'messagebody',
    function($scope, $location, appRepository, itemRepository, title, close,
        parentId, resultData, $timeout, messagebody) {

        $scope.resultData = {};
        $scope.title = title;
        $scope.messagebody = messagebody;
        $scope.parentId = parentId;

        $scope.itemSerials = itemRepository.getAllSerialsByStockAddId($scope.parentId);
        $scope.itemSerials
            .$promise
            .then(function() {}, function() {});

        $scope.range = function (min, max, step) {
            step = step || 1;
            var input = [];
            for (var i = min; i <= max; i += step) input.push(i);

            return input;
        };



        $scope.confirmClick = function () {

            $scope.resultData = 1;
            
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