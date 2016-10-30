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
using Blog.Models;
using Blog.Services;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Blog.Controllers.Api
{
    [System.Web.Mvc.Authorize]
    public class LayoutSettingsController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();


        // GET: api/LayoutSettings
        public IQueryable<LayoutSettings> GetLayoutSettings()
        {
            return db.LayoutSettings;
        }

        // GET: api/LayoutSettings/5
        [ResponseType(typeof(LayoutSettings))]
        public IHttpActionResult GetLayoutSettings(bool forCurrentUser)
        {

            var blogId = CurrentUser.CurrentBlogId;
            if(CurrentUser.CurrentBlogId == null)
                return NotFound();

            LayoutSettings layoutSettings = db.LayoutSettings.Find(blogId);
            if (layoutSettings == null)
                return NotFound();

            return Ok(layoutSettings);
        }

        // PUT: api/LayoutSettings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLayoutSettings([Bind(Include = "WidgetsColumnSide,WidgetList")] LayoutSettings layoutSettings)
        {
            layoutSettings.Id = CurrentUser.CurrentBlogId.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(layoutSettings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LayoutSettingsExists(layoutSettings.Id))
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

        // POST: api/LayoutSettings
        [ResponseType(typeof(LayoutSettings))]
        public IHttpActionResult PostLayoutSettings(LayoutSettings layoutSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LayoutSettings.Add(layoutSettings);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LayoutSettingsExists(layoutSettings.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = layoutSettings.Id }, layoutSettings);
        }

        // DELETE: api/LayoutSettings/5
        [ResponseType(typeof(LayoutSettings))]
        public IHttpActionResult DeleteLayoutSettings(int id)
        {
            LayoutSettings layoutSettings = db.LayoutSettings.Find(id);
            if (layoutSettings == null)
            {
                return NotFound();
            }

            db.LayoutSettings.Remove(layoutSettings);
            db.SaveChanges();

            return Ok(layoutSettings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LayoutSettingsExists(int id)
        {
            return db.LayoutSettings.Count(e => e.Id == id) > 0;
        }

      
        public User CurrentUser
        {
            [System.Web.Mvc.Authorize]
            get
            {
                return db.Users.Find(User.Identity.GetUserId());
            }
        }
    }
}