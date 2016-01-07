'use strict';

hrmsModule.controller('AssignmentModalController',
    ['$uibModalInstance', 'courseRepository', 'validationRepository',
        function ($uibModalInstance, courseRepository, validationRepository) {

            var vm = this;

            //var assign = {
            //    categoryId:0,
            //    courseId: 0,
            //    duration: '123456',
            //    timePeriod: '',
            //    location: '',
            //    organizer: '',
            //    categoryDetail: {
            //        nameAr: ''
            //    }
            //};

            vm.ok = function (assign) {
                console.log('ok');
                $uibModalInstance.close(assign);
            };

    vm.cancel = function () {
        console.log('cancel');
        $uibModalInstance.dismiss('cancel');
    };

    vm.activate = function () {

        validationRepository.getTrainingCategories.$promise
            .then(function (response) {
                vm.categories = response;
            }, function (err) {
                console.log(err);
            });


        courseRepository.getAllCourses()
            .then(function (response) {
                console.log(response);
                vm.courses = response;
            }, function (err) {
                //alert("error");
            })
            .then(function () {
                vm.isBusy = false;
                if (typeof done !== 'undefined') { done(); }
            });

    };

    vm.activate();

}]);