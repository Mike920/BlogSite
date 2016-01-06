using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO;
using System.Web;
using Blog.Models;
using Blog.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.BlogDbContext>
    {

        public Configuration()
        {
        }


        protected override void Seed(Blog.Models.BlogDbContext context)
        {

            if(!InitializeIdentityForEF(context))
                return;

            var categories = new[]
            {
                new BlogCategory { Name = "Lifestyle" },
                new BlogCategory { Name = "Entertainment" },
                new BlogCategory { Name = "Food" }
            };
            context.BlogCategories.AddOrUpdate(categories);
            context.SaveChanges();

            var users = PopulateUsers(context);
            var blogs = PopulateBlogs(context,users,categories);

            var postCategories = new List<PostCategory>();
            foreach (var blog in blogs)
            {
                postCategories.AddRange(new PostCategory[]
                {
                    new PostCategory { Name = "General", BlogId = blog.Id},
                    new PostCategory { Name = "Entertainment", BlogId = blog.Id },
                    new PostCategory { Name = "Food", BlogId = blog.Id }
                });
            }
            context.PostCategories.AddOrUpdate(postCategories.ToArray());
            context.SaveChanges();

            var posts = PopulatePosts(context,blogs,postCategories);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private Post[] PopulatePosts(BlogDbContext db, Models.Blog[] blogs, List<PostCategory> postCategories)
        {
            var posts = new List<Post>();
            int counter = 0;

            foreach (var blog in blogs)
                for (int i = 0; i < 20; i++)
                {
                    var title = GetRandomSentence();
                    posts.Add(new Post
                    {
                        BlogId = blog.Id,
                        Title = title,
                        CategoryId = postCategories[counter % postCategories.Count].Id,
                        Content = GetRandomSentences(Rand.Next(10,30)),
                        PublishDate = DateTime.Now.AddDays(Rand.Next(0,3000) * -1),
                        UrlName = ServerTools.GenerateUrlFriendlyString(title),
                        Published = true
                    });
                    counter++;
                }
            db.Posts.AddOrUpdate(posts.ToArray());
            db.SaveChanges();
            return posts.ToArray();
        }

        public static bool InitializeIdentityForEF(BlogDbContext db)
        {
            /*var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();*/
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<User>(db);
            var userManager = new UserManager<User>(userStore);

            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }
            else
            {
                return false;
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }
            else
            {
                return false;
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
            return true;
        }

        public static User[] PopulateUsers(BlogDbContext db)
        {
            var userStore = new UserStore<User>(db);
            var userManager = new UserManager<User>(userStore);

            var users = new User[]
            {
                new User { UserName = "AdamSmith", Email = "smith@mail.com" },
                new User { UserName = "JohnJagger", Email = "jj@mail.com" },
                new User { UserName = "KevinJonson", Email = "kevinjonson@mail.com" },
                new User { UserName = "MichaelKerry", Email = "smith@mail.com" },
                new User { UserName = "AmandaF", Email = "amanda@mail.com" }
            };
            string password = "!Q2w3e4r5t";

            for (int i = 0; i < users.Length; i++)
            {
                var user = userManager.FindByName(users[i].UserName);
                if (user == null)
                    userManager.Create(users[i], password);
                else
                    users[i] = user;
            }
            db.SaveChanges();
            return users;
        }
        public static Models.Blog[] PopulateBlogs(BlogDbContext db, User[] users, BlogCategory[] categories)
        {
            var blogs = new List<Models.Blog>();
            int counter = 0;
            foreach (var user in users)
                for (int i = 0; i < 6; i++)
                {
                    blogs.Add(new Models.Blog
                    {
                        Name = GetRandomSentence(),
                        UrlName = UniqueWords[counter],
                        Description = GetRandomSentences(Rand.Next(5, 15)),
                        MiniatureUrl = GetNextMiniature(),
                        UserId = user.Id,
                        CategoryId = categories[counter%categories.Length].Id,
                        Visits = Rand.Next(0, 5000),
                        HeaderUrl = GetNextHeader()
                    });
                    counter++;
                }

            db.Blogs.AddOrUpdate(blogs.ToArray());
            db.SaveChanges();
            foreach (var blog in blogs)
            {
                db.LayoutSettings.AddOrUpdate(new LayoutSettings{Id = blog.Id});
            }
            db.SaveChanges();
            return blogs.ToArray();
        }

        private static int miniatureFileNumber = 0;
        private static string GetNextMiniature()
        {
            string[] filePaths = Directory.GetFiles(ServerTools.Paths.MediaFolderPath("Seed","Miniatures"));

            if (miniatureFileNumber >= filePaths.Length)
                miniatureFileNumber = 0;
            return ServerTools.RelativePath(filePaths[miniatureFileNumber++]);

        }

        private static int headerFileNumber = 0;
        private static string GetNextHeader()
        {
            string[] filePaths = Directory.GetFiles(ServerTools.Paths.MediaFolderPath("Seed", "Headers"));

            if (headerFileNumber >= filePaths.Length)
                headerFileNumber = 0;
            return ServerTools.RelativePath(filePaths[headerFileNumber++]);

        }

        private static string GetRandomSentences(int count)
        {
            string sentences = "";
            for (int i = 0; i < count; i++)
            {
                sentences += (Sentences[Rand.Next(0, Sentences.Count - 1)] + ". ");
            }
            return sentences;
        }
        private static string GetRandomSentence()
        {
            return Sentences[Rand.Next(0, Sentences.Count - 1)];
        }

        private static Random rand = new Random();

        private static Random Rand
        {
            get { return rand; }
            set { rand = value; }
        }

        private static List<string> UniqueWords
        {
            get { return loremIpsum.Split(' ', '.').Where(w => w.Length > 2).Select(w => w.Trim()).Distinct().ToList(); }
        }

        private static List<string> Sentences
        {
            get { return loremIpsum.Split('.').Select( s => s.Trim()).ToList(); }
        }

        private const string loremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer hendrerit finibus condimentum. Suspendisse nec malesuada erat, eget consequat libero. Curabitur interdum varius justo nec porttitor. Vestibulum eros velit, semper nec dictum eget, aliquam non nisl. Vivamus sollicitudin arcu sit amet purus ultricies, ut faucibus elit molestie. Quisque imperdiet diam vitae scelerisque tristique. Mauris nec est auctor, aliquam ipsum et, auctor odio. Integer nisi sapien, volutpat et rhoncus id, rutrum vel elit. Suspendisse quis vulputate nisi. Donec pulvinar nisl libero, sit amet suscipit arcu facilisis ac. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam in accumsan neque.

Sed facilisis diam sem, vitae fringilla justo congue vel. Ut rutrum ante et turpis tristique consectetur. Donec in ante turpis. Nullam eu porttitor augue, nec ultricies ipsum. Phasellus semper suscipit arcu vel auctor. Phasellus sed urna ac ex ornare commodo. Sed ut velit non nisl facilisis aliquet eu vel eros. Nunc lacinia mauris vitae urna suscipit mattis a faucibus velit. Proin sit amet varius ligula. Aliquam efficitur elit ante, a congue sapien vehicula quis. Nullam volutpat luctus ex sed faucibus. Nulla condimentum aliquam sollicitudin. Suspendisse semper, libero at mattis aliquet, lectus purus posuere leo, eget sodales ex felis non augue.

Sed in enim in orci pharetra congue. Maecenas aliquet vel nibh faucibus tristique. Cras condimentum, mauris id laoreet laoreet, arcu diam sodales leo, vel feugiat mi lectus quis massa. Nam at ipsum id purus gravida finibus. Ut eget lacinia urna. Mauris tincidunt ullamcorper accumsan. Etiam faucibus suscipit justo, vel viverra sapien aliquet at. Aliquam nisl risus, commodo sit amet hendrerit eget, scelerisque eget ipsum. Aliquam blandit elit iaculis ante finibus, id laoreet nibh sagittis. Sed gravida augue et ipsum commodo, quis porttitor turpis interdum. Cras eros orci, luctus at magna quis, ultricies imperdiet ex.

Nulla pharetra, diam non tempor tempus, elit metus bibendum ante, quis hendrerit enim urna id turpis. Suspendisse congue sapien congue posuere congue. Integer a magna eget elit suscipit tincidunt. Sed in bibendum urna. Quisque vitae scelerisque magna, sed blandit nibh. Cras venenatis, sem sit amet fringilla sollicitudin, risus mi faucibus dui, id pulvinar metus dolor id leo. Donec ac velit a ipsum volutpat tincidunt in eget dolor.

Aliquam tincidunt egestas nisl at vulputate. Ut ultrices, tellus maximus auctor dignissim, augue felis pulvinar turpis, in imperdiet mauris mi quis nisi. Nam odio turpis, malesuada sed sapien non, egestas vehicula orci. In pellentesque elit quis pretium pretium. Donec vitae nisl eget odio vehicula efficitur vel sit amet ipsum. Maecenas felis dui, bibendum sit amet elementum nec, porta eu dolor. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Quisque lacinia eros molestie justo commodo, id imperdiet enim rutrum. Integer non quam elit. Donec vestibulum urna quis ante pretium ullamcorper eget vitae mi. Nulla dapibus ipsum ac blandit ultrices. Fusce id quam lacinia, volutpat ante pellentesque, sagittis velit. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec vulputate elit eu nunc condimentum finibus. Vivamus condimentum fringilla felis, vel interdum elit condimentum mattis. Curabitur varius nisl leo, a mattis purus lacinia nec.";

    }
}
