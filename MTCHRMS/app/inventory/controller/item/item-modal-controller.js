

moduleModal.controller('ItemModalController',
[
    '$scope', 'appRepository', 'itemRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', '$upload', 'itemDepartment', 'itemYear', 'itemSupplier', 'itemManufacturer',
    'departmentRepository', 'validationRepository', 'supplierRepository', 'manufacturerRepository',

    function ($scope, appRepository, itemRepository, title, close,
        parentId, resultData, $timeout, $upload, itemDepartment, itemYear, itemSupplier, itemManufacturer,
        departmentRepository, validationRepository, supplierRepository, manufacturerRepository) {
        
        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        $scope.itemDepartment = itemDepartment;
        $scope.itemYear = itemYear;
        $scope.itemSupplier = itemSupplier;
        $scope.itemManufacturer = itemManufacturer;

        
        $scope.departments = departmentRepository.getAllDepartment();
        $scope.itemYears = validationRepository.getItemYears;
        $scope.itemSuppliers = supplierRepository.getAllSuppliers();
        $scope.iManufacturers = manufacturerRepository.getAllManufacturers();
        console.log("---------------------");
        console.log($scope.iManufacturers);

       
        $scope.saveItemDepartment = function (parentId, itemDepartment) {
            $scope.errors = [];
            itemDepartment.itemId = parentId;

            itemRepository.addItemDepartment(itemDepartment)
                .$promise
                .then(
                    function (result) {
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvDepartment').modal('hide');
                    }, function (error) {
                        console.log("item department save - Error !--------");
                        if (error.status == 302) {
                            $("#dvduplicate").addClass("error");
                            appRepository.showDuplicateGritterNotification();
                        } else {
                            appRepository.showErrorGritterNotification();
                        }
                        $scope.errors = error.data;
                    }
                );
        };

        $scope.saveItemYear = function (parentId, itemYear) {
            $scope.errors = [];
            itemYear.itemId = parentId;

            itemRepository.addItemYear(itemYear)
                .$promise
                .then(
                    function (result) {
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvYear').modal('hide');
                    }, function(error) {
                    console.log("item year save - Error !");
                    if (error.status == 302) {
                        $("#dvduplicate").addClass("error");
                        appRepository.showDuplicateGritterNotification();
                    } else {
                        appRepository.showErrorGritterNotification();
                    }
                    $scope.errors = error.data;
                }
            );
        };

        $scope.saveItemSupplier = function(parentId, itemSupplier) {
            $scope.errors = [];
            itemSupplier.itemId = parentId;

            itemRepository.addItemSupplier(itemSupplier)
                .$promise
                .then(
                    function(result) {
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvSupplier').modal('hide');
                        //
                    }, function(error) {
                        console.log("item supplier save - Error !");
                        if (error.status == 302) {
                            $("#dvduplicate").addClass("error");
                            appRepository.showDuplicateGritterNotification();
                        } else {
                            appRepository.showErrorGritterNotification();
                        }
                        $scope.errors = error.data;
                    }
                );
        };

        $scope.saveItemManufacturer = function (parentId, itemManufacturer) {
            $scope.errors = [];
            itemManufacturer.itemId = parentId;
            itemRepository.addItemManufacturer(itemManufacturer)
                .$promise
                .then(
                    function (result) {
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvManufacturer').modal('hide');
                    }, function (error) {
                        console.log("item manufacturer save - Error !--------");
                        if (error.status == 302) {
                            $("#dvduplicate").addClass("error");
                            appRepository.showDuplicateGritterNotification();
                        } else {
                            appRepository.showErrorGritterNotification();
                        }
                        $scope.errors = error.data;
                    }
                );
        };

        $scope.close = function () {
            console.log("close function modal controller :");
            close({
                resultData: $scope.resultData
            }, 300); // close, but give 500ms for bootstrap to animate

        };


        $scope.upload = [];

        $scope.onFileSelect = function (parentId, $files) {
            //$files: an array of files selected, each file has name, size, and type.
            
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/item/upload", // webapi url
                        method: "POST",
                        data: { parentId1: parentId },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.resultData = data;
                        appRepository.showUpdateSuccessGritterNotification();
                        $scope.close();
                        $('#dvImage').modal('hide');
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

        $scope.onAttachmentUpload = function (parentId, $files) {
            //$files: an array of files selected, each file has name, size, and type.
            console.log("file attachment");
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];
                (function (index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/item/attachment", // webapi url
                        method: "POST",
                        data: { parentId1: parentId },
                        file: $file
                    }).progress(function (evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.resultData = data;
                        appRepository.showUpdateSuccessGritterNotification();
                        $scope.close();
                        $('#dvItemAttachment').modal('hide');
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