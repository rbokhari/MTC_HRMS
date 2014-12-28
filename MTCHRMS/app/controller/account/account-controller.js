
//'use strict';

hrmsModule.controller('AccountController',
[
    '$scope', 'authRepository', '$location','$window', 'accountRepository',
    function ($scope, authRepository, $location, $window, accountRepository) {

        console.log("account controller");
        $scope.loginData = {
            userName: "",
            password: ""
        };
        $scope.message = "";
        $scope.login = function() {
            authRepository.login($scope.loginData).then(function (response) {
                    $window.location.href = '/HRMSPortal';
                },
                function (err) {
                    console.log(err);
                    //$scope.message = err.error_description;
                    //$scope.message = err.error;
                    $scope.message = "Invalid Username or Password !";
                });
        };

        $scope.userDetail = function(id) {
            accountRepository.getUserById(id)
                .then(function(response) {
                    $scope.userInfo = response.data;

                }, function(err) {

                });
        };

        $scope.RoleDetail = function (id) {
            accountRepository.getRoleById(id)
                .then(function (response) {
                    $scope.roleInfo = response.data;

                }, function (err) {

                });
        };

        $scope.moduleDetail = function (id) {
            accountRepository.getModuleById(id)
                .then(function (response) {
                    $scope.moduleInfo = response.data;

                }, function (err) {

                });
        };


    }
]);
