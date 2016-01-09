using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels.Api
{
    public class DashboardViewModel
    {
        public BlogStatistics CurrentBlog { get; set; }
        public IEnumerable<BlogDashboard> Blogs { get; set; }
    }

    public class BlogStatistics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Visits { get; set; }
        public int TotalPosts { get; set; }

    }

    public class BlogDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
