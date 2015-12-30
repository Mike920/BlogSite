using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.ViewModels
{
    public class BlogInformationViewModel
    {

        [Required]
        public string Name { get; set; }
        
        [Required][DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int BlogCategoryId { get; set; }
        public virtual BlogCategory Category { get; set; }

        [Required]
        public string MiniatureUrl { get; set; }
        
    }
}
