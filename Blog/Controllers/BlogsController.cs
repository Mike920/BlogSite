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

        private const int postsPerPage = 12;

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

            var blogs = db.Blogs.OrderBy(b => b.Visits).Take(12).ToPagedList(page, postsPerPage); // will only contain 25 products max because of the pageSize


            return View(blogs);
        }

        [AllowAnonymous]
        public ActionResult Category(string id, int page = 1)
        {
            IEnumerable<Models.Blog> blogs;
            if (id == null)
            {
                blogs = db.Blogs.OrderBy(b => b.Name).ToPagedList(page, postsPerPage);
                ViewBag.Header = "All blogs";
            }
            else
            {
                var cat = db.BlogCategories.FirstOrDefault(c => c.UrlSlug == id);

                if (cat == null)
                    return HttpNotFound();

                blogs = db.Blogs.Where(b => b.Category.UrlSlug == id)
                    .OrderBy(b => b.Name)
                    .ToPagedList(page, postsPerPage);

                ViewBag.Header = cat.Name + " blogs";
            }

            ViewBag.Action = "Category";
            return View("Index",blogs);
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