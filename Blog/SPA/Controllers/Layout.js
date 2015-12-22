'use strict';

angular
    .module('app')
    .controller('LayoutCtrl', LayoutCtrl);

LayoutCtrl.$inject = ['$scope', '$routeParams', 'PostService', 'PostCategoryService'];


function LayoutCtrl($scope, $routeParams, PostService, PostCategoryService) {

    $scope.loading = true;
    $scope.widgets = ['Author', 'Categories', 'RecentPosts'];

}
