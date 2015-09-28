/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('LeaveController',
[
    '$scope', 'appRepository', 'leaveRepository', 'validationRepository', 'employeeRepository', '$location', '$routeParams',
    function ($scope, appRepository, leaveRepository, validationRepository, employeeRepository, $location, $routeParams) {

        console.log("leave controller");
        //$scope.myname = "yahoo";
        $scope.isBusy = false;

        $scope.loadLeave = function() {
            $scope.isBusy = true;
            $scope.leaves = leaveRepository.getAllLeaves();

            $scope.leaves.$promise.then(function (res) {
                    //alert("success");
                }, function(err) {
                    //alert("error");
                })
                .then(function() { $scope.isBusy = false; });
        };

        $scope.loadLeaveAdd = function () {

            $scope.schedules = validationRepository.getLeaveSchedules;
            $scope.types = validationRepository.getLeaveTypes;

        };

        $scope.save = function(leave) {
            
            $scope.errors = [];
            leaveRepository.addLeave(leave).$promise.then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/' + $scope.mainPortal + '/leave');
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        };

        $scope.saveAddNew = function (leave) {
            $scope.errors = [];

            var clearDept = {
                departmentCode: "",
                departmentName: "",
                statusId: ""
            };

            leaveRepository.addLeave(leave).$promise.then(
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

        $scope.edit = function (leave) {
            $scope.errors = [];
            leaveRepository.editLeave(leave).then(
                function() {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/' + $scope.mainPortal + '/leave');
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
            $scope.leave = leaveRepository.getLeaveById($routeParams.id);
        }

    }
]);
