﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.ViewModels;

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        private BlogDbContext db = new BlogDbContext{Configuration = { LazyLoadingEnabled = true}};

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Blog);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(string blogName, int postId)
        {
            Post post = db.Posts.Find(postId);
            if (post == null)
                return HttpNotFound();
            var blog = db.Blogs.Find(post.BlogId);
            var layout = db.LayoutSettings.Find(blog.Id);
            var model = new DisplayPost {Post = post, Blog = blog, LayoutSettings = layout};

            return View("Themes/Default",model);
        }



        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,UrlName,Content,Published,PublishDate,Modified,BlogId,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,UrlName,Content,Published,PublishDate,Modified,BlogId,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.Blogs, "Id", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
