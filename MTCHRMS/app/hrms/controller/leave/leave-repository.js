/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('leaveRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllLeaves = function() {
        return $resource('/api/leave').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getLeaveById = function (id) {
        return $resource('/api/leave/' + id).get();
    };

    var _addLeave = function (leave) {
        return $resource('/api/leave').save(leave);
    };

    var _editLeave = function (leave) {
        return $http.put('/api/leave/' + leave.leaveId, leave);
    };

    return {
        getAllLeaves: _getAllLeaves,
        getLeaveById: _getLeaveById,
        addLeave: _addLeave,
        editLeave: _editLeave
    };

}]);
