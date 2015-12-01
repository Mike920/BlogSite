using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class PostCategory
    {
        public PostCategory()
        {
            Blogs = new List<Blog>();
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }

       // public  string Description { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Post> Posts
        { get; set; }

        public string UrlPart
        {
            get
            {
                //todo convert title to some url friendly text
                throw new NotImplementedException();
            }
        }
    }
}
