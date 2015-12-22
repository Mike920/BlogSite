using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ViewModels
{
    public class DisplayUser
    {
        public  string Email { get; set; }
        public string Id { get; set; }
        public  string UserName { get; set; }
        public int? CurrentBlogId { get; set; }

       /* public ICollection<Blog> Blogs { get; set; }*/
    }
}
