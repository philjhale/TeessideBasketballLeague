using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;

namespace Basketball.Web.Controllers
{
    public class ViewCupController : BaseController
    {
        private readonly ICupService cupService;
        private readonly IStatsReportingService statsReportingService;
        private readonly ICompetitionService competitionService;

        public ViewCupController(ICupService cupService, IStatsReportingService statsReportingService, ICompetitionService competitionService)
        {
            this.cupService            = cupService;
            this.statsReportingService = statsReportingService;
            this.competitionService    = competitionService;
        }

        [OutputCache(CacheProfile="Public")]
        public ActionResult Index(int? cupId)
        {
            // TODO Include seasonId as well?
            Cup cup = cupId.HasValue ? cupService.Get(cupId.Value) : null;

            if(cup == null)
                return RedirectToAction("Index", "Home");

            // TODO Unit test
            // TODO Fix this utterly utterly shite method
            List<List<Fixture>> fixturesForDisplay = cupService.GetCupFixturesForDisplay(cupId.Value);

            var model = new ViewCupViewModel(cup.CupName, fixturesForDisplay, statsReportingService.GetTopAvgScorersForCup(cup.Id, competitionService.GetCurrentSeason().Id, 5));
            
            return View(model);
        }
    }
}