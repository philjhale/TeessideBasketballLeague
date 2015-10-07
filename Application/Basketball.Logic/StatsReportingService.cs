using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public class StatsReportingService : IStatsReportingService
    {
        #region Init
        private readonly IStatsReportingRepository statsReportingRepository;
        private readonly ICompetitionRepository competitionRepository;
        private readonly IOptionService optionService;
        private readonly IFixtureService fixtureService;
        

        public StatsReportingService(IStatsReportingRepository statsReportingRepository,
            ICompetitionRepository competitionRepository,
            IOptionService optionService,
            IFixtureService fixtureService)
        {
            this.statsReportingRepository = statsReportingRepository;
            this.competitionRepository = competitionRepository;
            this.optionService = optionService;
            this.fixtureService = fixtureService;
        }
        #endregion

        #region PlayerFixture
        public PlayerFixture GetPlayerFixture(int id)
        {
            return statsReportingRepository.GetPlayerFixture(id);
        }

        public List<PlayerFixture> GetPlayerFixturesForLeagueAndPlayer(int playerId, int leagueId)
        {
            return statsReportingRepository.GetPlayerFixturesForLeagueAndPlayer(playerId, leagueId).ToList();
        }
        public List<PlayerFixture> GetPlayerStatsForFixture(int fixtureId, int teamLeagueId)
        {
            return this.statsReportingRepository.GetPlayerStatsForFixture(fixtureId, teamLeagueId).ToList();
        }

        public List<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId, int numResults)
        {
            return this.statsReportingRepository.GetTopAvgScorersForLeague(leagueId, numResults).ToList();
        }

        public List<PlayerFixture> GetPlayerFixtureStatsForSeason(int playerId, int seasonId)
        {
            return this.statsReportingRepository.GetPlayerFixtureStatsForSeason(playerId, seasonId).ToList();
        }

        public List<PlayerFixture> GetTopPlayerStatsForFixture(int fixtureId, int teamLeagueId)
        {
            bool mvpFound = false;
            PlayerFixture mvp = null;

            // Get top scorers for fixture
            // TODO Remove hardcoded number of results value
            List<PlayerFixture> topScorers = this.statsReportingRepository.GetTopScorersForFixture(fixtureId, teamLeagueId, 3).ToList();

            foreach (PlayerFixture pf in topScorers)
            {
                if (pf.IsMvp == "Y")
                {
                    mvpFound = true;
                    break;
                }
            }

            // If MVP not found, find them. I.e. So the MVP is always shown
            if (!mvpFound)
            {
                // TODO Move this to a separate method?
                mvp = this.statsReportingRepository.GetMvpForFixture(fixtureId, teamLeagueId).SingleOrDefault();

                if (mvp != null)
                    topScorers.Add(mvp);
            }

            return topScorers;
        }

        public List<PlayerFixture> GetPlayerFixtureStatusForAllSeasons(int playerId)
        {
            return statsReportingRepository.GetPlayerFixtureStatusForAllSeasons(playerId).ToList();
        }

        public List<PlayerFixture> GetPlayerFixtureStatsForCupAndSeason(int playerId, int cupId, int seasonId)
        {
            return statsReportingRepository.GetPlayerFixtureStatsForCupAndSeason(playerId, cupId, seasonId).ToList();
        }
        #endregion

        #region PlayerCareerStats
        public PlayerCareerStats GetPlayerCareerStatsByPlayerId(int playerId)
        {
            return this.statsReportingRepository.GetPlayerCareerStatsByPlayerId(playerId);
        }
        #endregion

        #region PlayerSeasonStats

        public List<PlayerSeasonStats> GetPlayerAllSeasonStats(int playerId)
        {
            return this.statsReportingRepository.GetPlayerAllSeasonStats(playerId).ToList();
        }

        public PlayerSeasonStats GetPlayerSeasonStats(int playerId, int seasonId)
        {
            return this.statsReportingRepository.GetPlayerSeasonStats(playerId, seasonId);
        }

        public List<PlayerSeasonStats> GetPlayerSeasonStatsForTeamAndSeason(int teamId, int seasonId)
        {
            return this.statsReportingRepository.GetPlayerSeasonStatsForTeamAndSeason(teamId, seasonId).ToList();
        }
        #endregion

        #region PlayerCupStats
        public PlayerCupStats GetPlayerCupStats(int playerId, int cupId, int seasonId)
        {
            return statsReportingRepository.GetPlayerCupStats(playerId, cupId, seasonId);
        }

        public List<PlayerCupStats> GetTopAvgScorersForCup(int cupId, int seasonId, int numResults)
        {
            return statsReportingRepository.GetTopAvgScorersForCup(cupId, seasonId, numResults).ToList();
        }
        #endregion

        #region PlayerLeagueStats
        public PlayerLeagueStats GetPlayerLeagueStats(int playerId, int leagueId)
        {
            return statsReportingRepository.GetPlayerLeagueStats(playerId, leagueId);
        }
        #endregion

        #region Top Player Stats
        public List<PlayerLeagueStats> GetTopAvgScorersForLeague(int leagueId)
        {
            return this.statsReportingRepository.GetTopAvgScorersForLeague(leagueId, int.Parse(optionService.GetByName(Option.HOME_NUM_TICKER_PLAYER_STATS))).ToList();
        }

        public List<PlayerLeagueStats> GetTopScorersForLeague(int leagueId)
        {
            return this.statsReportingRepository.GetTopScorersForLeague(leagueId, int.Parse(optionService.GetByName(Option.HOME_NUM_TICKER_PLAYER_STATS))).ToList();
        }

        public List<PlayerLeagueStats> GetTopAvgFoulersForLeague(int leagueId)
        {
            return this.statsReportingRepository.GetTopAvgFoulersForLeague(leagueId, Int32.Parse(optionService.GetByName(Option.HOME_NUM_TICKER_PLAYER_STATS))).ToList();
        }

        public List<PlayerLeagueStats> GetTopFoulersForLeague(int leagueId)
        {
            return this.statsReportingRepository.GetTopFoulersForLeague(leagueId, Int32.Parse(optionService.GetByName(Option.HOME_NUM_TICKER_PLAYER_STATS))).ToList();
        }

        public List<PlayerLeagueStats> GetTopMvpAwardsForLeague(int leagueId)
        {
            return this.statsReportingRepository.GetTopMvpAwardsForLeague(leagueId, Int32.Parse(optionService.GetByName(Option.HOME_NUM_TICKER_PLAYER_STATS))).ToList();
        }

        public List<object> BuildStatsTicker()
        {
            List<object> statsList = new List<object>();
            Season currentSeason = competitionRepository.GetCurrentSeason();
            List<League> leagueList = new List<League>();
            List<PlayerLeagueStats> playerStats;
            string statsOutput;

            if (currentSeason != null)
                leagueList = competitionRepository.GetLeaguesForSeason(currentSeason.Id).ToList();

            foreach (League league in leagueList)
            {
                // TODO Find a better way to do this because this code is nasty
                // TODO Add unit tests

                // Top average scorers
                playerStats = GetTopAvgScorersForLeague(league.Id);
                statsOutput = "";
                foreach (PlayerLeagueStats pls in playerStats)
                    statsOutput = OutputStats(statsOutput, pls, pls.PointsPerGame.ToString("0.00"));

                if (playerStats.Count > 0)
                    statsOutput = statsOutput.Remove(statsOutput.Length - 2, 2);
                statsList.Add(new { Description = "Top Scorers Avg (" + league.ToString() + ")", Stats = statsOutput });

                // Top total scorers
                playerStats = GetTopScorersForLeague(league.Id);
                statsOutput = "";
                foreach (PlayerLeagueStats pls in playerStats)
                    statsOutput = OutputStats(statsOutput, pls, pls.TotalPoints.ToString());

                if (playerStats.Count > 0)
                    statsOutput = statsOutput.Remove(statsOutput.Length - 2, 2);

                statsList.Add(new { Description = "Top Scorers Total (" + league.ToString() + ")", Stats = statsOutput });

                // Top average foulrs
                playerStats = GetTopAvgFoulersForLeague(league.Id);
                statsOutput = "";
                foreach (PlayerLeagueStats pls in playerStats)
                    statsOutput = OutputStats(statsOutput, pls, pls.FoulsPerGame.ToString("0.00"));

                if (playerStats.Count > 0)
                    statsOutput = statsOutput.Remove(statsOutput.Length - 2, 2);

                statsList.Add(new { Description = "Worst Foulers Avg (" + league.ToString() + ")", Stats = statsOutput });

                // Top total foulrs
                playerStats = GetTopFoulersForLeague(league.Id);
                statsOutput = "";
                foreach (PlayerLeagueStats pls in playerStats)
                    statsOutput = OutputStats(statsOutput, pls, pls.TotalFouls.ToString());

                if (playerStats.Count > 0)
                    statsOutput = statsOutput.Remove(statsOutput.Length - 2, 2);

                statsList.Add(new { Description = "Worst Foulers Total (" + league.ToString() + ")", Stats = statsOutput });

                //  Most MVP awards
                playerStats = GetTopMvpAwardsForLeague(league.Id);
                statsOutput = "";
                foreach (PlayerLeagueStats pls in playerStats)
                    statsOutput = OutputStats(statsOutput, pls, pls.MvpAwards.ToString());

                if (playerStats.Count > 0)
                    statsOutput = statsOutput.Remove(statsOutput.Length - 2, 2);

                statsList.Add(new { Description = "Most MVP Awards (" + league.ToString() + ")", Stats = statsOutput });
            }

            return statsList;
        }

        private string OutputStats(string statsOutput, PlayerLeagueStats pls, string playerStat)
        {
            // Link is hardcoded rather that using the help which isn't great
            return statsOutput + "<a title=\"Click to view player stats\" href=\"/Stats/Player?id=" + pls.Player.Id + "\">" + pls.Player.ShortName + "</a> " + playerStat + ", ";
        }
        #endregion

        #region League/Cup Winners
        public List<LeagueWinner> GetLeagueWinsForTeam(int teamId)
        {
            return this.statsReportingRepository.GetLeagueWinsForTeam(teamId).ToList();           
        }

        public List<LeagueWinner> GetPastLeagueWinners()
        {
            return this.statsReportingRepository.GetAllLeagueWinners().OrderByDescending(lw => lw.League.Season.Id).ToList();
        }

        public List<CupWinner> GetCupWinsForTeam(int teamId)
        {
            return statsReportingRepository.GetCupWinnersForTeam(teamId).ToList();
        }

        public List<CupWinner> GetPastCupWinners()
        {
            return statsReportingRepository.GetAllCupWinners().OrderByDescending(lw => lw.Season.Id).ToList();
        }

        #endregion

        #region Team Stats
        public Fixture GetBiggestHomeWinForTeam(int teamId)
        {
            int? fixtureId = this.statsReportingRepository.GetFixtureIdForBiggestedHomeWinForTeam(teamId);

            if (!fixtureId.HasValue || fixtureId <= 0)
                return null;

            return fixtureService.Get(fixtureId.Value);
        }

        public Fixture GetBiggestAwayWinForTeam(int teamId)
        {
            int? fixtureId = this.statsReportingRepository.GetFixtureIdForBiggestedAwayWinForTeam(teamId);

            if (!fixtureId.HasValue || fixtureId <= 0)
                return null;

            return fixtureService.Get(fixtureId.Value);
        }

        public int GetTotalWins(int teamId)
        {
            return this.statsReportingRepository.GetTotalWins(teamId);
        }

        public int GetTotalLosses(int teamId)
        {
            return this.statsReportingRepository.GetTotalLosses(teamId);
        }

        public int GetTotalPointsFor(int teamId)
        {
            return this.statsReportingRepository.GetTotalPointsFor(teamId);
        }

        public int GetTotalPointsAgainst(int teamId)
        {
            return this.statsReportingRepository.GetTotalPointsAgainst(teamId);
        }

        public decimal GetAveragePointsPerGameForTeamFor(int teamId)
        {
            try
            {
                return (decimal)GetTotalPointsFor(teamId) / (decimal)fixtureService.CountFixturesForTeam(teamId);
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        public decimal GetAveragePointsPerGameForTeamAgainst(int teamId)
        {
            try
            {
                return (decimal)GetTotalPointsAgainst(teamId) / (decimal)fixtureService.CountFixturesForTeam(teamId);
            }
            catch (DivideByZeroException)
            {
                return 0;
            }
        }

        #endregion

        //#region CRUD
        //public void InsertOrUpdatePlayerFixturesThatHavePlayed(List<PlayerFixture> playerFixtures, List<bool> hasPlayed)
        //{
        //    statsRepository.InsertOrUpdatePlayerFixturesThatHavePlayed(playerFixtures, hasPlayed);
        //}
        //#endregion
    }
}
