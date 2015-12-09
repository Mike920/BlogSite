'use strict';

/* Services */

angular
    .module('app')
    .factory('PostCategoryService', PostCategoryService);

PostCategoryService.$inject = ['$resource'];

function PostCategoryService($resource) {
          return $resource('/api/postcategories/', null,
            {
                'update': { method: 'PUT' }
            });
      };
