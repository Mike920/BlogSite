using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels.Api
{
    public class PostList
    {
        public int BlogId { get; set; }
        public IEnumerable<PostOnList> Posts { get; set; }
    }
    public class PostOnList
    {
        public int Id { get; set; }
        public string UrlName { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
