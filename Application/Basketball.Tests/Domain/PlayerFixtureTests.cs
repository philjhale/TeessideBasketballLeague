using NUnit.Framework;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using System;


namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PlayerFixtureTests
    {
        private User user;
        private Season season;
        private Team homeTeam;
        private Team awayTeam;
        private League league;
        private TeamLeague teamLeagueHome;
        private TeamLeague teamLeagueAway;
        private Player player;
        private DateTime fixtureDate;
        private Fixture fixture;

        [TestFixtureSetUp]
        public void Setup()
        {
            user           = new User();
            season         = new Season(2008, 2009);
            homeTeam       = new Team("home", "homeTeam");
            awayTeam       = new Team("away", "awayTeam");
            league         = new League(season, "league desc", 1, 1);
            teamLeagueHome = new TeamLeague(league, homeTeam, "home", "homeTeam");
            teamLeagueAway = new TeamLeague(league, awayTeam, "away", "awayTeam");
            player         = new Player("Phil", "Hale");
            fixtureDate    = DateTime.Today;
            fixture        = new Fixture(teamLeagueHome, teamLeagueAway, fixtureDate, user);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            season         = null;
            homeTeam       = null;
            homeTeam       = null;
            league         = null;
            teamLeagueHome = null;
            teamLeagueAway = null;
            fixture        = null;
        }

        [Test]
        public void CanCreatePlayerFixture()
        {
            PlayerFixture playerFixture = new PlayerFixture(teamLeagueHome, fixture, player, 15, 3);

            Assert.IsNotNull(playerFixture);
            Assert.That(playerFixture.TeamLeague, Is.EqualTo(teamLeagueHome));
            Assert.That(playerFixture.Fixture, Is.EqualTo(fixture));
            Assert.That(playerFixture.Player, Is.EqualTo(player));
            Assert.That(playerFixture.PointsScored, Is.EqualTo(15));
            Assert.That(playerFixture.Fouls, Is.EqualTo(3));
            Assert.That(playerFixture.IsMvp, Is.EqualTo("N"));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerFixtureWithNullTeamLeague()
        {
            new PlayerFixture(null, fixture, player, 1, 1);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerFixtureWithNullFixture()
        {
            new PlayerFixture(teamLeagueHome, null, player, 1, 1);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerFixtureWithNullPlayer()
        {
            new PlayerFixture(teamLeagueHome, fixture, null, 1, 1);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerFixtureWithMinusPoints()
        {
            new PlayerFixture(teamLeagueHome, fixture, null, -1, 1);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePlayerFixtureWithMinusFouls()
        {
            new PlayerFixture(teamLeagueHome, fixture, null, 1, -1);
        }
    }
}
