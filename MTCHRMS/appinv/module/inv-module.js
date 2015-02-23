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
        .when('/HRMSPortal', {
            //$location.path: '/HRMSPortal'
    redirectTo: '/HRMSPortal'

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

