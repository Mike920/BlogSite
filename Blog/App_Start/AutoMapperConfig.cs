using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Api;

namespace Blog
{
    class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<CreateBlogViewModel, Models.Blog>();
            AutoMapper.Mapper.CreateMap<Models.Blog, EditBlogViewModel>();
            AutoMapper.Mapper.CreateMap<EditBlogViewModel, Models.Blog>();
            AutoMapper.Mapper.CreateMap<PostCategory, EditPostCategory>();
            AutoMapper.Mapper.CreateMap<CreatePost, Post>();
            AutoMapper.Mapper.CreateMap<Post, CreatePost>();
            AutoMapper.Mapper.CreateMap<User, DisplayUser>();
            AutoMapper.Mapper.CreateMap<Post, PostOnList>();

        }
    }
}
