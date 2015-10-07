using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Common.Extensions;
using Basketball.Common.Mapping;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service.Extensions;
using Basketball.Service.Interfaces;

namespace Basketball.Service
{
    public class MatchResultService : IMatchResultService 
    {
        private readonly IMatchResultRepository matchResultRepository;
        private readonly IPlayerService playerService;
        private readonly ICompetitionService competitionService;
        private readonly IFixtureService fixtureService;
        private readonly IStatsReportingService statsReportingService;


        public MatchResultService(IMatchResultRepository matchResultRepository, 
            IPlayerService playerService, 
            ICompetitionService competitionService, 
            IFixtureService fixtureService,
            IStatsReportingService statsReportingService)
        {
            this.matchResultRepository          = matchResultRepository;
            this.playerService                  = playerService;
            this.competitionService             = competitionService;
            this.fixtureService                 = fixtureService;
            this.statsReportingService          = statsReportingService;
        }

        public void Commit()
        {
            matchResultRepository.Commit();
            fixtureService.Commit();
        }

        #region TeamLeague

        public TeamLeague UpdateTeamLeagueStats(int teamLeagueId)
        {
            TeamLeague newStats = this.competitionService.GetTeamLeague(teamLeagueId);
            newStats = ResetStats(newStats);

            List<Fixture> teamFixtureList = fixtureService.GetPlayedFixturesForTeamInReverseDateOrder(teamLeagueId);

            //Console.WriteLine("fixtures found: " + fixtureList.Count);

            // TODO Refactor this. Probably should be in save fixture method
            foreach (Fixture f in teamFixtureList)
            {
                if(f.IsHomeForfeit())
                {
                    f.HomeTeamScore = StandingsCalculations.ForfeitLossScore;
                    f.AwayTeamScore = StandingsCalculations.ForfeitWinScore;
                }
                else if(f.IsAwayForfeit())
                {
                    f.HomeTeamScore = StandingsCalculations.ForfeitWinScore;
                    f.AwayTeamScore = StandingsCalculations.ForfeitLossScore;
                }
            }

            var nonCupFixtures              = teamFixtureList.Where(f => !f.IsCupFixture).ToList();
            newStats.PointsLeague           = newStats.GetLeaguePointsFromWins(nonCupFixtures);
            newStats.PointsLeague          += newStats.GetLeaguePointsFromLosses(nonCupFixtures);
            newStats.PointsScoredFor        = newStats.GetPointsScoredFor(nonCupFixtures);
            newStats.PointsScoredAgainst    = newStats.GetPointsScoredAgainst(nonCupFixtures);
            newStats.PointsScoredDifference = newStats.PointsScoredFor - newStats.PointsScoredAgainst;
            newStats.GamesPlayed            = newStats.GetGamesPlayed(nonCupFixtures);
            newStats.GamesWonHome           = newStats.GetHomeWins(nonCupFixtures);
            newStats.GamesWonAway           = newStats.GetAwayWins(nonCupFixtures);
            newStats.GamesLostHome          = newStats.GetHomeLosses(nonCupFixtures);
            newStats.GamesLostAway          = newStats.GetAwayLosses(nonCupFixtures);
            newStats.GamesWonTotal          = newStats.GetTotalWins(nonCupFixtures);
            newStats.GamesLostTotal         = newStats.GetTotalLosses(nonCupFixtures);
            newStats.GamesForfeited         = newStats.GetForfeitedGames(nonCupFixtures);

            // Deduct any penalty points (penalties defaults to 0)
            newStats.PointsLeague -= newStats.PointsPenalty;

            // Pts difference
            //newStats.PointsScoredDifference = newStats.PointsScoredFor - newStats.PointsScoredAgainst;
            if (newStats.GamesPlayed > 0)
            {
                newStats.GamesPct = (decimal)newStats.GamesWonTotal / (decimal)newStats.GamesPlayed;
                newStats.PointsScoredPerGameAvg = (decimal)newStats.PointsScoredFor / (decimal)newStats.GamesPlayed;
                newStats.PointsAgainstPerGameAvg = (decimal)newStats.PointsScoredAgainst / (decimal)newStats.GamesPlayed;
                newStats.PointsScoredPerGameAvgDifference = newStats.PointsScoredPerGameAvg - newStats.PointsAgainstPerGameAvg;
            }

            // Calc streak
            bool? lastResultWin = null;
            bool streakWin = false;
            int streakNumber = 0;
            foreach (Fixture f in teamFixtureList)
            {
                // Home
                if (f.HomeTeamLeague.Id == teamLeagueId)
                {
                    if (f.HomeTeamScore > f.AwayTeamScore) // Home win
                        streakWin = true;
                    else
                        streakWin = false;
                }
                // Away
                else if (f.AwayTeamLeague.Id == teamLeagueId)
                {
                    if (f.AwayTeamScore > f.HomeTeamScore) // Away win
                        streakWin = true;
                    else
                        streakWin = false;
                }

                //Console.WriteLine(streakWin);
                // Keep track of the last result
                if (lastResultWin == null)
                    lastResultWin = streakWin;

                //Console.WriteLine("streak: " + streakWin);
                //Console.WriteLine("lastResultWin: " + lastResultWin);

                // If the present result was not the same as the last
                // result exit the loop
                if (streakWin != lastResultWin)
                    break;

                streakNumber++;
            }

            if (lastResultWin != null && streakNumber > 0)
                newStats.Streak = ((bool)lastResultWin == true ? "W" : "L") + streakNumber;
            else
                newStats.Streak = "";


            matchResultRepository.UpdateTeamLeague(newStats);

            return newStats;
        }

