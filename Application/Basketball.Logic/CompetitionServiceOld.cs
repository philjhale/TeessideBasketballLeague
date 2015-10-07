using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Basketball.Common.Mapping;
using Basketball.Data;
using Basketball.Domain;
using Basketball.Data.Interfaces;
using Basketball.Service.Exceptions;
using Basketball.Service.Interfaces;
using System.Linq.Expressions;

namespace Basketball.Service
{
    public class CompetitionService : ICompetitionService
    {
        readonly ICompetitionRepository competitionRepository;
        readonly IOptionRepository optionRepository;
        private readonly IStatsService statsService; // Bit wierd. Passing in repositories and services. Which one should it be

        public CompetitionService(ICompetitionRepository competitionRepository, 
            IOptionRepository optionRepository,
            IStatsService statsService) 
        {
            this.competitionRepository = competitionRepository;
            this.optionRepository = optionRepository;
            this.statsService = statsService;
        }

        private void Commit()
        {
            competitionRepository.Commit();
        }

        #region Fixture
        public List<Fixture> GetTeamHomeFixturesForCurrentSeason(int teamId)
        {
            return competitionRepository.GetTeamHomeFixturesForCurrentSeason(teamId);
        }
        
        public List<Fixture> GetLatestMatchResults()
        {
            return competitionRepository.GetLatestMatchResults(int.Parse(optionRepository.GetByName(Option.HOME_NUM_MATCH_REPORTS)));
        }

        public List<Fixture> GetFixturesForCurrentSeasonFilter(int teamId, string isPlayed, string isCup)
        {
            return competitionRepository.GetFixturesForCurrentSeasonFilter(teamId, isPlayed, isCup);
        }

        public List<Fixture> GetFixturesForCurrentSeasonFilter()
        {
            return competitionRepository.GetFixturesForCurrentSeasonFilter(-1, "N", null);
        }
        #endregion

        #region Season DONE
        public Season GetCurrentSeason()
        {
            return competitionRepository.GetCurrentSeason();
        }

        // TODO Lookup current season
        public Season CreateNextSeason(Season currentSeason)
        {
            if (currentSeason != null && currentSeason.StartYear == DateTime.Today.Year)
                return null;
            else
                return new Season(DateTime.Today.Year, DateTime.Today.Year + 1);
        } 
        #endregion

        #region League DONE
        public List<League> GetLeaguesForSeason(int seasonId)
        {
            return competitionRepository.GetLeaguesForSeason(seasonId);
        }

        public List<League> GetLeaguesForCurrentSeason()
        {
            return competitionRepository.GetLeaguesForSeason(competitionRepository.GetCurrentSeason().Id);
        }

        public List<TeamLeague> GetStandingsForLeague(int leagueId)
        {
            return competitionRepository.GetStandingsForLeague(leagueId);
        }
        #endregion

        #region TeamLeague DONE

        // TODO Bit too much length on this method
        public TeamLeague UpdateTeamLeagueStats(int teamLeagueId)
        {
            TeamLeague newStats = competitionRepository.GetTeamLeague(teamLeagueId);
            newStats = ResetStats(newStats);

            IList<Fixture> fixtureList = competitionRepository.GetPlayedFixturesForTeamInReverseDateOrder(teamLeagueId);

            //Console.WriteLine("fixtures found: " + fixtureList.Count);

            // Loop through fixtures and add up stats
            foreach (Fixture f in fixtureList)
            {
                if (f.IsCupFixture == "N") // This may cause everything not to work
                {
                    Console.WriteLine("fixture " + f.Id);
                    newStats.GamesPlayed++;
                    // Home game
                    if (f.HomeTeamLeague.Id == teamLeagueId)
                    {
                        // Pts scored for/against
                        newStats.PointsScoredFor += (int)f.HomeTeamScore;
                        newStats.PointsScoredAgainst += (int)f.AwayTeamScore;

                        // Home win
                        if (f.HomeTeamScore > f.AwayTeamScore)
                        {
                            newStats.GamesWonTotal++;
                            newStats.GamesWonHome++;
                            newStats.PointsLeague += 3; // TODO Constant

                        }
                        // Home loss
                        else
                        {
                            newStats.GamesLostTotal++;
                            newStats.GamesLostHome++;
                            newStats.PointsLeague += 1; // TODO Constant
                        }
                    }
                    // Away team
                    else if (f.AwayTeamLeague.Id == teamLeagueId)
                    {
                        // Pts scored for/against
                        newStats.PointsScoredFor += (int)f.AwayTeamScore;
                        newStats.PointsScoredAgainst += (int)f.HomeTeamScore;

                        // Away win
                        if (f.AwayTeamScore > f.HomeTeamScore)
                        {
                            newStats.GamesWonTotal++;
                            newStats.GamesWonAway++;
                            newStats.PointsLeague += 3; // TODO Constant

                        }
                        // Away loss
                        else
                        {
                            newStats.GamesLostTotal++;
                            newStats.GamesLostAway++;
                            newStats.PointsLeague += 1; // TODO Constant
                        }
                    }
                }
            }

            // Deduct any penalty points (penalties defaults to 0)
            newStats.PointsLeague -= newStats.PointsPenalty;

            // Pts difference
            newStats.PointsScoredDifference = newStats.PointsScoredFor - newStats.PointsScoredAgainst;
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
            foreach (Fixture f in fixtureList)
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

                Console.WriteLine(streakWin);
                // Keep track of the last result
                if (lastResultWin == null)
                    lastResultWin = streakWin;

                Console.WriteLine("streak: " + streakWin);
                Console.WriteLine("lastResultWin: " + lastResultWin);

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

            return newStats;
        }

