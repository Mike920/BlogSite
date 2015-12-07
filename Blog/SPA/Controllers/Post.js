'use strict';

angular
    .module('app')
    .controller('PostCtrl', PostCtrl);

PostCtrl.$inject = ['$scope', '$routeParams', 'PostService'];


function PostCtrl($scope, $routeParams, PostService) {

    $scope.loading = true;

    PostService.get({ id: $routeParams.id },
        function (model) {
            $scope.model = model;
            $scope.loading = false;
        },
        function(error) {
            $('#upload-form').prepend();
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