        public TeamLeague ResetStats(TeamLeague tl)
        {
            tl.PointsLeague            = 0;
            tl.GamesPlayed             = 0;
            tl.GamesPct                = 0;
            tl.GamesWonTotal           = 0;
            tl.GamesWonHome            = 0;
            tl.GamesWonAway            = 0;
            tl.GamesLostTotal          = 0;
            tl.GamesLostHome           = 0;
            tl.GamesLostAway           = 0;
            tl.PointsScoredFor         = 0;
            tl.PointsScoredAgainst     = 0;
            tl.PointsScoredDifference  = 0;
            tl.Streak                  = null;
            tl.PointsAgainstPerGameAvg = 0;
            tl.PointsScoredPerGameAvg  = 0;
            tl.GamesForfeited          = 0;

            return tl;
        }
        #endregion

        #region Match Result
        // TODO This method really needs refactoring as it's difficult to unit test
        public void SaveMatchStats(bool hasPlayerStats, List<PlayerFixtureStats> playerStats, TeamLeague teamLeague, Fixture fixture)
        {
            // Deal appropriately with each player
            // Existing or non existing players that played - update or insert
            // Existing players that did not play - delete
            // Non existing players that did not play - ignore
            List<PlayerFixture> playerStatsToUpdate = new List<PlayerFixture>();
            PlayerFixture actionablePlayerFixture = null;
            foreach (var ps in playerStats)
            {
                if (ps.PlayerFixtureId > 0)
                    actionablePlayerFixture = this.statsReportingService.GetPlayerFixture(ps.PlayerFixtureId);
                else
                {
                    // Not need to add scores or fouls yet. That will be done by MapToPlayerFixture
                    // Not sure if the new Player setting Id thing will work
                    actionablePlayerFixture = new PlayerFixture(teamLeague, fixture, playerService.Get(ps.PlayerId), 0, 0);
                }

                if (ps.HasPlayed)
                {
                    matchResultRepository.InsertOrUpdatePlayerFixture(ps.MapToPlayerFixture(actionablePlayerFixture));
                    playerStatsToUpdate.Add(actionablePlayerFixture);
                }
                else if (!ps.HasPlayed && ps.PlayerFixtureId > 0)
                {
                    matchResultRepository.DeletePlayerFixture(actionablePlayerFixture);
                    // Awful hack alert. Because the PlayerFixture has been deleted the PlayerFixture.Player object will
                    // be null. But, we still need it, so the only solution I can currently think of is to get it again
                    playerStatsToUpdate.Add(new PlayerFixture(teamLeague, fixture, playerService.Get(ps.PlayerId), 0, 0));
                }
            }

            Commit();

            // Are all commits necessary?

            // Update player league stats
            UpdatePlayerLeagueStats(playerStatsToUpdate, teamLeague.League);
            Commit();

            // Update player season stats
            Season currentSeason = competitionService.GetCurrentSeason();
            UpdatePlayerSeasonStats(playerStatsToUpdate, currentSeason);
            Commit();

            if(fixture.IsCupFixture)
            {
                UpdatePlayerCupStats(playerStatsToUpdate, fixture.Cup, currentSeason);
                Commit();
            }

            // Update player career stats
            UpdatePlayerCareerStats(playerStatsToUpdate);
            Commit();

            // Update team stats
            UpdateTeamLeagueStats(teamLeague.Id);
            Commit();
        }




