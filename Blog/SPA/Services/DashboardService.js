'use strict';

/* Services */

angular
    .module('app')
    .factory('DashboardService', DashboardService); 

DashboardService.$inject = ['$resource'];

function DashboardService($resource) {
    return $resource('/api/dashboard/:id', { id: '@id' },
            {
                'update': { method: 'PUT' }
            });
      };
