using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using NUnit.Framework;

namespace Basketball.Tests.Domain.ValueObjects
{
    [TestFixture]
    public class PlayerFixtureStatsTests
    {
        private PlayerFixtureStats playerFixtureStats;

        [SetUp]
        public void SetUp()
        {
            playerFixtureStats = new PlayerFixtureStats();
        }

        [TearDown]
        public void TearDown()
        {
            playerFixtureStats = null;
        }

        [Test]
        public void Constructor_Success()
        {
            PlayerFixture pf = new PlayerFixture()
            {
                Id = 2,
                Player = new Player() { Id = 10, Forename = "A", Surname = "B" },
                PointsScored = 3,
                Fouls = 5,
                IsMvp = "Y"
            };
            PlayerFixtureStats stats = new PlayerFixtureStats(pf, true);
            
            Assert.That(stats.HasPlayed, Is.True);
        }

        [Test]
        public void MapToModel_Success()
        {
            PlayerFixture pf = new PlayerFixture()
            {
                Id = 2,
                Player = new Player() {Id = 10, Forename = "A", Surname = "B"},
                PointsScored = 3,
                Fouls = 5,
                IsMvp = "Y"
            };

            playerFixtureStats.MapToModel(pf);

            Assert.That(playerFixtureStats.PlayerFixtureId, Is.EqualTo(pf.Id));
            Assert.That(playerFixtureStats.PlayerId, Is.EqualTo(pf.Player.Id));
            Assert.That(playerFixtureStats.Name, Is.EqualTo(pf.Player.ToString()));
            Assert.That(playerFixtureStats.PointsScored, Is.EqualTo(pf.PointsScored));
            Assert.That(playerFixtureStats.Fouls, Is.EqualTo(pf.Fouls));
            Assert.That(playerFixtureStats.IsMvp, Is.True);
        }

        [Test]
        public void MapToPlayerFixture_Success()
        {
            playerFixtureStats.PointsScored = 10;
            playerFixtureStats.Fouls = 4;
            playerFixtureStats.IsMvp = false;

            PlayerFixture pf = new PlayerFixture()
            {
                Id = 2,
                Player = new Player() { Id = 10, Forename = "A", Surname = "B" },
                PointsScored = 3,
                Fouls = 5,
                IsMvp = "Y"
            };

           pf = playerFixtureStats.MapToPlayerFixture(pf);

           Assert.That(pf.Id,                Is.EqualTo(2));
           Assert.That(pf.Player.Id,         Is.EqualTo(10));
           Assert.That(pf.Player.ToString(), Is.EqualTo("A B"));
           Assert.That(pf.PointsScored,      Is.EqualTo(playerFixtureStats.PointsScored));
           Assert.That(pf.Fouls,             Is.EqualTo(playerFixtureStats.Fouls));
           Assert.That(pf.                   IsMvp, Is.EqualTo("N"));
        }
    }
}
