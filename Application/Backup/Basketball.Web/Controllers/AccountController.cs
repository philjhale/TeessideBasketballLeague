using System.Web.Mvc;
using System.Web.Security;
using Basketball.Common.Resources;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;

using Basketball.Web.ViewModels;

namespace Basketball.Web.Controllers
{
    public class AccountController : BaseController
    {
        readonly IMembershipService membershipService;

        public AccountController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        #region Actions
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (membershipService.CanLogOn(model.Username, model.Password))
                {
                    ClearCache();

                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }

                ErrorMessage(FormMessages.AccountLogOnFailed);
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            ClearCache();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                membershipService.ResetPassword(model.Username);
                membershipService.Commit();

                SuccessMessage(FormMessages.AccountResetPassword);
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        #endregion
    }
}
