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
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }

        public string UrlSlug { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<Post> Posts
        { get; set; }
   
        
    }
}
