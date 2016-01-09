'use strict';

angular
    .module('app')
    .controller('DashboardCtrl', DashboardCtrl);

DashboardCtrl.$inject = ['$scope', '$routeParams', 'BlogService', 'DashboardService', 'Helpers'];


function DashboardCtrl($scope, $routeParams, BlogService, DashboardService, Helpers) {
    $scope.loading = true;

    DashboardService.get(null,
        function (data) {
            $scope.blogs = data.Blogs;
            $scope.model = data.CurrentBlog;
            $scope.selected = data.CurrentBlog.Id;
            $scope.loading = false;
        },
        function (error) {
            $scope.status = Helpers.error('Connection error');
            $scope.loading = false;
        });

    $scope.submit = function () {
        $scope.loading = true;
        DashboardService.update({ id: $scope.selected },
            function (blog) {
                $scope.model = blog;
                $scope.loading = false;
            },
            function (error) {
                $scope.status = Helpers.error('An error has occured.');
                $scope.loading = false;
            });
    };
}
