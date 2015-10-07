using NUnit.Framework;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using System;


namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PlayerSeasonStatsTests
    {
        [Test]
        public void CanCreatePlayerSeasonStats()
        {
            Season season = new Season(2009, 2010);
            Player player = new Player("Phil", "Hale");
            int totalPoints = 50;
            int totalFouls = 10;
            int gamesPlayed = 4;
            int mvpAwards = 3;

            PlayerSeasonStats stats = new PlayerSeasonStats(player, season, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Season, Is.EqualTo(season));
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.TotalPoints, Is.EqualTo(totalPoints));
            Assert.That(stats.PointsPerGame, Is.EqualTo(12.5f));
            Assert.That(stats.TotalFouls, Is.EqualTo(totalFouls));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(2.5f));
            Assert.That(stats.GamesPlayed, Is.EqualTo(gamesPlayed));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }

        [Test]
        public void CanCreatePlayerSeasonStatsWithNegativeValues()
        {
            Season season = new Season(2009, 2010);
            Player player = new Player("Phil", "Hale");
            int totalPoints = -2;
            int totalFouls = -3;
            int gamesPlayed = -2;
            int mvpAwards = 0;

            PlayerSeasonStats stats = new PlayerSeasonStats(player, season, totalPoints, totalFouls, gamesPlayed, mvpAwards);

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
        public void CanCreatePlayerSeasonStatsWithZeroGamesPlayed()
        {
            Season season = new Season(2009, 2010);
            Player player = new Player("Phil", "Hale");
            int totalPoints = -2;
            int totalFouls = 2;
            int gamesPlayed = 0;
            int mvpAwards = 5;

            PlayerSeasonStats stats = new PlayerSeasonStats(player, season, totalPoints, totalFouls, gamesPlayed, mvpAwards);

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
        public void CannotCreatePlayerSeasonStatsWithNullPlayer()
        {
            new PlayerSeasonStats(null, new Season(2009, 2010), 1, 2, 3, 0);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerSeasonStatsWithNullSeason()
        {
            new PlayerSeasonStats(new Player("Phil", "Hale"), null, 1, 2, 3, 0);
        }

    }
}
