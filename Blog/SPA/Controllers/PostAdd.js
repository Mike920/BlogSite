'use strict';

angular
    .module('app')
    .controller('PostAddCtrl', PostAddCtrl);

PostAddCtrl.$inject = ['$scope', '$routeParams', 'PostService', 'PostCategoryService'];


function PostAddCtrl($scope, $routeParams, PostService, PostCategoryService) {

    $scope.loading = true;
    var isUpdate = $routeParams.id != null;
    if (isUpdate) {
        PostService.get({ id: $routeParams.id },
            function(model) {
                $scope.model = model;
                $("iframe").contents().find("#editor").text(model.Content);
                $scope.loading = false;
            },
            function(error) {
                $('#upload-form').prepend();
                $scope.loading = false;
            });
    }

    PostCategoryService.query(
        function (categories) {
            $scope.categories = categories;
            $scope.loading = false;
        },
        function (error) {
            $('#statusBox').remove();
            var status = $('<div id="statusBox" class="alert alert-error" role="alert">Connection error.</div>').hide().fadeIn('normal');
            $('#upload-form').prepend(status);
            $scope.loading = false;
        });

    $scope.submit = function () {
        if ($('#upload-form').valid()) {
            $scope.loading = true;
            var successHandler = function(success) {
                $('#statusBox').remove();
                var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                $('#upload-form').prepend(status);
                $scope.loading = false;
            };
            var errorHandler = function(error) {
                if (error.status === 400) { //validation error
                    var ms = error.data.ModelState;
                    // Display validation errors
                    $(Object.keys(ms)).each(function(index, key) {
                        var val = key.split('.').pop();
                        var errorSpan = $("span[data-valmsg-for='" + val + "']");
                        errorSpan.html("<span style='color:#b94a48'>" + ms[key][0] + "</span>");
                        errorSpan.show();
                    });
                    $scope.loading = false;
                };
            }
            if (isUpdate) {
                PostService.update({ id: $routeParams.id },$scope.model, successHandler, errorHandler);
            }
            else{
                PostService.save($scope.model, successHandler, errorHandler);
            };
        }
    };
}
