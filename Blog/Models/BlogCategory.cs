using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Utility;

namespace Blog.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string UrlSlug { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
