'use strict';

angular
    .module('app')
    .controller('PostAddCtrl', PostAddCtrl);

PostAddCtrl.$inject = ['$scope', '$routeParams', 'PostService', 'PostCategoryService'];


function PostAddCtrl($scope, $routeParams, PostService, PostCategoryService) {

    $scope.loading = true;

    PostCategoryService.query(
        function (model) {
            $scope.model = model;
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
            PostService.save($scope.model,
                function (success) {
                    $('#statusBox').remove();
                    var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                    $('#upload-form').prepend(status);
                    $scope.loading = false;
                },
                function(error) {
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
                    }
                });
            
        }
    };
}
