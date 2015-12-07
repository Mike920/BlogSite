using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Models
{
    public class BlogDbContext : IdentityDbContext<User>
    {

        public BlogDbContext()
            : base("BlogDbContext")
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public DbSet<Blog> Blogs { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
        
        public System.Data.Entity.DbSet<BlogCategory> BlogCategories { get; set; }

        public System.Data.Entity.DbSet<Models.Post> Posts { get; set; }
    }
}
