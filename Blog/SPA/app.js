(function () {
'use strict';

var app = angular.module('app', ['ngResource', 'ngRoute']);

app.config(['$routeProvider', function ($routeProvider) {


    $routeProvider.when('/BlogSettings/:id', {
        templateUrl: '/AdminPanel/EditBlog',
        controller: 'BlogSettingsCtrl',
        caseInsensitiveMatch: true
    });
/*    $routeProvider.when('/simple', {
        templateUrl: 'app/simple/simple.html',
        controller: 'simpleCtrl',
        caseInsensitiveMatch: true
    });
    $routeProvider.when('/advanced', {
        templateUrl: 'app/advanced/advanced.html',
        controller: 'advancedCtrl',
        caseInsensitiveMatch: true
    });*/
    $routeProvider.otherwise({
        templateUrl: '/AdminPanel/Dashboard',
        controller: 'DashboardCtrl',
        caseInsensitiveMatch: true
    });
}]);

app.run([function () {
}]);
})();