        private TeamLeague ResetStats(TeamLeague tl)
        {
            tl.PointsLeague = 0;
            tl.GamesPlayed = 0;
            tl.GamesPct = 0;
            tl.GamesWonTotal = 0;
            tl.GamesWonHome = 0;
            tl.GamesWonAway = 0;
            tl.GamesLostTotal = 0;
            tl.GamesLostHome = 0;
            tl.GamesLostAway = 0;
            tl.PointsScoredFor = 0;
            tl.PointsScoredAgainst = 0;
            tl.PointsScoredDifference = 0;
            tl.Streak = null;
            tl.PointsAgainstPerGameAvg = 0;
            tl.PointsScoredPerGameAvg = 0;

            return tl;
        }
        #endregion

        #region Team
        public List<Team> GetTeamsForCurrentSeason()
        {
            return competitionRepository.GetTeamsForCurrentSeason();
        }
        #endregion

        #region CRUD
        public Fixture GetFixture(int id)
        {
            return competitionRepository.GetFixture(id);
        }

        public List<Season> GetSeasons()
        {
            return competitionRepository.GetSeasons(null, null);
        }

        public List<Season> GetSeasons(Expression<Func<Season, bool>> filter = null,
            Func<IQueryable<Season>, IOrderedQueryable<Season>> orderBy = null)
        {
            return competitionRepository.GetSeasons(filter, orderBy);
        }

        private void UpdateFixture(Fixture fixture)
        {
            competitionRepository.UpdateFixture(fixture);
        }
        #endregion

        #region Match Results

