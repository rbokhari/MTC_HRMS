/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('CourseController',
[
    '$scope', 'appRepository', 'validationRepository', 'courseRepository', '$location', '$routeParams',
    function ($scope, appRepository, validationRepository, courseRepository, $location, $routeParams) {

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

        //alert($routeParams.id);
        $scope.getCourse = function() {
            if ($routeParams.id != undefined) {
                $scope.course = courseRepository.getCourseById($routeParams.id);
            }
        }

    }
]);
