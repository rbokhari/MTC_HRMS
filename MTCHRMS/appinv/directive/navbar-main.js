

invModule.directive("navbarMainInv", function() {
    return {
        restrict: 'E',
        templateUrl: '/templatesinv/main/navbar.html'
    };
});

invModule.directive("navbarNotifyInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/templatesinv/main/navbar-notify.html',
        controller: 'DashboardController'
    };
});

invModule.directive("navbarUserInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/templatesinv/main/navbar-user.html'
    };
});

invModule.directive("menuMainInv", function () {
    return {
        restrict: 'E',
        templateUrl: '/templatesinv/main/menu-main.html'
    };
});
