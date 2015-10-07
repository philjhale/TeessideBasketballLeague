using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.Admin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class PenaltiesController : BaseController
    {
        readonly IPenaltyService penaltyService;
        readonly ILeagueService leagueService;
        readonly ITeamService teamService;
        readonly IMatchResultService matchResultService;

        public PenaltiesController(IPenaltyService penaltyService,
            ILeagueService leagueService,
            ITeamService teamService,
            IMatchResultService matchResultService)
        {
            this.penaltyService = penaltyService;
            this.leagueService = leagueService;
            this.teamService = teamService;
            this.matchResultService = matchResultService;
        }

        #region Action
        public ActionResult Index()
        {
            return View(penaltyService.Get(orderBy: q => q.OrderByDescending(p => p.Id)));
        }

        public ActionResult Create()
        {
            PenaltyViewModel model = new PenaltyViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PenaltyViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Penalty.League = leagueService.Get(model.LeagueId);
                model.Penalty.Team = teamService.Get(model.TeamId);

                penaltyService.Save(model.Penalty);
                penaltyService.Commit(); // Seems to have to be committed before updating points/stats

                UpdateTeamLeaguePenaltiesAndStats(model.LeagueId, model.TeamId);

                penaltyService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);

                return RedirectToAction("Index");
            }

            PopulateStaticData(model);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            PenaltyViewModel model = new PenaltyViewModel();
            model.MapToModel(penaltyService.Get(id));

            PopulateStaticData(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PenaltyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Penalty penaltyToUpdate = penaltyService.Get(model.Penalty.Id);
                TryUpdateModel(penaltyToUpdate, "Penalty");

                penaltyToUpdate.League = leagueService.Get(model.LeagueId);
                penaltyToUpdate.Team = teamService.Get(model.TeamId);

                penaltyService.Save(penaltyToUpdate);

                UpdateTeamLeaguePenaltiesAndStats(model.LeagueId, model.TeamId);

                penaltyService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);

                return RedirectToAction("Index");
            }
 
            PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Penalty penaltyToDelete = penaltyService.Get(id);
            int leagueId            = penaltyToDelete.League.Id;
            int teamId              = penaltyToDelete.Team.Id;

            using (TransactionScope scope = new TransactionScope())
            {
                penaltyService.Delete(penaltyToDelete);
                penaltyService.Commit();

                UpdateTeamLeaguePenaltiesAndStats(leagueId, teamId);
                penaltyService.Commit();

                scope.Complete();
            }
            

            SuccessMessage(FormMessages.DeleteSuccess);

            return RedirectToAction("Index");
        }

        #endregion


        private void PopulateStaticData(PenaltyViewModel model)
        {
            model.Teams = teamService.GetTeamsForCurrentSeason().ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString());
            model.Leagues = leagueService.Get(orderBy: q => q.OrderByDescending(s => s.Id)).ToSelectList(x => x.Season.ToString() + " " + x.ToString(), x => x.Id.ToString(), model.LeagueId.ToString());
        }

        private void UpdateTeamLeaguePenaltiesAndStats(int leagueId, int teamId)
        {
            TeamLeague teamLeague = penaltyService.UpdateTeamLeaguePenaltyPoints(leagueId, teamId);
            this.matchResultService.UpdateTeamLeagueStats(teamLeague.Id);
        }

    }
}
