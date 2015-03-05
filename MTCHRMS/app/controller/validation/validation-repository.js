/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('validationRepository', ['$resource', function ($resource) {

    var _getAllDetailsByValidationId = function(id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailByValidationId').query();
    };

    var _getSingleDetailByValidationId = function(vid) {
        return $resource('/api/validation/' + vid).get();
    };

    var _getValidationById = function (id) {
        return $resource('/api/validation/ValiationById/' + id).get();
    };

    return {

        getAllDetailsByValidationId: _getAllDetailsByValidationId,
        getSingleDetailByValidationId: _getSingleDetailByValidationId,
        getValidationById: _getValidationById

    //    get: function() {
    //        return $resource('/api/validation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    //    }
    };

}]);