using System.Linq;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class CommonController : Controller
    {
        BlogDbContext db = new BlogDbContext();
         [ChildActionOnly]
        public ActionResult MainMenu()
         {
             var categories = db.BlogCategories.ToList();

            return PartialView("_MainMenu", categories);
        }

    }
}
