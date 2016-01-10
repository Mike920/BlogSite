using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog.Models;
using Blog.ViewModels.Api;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
    [Authorize]
    public class DashboardController : BaseApiController
    {

        // GET: api/Dashboard
        public IHttpActionResult Get()
        {
            if(CurrentBlogId == null)
                return BadRequest("No blog selected.");

            var userId = User.Identity.GetUserId();

            var blogs = Db.Blogs.Where(b => b.UserId == userId)
                        .Select(blog => new BlogDashboard {Id = blog.Id, Name = blog.Name});

            var currentBlog = Db.Blogs.Where(b => b.Id == CurrentBlogId)
                .Select( blog => new BlogStatistics {Id = blog.Id, Name = blog.Name, Visits = blog.Visits, TotalPosts = blog.Posts.Count})
                .SingleOrDefault();

            DashboardViewModel vm = new DashboardViewModel { CurrentBlog = currentBlog, Blogs = blogs };


            return Ok(vm);
        }

        // GET: api/Dashboard/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dashboard/5
        public IHttpActionResult Put(int id)
        {
            var userId = User.Identity.GetUserId();
            var blog = Db.Blogs.Where(b => b.Id == id && b.UserId == userId)
                .Select(b => new BlogStatistics {Id = b.Id, Name = b.Name, Visits = b.Visits, TotalPosts = b.Posts.Count})
                .FirstOrDefault();
                
            if (blog == null)
                return Unauthorized();

            var user = Db.Users.Find(userId);
            user.CurrentBlogId = id;
            Db.Entry(user).State = EntityState.Modified;
            Db.SaveChanges();

            return Ok(blog);
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
        }
    }
}
