using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.Admin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    // TODO Add validation. I.e. Only one winner for division
    [SiteAdmin]
    public class UsersController : BaseController
    {
        private readonly IUserService userService;
        private readonly ITeamService teamService;

        public UsersController(IUserService userService,
            ITeamService teamService)
        {
            Check.Require(userService != null, "userService may not be null");
            Check.Require(teamService != null, "teamService may not be null");

            this.userService = userService;
            this.teamService = teamService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(userService.Get(orderBy: q => q.OrderBy(u => u.Team.TeamNameLong)));
        }

        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.User.Team = teamService.Get(model.TeamId);

                // TODO Randomise and email password
                model.User.Password = model.User.Password.ToMd5();
                
                userService.Insert(model.User);
                userService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            
            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            User user = userService.Get(id);
            UserViewModel model = new UserViewModel();
            model.MapToModel(user);

            PopulateStaticData(model);
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.Get(model.User.Id);
                TryUpdateModel(user, "User");
                user.Team = teamService.Get(model.TeamId);

                userService.Update(user);
                userService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            userService.Delete(id);
            userService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 
        #endregion

        private void PopulateStaticData(UserViewModel model)
        {
            model.Teams = teamService.Get(orderBy: q => q.OrderBy(t => t.TeamNameLong)).ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString());
        }
    }
}
