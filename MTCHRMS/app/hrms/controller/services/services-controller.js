
'use strict';

hrmsModule.controller('ServicesController',
[
    'authRepository', 'appRepository', 'servicesRepository', 'employeeRepository', '$location', '$routeParams',
    'departmentRepository', 'validationRepository', 'ModalService',
    function (authRepository, appRepository, servicesRepository, employeeRepository, $location, $routeParams,
        departmentRepository, validationRepository, ModalService) {

        var vm = this;

        vm.authentication = authRepository.authentication;

        vm.loadEmployee = function (id) {
            vm.employees = employeeRepository.getEmployeesByDepartmentId(id);
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
                employeeRepository.getEmployeeLeaveTicketDetailById(authRepository.authentication.employeeId)
                    .$promise
                    .then(function (response) {
                        vm.employee = response;
                    }, function () { })
                    .then(function () {
                        vm.isBusy = true;
                    });
            } else {
                console.log("else part");
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

                }, function (err) {
                    
                });


                
            }
        }

        vm.saveApplyLeave = function (ctrl, leave) {
            leave.leaveYearId = vm.employee.leaveYearCurrentId;
            leave.statusId = 1;
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
                                $location.url('/' + vm.mainPortal);
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
}]);