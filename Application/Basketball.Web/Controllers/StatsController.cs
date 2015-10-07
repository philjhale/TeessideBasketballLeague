using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Basketball.Domain.Entities;
using Basketball.Web.BaseTypes;
using Basketball.Web.ViewModels;
using Basketball.Service.Interfaces;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.Controllers
{
    public class StatsController : BaseController
    {
        readonly IStatsReportingService statsReportingService;
        readonly ICompetitionService competitionService;
        readonly IPlayerService playerService;
        readonly IFixtureService fixtureService;

        public StatsController(IStatsReportingService statsReportingService,
            ICompetitionService competitionService,
            IPlayerService playerService,
            IFixtureService fixtureService)
        {
            this.statsReportingService = statsReportingService;
            this.competitionService = competitionService;
            this.playerService = playerService;
            this.fixtureService = fixtureService;
        }

        #region Actions
        [OutputCache(CacheProfile="Public")]
        public ActionResult ViewMatchResults()
        {
            List<Fixture> latestFixtures = fixtureService.GetLatestMatchResults(10);

            return View(latestFixtures);
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult ViewMatch(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            Fixture fixture = fixtureService.Get(id.Value);

            if (fixture == null)
                return RedirectToAction("Index", "Home");

            MatchStatsViewModel model = new MatchStatsViewModel()
            {
                Fixture = fixture,
                HomePlayerStats = this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.HomeTeamLeague.Id),
                AwayPlayerStats = this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.AwayTeamLeague.Id),
                FixtureHistory = fixtureService.GetHistoricFixturesBetweenTeams(fixture.HomeTeamLeague.Team.Id, fixture.AwayTeamLeague.Team.Id, fixture.Id)
            };

            model.SetHistoryFixtureWins();

            return View(model);
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Standings()
        {
            return View(PopulateTeamStatsViewModel(null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		[OutputCache(CacheProfile="Public")]
        public ActionResult Standings(int? filterBySeasonId)
        {
            if (filterBySeasonId.HasValue)
                return View(PopulateTeamStatsViewModel(filterBySeasonId));
            else
                return RedirectToAction("Standings");
        }

        // This only exists so it can be called via a get
        // Route is defined in Global.asax.cs so it looks like Standings
		[OutputCache(CacheProfile="Public")]
        public ActionResult StandingsGet(int? filterBySeasonId)
        {
            if(filterBySeasonId.HasValue)
                return View("Standings", PopulateTeamStatsViewModel(filterBySeasonId));
            else
                return RedirectToAction("Standings");
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Team()
        {
            return View(PopulateTeamStatsViewModel(null));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
		[OutputCache(CacheProfile="Public")]
        public ActionResult Team(int filterBySeasonId)
        {
            return View(PopulateTeamStatsViewModel(filterBySeasonId));
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Players()
        {
            return View(PopulatePlayerStatsViewModel(null));
        }

        // This doesn't seem to cache correctly
        [ValidateAntiForgeryToken]
        [HttpPost]
		[OutputCache(CacheProfile="Public")]
        public ActionResult Players(int filterBySeasonId)
        {
            return View(PopulatePlayerStatsViewModel(filterBySeasonId));
        } 

		[OutputCache(CacheProfile="Public")]
        public ActionResult Player(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            Player player = playerService.Get(id.Value);

            if (player == null)
                return RedirectToAction("Index", "Home");

            PlayerStatsViewModel model = new PlayerStatsViewModel()
            {
                Player = player,
                CurrentSeasonFixtureStats = this.statsReportingService.GetPlayerFixtureStatsForSeason(id.Value, competitionService.GetCurrentSeason().Id),
                SeasonStats = this.statsReportingService.GetPlayerAllSeasonStats(id.Value),
                CareerStats = this.statsReportingService.GetPlayerCareerStatsByPlayerId(id.Value)
            };
           
            return View(model);
        }

        [OutputCache(CacheProfile = "Public")]
        public ActionResult PastWinners()
        {
            var model = new PastWinnersViewModel(statsReportingService.GetPastLeagueWinners(), statsReportingService.GetPastCupWinners());
            return View(model);
        }

        public ActionResult PastLeagueWinners()
        {
            return RedirectToActionPermanent("PastWinners", "Stats");
        }
        #endregion

        #region Populate view model
        private TeamStatsViewModel PopulateTeamStatsViewModel(int? seasonId)
        {
            TeamStatsViewModel model = new TeamStatsViewModel();
            List<League> currentLeagues;

            model.Seasons = competitionService.GetSeasons(null, q => q.OrderByDescending(d => d.Id));

            if (!seasonId.HasValue)
                model.FilterBySeasonId = model.Seasons.Max(x => x.Id);
            else
                model.FilterBySeasonId = seasonId.Value;

            currentLeagues = competitionService.GetLeaguesForSeason(model.FilterBySeasonId);

            foreach (League league in currentLeagues)
            {
                model.DivisionStandings.Add(new DivisionStandings()
                {
                    Name = league.ToString(),
                    Standings = competitionService.GetStandingsForLeague(league.Id)
                });
            }

            return model;
        }

        private TopPlayerStatsViewModel PopulatePlayerStatsViewModel(int? seasonId)
        {
            TopPlayerStatsViewModel model = new TopPlayerStatsViewModel();
            List<League> currentLeagues;

            model.Seasons = competitionService.GetSeasons(null, q => q.OrderByDescending(d => d.Id));

            if (!seasonId.HasValue)
                model.FilterBySeasonId = model.Seasons.Max(x => x.Id);
            else
                model.FilterBySeasonId = seasonId.Value;

            currentLeagues = competitionService.GetLeaguesForSeason(model.FilterBySeasonId);

            foreach (League league in currentLeagues)
            {
                model.DivisionPlayerStats.Add(new DivisionPlayerStats()
                {
                    Name = league.ToString(),
                    TopAvgScorers = this.statsReportingService.GetTopAvgScorersForLeague(league.Id, 10)
                });
            }

            model.SeasonHasStats = model.DivisionPlayerStats.Count > 0 && model.DivisionPlayerStats[0].TopAvgScorers.Any();

            return model;
        } 
        #endregion
    }
}
