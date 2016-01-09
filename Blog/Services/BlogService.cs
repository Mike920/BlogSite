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

        const int ImgWidth = 360, ImgHeight = 300;

        public BlogService(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool CreateBlog(CreateBlogViewModel viewModel, string userId)
        {
            if (viewModel.UrlName.ToLower() != ServerTools.GenerateUrlFriendlyString(viewModel.UrlName))
                _modelState.AddModelError("UrlName","Url name may only consist of letters, numbers and dashes.");
            
            if (!_modelState.IsValid || 
                (viewModel.MiniatureUrl != null && !ServerTools.Paths.TempFolderContains(viewModel.MiniatureUrl))) return false;



            Models.Blog blog = Mapper.Map<Models.Blog>(viewModel);
            blog.UserId = userId;

            string blogFolderPath = CreateBlogFolder(blog.UrlName);

            if (viewModel.MiniatureUrl != null)
            {
                bool invalidImage = false;
                string tempImgPath = Path.Combine(ServerTools.Paths.TempFolder, viewModel.MiniatureUrl);
                string imageDestinationPath = "";
                using (var img = Image.FromFile(tempImgPath)) 
                { 
                    if (!ValidateImage(img))
                        invalidImage = true;
                    else
                        imageDestinationPath = ResizeAndSave(img,viewModel.MiniatureUrl, blogFolderPath);
                }
                File.Delete(tempImgPath);
                if (invalidImage)
                    return false;
                blog.MiniatureUrl = ServerTools.RelativePath(imageDestinationPath);
            }
            else
                blog.MiniatureUrl = "/MediaData/Default/miniature.jpg";

            var layoutSettings = new LayoutSettings();
            /*_db.LayoutSettings.Add(layoutSettings);*/
            blog.LayoutSettings = layoutSettings;

            _db.Blogs.Add(blog);
            _db.SaveChanges();

            var postCategory = new PostCategory {Name = "General", UrlSlug = "general", BlogId = blog.Id };
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
            string path = ServerTools.Paths.MediaFolderBlogsPath(name);
            Directory.CreateDirectory(path);
            return path;
        }


        private bool ValidateImage(Image img)
        {
            if (img.Width < ImgWidth || img.Height < ImgHeight)
            {
                _modelState.AddModelError("MiniatureUrl", "Image is too small.");
                return false;
            }
            return true;
        }

        private string ResizeAndSave(Image img, string tempFile, string blogFolder)
        {
            string fileName = "miniature." + tempFile.Split('.').Last();
            string imageDestinationPath = Path.Combine(blogFolder, fileName);

            if (img.Width != ImgWidth || img.Height != ImgHeight)
            {
                var resized = ImageHelper.ResizeAndCropToFixedSize(img, ImgWidth, ImgHeight, true); //Imager.Resize(img, ImgWidth, ImgHeight, false);
                resized.Save(imageDestinationPath);
            }
            else
                img.Save(imageDestinationPath);

            return imageDestinationPath;
        }

        private static object Lock = new object();
        public void IncrementVisitCounter(HttpContextBase httpContext, int blogId)
        {
            string key = "visits" + blogId;
            int value;
            bool limitReached = false;
            lock (Lock)
            {
                value = (httpContext.Application[key] as int? ?? 0 ) + 1;
                limitReached = value >= 2;
                httpContext.Application[key] = limitReached ? 0 : value;
            }
            if (limitReached)
            {
                 var blog = _db.Blogs.Find(blogId);
                blog.Visits += value;

                _db.Entry(blog).State = EntityState.Modified;
                _db.SaveChanges();  
            }
        }
    }
}
