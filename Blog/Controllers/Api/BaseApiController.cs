using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Blog.Models;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers.Api
{
   public class BaseApiController : ApiController
    {
        BlogDbContext db = new BlogDbContext();

        public BlogDbContext Db
        {
            get { return db; }
            set { db = value; }
        }
        

        public string CurrentUserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public int? CurrentBlogId
        {
            get
            {
                if (CurrentUserId == null)
                    return null;
                return db.Users.Where(u => u.Id == CurrentUserId)
                        .Select(u => u.CurrentBlogId)
                        .SingleOrDefault();
            }
        }
    }
}
