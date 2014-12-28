
hrmsModule.controller('EmployeeModalController',
[
    '$scope', 'appRepository', 'employeeRepository', 'validationRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', '$upload',

    function ($scope, appRepository, employeeRepository, validationRepository, title, close,
        parentId, resultData, $timeout, $upload) {

        //$scope.name = null;
        //$scope.age = null;
        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;

        $scope.genders = validationRepository.getAllDetailsByValidationId(5);

        $scope.saveEmployeePassport = function(parentId, employeePassport) {
            $scope.errors = [];
            employeePassport.employeeDefId = parentId;
            employeeRepository.addEmployeePassport(employeePassport)
                .$promise
                .then(
                    function(resultEmployeePassport) {
                        // success case
                        $scope.resultData = resultEmployeePassport;
                        appRepository.showAddSuccessGritterNotification();
                        //console.log("employee passport save - Successfully !" + resultEmployeePassport.issuePlace);
                        //console.log("employee passport save - Successfully !" + $scope.newPassport.issuePlace);
                        $scope.close();
                        $('#dvPassport').modal('hide');
                        //$location.url('/HRMSPortal/employee/detail/' + resultEmployeePassport.id);
                    }, function(response) {
                        // failure case
                        console.log("employee passport save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
            //.then(function () { $scope.close(); });
        };

        $scope.saveEmployeeVisa = function(parentId, employeeVisa) {
            $scope.errors = [];
            employeeVisa.employeeDefId = parentId;
            employeeRepository.addEmployeeVisa(employeeVisa)
                .$promise
                .then(
                    function(resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvVisa').modal('hide');
                    }, function(response) {
                        // failure case
                        console.log("employee visa save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        $scope.savePreviousEmployment = function(parentId, employeePreviousEmployement) {
            $scope.errors = [];

            employeePreviousEmployement.employeeDefId = parentId;
            employeeRepository.addEmployeePreviousEmployment(employeePreviousEmployement)
                .$promise
                .then(
                    function(resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvPreviousEmployement').modal('hide');
                    }, function(response) {
                        // failure case
                        console.log("savePreviousEmployment save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        $scope.saveEmployeeChild = function(parentId, employeeChild) {
            $scope.errors = [];

            employeeChild.employeeDefId = parentId;
            employeeRepository.addEmployeeChild(employeeChild)
                .$promise
                .then(
                    function(resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvChildren').modal('hide');
                    }, function(response) {
                        // failure case
                        console.log("saveEmployeeChild save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        //$scope.saveImage = function(parentId, employeeDef) {
        //    //alert(employeeDef.employeeCode);
        //    $scope.errors = [];
        //    employeeRepository.addEmployeeImage(employeeDef).$promise.then(
        //        function(resultEmployeeDef) {
        //            // success case
        //            console.log("save - Successfully !");
        //            appRepository.showAddSuccessGritterNotification();
        //            $location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
        //        }, function(response) {
        //            // failure case
        //            console.log("save - Error !");
        //            appRepository.showErrorGritterNotification();
        //            $scope.errors = response.data;
        //        }
        //    );
        //};

        $scope.close = function() {
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
                (function(index) {
                    $scope.upload[index] = $upload.upload({
                        url: "/api/employee/upload", // webapi url
                        method: "POST",
                        data: { parentId1: parentId },
                        file: $file
                    }).progress(function(evt) {
                        // get upload percentage
                        console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
                    }).success(function(data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.resultData = data;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvImage1').modal('hide');
                        //$scope.employee[0].empPicture = data.empPicture;
                        console.log(data);
                    }).error(function(data, status, headers, config) {
                        // file failed to upload
                        appRepository.showErrorGritterNotification();
                        console.log(data);
                    });
                })(i);
            }
        };

        $scope.abortUpload = function(index) {
            $scope.upload[index].abort();
        };


    }
]);