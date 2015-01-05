///#source 1 1 /app/controller/account/authInterceptorService.js
//'use strict';

hrmsModule.factory('authInterceptorService', ['$q', '$location', 'localStorageService', '$window', function ($q, $location, localStorageService, $window) {

    var authInterceptorServiceFactory = {};

    var _request = function(config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            //console.log(authData.token);
            config.headers.Authorization = 'Bearer ' + authData.token;
            //config.headers.userFullNameIs = "this is full name";
        }

        return config;
    };

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            //alert("401 status");
            console.log("unauthorized call");
            //$location.path('/login');
            $window.location.href = '/login';
            //$window.location.reload();
            //$route.reload();
        }
        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);

///#source 1 1 /app/controller/account/auth-repository.js
//'use strict';
hrmsModule.factory('authRepository', ['$http', '$q', 'localStorageService', 'employeeRepository',
    function ($http, $q, localStorageService, employeeRepository) {

    var serviceBase = 'http://localhost:90/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        employeeId: 0,
        userName: "",
        fullName: "",
        departmentName: "",
        departmentId: 0,
        empPicture:""
    };

    //var _saveRegistration = function (registration) {

    //    _logOut();

    //    return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
    //        return response;
    //    });

    //};

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        
        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        console.log("logOut from System");

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    var _fillAuthData = function () {
        
        var authData = localStorageService.get('authorizationData');
        
        if (authData) {
            //console.log("authData.yes :(" + authData + ")");
            //var employeeData = employeeRepository.getEmployeeDetailByUserName(authData.userName);
            employeeRepository.getEmployeeDetailByUserName(authData.userName).$promise
                .then(function(response) {
                    //console.log("authData.fill");
                    _authentication.isAuth = true;
                    _authentication.userName = response.userName;
                    _authentication.fullName = response.employeeName;
                    _authentication.employeeId = response.id;
                    _authentication.departmentName = response.postedTo;
                    _authentication.empPicture = response.empPicture;


                }, function() {
                    //console.log("authData.fail");
                    _logOut();
                    _authentication.isAuth = false;
                });

            //console.log(employeeData.d.employeeName);
            _authentication.isAuth = true;
        }
    };

    //authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);
