

module.controller('SupplierModalController',
[
    '$scope', 'appRepository', 'supplierRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', '$upload', 'supplierContact', 'supplierContract',

    function ($scope, appRepository, supplierRepository, title, close,
        parentId, resultData, $timeout, $upload, supplierContact, supplierContract) {
        
        //$scope.name = null;
        //$scope.age = null;
        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        $scope.supplierContact = supplierContact;
        $scope.supplierContract = supplierContract;

        $scope.saveSupplierContact = function (parentId, supplierContact) {
            $scope.errors = [];
            supplierContact.supplierId = parentId;

            supplierRepository.addSupplierContact(supplierContact)
                .$promise
                .then(
                    function (result) {
                        // success case
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvContact').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("supplier contact save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.saveSupplierContract = function (parentId, supplierContract) {
            $scope.errors = [];
            supplierContract.supplierId = parentId;

            supplierRepository.addSupplierContract(supplierContract)
                .$promise
                .then(
                    function (result) {
                        // success case
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvContract').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("supplier contract save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.close = function () {
            console.log("close funciton modal controller :");
            close({
                resultData: $scope.resultData
            }, 500); // close, but give 500ms for bootstrap to animate

        };


        $scope.upload = [];
        //$scope.fileUploadObj = { testString1: "Test string 1", testString2: "Test string 2" };

        $scope.onFileSelect = function (parentId, $files) {
            //$files: an array of files selected, each file has name, size, and type.
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/employee/upload", // webapi url
                        method: "POST",
                        data: { parentId1: parentId },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.resultData = data;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvImage1').modal('hide');
                        //$scope.employee[0].empPicture = data.empPicture;
                        console.log(data);
                    }).error(function (data, status, headers, config) {
                        // file failed to upload
                        appRepository.showErrorGritterNotification();
                        console.log(data);
                    });
                })(i);
            }
        };

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        };
    }
]);