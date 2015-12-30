using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Services
{
    class BlogsService
    {
         private ModelStateDictionary _modelState;
        private BlogDbContext _db;
        public BlogsService(ModelStateDictionary modelState)
        {
            _modelState = modelState;
            _db = new BlogDbContext();
        }


    }
}
