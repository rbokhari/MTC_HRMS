var invModule = angular.module("InventoryModule", 
    ['ngRoute', 'ngResource'])
    .config(function ($routeProvider, $locationProvider) {

        console.log('inventory module router call !');

        //$httpProvider.interceptors.push('authInterceptorService');

        $routeProvider
            .when('/INVPortal', {
                templateUrl: '/templatesinv/dashboard.html',
                controller: 'DashboardController'
            });

    $routeProvider
        .when('/INVPortal/supplier/list', {
            templateUrl: '/templatesinv/supplier/supplier.html',
            controller: 'DashboardController'
        });

    $routeProvider
        .when('/INVPortal/supplier/add', {
            templateUrl: '/templatesinv/supplier/supplier-add.html',
            controller: 'DashboardController'
        });


    $routeProvider
        .when('/INVPortal/store/list', {
            templateUrl: '/templatesinv/store/location.html',
            controller: 'DashboardController'
        });

    $routeProvider
        .when('/INVPortal/store/add', {
            templateUrl: '/templatesinv/store/location-add.html',
            controller: 'DashboardController'
        });

    $routeProvider
        .when('/INVPortal/item/list', {
            templateUrl: '/templatesinv/item/items.html',
            controller: 'DashboardController'
        });

    $routeProvider
        .when('/INVPortal/item/add', {
            templateUrl: '/templatesinv/item/item-add.html',
            controller: 'DashboardController'
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

