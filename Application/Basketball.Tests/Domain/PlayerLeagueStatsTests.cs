using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PlayerLeagueStatsTests
    {
        Season season = null;
        Player player = null;
        League league = null;

        [TestFixtureSetUp]
        public void Init()
        {
            season = new Season(2009, 2010);
            player = new Player("Phil", "Hale");
            league = new League(season, "Mens", 1, 1);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            season = null;
            player = null;
            league = null;
        }

        [Test]
        public void CanCreatePlayerLeagueStats()
        {
            
            int totalPoints = 50;
            int totalFouls = 10;
            int gamesPlayed = 4;
            int mvpAwards = 10;

            PlayerLeagueStats stats = new PlayerLeagueStats(player, season, league, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Season, Is.EqualTo(season));
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.League, Is.EqualTo(league));
            Assert.That(stats.TotalPoints, Is.EqualTo(totalPoints));
            Assert.That(stats.PointsPerGame, Is.EqualTo(12.5f));
            Assert.That(stats.TotalFouls, Is.EqualTo(totalFouls));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(2.5f));
            Assert.That(stats.GamesPlayed, Is.EqualTo(gamesPlayed));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }

        [Test]
        public void CanCreatePlayerLeagueStatsWithNegativeValues()
        {
            int totalPoints = -2;
            int totalFouls = -3;
            int gamesPlayed = -2;
            int mvpAwards = 0;

            PlayerLeagueStats stats = new PlayerLeagueStats(player, season, league, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Season, Is.EqualTo(season));
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.TotalPoints, Is.EqualTo(0));
            Assert.That(stats.PointsPerGame, Is.EqualTo(0));
            Assert.That(stats.TotalFouls, Is.EqualTo(0));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(0));
            Assert.That(stats.GamesPlayed, Is.EqualTo(0));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }

        [Test]
        public void CanCreatePlayerLeagueStatsWithZeroGamesPlayed()
        {
            int totalPoints = -2;
            int totalFouls = 2;
            int gamesPlayed = 0;
            int mvpAwards = 2;

            PlayerLeagueStats stats = new PlayerLeagueStats(player, season, league, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Season, Is.EqualTo(season));
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.GamesPlayed, Is.EqualTo(0));
            Assert.That(stats.TotalPoints, Is.EqualTo(0));
            Assert.That(stats.PointsPerGame, Is.EqualTo(0));
            Assert.That(stats.TotalFouls, Is.EqualTo(0));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(0));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));     
        }


        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerLeagueStatsWithNullPlayer()
        {
            new PlayerLeagueStats(null, season, league, 1, 2, 3, 0);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerLeagueStatsWithNullSeason()
        {
            new PlayerLeagueStats(player, null, league, 1, 2, 3, 0);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerLeagueStatsWithNullLeague()
        {
            new PlayerLeagueStats(player, season, null, 1, 2, 3, 0);
        }
    }
}
