﻿'use strict';

invModule.directive("itemPicture", ['itemRepository', '$timeout', function (itemRepository, $timeout) {
    return {
        // Usage
        // <img data-img-loading-value="{{value}}" data-img-loading-size="" alt="" />
        restrict: 'A',
        link: function (scope, element, attrs) {
            //attrs.$set("src", "/Content/img/loader.gif");
            console.log("itemPicture called");
            attrs.$observe("itemPicture", function (value) {
                if (value) {
                    $timeout(function (){
                        itemRepository.getItemPicture(value)
                            .$promise.then(function (response) {
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
            });
        }
    };
}]);
