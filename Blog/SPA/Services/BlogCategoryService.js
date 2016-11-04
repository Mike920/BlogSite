'use strict';

/* Services */

angular
    .module('app')
    .factory('BlogCategoryService', BlogCategoryService); 

BlogCategoryService.$inject = ['$resource'];

function BlogCategoryService($resource) {
    return $resource('/api/BlogCategories/:id', null,
            {
                'update': { method: 'PUT' }
            });
      };
