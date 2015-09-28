/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

hrmsModule.controller('TicketController',
[
    '$scope', 'appRepository', 'ticketRepository', 'validationRepository', 'employeeRepository', '$location', '$routeParams',
    function ($scope, appRepository, ticketRepository, validationRepository, employeeRepository, $location, $routeParams) {

        console.log("ticket controller");
        //$scope.myname = "yahoo";
        $scope.isBusy = false;

        $scope.loadTicket = function() {
            $scope.isBusy = true;
            $scope.tickets = ticketRepository.getAllTickets();

            $scope.tickets.$promise.then(function () {
                    //alert("success");
                }, function() {
                    //alert("error");
                })
                .then(function() { $scope.isBusy = false; });
        };

        $scope.loadTicketAdd = function () {

            $scope.schedules = validationRepository.getLeaveSchedules;
            $scope.eligibilities = validationRepository.getTicketEligibilities;

        };

        $scope.save = function(ticket) {
            
            $scope.errors = [];
            ticketRepository.addTicket(ticket).$promise.then(
                function (response) {
                    appRepository.showAddSuccessGritterNotification();
                    console.log("save - Successfully !");
                    $location.url('/' + $scope.mainPortal + '/ticket');
                }, function(err) {
                    // failure case
                    console.log("save - Error !");
                    appRepository.showErrorGritterNotification();
                    $scope.errors = err.data;
                }
            );
        };

        $scope.saveAddNew = function(ticket) {
            $scope.errors = [];

            var clearTicket = {
                departmentCode: "",
                departmentName: "",
                statusId: ""
            };

            ticketRepository.addTicket(ticket).$promise.then(
                function() {
                    // success case
                    $scope.ticketForm.$setPristine();
                    $scope.ticket = clearTicket;
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

        $scope.edit = function (ticket) {
            $scope.errors = [];
            ticketRepository.editTicket(ticket).then(
                function() {
                    // success case
                    console.log("edit done - Successfully !");
                    appRepository.showUpdateSuccessGritterNotification();

                    $location.url('/' + $scope.mainPortal + '/ticket');
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
            $scope.ticket = ticketRepository.getTicketById($routeParams.id);
        }

    }
]);
