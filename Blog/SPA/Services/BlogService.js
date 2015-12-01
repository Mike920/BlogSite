'use strict';

/* Services */

angular.module('app')
    .factory('BlogService', 
      function ($resource) {
          return $resource('/api/blogs/:id', null,
            {
                'update': { method: 'PUT' }
            });
          /*return $resource('api/blogs/:blogId.json', {}, {
              query: { method: 'GET', params: { blogId: 'blogs' }, isArray: true }
          });*/
      });
