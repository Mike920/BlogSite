﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    [Authorize]
    public class NgPartialsController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult EditBlog()
        {
            return View();
        }
    }
}