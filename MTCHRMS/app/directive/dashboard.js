
hrmsModule.directive("departments", function () {
    return {
        restrict: 'E',
        templateUrl: '/templates/dashboard/departments.html'
    };
});

hrmsModule.directive("nationalities", function () {
    return {
        restrict: 'E',
        templateUrl: '/templates/dashboard/nationalities.html'
    };
});
