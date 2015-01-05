
hrmsModule.directive("imgLoadingValue", function () {
    return {
        // Usage
        // <img data-img-loading-value="{{value}}" data-img-loading-size="" alt="" />
        restrict: 'A',
        link: function (scope, element, attrs) {
            attrs.$observe("imgLoadingValue", function(value) {
                if (value) {
                    attrs.$set('src',"data:image/jpeg;base64," + value);
                } else {
                    attrs.$observe("imgLoadingSize", function(value1) {
                        if (value1 == 'mini') {
                            attrs.$set('src', "/Content/img/avatar-mini.png");
                        }
                        else if (value1 == 'profile') {
                            attrs.$set('src', "/Content/img/profile-pic.jpg");
                        }
                    });
                }
            });
        }
    };
});
