

invModule.directive("navbarMainInv", function() {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/main/navbar.html'
    };
});

invModule.directive("navbarNotifyInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/main/navbar-notify.html',
        controller: 'DashboardController'
    };
});

invModule.directive("navbarUserInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/main/navbar-user.html'
    };
});

invModule.directive("menuMainInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/main/menu-main.html'
    };
});
