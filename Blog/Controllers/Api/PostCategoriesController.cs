using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper.QueryableExtensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
    [Authorize]
    public class PostCategoriesController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        public string CurrentUserId { get; set; }

        public PostCategoriesController()
        {
            CurrentUserId = User.Identity.GetUserId();
        }
        // GET: api/PostCategories
        public IQueryable<EditPostCategory> GetPostCategories()
        {
            var blogId = db.Blogs.FirstOrDefault(blog => blog.UserId == CurrentUserId).Id;

            return db.PostCategories.Where(p => p.BlogId == blogId).ProjectTo<EditPostCategory>();
        }

        // GET: api/PostCategories/5
        [ResponseType(typeof(PostCategory))]
        public IHttpActionResult GetPostCategory(int id)
        {
            PostCategory postCategory = db.PostCategories.Find(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return Ok(postCategory);
        }

        // PUT: api/PostCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPostCategory(int id, PostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(postCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PostCategories
        [ResponseType(typeof(PostCategory))]
        public IHttpActionResult PostPostCategory(PostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PostCategories.Add(postCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = postCategory.Id }, postCategory);
        }

        // DELETE: api/PostCategories/5
        [ResponseType(typeof(PostCategory))]
        public IHttpActionResult DeletePostCategory(int id)
        {
            PostCategory postCategory = db.PostCategories.Find(id);
            if (postCategory == null)
            {
                return NotFound();
            }

            db.PostCategories.Remove(postCategory);
            db.SaveChanges();

            return Ok(postCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostCategoryExists(int id)
        {
            return db.PostCategories.Count(e => e.Id == id) > 0;
        }
    }
}