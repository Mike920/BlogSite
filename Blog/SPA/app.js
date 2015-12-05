(function () {
'use strict';

var app = angular.module('app', ['ngResource', 'ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {


    $routeProvider.when('/BlogSettings/:id', {
        templateUrl: '/AdminPanel/EditBlog',
        controller: 'BlogSettingsCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.otherwise({
        templateUrl: '/AdminPanel/Dashboard',
        controller: 'DashboardCtrl',
        caseInsensitiveMatch: true
    });

    // use the HTML5 History API
    $locationProvider.html5Mode(true);
}]);

app.run([function () {
}]);
})();