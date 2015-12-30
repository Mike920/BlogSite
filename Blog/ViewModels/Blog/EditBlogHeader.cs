using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Utility;

namespace Blog.ViewModels
{
    public class EditBlogHeader
    {
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }
        [Required]
        public string File { get; set; }
        public string FileName { get; set; }
    }
}
