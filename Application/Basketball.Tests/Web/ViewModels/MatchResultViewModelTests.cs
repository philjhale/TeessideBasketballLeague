using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service.Exceptions;
using Basketball.Web.ViewModels;
using NUnit.Framework;
using NSubstitute;

namespace Basketball.Tests.Web.ViewModels
{
    [TestFixture]
    public class MatchResultViewModelTests
    {
        private MatchResultViewModel model;

        [SetUp]
        public void SetUp()
        {
            model = new MatchResultViewModel();
        }

        [TearDown]
        public void TearDown()
        {
            model = null;
        }


        [Test]
        public void Constructor_InitProperties_Success()
        {
            Assert.That(model.HomeTeamScore, Is.EqualTo(0));
            Assert.That(model.AwayTeamScore, Is.EqualTo(0));
        }

        #region Mapping
        [Test]
        public void MapToModel_Success()
        {
            Fixture f = new Fixture()
            {
                Id = 10,
                HomeTeamLeague = new TeamLeague(new League(), new Team("homeTeamName", "homeTeamNameLong"), "tlHomeTeamName", "tlHomeTeamNameLong"),
                AwayTeamLeague = new TeamLeague(new League(), new Team("awayTeamName", "awayTeamNameLong"), "tlAwayTeamName", "tlAwayTeamNameLong"),
                HomeTeamScore = 99,
                AwayTeamScore = 78,
                HasPlayerStats = "Y",
                Report = "123",
                IsPenaltyAllowed = true,
            };

            model.MapToModel(f);

            Assert.That(model.FixtureId, Is.EqualTo(f.Id));
            Assert.That(model.HomeTeamName, Is.EqualTo(f.HomeTeamLeague.TeamNameLong));
            Assert.That(model.AwayTeamName, Is.EqualTo(f.AwayTeamLeague.TeamNameLong));
            Assert.That(model.HomeTeamScore, Is.EqualTo(f.HomeTeamScore));
            Assert.That(model.AwayTeamScore, Is.EqualTo(f.AwayTeamScore));
            Assert.That(model.HasPlayerStats, Is.EqualTo(true));
            Assert.That(model.Report, Is.EqualTo(f.Report));
            Assert.That(model.IsPenaltyAllowed, Is.EqualTo(true));
        }

        [Test]
        public void MapToFixture_Success()
        {
            model = new MatchResultViewModel()
            {
                FixtureId = 10,
                HomeTeamName = "Velocity",
                AwayTeamName = "Vikings",
                HomeTeamScore = 99,
                AwayTeamScore = 78,
                HasPlayerStats = true,
                Report = "123",
                IsPenaltyAllowed = true,
            };

            Fixture f = new Fixture() { HomeTeamLeague = new TeamLeague(), AwayTeamLeague = new TeamLeague() };
            f = model.MapToFixture(f);

            Assert.That(f.Id, Is.Not.EqualTo(model.FixtureId));
            Assert.That(f.HomeTeamLeague.TeamNameLong, Is.Not.EqualTo(model.HomeTeamName));
            Assert.That(f.AwayTeamLeague.TeamNameLong, Is.Not.EqualTo(model.AwayTeamName));
            Assert.That(f.HomeTeamScore, Is.EqualTo(model.HomeTeamScore));
            Assert.That(f.AwayTeamScore, Is.EqualTo(model.AwayTeamScore));
            Assert.That(f.HasPlayerStats, Is.EqualTo("Y"));
            Assert.That(f.Report, Is.EqualTo(model.Report));
            Assert.That(f.IsPenaltyAllowed, Is.EqualTo(true));
            Assert.That(f.LastUpdated, Is.Not.Null);
            Assert.That(f.LastUpdatedBy, Is.Null);
        } 

