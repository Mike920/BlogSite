'use strict';

angular
    .module('app')
    .controller('CategoryAddCtrl', CategoryAddCtrl);

CategoryAddCtrl.$inject = ['$scope', '$routeParams', 'PostCategoryService'];


function CategoryAddCtrl($scope, $routeParams, PostCategoryService) {

    $scope.loading = false;
    $scope.title = "Create category";
    var isUpdate = $routeParams.id != null;
    if (isUpdate) {
        $scope.title = "Edit category";
        PostCategoryService.get({ id: $routeParams.id },
            function(model) {
                $scope.model = model;
                $scope.loading = false;
            },
            function(error) {
                $('#upload-form').prepend();
                $scope.loading = false;
            });
    }



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
                PostCategoryService.update({ id: $routeParams.id }, $scope.model, successHandler, errorHandler);
            }
            else{
                PostCategoryService.save($scope.model, successHandler, errorHandler);
            };
        }
    };
}
