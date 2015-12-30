using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Utility;

namespace Blog.ViewModels
{
    public class CreateBlogViewModel
    {

        [Required][RemoteValidation("IsBlogNameUnique","Validation")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required]
        [RemoteValidation("IsBlogUrlUnique", "Validation")]
        public string UrlName { get; set; }
        
        [Required][DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string MiniatureUrl { get; set; }
        
    }
}