        [Test]
        public void MapToPlayerFixtureStats_ZeroExistingPlayersZeroPlayersInTeam_ReturnsEmptyList()
        {
            var result = model.MapToPlayerFixtureStats(new List<PlayerFixture>(), new List<Player>(), new TeamLeague(), new Fixture());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void MapToPlayerFixtureStats_OnePlayerHasPlayedOnePlayeHasNot_ReturnsList()
        {
            var existingPlayerFixtures = new List<PlayerFixture>();
            existingPlayerFixtures.Add(new PlayerFixture() { Player = new Player() {Id = 11 }, PointsScored = 3, Fouls = 2, IsMvp = "Y"});

            var playersInTeam = new List<Player>();
            playersInTeam.Add(new Player() { Id = 5 });
            playersInTeam.Add(new Player() { Id = 11 });

            var result = model.MapToPlayerFixtureStats(existingPlayerFixtures, playersInTeam, new TeamLeague(), new Fixture());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Count(x => x.PlayerId == 5), Is.EqualTo(1));
            Assert.That(result.Count(x => x.PlayerId == 11), Is.EqualTo(1));

            Assert.That(result.Where(x => x.PlayerId == 5).Single().PointsScored, Is.EqualTo(0));
            Assert.That(result.Where(x => x.PlayerId == 5).Single().Fouls, Is.EqualTo(0));
            Assert.That(result.Where(x => x.PlayerId == 5).Single().IsMvp, Is.False);

            Assert.That(result.Where(x => x.PlayerId == 11).Single().PointsScored, Is.EqualTo(3));
            Assert.That(result.Where(x => x.PlayerId == 11).Single().Fouls, Is.EqualTo(2));
            Assert.That(result.Where(x => x.PlayerId == 11).Single().IsMvp, Is.True);
        }

        #endregion


