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
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        private BlogService _service;

        public User CurrentUser 
        {
            get { return db.Users.Find(User.Identity.GetUserId());} 
        }

        public BlogController()
        {
            _service = new BlogService(ModelState);
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View();
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

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "BlogCategoryId", "BlogCategoryId");
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

            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blog.BlogCategoryId);
            return View(blogViewModel);
        }

        // POST: Blog2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UrlName,Description,ImageUrl,BlogCategoryId,UserId")] Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryId = new SelectList(db.BlogCategories, "Id", "Name", blog.BlogCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", blog.UserId);
            return View(Mapper.Map < EditBlogViewModel > (blog));
        }
        
    }
}