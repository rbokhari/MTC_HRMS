/// <reference path="department-repository.js" />
/// <reference path="~/Scripts/angular.js" />

'use strict';

invModule.controller('ValidationController',
    ['$scope', 'validationRepository', '$location', '$routeParams',
        function ($scope, validationRepository, $location, $routeParams) {

    console.log("validation controller");

    $scope.isBusy = true;

    //$scope.validationId = $routeParams.id;

    //if ($routeParams.id != undefined) {
    $scope.validation = validationRepository.getValidationById($routeParams.id);
    //}
    
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
        validationRepository.addValidationDetail(validationDetail).$promise.then(
            function() {
                // success case
                $.gritter.add({
                    title: $scope.validation.nameEn,
                    text: 'Added Successfully !',
                    time: 2000,
                    position: 'center'
                });

                console.log("save - Successfully !");
                $location.url('/INVPortal/definition/validation/' + id);
            }, function(error) {
                // failure case
                console.log("save - Error !");
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

        validationRepository.save(validationDetail).$promise.then(
            function() {
                // success case
                $scope.departmentForm.$setPristine();
                $scope.department = clearDept;
                console.log("saveAddNew - Successfully !");

            }, function(response) {
                // failure case
                $scope.errors = response.data;
                console.log("saveAddNew - Error !");
            }
        );
    };

    $scope.edit = function (validationDetail) {
        $scope.errors = [];

        validationRepository.editValidationDetail(validationDetail).then(
            function (response) {
                // success case
                console.log("edit - Successfully !");
                $location.url('/INVPortal/definition/validation/' + validationDetail.validationId);
            }, function (error) {
                // failure case
                console.log("edit - Error !");
                $scope.errors = response.data;
            }
        );
    };

}]);