        /// <exception cref="MatchResultScoresSameException"></exception>
        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        public void SaveMatchResult(Fixture fixtureToUpdate, 
            List<PlayerFixture> homePlayerStats, 
            List<PlayerFixture> awayPlayerStats, 
            List<bool> homeHasPlayed, 
            List<bool> awayHasPlayed, 
            int homeMvpPlayerId, 
            int awayMvpPlayerId)
        {
            ValidateMatchResultFixture(fixtureToUpdate);
			ValidatePlayerFixtures(homePlayerStats, homeHasPlayed, fixtureToUpdate.HomeTeamScore, fixtureToUpdate.HasPlayerStats.YesNoToBool(), homeMvpPlayerId);
            ValidatePlayerFixtures(awayPlayerStats, awayHasPlayed, fixtureToUpdate.AwayTeamScore, fixtureToUpdate.HasPlayerStats.YesNoToBool(), awayMvpPlayerId);

            // Assume that if the fixture is cancelled and the  result has been added, the game 
            // is no longer cancelled
            if (fixtureToUpdate.IsCancelled == "Y" /*&& model.HomeTeamScore != null*/)
                fixtureToUpdate.IsCancelled = "N";

            // Set the fixture as played
            fixtureToUpdate.IsPlayed = "Y"; // TODO Add tickbox for played?

            if (fixtureToUpdate.ResultAddedDate == null)
                fixtureToUpdate.ResultAddedDate = DateTime.Now;

            // Set MVP
            if (fixtureToUpdate.HasPlayerStats.YesNoToBool())
            {
                SetFixtureMvp(homePlayerStats, homeMvpPlayerId);
                SetFixtureMvp(awayPlayerStats, awayMvpPlayerId);
            }

            using(TransactionScope scope = new TransactionScope())
            {
                competitionRepository.UpdateFixture(fixtureToUpdate);

                // Delete existing records that have been marked as not played
                statsService.DeleteExistingPlayerFixturesThatDidNotPlay(homePlayerStats, homeHasPlayed);
                statsService.DeleteExistingPlayerFixturesThatDidNotPlay(awayPlayerStats, awayHasPlayed);

                // Remove players that did not player from the list so we only update those who played
                RemovePlayersThatDidNotPlay(homePlayerStats, homeHasPlayed);
                RemovePlayersThatDidNotPlay(awayPlayerStats, awayHasPlayed);

                // Save player fixture records. Pass whether each player has played so players that haven't played are not
                // updated. If this wasn't passed in the PlayerFixture records would be delete by the method call above then
                // inserted again by the call below

                // The logic is a bit odd here. All players, played or unplayed must be passed to the update 
                // season/league/career methods so they are always update (e.g. if a PlayerFixture record is saved then marked
                // unplayed it is deleted, but the player record must still be passed to the update stats method to recalculate
                // the stats
                statsService.InsertOrUpdatePlayerFixturesThatHavePlayed(homePlayerStats, homeHasPlayed);
                statsService.InsertOrUpdatePlayerFixturesThatHavePlayed(awayPlayerStats, awayHasPlayed);
                //statsService.Commit();

                // Update player league stats
                statsService.UpdatePlayerLeagueStats(homePlayerStats, fixtureToUpdate.HomeTeamLeague.League);
                statsService.UpdatePlayerLeagueStats(awayPlayerStats, fixtureToUpdate.HomeTeamLeague.League);

                // Update player season stats
                Season currentSeason = GetCurrentSeason();
                statsService.UpdatePlayerSeasonStats(homePlayerStats, currentSeason);
                statsService.UpdatePlayerSeasonStats(awayPlayerStats, currentSeason);

                // Update player career stats
                statsService.UpdatePlayerCareerStats(homePlayerStats);
                statsService.UpdatePlayerCareerStats(awayPlayerStats);

                // Update team stats
                UpdateTeamLeagueStats(fixtureToUpdate.HomeTeamLeague.Id);
                UpdateTeamLeagueStats(fixtureToUpdate.AwayTeamLeague.Id);

                Commit();
                //teamLeagueRepository.SaveOrUpdate(teamLeagueRepository.UpdateTeamLeagueStats(fixtureToUpdate.HomeTeamLeague.Id));
                //teamLeagueRepository.DbContext.CommitChanges();
                //teamLeagueRepository.SaveOrUpdate(teamLeagueRepository.UpdateTeamLeagueStats(fixtureToUpdate.AwayTeamLeague.Id));


                scope.Complete();
            }

        }

        public void DeleteExistingPlayerFixturesThatDidNotPlay(IList<PlayerFixture> playerFixtures, IList<bool> hasPlayed)
        {

            // We are only interested in PlayerFixture records that were saved because a player was marked as played, then
            // subsequently they were marked as unplayed. These records should be deleted
            for (int i = 0; i < playerFixtures.Count; i++)
            {
                if (!hasPlayed[i])
                {
                    // Check if record exists. If if does, delete it
                    if (playerFixtures[i].Id > 0)
                        DeletePlayerFixture(playerFixtures[i]);
                }
            }
        }

        /// <summary>
        /// Returns false if fixture is invalid
        /// 
        /// I.e. If scores are the same, home or team score is zero and is not a walkover,
        /// is a walkover and home or away score is not zero
        /// </summary>
        /// <param name="fixture"></param>
        /// <exception cref="MatchResultMaxPlayersExceededException"></exception>
        /// <exception cref="MatchResultLessThanFivePlayersEachTeamException"></exception>
        /// <exception cref="MatchResultSumOfScoresDoesNotMatchTotalException"></exception>
        /// <exception cref="MatchResultNoMvpException"></exception>
        /// <exception cref="MatchResultNoStatsMoreThanZeroPlayersException"></exception>
        /// <exception cref="MatchResultScoresSameException"></exception>
        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        private void ValidateMatchResultFixture(Fixture fixture)
        {
            if (fixture.HomeTeamScore == fixture.AwayTeamScore)
            {
                throw new MatchResultScoresSameException();
            }
            // If player stats included then home and away team scores must be greater than zero
            if (fixture.HasPlayerStats.YesNoToBool() && (fixture.HomeTeamScore <= 0 || fixture.AwayTeamScore <= 0))
            {
                throw new MatchResultZeroTeamScoreException();
            }
            //else if ((fixture.HasPlayerStats == "Y" || fixture.HasPlayerStats.ToLower() == "true") && fixture.HomeTeamScore != 0 && fixture.AwayTeamScore != 0)
            //{
            //    TempData[FormMessages.MessageTypeFailure] = FormMessages.MatchResultWalkoverOneTeamZeroScore;
            //    return false;
            //}
        }

