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
    public class BlogCategoriesController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        //public string CurrentUserId { get; set; }

        //public BlogCategoriesController()
        //{
        //    CurrentUserId = User.Identity.GetUserId();
        //}
        // GET: api/BlogCategories
        public IQueryable<EditBlogCategory> GetBlogCategories()
        {
            return db.BlogCategories.ProjectTo<EditBlogCategory>();
        }

        // GET: api/BlogCategories/5
        [ResponseType(typeof(BlogCategory))]
        public IHttpActionResult GetBlogCategory(int id)
        {
            BlogCategory BlogCategory = db.BlogCategories.Find(id);
            if (BlogCategory == null)
            {
                return NotFound();
            }

            return Ok(BlogCategory);
        }

        // PUT: api/BlogCategories/5
        [ResponseType(typeof(void))]
        private IHttpActionResult PutBlogCategory(int id, BlogCategory BlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != BlogCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(BlogCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogCategoryExists(id))
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

        // POST: api/BlogCategories
        [ResponseType(typeof(BlogCategory))]
        private IHttpActionResult PostBlogCategory(BlogCategory BlogCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlogCategories.Add(BlogCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = BlogCategory.Id }, BlogCategory);
        }

        // DELETE: api/BlogCategories/5
        [ResponseType(typeof(BlogCategory))]
        private IHttpActionResult DeleteBlogCategory(int id)
        {
            BlogCategory BlogCategory = db.BlogCategories.Find(id);
            if (BlogCategory == null)
            {
                return NotFound();
            }

            db.BlogCategories.Remove(BlogCategory);
            db.SaveChanges();

            return Ok(BlogCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogCategoryExists(int id)
        {
            return db.BlogCategories.Count(e => e.Id == id) > 0;
        }
    }
}