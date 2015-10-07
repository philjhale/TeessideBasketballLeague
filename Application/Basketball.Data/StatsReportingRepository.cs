using System.Linq;
using Basketball.Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections;
using System.IO;
using Basketball.Data.Interfaces;

namespace Basketball.Data
{
    /// <summary>
    /// Used to report on stats. Should only get and never save
    /// Encapsulates PlayerFixture, PlayerSeasonStats and PlayerCareerStats (plus LeagueWinner for querying purposes)
    /// </summary>
    public class StatsReportingRepository : IStatsReportingRepository
    {
        #region Init

        private readonly ITeamLeagueRepository          teamLeagueRepository;
        private readonly IPlayerLeagueStatsRepository   playerLeagueStatsRepository;
        private readonly IPlayerFixtureRepository       playerFixtureRepository;
        private readonly IPlayerSeasonStatsRepository   playerSeasonStatsRepository;
        private readonly IPlayerCareerStatsRepository   playerCareerStatsRepository;
        private readonly ILeagueWinnerRepository        leagueWinnerRepository;
        private readonly ICupWinnerRepository           cupWinnerRepository;
        private readonly IFixtureRepository             fixtureRepository;
        private readonly IPlayerCupStatsRepository      playerCupStatsRepository;

        public StatsReportingRepository(ITeamLeagueRepository teamLeagueRepository,
            IPlayerLeagueStatsRepository playerLeagueStatsRepository,
            IPlayerFixtureRepository playerFixtureRepository,
            IPlayerSeasonStatsRepository playerSeasonStatsRepository,
            IPlayerCareerStatsRepository playerCareerStatsRepository,
            ILeagueWinnerRepository leagueWinnerRepository,
            ICupWinnerRepository cupWinnerRepository,
            IFixtureRepository fixtureRepository,
            IPlayerCupStatsRepository playerCupStatsRepository)
        {
            this.teamLeagueRepository        = teamLeagueRepository;
            this.playerLeagueStatsRepository = playerLeagueStatsRepository;
            this.playerFixtureRepository     = playerFixtureRepository;
            this.playerSeasonStatsRepository = playerSeasonStatsRepository;
            this.playerCareerStatsRepository = playerCareerStatsRepository;
            this.leagueWinnerRepository      = leagueWinnerRepository;
            this.cupWinnerRepository         = cupWinnerRepository;
            this.fixtureRepository           = fixtureRepository;
            this.playerCupStatsRepository    = playerCupStatsRepository;
        } 
        #endregion

        #region PlayerFixture
        public IQueryable<PlayerFixture> GetPlayerStatsForFixture(int fixtureId, int teamLeagueId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Fixture.Id == fixtureId
                 && pf.TeamLeague.Id == teamLeagueId
                 orderby pf.Player.Forename
                 select pf);
        }

