
//'use strict';

accModule.controller('AccountController',
[
    'authRepository', '$location','$window','appModules',
    function (authRepository, $location, $window, appModules) {

        console.log("account controller");
        var vm = this;

        vm.loginData = {
            userName: "",
            password: ""
        };
        vm.message = "";
        vm.login = function () {
            authRepository.login(vm.loginData)
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
                        vm.message = "Invalid Username or Password !";
                    }
                    else if (err.status == 500) {
                        vm.message = "User not allowed to login system !";
                    }
                });
        };

        vm.authData = function () {
            authRepository.fillAuthData();
            vm.authentication = authRepository.authentication;
        };

        vm.redirect = function () {
            authRepository.fillAuthData()
                .then(function (res) {
                    vm.authentication = authRepository.authentication;
                    if (vm.authentication.moduleId == appModules.HRMS_Module) {
                        // check language also here
                        $window.location.href = '/HRMSPortal';
                    } else if (vm.authentication.moduleId == appModules.INV_Module) {
                        $window.location.href = '/INVPortal';
                    }
                },
                function(err) {
                    console.log(err);
                    $window.location.href = '/Login';
                });
        };
    }
]);
