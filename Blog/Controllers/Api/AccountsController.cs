using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Blog.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class AccountsController : ApiController
    {
        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/accounts/Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel model)
        {
            if (model == null)
                return BadRequest("Error");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Ok();
/*                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:*/
                default:
                    /*ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);*/
                    return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/accounts/Logout")]
        public IHttpActionResult Logout()
        {
            var db = new BlogDbContext();

            var username = db.Users.Find(User.Identity.GetUserId()).UserName;
            AuthenticationManager.SignOut();
            return Ok(username);
        }

    }
}
