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
using System.Web.Http.OData;
using System.Web.Http.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
    public class BlogsController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: api/Blogs
        //Odata query example: /api/blogs?$filter=Category eq 'General'
        [EnableQuery]
        public IQueryable<EditBlogViewModel> GetBlogs(bool forCurrentUser = false)
        {
            if (forCurrentUser)
            {
                string currentUser = User.Identity.GetUserId();
                if ( currentUser == null)
                    return null;
                return db.Blogs.Where(b => b.UserId == currentUser)
                                .ProjectTo<EditBlogViewModel>();
            }
            return db.Blogs.ProjectTo<EditBlogViewModel>();
        }

        // GET: api/Blogs/5
        [ResponseType(typeof(EditBlogViewModel))]
        public IHttpActionResult GetBlog(int id)
        {
            var blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            var vm = Mapper.Map<EditBlogViewModel>(blog);
            return Ok(vm);
        }

        // PUT: api/Blogs/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlog(int id, EditBlogViewModel blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blog.Id)
            {
                return BadRequest();
            }

            Models.Blog blogModel = db.Blogs.Find(id);

            if (blogModel == null)
                return NotFound();

            if (!BelongsToCurrentUser(blogModel))
                return Unauthorized();

            Mapper.Map(blog, blogModel);

            db.Entry(blogModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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

        private bool BelongsToCurrentUser(Models.Blog blog)
        {
            return User.Identity.GetUserId() == blog.UserId;
        }

        // POST: api/Blogs
        [ResponseType(typeof(Models.Blog))]
        public IHttpActionResult PostBlog(Models.Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blogs.Add(blog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [ResponseType(typeof(Models.Blog))]
        public IHttpActionResult DeleteBlog(int id)
        {
            Models.Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }

            db.Blogs.Remove(blog);
            db.SaveChanges();

            return Ok(blog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogExists(int id)
        {
            return db.Blogs.Count(e => e.Id == id) > 0;
        }
    }
}