'use strict';

angular
    .module('app')
    .controller('LayoutCtrl', LayoutCtrl);

LayoutCtrl.$inject = ['$scope', '$routeParams', 'LayoutSettingsService', 'UserService'];


function LayoutCtrl($scope, $routeParams, LayoutSettingsService,UserService) {

    $scope.loading = true;

    $scope.widgets = ['Author', 'Categories', 'Recent Posts'];


    LayoutSettingsService.get({ forCurrentUser: true },
        function (model) {
            $scope.model = model;
            $scope.loading = false;

            //remove duplicates
            $($scope.model.WidgetList).each(function(i, val1) {
                $($scope.widgets).each(function(j, val2) {
                    if (val1 === val2) {
                        $scope.widgets.splice(j, 1);
                    }
                });
            });
        },
        function (error) {
            $('#statusBox').remove();
            var status = $('<div id="statusBox" class="alert alert-error" role="alert">Connection error.</div>').hide().fadeIn('normal');
            $('#upload-form').prepend(status);
            $scope.loading = false;
        });

    $scope.submit = function () {
        $scope.loading = true;

        $scope.model.Widgets = "";
        $($scope.model.WidgetList).each(function (i, val1) {
            $scope.model.Widgets += val1 + ';';
        });

        LayoutSettingsService.update( $scope.model,
            function (success) {
                $scope.status = { msg: "Changes have been saved.", clas: "alert-success" };
               
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

    $scope.sortableOptions = {
        placeholder: "app",
        connectWith: ".apps-container",
        update: function (event, ui) {
            // on cross list sortings recieved is not true
            // during the first update
            // which is fired on the source sortable
            if (!ui.item.sortable.received) {
                var originNgModel = ui.item.sortable.sourceModel;
                var itemModel = originNgModel[ui.item.sortable.index];

                // check that its an actual moving
                // between the two lists
                if (originNgModel == $scope.widgets &&
                    ui.item.sortable.droptargetModel == $scope.model.WidgetList) {
                    var exists = !!$scope.model.WidgetList.filter(function (x) { return x === itemModel }).length;
                    if (exists) {
                        ui.item.sortable.cancel();
                    }
                }
            }
        }
    };

}
