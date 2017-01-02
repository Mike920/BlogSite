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


        private string currentUserId = null;
        public string CurrentUserId
        {
            get
            {
                return currentUserId ?? (currentUserId = User.Identity.GetUserId());
            }
        }

        private int? currentBlogId = null;
        public int? CurrentBlogId
        {
            get
            {
                return currentBlogId ?? (
                            CurrentUserId == null ? null :
                            currentBlogId = db.Users.Where(u => u.Id == CurrentUserId)
                            .Select(u => u.CurrentBlogId)
                            .SingleOrDefault()
                    );
            }
        }
    }
}
