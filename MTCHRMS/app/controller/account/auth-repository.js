//'use strict';
hrmsModule.factory('authRepository', [
    '$http', '$q', 'localStorageService', 'employeeRepository', 'accountRepository',
    function($http, $q, localStorageService, employeeRepository, accountRepository) {

        var serviceBase = 'http://localhost:90/';
        var authServiceFactory = {};

        var _authentication = {
            isAuth: false,
            employeeId: 0,
            userName: "",
            fullName: "",
            departmentName: "",
            departmentId: 0,
            empPicture: "",
            email: "",
            phone: "",
            roles:""
        };

        //var _saveRegistration = function (registration) {
        //    _logOut();
        //    return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
        //        return response;
        //    });
        //};

        var _lockLogin = function(loginData) {

            var deferred = $q.defer();

            $http.post('/api/account/?userName=' + loginData.userName + '&password=' + loginData.password)
                .success(function(response) {

                    _authentication.isAuth = true;
                    _authentication.userName = loginData.userName;

                    deferred.resolve(response);

                }).error(function(err, status) {
                    //_logOut();
                    deferred.reject(err);
                });

            return deferred.promise;

        };

        var _login = function(loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function(response) {

                //alert("login :" + loginData.userName);
                //employeeData(loginData.userName);

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, role: _authentication.roles });

                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;

                //alert(_authentication.userName);

                deferred.resolve(response);

            }).error(function(err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function() {

            console.log("logOut from System 111");

            localStorageService.remove('authorizationData');

            _authentication.isAuth = false;
            _authentication.userName = "";

        };

        var _fillAuthData = function() {

            var authData = localStorageService.get('authorizationData');

            if (authData) {
                //console.log("authData.yes :(" + authData + ")");
                //var employeeData = employeeRepository.getEmployeeDetailByUserName(authData.userName);
                //alert("auth : " + authData.userName);
                employeeData(authData.userName);

                //console.log(employeeData.d.employeeName);
                _authentication.isAuth = true;
            }
        };

        function employeeData(userName) {
            //alert(userName);
            employeeRepository.getEmployeeDetailByUserName(userName)
                .$promise
                .then(function(response) {
                    //console.log(response);
                    //alert("auth-repository->fillAuthDate");
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.fullName = response.employeeName;
                    _authentication.employeeId = response.id;
                    _authentication.departmentName = response.postedTo;
                    _authentication.empPicture = response.empPicture;
                    _authentication.email = response.email;
                    _authentication.phone = response.phone;

                    accountRepository.getRoleById(response.id)
                        .$promise
                        .then(function (response1) {
                        
                        _authentication.roles = response1.roleName;

                        localStorageService.set('userData', { userName: userName, role: _authentication.roles });
                    });

            }, function(err) {
                    //alert("authData.fail :" + err);
                    _logOut();
                    _authentication.isAuth = false;
                });
        }


        //authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;
        authServiceFactory.lockLogin = _lockLogin;

        return authServiceFactory;

    }
]);