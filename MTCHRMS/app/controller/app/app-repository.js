/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('appRepository', ['$resource', '$http', function ($resource, $http) {

    var _showAddSuccessGritterNotification = function () {
        $.gritter.add({
            title: "MTC",
            text: "Record added successfully !",
            time: 4000,
            image: '/Content/img/tick.png',
            position: 'center'
        });

        return true;
    };

    var _showUpdateSuccessGritterNotification = function () {
        $.gritter.add({
            title: "MTC",
            text: "Record updated Successfully !",
            time: 4000,
            image: '/Content/img/tick.png',
            position: 'center'
        });

        return true;
    };

    var _showErrorGritterNotification = function () {
        $.gritter.add({
            title: "MTC",
            text: "Error occured !",
            time: 4000,
            image: '/Content/img/cross.png',
            position: 'center'
        });

        return true;
    };

    return {
        showAddSuccessGritterNotification: _showAddSuccessGritterNotification,
        showUpdateSuccessGritterNotification: _showUpdateSuccessGritterNotification,
        showErrorGritterNotification: _showErrorGritterNotification
    };

}]);
