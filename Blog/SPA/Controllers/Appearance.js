'use strict';

angular
    .module('app')
    .controller('AppearanceCtrl', AppearanceCtrl);

AppearanceCtrl.$inject = ['$scope', '$routeParams', 'LayoutSettingsService', 'UserService'];


function AppearanceCtrl($scope, $routeParams, LayoutSettingsService,UserService) {

    $scope.loading = true;

}
