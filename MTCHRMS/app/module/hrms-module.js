var hrmsModule = angular.module("hrmsModule", 
    ['ngRoute', 'ngResource', 'angularModalService', 'ngAnimate', 'ngSanitize', 'angularUtils.directives.dirPagination',
        'angularFileUpload', 'LocalStorageModule', 'pascalprecht.translate', 'accModule'])
    .config(function ($routeProvider, $locationProvider, $httpProvider, $translateProvider) {

        console.log('hrms module router call !');

        $httpProvider.interceptors.push('authInterceptorService');

        $translateProvider
            .preferredLanguage('ar_OM')
            .fallbackLanguage('en_US')
            .registerAvailableLanguageKeys(['en_US', 'ar_OM'])
            .determinePreferredLanguage();


    //$translateProvider.translations('en_US', {
    //    DEPARTMENT_NAME: 'Hello there, This is my awesome app!',
    //    INTRO_TEXT: 'And it has i18n support!'
    //});
       // .translations('de', {
        //    HEADLINE: 'Hey, das ist meine großartige App!',
        //    INTRO_TEXT: 'Und sie untersützt mehrere Sprachen!'
        //});
        
        $translateProvider.useStaticFilesLoader({
            prefix: '/scripts/angularTranslate/lang/',
            suffix: '.json'
        });
    //alert(mainPortal);
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
            .when('/HRMSPortal/employee/visaexpire', {
                templateUrl: '/templates/hrms/employee/employee-visa-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/passportexpire', {
                templateUrl: '/templates/hrms/employee/employee-passport-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/contractexpire', {
                templateUrl: '/templates/hrms/employee/employee-contract-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/probationexpire', {
                templateUrl: '/templates/hrms/employee/employee-probation-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/listing', {
                templateUrl: '/templates/hrms/listing/list.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/contactlist', {
                templateUrl: '/templates/hrms/listing/employee-contact-list.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortal/employee/appraisal', {
                templateUrl: '/templates/hrms/listing/employee-appraisal-list.html',
                controller: 'EmployeeController'
            });

    $routeProvider
        .when('/HRMSPortalAr', {
            templateUrl: '/templates/dashboard.html',
            controller: 'DashboardController'
        });


        $routeProvider
            .when('/INVPortal', {
                redirectTo: '/INVPortal'
                
            });

        $routeProvider
            .otherwise({ redirectTo: '/HRMSPortal' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

        //$translateProvider.useStaticFilesLoader({
        //    prefix: '/hrmsportal/app/translate/',
        //    suffix: '.json'
        //});

        //$translateProvider.fallbackLanguage('en-US');
        //$translateProvider.useLocalStorage();
        //$translateProvider.preferredLanguage('en-US');

    });

hrmsModule.run(['authRepository', function (authRepository) {
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

