
'use strict';

hrmsModule.controller('ServicesController',
[
    '$scope', 'appRepository', 'servicesRepository', 'employeeRepository', '$location', '$routeParams',
    'departmentRepository', 'validationRepository', 'ModalService',
    function ($scope, appRepository, servicesRepository, employeeRepository, $location, $routeParams,
        departmentRepository, validationRepository, ModalService) {

        var vm = this;
        vm.employees = [2];
        vm.loadEmployee = function (index, id) {
            vm.employees[index] = employeeRepository.getEmployeesByDepartmentId(id);
        }

        vm.loadEmployeeLeaveTicketDetail = function () {
            vm.isBusy = false;
            departmentRepository.getAllDepartment()
                .then(function(response) {
                    vm.departments = response;
                }, function (err) { });
            vm.leavetypes = validationRepository.getLeaveTypes;
            // for applying new leave, then load current user data, otherwise load employee leave data
            if (typeof $routeParams.id === 'undefined') {
                employeeRepository.getEmployeeLeaveTicketDetailById($scope.authentication.employeeId)
                    .$promise
                    .then(function (response) {
                        vm.employee = response;
                    }, function (err) { })
                    .then(function () {
                        vm.isBusy = true;
                    });
            } else {
                servicesRepository.getLeave($routeParams.id)
                    .$promise
                    .then(function (response) {
                        console.log(response.leave);
                        vm.leave = response.leave;

                        employeeRepository.getEmployeeLeaveTicketDetailById(vm.leave.employeeDefId)
                                            .$promise
                                            .then(function (response) {
                                                vm.employee = response;
                                            }, function () { })
                                            .then(function () {
                                                vm.isBusy = true;
                                            });
                }, function (err) { });   
            }
        }

        vm.saveApplyLeave = function (ctrl, leave) {
            leave.leaveYearId = vm.employee.leaveYearCurrentId;
            leave.employeeDefId = vm.employee.id;
            console.log("leave", leave);
            vm.errors = [];

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
                                vm.errors = response.data;
                            });
                        
                    } else {
                        //$location.url('/INVPortal/item/detail/' + id);
                    }
                    console.log(result.resultData);
                });
            });
        }


        vm.acceptLeave = function (ctrl, leave) {
            
            if (leave.statusId==5200) {
                leave.alternateEmployeeDecision = 1;
            }else if (leave.statusId==5201) {
                leave.lineManagerDecision = 1;
            }else if (leave.statusId==5202) {
                leave.departmentManagerDecision = 1;
            } else if (leave.statusId == 5203) {
                leave.hREmployeeId = $scope.authentication.employeeId;
                leave.hREmployeeDecision = 1;
            }
            vm.errors = [];
            console.log("leave", leave);
            //appRepository.showPageBusyNotification(ctrl, '<i class="icon-ok"></i>&nbsp;Saving...');

            ModalService.showModal({
                templateUrl: "/app/common/templates/modal/confirm-modal.html",
                controller: "HRMSServicesModalController",
                inputs: {
                    title: "Confirm Leaves",
                    messagebody: "Are you sure about your replacement ?",
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
                                appRepository.hidePageBusyNotification(ctrl, '<i class="icon-ok"></i>&nbsp;Save');
                                console.log("save - Error !");
                                appRepository.showErrorGritterNotification();
                                vm.errors = response.data;
                            });

                    } else {
                        //$location.url('/INVPortal/item/detail/' + id);
                    }
                    console.log(result.resultData);
                });
            });
        }
}]);