using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper;
using Blog.Models;
using Blog.Utility;
using Blog.Services;
using Blog.ViewModels;
using Blog.ViewModels.Blog;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Blog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        private BlogService _service;

        private const int postsPerPage = 5;
        public User CurrentUser 
        {
            get { return db.Users.Find(User.Identity.GetUserId());} 
        }

        public BlogController()
        {
            _service = new BlogService(ModelState);
        }

        // GET: Blog
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Display(string blogName, int page=1)
        {
            var blog = db.Blogs.FirstOrDefault(b => b.UrlName == blogName);
            if (blog == null)
                return HttpNotFound();

            var posts = blog.Posts.Where(p => p.Published).OrderBy(p => p.PublishDate).ToPagedList(page, postsPerPage);
            var layoutSettings = blog.LayoutSettings;
            var viewModel = new DisplayBlog {Blog = blog, Posts = posts, LayoutSettings = layoutSettings};
            //var posts = db.Posts.Where(p => p.Blog.Name == blogName && p.Published);

            _service.IncrementVisitCounter(HttpContext,blog.Id);
            
            return View("Default/Default",viewModel);
        }

        [AllowAnonymous]
        public ActionResult Category(string blogName, string id, int page = 1)
        {
            if (blogName == null)
                return HttpNotFound();

            var blog = db.Blogs.FirstOrDefault(b => b.UrlName == blogName);
            if (blog == null)
                return HttpNotFound();

            var cat = db.PostCategories.FirstOrDefault(c => c.UrlSlug == id && c.BlogId == blog.Id);
            if (cat == null)
                return HttpNotFound();

            var posts = blog.Posts.Where(p => p.Published && p.Category.Id == cat.Id).OrderBy(p => p.PublishDate).ToPagedList(page, postsPerPage);
            var layoutSettings = blog.LayoutSettings;
            var viewModel = new DisplayBlog { Blog = blog, Posts = posts, LayoutSettings = layoutSettings };

            ViewBag.Header = cat.Name + " posts";

            ViewBag.Action = "/category/" + cat.UrlSlug;
            return View("Default/Default", viewModel);

          
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult CategoriesWidget(int blogId)
        {
            var blog =  db.Blogs.Find(blogId);
            var categories = blog != null ? blog.PostCategories : new List<PostCategory>();

            ViewBag.BlogUrl = blog.UrlName;

            return View("Default/Widgets/_CategoriesWidget",categories);
        }

        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult RecentPostsWidget(int blogId)
        {
            var blog = db.Blogs.Find(blogId);
            var posts = blog != null ? blog.Posts.OrderBy( p => p.PublishDate).Take(5) 
                : new List<Post>();

            RecentPostsViewModel vm = new RecentPostsViewModel
            {
                BlogUrlName = blog.UrlName,
                Posts = posts
            };

            return View("Default/Widgets/_RecentPostsWidget", vm);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateBlogViewModel model)
        {
            string currentUserId = User.Identity.GetUserId();

            if(_service.CreateBlog(model, currentUserId))
                return RedirectToAction("Index","AdminPanel");

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name");
            return View(model);
        }


        // GET: Blog2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Models.Blog blog = db.Blogs.Find(id);
            if (blog == null)
                return HttpNotFound();
            if (blog.UserId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var blogViewModel = Mapper.Map<EditBlogViewModel>(blog);

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blog.CategoryId);
            return View(blogViewModel);
        }

        // POST: Blog2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UrlName,Description,MiniatureUrl,CategoryId,UserId")] Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blog.CategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", blog.UserId);
            return View(Mapper.Map < EditBlogViewModel > (blog));
        }
        
    }
}