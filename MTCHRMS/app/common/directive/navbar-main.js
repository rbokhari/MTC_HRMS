

accModule.directive("navbarMain", function() {
    return {
        restrict: 'E',
        templateUrl: '/app/common/templates/main/navbar.html',
        controller: 'AppController as vm'
    };
});

accModule.directive("navbarNotify", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/common/templates/main/navbar-notify.html',
        controller: 'AppController as vm'
    };
});

accModule.directive("navbarUser", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/common/templates/main/navbar-user.html',
        controller: 'AppController as vm'
    };
});
