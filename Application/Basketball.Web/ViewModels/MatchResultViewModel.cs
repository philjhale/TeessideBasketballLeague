using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Basketball.Common.Mapping;
using Basketball.Common.Resources;
using Basketball.Common.Validation;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service.Exceptions;

namespace Basketball.Web.ViewModels
{
    public class MatchResultViewModel
    {
        public int FixtureId { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        [Integer]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public int? HomeTeamScore { get; set; }

        [Integer]
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public int? AwayTeamScore { get; set; }

        public List<PlayerFixtureStats> HomePlayerStats { get; set; }
        public List<PlayerFixtureStats> AwayPlayerStats { get; set; }

        public int? HomeMvpPlayerId { get; set; }
        public int? AwayMvpPlayerId { get; set; }

        public bool HasPlayerStats { get; set; }

        public bool IsPenaltyAllowed { get; set; }

        [AllowHtml]
        public string Report { get; set; }

        public bool IsForfeit { get; set; }
        public int? ForfeitingTeamId { get; set; }

        public MatchResultViewModel()
        {
            HomeTeamScore = 0;
            AwayTeamScore = 0;
        }


        #region Mapping
        public void MapToModel(Fixture fixture)
        {
            this.FixtureId = fixture.Id;

            this.HomeTeamId = fixture.HomeTeamLeague.Team.Id;
            this.AwayTeamId = fixture.AwayTeamLeague.Team.Id;

            this.HomeTeamName = fixture.HomeTeamLeague.TeamNameLong;
            this.AwayTeamName = fixture.AwayTeamLeague.TeamNameLong;

            this.HomeTeamScore = fixture.HomeTeamScore;
            this.AwayTeamScore = fixture.AwayTeamScore;

            this.HasPlayerStats = fixture.HasPlayerStats.YesNoToBool();

            this.Report = fixture.Report;

            this.IsPenaltyAllowed = fixture.IsPenaltyAllowed;

            this.IsForfeit = fixture.IsForfeit;
        }

        public Fixture MapToFixture(Fixture fixture)
        {
            fixture.HomeTeamScore = this.HomeTeamScore;
            fixture.AwayTeamScore = this.AwayTeamScore;

            fixture.HasPlayerStats = this.HasPlayerStats.BoolToYesNo();

            fixture.Report = this.Report;

            fixture.IsPenaltyAllowed = this.IsPenaltyAllowed;

            fixture.IsForfeit = this.IsForfeit;

            return fixture;
        }

        /// <summary>
        /// Returns a complete list of PlayerFixtureStats representing an entire team for 
        /// a fixture, regardless of whether the player has played in the fixture
        /// </summary>
        /// <param name="existingPlayerFixtures"></param>
        /// <param name="allPlayersInTeam"></param>
        /// <param name="teamLeague"></param>
        /// <param name="fixture"></param>
        /// <returns></returns>
        public List<PlayerFixtureStats> MapToPlayerFixtureStats(List<PlayerFixture> existingPlayerFixtures, 
            List<Player> allPlayersInTeam,
            TeamLeague teamLeague,
            Fixture fixture) 
        {
            List<PlayerFixtureStats> playerFixturesScreen = new List<PlayerFixtureStats>();
            PlayerFixture existingPlayerFixture;


            foreach (Player player in allPlayersInTeam)
            {
                // Select PF record if exists
                existingPlayerFixture = existingPlayerFixtures.Where(x => x.Player.Id == player.Id).SingleOrDefault();

                // If a PF record exists, add it. If not, add a blank record
                if (existingPlayerFixture != null)
                    playerFixturesScreen.Add(new PlayerFixtureStats(existingPlayerFixture, true));
                else
                    playerFixturesScreen.Add(new PlayerFixtureStats(new PlayerFixture(teamLeague, fixture, player, 0, 0), false));

            }

            return playerFixturesScreen;
        }

        public void MapMvps()
        {
            if (!HasPlayerStats)
                return;

            if(!HasHomeAndAwayMvp())
                throw new MatchResultNoMvpException();

            HomePlayerStats.Where(x => x.PlayerId == HomeMvpPlayerId).Single().IsMvp = true;
            AwayPlayerStats.Where(x => x.PlayerId == AwayMvpPlayerId).Single().IsMvp = true;
        }

        #endregion


        #region Validation
        /// <exception cref="MatchResultScoresSameException"></exception>
        /// <exception cref="MatchResultZeroTeamScoreException"></exception>
        public void ValidateFixture()
        {
            if (!IsForfeit && HomeTeamScore == AwayTeamScore)
                throw new MatchResultScoresSameException();

            // If player stats included then home and away team scores must be greater than zero
            if (HasPlayerStats && (HomeTeamScore <= 0 || AwayTeamScore <= 0))
                throw new MatchResultZeroTeamScoreException();
        }

        /// <exception cref="MatchResultMaxPlayersExceededException"></exception>
        /// <exception cref="MatchResultLessThanFivePlayersEachTeamException"></exception>
        /// <exception cref="MatchResultSumOfScoresDoesNotMatchTotalException"></exception>
        /// <exception cref="MatchResultNoMvpException"></exception>
        /// <exception cref="MatchResultNoStatsMoreThanZeroPlayersException"></exception>
        public void ValidatePlayerStats()
        {
            ValidatePlayerStats(HomePlayerStats, HomeTeamScore, HasPlayerStats);
            ValidatePlayerStats(AwayPlayerStats, AwayTeamScore, HasPlayerStats);
        }

        /// <exception cref="MatchResultMaxPlayersExceededException"></exception>
        /// <exception cref="MatchResultLessThanFivePlayersEachTeamException"></exception>
        /// <exception cref="MatchResultSumOfScoresDoesNotMatchTotalException"></exception>
        /// <exception cref="MatchResultNoMvpException"></exception>
        /// <exception cref="MatchResultNoStatsMoreThanZeroPlayersException"></exception>
        public void ValidatePlayerStats(List<PlayerFixtureStats> playerFixtureStats,
            int? score,
            bool hasPlayerStats)
        {
            if (hasPlayerStats)
            {
                // Check the number of players playing in the game
                if (playerFixtureStats.Count(x => x.HasPlayed) > 16)
                    throw new MatchResultMaxPlayersExceededException();

                // Check minimum of five players
                if (playerFixtureStats.Count(x => x.HasPlayed) < 5)
                    throw new MatchResultLessThanFivePlayersEachTeamException();

                // Check players score total matches the fixture score
                if (playerFixtureStats.Count > 0 && playerFixtureStats.Where(x => x.HasPlayed).Sum(x => x.PointsScored) != score)
                    throw new MatchResultSumOfScoresDoesNotMatchTotalException(); //This should never happen, but just in case...

                // Check the MVP has been entered
                if (!HasHomeAndAwayMvp())
                    throw new MatchResultNoMvpException();
            }
            else
            {
                // If no player stats selected check that there are no players
                if (playerFixtureStats.Count(x => x.HasPlayed) > 0)
                    throw new MatchResultNoStatsMoreThanZeroPlayersException();
            }
        } 

        public bool HasHomeAndAwayMvp()
        {
            return HomeMvpPlayerId.HasValue && AwayMvpPlayerId.HasValue && HomeMvpPlayerId.Value > 0 && AwayMvpPlayerId.Value > 0;
        }
        #endregion
    }
}