        public IQueryable<PlayerFixture> GetPlayerFixtureStatsForSeason(int playerId, int seasonId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Fixture.HomeTeamLeague.League.Season.Id == seasonId
                    && pf.Player.Id == playerId
                 orderby pf.Fixture.FixtureDate
                 select pf);
        }

        public IQueryable<PlayerFixture> GetPlayerFixtureStatusForAllSeasons(int playerId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Player.Id == playerId
                 select pf);
        }

        public IQueryable<PlayerFixture> GetPlayerFixturesForLeagueAndPlayer(int playerId, int leagueId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Player.Id == playerId
                    && pf.TeamLeague.League.Id == leagueId
                    && !pf.Fixture.IsCupFixture
                 select pf);
        }

        public IQueryable<PlayerFixture> GetPlayerFixtureStatsForCupAndSeason(int playerId, int cupId, int seasonId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Fixture.IsCupFixture
                    && pf.Fixture.Cup.Id == cupId
                    && pf.Fixture.HomeTeamLeague.League.Season.Id == seasonId
                    && pf.Player.Id == playerId
                 orderby pf.Fixture.FixtureDate
                 select pf);
        }
        #endregion

        #region PlayerSeasonStats

        public IQueryable<PlayerSeasonStats> GetPlayerAllSeasonStats(int playerId)
        {
            return
                (from pss in playerSeasonStatsRepository.Get()
                 where pss.Player.Id == playerId
                 orderby pss.Season.Id
                 select pss);
            //ICriteria criteria = Session.CreateCriteria(typeof(PlayerSeasonStats))
            //    .Add(Expression.Eq("Player.Id", playerId))
            //     .AddOrder(Order.Asc("Season.Id"));

            //return criteria.List<PlayerSeasonStats>();
        }

        public IQueryable<PlayerSeasonStats> GetPlayerSeasonStatsForTeamAndSeason(int teamId, int seasonId)
        {
            return
                (from pss in playerSeasonStatsRepository.Get()
                 where pss.Player.Team.Id == teamId
                    && pss.Season.Id == seasonId
                 orderby pss.Player.Forename, pss.Player.Surname
                 select pss);
        }
        #endregion

        #region CRUD
        public PlayerSeasonStats GetPlayerSeasonStats(int id)
        {
            return playerSeasonStatsRepository.Get(id);
        }

        public PlayerSeasonStats GetPlayerSeasonStats(int playerId, int seasonId)
        {
            return (from pss in playerSeasonStatsRepository.Get()
                    where pss.Player.Id == playerId
                        && pss.Season.Id == seasonId
                    select pss).SingleOrDefault();
        }

        public PlayerFixture GetPlayerFixture(int id)
        {
            return playerFixtureRepository.Get(id);
        }

        public PlayerLeagueStats GetPlayerLeagueStats(int id)
        {
            return playerLeagueStatsRepository.Get(id);
        }

        public PlayerLeagueStats GetPlayerLeagueStats(int playerId, int leagueId)
        {
            return (from pss in playerLeagueStatsRepository.Get()
                    where pss.Player.Id == playerId
                        && pss.League.Id == leagueId
                    select pss).SingleOrDefault();
        }

        public PlayerCupStats GetPlayerCupStats(int playerId, int cupId, int seasonId)
        {
            return (from pcs in playerCupStatsRepository.Get()
                    where pcs.Player.Id == playerId
                        && pcs.Cup.Id == cupId
                        && pcs.Season.Id == seasonId
                    select pcs).SingleOrDefault();
        }

        /// <returns>Returns object or null</returns>
        public PlayerCareerStats GetPlayerCareerStatsByPlayerId(int playerId)
        {
            return
                (from pcs in playerCareerStatsRepository.Get()
                 where pcs.Player.Id == playerId
                 select pcs).SingleOrDefault();
        }
        #endregion

        #region League/Cup Winners
        public IQueryable<LeagueWinner> GetLeagueWinsForTeam(int teamId)
        {
            return (from lw in leagueWinnerRepository.Get() 
                    where lw.Team.Id == teamId 
                    orderby lw.League.Season.StartYear descending 
                    select lw);
        }

        public IQueryable<LeagueWinner> GetAllLeagueWinners()
        {
            return (from lw in leagueWinnerRepository.Get()
                    select lw);
        }

        public IQueryable<CupWinner> GetCupWinnersForTeam(int teamId)
        {
            return (from cw in cupWinnerRepository.Get() 
                    where cw.Team.Id == teamId 
                    orderby cw.Season.StartYear descending 
                    select cw);
        }

        public IQueryable<CupWinner> GetAllCupWinners()
        {
            return (from lw in cupWinnerRepository.Get()
                    select lw);
        }
        #endregion

        #region Team Stats
        public int? GetFixtureIdForBiggestedHomeWinForTeam(int teamId)
        {
            // Select all the fixture id and winning margin for all winning home fixtures
            var homeWinsPointsDifference = from f in fixtureRepository.Get()
                    where f.HomeTeamLeague.Team.Id == teamId
                        && f.HomeTeamScore > f.AwayTeamScore
                        select new { f.Id, Diff = f.HomeTeamScore - f.AwayTeamScore};

            // Select the fixtureI of the fixture with the biggest margin
             return (from fixture in homeWinsPointsDifference
                     group fixture by new {fixture.Id, fixture.Diff} into grp
                     orderby grp.Key.Diff descending 
                     select grp.Key.Id).Take(1).SingleOrDefault();
        }

        public int? GetFixtureIdForBiggestedAwayWinForTeam(int teamId)
        {
            // Select all the fixture id and winning margin for all winning away fixtures
            var awayWinsPointsDifferent = from f in fixtureRepository.Get()
                                           where f.AwayTeamLeague.Team.Id == teamId
                                               && f.AwayTeamScore > f.HomeTeamScore
                                           select new { f.Id, Diff = f.AwayTeamScore - f.HomeTeamScore };

            // Select the fixtureI of the fixture with the biggest margin
            return (from fixture in awayWinsPointsDifferent
                    group fixture by new { fixture.Id, fixture.Diff } into grp
                    orderby grp.Key.Diff descending
                    select grp.Key.Id).Take(1).SingleOrDefault();
        } 

        public int GetTotalWins(int teamId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.Team.Id == teamId
                    select tl.GamesWonTotal).Sum();
        }

        public int GetTotalLosses(int teamId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.Team.Id == teamId
                    select tl.GamesLostTotal).Sum();
        }

        public int GetTotalPointsFor(int teamId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.Team.Id == teamId
                    select tl.PointsScoredFor).Sum();
        }

        public int GetTotalPointsAgainst(int teamId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.Team.Id == teamId
                    select tl.PointsScoredAgainst).Sum();
        }

        public int GetAveragePointsPerGameForTeam(int teamId)
        {
            return (from tl in teamLeagueRepository.Get()
                    where tl.Team.Id == teamId
                    select tl.PointsScoredFor).Sum();
        }

        #endregion

        #region Top Stats

        public IQueryable<PlayerFixture> GetTopScorersForFixture(int fixtureId, 
            int teamLeagueId,
            int numTopScorers)
        {
            return
                (from pf in playerFixtureRepository.Get()
                 where pf.Fixture.Id == fixtureId
                    && pf.TeamLeague.Id == teamLeagueId
                 orderby pf.PointsScored descending, pf.Player.Forename
                 select pf)
                 .Take(numTopScorers);
            //Session.CreateCriteria(typeof(PlayerFixture))
            //    .CreateAlias("Player", "p")
            //    .Add(Expression.Eq("Fixture.Id", fixtureId))
            //    .Add(Expression.Eq("TeamLeague.Id", teamLeagueId))
            //    .AddOrder(Order.Desc("PointsScored"))
            //    .AddOrder(Order.Asc("p.Forename"))
            //    .SetMaxResults(numTopScorers)
            //    .List<PlayerFixture>();
        }

        public IQueryable<PlayerFixture> GetMvpForFixture(int fixtureId, 
            int teamLeagueId)
        {
            return
                (from pf in playerFixtureRepository.Get()
                     where pf.Fixture.Id == fixtureId
                        && pf.TeamLeague.Id == teamLeagueId
                        && pf.IsMvp == "Y"
                     select pf);
            //Session.CreateCriteria(typeof(PlayerFixture))
            //    .CreateAlias("Player", "p")
            //    .Add(Restrictions.Eq("Fixture.Id", fixtureId))
            //    .Add(Restrictions.Eq("TeamLeague.Id", teamLeagueId))
            //    .Add(Restrictions.Eq("IsMvp", "Y"))
            //    .UniqueResult<PlayerFixture>();
        }

        public IQueryable<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId, int numResults)
        {
            // Find the top numResults PlayerLeagueStats record for the specified season where the player players for
            // a specified team
            return
                (from pls in playerLeagueStatsRepository.Get()
                 where pls.League.Id == leagueId
                 orderby pls.PointsPerGame descending
                 select pls).Take(numResults);
            //List<PlayerLeagueStats> topScorers = Session.CreateCriteria(typeof(PlayerLeagueStats))
            //    .Add(Restrictions.Eq("League.Id", leagueId))
            //    .AddOrder(Order.Desc("PointsPerGame"))
            //    .SetMaxResults(numResults)
            //    .List<PlayerLeagueStats>();

            //return topScorers;
        }

        public IQueryable<PlayerLeagueStats> GetTopScorersForLeague(int leagueId, int numResults)
        {
            return
                (from pls in playerLeagueStatsRepository.Get()
                 where pls.League.Id == leagueId
                 orderby pls.TotalPoints descending
                 select pls).Take(numResults);
        }

        public IQueryable<PlayerLeagueStats> GetTopAvgFoulersForLeague(int leagueId, int numResults)
        {
            return
                (from pls in playerLeagueStatsRepository.Get()
                 where pls.League.Id == leagueId
                 orderby pls.FoulsPerGame descending
                 select pls).Take(numResults);
            //return Session.CreateCriteria(typeof(PlayerLeagueStats))
            //    .Add(Restrictions.Eq("League.Id", leagueId))
            //    .AddOrder(Order.Desc("FoulsPerGame"))
            //    .SetMaxResults(numResults)
            //    .List<PlayerLeagueStats>();
        }

        public IQueryable<PlayerLeagueStats> GetTopFoulersForLeague(int leagueId, int numResults)
        {
            return
                (from pls in playerLeagueStatsRepository.Get()
                 where pls.League.Id == leagueId
                 orderby pls.TotalFouls descending
                 select pls).Take(numResults);
        }

        public IQueryable<PlayerLeagueStats> GetTopMvpAwardsForLeague(int leagueId, int numResults)
        {
            return
                (from pls in playerLeagueStatsRepository.Get()
                 where pls.League.Id == leagueId
                    && pls.MvpAwards > 0
                 orderby pls.MvpAwards descending
                 select pls).Take(numResults);
            //return Session.CreateCriteria(typeof(PlayerLeagueStats))
            //    .Add(Restrictions.Eq("League.Id", leagueId))
            //    .Add(Restrictions.Gt("MvpAwards", 0))
            //    .AddOrder(Order.Desc("MvpAwards"))
            //    .SetMaxResults(numResults)
            //    .List<PlayerLeagueStats>();
        }

        public IQueryable<PlayerCupStats> GetTopAvgScorersForCup(int cupId, int seasonId, int numResults)
        {
            return
                (from pcs in playerCupStatsRepository.Get()
                 where pcs.Cup.Id == cupId
                    && pcs.Season.Id == seasonId
                 orderby pcs.TotalPoints descending
                 select pcs).Take(numResults);
        }
        #endregion
        
        #region PlayerFixture CRUD

        //public PlayerFixture GetPlayerFixture(int id)
        //{
        //    return Session.Get<PlayerFixture>(id);
        //}

        

        //public PlayerFixture SaveOrUpdatePlayerFixture(PlayerFixture playerFixture)
        //{
        //    Check.Require(!(playerFixture is IHasAssignedId<int>),
        //        "For better clarity and reliability, Entities with an assigned Id must call Save or Update");

        //    Session.SaveOrUpdate(playerFixture);
        //    return playerFixture;
        //}

        // Unused
        //public void InsertOrUpdatePlayerFixturesThatHavePlayed(List<PlayerFixture> playerFixtures, List<bool> hasPlayed)
        //{
        //    for (int i = 0; i < playerFixtures.Count; i++)
        //    {
        //        // Do not update players that have not played
        //        if (hasPlayed[i])
        //            playerFixtureRepository.InsertOrUpdate(playerFixtures[i]);
        //    }

        //}

        #endregion

        //#region PlayerSeasonStats CRUD

        

        

        

        //#endregion

        //#region PlayerLeagueStats CRUD

        

        


        //#endregion

        //#region PlayerCareerStats CRUD

        //public PlayerCareerStats GetPlayerCareerStats(int id)
        //{
        //    return Session.Get<PlayerCareerStats>(id);
        //}

        

        

        ////public void SaveOrUpdatePlayerFixtures(List<PlayerFixture> playerFixtures)
        ////{
        ////    foreach (PlayerFixture pf in playerFixtures)
        ////        SaveOrUpdatePlayerFixture(pf);
        ////}

        //#endregion
        
    }
}
