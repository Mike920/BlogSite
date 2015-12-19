using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.ViewModels
{
    public class DisplayBlog
    {
        public Models.Blog Blog { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public LayoutSettings LayoutSettings { get; set; }
    }
}
