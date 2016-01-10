'use strict';

angular
    .module('app')
    .controller('PostsCtrl', PostsCtrl);

PostsCtrl.$inject = ['$scope', '$routeParams', 'PostService','UserService'];


function PostsCtrl($scope, $routeParams, PostService, UserService) {

    $scope.loading = true;
    UserService.user.$promise.then(function (result) {
        $scope.blogId = result.CurrentBlogId;
    });
    PostService.query(
        function (model) {
            $scope.posts = model;
            $scope.loading = false;
        },
        function(error) {
            $scope.status = Helpers.error('Connection error');
            $scope.loading = false;
        });
    PostService.query();

    $scope.submit = function () {
        if ($('#upload-form').valid()) {
            $scope.loading = true;
            PostService.update({ id: $scope.model.Id }, $scope.model,
                function (success) {
                    $('#statusBox').remove();
                    var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                    $('#upload-form').prepend(status);
                    /* setTimeout(function () { status.fadeOut('slow', function () { $(this).remove(); }) }, 2000);*/ // Info status fade out
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
