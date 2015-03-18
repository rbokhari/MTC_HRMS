/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('departmentRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllDepartment = function() {
        return $resource('/api/department').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getDepartmentById = function(id) {
        return $resource('/api/department/' + id).get();
    };

    var _addDepartment = function(department) {
        return $resource('/api/department').save(department);
    };

    var _editDepartment = function(department) {
        return $http.put('/api/department/' + department.id, department);
    };

    return {
        getAllDepartment: _getAllDepartment,
        getDepartmentById: _getDepartmentById,
        addDepartment: _addDepartment,
        editDepartment: _editDepartment
    };

}]);


//hrmsModule.factory('departmentRepository', ['$resource', function ($resource) {
//    return {
//        get: function () {
//            return $resource('/api/department').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
//        }
//    };

//}]);

//hrmsModule.factory('departmentSingleRepository', ['$resource', function ($resource) {
//    return {
//        get: function (id) {
//            return $resource('/api/department/' + id).get(); 
//        }
//    };

//}]);

//hrmsModule.factory('departmentAddRepository', ['$resource', function ($resource) {

//    return {
//        save: function (department) {
//            return $resource('/api/department').save(department);
//        }
//    };

//}]);

//hrmsModule.factory('departmentEditRepository', ['$http', function ($http) {

//    return {
//        put: function (id, department) {
//            return $http.put('/api/department/' + id, department);
//        }
//    };

//}]);
