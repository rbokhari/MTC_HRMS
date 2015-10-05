
moduleModal.controller('EmployeeModalController',
[
    '$scope', '$location', 'appRepository', 'employeeRepository', 'validationRepository', 'leaveRepository', 'ticketRepository', 'title', 'close',
    'parentId', 'resultData', '$timeout', '$upload', 'employeePassport', 'employeeVisa', 'employeeQualification','validations',

    function ($scope, $location, appRepository, employeeRepository, validationRepository, leaveRepository, ticketRepository, title, close,
        parentId, resultData, $timeout, $upload, employeePassport, employeeVisa, employeeQualification, validations) {

        //$scope.name = null;
        //$scope.age = null;

        if ($location.path().indexOf('/HRMSPortalAr') == 0) {
            $scope.lang = "ar_OM";
        } else {
            $scope.lang = "en_US";
        }

        $scope.resultData = {};
        $scope.title = title;
        $scope.parentId = parentId;
        $scope.employeePassport = employeePassport;
        $scope.employeeVisa = employeeVisa;
        $scope.employeeQualification = employeeQualification;

        $scope.genders = validationRepository.getAllDetailsByValidationId(validations.GENDER);
        $scope.qualificationLevels = validationRepository.getAllDetailsByValidationId(validations.QUALIFICATION_LEVEL);
        $scope.employeeStatus = validationRepository.getAllDetailsByValidationId(validations.EMPLOYEE_STATUS);
        
        employeeRepository.getEmployeeContract($scope.parentId)
            .$promise
            .then(function (response) {
                $scope.contract = response;
            }, function (err) {
                console.log(err);
            });

        leaveRepository.getAllLeaves()
            .$promise
            .then(function (response) {
                $scope.leaves = response;
                }, function (err) {
            
                });

        ticketRepository.getAllTickets()
            .$promise
            .then(function (response) {
                $scope.tickets = response;
            }, function (err) {

            });

        //alert("yahoo :" + $scope.parentId);
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
                        $scope.close();
                        $('#dvPassport').modal('hide');
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

        $scope.saveEmployeeQualification = function (parentId, employeeQualification) {
            $scope.errors = [];

            employeeQualification.employeeDefId = parentId;
            employeeRepository.addEmployeeQualification(employeeQualification)
                .$promise
                .then(
                    function (resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvQualification').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("saveEmployeeQualification save - Error !");
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

        $scope.saveEmployeeContract = function (parentId, employeeContract) {
            $scope.errors = [];

            console.log(employeeContract);

            employeeContract.employeeDefId = parentId;
            employeeRepository.addEmployeeContract(employeeContract)
                .$promise
                .then(
                    function (resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvContract').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("saveEmployeeContract save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        $scope.loadLeaveCategory = function () {
            console.log("loading");


        };

        $scope.leaveSelect = function (id) {
            //alert("hello");
            console.log(id);

            angular.forEach($scope.leaves, function (val) {
                console.log(id + ' : ' + val.leaveId);
                if (id == val.leaveId) {
                    $('#tSchedule').val(val.scheduleDetail.nameEn);
                    $scope.leaveCategory.totalLeaves =val.total;
                }
            });
        };

        $scope.ticketSelect = function (id) {
            angular.forEach($scope.tickets, function (val) {
                console.log(id + ' : ' + val.ticketId);
                if (id == val.ticketId) {
                    $('#tSchedule').val(val.scheduleDetail.nameEn);
                    $('#tEligibility').val(val.eligibilityDetail.nameEn);
                }
            });
        };

        $scope.saveEmployeeLeaveCategory = function (parentId, contractId, leaveCategory) {
            $scope.errors = [];

            leaveCategory.employeeDefId = parentId;
            leaveCategory.contractId = contractId;
            leaveCategory.notes = '';
            leaveCategory.isActive = 1;
            employeeRepository.addEmployeeLeaveCategory(leaveCategory)
                .$promise
                .then(
                    function (resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvLeaveCategory').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("saveEmployeeLeaveCategory save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

        $scope.saveEmployeeTicketCategory = function (parentId, contractId, ticketCategory) {
            $scope.errors = [];

            ticketCategory.employeeDefId = parentId;
            ticketCategory.contractId = contractId;
            ticketCategory.isActive = 1;

            console.log(ticketCategory);

            employeeRepository.addEmployeeTicketCategory(ticketCategory)
                .$promise
                .then(
                    function (resultEmployee) {
                        // success case
                        $scope.resultData = resultEmployee;
                        appRepository.showAddSuccessGritterNotification();
                        $scope.close();
                        $('#dvTicketCategory').modal('hide');
                    }, function (response) {
                        // failure case
                        console.log("saveEmployeeTicketCategory save - Error !");
                        appRepository.showErrorGritterNotification();
                        $scope.errors = response.data;
                    }
                );
        };

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