'use strict';

/* Services */

angular
    .module('app')
    .factory('HelpersService', HelpersService);

function HelpersService() {
/*
    var resource = $resource('/api/CurrentUser', null,
      {
          'update': { method: 'PUT' }
      });

    var user = null;

    var getCurrentUser = function() {
        if (user === null) {
            user = resource.get();
        }
        return user;
    }
*/

    
    return {
        user: getCurrentUser(),
        resource: resource,
        refresh: function() {
            user = resource.get();
        }
    };
};
