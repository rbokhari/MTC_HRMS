var accModule = angular.module("accModule",
    [
        'angularModalService', 'LocalStorageModule', 'pascalprecht.translate'
    ])
    .constant("appModules", {
        "HRMS_Module": "1",
        "INV_Module":"2"
    })
    .config(function($routeProvider, $locationProvider, $httpProvider, $translateProvider) {

    });