using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Utility;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers
{
    public class FileController : Controller
    {
        private BlogDbContext db = new BlogDbContext();
        private FileService _fileService;

        public FileController()
        {
            _fileService = new FileService(ModelState);
        }

        [HttpPost]
        public JsonResult UploadTempFile()
        {
            IEnumerable<string> fileNames = _fileService.UploadToTempDir(HttpContext.Request.Files);
            
            return Json(fileNames);
        }
    }
}