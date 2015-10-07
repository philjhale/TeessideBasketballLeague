using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Areas.TeamAdmin.ViewModels;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class TeamsController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly IDayOfWeekService dayOfWeekService;

        public TeamsController(ITeamService teamService, IDayOfWeekService dayOfWeekService)
        {
            Check.Require(teamService != null, "teamService may not be null");
            Check.Require(dayOfWeekService != null, "dayOfWeekService may not be null");

            this.teamService = teamService;
            this.dayOfWeekService = dayOfWeekService;
        }

        public ActionResult Index()
        {
            List<Team> teamList = teamService.Get(orderBy: q => q.OrderBy(t => t.TeamNameLong));
            return View(teamList);
        }

        public ActionResult Create()
        {
            MyTeamViewModel model = new MyTeamViewModel();

            PopulateStaticDate(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(MyTeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DayOfWeekId.HasValue)
                    model.Team.GameDay = dayOfWeekService.Get(model.DayOfWeekId.Value);

                // TODO FIX GameeDay is NOT being saved
                teamService.Save(model.Team);
                teamService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            PopulateStaticDate(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            MyTeamViewModel model = new MyTeamViewModel();

            model.Team = teamService.Get(id);
    
            PopulateStaticDate(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(MyTeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Team MUST be loaded first, then ALL the values copied over
                Team team = teamService.Get(model.Team.Id);
				TryUpdateModel(team, "Team"); // Should copy all values from model to team
                team.GameDay = dayOfWeekService.Get(model.DayOfWeekId.Value);
                teamService.Update(team);

                teamService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            PopulateStaticDate(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            teamService.Delete(id);
            teamService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        }

        private void PopulateStaticDate(MyTeamViewModel model)
        {
            model.DaysOfWeek = new SelectList(dayOfWeekService.Get(), "Id", "Day", model.Team.GameDay.Id);
        }
    }
}
