using NUnit.Framework;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using System;


namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PlayerCareerStatsTests
    {
        [Test]
        public void CanCreatePlayerCareerStats()
        {
            Player player = new Player("Phil", "Hale");
            int totalPoints = 50;
            int totalFouls = 10;
            int gamesPlayed = 4;
            int mvpAwards = 1;

            PlayerCareerStats stats = new PlayerCareerStats(player, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.TotalPoints, Is.EqualTo(totalPoints));
            Assert.That(stats.PointsPerGame, Is.EqualTo(12.5f));
            Assert.That(stats.TotalFouls, Is.EqualTo(totalFouls));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(2.5f));
            Assert.That(stats.GamesPlayed, Is.EqualTo(gamesPlayed));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }

        [Test]
        public void CanCreatePlayerCareerStatsWithNegativeValues()
        {
            Player player = new Player("Phil", "Hale");
            int totalPoints = -2;
            int totalFouls = -3;
            int gamesPlayed = -2;
            int mvpAwards = 2;

            PlayerCareerStats stats = new PlayerCareerStats(player, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.TotalPoints, Is.EqualTo(0));
            Assert.That(stats.PointsPerGame, Is.EqualTo(0));
            Assert.That(stats.TotalFouls, Is.EqualTo(0));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(0));
            Assert.That(stats.GamesPlayed, Is.EqualTo(0));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }

        [Test]
        public void CanCreatePlayerCareerStatsWithZeroGamesPlayed()
        {
            Player player = new Player("Phil", "Hale");
            int totalPoints = -2;
            int totalFouls = 2;
            int gamesPlayed = 0;
            int mvpAwards = 2;

            PlayerCareerStats stats = new PlayerCareerStats(player, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            Assert.IsNotNull(stats);
            Assert.That(stats.Player, Is.EqualTo(player));
            Assert.That(stats.TotalPoints, Is.EqualTo(0));
            Assert.That(stats.PointsPerGame, Is.EqualTo(0));
            Assert.That(stats.TotalFouls, Is.EqualTo(0));
            Assert.That(stats.FoulsPerGame, Is.EqualTo(0));
            Assert.That(stats.GamesPlayed, Is.EqualTo(0));
            Assert.That(stats.MvpAwards, Is.EqualTo(mvpAwards));
        }


        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerCareerStatsWithNullPlayer()
        {
            new PlayerCareerStats(null, 1, 2, 3, 0);
        }

    }
}
