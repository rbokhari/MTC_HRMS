/// <reference path="../../module/acc-module.js" />

'use strict';

accModule.factory('accountRepository', ['$resource', function ($resource) {

    var _getUserById = function (id) {
        return $resource('/api/account/GetUserDetail/?id=' + id).get();
    };

    var _getUserByUserName = function (username) {
        return $resource('/api/account/GetUserDetailByUserName/?username=' + username).get();
    };

    var _getRoleById = function (id) {
        return $resource('/api/account/GetRoleDetail/?id=' + id).get();
    };

    var _getModuleById = function (id) {
        return $resource('/api/account/GetModuleDetail/?id' + id).get();
    };

    var _getUserDetailByUserName = function (userName) {
        return $resource('/api/employee/GetEmployeeByUserName/?userName=' + userName).get();
    };


    return { 
        getUserById: _getUserById,
        getUserByUserName:_getUserByUserName,
        getRoleById: _getRoleById,
        getModuleById: _getModuleById,
        getUserDetailByUserName: _getUserDetailByUserName
    };

}]);

