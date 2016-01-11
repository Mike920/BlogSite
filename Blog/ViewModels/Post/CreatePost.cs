using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.ViewModels
{
    public class CreatePost
    {
        public CreatePost()
        {
            Tags = new List<Tag>();
        }

        public int Id { get; set; }

        [Required]
        public  string Title { get; set; }


        [Required]
        [AllowHtml]
        [MaxLength]
        public  string Content { get; set; }

        [Display(Name = "Publish")]
        public  bool Published { get; set; }

        [ScaffoldColumn(false)]
        public string BlogId { get; set; }
        [Required]
        public string CategoryId { get; set; }

        public virtual ICollection<Tag> Tags
        { get; set; }


    }
}
