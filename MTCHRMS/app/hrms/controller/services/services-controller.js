
'use strict';

hrmsModule.controller('ServicesController',
[
    '$scope', 'appRepository', 'servicesRepository', 'employeeRepository', '$location', '$routeParams',
    'departmentRepository', 'validationRepository', 'ModalService',
    function ($scope, appRepository, servicesRepository, employeeRepository, $location, $routeParams,
        departmentRepository, validationRepository, ModalService) {

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

        $scope.saveApplyLeave = function (ctrl, leave) {
            leave.leaveYearId = $scope.employee.leaveYearCurrentId;
            leave.statusId = 1;
            leave.employeeDefId = $scope.employee.id;
            console.log("leave", leave);
            $scope.errors = [];

            //appRepository.showPageBusyNotification(ctrl, '<i class="icon-ok"></i>&nbsp;Saving...');

            ModalService.showModal({
                templateUrl: "/app/common/templates/modal/confirm-modal.html",
                controller: "HRMSServicesModalController",
                inputs: {
                    title: "Confirm Leaves",
                    messagebody: "Are you sure about your leave application ?",
                    parentId: 2,
                    resultData: {}
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close.then(function (result) {
                    $('.modal-backdrop').remove();
                    if (result.resultData === 1) {
                        appRepository.showPageBusyNotification(ctrl, '<i class="icon-ok"></i>&nbsp;Sending ...');

                        servicesRepository.applyLeave(leave)
                            .$promise.then(function () {
                                appRepository.hidePageBusyNotification(ctrl, '<i class="icon-ok"></i>&nbsp;Save');
                                appRepository.showAddSuccessGritterNotification();
                                console.log("save - Successfully !");
                                $location.url('/' + $scope.mainPortal);
                            }, function (response) {
                                // failure case
                                console.log("save - Error !");
                                appRepository.showErrorGritterNotification();
                                $scope.errors = response.data;
                            });
                        
                    } else {
                        //$location.url('/INVPortal/item/detail/' + id);
                    }
                    console.log(result.resultData);
                });
            });


            
        }
}]);