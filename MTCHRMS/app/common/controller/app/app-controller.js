/// <reference path="../../module/hrms-module.js" />

'use strict';
accModule.controller('AppController',
[
    '$location', 'authRepository', '$window', 'appRepository', '$translate', '$timeout', '$interval', 'appRoles', 'servicesRepository',
    function ($location, authRepository, $window, appRepository, $translate, $timeout, $interval, appRoles, servicesRepository) {

        console.log("app controller");

        var vm = this;

        vm.message = "";
        vm.appRoles = appRoles;

        if ($location.path().indexOf('/HRMSPortalAr') == 0) {
            vm.lang = "ar_OM";
            vm.mainPortal = "HRMSPortalAr";
            vm.activeModule = 1;
            $translate.use('ar_OM');
        } else if ($location.path().indexOf('/HRMSPortal') == 0) {
            vm.lang = "en_US";
            vm.mainPortal = "HRMSPortal";
            vm.activeModule = 1;
            $translate.use('en_US');
        } else if ($location.path().indexOf('/INVPortal') == 0) {
            vm.lang = "en_US";
            vm.mainPortal = "INVPortal";
            vm.activeModule = 2;
            $translate.use('en_US');
        }

        vm.currentDateNow = new Date();
        //$translate.use('en_US');
       
        //$scope.translate = function () {
        //    translationService.getTranslation($scope, $scope.selectedLanguage);
        //};

        //Init
        //$scope.selectedLanguage = 'en';
        //$scope.translate();
        vm.authentication = authRepository.authentication;

        vm.tab = 1;       // set active tab bydefault
        // set which tab to activate
        vm.setTab = function (setTab) {
            this.tab = setTab;
        };
        // verify if tab is selected or not, use for ng-class 
        vm.isTabSelected = function (checkTab) {
            return this.tab === checkTab;
        };

        vm.lock = function () {
            $window.location.href = '/lock';
        };

        var count = 0;
        function loadNotification() {
            count += 1;
            console.log("calling... :" + count);
            servicesRepository.getNotifications()
                .then(function (response) {
                    vm.messages = response.notifications;
            }, function (err) { });
        };

        vm.daysCount = function (start) {
            return moment(start).startOf('hour').fromNow();
        };
        

        vm.logOut = function () {
            
            authRepository.logOut();
            vm.authentication = authRepository.authentication;
            //$location.path('/home');
            $window.location.href = '/login';
            appRepository.showSuccessGritterNotification();
        };

        vm.lockLogin = function () {
            vm.loginData.userName = vm.authentication.userName;
            authRepository.lockLogin(vm.loginData)
                .then(function (response) {
                    $window.location.href = '/HRMSPortal';
                }, function (err) {
                    console.log(err);
                    vm.message = "Invalid Password !";
                });
        };

        vm.getAuthenticationData = function () {
            vm.authentication = authRepository.authentication;
        };

        $timeout(loadNotification, 1000);

        $interval(loadNotification, 50000);
    }
]);
