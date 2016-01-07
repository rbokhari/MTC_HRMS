/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('assignmentRepository',
    ['$resource', '$http', '$q', 'localStorageService',
        function ($resource, $http, $q, localStorageService) {

    var _getAssignments = function (search) {
        var req = {
            method: 'GET',
            url: '/api/courseassignment',
            params: {                   // use data for post request
                departmentId: search.departmentId,
                yearId: search.yearId
            }
        };
        var deferred = $q.defer();

        $http(req)
            .success(function (res) {
                deferred.resolve(res);
            })
            .error(function (err) {
                deferred.reject(err);
            });

        return deferred.promise;
    };

    var _getAssignment = function (id) {

        var req = {
            method: 'GET',
            url: '/api/courseassignment/'+id,
        };
        var deferred = $q.defer();

        $http(req)
            .success(function (res) {
                deferred.resolve(res);
            })
            .error(function (err) {
                deferred.reject(err);
            });

        return deferred.promise;
    };

    var _addAssignment = function(assign) {
        return $resource('/api/courseassignment').save(assign);
    };

    var _updateAssignment = function (assign) {
        return $http.put('/api/courseassignment', assign);
    };

    return {
        getAssignments: _getAssignments,
        getAssignment:_getAssignment,
        addAssignment: _addAssignment,
        updateAssignment: _updateAssignment
    };

}]);