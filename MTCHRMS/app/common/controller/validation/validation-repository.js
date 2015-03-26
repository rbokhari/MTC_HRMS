/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/inv-module.js" />


'use strict';

accModule.factory('validationRepository', ['$resource', '$http', 'validations', function ($resource, $http, validations) {

    var _getAllDetailsByValidationId = function(id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailByValidationId').query();
    };

    var _getSingleDetailByDetailId = function (id) {
        return $resource('/api/validation/' + id + '/GetValidationDetailById').get();
    };

    var _getValidationById = function (id) {
        return $resource('/api/validation/' + id + '/GetValiationById/').get();
    };

    var _addValidationDetail = function (validationDetail) {
        return $resource('/api/validation').save(validationDetail);
    };

    var _editValidationDetail = function (validationDetail) {
        return $http.put('/api/validation/' + validationDetail.id, validationDetail);
    };


    return {

        getAllDetailsByValidationId: _getAllDetailsByValidationId,
        getSingleDetailByDetailId: _getSingleDetailByDetailId,
        getValidationById: _getValidationById,
        addValidationDetail: _addValidationDetail,
        editValidationDetail: _editValidationDetail,

        getNationalities: _getAllDetailsByValidationId(validations.NATIONALITY),
        getCountries: _getAllDetailsByValidationId(validations.COUNTRY),
        getMaritalStatus: _getAllDetailsByValidationId(validations.MARITAL_STATUS),
        getGenders: _getAllDetailsByValidationId(validations.GENDER),
        getQualificationLevels: _getAllDetailsByValidationId(validations.QUALIFICATION_LEVEL),
        getItemTypes: _getAllDetailsByValidationId(validations.ITEM_TYPE),
        getItemCategories: _getAllDetailsByValidationId(validations.ITEM_CATEGORY),
        getItemTechnicians: _getAllDetailsByValidationId(validations.ITEM_TECHNICIAN),
        getItemYears: _getAllDetailsByValidationId(validations.ITEM_YEAR)
        

    //    get: function() {
    //        return $resource('/api/validation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    //    }
    };

}]);