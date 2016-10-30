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
    public class BlogViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UrlName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string MiniatureUrl { get; set; }
    }
}
