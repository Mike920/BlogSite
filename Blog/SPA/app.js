(function () {
'use strict';

var app = angular.module('app', ['ngResource', 'ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {


    $routeProvider.when('/BlogSettings/:id', {
        templateUrl: '/NgPartials/EditBlog',
        controller: 'BlogSettingsCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.when('/Posts/', {
        templateUrl: '/NgPartials/Posts',
        controller: 'PostCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.when('/Posts/Add', {
        templateUrl: '/NgPartials/PostAdd',
        controller: 'PostAddCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.when('/Dashboard', {
        templateUrl: '/NgPartials/Dashboard',
        controller: 'DashboardCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.otherwise({
        redirectTo: '/Dashboard'
    });

    // use the HTML5 History API
    $locationProvider.html5Mode(true);
}]);

app.run([function () {
}]);
})();