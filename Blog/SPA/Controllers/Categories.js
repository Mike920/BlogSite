'use strict';

angular
    .module('app')
    .controller('CategoriesCtrl', CategoriesCtrl);

CategoriesCtrl.$inject = ['$scope', '$routeParams', 'PostCategoryService', 'UserService', '$filter'];


function CategoriesCtrl($scope, $routeParams, PostCategoryService, UserService, $filter) {

    $scope.loading = true;
    UserService.user.$promise.then(function (result) {
        $scope.blogId = result.CurrentBlogId;

            $('#categories-grid').DataTable({
                "ajax": {
                    "url": "/api/postcategories",
                    "dataSrc": ""
                },

                "columns": [
                {
                    "data": "Name",
                    "width": "90%"
                },
                {
                    "data": "Actions",
                    render: actions,
                    "width": "10%"
                }
                ]
            });

            function actions(data, type, row) {
                return '<a href="Categories/Edit/' + row.Id + '" class="btn-sm btn-success">Edit</a>'
                  + '<a href="" style="margin-left: 5px;" class="btn-sm btn-danger">Delete</a>';
            };
    });

}
