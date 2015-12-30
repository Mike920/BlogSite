'use strict';

/* Services */

angular
    .module('app')
    .factory('LayoutSettingsService', LayoutSettingsService); 

LayoutSettingsService.$inject = ['$resource'];

function LayoutSettingsService($resource) {
          return $resource('/api/layoutsettings/:id', null,
            {
                'update': { method: 'PUT' }
            });
      };
