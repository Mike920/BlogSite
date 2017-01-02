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
using AutoMapper;
using Blog.Utility;

namespace Blog.Controllers.Api
{
    [Authorize]
    public class PostCategoriesController : BaseApiController
    {
       
        // GET: api/PostCategories
        public IQueryable<EditPostCategory> GetPostCategories()
        {
            var blogId = CurrentBlogId;
            return Db.PostCategories.Where(p => p.BlogId == blogId).ProjectTo<EditPostCategory>();
        }

        // GET: api/PostCategories/5
        [ResponseType(typeof(PostCategory))]
        public IHttpActionResult GetPostCategory(int id)
        {
            PostCategory postCategory = Db.PostCategories.Where(p => p.Id == id && p.BlogId == CurrentBlogId.Value).FirstOrDefault();
            if (postCategory == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EditPostCategory>(postCategory));
        }

        // PUT: api/PostCategories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPostCategory(int id, EditPostCategory postCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postCategory.Id)
            {
                return BadRequest();
            }

            var postDb = Db.PostCategories.Where(p => p.Id == postCategory.Id && p.BlogId == CurrentBlogId).FirstOrDefault();

            if (postDb == null)
                return BadRequest("Post not found");

            Mapper.Map(postCategory, postDb);
            postDb.UrlSlug = ServerTools.GenerateUrlFriendlyString(postCategory.Name);

            Db.Entry(postDb).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
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
            postCategory.BlogId = CurrentBlogId.Value;
            postCategory.UrlSlug = ServerTools.GenerateUrlFriendlyString(postCategory.Name);
            Db.PostCategories.Add(postCategory);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = postCategory.Id }, postCategory);
        }

        // DELETE: api/PostCategories/5
        [ResponseType(typeof(PostCategory))]
        public IHttpActionResult DeletePostCategory(int id)
        {
            PostCategory postCategory = Db.PostCategories.Where(p => p.Id == id && p.BlogId == CurrentBlogId.Value).FirstOrDefault();
            if (postCategory == null)
                return NotFound();
            

            Db.PostCategories.Remove(postCategory);
            Db.SaveChanges();

            return Ok(postCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostCategoryExists(int id)
        {
            return Db.PostCategories.Count(e => e.Id == id) > 0;
        }
    }
}