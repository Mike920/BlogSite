using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
//using System.Web.UI.WebControls;
using AutoMapper;
using Blog.Utility;
using Blog.ViewModels;
using Blog.Models;
using System.Drawing;
using Blog = Blog.Models.Blog;
using Omu.Drawing;

namespace Blog.Services
{
    public class BlogService
    {
        private ModelStateDictionary _modelState;
        private BlogDbContext _db = new BlogDbContext();
        public BlogService(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool CreateBlog(CreateBlogViewModel viewModel, string userId)
        {
            if (!_modelState.IsValid || !ServerTools.Paths.TempFolderContains(viewModel.MiniatureUrl)) return false;

            Models.Blog blog = Mapper.Map<Models.Blog>(viewModel);
            blog.UserId = userId;

            string blogFolderPath = CreateBlogFolder(blog.Name);
            //   string imageDestinationPath = MoveFromTempToBlogDir(viewModel.MiniatureUrl, blogFolderPath);
            string imageDestinationPath = ResizeAndSave(viewModel.MiniatureUrl, blogFolderPath);

            //todo server side image resize
            blog.MiniatureUrl = ServerTools.RelativePath(imageDestinationPath);

            var layoutSettings = new LayoutSettings();
            /*_db.LayoutSettings.Add(layoutSettings);*/
            blog.LayoutSettings = layoutSettings;

            _db.Blogs.Add(blog);
            _db.SaveChanges();

            var postCategory = new PostCategory { Id = 1, Name = "General", BlogId = blog.Id };
            _db.PostCategories.Add(postCategory);

            var user = _db.Users.Find(userId);
            if (user.CurrentBlogId == null)
            {
                user.CurrentBlogId = blog.Id;
                _db.Entry(user).State = EntityState.Modified;
            }
            _db.SaveChanges();

            return true;
        }

        private string CreateBlogFolder(string name)
        {
            string path = Path.Combine(ServerTools.Paths.MediaFolderPath("Blogs"), name);
            Directory.CreateDirectory(path);
            return path;
        }

        private string ResizeAndSave(string tempFile, string blogFolder)
        {
            string tempImgPath = Path.Combine(ServerTools.Paths.TempFolder, tempFile);
            string fileName = "miniature.jpg"; //" + tempFile.Split('.').Last();
            string imageDestinationPath = Path.Combine(blogFolder, fileName);

            //Image img = Image.FromFile(tempImgPath);
            //
            using (FileStream fs = new FileStream(tempImgPath, FileMode.Open, FileAccess.Read))
            {
                using (Image img = Image.FromStream(fs))
                {
                    var resized = Imager.Resize(img, 300, 250, false);
                    Imager.SaveJpeg(imageDestinationPath,img);
                }
            }
                    //
                   
            
            File.Delete(tempImgPath);
            return imageDestinationPath;
        }

        private string MoveFromTempToBlogDir(string tempFile, string blogFolder)
        {
            string sourcePath = Path.Combine(ServerTools.Paths.TempFolder, tempFile);
            string imageDestinationPath = Path.Combine(blogFolder, tempFile);
            File.Move(sourcePath, imageDestinationPath);
            return imageDestinationPath;
        }


        public void IncrementVisitCounter(HttpContextBase httpContext, int blogId)
        {
            string key = "visits" + blogId;
            int value = httpContext.Application[key] as int? ?? 0;

            httpContext.Application[key] = ++value;

            if (value >= 2)
            {
                var blog = _db.Blogs.Find(blogId);
                blog.Visits += value;

                _db.Entry(blog).State = EntityState.Modified;
                _db.SaveChanges();

                httpContext.Application[key] = 0;
            }

        }
    }
}
