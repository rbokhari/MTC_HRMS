

hrmsModule.directive("navbarMain", function() {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/main/navbar.html'
    };
});

hrmsModule.directive("navbarNotify", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/main/navbar-notify.html',
        controller: 'DashboardController'
    };
});

hrmsModule.directive("navbarUser", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/main/navbar-user.html'
    };
});

hrmsModule.directive("menuMain", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/hrms/templates/main/menu-main.html'
    };
});
