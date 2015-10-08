/// <reference path="../../module/hrms-module.js" />
/// <reference path="employee-repository.js" />

'use strict';

hrmsModule.controller('EmployeeController',
[
    '$scope', 'appRepository', 'employeeRepository', '$location', '$routeParams', 'departmentRepository', 'validationRepository', 'ModalService', 'EmployeeStream',
    function ($scope, appRepository, employeeRepository, $location, $routeParams, departmentRepository, validationRepository, ModalService, EmployeeStream) {

        console.log("employee controller");

        $scope.dateJoin = {
            startDate: null,
            endDate: null
        };

        $scope.dateAge = {
            startDate: null,
            endDate: null
        };


        $scope.isBusy = false;

        //'#joinDateRange').daterangepicker();
        //$('#joiningDate span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));

        $('#joiningDate').daterangepicker({
            format: 'DD/MM/YYYY',
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            minDate: '01/01/2010',
            maxDate: moment(),
            //dateLimit: { days: 60 },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {

                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
                'Last Year': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')]
            },
            opens: 'left',
            drops: 'down',
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-primary',
            cancelClass: 'btn-default',
            separator: ' to ',
            locale: {
                applyLabel: 'Apply',
                cancelLabel: 'Clear',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                firstDay: 1
            }
        }, function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $scope.dateJoin.startDate = start.toISOString();
            $scope.dateJoin.endDate = end.toISOString();
            $('#joiningDate span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        });

        $('#joiningDate').on('cancel.daterangepicker', function (ev, picker) {
            //do something, like clearing an input
            //console.log(start.toISOString(), end.toISOString(), label);
            $('#joiningDate  span').html('');
        });

        $('#birthDate').daterangepicker({
            format: 'DD/MM/YYYY',
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            minDate: '01/01/1950',
            maxDate: moment().subtract(20, 'year'),
            //dateLimit: { days: 60 },
            showDropdowns: true,
            showWeekNumbers: true,
            timePicker: false,
            timePickerIncrement: 1,
            timePicker12Hour: true,
            ranges: {
                '50 or more': [moment().subtract(80, 'year'), moment().subtract(50, 'year')],
                '40 to 50 Age': [moment().subtract(50, 'year'), moment().subtract(40, 'year')],
                '30 to 40 Age': [moment().subtract(40, 'year'), moment().subtract(30, 'year')],
                '20 to 30 Age': [moment().subtract(30, 'year'), moment().subtract(20, 'year')],
            },
            opens: 'left',
            drops: 'down',
            buttonClasses: ['btn', 'btn-sm'],
            applyClass: 'btn-primary',
            cancelClass: 'btn-default',
            separator: ' to ',
            locale: {
                applyLabel: 'Apply',
                cancelLabel: 'Clear',
                fromLabel: 'From',
                toLabel: 'To',
                customRangeLabel: 'Custom',
                daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                monthNames: ['Jan', 'Feb', 'March', 'April', 'May', 'June', 'July', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
                firstDay: 1
            }
        }, function (start, end, label) {
            console.log(start.toISOString(), end.toISOString(), label);
            $scope.dateAge.startDate = start.toISOString();
            $scope.dateAge.endDate = end.toISOString();
            $('#birthDate span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        });

        $('#birthDate').on('cancel.daterangepicker', function (ev, picker) {
            //do something, like clearing an input
            //console.log(start.toISOString(), end.toISOString(), label);
            $('#birthDate  span').html('');
        });


        $scope.daysDiff = function (start) {
            return moment(start).diff(moment(new Date()), 'day');
        };

        $scope.checkLeavePeriod = function(date1, date2) {
            var currentDate = new Date();
            var firstDate = new Date(date1);
            var secondDate = new Date(date2);

            return (currentDate >= firstDate && currentDate <= secondDate);
        }

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
                    title: "1", // "Add New Qualification",
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
                    title: "2", // "Update Qualification",
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
            if (confirm("Are you sure to delete this record ?") === true) {
                employeeRepository.deleteEmployeeQualification(qualification)
                    .$promise
                    .then(function () {
                        console.log($scope.employee[0].employeeQualifications);
                        appRepository.showDeleteGritterNotification();
                        $scope.employee[0].employeeQualifications.pop(qualification);
                    }, function (error) {
                        appRepository.showErrorGritterNotification();
                    });
            }
        };

        $scope.showPreviousEmployement = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-previous-employement.html",
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
                    $scope.employee[0].childrens.push(result.resultData);
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

        $scope.showAddContract = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-contract-add.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //$scope.employee[0].previousEmployments.push(result.resultData);
                });

            });
        };

        $scope.showLeaveCategory = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-leave-category.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //$scope.employee[0].previousEmployments.push(result.resultData);
                });

            });
        };

        $scope.showTicketCategory = function (id) {
            ModalService.showModal({
                templateUrl: "/app/hrms/templates/hrms/employee/employee-ticket-category.html",
                controller: "EmployeeModalController",
                inputs: {
                    title: "",
                    parentId: id,
                    employeePassport: {},
                    employeeVisa: {},
                    employeeQualification: {},
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    //$scope.employee[0].previousEmployments.push(result.resultData);
                });

            });
        };

        // Modal service end -------------------

        $scope.loadEmployees = function () {
            $scope.isBusy = false;
            $scope.employees = employeeRepository.getAllEmployees();
            $scope.employees.$promise.then(function() {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function () { $scope.isBusy = true; });
        };


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
        $scope.departments.$promise.then(function () {
            console.log("Department reach");
            //console.log("department id ", $scope.employee.postedTo);
            
        }, function() {
            
        });

        $scope.nationalities = validationRepository.getNationalities; //validationRepository.getAllDetailsByValidationId(2);
        $scope.countries = validationRepository.getCountries; //validationRepository.getAllDetailsByValidationId(3);
        $scope.maritals = validationRepository.getMaritalStatus;  //validationRepository.getAllDetailsByValidationId(4);
        $scope.genders = validationRepository.getGenders;   //validationRepository.getAllDetailsByValidationId(5);
        $scope.employeeStatus = validationRepository.getEmployeeStatus;
        

        if ($routeParams.id != undefined) {
            $scope.isBusy = false;
            employeeRepository.getEmployeeDetailById($routeParams.id)
                .$promise
                .then(function (response) {
                    $scope.employee = response;
                }, function () { })
                .then(function () {
                    $scope.isBusy = true;
                });
            //console.log($scope.employee);
        }
        
        $scope.save = function(employeeDef) {
            $scope.errors = [];
            console.log(employeeDef);
            employeeRepository.addEmployee(employeeDef)
                .$promise
                .then(
                function(resultEmployeeDef) {
                    // success case
                    console.log("save - Successfully !");
                    appRepository.showAddSuccessGritterNotification();
                    console.log('/' + $scope.mainPortal + '/employee/detail/' + resultEmployeeDef.id);
                    $location.url('/' + $scope.mainPortal + '/employee/detail/' + resultEmployeeDef.id);
                }, function(error) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = error.data;
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
                    $scope.resetForm();
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
                    console.log('/' + $scope.mainPortal + '/employee/detail/' + employeeDef.id);
                    $location.url('/' + $scope.mainPortal + '/employee/detail/' + employeeDef.id);
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

        $scope.employee = {};

        $scope.clearSearch = function () {
            $scope.errors = [];

            $scope.employee.employeeCode = "";
            $scope.employee.employeeName = "";
            $scope.employee.postedTo = 0;
            $scope.employee.statusId = 0;
            $scope.employee.employeeGenderId = 0;
            $scope.employee.condition = 0;
            $scope.employee.nationalityId = 0;

            $scope.dateAge.startDate = null;
            $scope.dateAge.endDate = null;

            $scope.dateJoin.startDate = null;
            $scope.dateJoin.endDate = null;

            $('#birthDate span').html('');
            $('#joiningDate span').html('');

        };

        $scope.clearSearch();

        //console.log($routeParams);
        function getSearchData(employee) {
            $scope.empSearchResults = employeeRepository.getEmployeesSearchList(employee);
            $scope.empSearchResults
                .$promise
                .then(function(response) {
                    console.log(response);

                }, function(error) {

                })
                .then(function() {
                    $scope.isBusy = false;
                });
        }

        if ($routeParams.param01 != undefined) {
            $scope.clearSearch();
            $scope.employee.postedTo = $routeParams.param01;
            getSearchData($scope.employee);
            console.log("first param reach");
            
        }

        if ($routeParams.param02 != undefined) {
            $scope.clearSearch();
            $scope.employee.nationalityId = $routeParams.param02;
            getSearchData($scope.employee);
            console.log("second param reach");
            
        }

        $scope.employeeSearch = function (employee) {
            $scope.errors = [];
            $scope.isBusy = true;

            if ($scope.dateJoin.startDate != null) {
                employee.joiningStartDate = $scope.dateJoin.startDate;
                employee.joiningEndDate = $scope.dateJoin.endDate;
            }

            if ($scope.dateAge.startDate != null) {
                employee.AgeStartDate = $scope.dateAge.startDate;
                employee.AgeEndDate = $scope.dateAge.endDate;
            }

            getSearchData(employee);

        };


    }
]);

