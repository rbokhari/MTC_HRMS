'use strict';

hrmsModule.factory('servicesRepository', ['$resource', '$http', '$q', function ($resource, $http, $q) {

    var _applyLeave = function (leave) {
        return $resource('/api/hrmservices/postemployeeleave').save(leave);
    };

    var _getLeave = function (id) {
        return $resource('/api/hrmsservices/getLeave/' + id).get();
    };

    var _getNotifications = function (id) {
        var req = {
            method: 'GET',
            url: '/api/hrmsservices/getServiceNotification'
        };
        var deferred = $q.defer();

        $http(req)
                .success(function (res) {
                    deferred.resolve(res);
                })
                .error(function (err) {
                    deferred.reject(err);
                });
        //return $resource('/api/hrmservices/getServiceNotification').get();
        return deferred.promise;
    };

    return {
        applyLeave: _applyLeave,
        getLeave: _getLeave,
        getNotifications: _getNotifications
    };

}]);