        // Delete existing records that have been marked as not played
        //statsService.DeleteExistingPlayerFixturesThatDidNotPlay(homePlayerStats, homeHasPlayed);
        //statsService.DeleteExistingPlayerFixturesThatDidNotPlay(awayPlayerStats, awayHasPlayed);

        //// Remove players that did not player from the list so we only update those who played
        //RemovePlayersThatDidNotPlay(homePlayerStats, homeHasPlayed);
        //RemovePlayersThatDidNotPlay(awayPlayerStats, awayHasPlayed);

        //// Save player fixture records. Pass whether each player has played so players that haven't played are not
        //// updated. If this wasn't passed in the PlayerFixture records would be delete by the method call above then
        //// inserted again by the call below

        //// The logic is a bit odd here. All players, played or unplayed must be passed to the update 
        //// season/league/career methods so they are always update (e.g. if a PlayerFixture record is saved then marked
        //// unplayed it is deleted, but the player record must still be passed to the update stats method to recalculate
        //// the stats
        //statsService.InsertOrUpdatePlayerFixturesThatHavePlayed(homePlayerStats, homeHasPlayed);
        //statsService.InsertOrUpdatePlayerFixturesThatHavePlayed(awayPlayerStats, awayHasPlayed);
        ////statsService.Commit();

        //// Update player league stats
        //statsService.UpdatePlayerLeagueStats(homePlayerStats, fixtureToUpdate.HomeTeamLeague.League);
        //statsService.UpdatePlayerLeagueStats(awayPlayerStats, fixtureToUpdate.HomeTeamLeague.League);

        //// Update player season stats
        //Season currentSeason = GetCurrentSeason();
        //statsService.UpdatePlayerSeasonStats(homePlayerStats, currentSeason);
        //statsService.UpdatePlayerSeasonStats(awayPlayerStats, currentSeason);

        //// Update player career stats
        //statsService.UpdatePlayerCareerStats(homePlayerStats);
        //statsService.UpdatePlayerCareerStats(awayPlayerStats);

        //// Update team stats
        //UpdateTeamLeagueStats(fixtureToUpdate.HomeTeamLeague.Id);
        //UpdateTeamLeagueStats(fixtureToUpdate.AwayTeamLeague.Id);

        #endregion

