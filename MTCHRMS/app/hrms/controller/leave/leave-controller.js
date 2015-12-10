/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('LeaveController',
[
    '$scope', 'appRepository', 'leaveRepository', 'validationRepository', 'employeeRepository', '$location', '$routeParams',
    function ($scope, appRepository, leaveRepository, validationRepository, employeeRepository, $location, $routeParams) {

        console.log("leave controller");
        var vm = this;
        //$scope.myname = "yahoo";
        vm.isBusy = false;

        vm.loadLeave = function () {
            vm.isBusy = true;
            vm.leaves = leaveRepository.getAllLeaves();

            vm.leaves.$promise.then(function (res) {
                    //alert("success");
                }, function(err) {
                    //alert("error");
                })
                .then(function () { vm.isBusy = false; });
        };

        vm.loadLeaveAdd = function () {
            vm.schedules = validationRepository.getLeaveSchedules;
            vm.types = validationRepository.getLeaveTypes;

        };

        vm.save = function (leave) {
            
            vm.errors = [];
            leaveRepository.addLeave(leave).$promise.then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/' + $scope.mainPortal + '/leave');
                }, function(response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    vm.errors = response.data;
                }
            );
        };

        vm.saveAddNew = function (leave) {
            vm.errors = [];

            var clearDept = {
                departmentCode: "",
                departmentName: "",
                statusId: ""
            };

            leaveRepository.addLeave(leave).$promise.then(
                function() {
                    // success case
                    vm.departmentForm.$setPristine();
                    vm.department = clearDept;
                    console.log("saveAddNew - Successfully !");

                    appRepository.showAddSuccessGritterNotification();

                }, function(response) {
                    // failure case
                    vm.errors = response.data;
                    appRepository.showErrorGritterNotification();
                    console.log("saveAddNew - Error !");
                }
            );
        };

        vm.edit = function (leave) {
            vm.errors = [];
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
                    vm.errors = response.data;
                }
            );
        };

        //alert($routeParams.id);
        if ($routeParams.id != undefined) {
            vm.leave = leaveRepository.getLeaveById($routeParams.id);
        }

    }
]);
