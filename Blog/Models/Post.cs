using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Models
{
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }

        [Required]
        public  string Title { get; set; }

        //Generate dynamically
        [Required][ScaffoldColumn(false)]
        public string UrlName { get; set; }

        [DataType(DataType.MultilineText)][Required][AllowHtml]
        [StringLength(4000)]
        public  string Content { get; set; }

        [Display(Name = "Publish")]
        public  bool Published { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy, HH:mm}")]
        public  DateTime? PublishDate { get; set; }

        [ScaffoldColumn(false)]
        public  DateTime? Modified { get; set; }
        [ScaffoldColumn(false)]
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual PostCategory Category
        { get; set; }

        public virtual ICollection<Tag> Tags
        { get; set; }


    }
}
