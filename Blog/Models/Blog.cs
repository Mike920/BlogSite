using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Blog
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Address")][Required]
        public string UrlName { get; set; }
        
        [Required][DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual BlogCategory Category { get; set; }

        //[ScaffoldColumn(false)]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public Blog()
        {
            Posts = new List<Post>();
            PostCategories = new List<PostCategory>();
            Tags = new List<Tag>();
        }
    }
}
