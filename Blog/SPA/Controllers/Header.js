'use strict';

angular
    .module('app')
    .controller('HeaderCtrl', HeaderCtrl);

HeaderCtrl.$inject = ['$scope', 'BlogService', 'Helpers'];


function HeaderCtrl($scope , BlogService, Helpers) {

    $scope.loading = true;
    $scope.model = {};

    BlogService.get({ currentBlog: true },
        function (model) {
            $scope.model = model;
            $scope.loading = false;
        },
        function (error) {
            $scope.status = Helpers.error('Error');
            $scope.loading = false;
        });

    $scope.submit = function () {
            $scope.loading = true;
            BlogService.update({ currentBlog: true, editHeader: true }, $scope.model,
                function (success) {
                    $scope.status = { msg: "Changes have been saved.", clas: "alert-success" };
                    $("#imageInput").trigger('dispose');
                    $scope.model.HeaderUrl = success.HeaderUrl + '?decache=' + (new Date()).toString();
                    $('#image').attr('src', $scope.model.HeaderUrl);
                    $scope.model.File = null;
                   /* $('#statusBox').remove();
                    var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                    $('#upload-form').prepend(status);*/
                    $scope.loading = false;
                },
                function (error) {
                    if (error.status === 400) { //validation error
                        var ms = error.data.ModelState;
                        // Display validation errors
                        $(Object.keys(ms)).each(function (index, key) {
                            var val = key.split('.').pop();
                            var errorSpan = $("span[data-valmsg-for='" + val + "']");
                            errorSpan.html("<span style='color:#b94a48'>" + ms[key][0] + "</span>");
                            errorSpan.show();
                        });
                        $scope.loading = false;
                    } else {
                        $scope.status = { mgs: "Error.", clas: "alert-error" };
                    }
                });
    };
}
