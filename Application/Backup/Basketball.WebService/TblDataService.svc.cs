using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using Basketball.Common.Mapping;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.WebService.DataTransferObjects;

namespace Basketball.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TblDataService" in code, svc and config file together.
    /// <summary>
    /// Useful info
    /// 
    /// WCF services present some problems to Entity Framework. Firstly ProxyCreationEnabled must be set to false
    /// because WCF cannot handle the proxy objects for reasons I don't understand. Secondly setting this value to false
    /// means that nothing can be lazy loaded which means navigation properites MUST be eager loaded explicitly
    /// 
    /// So...what all this means is that the web site uses BasketballContext which enables lazy loading while WCF uses WcfBasketballContext
    /// which disables lazy loading and enables forced loading of navigation properties (see BaseRepository and MustEagerLoadClosestNavigationProperties
    /// 
    /// Conventions
    /// 
    /// Currently if any exceptions are being thrown, they are caught, logged and null is returned
    /// 
    /// TODO Log exceptions
    /// TODO Check how much data is actually being sent
    /// </summary>
    [ServiceBehavior(Namespace = "http://www.teessidebasketball.co.uk")]
    public class TblDataService : ITblDataService
    {
        private readonly INewsService newsService;
        private readonly IFixtureService fixtureService;
        private readonly ITeamService teamService;
        private readonly ICompetitionService competitionService;
        private readonly IStatsReportingService statsReportingService;
        private readonly IPlayerService playerService;

        public TblDataService(INewsService newsService, IFixtureService fixtureService, ITeamService teamService, ICompetitionService competitionService, IStatsReportingService statsReportingService, IPlayerService playerService)
        {
            this.newsService        = newsService;
            this.fixtureService     = fixtureService;
            this.teamService        = teamService;
            this.competitionService = competitionService;
            this.statsReportingService       = statsReportingService;
            this.playerService      = playerService;
        }

        #region Web service methods
        public List<News> GetNews(int numberOfArticles)
        {
            return newsService.GetTop(numberOfArticles, orderBy: q => q.OrderByDescending(n => n.Id));
        }

        public List<FixtureDto> GetLatestMatchResults(int numberOfMatchResults)
        {
            return (from result in fixtureService.GetLatestMatchResults(numberOfMatchResults) 
                        select new FixtureDto(result, false)).ToList();
        }

        public MatchResultDto GetMatchResult(int fixtureId)
        {
            // TODO Stip HTML from report http://htmlagilitypack.codeplex.com/
            Fixture fixture = fixtureService.Get(fixtureId);
            List<PlayerFixture> homePlayerStats = this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.HomeTeamLeague.Id);
            List<PlayerFixture> awayPlayerStats = this.statsReportingService.GetPlayerStatsForFixture(fixture.Id, fixture.AwayTeamLeague.Id);

            return new MatchResultDto(fixture, homePlayerStats, awayPlayerStats);
        }

        /// <param name="teamId">0 for all teams</param>
        /// <param name="isPlayed">Y/N or null/blank for all teams</param>
        public List<FixtureDto> GetFixturesFiltered(int teamId, string isPlayed)
        {
            return (from result in fixtureService.GetFixturesForCurrentSeasonFilter(teamId, isPlayed, null) select new FixtureDto(result, false)).ToList();
        }

        public List<Team> GetTeamsForCurrentSeason()
        {
            return teamService.GetTeamsForCurrentSeason().OrderBy(x => x.TeamName).ToList();
        }

        public List<TeamNameDto> GetTeamsNamesForCurrentSeason()
        {
            return (from result in teamService.GetTeamsForCurrentSeason().OrderBy(x => x.TeamName)
                    select new TeamNameDto(result)).ToList();
        }

        public LeagueStandingsDto GetStandingsForCurrentSeason()
        {
            List<League> currentLeagues = competitionService.GetLeaguesForCurrentSeason();
            var leagueStandings = new LeagueStandingsDto();

            foreach (League league in currentLeagues)
            {
                leagueStandings.DivisionStandings.Add(new DivisionStandingsDto(league.ToString(), competitionService.GetStandingsForLeague(league.Id)));
            }

            return leagueStandings;
        }

        public Team GetTeam(int id)
        {
            return teamService.Get(id);
        }

        public PlayerStatsDto GetAllPlayerStats(int id)
        {
            Player player = playerService.Get(id);

            List<PlayerFixture> playerFixturesForSeason = this.statsReportingService.GetPlayerFixtureStatsForSeason(id, competitionService.GetCurrentSeason().Id);
            List<int> fixtureIds = playerFixturesForSeason.Select(y => y.Fixture.Id).ToList();
            List<Fixture> fixturesForSeason = fixtureService.Get(x => fixtureIds.Contains(x.Id)); // Required because EF can't pull back all PlayerFixture data
            
            return new PlayerStatsDto(
                player,
                playerFixturesForSeason,
                fixturesForSeason,
                this.statsReportingService.GetPlayerAllSeasonStats(id),
                this.statsReportingService.GetPlayerCareerStatsByPlayerId(id)
           );
        }

        public void TestError()
        {
            throw new Exception("this is a test");
        } 
        #endregion
    }
}
