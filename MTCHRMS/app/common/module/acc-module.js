var accModule = angular.module("accModule",
    [
        'ngRoute', 'ngResource', 'LocalStorageModule', 'pascalprecht.translate'
    ])
    .constant("appModules", {
        "HRMS_Module": "1",
        "INV_Module": "2"
    })
    .constant("appRoles", {
        "ADMIN": "1",
        "HRMS_ADMIN": "2",
        "HRMS_USER": "3",
        "INV_ADMIN": "4",
        "INV_USER":"5"
    })
    .config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });