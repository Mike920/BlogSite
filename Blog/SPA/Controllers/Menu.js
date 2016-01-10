'use strict';

angular
    .module('app')
    .controller('MenuCtrl', MenuCtrl);

MenuCtrl.$inject = ['$scope'];


function MenuCtrl($scope) {
    var setActive = function () {
        var url = window.location.pathname.replace("/AdminPanel/", "");
        $(".sidebar-menu a").each(function(e) {
            $(this).parentsUntil("#sidebar-menu").removeClass("active");
        });
        $(".sidebar-menu a").each(function (e) {
            if ($(this).attr("href") === url || $(this).attr("href") + '/' === url) {
                $(this).parentsUntil("#sidebar-menu").addClass("active");
            }
        });
    };

    setActive();

    $scope.$on('$routeChangeSuccess', setActive);

}
