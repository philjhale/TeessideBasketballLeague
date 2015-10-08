using System.Web.Mvc;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using Basketball.Service.Interfaces;

namespace Basketball.Web.Controllers
{
    public partial class FixturesController : BaseController
    {
        private readonly ICompetitionService competitionService;
        private readonly IFixtureService fixtureService;
        private readonly ITeamService teamService;
        private readonly ICupService cupService;

        public FixturesController(ICompetitionService competitionService,
            IFixtureService fixtureService,
            ITeamService teamService,
            ICupService cupService)
        {
            this.competitionService = competitionService;
            this.fixtureService     = fixtureService;
            this.teamService        = teamService;
            this.cupService         = cupService;
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Index()
        {
            FixturesViewModel model = new FixturesViewModel(
                competitionService.GetCurrentSeason().ToString(),
                teamService.GetTeamsForCurrentSeason(),
                competitionService.GetLeaguesForCurrentSeason(),
                cupService.GetCupsForCurrentSeason(),
                fixtureService.GetFixturesForCurrentSeasonFilter(-1, "N", null)
            );

            return View(model);
        }

        [HttpPost]
		[OutputCache(CacheProfile="Public")]
        public ActionResult Index(FixturesViewModel model)
        {
            if (model == null)
                return RedirectToAction("Index");

            // TODO Pass teams and season in request?
            model.PopulateData(
                competitionService.GetCurrentSeason().ToString(),
                teamService.GetTeamsForCurrentSeason(),
                competitionService.GetLeaguesForCurrentSeason(),
                cupService.GetCupsForCurrentSeason(),
                fixtureService.GetFixturesForCurrentSeasonFilter(model.FilterByTeamId, model.FilterByIsPlayed, model.IsFilteredByLeague(), model.IsFilteredByCup(), model.GetLeagueOrCupId())
            );

            return View(model);
        }
    }
}
