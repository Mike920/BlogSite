using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.ViewModels
{
    public class DisplayPost
    {
        public Models.Blog Blog { get; set; }
        public Post Post { get; set; }
        public LayoutSettings LayoutSettings { get; set; }
    }
}