        public PlayerSeasonStats UpdatePlayerSeasonStats(PlayerFixture playerFixture, Season season)
        {
            PlayerSeasonStats playerSeasonStats;
            int totalPoints = 0;
            int totalFouls = 0;
            int mvpAwards = 0;

            // Get all PlayerFixture records for specified season
            List<PlayerFixture> playerFixturesForSeason = this.statsReportingService.GetPlayerFixtureStatsForSeason(playerFixture.Player.Id, season.Id).ToList();

            // Total stats
            foreach (PlayerFixture pf in playerFixturesForSeason)
            {
                totalPoints += pf.PointsScored;
                totalFouls += pf.Fouls;
                if (pf.IsMvp == "Y")
                    mvpAwards++;
            }

            // Find existing record
            playerSeasonStats = this.statsReportingService.GetPlayerSeasonStats(playerFixture.Player.Id, season.Id);

            // If doesn't exist, create new
            if (playerSeasonStats == null)
                playerSeasonStats = new PlayerSeasonStats(playerFixture.Player, season, totalPoints, totalFouls, playerFixturesForSeason.Count, mvpAwards);
            else
            {
                // Update values
                playerSeasonStats.UpdateStats(totalPoints, totalFouls, playerFixturesForSeason.Count, mvpAwards);
            }

            // Save
            matchResultRepository.SavePlayerSeasonStats(playerSeasonStats);

            return playerSeasonStats;
        }

        public void UpdatePlayerSeasonStats(List<PlayerFixture> playerFixtures, Season season)
        {
            foreach (PlayerFixture pf in playerFixtures)
                UpdatePlayerSeasonStats(pf, season);
        }

        public PlayerCupStats UpdatePlayerCupStats(PlayerFixture playerFixture, Cup cup, Season season)
        {
            PlayerCupStats playerCupStats;
            
            // Get all PlayerFixture records for specified season
            List<PlayerFixture> playerFixturesForCup = statsReportingService.GetPlayerFixtureStatsForCupAndSeason(playerFixture.Player.Id, cup.Id, season.Id).ToList();

            // Total stats
            int totalPoints = playerFixturesForCup.Sum(pf => pf.PointsScored);
            int totalFouls = playerFixturesForCup.Sum(pf => pf.Fouls);
            int mvpAwards = playerFixturesForCup.Count(pf => pf.IsMvp == "Y");
            

            // Find existing record
            playerCupStats = statsReportingService.GetPlayerCupStats(playerFixture.Player.Id, cup.Id, season.Id);

            // If doesn't exist, create new
            if (playerCupStats == null)
                playerCupStats = new PlayerCupStats(playerFixture.Player, cup, season, totalPoints, totalFouls, playerFixturesForCup.Count, mvpAwards);
            else
            {
                // Update values
                playerCupStats.UpdateStats(totalPoints, totalFouls, playerFixturesForCup.Count, mvpAwards);
            }

            // Save
            matchResultRepository.SavePlayerCupStats(playerCupStats);

            return playerCupStats;
        }

        public void UpdatePlayerCupStats(List<PlayerFixture> playerFixtures, Cup cup, Season season)
        {
            foreach (PlayerFixture pf in playerFixtures)
                UpdatePlayerCupStats(pf, cup, season);
        }

        public PlayerLeagueStats UpdatePlayerLeagueStats(PlayerFixture playerFixture, League league)
        {
            PlayerLeagueStats playerLeagueStats;
            int totalPoints = 0;
            int totalFouls = 0;
            int mvpAwards = 0;

            // Get all PlayerFixture records for specified league
            List<PlayerFixture> playerFixturesForLeague = this.statsReportingService.GetPlayerFixturesForLeagueAndPlayer(playerFixture.Player.Id, league.Id).ToList();

            // Total stats
            foreach (PlayerFixture pf in playerFixturesForLeague)
            {
                totalPoints += pf.PointsScored;
                totalFouls += pf.Fouls;
                if (pf.IsMvp == "Y")
                    mvpAwards++;
            }

            // Find existing record
            playerLeagueStats = this.statsReportingService.GetPlayerLeagueStats(playerFixture.Player.Id, league.Id);

            // Getting the play
            // If doesn't exist, create new
            if (playerLeagueStats == null)
                playerLeagueStats = new PlayerLeagueStats(playerFixture.Player, league.Season, league, totalPoints, totalFouls, playerFixturesForLeague.Count, mvpAwards);
            else
            {
                // Update values
                playerLeagueStats.UpdateStats(totalPoints, totalFouls, playerFixturesForLeague.Count, mvpAwards);
            }

            // Save
            matchResultRepository.SavePlayerLeagueStats(playerLeagueStats);

            return playerLeagueStats;
        }

