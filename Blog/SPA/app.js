(function () {
'use strict';

var app = angular.module('app', ['ngResource', 'ngRoute', 'ui.sortable']);

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
    $routeProvider.when('/Appearance/Layout', {
        templateUrl: '/NgPartials/EditLayout',
        controller: 'LayoutCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.when('/Appearance/Header', {
        templateUrl: '/NgPartials/EditHeader',
        controller: 'HeaderCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.otherwise({
        redirectTo: '/Dashboard'
    });

    // use the HTML5 History API
    $locationProvider.html5Mode(true);
}]);

app.run(['$rootScope', 'UserService', function ($rootScope, UserService) {
    UserService.refresh();
}]);
})();