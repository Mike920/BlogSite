using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class ValidationController : Controller
    {
        BlogDbContext db = new BlogDbContext();

        public JsonResult IsBlogNameUnique(string name)
        {
            if (db.Blogs.FirstOrDefault( b => b.Name == name) == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(string.Format("Blog '{0}' already exists", name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBlogNameUniqueOrUnchanged(string name, int Id)
        {
            var blog = db.Blogs.FirstOrDefault(b => b.Name == name);
            if (blog == null || blog.Id == Id)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(string.Format("Blog '{0}' already exists", name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBlogUrlUnique(string url)
        {
            if (db.Blogs.FirstOrDefault(b => b.UrlName == url) == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(string.Format("Blog with such address already exists", url), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBlogUrlUniqueOrUnchanged(string url, int Id)
        {
            var blog = db.Blogs.FirstOrDefault(b => b.UrlName == url);
            if ( blog == null || blog.Id == Id)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(string.Format("Blog with such address already exists", url), JsonRequestBehavior.AllowGet);
        }
    }
}