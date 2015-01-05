﻿/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('accountRepository', ['$resource', function ($resource) {

    var _getUserById = function (id) {
        return $resource('/api/account/GetUserDetail/?id' + id).get();
    };

    var _getRoleById = function (id) {
        return $resource('/api/account/GetRoleDetail/?id=' + id).get();
    };

    var _getModuleById = function (id) {
        return $resource('/api/account/GetModuleDetail/?id' + id).get();
    };

    return {
        getUserById: _getUserById,
        getRoleById: _getRoleById,
        getModuleById: _getModuleById
    };

}]);

