'use strict';

angular
    .module('app')
    .controller('UserCtrl', UserCtrl);

UserCtrl.$inject = ['$scope', '$routeParams', 'UserService'];


function UserCtrl($scope, $routeParams, UserService) {
    $scope.user = UserService.user;
}
