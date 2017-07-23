using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Net;
using CrimeReportSystem.Models.Account;
using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Provider.Security;

namespace CrimeReportSystem.Controllers
{
    [Authorize]
    public class AccountController : TCRControllerBase
    {
        private DataContext Context { get; set; }
        private bool _DisposeContext = false;

        private ISecurityProvider SecurityProvider { get; set; }

        public AccountController()
        {
            Context = new DataContext();
            _DisposeContext = true;
            SecurityProvider = new SecurityProvider(Context,CurrentUser);
        }

        public AccountController(ISecurityProvider securityProvider)
        {
            SecurityProvider = securityProvider;
        }

        protected override void Dispose(bool disposing)
        {
            if (_DisposeContext)
                Context.Dispose();

            base.Dispose(disposing);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error details");

                var user = SecurityProvider.UserLogin(model.UserName, model.Password);
                CurrentUser = new Models.Account.CurrentUserModel(user);

                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                return SerializeToAngular(CurrentUser);

            }
            catch (SecurityException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = SecurityProvider.SignUp(model.UserName, model.Password, model.Email,
                        model.Title, model.FirstName, model.Surname,
                        model.IDOrPassport, model.GenderType, model.Telephone,
                        model.AddressLine1, model.AddressLine2, model.City,
                        model.PostalCode);

                    var aCurrentUser = SecurityProvider.UserIdentityToCurrentUser(user);
                    CurrentUser = new Models.Account.CurrentUserModel(aCurrentUser);

                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    return SerializeToAngular(CurrentUser);

                }
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError,"Validation Error");
            }
            catch (SecurityException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
