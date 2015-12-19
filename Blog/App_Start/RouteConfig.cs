using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "CreateBlog",
               url: "Blog/Create",
               defaults: new { controller = "Blog", action = "Create" }
           );

            routes.MapRoute(
               name: "Blog",
               url: "Blog/{blogName}",
               defaults: new { controller = "Blog", action = "Display" }
           );

            routes.MapRoute(
               name: "Post",
               url: "Blog/{blogName}/{postId}/{postName}",
               defaults: new { controller = "Posts", action = "Details", postName="" }
           );

            routes.MapRoute(
               name: "AdminPanel",
               url: "AdminPanel/{*.}",
               defaults: new { controller = "AdminPanel", action = "Index" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}