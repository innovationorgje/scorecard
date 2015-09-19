using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using scorecard.Data;
using scorecard.Models;

namespace scorecard.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return RedirectToAction("ExternalLogin", "Account", new { provider="LinkedIn", returnUrl=returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ExternalLoginFailureViewModel model)
        {
            MessageHandler.SendEmailAsync("matt@code.je", "Application for access to innovation.org.je", string.Format("{0} has requested access.", model.EmailAddress));

            return View("ApplicationSent");
        }
                
        //
        // GET: /Account/ExternalLogin
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

                    // Get the information about the user from the external login provider
                    var identity = loginInfo.ExternalIdentity;
                    string email = loginInfo.Email;
                    string name = identity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:name").Value;
                    string accessToken = identity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:accesstoken").Value;
                    string url = identity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:url").Value;

                    // check they are authorised
                    RAGContext rag = new RAGContext();
                    var permittedUsers = rag.PermittedUsers.Where(p => p.Email == email);

                    if (permittedUsers != null && permittedUsers.Count() == 1)
                    {
                        var permitted = permittedUsers.First();

                        var user = new ApplicationUser { UserName = email, Email = email, FullName = name, LinkedInProfile = url, LinkedInToken = accessToken, PermittedUser=permitted };

                        var createResult = await UserManager.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            // set roles                            
                            if(!string.IsNullOrEmpty(permitted.Roles))
                            {
                                string[] roles = permitted.Roles.Split(new char[] { ',' });

                                foreach (string role in roles)
                                {
                                    await this.UserManager.AddToRoleAsync(user.Id, role);
                                }
                            }

                            createResult = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                            if (createResult.Succeeded)
                            {
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                    }

                    ExternalLoginFailureViewModel model = new ExternalLoginFailureViewModel(email);
                    return View("ExternalLoginFailure", model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}