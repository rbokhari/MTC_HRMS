/// <reference path="../../module/hrms-module.js" />

'use strict';
hrmsModule.controller('AppController',
[
    '$scope', '$location', 'authRepository', '$window','appRepository', '$translate', 
    function ($scope, $location, authRepository, $window, appRepository, $translate) {

        console.log("app controller");

        $scope.message = "";

        $scope.currentDateNow = new Date();
        $translate.use('en_US');

        //$scope.translate = function () {
        //    translationService.getTranslation($scope, $scope.selectedLanguage);
        //};

        //Init
        //$scope.selectedLanguage = 'en';
        //$scope.translate();

        $scope.lock = function () {
            $window.location.href = '/lock';
        };

        $scope.logOut = function () {
            
            authRepository.logOut();
            $scope.authentication = authRepository.authentication;
            //$location.path('/home');
            $window.location.href = '/login';
            appRepository.showSuccessGritterNotification();
        };

        $scope.lockLogin = function () {
            $scope.loginData.userName = $scope.authentication.userName;
            authRepository.lockLogin($scope.loginData)
                .then(function (response) {
                    $window.location.href = '/HRMSPortal';
                }, function (err) {
                    console.log(err);
                    $scope.message = "Invalid Password !";
                });
        };

        $scope.getAuthenticationData = function() {
            $scope.authentication = authRepository.authentication;
        };

    }
]);
