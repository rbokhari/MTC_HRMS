/// <reference path="../../module/acc-module.js" />

'use strict';

accModule.factory('appRepository', ['$resource', '$http', function ($resource, $http) {

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

    var _showDuplicateGritterNotification = function () {
        $.gritter.add({
            title: "MTC",
            text: "Record already exists !",
            time: 4000,
            image: '/Content/img/warning-icon.png',
            position: 'center'
        });

        return true;
    };

    var _showDeleteSuccessGritterNotification = function () {
        $.gritter.add({
            title: "MTC",
            text: "Record deleted successfully !",
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
        showErrorGritterNotification: _showErrorGritterNotification,
        showDeleteGritterNotification: _showDeleteSuccessGritterNotification,
        showDuplicateGritterNotification: _showDuplicateGritterNotification
    };

}]);
