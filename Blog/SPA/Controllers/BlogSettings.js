'use strict';

angular
    .module('app')
    .controller('BlogSettingsCtrl', BlogSettingsCtrl);

BlogSettingsCtrl.$inject = ['$scope', '$routeParams', 'BlogService'];


function BlogSettingsCtrl($scope, $routeParams, BlogService) {
    BlogService.get({ id: $routeParams.id },
        function (model) {
            $scope.model = model;
        },
        function(error) {
            $('#upload-form').prepend();
        });
    BlogService.query();

    $scope.submit = function () {
        if ($('#upload-form').valid()) {
            BlogService.update({ id: $scope.model.Id }, $scope.model,
                function (success) {
                    $('#statusBox').remove();
                    var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                    $('#upload-form').prepend(status);
                    /* setTimeout(function () { status.fadeOut('slow', function () { $(this).remove(); }) }, 2000);*/ // Info status fade out
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

                    }
                });
            
        }
        var someWeirdShitHere = 2;
    };
}
