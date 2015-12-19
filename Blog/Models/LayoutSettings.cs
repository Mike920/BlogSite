using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class LayoutSettings
    {
        public LayoutSettings()
        {
            WidgetsColumnSide = "right";
            Widgets = "";
        }
         [Key, ForeignKey("Blog")]
        public int Id { get; set; }

         public string Widgets { get; set; }
        public string WidgetsColumnSide { get; set; }

        [NotMapped]
        public List<string> WidgetList
        {
            get { return  Widgets.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();}
            set
            {
                Widgets = "";
                value.Select(w => Widgets += w + ";");
            }
        }


        //public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Widget
    {
        public const string Author = "Author";
        public const string Categories = "Categories";
        public const string RecentPosts = "RecentPosts";
    }
}
