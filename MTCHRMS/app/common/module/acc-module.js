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
    .config(function ($httpProvider, $routeProvider) {
        $httpProvider.interceptors.push('authInterceptorService');

    $routeProvider
        .when('/INVPortal/definition/validation/:id', {
            templateUrl: '/app/common/templates/validation/validation.html',
            controller: 'ValidationController'
        });

        $routeProvider
            .when('/INVPortal/definition/validation/:id/edit/:pid', {
                templateUrl: '/app/common/templates/validation/validation-edit.html',
                controller: 'ValidationController'
            });

    $routeProvider
        .when('/INVPortal/definition/validation/:id/add', {
            templateUrl: '/app/common/templates/validation/validation-add.html',
            controller: 'ValidationController'
        });

});