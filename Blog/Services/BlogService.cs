using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AutoMapper;
using Blog.Utility;
using Blog.ViewModels;
using Blog.Models;
using Blog = Blog.Models.Blog;

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
            if (!_modelState.IsValid || !ServerTools.Paths.TempFolderContains(viewModel.ImageUrl)) return false;

            Models.Blog blog = Mapper.Map<Models.Blog>(viewModel);
            blog.UserId = userId;

            string blogFolderPath = CreateBlogFolder(blog.Name);
            string imageDestinationPath = MoveFromTempToBlogDir(viewModel.ImageUrl, blogFolderPath);

            //todo server side image resize
            blog.ImageUrl = ServerTools.RelativePath(imageDestinationPath);
                
            _db.Blogs.Add(blog);

            var postCategory = new PostCategory {Id = 1, Name = "General", BlogId = blog.Id};
            _db.PostCategories.Add(postCategory);
            _db.SaveChanges();
            return true;
        }

        private string CreateBlogFolder(string name)
        {
            string path = Path.Combine(ServerTools.Paths.MediaFolderPath(), name);
            Directory.CreateDirectory(path);
            return path;
        }

        private string MoveFromTempToBlogDir(string tempFile, string blogFolder)
        {
            string sourcePath = Path.Combine(ServerTools.Paths.TempFolder, tempFile);
            string imageDestinationPath = Path.Combine(blogFolder, tempFile);
            File.Move(sourcePath, imageDestinationPath);
            return imageDestinationPath;
        }

    }
}
