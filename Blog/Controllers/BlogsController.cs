using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        private BlogsService _service;

        public BlogsController()
        {
            _service = new BlogsService(ModelState);
        }
        public User CurrentUser
        {
            get { return db.Users.Find(User.Identity.GetUserId()); }
        }

        public ActionResult ActiveBlog()
        {
            var blogId = CurrentUser.CurrentBlogId;
            if (blogId == null)
                return HttpNotFound();

            var blog = db.Blogs.Find(blogId);
            if (blog == null)
                return HttpNotFound();

            return Redirect("/Blog/" + blog.UrlName);
        }
    }
}