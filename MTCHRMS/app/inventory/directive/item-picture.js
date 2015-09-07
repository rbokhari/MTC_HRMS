'use strict';

invModule.directive("itemPicture", ['itemRepository', '$timeout', function (itemRepository, $timeout) {
    return {
        // Usage
        // <img data-img-loading-value="{{value}}" data-img-loading-size="" alt="" />
        restrict: 'A',
        link: function (scope, element, attrs) {
            //attrs.$set("src", "/Content/img/loader.gif");
            console.log("itemPicture called");
            //alert("me");
            attrs.$observe("itemPicture", function (value) {
                //console.log(value);
                
                if (value) {
                    $timeout(function (){
                        itemRepository.getItemPicture(value)
                            .$promise.then(function (response) {
                                //console.log(response.picture);
                                if (response.picture !== null) {
                                    attrs.$set('src', "data:image/jpeg;base64," + response.picture);
                                    attrs.$set('style', "height: 48px; width: 48px;");
                                }else {
                                    attrs.$set("src", "/Content/img/item.jpg");
                                }
                            
                            }, function (err) {
                                console.error(err);
                                attrs.$set("src", "/Content/img/item.jpg");
                            });
                        
                        }, 200);
                }

                //if (value) {
                //    attrs.$set('src', "data:image/jpeg;base64," + value);
                //} else {
                //    attrs.$observe("imgLoadingSize", function (value1) {
                //        if (value1 === 'mini') {
                //            attrs.$set('src', "/Content/img/avatar-mini.png");
                //        }
                //        else if (value1 === 'profile') {
                //            attrs.$set('src', "/Content/img/profile-pic.jpg");
                //        }
                //        else if (value1 === 'item') {
                //            attrs.$set("src", "/Content/img/item.jpg");
                //        }
                //        else if (value1 === "itemmini") {
                //            attrs.$set("src", "/Content/img/item.jpg");
                //        }
                //    });
                //}
            });
        }
    };
}]);
