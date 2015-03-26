

moduleModal.controller('ItemModalController',
[
    '$scope', 'appRepository', 'itemRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', '$upload', 'itemDepartment', 'itemYear', 'itemSupplier','departmentRepository','validationRepository','supplierRepository',

    function ($scope, appRepository, itemRepository, title, close,
        parentId, resultData, $timeout, $upload, itemDepartment, itemYear, itemSupplier, departmentRepository, validationRepository, supplierRepository) {
        
        //$scope.name = null;
        //$scope.age = null;
        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        $scope.itemDepartment = itemDepartment;
        $scope.itemYear = itemYear;
        $scope.itemSupplier = itemSupplier;

        
        $scope.departments = departmentRepository.getAllDepartment();
        $scope.itemYears = validationRepository.getItemYears;
        $scope.itemSuppliers = supplierRepository.getAllSuppliers();

        //$scope.loadDepartments = function () {
        //    alert("loading department");
        //    $scope.departments = departmentRepository.getAllDepartment();
        //};

        //$scope.loadYears = function() {
        //    $scope.itemYears = validationRepository.getItemYears;
        //};
        
        //$scope.loadSuppliers = function() {
        //    $scope.suppliers = supplierRepository.getAllSuppliers;
        //};
        
        

        $scope.saveItemDepartment = function (parentId, itemDepartment) {
            $scope.errors = [];
            itemDepartment.itemId = parentId;

            itemRepository.addItemDepartment(itemDepartment)
                .$promise
                .then(
                    function (result) {
                        // success case
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvDepartment').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("item department save - Error !--------");
                        console.log(response);
                        if (response.status == 302) {   
                            $("#dvduplicate").addClass("error");
                            //$("#imgError").visibility="visible";
                            appRepository.showDuplicateGritterNotification();
                        } else {
                            appRepository.showErrorGritterNotification();
                        }
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.saveItemYear = function (parentId, itemYear) {
            $scope.errors = [];
            itemYear.itemId = parentId;

            itemRepository.addItemYear(itemYear)
                .$promise
                .then(
                    function (result) {
                        // success case
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvYear').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("item year save - Error !");
                    if (response.status == 302) {
                        $("#dvduplicate").addClass("error");
                        //$("#imgError").visibility="visible";
                        appRepository.showDuplicateGritterNotification();
                    } else {
                        appRepository.showErrorGritterNotification();
                    }
                    $scope.errors = response.data;
                }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.saveItemSupplier = function (parentId, itemSupplier) {
            $scope.errors = [];
            itemSupplier.itemId = parentId;

            itemRepository.addItemSupplier(itemSupplier)
                .$promise
                .then(
                    function (result) {
                        // success case
                        $scope.resultData = result;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvSupplier').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("item supplier save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.close = function () {
            console.log("close function modal controller :");
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

        $scope.abortUpload = function (index) {
            $scope.upload[index].abort();
        };
    }
]);