var invModule = angular.module("InventoryModule", 
    ['ngRoute', 'ngResource', 'angularUtils.directives.dirPagination'])
    .config(function ($routeProvider, $locationProvider) {

        console.log('inventory module router call !');

        //$httpProvider.interceptors.push('authInterceptorService');

        $routeProvider
            .when('/INVPortal', {
                templateUrl: '/templatesinv/dashboard.html',
                controller: 'DashboardController'
            });

    $routeProvider
        .when('/INVPortal/definition/supplier/list', {
            templateUrl: '/templatesinv/supplier/supplier.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier', {
            templateUrl: '/templatesinv/supplier/supplier.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier/add', {
            templateUrl: '/templatesinv/supplier/supplier-add.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/supplier/edit/:id', {
            templateUrl: '/templatesinv/supplier/supplier-edit.html',
            controller: 'SupplierController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id', {
            templateUrl: '/templatesinv/validation/validation.html',
            controller: 'ValidationController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id/edit/:pid', {
            templateUrl: '/templatesinv/validation/validation-edit.html',
            controller: 'ValidationController'
        });

    $routeProvider
        .when('/INVPortal/definition/validation/:id/add', {
            templateUrl: '/templatesinv/validation/validation-add.html',
            controller: 'ValidationController'
        });


    $routeProvider
        .when('/INVPortal/definition/store/list', {
            templateUrl: '/templatesinv/store/location.html',
            controller: 'LocationController'
        });

    $routeProvider
        .when('/INVPortal/definition/store/add', {
            templateUrl: '/templatesinv/store/location-add.html',
            controller: 'LocationController'
        });

    $routeProvider
        .when('/INVPortal/item/list', {
            templateUrl: '/templatesinv/item/items.html',
            controller: 'ItemController'
        });

    $routeProvider
        .when('/INVPortal/item/add', {
            templateUrl: '/templatesinv/item/item-add.html',
            controller: 'ItemController'
        });

    $routeProvider
        .when('/INVPortal/item/detail/:id', {
            templateUrl: '/templatesinv/item/item-detail.html',
            controller: 'ItemController'
        });


    $routeProvider
        .when('/INVPortal/item/edit/:id', {
            templateUrl: '/templatesinv/item/item-edit.html',
            controller: 'ItemController'
        });


        $routeProvider
            .otherwise({ redirectTo: '/INVPortal' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

    });

//hrmsModule.run(['authRepository', function (authRepository) {
//    authRepository.fillAuthData();
//}]);

    //.run([
    //    '$rootScope', '$location', function($rootScope, $location) {
    //        var path = function() { return $location.path(); };
    //        $rootScope.$watch(path, function(newVal, oldVal) {
    //            $rootScope.activetab = newVal;
    //            // alert(newVal);
    //        });
    //    }
    //]);

