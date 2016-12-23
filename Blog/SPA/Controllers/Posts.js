'use strict';

angular
    .module('app')
    .controller('PostsCtrl', PostsCtrl);

PostsCtrl.$inject = ['$scope', '$routeParams', 'PostService','UserService', '$filter'];


function PostsCtrl($scope, $routeParams, PostService, UserService, $filter) {

    $scope.loading = true;
    UserService.user.$promise.then(function (result) {
        $scope.blogId = result.CurrentBlogId;

            $('#posts-grid').DataTable({
                "ajax": {
                    "url": "/api/posts",
                    "dataSrc": "Posts"
                },

                "columns": [
                {
                    "data": "Title",
                    render: titleTemplate
                },
                {
                    "data": "PublishDate",
                    render: dateTemplate
                },
                {
                    "data": "UrlName",
                    render: urlTemplate
                }
                ]
            });

            function titleTemplate(data, type, row) {
                var temp = '<a href="/Blog/' + result.CurrentBlogId+ '/' + row.Id + '/' + row.UrlName + '" target="_blank">' + row.Title + '</a>';
                return temp;
            };
            
            function dateTemplate(data, type, row) {
                return $filter('date')(row.PublishDate,'MMM d, y HH:mm');
            };

            function urlTemplate(data, type, row) {
                return '<a href="Posts/Edit/' + row.Id +'" class="btn-sm btn-success">Edit</a>'
                  + '<a href="" style="margin-left: 5px;" class="btn-sm btn-danger">Delete</a>';
            };
    });
    //PostService.query(
    //    function (model) {
    //        $scope.posts = model;
    //        model.forEach(function (e) {
    //            //e.Link = "/Blog/" + $scope.blogId + "/" + e.Id + "/" + e.UrlName;
                
    //            e.Link = "/Blog/" + $scope.blogId + "/" + e.Id + "/" + e.UrlName;
    //                // '<a ng-href="' + link + '" target="_blank">' + e.Title + '</a>'
                
    //        });
    //        $scope.gridOptions.data = model;
    //        $scope.loading = false;
    //    },
    //    function(error) {
    //        $scope.status = Helpers.error('Connection error');
    //        $scope.loading = false;
    //    });
    //PostService.query();

    $scope.gridOptions = {
        enableSorting: true,
        columnDefs: [
          {
              name: 'Title', field: 'Link',  width: '600', height:'*',
              cellTemplate: '<a ng-href="{{row.entity.Link}}" target="_blank">{{row.entity.Title}}</a>'
          },
          { name: 'Date', field: 'PublishDate' },
          {
              name: 'Actions',
              cellTemplate: '<a href="Posts/Edit/{{row.entity.Id}}" class="btn-sm btn-success">Edit</a>'
                  + '<a href="" style="margin-left: 5px;" class="btn-sm btn-danger">Delete</a>'
          }
          //{ name: '1stFriend', field: 'friends[0]' },
          //{ name: 'city', field: 'address.city' },
          //{ name: 'getZip', field: 'getZip()', enableCellEdit: false }
        ],
        //data: [{
        //    //"first-name": "Cox",
        //    //"friends": ["friend0"],
        //    //"address": { street: "301 Dove Ave", city: "Laurel", zip: "39565" },
        //    //"getZip": function () { return this.address.zip; },

        //    "Title": function () { return "/Blog/" + $scope.blogId+ "/" +this.Id + "/" + this.UrlName; }
        //}
        //]
    };

    $scope.submit = function () {
        if ($('#upload-form').valid()) {
            $scope.loading = true;
            PostService.update({ id: $scope.model.Id }, $scope.model,
                function (success) {
                    $('#statusBox').remove();
                    var status = $('<div id="statusBox" class="alert alert-success" role="alert">Changes have been saved.</div>').hide().fadeIn('normal');
                    $('#upload-form').prepend(status);
                    /* setTimeout(function () { status.fadeOut('slow', function () { $(this).remove(); }) }, 2000);*/ // Info status fade out
                    $scope.loading = false;
                },
                function(error) {
                    if (error.status === 400) { //validation error
                        var ms = error.data.ModelState;
                        // Display validation errors
                        $(Object.keys(ms)).each(function(index, key) {
                            var val = key.split('.').pop();
                            var errorSpan = $("span[data-valmsg-for='" + val + "']");
                            errorSpan.html("<span style='color:#b94a48'>" + ms[key][0] + "</span>");
                            errorSpan.show();
                        });
                        $scope.loading = false;
                    }
                });
            
        }
    };
}
