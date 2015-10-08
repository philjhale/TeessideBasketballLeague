using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.BaseTypes;
using Basketball.Common.Resources;
using Basketball.Service.Exceptions;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;

namespace Basketball.Web.Areas.TeamAdmin.Controllers
{
    public class ChangePasswordController : BaseController
    {
        private readonly IMembershipService membershipService;

        public ChangePasswordController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.Validate();

                    membershipService.ChangePasswordForLoggedInUser(model.CurrentPassword, model.NewPassword);
                    membershipService.Commit();

                    SuccessMessage(FormMessages.AccountChangePassword);
                    return RedirectToAction("Index", "MatchResult");
                }
                catch (ChangePasswordNewPasswordsDoNotMatchException)
                {
                    ErrorMessage(FormMessages.AccountChangePasswordNewAndConfirmPasswordsDoNotMatch);
                }
                catch(ChangePasswordCurrentPasswordIncorrectException)
                {
                    ErrorMessage(FormMessages.AccountChangePasswordCurrentPasswordIncorrect);
                }
            }

            return View(model);
        }

    }
}
