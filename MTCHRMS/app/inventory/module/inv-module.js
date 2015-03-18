var invModule = angular.module("InventoryModule", 
    ['ngRoute', 'ngResource', 'angularModalService', 'ngAnimate', 'ngSanitize', 'angularUtils.directives.dirPagination',
        'angularFileUpload', 'LocalStorageModule', 'pascalprecht.translate', 'accModule', 'hrmsModule'])
    .config(function ($routeProvider, $httpProvider, $locationProvider) {

        console.log('inventory module router call !');

        $httpProvider.interceptors.push('authInterceptorService');

        $routeProvider
            .when('/INVPortal', {
                templateUrl: '/app/inventory/templates/dashboard.html',
                controller: 'DashboardController'
            });

    $routeProvider
        .when('/INVPortal/definition/supplier/list', {
            templateUrl: '/app/inventory/templates/supplier/supplier.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier', {
            templateUrl: '/app/inventory/templates/supplier/supplier.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier/add', {
            templateUrl: '/app/inventory/templates/supplier/supplier-add.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier/edit/:id', {
            templateUrl: '/app/inventory/templates/supplier/supplier-edit.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id', {
            templateUrl: '/app/inventory/templates/validation/validation.html',
            controller: 'ValidationController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id/edit/:pid', {
            templateUrl: '/app/inventory/templates/validation-edit.html',
            controller: 'ValidationController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id/add', {
            templateUrl: '/app/inventory/templates/validation-add.html',
            controller: 'ValidationController'
        });


    $routeProvider
        .when('/INVPortal/definition/store/list', {
            templateUrl: '/app/inventory/templates/store/location.html',
            controller: 'LocationController'
        });

    $routeProvider
        .when('/INVPortal/definition/store/add', {
            templateUrl: '/app/inventory/templates/store/location-add.html',
            controller: 'LocationController'
        });

    $routeProvider
        .when('/INVPortal/item/list', {
            templateUrl: '/app/inventory/templates/item/items.html',
            controller: 'ItemController'
        });

    $routeProvider
        .when('/INVPortal/item/add', {
            templateUrl: '/app/inventory/templates/item/item-add.html',
            controller: 'ItemController'
        });

    $routeProvider
        .when('/INVPortal/item/detail/:id', {
            templateUrl: '/app/inventory/templates/item/item-detail.html',
            controller: 'ItemController'
        });


    $routeProvider
        .when('/INVPortal/item/edit/:id', {
            templateUrl: '/app/inventory/templates/item/item-edit.html',
            controller: 'ItemController'
        });


        $routeProvider
            .otherwise({ redirectTo: '/INVPortal' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

    });

//invModule.run(['authRepository', function (authServiceFactory) {
//    authServiceFactory.fillAuthData();
//    console.log(authServiceFactory.authentication);
//}]);

