/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('courseRepository', ['$resource', '$http', '$q', 'localStorageService', function ($resource, $http, $q, localStorageService) {

    var _getAllCourses = function (forceRefresh) {
        if (typeof forceRefresh === 'undefined') { forceRefresh = false; }
        var req = {
            method: 'GET',
            url: '/api/course'
        };
        var deferred = $q.defer();
        var coursetData = null;

        if (!forceRefresh) { coursetData = localStorageService.get('courses') }

        if (coursetData) {
            console.log("found courses from storage");

            deferred.resolve(coursetData);
        } else {
            console.log("fetch courses from server");
            $http(req)
                .success(function (res) {
                    localStorageService.set('courses', res);
                    deferred.resolve(res);
                })
                .error(function (err) {
                    deferred.reject(err);
                });
        }
        return deferred.promise;
    };

    var _getCourseById = function(id) {
        return $resource('/api/course/' + id).get();
    };

    var _addCourse = function(course) {
        return $resource('/api/course').save(course);
    };

    var _editCourse = function (course) {
        return $http.put('/api/course', course);
    };

    return {
        getAllCourses: _getAllCourses,
        getCourseById: _getCourseById,
        addCourse: _addCourse,
        editCourse: _editCourse
    };

}]);