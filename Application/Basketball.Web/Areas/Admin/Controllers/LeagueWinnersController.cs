using System.Linq;
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
    public class LeagueWinnersController : BaseController
    {
        private readonly ILeagueWinnerService leagueWinnerService;
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;

        public LeagueWinnersController(ILeagueWinnerService leagueWinnerService,
            ILeagueService leagueService, 
            ITeamService teamService)
        {
            Check.Require(leagueWinnerService != null, "leagueWinnerService may not be null");
            Check.Require(leagueService != null, "leagueService may not be null");
            Check.Require(teamService != null, "teamService may not be null");

            this.leagueWinnerService = leagueWinnerService;
            this.leagueService = leagueService;
            this.teamService = teamService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(leagueWinnerService.Get(orderBy: q => q.OrderByDescending(lw => lw.Id)));
        }

        public ActionResult Create()
        {
            LeagueWinnerViewModel model = new LeagueWinnerViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(LeagueWinnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                LeagueWinner leagueWinner = new LeagueWinner(leagueService.Get(model.LeagueId), teamService.Get(model.TeamId));
                
                leagueWinnerService.Insert(leagueWinner);
                leagueWinnerService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            
            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            LeagueWinner leagueWinner = leagueWinnerService.Get(id);
            LeagueWinnerViewModel model = new LeagueWinnerViewModel();
            model.MapToModel(leagueWinner);

            PopulateStaticData(model);
            
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(LeagueWinnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                LeagueWinner leagueWinnerToUpdate = leagueWinnerService.Get(model.LeagueWinnerId);

                leagueWinnerToUpdate.League = leagueService.Get(model.LeagueId);
                leagueWinnerToUpdate.Team = teamService.Get(model.TeamId);

                leagueWinnerService.Update(leagueWinnerToUpdate);
                leagueWinnerService.Commit();

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
            leagueWinnerService.Delete(id);
            leagueWinnerService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 
        #endregion


        private void PopulateStaticData(LeagueWinnerViewModel model)
        {
            model.Teams = teamService.GetTeamsForCurrentSeason().ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString());
            model.Leagues = leagueService.Get(orderBy: q => q.OrderByDescending(s => s.Id)).ToSelectList(x => x.Season.ToString() + " " + x.ToString(), x => x.Id.ToString(), model.LeagueId.ToString());
        }
    }
}
