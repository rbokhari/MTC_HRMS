
//'use strict';

accModule.controller('AccountController',
[
    '$scope', 'authRepository', '$location','$window', 'accountRepository','appModules',
    function ($scope, authRepository, $location, $window, accountRepository, appModules) {

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
                    //authRepository.fillAuthData();
                    $scope.authentication = authRepository.authentication;
                    //console.log($scope.authentication.moduleId);

                    if ($scope.authentication.moduleId == appModules.HRMS_Module) {
                        // check language also here
                        $window.location.href = '/HRMSPortal';
                    } else if ($scope.authentication.moduleId == appModules.INV_Module) {
                        $window.location.href = '/INVPortal';
                    }

                   
                    //$window.location.href = '/HRMSPortal';
                },
                function (err) {
                    console.log(err);
                    //$scope.message = err.error_description;
                    //$scope.message = err.error;
                    $scope.message = "Invalid Username or Password !";
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
