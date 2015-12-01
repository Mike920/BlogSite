using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Tag
    {
        public Tag()
        {
            Posts = new List<Post>();
        }

        [Key]
        public  string Name
        { get; set; }

        public  string Description
        { get; set; }
        public string BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<Post> Posts
        { get; set; }

        public string UrlPart
        {
            get
            {
                //todo convert title to some url friendly text
                throw new NotImplementedException();
            }
        }
    }
}