        /// <summary>
        /// Does various checks on playerFixture records:
        /// + If players haven't played remove the associated PlayerFixture element from the collection
        /// + Only 12 players per side can play
        /// + Ensure that the match score is the same as the sum of the player scores
        /// + Must enter MVP
        /// </summary>
        /// <exception cref="MatchResultMaxPlayersExceededException"></exception>
        /// <exception cref="MatchResultLessThanFivePlayersEachTeamException"></exception>
        /// <exception cref="MatchResultSumOfScoresDoesNotMatchTotalException"></exception>
        /// <exception cref="MatchResultNoMvpException"></exception>
        /// <exception cref="MatchResultNoStatsMoreThanZeroPlayersException"></exception>
        private void ValidatePlayerFixtures(List<PlayerFixture> playerFixtures, 
            List<bool> hasPlayed, 
            int? score, 
            bool hasPlayerStats, 
            int? mvpId)
        {
            //ZeroPlayersThatDidNotPlay(playerFixtures, hasPlayed);

            if (hasPlayerStats)
            {
                // Check the number of players playing in the game
                if (hasPlayed.Count(x => x == true) > 12)
                {
					throw new MatchResultMaxPlayersExceededException();
                }
                // Check minimum of five players
                else if (hasPlayed.Count(x => x == true) < 5)
                {
					throw new MatchResultLessThanFivePlayersEachTeamException();
                }
                // Check players score total matches the fixture score
                else if (playerFixtures.Count > 0 && playerFixtures.Sum(x => x.PointsScored) != score)
                {
                    // This should never happen, but just in case...
					throw new MatchResultSumOfScoresDoesNotMatchTotalException();
                }
 
                // Check the MVP has been entered
                else if (!mvpId.HasValue || mvpId <= 0)
                {
					throw new MatchResultNoMvpException();
                }
            }
            else
            {
                // If no player stats selected check that there are no players
                if (hasPlayed.Count(x => x == true) > 0)
                {
                    throw new MatchResultNoStatsMoreThanZeroPlayersException();
                }
            }
        }
		
		/// <summary>
        /// Zeros players score and fouls from playerFixtures collection that did not play in the fixture
        /// </summary>
        /// <param name="playerFixtures"></param>
        /// <param name="hasPlayed"></param>
        private void ZeroPlayersThatDidNotPlay(List<PlayerFixture> playerFixtures, List<bool> hasPlayed)
        {
            int i = 0;
            for (i = playerFixtures.Count - 1; i >= 0; i--)
            {
                if (hasPlayed[i] == false)
                {
                    playerFixtures[i].Fouls = 0;
                    playerFixtures[i].PointsScored = 0;
                }
            }
        }

        /// <summary>
        /// Set the mvp for the match. Sets all players in the list to IsMvp = N except the actual MVP
        /// </summary>
        /// <param name="playerFixtures"></param>
        /// <param name="mvpPlayerId"></param>
        private void SetFixtureMvp(List<PlayerFixture> playerFixtures, int? mvpPlayerId)
        {
            foreach (PlayerFixture pf in playerFixtures)
            {
                if (mvpPlayerId.Value == pf.Player.Id)
                    pf.IsMvp = "Y";
                else
                    pf.IsMvp = "N";

            }
        }
		
		/// <summary>
        /// Removes players from the playerFixtures collection that did not play in the fixture
        /// </summary>
        /// <param name="playerFixtures"></param>
        /// <param name="hasPlayed"></param>
        private void RemovePlayersThatDidNotPlay(IList<PlayerFixture> playerFixtures, IList<bool> hasPlayed)
        {
            // Remove players who have not played in the fixture
            int i = 0;
            for (i = playerFixtures.Count - 1; i >= 0; i--)
            {
                if (hasPlayed[i] == false)
                {
                    playerFixtures.RemoveAt(i);
                    hasPlayed.RemoveAt(i);
                }
            }
        }
		#endregion
    }
}
