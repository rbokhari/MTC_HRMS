hrmsModule.directive('datepicker',function () {
    return {
        restrict: 'A',
        // Always use along with an ng-model
        require: '?ngModel',
        //priority: '5',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;
            ngModel.$render = function () { //This will update the view with your model in case your model is changed by another code.
                //alert(ngModel.$viewValue || '');
                element.datepicker('update', ngModel.$viewValue || '');

                if (typeof (ngModel.$viewValue) != "undefined"){
                var _date = new Date(ngModel.$viewValue);
                //alert(typeof (ngModel.$viewValue) != "undefined");
                //element.val((_date.getUTCMonth() + 1) + '/' + (_date.getUTCDate()) + '/' + _date.getUTCFullYear());
                    //element.val(_date);
                element.val((_date.getDate()) + '/' + (_date.getMonth() + 1) + '/' + _date.getFullYear());
                }

            };
            //ngModel.$formatters.unshift(function (modelValue) {
            //    return $filter('date')(modelValue);
            //});
            element.datepicker().on("changeDate", function (event) {
                scope.$apply(function () {
                    //var today = new Date(event.date.getUTCFullYear(), event.date.getUTCMonth() + 1, event.date.getUTCDate() + 1, 0, 0, 0, 0);
                    //alert((event.date.getUTCDate() + 1) + "/" + (event.date.getUTCMonth() + 1) + "/" + event.date.getUTCFullYear());
                    //alert("changeDate");
                    //alert(event.date.getDate());
                    //ngModel.$setViewValue(event.date);
                    //if ()
                    ngModel.$setViewValue((event.date.getMonth() + 1) + '/' + (event.date.getDate()) + '/' + event.date.getFullYear());//This will update the model property bound to your ng-model whenever the datepicker's date changes.
                });
            });
        }
    };
});
