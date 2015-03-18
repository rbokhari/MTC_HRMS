//'use strict';

accModule.factory('authInterceptorService', ['$q', '$location', 'localStorageService', '$window', function ($q, $location, localStorageService, $window) {

    var authInterceptorServiceFactory = {};

    var _request = function(config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            var customData = localStorageService.get('userData');
            config.headers.Authorization = 'Bearer ' + authData.token;

            
            if (customData) {
                config.headers.userId = customData.userId;
                config.headers.roleId = customData.roleId;
                config.headers.roles = customData.role;
            }
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
