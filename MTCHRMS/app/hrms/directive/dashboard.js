
hrmsModule.directive("departments", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/dashboard/departments.html'
    };
});

hrmsModule.directive("nationalities", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/dashboard/nationalities.html'
    };
});
