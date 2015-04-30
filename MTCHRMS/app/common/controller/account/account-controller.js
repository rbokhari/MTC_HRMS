
//'use strict';

accModule.controller('AccountController',
[
    '$scope', 'authRepository', '$location','$window','appModules',
    function ($scope, authRepository, $location, $window, appModules) {

        console.log("account controller");
        $scope.loginData = {
            userName: "",
            password: ""
        };
        $scope.message = "";
        $scope.login = function () {
            authRepository.login($scope.loginData)
                .then(function (response) {
                    //moduleDetail();
                //roleDetail();
                    authRepository.fillAuthData();
                    
                    //$scope.authentication = authRepository.authentication;
                    //alert($scope.authentication.moduleId);
                    $window.location.href = '/index';
                    //if ($scope.authentication.moduleId == appModules.HRMS_Module) {
                    //    // check language also here
                    //    $window.location.href = '/HRMSPortal';
                    //} else if ($scope.authentication.moduleId == appModules.INV_Module) {
                    //    $window.location.href = '/INVPortal';
                    //}
                    
                    //$window.location.href = '/HRMSPortal';
                },
                function (err) {
                    console.log(err.status);
                    //$scope.message = err.error_description;
                    //$scope.message = err.error;
                    if (err.status == undefined) {
                        $scope.message = "Invalid Username or Password !";
                    }
                    else if (err.status == 500) {
                        $scope.message = "User not allowed to login system !";
                    }
                });
        };

        $scope.authData = function() {
            authRepository.fillAuthData();
            $scope.authentication = authRepository.authentication;
        };

        $scope.redirect = function () {
            authRepository.fillAuthData()
                .then(function(res) {
                    $scope.authentication = authRepository.authentication;
                    if ($scope.authentication.moduleId == appModules.HRMS_Module) {
                        // check language also here
                        $window.location.href = '/HRMSPortal';
                    } else if ($scope.authentication.moduleId == appModules.INV_Module) {
                        $window.location.href = '/INVPortal';
                    }
                },
                function(err) {
                    console.log(err);
                    $window.location.href = '/Login';
                });
        };


        

        //var userDetail = function(id) {
        //    accountRepository.getUserById(id)
        //        .then(function(response) {
        //            $scope.userInfo = response.data;

        //        }, function(err) {

        //        });
        //};

        //var roleDetail = function (id) {
        //    accountRepository.getRoleById(id)
        //        .then(function (response) {
        //            $scope.roleInfo = response.data;

        //        }, function (err) {

        //        });
        //};

        //var moduleDetail = function (id) {
        //    accountRepository.getModuleById(id)
        //        .then(function (response) {
        //            $scope.moduleInfo = response.data;

        //        }, function (err) {

        //        });
        //};


    }
]);
