using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Blog.Models;
using Blog.Utility;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
    [Authorize]
    public class PostsController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: api/Posts
        [AllowAnonymous]
        public IQueryable<Post> GetPosts()
        {
            return db.Posts;
        }

        // GET: api/Posts/5
        [AllowAnonymous]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, CreatePost post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostPost(CreatePost post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Models.Post postModel = Mapper.Map<Models.Post>(post);

            if (postModel.Published)
                postModel.PublishDate = DateTime.Now;

            postModel.BlogId = (int)CurrentUser.CurrentBlogId;
            postModel.UrlName = ServerTools.GenerateUrlFriendlyString(postModel.Title);
            //todo create tags

            db.Posts.Add(postModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }



        // DELETE: api/Posts/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }

        public User CurrentUser {
            get
            {
                return db.Users.Find(User.Identity.GetUserId());
            } 
        }
        public Models.Blog CurrentBlog
        {
            get
            {
                return db.Blogs.Find(CurrentUser.CurrentBlogId);
            }
        }
    }
}