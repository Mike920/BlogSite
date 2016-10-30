using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
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
        public ActionResult Index()
        {
            var blogs = db.Blogs.OrderBy(b => b.Visits).Take(postsPerPage).ProjectTo<BlogViewModel>()
                .ToPagedList(1, postsPerPage);

            return View(blogs);
        }

        [AllowAnonymous]
        public ActionResult Category(string id, int page = 1)
        {
            IQueryable<Models.Blog> blogs = db.Blogs.OrderBy(b => b.Name);
            if (id == null)
                ViewBag.Header = "All blogs";
            else
            {
                var cat = db.BlogCategories.FirstOrDefault(c => c.UrlSlug == id);

                if (cat == null)
                    return HttpNotFound();

                blogs = blogs.Where(b => b.Category.UrlSlug == id);

                ViewBag.Header = cat.Name + " blogs";
            }

            var viewModel = blogs.ProjectTo<BlogViewModel>().ToPagedList(page, postsPerPage);
            ViewBag.Action = "Category";
            return View("Index", viewModel);
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