        public void UpdatePlayerLeagueStats(List<PlayerFixture> playerFixtures, League league)
        {
            foreach (PlayerFixture pf in playerFixtures)
                UpdatePlayerLeagueStats(pf, league);
        }

        public PlayerCareerStats UpdatePlayerCareerStats(PlayerFixture playerFixture)
        {
            PlayerCareerStats playerCareerStats;
            int totalPoints = 0;
            int totalFouls = 0;
            int mvpAwards = 0;

            // Get all PlayerFixture records for all seasons
            List<PlayerFixture> playerFixturesForCareer = this.statsReportingService.GetPlayerFixtureStatusForAllSeasons(playerFixture.Player.Id).ToList();

            // Total stats
            foreach (PlayerFixture pf in playerFixturesForCareer)
            {
                totalPoints += pf.PointsScored;
                totalFouls += pf.Fouls;
                if (pf.IsMvp == "Y")
                    mvpAwards++;
            }

            // Find existing record
            playerCareerStats = this.statsReportingService.GetPlayerCareerStatsByPlayerId(playerFixture.Player.Id);

            // If doesn't exist, create new
            if (playerCareerStats == null)
                playerCareerStats = new PlayerCareerStats(playerFixture.Player, totalPoints, totalFouls, playerFixturesForCareer.Count, mvpAwards);
            else
            {
                // Update values
                playerCareerStats.UpdateStats(totalPoints, totalFouls, playerFixturesForCareer.Count, mvpAwards);
            }

            // Save
            matchResultRepository.SavePlayerCareerStats(playerCareerStats);

            return playerCareerStats;
        }

        public void UpdatePlayerCareerStats(List<PlayerFixture> playerFixtures)
        {
            foreach (PlayerFixture pf in playerFixtures)
                UpdatePlayerCareerStats(pf);
        }

        public void DeleteExistingPlayerFixturesThatDidNotPlay(List<PlayerFixture> playerFixtures, List<bool> hasPlayed)
        {

            // We are only interested in PlayerFixture records that were saved because a player was marked as played, then
            // subsequently they were marked as unplayed. These records should be deleted
            for (int i = 0; i < playerFixtures.Count; i++)
            {
                if (!hasPlayed[i])
                {
                    // Check if record exists. If if does, delete it
                    if (playerFixtures[i].Id > 0)
                        matchResultRepository.DeletePlayerFixture(playerFixtures[i]);
                }
            }

            //statsRepository.Save();
        }

        // This is a bit confusing. It's not really saving the match result. What's the point?
        public Fixture SaveMatchResult(Fixture fixtureToUpdate, User lastUpdatedBy, int? forfeitingTeamId = null)
        {
            if(forfeitingTeamId.HasValue && fixtureToUpdate.IsForfeit)
                fixtureToUpdate.ForfeitingTeam = fixtureToUpdate.HomeTeamLeague.Team.Id == forfeitingTeamId ? fixtureToUpdate.HomeTeamLeague.Team : fixtureToUpdate.AwayTeamLeague.Team;
            else if(fixtureToUpdate.HasPlayerStats.YesNoToBool())
            {
                fixtureToUpdate.IsForfeit = false;
                fixtureToUpdate.ForfeitingTeam.Touch();
                fixtureToUpdate.ForfeitingTeam = null;
            }

            fixtureToUpdate.IsPlayed = "Y"; // TODO Add tickbox for played?

            if (fixtureToUpdate.ResultAddedDate == null)
                fixtureToUpdate.ResultAddedDate = DateTime.Now;

            fixtureToUpdate.LastUpdated = DateTime.Now;
            fixtureToUpdate.LastUpdatedBy = lastUpdatedBy;

            fixtureService.Update(fixtureToUpdate);
            Commit();

            return fixtureToUpdate;
        }
    }
}
