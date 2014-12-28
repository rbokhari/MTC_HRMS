var hrmsModule = angular.module("hrmsModule",
    ['ngRoute', 'ngResource', 'angularModalService', 'ngAnimate', 'ngSanitize', 'angularUtils.directives.dirPagination', 'angularFileUpload', 'LocalStorageModule'])
    .config(function($routeProvider, $locationProvider, $httpProvider) {
        console.log('hrms module router call !');

        //alert(config.headers.Authorization);

        $httpProvider.interceptors.push('authInterceptorService');

        $routeProvider
            .when('/HRMSPortal', {
                templateUrl: '/templates/dashboard.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortal/department', {
                templateUrl: '/templates/hrms/department/department.html',
                controller: 'DepartmentController'
            });

        $routeProvider
            .when('/HRMSPortal/department/add', {
                templateUrl: '/templates/hrms/department/department-add.html',
                controller: 'DepartmentController'
            });
        $routeProvider
            .when('/HRMSPortal/department/edit/:id', {
                templateUrl: '/templates/hrms/department/department-edit.html',
                controller: 'DepartmentController'
            });

        $routeProvider
            .when('/HRMSPortal/employee', {
                templateUrl: '/templates/hrms/employee/employee.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortal/employee/add', {
                templateUrl: '/templates/hrms/employee/employee-add.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortal/employee/detail/:id', {
                templateUrl: '/templates/hrms/employee/employee-detail.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortal/employee/edit/:id', {
                templateUrl: '/templates/hrms/employee/employee-edit.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortal/oc', {
                templateUrl: '/templates/hrms/oc/organization-chart.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortal/leaverequest', {
                templateUrl: '/templates/services/leave-request-form.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .otherwise({ redirectTo: '/HRMSPortal' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });


    });

hrmsModule.run(['authRepository', function (authRepository) {
    //alert($scope.author);
    authRepository.fillAuthData();
}]);

    //.run([
    //    '$rootScope', '$location', function($rootScope, $location) {
    //        var path = function() { return $location.path(); };
    //        $rootScope.$watch(path, function(newVal, oldVal) {
    //            $rootScope.activetab = newVal;
    //            // alert(newVal);
    //        });
    //    }
    //]);
