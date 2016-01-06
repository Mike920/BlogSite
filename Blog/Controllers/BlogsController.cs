using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.Services;
using Microsoft.AspNet.Identity;
using PagedList;

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

        [AllowAnonymous]
        public ActionResult Index(int page = 1)
        {
           // var blogs = db.Blogs; //MyProductDataSource.FindAllProducts(); //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var blogs = db.Blogs.OrderBy(b => b.Visits).ToPagedList(page, 10); // will only contain 25 products max because of the pageSize


            return View(blogs);
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