
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


invModule.directive("itemtype", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/itemtypes.html'
    };
});

invModule.directive("itemcategory", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/itemcategories.html'
    };
});

invModule.directive("locations", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/itemlocations.html'
    };
});

invModule.directive("departments", function () {
    return {
        restrict: 'E',
        templateUrl: '/app/inventory/templates/dashboard/departments.html'
    };
});
