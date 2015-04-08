
invModule.directive("suppliers", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/suppliers.html'
    };
});


invModule.directive("manufacturers", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/manufacturers.html'
    };
});

