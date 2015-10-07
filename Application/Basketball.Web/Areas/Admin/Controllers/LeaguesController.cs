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

namespace Basketball.Web.Areas.Admin.Controllers
{
    public class LeaguesController : BaseController
    {
        // Cheating a bit here. CompetitionService should do all this stuff
        private readonly ICompetitionService competitionService;
        private readonly ILeagueService leagueService;

        public LeaguesController(ILeagueService leagueService, ICompetitionService competitionService)
        {
            Check.Require(leagueService != null, "leagueService may not be null");
            Check.Require(competitionService != null, "competitionService may not be null");

            this.leagueService = leagueService;
            this.competitionService = competitionService;
        }

        #region Actions
        public ActionResult Index()
        {
            List<League> leagueList = leagueService.Get(orderBy: q => q.OrderByDescending(l => l.Id));
            return View(leagueList);
        }

        public ActionResult Create()
        {
            LeagueViewModel model = new LeagueViewModel();

            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(LeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO Refactor at some point
                model.League.Season = competitionService.GetSeasons(s => s.Id == model.SeasonId)[0];
                leagueService.Insert(model.League);
                leagueService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            
            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            LeagueViewModel model = new LeagueViewModel();

            model.League = leagueService.Get(id);

            PopulateStaticData(model);
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(LeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                League leagueToUpdate = leagueService.Get(model.League.Id);
                TryUpdateModel(leagueToUpdate, "League");
                leagueToUpdate.Season = competitionService.GetSeasons(s => s.Id == model.SeasonId)[0];

                leagueService.Update(leagueToUpdate);
                leagueService.Commit();

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
            leagueService.Delete(id);
            leagueService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 
        #endregion


        private void PopulateStaticData(LeagueViewModel model)
        {
            model.SeasonId = model.League.Season.Id;
            model.Seasons = competitionService.GetSeasons(orderBy: q => q.OrderByDescending(s => s.Id)).ToSelectList(x => x.Name, x => x.Id.ToString(), model.League.Season.Id.ToString());
        }
    }
}
