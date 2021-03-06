﻿'use strict'
var hrmsModule = angular.module("hrmsModule",
    ['ngRoute', 'ngResource', 'angularModalService', 'ngAnimate', 'ngSanitize', 'angularUtils.directives.dirPagination',
        'angularFileUpload', 'LocalStorageModule', 'pascalprecht.translate', 'toggle-switch', 'ui.bootstrap', 'ui.select', 'accModule'])
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
            .when('/HRMSPortalAr', {
                templateUrl: '/app/hrms/templates/dashboard.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/department', {
                templateUrl: '/app/hrms/templates/hrms/department/department.html',
                controller: 'DepartmentController'
            });

        $routeProvider
            .when('/HRMSPortalAr/department/add', {
                templateUrl: '/app/hrms/templates/hrms/department/department-add.html',
                controller: 'DepartmentController'
            });
        $routeProvider
            .when('/HRMSPortalAr/department/edit/:id', {
                templateUrl: '/app/hrms/templates/hrms/department/department-edit.html',
                controller: 'DepartmentController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortalAr/employee/add', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-add.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortalAr/employee/detail/:id', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-detail.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortalAr/employee/edit/:id', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-edit.html',
                controller: 'EmployeeController'
            });
        $routeProvider
            .when('/HRMSPortalAr/oc', {
                templateUrl: '/app/hrms/templates/hrms/oc/organization-chart.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortalAr/leaverequest', {
                templateUrl: '/app/hrms/templates/services/leave-request-form.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/visaexpire', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-visa-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/passportexpire', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-passport-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/contractexpire', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-contract-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/probationexpire', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-probation-expiry.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/listing', {
                templateUrl: '/app/hrms/templates/hrms/listing/list.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/contactlist', {
                templateUrl: '/app/hrms/templates/hrms/listing/employee-contact-list.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/appraisal', {
                templateUrl: '/app/hrms/templates/hrms/listing/employee-appraisal-list.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/HRMSPortalAr/employee/search', {
                templateUrl: '/app/hrms/templates/hrms/employee/employee-search.html',
                controller: 'EmployeeController'
            });

        $routeProvider
            .when('/HRMSPortalAr/ticket', {
                templateUrl: '/app/hrms/templates/hrms/ticket/ticket.html',
                controller: 'TicketController'
            });
        $routeProvider
            .when('/HRMSPortalAr/ticket/add', {
                templateUrl: '/app/hrms/templates/hrms/ticket/ticket-add.html',
                controller: 'TicketController'
            });
        $routeProvider
            .when('/HRMSPortalAr/ticket/edit/:id', {
                templateUrl: '/app/hrms/templates/hrms/ticket/ticket-edit.html',
                controller: 'TicketController'
            });

        $routeProvider
            .when('/HRMSPortalAr/leave', {
                templateUrl: '/app/hrms/templates/hrms/leave/leave.html',
                controller: 'LeaveController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/leave/add', {
                templateUrl: '/app/hrms/templates/hrms/leave/leave-add.html',
                controller: 'LeaveController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/leave/edit/:id', {
                templateUrl: '/app/hrms/templates/hrms/leave/leave-edit.html',
                controller: 'LeaveController as vm'
            });

        $routeProvider
            .when('/HRMSPortalAr/training/courses', {
                templateUrl: '/app/hrms/templates/tr/course/courses.html',
                controller: 'CourseController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/training/course/add', {
                templateUrl: '/app/hrms/templates/tr/course/course-add.html',
                controller: 'CourseController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/training/course/edit/:id', {
                templateUrl: '/app/hrms/templates/tr/course/course-edit.html',
                controller: 'CourseController as vm'
            });

        $routeProvider
            .when('/HRMSPortalAr/training/courses', {
                templateUrl: '/app/hrms/templates/tr/course/courses.html',
                controller: 'CourseController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/training/course/add', {
                templateUrl: '/app/hrms/templates/tr/course/course-add.html',
                controller: 'CourseController as vm'
            });
        $routeProvider
            .when('/HRMSPortalAr/training/course/edit/:id', {
                templateUrl: '/app/hrms/templates/tr/course/course-edit.html',
                controller: 'CourseController as vm'
            });

        $routeProvider
            .when('/HRMSPortalAr/training/assignment/add/:id', {
                templateUrl: '/app/hrms/templates/tr/assignment/assignment-add.html',
                controller: 'AssignmentController as vm'
            });


        $routeProvider
            .when('/HRMSPortalAr', {
                templateUrl: '/app/hrms/templates/dashboard.html',
                controller: 'DashboardController'
            });

        $routeProvider
            .when('/INVPortal', {
                redirectTo: '/INVPortal'
                
            });

        $routeProvider
            .otherwise({ redirectTo: '/HRMSPortal' });

        $locationProvider.html5Mode({ enabled: true, requireBase: false });

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

