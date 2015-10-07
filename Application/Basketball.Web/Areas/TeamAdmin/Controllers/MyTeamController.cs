using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Areas.TeamAdmin.ViewModels;
using Basketball.Common.BaseTypes;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Exceptions;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using DayOfWeek = Basketball.Domain.Entities.DayOfWeek;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.TeamAdmin.Controllers
{
    [TeamAdmin]
    public class MyTeamController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly IMembershipService membershipService;
        private readonly IDayOfWeekService dayOfWeekService;

        public MyTeamController(ITeamService teamService,
            IMembershipService membershipService,
            IDayOfWeekService dayOfWeekService)
        {
            this.teamService = teamService;
            this.membershipService = membershipService;
            this.dayOfWeekService = dayOfWeekService;
        }

        public ActionResult Index()
        {
            User user = membershipService.GetLoggedInUser();
            MyTeamViewModel model = new MyTeamViewModel();

            model.Team = teamService.Get(user.Team.Id);
            model.DaysOfWeek = new SelectList(dayOfWeekService.Get(), "Id", "Day", model.Team.GameDay.Id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MyTeamViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Team MUST be loaded first, then ALL the values copied over
                Team team = teamService.Get(model.Team.Id);
                TryUpdateModel(team, "Team"); // Should copy all values from model to team
                team.GameDay = dayOfWeekService.Get(model.DayOfWeekId.Value);
                teamService.Update(team);

                teamService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
            }

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Console.Out.WriteLine(error.ErrorMessage);
                }
            }


            model.DaysOfWeek = new SelectList(dayOfWeekService.Get(), "Id", "Day", model.DayOfWeekId);

            return View(model);
        }

    }
}
