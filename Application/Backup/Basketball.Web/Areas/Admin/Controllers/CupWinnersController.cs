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
    public class CupWinnersController : BaseController
    {
        private readonly ICupWinnerService cupWinnerService;
        private readonly ICupService cupService;
        private readonly ITeamService teamService;
        private readonly ICompetitionService competitionService;

        public CupWinnersController(ICupWinnerService cupWinnerService,
            ICupService cupService, 
            ITeamService teamService,
            ICompetitionService competitionService)
        {
            Check.Require(cupWinnerService        != null, "leagueWinnerService may not be null");
            Check.Require(cupService              != null, "leagueService may not be null");
            Check.Require(teamService             != null, "teamService may not be null");
            Check.Require(competitionService      != null, "competitionService may not be null");

            this.cupWinnerService   = cupWinnerService;
            this.cupService         = cupService;
            this.teamService        = teamService;
            this.competitionService = competitionService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(this.cupWinnerService.Get(orderBy: q => q.OrderByDescending(lw => lw.Id)));
        }

        public ActionResult Create()
        {
            CupWinnerViewModel model = new CupWinnerViewModel();
            
            PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CupWinnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                CupWinner leagueWinner = new CupWinner(competitionService.GetCurrentSeason(), cupService.Get(model.CupId), teamService.Get(model.TeamId));
                
                this.cupWinnerService.Insert(leagueWinner);
                this.cupWinnerService.Commit();

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
            this.cupWinnerService.Delete(id);
            this.cupWinnerService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        } 
        #endregion

        private void PopulateStaticData(CupWinnerViewModel model)
        {
            model.Teams = teamService.GetTeamsForCurrentSeason().ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.TeamId.ToString());
            model.Cups = this.cupService.Get(orderBy: q => q.OrderByDescending(s => s.Id)).ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.CupId.ToString());
        }
    }
}
