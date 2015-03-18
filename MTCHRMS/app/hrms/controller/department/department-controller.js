/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('DepartmentController',
[
    '$scope', 'appRepository', 'departmentRepository', 'employeeRepository', '$location', '$routeParams',
    function($scope, appRepository, departmentRepository, employeeRepository, $location, $routeParams) {

        console.log("department controller");
        //$scope.myname = "yahoo";
        $scope.isBusy = false;

        $scope.loadDepartment = function() {
            $scope.isBusy = true;
            $scope.departments = departmentRepository.getAllDepartment();

            $scope.departments.$promise.then(function() {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() {
                    $scope.employees = employeeRepository.getAllEmployees();
                })
                .then(function() { $scope.isBusy = false; });
        };


        $scope.save = function(department) {
            
            $scope.errors = [];
            departmentRepository.addDepartment(department).$promise.then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/HRMSPortal/department');
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveAddNew = function(department) {
            $scope.errors = [];

            var clearDept = {
                departmentCode: "",
                departmentName: "",
                statusId: ""
            };

            departmentRepository.addDepartment(department).$promise.then(
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

        $scope.edit = function(department) {
            $scope.errors = [];
            departmentRepository.editDepartment(department).then(
                function() {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/HRMSPortal/department');
                }, function(response) {
                    // failure case
                    console.log("edit - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        //alert($routeParams.id);
        if ($routeParams.id != undefined) {
            $scope.department = departmentRepository.getDepartmentById($routeParams.id);
        }

    }
]);
