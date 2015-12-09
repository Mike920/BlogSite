using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.ViewModels;

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
        }
    }
}
