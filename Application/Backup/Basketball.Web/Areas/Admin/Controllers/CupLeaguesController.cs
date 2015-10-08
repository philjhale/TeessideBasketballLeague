using System.Web.Mvc;
using System.Linq;
using Basketball.Common.Extensions;
using Basketball.Common.Resources;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Areas.Admin.ViewModels;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class CupLeaguesController : BaseController
    {
        private readonly ICupLeagueService cupLeagueService;
        private readonly ICupService cupService;
        private readonly ICompetitionService competitionService;

        public CupLeaguesController(ICupLeagueService cupLeagueService, 
            ICupService cupService,
            ICompetitionService competitionService)
        {
            Check.Require(cupLeagueService != null, "cupLeagueService may not be null");
            Check.Require(cupService != null, "cupService may not be null");
            Check.Require(competitionService != null, "competitionService may not be null");

            this.cupLeagueService   = cupLeagueService;
            this.cupService         = cupService;
            this.competitionService = competitionService;
        }

        #region Actions
        public ActionResult Index()
        {
            return View(cupLeagueService.Get());
        }

        public ActionResult Create()
        {
            var model = new CupLeagueViewModel();
            this.PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CupLeagueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cupLeague = new CupLeague(cupService.Get(model.CupId), competitionService.GetLeague(model.LeagueId));

                cupLeagueService.Insert(cupLeague);
                cupLeagueService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            this.PopulateStaticData(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            cupLeagueService.Delete(id);
            cupLeagueService.Commit();
            SuccessMessage(FormMessages.DeleteSuccess);
            return RedirectToAction("Index");
        }
        #endregion

        private void PopulateStaticData(CupLeagueViewModel model)
        {
            model.Cups = cupService.Get(orderBy: q => q.OrderBy(t => t.CupName)).ToSelectList(x => x.ToString(), x => x.Id.ToString(), model.CupId.ToString());
            model.Leagues = competitionService.GetLeaguesForCurrentSeason().ToSelectList(x => x.Season.ToString() + " " + x.ToString(), x => x.Id.ToString(), model.LeagueId.ToString());
        }
    }
}
