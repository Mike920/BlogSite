'use strict';

angular
    .module('app')
    .controller('BlogSettingsCtrl', BlogSettingsCtrl);

BlogSettingsCtrl.$inject = ['$scope','$routeParams', 'BlogService'];


function BlogSettingsCtrl($scope, $routeParams, BlogService) {
        BlogService.get({ id: $routeParams.id }, function (model) {
            $scope.model = model;
        });
        BlogService.query();

        $scope.submit = function ($event) {
            if($($event.currentTarget).valid())
                BlogService.update({ id: $scope.model.Id }, $scope.model);
    };
}
