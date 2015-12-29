/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

accModule.controller('ValidationController',
    ['$scope', 'appRepository', 'validationRepository', '$location', '$routeParams',
        function ($scope, appRepository, validationRepository, $location, $routeParams) {

    console.log("validation controller");

    $scope.isBusy = true;

    //$scope.validationId = $routeParams.id;

    if ($routeParams.id != undefined) {
        $scope.validation = validationRepository.getValidationById($routeParams.id);
    }
    
    $scope.loadValidationDetails = function () {
        $scope.validationDetails = validationRepository.getAllDetailsByValidationId($routeParams.id);
        $scope.validationDetails.$promise.then(function() {
                // success
            }, function() {
                // error
            })
            .then(function() { $scope.isBusy = false; });

    };
    //alert($routeParams.pid);
    $scope.loadDetail = function () {
        $scope.validationDetail = validationRepository.getSingleDetailByDetailId($routeParams.pid);
        $scope.validationDetail.$promise.then(function(response) {
                // success
            }, function(error) {
                // error
            })
            .then(function() { $scope.isBusy = false; });

    };

    // $routeParams.id
    //if ($routeParams.id != undefined) {
       // $scope.validationDetail = validationRepository.getSingleDetailByValidationId($routeParams.id);
    //}

    $scope.save = function(id, validationDetail) {
        $scope.errors = [];
        validationDetail.validationId = id;
        if (angular.isUndefined(validationDetail.isActive) || validationDetail.isActive == 'false' || validationDetail.isActive == 0) {
            validationDetail.isActive = 0;
        } else {
            validationDetail.isActive = 1;
        }
        validationRepository.addValidationDetail(validationDetail)
            .$promise
            .then(
            function() {
                // success case
                //$.gritter.add({
                //    title: $scope.validation.nameEn,
                //    text: 'Added Successfully !',
                //    time: 2000,
                //    position: 'center'
                //});
                appRepository.showAddSuccessGritterNotification();
                console.log("save - Successfully !");
                $location.url('/' + $scope.mainPortal + '/definition/validation/' + id);
            }, function(error) {
                // failure case
                appRepository.showErrorGritterNotification();
                console.log(error);
                $scope.errors = error.data;
            }
        );
    };

    $scope.saveAddNew = function (validationDetail) {
        $scope.errors = [];

        var clearDept = {
            departmentCode: "",
            departmentName: "",
            statusId:""
        };
        if (angular.isUndefined(validationDetail.isActive) || validationDetail.isActive == 'false' || validationDetail.isActive == 0) {
            validationDetail.isActive = 0;
        } else {
            validationDetail.isActive = 1;
        }
        validationRepository.save(validationDetail).$promise.then(
            function() {
                // success case
                $scope.departmentForm.$setPristine();
                $scope.department = clearDept;
                appRepository.showAddSuccessGritterNotification();
                console.log("saveAddNew - Successfully !");

            }, function(response) {
                // failure case
                appRepository.showErrorGritterNotification();
                $scope.errors = response.data;
                console.log("saveAddNew - Error !");
            }
        );
    };

    $scope.edit = function (validationDetail) {
        $scope.errors = [];
        if (angular.isUndefined(validationDetail.isActive) || validationDetail.isActive == 'false' || validationDetail.isActive == 0) {
            validationDetail.isActive = 0;
        } else {
            validationDetail.isActive = 1;
        }
        validationRepository.editValidationDetail(validationDetail)
            .then(function (response) {
                // success case
                console.log("edit - Successfully !");
                appRepository.showUpdateSuccessGritterNotification();
                $location.url('/' + $scope.mainPortal + '/definition/validation/' + validationDetail.validationId);
            }, function (error) {
                // failure case
                appRepository.showErrorGritterNotification();
                console.log("edit - Error !");
                $scope.errors = response.data;
            }
        );
    };

}]);
