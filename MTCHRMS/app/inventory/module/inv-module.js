var invModule = angular.module("InventoryModule", 
    ['ngRoute', 'ngResource', 'angularModalService', 'ngAnimate', 'ngSanitize', 'angularUtils.directives.dirPagination',
        'angularFileUpload', 'LocalStorageModule', 'pascalprecht.translate', 'toggle-switch', 'accModule', 'hrmsModule'])
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
        .when('/INVPortal/definition/manufacturer', {
            templateUrl: '/app/inventory/templates/manufacturer/manufacturer.html',
            controller: 'ManufacturerController'
        });

    $routeProvider
        .when('/INVPortal/definition/manufacturer/add', {
            templateUrl: '/app/inventory/templates/manufacturer/manufacturer-add.html',
            controller: 'ManufacturerController'
        });

    $routeProvider
        .when('/INVPortal/definition/manufacturer/edit/:id', {
            templateUrl: '/app/inventory/templates/manufacturer/manufacturer-edit.html',
            controller: 'ManufacturerController'
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
        .when('/INVPortal/definition/store/edit/:id', {
            templateUrl: '/app/inventory/templates/store/location-edit.html',
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
        .when('/INVPortal/item/detail/addstock/:id', {
            templateUrl: '/app/inventory/templates/item/item-stock-add.html',
            controller: 'ItemController'
        });

    $routeProvider
        .when('/INVPortal/item/detail/updatestock/:id/:stockAddId', {
            templateUrl: '/app/inventory/templates/item/item-stock-update.html',
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

