var accModule = angular.module("accModule",
    ['ngRoute', 'ngResource', 'LocalStorageModule', 'pascalprecht.translate'])

    .constant("appModules", {
        "HRMS_Module": "1",
        "INV_Module": "2"
    })

    .constant("appRoles", {
        "ADMIN": "1",
        "HRMS_ADMIN_EXPATRIATE": "2",
        "HRMS_USER_EXPATRIATE": "3",
        "INV_ADMIN": "4",
        "INV_USER":"5",
        "HRMS_ADMIN_LOCAL": "6",
        "HRMS_USER_LOCAL": "7"
    })

    .constant("validations", {
        "NATIONALITY": "2",
        "COUNTRY": "3",
        "MARITAL_STATUS": "4",
        "GENDER": "5",
        "QUALIFICATION_LEVEL": "6",
        "ITEM_TYPE": "7",
        "ITEM_CATEGORY": "8",
        "ITEM_TECHNICIAN": "9",
        "ITEM_YEAR": "10",
        "EMPLOYEE_STATUS": "11",
        "MAINTENANCE_TYPE": "12",
        "ITEM_STOCK_STATUS": "13",
        "LEAVE_TYPE": "14",
        "LEAVE_SCHEDULE": "15",
        "TICKET_ELIGIBILITY": "16"

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