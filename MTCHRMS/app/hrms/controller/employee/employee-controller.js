﻿/// <reference path="../../module/hrms-module.js" />
/// <reference path="employee-repository.js" />

'use strict';

hrmsModule.controller('EmployeeController',
[
    '$scope', 'appRepository', 'employeeRepository', '$location', '$routeParams', 'departmentRepository', 'validationRepository', 'ModalService', 'EmployeeStream',
    function ($scope, appRepository, employeeRepository, $location, $routeParams, departmentRepository, validationRepository, ModalService, EmployeeStream) {

        console.log("employee controller");

        $scope.isBusy = false;

        $scope.daysDiff = function (start) {
            return moment(start).diff(moment(new Date()), 'day');
        };

        // bootstrap tab setting property and function for angularjs
        $scope.tab = 1;       // set active tab bydefault

        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };

        // verify if tab is selected or not, use for ng-class 
        $scope.isTabSelected = function (checkTab) {
            return this.tab === checkTab;
        };

        // Modal service start ----------------
        $scope.showPassport = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-passport.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Add New Passport",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function(modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //employee[0].employeePassports.splice(0, 0, resultEmployeePassport.data);
                    //console.log("show passport close : " + result.newPassport.id);
                    $scope.employee[0].employeePassports.push(result.resultData);
                    //$scope.complexResult = "Name: " + result.name + ", age: " + result.age;
                    //$('.modal').modal('hide');
                    //modal.element.close();
                });
                
            });
        };

        $scope.editPassport = function (passport) {
            //console.log(passport);
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-passport.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Update Passport",
                    parentId: passport.employeeDefId,
                    employeePassport: passport,
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deletePassport = function (passport) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeePassport(passport)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.employee[0].employeePassports.pop(passport);
                    }, function(error) {
                        appRepository.showErrorGritterNotification();
                });
            }
        };


        $scope.showVisa = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-visa.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Add New Visa",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.employee[0].employeeVisas.push(result.resultData);
                });

            });
        };

        $scope.editVisa = function (visa) {
            //console.log(visa);
            //var visaCopy = {};

            //angular.copy(visa, visaCopy);

            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-visa.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Update Visa",
                    parentId: visa.employeeDefId,
                    employeePassport: {},
                    employeeVisa: visa,
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteVisa = function(visa) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeeVisa(visa)
                    .$promise
                    .then(function() {
                        appRepository.showDeleteGritterNotification();
                        $scope.employee[0].employeeVisas.pop(visa);
                    }, function(error) {
                        appRepository.showErrorGritterNotification();
                    });
            } 
        };

        $scope.showQualification = function (id) {
            
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-qualification.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Add New Qualification",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.employee[0].employeeQualifications.push(result.resultData);
                });

            });
        };

        $scope.editQualification = function (qualification) {
            //console.log(qualification);
            //var visaCopy = {};
           
            //angular.copy(visa, visaCopy);
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-qualification.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Update Qualification",
                    parentId: qualification.employeeDefId,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: qualification,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
            });
        };

        $scope.deleteQualification = function (qualification) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeeQualification(qualification)
                    .$promise
                    .then(function () {
                        console.log($scope.employee[0].employeeQualifications);
                        appRepository.showDeleteGritterNotification();
                        console.log(qualification);
                        $scope.employee[0].employeeQualifications.pop(qualification);
                        console.log($scope.employee[0].employeeQualifications);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.showPreviousEmployement = function (id) {
            ModalService.showModal({
                templateUrl: "/templates/hrms/employee/employee-previous-employement.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Add New Employement",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $scope.employee[0].previousEmployments.push(result.resultData);
                });

            });
        };

        $scope.deletePreviousEmployement = function (previousEmployement) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeePreviousEmployement(previousEmployement)
                    .$promise
                    .then(function () {
                        appRepository.showDeleteGritterNotification();
                        $scope.employee[0].previousEmployments.pop(previousEmployement);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };


        $scope.showChild = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-child.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Add New Child",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //console.log(result);
                    //var childGenderDetail = { 'nameEn': 'Male111' };

                    //result.push(childGenderDetail);
                    //console.log(result);
                    $scope.employee[0].childrens.push(result.resultData);
                    //$scope.employee[0].childrens[2].childGenderDetail.push({ 'nameEn': 'Male' });
                });

            });
        };

        $scope.deleteEmployeeChild = function (employeeChild) {
            var x;
            if (confirm("Are you sure to delete this record ?") == true) {
                employeeRepository.deleteEmployeeChild(employeeChild)
                    .$promise
                    .then(function () {
                        appRepository.showAddSuccessGritterNotification();
                        $scope.employee[0].childrens.pop(employeeChild);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.showImageForm = function (id) {
            
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-image1.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "Update Picture",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //console.log(result);
                    $scope.employee[0].empPicture = result.resultData.empPicture;
                });

            });

        };
        // Modal service end -------------------

        $scope.employees = employeeRepository.getAllEmployees();
        $scope.employees.$promise.then(function() {
                //alert("success");
            }, function() {
                //alert("error");
            })
            .then(function () { $scope.isBusy = true; });

        // Call employee hub service
        EmployeeStream.on('addNewEmployee', function (empId, userId) {
            //console.log("empID: " + empId + " , userId: " + userId);
            $scope.singleEmp = employeeRepository.getSingleEmployee(empId);
            $scope.singleEmp.$promise.then(function() {
                $scope.employees.push($scope.singleEmp[0]);
                appRepository.showAddSuccessGritterNotification();
            });
        });

        $scope.departments = departmentRepository.getAllDepartment();
        $scope.nationalities = validationRepository.getAllDetailsByValidationId(2);
        $scope.countries = validationRepository.getAllDetailsByValidationId(3);
        $scope.maritals = validationRepository.getAllDetailsByValidationId(4);
        $scope.genders = validationRepository.getAllDetailsByValidationId(5);
        

        if ($routeParams.id != undefined) {
            $scope.isBusy = false;
            $scope.employee = employeeRepository.getEmployeeDetailById($routeParams.id);
            $scope.employee.$promise
                .then(function() {}, function() {})
                .then(function() { $scope.isBusy = true; });
        }
        
        $scope.save = function(employeeDef) {
            $scope.errors = [];

            employeeRepository.addEmployee(employeeDef)
                .$promise
                .then(
                function(resultEmployeeDef) {
                    // success case
                    console.log("save - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    $location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveAddNew = function(employeeDef) {
            $scope.errors = [];

            var clearDept = {
                departmentCode: "",
                departmentName: "",
                statusId: ""
            };

            employeeRepository.addEmployee(employeeDef).$promise.then(
                function() {
                    // success case
                    appRepository.showAddSuccessGritterNotification();

                    $scope.employeeForm.$setPristine();
                    //$scope.employee = clearDept;
                    console.log("saveAddNew - Successfully !");

                }, function(response) {
                    // failure case
                    $scope.errors = response.data;
                    console.log("saveAddNew - Error !");
                    appRepository.showErrorGritterNotification();
                }
            );
        };

        $scope.edit = function(employeeDef) {
            $scope.errors = [];
            //employeeRepository.editEmployee(employeeDef).then(
            employeeRepository.addEmployee(employeeDef)
                .$promise
                .then(
                function() {
                    // success case
                    appRepository.showUpdateSuccessGritterNotification();
                    console.log("edit - Successfully !");
                    $location.url('/HRMSPortal/employee/detail/' + employeeDef.id);
                }, function(response) {
                    // failure case
                    console.log("edit - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveEmployeeQualification = function(id, employeeQualification) {
            $scope.errors = [];
            employeeQualification.employeeDefId = id;
            employeeRepository.addEmployeeQualification(employeeQualification).$promise.then(
                function(resultEmployeeDef) {
                    // success case
                    console.log("saveEmployeeQualification - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    //$location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
                }, function(response) {
                    // failure case
                    console.log("saveEmployeeQualification - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveEmployeeMarital = function(id, employeeMarital) {
            $scope.errors = [];
            employeeMarital.employeeDefId = id;
            employeeRepository.addEmployeeMarital(employeeMarital).$promise.then(
                function(resultemployeeMarital) {
                    // success case
                    console.log("saveEmployeeMarital - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    //$location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
                }, function(response) {
                    // failure case
                    console.log("saveEmployeeMarital - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveEmployeeQualification = function (id, employeeQualification) {
            $scope.errors = [];
            employeeQualification.employeeDefId = id;
            employeeRepository.addEmployeeQualification(employeeQualification).$promise.then(
                function (resultemployeeMarital) {
                    // success case
                    console.log("saveEmployeeMarital - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    //$location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
                }, function (response) {
                    // failure case
                    console.log("saveEmployeeMarital - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveEmployeeKin = function (id, employeeKin) {
            $scope.errors = [];
            employeeKin.employeeDefId = id;
            employeeRepository.addEmployeeKin(employeeKin).$promise.then(
                function (resultemployeeKin) {
                    // success case
                    console.log("saveEmployeeKin - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    //$location.url('/HRMSPortal/employee/detail/' + resultEmployeeDef.id);
                }, function (response) {
                    // failure case
                    console.log("saveEmployeeKin - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.employeeContactNo = function() {
            $scope.isBusy = true;
            $scope.employeeContactNoList = employeeRepository.getEmployeesContactNoList();
            $scope.employeeContactNoList.$promise
                .then(function(response) {
                        //
                    },
                    function() {
                        //
                    })
                    .then(function() {
                        //alert("aa");
                        $scope.isBusy = false;
                    });
            
        };

    }
]);