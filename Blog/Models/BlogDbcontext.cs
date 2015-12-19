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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                .HasRequired(p => p.Category)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }

         public DbSet<Blog> Blogs { get; set; }
        public System.Data.Entity.DbSet<BlogCategory> BlogCategories { get; set; }

        public System.Data.Entity.DbSet<Post> Posts { get; set; }
        public System.Data.Entity.DbSet<PostCategory> PostCategories { get; set; }
        public System.Data.Entity.DbSet<LayoutSettings> LayoutSettings { get; set; }
    }
}
