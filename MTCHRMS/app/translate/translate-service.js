
hrmsModule.service('translationService', function ($resource) {

    this.getTranslation = function ($scope, language) {
        console.log("translate data");
        var languageFilePath = '/hrmsportal/app/translate/translation_' + language + '.json';
        console.log(languageFilePath);
        $resource(languageFilePath).get(function (data) {
            $scope.translation = data;
            console.log($scope.translation);
        });
    };
});
