using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Utility;

namespace Blog.ViewModels
{
    public class EditBlogViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RemoteValidation("IsBlogNameUniqueOrUnchanged", "Validation", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        [Required]
        [RemoteValidation("IsBlogUrlUniqueOrUnchanged", "Validation", AdditionalFields = "Id")]
        public string UrlName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
