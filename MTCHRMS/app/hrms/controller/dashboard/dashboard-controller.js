/// <reference path="../../module/hrms-module.js" />

'use strict';
hrmsModule.controller('DashboardController',
[
    '$scope', 'appRepository', 'departmentRepository', 'employeeRepository', 'validationRepository', '$location', '$routeParams', '$translate', 'appRoles',
    function ($scope, appRepository, departmentRepository, employeeRepository, validationRepository, $location, $routeParams, $translate, appRoles) {

        console.log("dashboard controller");
        

        //$scope.isHRMSModule1 = appRepository.isHRMSModule;
        $scope.appRoles = appRoles;
        $scope.isBusy = true;
        $scope.isProbationBusy = true;
        $scope.isContractBusy = true;
        $scope.isVisaBusy = true;
        $scope.isPassportBusy = true;
        $scope.isAppraisalBusy = true;

        $scope.isActiveNavigation = function (viewLocation) {
            var active = (viewLocation === $location.path());
            return active;
        };

        $scope.isActiveParentNavigation = function (viewLocation) {
            var active = ($location.path().indexOf(viewLocation) > -1);
            return active;
        };

        $scope.daysDiff = function (start) {
            var currentDate = new Date();
            //return moment.utc(moment(new Date()).diff(moment(start))).format("DD");
            //return moment(start).startOf('day').fromNow();
            return moment(start).diff(moment(new Date()), 'day');
        };

        $scope.daysDiffMoment = function (start) {
            var currentDate = new Date();
            //return moment.utc(moment(new Date()).diff(moment(start))).format("DD");
            return moment(start).startOf('day').fromNow();
        };

        $scope.appraisalDaysCount = function (start) {
            var appraisalDate = new Date(start);
            //console.log(appraisalDate.getFullYear() + ": " + appraisalDate.getMonth() + " ->:: " + appraisalDate.getDate());
            
            appraisalDate.setFullYear(new Date().getFullYear(), appraisalDate.getMonth(), appraisalDate.getDate());

            //console.log(new Date().getFullYear() + ": " + appraisalDate.getMonth() + " :: " + appraisalDate.getDate());
            //return moment.utc(moment(new Date()).diff(moment(start))).format("DD");
            return moment(appraisalDate).endOf('day').fromNow();
        };


        var date = new Date();
        $scope.oldDate = date.setDate(date.getDate() - 60);

        $scope.gteComparator = function(a, b) {
            return new Date(a) >= new Date(b);
        };

        // = departmentRepository.getAllDepartment();
        
        departmentRepository.getAllDepartment()
            .then(function (response) {
            //alert("success");
            $scope.departments = response;
        }, function () {
            //alert("error");
        })
            .then(function () { $scope.isBusy = false; });
        
        //$scope.departments = departmentRepository.getAllDepartment();
        $scope.nationalities = validationRepository.getAllDetailsByValidationId(2);
        $scope.employees = employeeRepository.getAllEmployees();

        $scope.loadProbation = function() {
            $scope.probations = employeeRepository.getEmployeesProbationExpiry();
            $scope.probations.$promise
                .then(function(response) {
                    //console.log(response);
                    //$scope.Probations = response.data;
                }, function() {})
                .then(function () { $scope.isProbationBusy = false; });
        };

        $scope.loadContract = function () {
            $scope.contracts = employeeRepository.getEmployeesContractExpiry();
            $scope.contracts.$promise
                .then(function (response) {
                    //console.log(response);
                    //$scope.Probations = response.data;
                }, function () { })
                .then(function () { $scope.isContractBusy = false; });
        };

        $scope.loadPassport = function () {
            $scope.passports = employeeRepository.getEmployeesPassportExpiry();
            $scope.passports.$promise
                .then(function (response) {
                    //console.log(response);
                    //$scope.Probations = response.data;
                }, function () { })
                .then(function () { $scope.isPassportBusy = false; });
        };

        $scope.loadVisa = function () {
            $scope.visas = employeeRepository.getEmployeesVisaExpiry();
            $scope.visas.$promise
                .then(function (response) {
                    //console.log(response);
                    //$scope.Probations = response.data;
                }, function () { })
                .then(function () { $scope.isVisaBusy = false; });
        };

        $scope.loadAppraisal = function () {
            $scope.appraisals = employeeRepository.getEmployeesAppraisalList();
            $scope.appraisals.$promise
                .then(function (response) {
                    //console.log(response);
                    //$scope.Probations = response.data;
                }, function () { })
                .then(function () { $scope.isAppraisalBusy = false; });
        };

        if ($routeParams.id != undefined) {
            $scope.department = departmentRepository.getDepartmentById($routeParams.id);
        }

    }
]);
