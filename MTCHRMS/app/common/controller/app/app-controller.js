/// <reference path="../../module/hrms-module.js" />

'use strict';
accModule.controller('AppController',
[
    '$scope', '$location', 'authRepository', '$window', 'appRepository', '$translate', '$timeout', '$interval', 'appRoles', 'servicesRepository',
    function ($scope, $location, authRepository, $window, appRepository, $translate, $timeout, $interval, appRoles, servicesRepository) {

        console.log("app controller");

        //var appvm = this;

        $scope.message = "";
        $scope.appRoles = appRoles;

        if ($location.path().indexOf('/HRMSPortalAr') == 0) {
            $scope.lang = "ar_OM";
            $scope.mainPortal = "HRMSPortalAr";
            $scope.activeModule = 1;
            $translate.use('ar_OM');
        } else if ($location.path().indexOf('/HRMSPortal') == 0) {
            $scope.lang = "en_US";
            $scope.mainPortal = "HRMSPortal";
            $scope.activeModule = 1;
            $translate.use('en_US');
        } else if ($location.path().indexOf('/INVPortal') == 0) {
            $scope.lang = "en_US";
            $scope.mainPortal = "INVPortal";
            $scope.activeModule = 2;
            $translate.use('en_US');
        }

        $scope.currentDateNow = new Date();
        //$translate.use('en_US');
       
        //$scope.translate = function () {
        //    translationService.getTranslation($scope, $scope.selectedLanguage);
        //};

        //Init
        //$scope.selectedLanguage = 'en';
        //$scope.translate();
        $scope.authentication = authRepository.authentication;

        $scope.tab = 1;       // set active tab bydefault
        // set which tab to activate
        $scope.setTab = function (setTab) {
            this.tab = setTab;
        };
        // verify if tab is selected or not, use for ng-class 
        $scope.isTabSelected = function (checkTab) {
            return this.tab === checkTab;
        };

        $scope.lock = function () {
            $window.location.href = '/lock';
        };

        var count = 0;
        function loadNotification() {
            count += 1;
            console.log("calling... :" + count);
            servicesRepository.getNotifications()
                .then(function (response) {
                    $scope.messages = response.notifications;
            }, function (err) { });
        };

        $scope.daysCount = function (start) {
            return moment(start).startOf('hour').fromNow();
        };
        

        $scope.logOut = function () {
            
            authRepository.logOut();
            //vmappvm.authentication = authRepository.authentication;
            //$location.path('/home');
            $window.location.href = '/login';
            appRepository.showSuccessGritterNotification();
        };

        $scope.lockLogin = function () {
            $scope.loginData.userName = $scope.authentication.userName;
            authRepository.lockLogin(vm.loginData)
                .then(function (response) {
                    $window.location.href = '/HRMSPortal';
                }, function (err) {
                    console.log(err);
                    $scope.message = "Invalid Password !";
                });
        };

        //vm.getAuthenticationData = function () {
        //    alert("yahoo");
        //    console.log("auth data");
        //    vm.authentication = authRepository.authentication;
        //};

        $timeout(loadNotification, 1000);

        $interval(loadNotification, 50000);
    }
]);
