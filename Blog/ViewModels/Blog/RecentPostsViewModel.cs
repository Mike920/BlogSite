using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.ViewModels.Blog
{
    public class RecentPostsViewModel
    {
        public string BlogUrlName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