        #region HasHomeAndAwayMvp
        [Test]
        public void HasHomeAndAwayMvp_HomeMvpNull_ReturnsFalse()
        {
            model.HomeMvpPlayerId = null;
            model.AwayMvpPlayerId = 1;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_AwayMvpNull_ReturnsFalse()
        {
            model.HomeMvpPlayerId = 1;
            model.AwayMvpPlayerId = null;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_HomeMvpNullAwayMvpNull_ReturnsFalse()
        {
            model.HomeMvpPlayerId = null;
            model.AwayMvpPlayerId = null;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_HomeMvpZero_ReturnsFalse()
        {
            model.HomeMvpPlayerId = 0;
            model.AwayMvpPlayerId = 1;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_AwayMvpZero_ReturnsFalse()
        {
            model.HomeMvpPlayerId = 1;
            model.AwayMvpPlayerId = 0;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_HomeMvpZeroAwayMvpZero_ReturnsFalse()
        {
            model.HomeMvpPlayerId = 0;
            model.AwayMvpPlayerId = 0;

            Assert.That(model.HasHomeAndAwayMvp(), Is.False);
        }

        [Test]
        public void HasHomeAndAwayMvp_BothGreaterThanZero_ReturnsTrue()
        {
            model.HomeMvpPlayerId = 1;
            model.AwayMvpPlayerId = 1;

            Assert.That(model.HasHomeAndAwayMvp(), Is.True);
        }
        #endregion

        #region MapMvps
        [Test]
        public void MapMvps_MvpsNotSetAndHasPlayerStats_ThrowsMatchResultNoMvpException()
        {
            model.HasPlayerStats = true;

            Assert.Throws<MatchResultNoMvpException>(model.MapMvps);
        }

        [Test]
        public void MapMvps_MvpsNotSetAndHasNoPlayerStats_ThrowsMatchResultNoMvpException()
        {
            model.HasPlayerStats = false;

            Assert.DoesNotThrow(model.MapMvps);
        }

        [Test]
        public void MapMvps_MvpsSet_MvpsSet()
        {
            var homePlayerStats = new List<PlayerFixtureStats>();
            homePlayerStats.Add(new PlayerFixtureStats() { PlayerId = 1 });
            homePlayerStats.Add(new PlayerFixtureStats() { PlayerId = 2 });

            var awayPlayerStats = new List<PlayerFixtureStats>();
            awayPlayerStats.Add(new PlayerFixtureStats() { PlayerId = 3 });
            awayPlayerStats.Add(new PlayerFixtureStats() { PlayerId = 4 });

            model.HomeMvpPlayerId = 1;
            model.AwayMvpPlayerId = 4;

            model.HomePlayerStats = homePlayerStats;
            model.AwayPlayerStats = awayPlayerStats;

            model.HasPlayerStats = true;

            model.MapMvps();

            Assert.That(model.HomePlayerStats.Where(x => x.PlayerId == model.HomeMvpPlayerId).Single().IsMvp, Is.True);
            Assert.That(model.AwayPlayerStats.Where(x => x.PlayerId == model.AwayMvpPlayerId).Single().IsMvp, Is.True);
        } 
        #endregion

        #region ValidateFixture
        [Test]
        public void ValidationFixture_ScoresSameNoForfeit_ThrowsMatchResultScoresSameException()
        {
            model.HomeTeamScore = 3;
            model.AwayTeamScore = 3;
            model.IsForfeit = false;

            Assert.Throws<MatchResultScoresSameException>(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_ScoresSameForfeit_DoesNotThrow()
        {
            model.HomeTeamScore = 3;
            model.AwayTeamScore = 3;
            model.IsForfeit = true;

            Assert.DoesNotThrow(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasPlayerStatsAndHomeScoreZero_ThrowsMatchResultZeroTeamScoreException()
        {
            model.HasPlayerStats = true;
            model.HomeTeamScore = 0;
            model.AwayTeamScore = 3;

            Assert.Throws<MatchResultZeroTeamScoreException>(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasPlayerStatsAndAwayScoreZero_ThrowsMatchResultZeroTeamScoreException()
        {
            model.HasPlayerStats = true;
            model.HomeTeamScore = 3;
            model.AwayTeamScore = 0;

            Assert.Throws<MatchResultZeroTeamScoreException>(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasPlayerStatsScoresGreaterThanZero_DoesNotThrow()
        {
            model.HasPlayerStats = true;
            model.HomeTeamScore = 4;
            model.AwayTeamScore = 3;

            Assert.DoesNotThrow(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasNoPlayerStatsScoresGreaterThanZero_DoesNotThrow()
        {
            model.HasPlayerStats = false;
            model.HomeTeamScore = 4;
            model.AwayTeamScore = 3;

            Assert.DoesNotThrow(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasNoPlayerStatsHomeTeamScoreZero_DoesNotThrow()
        {
            model.HasPlayerStats = false;
            model.HomeTeamScore = 0;
            model.AwayTeamScore = 1;

            Assert.DoesNotThrow(model.ValidateFixture);
        }

        [Test]
        public void ValidationFixture_HasNoPlayerStatsAwayTeamScoreZero_DoesNotThrow()
        {
            model.HasPlayerStats = false;
            model.HomeTeamScore = 1;
            model.AwayTeamScore = 0;

            Assert.DoesNotThrow(model.ValidateFixture);
        }
        #endregion

        #region ValidatePlayerStats
        [Test]
        public void ValidatePlayerStats_HasPlayerStatsSeventeenPlayers_ThrowsMatchResultMaxPlayersExceededException()
        {
            var stats = new List<PlayerFixtureStats>();

            for (int i = 0; i < 17; i++ )
                stats.Add(new PlayerFixtureStats() { HasPlayed = true });

            stats.Add(new PlayerFixtureStats() { HasPlayed = false });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false });

            Assert.Throws<MatchResultMaxPlayersExceededException>(() => model.ValidatePlayerStats(stats, Arg.Any<int>(), true));
        }

        [Test]
        public void ValidatePlayerStats_HasPlayerStatsFourPlayers_ThrowsMatchResultLessThanFivePlayersEachTeamException()
        {
            var stats = new List<PlayerFixtureStats>();
            stats.Add(new PlayerFixtureStats() { HasPlayed = true });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false });

            Assert.Throws<MatchResultLessThanFivePlayersEachTeamException>(() => model.ValidatePlayerStats(stats, Arg.Any<int>(), true));
        }

        [Test]
        public void ValidatePlayerStats_HasPlayerStatsPlayerScoresDoNotEqualTotalScore_ThrowsMatchResultSumOfScoresDoesNotMatchTotalException()
        {
            var stats = new List<PlayerFixtureStats>();
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false, PointsScored = 1});

            Assert.Throws<MatchResultSumOfScoresDoesNotMatchTotalException>(() => model.ValidatePlayerStats(stats, 6, true));
        }

        [Test]
        public void ValidatePlayerStats_HasPlayerStatsNoMvp_ThrowsMatchResultNoMvpException()
        {
            var stats = new List<PlayerFixtureStats>();
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false, PointsScored = 1 });
            model.HomeMvpPlayerId = null;

            Assert.Throws<MatchResultNoMvpException>(() => model.ValidatePlayerStats(stats, 5, true));
        }

        [Test]
        public void ValidatePlayerStats_HasNoPlayerAndPlayersHavePlayed_ThrowsMatchResultNoStatsMoreThanZeroPlayersException()
        {
            var stats = new List<PlayerFixtureStats>();
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false, PointsScored = 1 });

            Assert.Throws<MatchResultNoStatsMoreThanZeroPlayersException>(() => model.ValidatePlayerStats(stats, 5, false));
        }

        [Test]
        public void ValidatePlayerStats_ValidDate_Success()
        {
            var stats = new List<PlayerFixtureStats>();
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = true, PointsScored = 1 });
            stats.Add(new PlayerFixtureStats() { HasPlayed = false, PointsScored = 1 });
            model.HomeMvpPlayerId = 1;
            model.AwayMvpPlayerId = 1;

            Assert.DoesNotThrow(() => model.ValidatePlayerStats(stats, 5, true));
        }
        #endregion
    }
}
