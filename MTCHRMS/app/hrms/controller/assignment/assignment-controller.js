/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('AssignmentController',
[
    '$scope', '$uibModal', 'appRepository', 'validationRepository', 'employeeRepository', 'departmentRepository', 'courseRepository', 'assignmentRepository', '$location', '$routeParams',
    function ($scope, $uibModal, appRepository, validationRepository, employeeRepository, departmentRepository, courseRepository, assignmentRepository, $location, $routeParams) {

        console.log("course controller");
        $('#liStaffTraining').addClass('active');
        //$scope.myname = "yahoo";
        $scope.isBusy = false;

        $scope.loadCourses = function (forceRefresh, done) {
            if (typeof forceRefresh === 'undefined') { forceRefresh = false; }
            $scope.isBusy = true;
            courseRepository.getAllCourses(forceRefresh)
                .then(function (response) {
                    $scope.courses = response;
                }, function(err) {
                    //alert("error");
                })
                .then(function () {
                    $scope.isBusy = false;
                    if (typeof done !== 'undefined') { done(); }
                });
        };

        $scope.loadCategories = function () {
            $scope.categories = validationRepository.getTrainingCategories;
        };

        $scope.save = function(course) {
            
            $scope.errors = [];
            if (angular.isUndefined(course.statusId) || course.statusId == 'false' || course.statusId == 0) {
                course.statusId = 0;
            } else {
                course.statusId = 1;
            }
            courseRepository.addCourse(course)
                .$promise
                .then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    $scope.loadCourses(true, function () {
                        $location.url('/' + $scope.mainPortal + '/training/courses');
                    });
                    //console.log("save - Successfully !");
                    
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveAddNew = function (course) {
            $scope.errors = [];

            var clearDept = {
                code: "",
                name: "",
                statusId: ""
            };
            if (angular.isUndefined(manufacturer.statusId) || manufacturer.statusId == 'false' || manufacturer.statusId == 0) {
                manufacturer.statusId = 0;
            } else {
                manufacturer.statusId = 1;
            }
            courseRepository.addCourse(course).$promise.then(
                function() {
                    // success case
                    $scope.departmentForm.$setPristine();
                    $scope.department = clearDept;
                    console.log("saveAddNew - Successfully !");

                    appRepository.showAddSuccessGritterNotification();

                }, function(response) {
                    // failure case
                    $scope.errors = response.data;
                    appRepository.showErrorGritterNotification();
                    console.log("saveAddNew - Error !");
                }
            );
        };

        $scope.edit = function (course) {
            $scope.errors = [];
            courseRepository.editCourse(course).then(
                function() {
                    // success case
                    appRepository.showUpdateSuccessGritterNotification();
                    $scope.loadCourses(true, function () {
                        $location.url('/' + $scope.mainPortal + '/training/courses');
                    });
                }, function(err) {
                    // failure case
                    console.log("edit - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = err.data;
                }
            );
        };

        $scope.search = {
            departmentId: 0,
            yearId: 0
        };
        $scope.loadAssignment = function () {
            if ($routeParams.id != undefined) {
                employeeRepository.getSingleEmployee($routeParams.id)
                    .$promise.then(function (response) {
                        $scope.emp = response[0];
                });
            }

            $scope.years = validationRepository.getTrainingYears;

            departmentRepository.getAllDepartment()
               .then(function (response) {
                   $scope.departments1 = response;
                   $scope.departments2 = response;
                   if ($routeParams.param01 != undefined && $routeParams.param02 != undefined) {
                       $scope.search.departmentId = $routeParams.param01;
                       $scope.search.yearId = $routeParams.param02;
                       $scope.assignmentSearch($scope.search);
                   }
               }, function () {
                   //alert("error");
               })
               .then(function () { $scope.isBusy = false; });

            courseRepository.getAllCourses()
                .then(function (response) {
                    $scope.courses = response;
                }, function (err) {
                    //alert("error");
                })
                .then(function () {
                    $scope.isBusy = false;
                    if (typeof done !== 'undefined') { done(); }
                });

           // used for assignment search screen
            
        }

        $scope.assignmentSearch = function (search) {
            $scope.isBusy = true;
            assignmentRepository.getAssignments(search)
                .then(function (response) {
                    console.log("assignment", response);
                    if (response.status == 200) {
                        $scope.assignments = response.assignments;
                        $scope.isBusy = false;
                    }
                }, function (error) {
                    console.log(error);
                    $scope.isBusy = false;
                });
        }

        $scope.assignment = {
            employeeId:0,
            departmentId:0,
            designation:'',
            yearId:0,
            consultantDepartmentId:0,
            consultantEmployeeId:0,
            advisorDepartmentId:0,
            advisorEmployeeId:0,
            statusId:1, // need to change that
            courseDetails: []
        };

        $scope.addCourseAssignment = function () {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/app/hrms/templates/tr/assignment/assignment-modal-add.html',
                controller: 'AssignmentModalController',
                controllerAs: 'vm',
                size: 'lg',
                animation: true,
                resolve: {
                    items: function () {
                        return 'yes';
                    }
                }
            });

            modalInstance.result.then(function (details) {
                //$scope.selected = selectedItem;
                console.log('added', details);
                $scope.assignment.courseDetails.push(details);
            }, function () {
                console.log('modal dismissed');
            });
        }

        $scope.loadConsultants = function (id) {
            $scope.consultants = employeeRepository.getEmployeesByDepartmentId(id);
        }

        $scope.loadAdvisorEmployee = function (id) {
            $scope.advisors = employeeRepository.getEmployeesByDepartmentId(id);
        }

        $scope.saveEmployeeAssignment = function (assign) {
            assign.employeeId = $scope.emp.id;
            assign.departmentId = $scope.emp.departmentId;
            assign.designation = $scope.emp.designationAr;

            console.log(assign);
            
            assignmentRepository.addAssignment(assign).$promise
                .then(function (response) {
                    console.log('success', response);
                    appRepository.showAddSuccessGritterNotification();
                    $location.url('/' + $scope.mainPortal + '/training/assignment/list/?param01=' + assign.departmentId + '&param02=' + assign.yearId);

                }, function (err) {
                    console.log('error', err);
                    appRepository.showErrorGritterNotification();
                });
        }

        $scope.upadteEmployeeAssignment = function (assign) {
            console.log(assign);
            assignmentRepository.updateAssignment(assign)
                .then(function (response) {
                    console.log('success', response);
                    appRepository.showAddSuccessGritterNotification();
                    $location.url('/' + $scope.mainPortal + '/training/assignment/list/?param01=' + assign.departmentId + '&param02=' + assign.yearId);
                }, function (err) {
                    console.log('error', err);
                    appRepository.showErrorGritterNotification();
                });
        }

        $scope.loadEmployeeAssignment = function () {

            if ($routeParams.id != undefined) {
                assignmentRepository.getAssignment($routeParams.id)
                    .then(function (response) {
                        if (response.status == 200) {
                            console.log(response.assignment.courseDetails)
                            $scope.loadConsultants(response.assignment.consultantDepartmentId);
                            $scope.loadAdvisorEmployee(response.assignment.advisorDepartmentId)
                            $scope.assignment = response.assignment;
                            angular.forEach($scope.assignment.courseDetails, function (row, index) {
                                console.log('row',row);
                                $scope.assignment.courseDetails[index].machineName = 'machine name';
                            });
                        }
                    }, function (err) {
                        console.log(err);
                    });
            }
        }

        //alert($routeParams.id);
        $scope.getCourse = function() {
            if ($routeParams.id != undefined) {
                $scope.course = courseRepository.getCourseById($routeParams.id);
            }
        }

    }
]);
