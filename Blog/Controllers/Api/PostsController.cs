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
using AutoMapper.QueryableExtensions;
using Blog.Models;
using Blog.Utility;
using Blog.ViewModels;
using Blog.ViewModels.Api;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
    [Authorize]
    public class PostsController : BaseApiController
    {

        // GET: api/Posts
        [AllowAnonymous]
        public IQueryable<PostOnList> GetPosts()
        {
            return Db.Posts.Where(p => p.BlogId == CurrentBlogId).ProjectTo<PostOnList>();
        }

        // GET: api/Posts/5
        [AllowAnonymous]
        [ResponseType(typeof(CreatePost))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = Db.Posts.FirstOrDefault(p => p.Id == id && p.BlogId == CurrentBlogId);
            if (post == null)
                return NotFound();

            return Ok(Mapper.Map<CreatePost>(post));
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

            var postDb = Db.Posts.FirstOrDefault(p => p.Id == post.Id && p.BlogId == CurrentBlogId);

            if (postDb == null)
                return BadRequest("Post not found");

            Mapper.Map(post, postDb);
            postDb.UrlName = ServerTools.GenerateUrlFriendlyString(postDb.Title);


            Db.Entry(postDb).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
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

            Db.Posts.Add(postModel);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }



        // DELETE: api/Posts/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = Db.Posts.SingleOrDefault(p => p.Id == id && p.BlogId == CurrentBlogId);
            
            if (post == null)
            {
                return NotFound();
            }


            Db.Posts.Remove(post);
            Db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return Db.Posts.Count(e => e.Id == id) > 0;
        }

        public User CurrentUser {
            get
            {
                return Db.Users.Find(User.Identity.GetUserId());
            } 
        }
        public Models.Blog CurrentBlog
        {
            get
            {
                return Db.Blogs.Find(CurrentUser.CurrentBlogId);
            }
        }
    }
}