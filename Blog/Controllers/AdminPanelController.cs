using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            if (db.Users.Find(User.Identity.GetUserId()).CurrentBlogId == null)
                return Redirect("/Blog/Create");
            return View();
        }


    }
}