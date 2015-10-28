'use strict';

hrmsModule.factory('servicesRepository', ['$resource', '$http', function ($resource, $http) {

    var _applyLeave = function (leave) {
        return $resource('/api/hrmservices/postemployeeleave').save(leave);
    };


    return {
        applyLeave: _applyLeave
    };


}]);