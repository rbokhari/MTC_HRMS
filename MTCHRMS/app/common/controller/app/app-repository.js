/// <reference path="../../module/acc-module.js" />

'use strict';

accModule.factory('appRepository', [
    '$resource',
    function($resource) {

        var _showAddSuccessGritterNotification = function() {
            $.gritter.add({
                title: "MTC",
                text: "Record added successfully !",
                time: 4000,
                image: '/Content/img/tick.png',
                position: 'center'
            });

            return true;
        };

        var _showDuplicateGritterNotification = function() {
            $.gritter.add({
                title: "MTC",
                text: "Record already exists !",
                time: 4000,
                image: '/Content/img/warning-icon.png',
                position: 'center'
            });

            return true;
        };

        var _showDeleteSuccessGritterNotification = function() {
            $.gritter.add({
                title: "MTC",
                text: "Record deleted successfully !",
                time: 4000,
                image: '/Content/img/tick.png',
                position: 'center'
            });

            return true;
        };

        var _showUpdateSuccessGritterNotification = function() {
            $.gritter.add({
                title: "MTC",
                text: "Record updated Successfully !",
                time: 4000,
                image: '/Content/img/tick.png',
                position: 'center'
            });

            return true;
        };

        var _showErrorGritterNotification = function() {
            $.gritter.add({
                title: "MTC",
                text: "Error occured !",
                time: 4000,
                image: '/Content/img/cross.png',
                position: 'center'
            });

            return true;
        };

        var _showPageBusyNotification = function (ctrl, buttontext) {
            ctrl = $('#cmdSave');
            
            $(ctrl)
                .prop('disabled', true)
                .html(buttontext);

            $('.submit-progress').removeClass("hidden");
            $("#main-content").addClass('submit-progress-bg');
        };

        var _hidePageBusyNotification = function (ctrl) {
            ctrl = $('#cmdSave');
            $(ctrl)
                .prop('disabled', false)
                .html('<i class="icon-ok"></i>&nbsp;Save');

            $('.submit-progress').addClass("hidden");
            $("#main-content").removeClass('submit-progress-bg');
        };

        var _setControlDisabled = function (ctrl) {
            $(ctrl).prop('disabled', true);
        };

        var _setControlEnabled = function (ctrl) {
            $(ctrl).prop('disabled', false);
        };


        return {
            showAddSuccessGritterNotification: _showAddSuccessGritterNotification,
            showUpdateSuccessGritterNotification: _showUpdateSuccessGritterNotification,
            showErrorGritterNotification: _showErrorGritterNotification,
            showDeleteGritterNotification: _showDeleteSuccessGritterNotification,
            showDuplicateGritterNotification: _showDuplicateGritterNotification,
            showPageBusyNotification: _showPageBusyNotification,
            hidePageBusyNotification: _hidePageBusyNotification,
            setControlDisabled: _setControlDisabled,
            setControlEnabled: _setControlEnabled
        };

    }
]);
