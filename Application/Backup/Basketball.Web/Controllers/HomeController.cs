using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.Controllers
{
    public class HomeController : BaseController
    {
        readonly private INewsService newsService;
        readonly private ICompetitionService competitionService;
        readonly private IStatsReportingService statsReportingService;
        readonly private IFixtureService fixtureService;
        readonly private IEventService eventService;
        readonly private ICupService cupService;

        public HomeController(INewsService newsService,
            ICompetitionService competitionService,
            IStatsReportingService statsReportingService,
            IFixtureService fixtureService,
            IEventService eventService,
            ICupService cupService)
        {
            this.newsService           = newsService;
            this.competitionService    = competitionService;
            this.statsReportingService = statsReportingService;
            this.fixtureService        = fixtureService;
            this.eventService          = eventService;
            this.cupService            = cupService;
        }

        public ActionResult MobileIndex()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        #region Actions
		[OutputCache(CacheProfile="Public")]
        public ActionResult Index()
        {
            if(HttpContext.GetOverriddenBrowser().IsMobileDevice)
                return View();
            
		    HomeViewModel model = new HomeViewModel();
            model.News = newsService.GetTop(3, orderBy: x => x.OrderByDescending(n => n.NewsDate));
            List<Fixture> latestFixtures = fixtureService.GetLatestMatchResults();

            // TODO Review Top player stats. Should MVP always be shown?
            foreach (Fixture fixture in latestFixtures)
            {
                model.LatestMatchResults.Add(new MatchResult()
                {
                    Fixture = fixture,
                    HomeTopPlayers = this.statsReportingService.GetTopPlayerStatsForFixture(fixture.Id, fixture.HomeTeamLeague.Id),
                    AwayTopPlayers = this.statsReportingService.GetTopPlayerStatsForFixture(fixture.Id, fixture.AwayTeamLeague.Id)
                });
            }

            List<League> currentLeagues = competitionService.GetLeaguesForCurrentSeason();

            foreach (League league in currentLeagues)
            {
                model.Divisions.Add(new DivisionStandings()
                {
                    Name = league.ToString(),
                    Standings = competitionService.GetStandingsForLeague(league.Id)
                });
            }

            model.NextEvents = eventService.GetNext();
            model.CupCompetitions = cupService.GetCupsForCurrentSeason();

            return View(model);
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult About()
        {
            return View();
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult VersionHistory()
        {
            return View();
        }

        public ActionResult GiveMeAnError()
        {
            return View();
        }

        #endregion

        #region AJAX
        [HttpGet]
		[OutputCache(CacheProfile="Public")]
        public JsonResult GetTickerStats()
        {
            return Request.IsAjaxRequest() ? this.Json(this.statsReportingService.BuildStatsTicker(), JsonRequestBehavior.AllowGet) : null;
        }

        #endregion
    }
}
