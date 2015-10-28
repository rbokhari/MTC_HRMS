
'use strict';

hrmsModule.controller('ServicesController',
[
    '$scope', 'appRepository', 'servicesRepository', 'employeeRepository', '$location', '$routeParams', 'departmentRepository', 'validationRepository',
    function ($scope, appRepository, servicesRepository, employeeRepository, $location, $routeParams, departmentRepository, validationRepository) {


        $scope.loadEmployeeLeaveTicketDetail = function () {
            $scope.isBusy = false;

            $scope.leavetypes = validationRepository.getLeaveTypes;

            employeeRepository.getEmployeeLeaveTicketDetailById($routeParams.id)
                .$promise
                .then(function (response) {
                    $scope.employee = response;
                }, function () { })
                .then(function () {
                    $scope.isBusy = true;
                });
        }

        $scope.saveApplyLeave = function (leave) {
            leave.leaveYearId = $scope.employee.leaveYearCurrentId;
            leave.statusId = 1;
            leave.employeeDefId = $scope.employee.id;
            console.log("leave", leave);
            $scope.errors = [];
            servicesRepository.applyLeave(leave).$promise.then(
                function () {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/' + $scope.mainPortal);
                }, function (response) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = response.data;
                }
            );
        }
}]);