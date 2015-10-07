using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class FixtureTests
    {
        User user;
        Season season;
        Team homeTeam;
        Team awayTeam;
        League league;
        TeamLeague homeTeamLeague;
        TeamLeague awayTeamLeague;
        DateTime fixtureDate;

        [TestFixtureSetUp]
        public void Setup()
        {
            user = new User();
            season = new Season(2008, 2009);
            homeTeam = new Team("home", "homeTeam");
            homeTeam.Id = 1;
            awayTeam = new Team("away", "awayTeam");
            awayTeam.Id = 2;
            league = new League(season, "league desc", 1, 1);
            homeTeamLeague = new TeamLeague(league, homeTeam, "home", "homeTeam");
            awayTeamLeague = new TeamLeague(league, awayTeam, "away", "awayTeam");
            fixtureDate = DateTime.Today;
        }

        [Test]
        public void CanCreateFixture()
        {
            
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user);

            Assert.IsNotNull(fixture);
            Assert.That(fixture.IsPlayed, Is.EqualTo("N"));
            Assert.That(fixture.IsCancelled, Is.EqualTo("N"));
            Assert.That(fixture.HomeTeamLeague == homeTeamLeague);
            Assert.That(fixture.HomeTeamLeague.TeamName == homeTeamLeague.TeamName);
            Assert.That(fixture.HomeTeamLeague.TeamNameLong == homeTeamLeague.TeamNameLong);
            Assert.That(fixture.AwayTeamLeague == awayTeamLeague);
            Assert.That(fixture.AwayTeamLeague.TeamName == awayTeamLeague.TeamName);
            Assert.That(fixture.AwayTeamLeague.TeamNameLong == awayTeamLeague.TeamNameLong);
            Assert.That(fixture.FixtureDate == fixtureDate);
            Assert.That(fixture.HasPlayerStats, Is.EqualTo("Y"));
            Assert.That(fixture.IsPenaltyAllowed, Is.True);
            Assert.That(fixture.LastUpdated, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateFixtureWithNullHomeLeague()
        {
            new Fixture(null, awayTeamLeague, fixtureDate, user);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateFixtureWithNullAwayLeague()
        {
            new Fixture(homeTeamLeague, null, fixtureDate, user);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateFixtureWithNullLastUpdatedBy()
        {
            new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, null);
        }

        [Test]
        public void IsCancelled_IsYFixtureDateNotChanged_RemainsY()
        {
            Fixture f = new Fixture();
            DateTime myDate = DateTime.Now;
            f.FixtureDate = myDate;
            f.IsCancelled = "Y";

            f.FixtureDate = myDate;
            Assert.That(f.IsCancelled, Is.EqualTo("Y"));
        }

        [Test]
        public void IsCancelled_IsYFixtureDateChanged_SetToN()
        {
            Fixture f = new Fixture();
            DateTime myDate = DateTime.Now;
            f.FixtureDate = myDate;
            f.IsCancelled = "Y";

            f.FixtureDate = myDate.AddDays(1);
            Assert.That(f.IsCancelled, Is.EqualTo("N"));
        }

        #region IsHomeForfeit
        [Test]
        public void IsHomeForfeit_NotPlayed_ReturnsFalse()
        {
            Fixture fixture = new Fixture() { IsPlayed = "N" };
            fixture.IsForfeit = true;
            
            Assert.That(fixture.IsHomeForfeit(), Is.False);
        }

        [Test]
        public void IsHomeForfeit_IsForfeitFalse_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            
            Assert.That(fixture.IsHomeForfeit(), Is.False);
        }

        [Test]
        public void IsHomeForfeit_IsForfeitTrueHomeIdDoesNotMatch_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = awayTeamLeague.Team;
            
            Assert.That(fixture.IsHomeForfeit(), Is.False);
        }

        [Test]
        public void IsHomeForfeit_IsForfeitTrueHomeIdMatches_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = homeTeamLeague.Team;
            
            Assert.That(fixture.IsHomeForfeit(), Is.True);
        }
        #endregion

        #region IsAwayForfeit
        [Test]
        public void IsAwayForfeit_NotPlayed_ReturnsFalse()
        {
            Fixture fixture = new Fixture() { IsPlayed = "N" };
            fixture.IsForfeit = true;
            
            Assert.That(fixture.IsAwayForfeit(), Is.False);
        }

        [Test]
        public void IsAwayForfeit_IsForfeitFalse_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            
            Assert.That(fixture.IsAwayForfeit(), Is.False);
        }

        [Test]
        public void IsAwayForfeit_IsForfeitTrueAwayIdDoesNotMatch_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = homeTeamLeague.Team;
            
            Assert.That(fixture.IsAwayForfeit(), Is.False);
        }

        [Test]
        public void IsAwayForfeit_IsForfeitTrueAwayIdMatches_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = awayTeamLeague.Team;
            
            Assert.That(fixture.IsAwayForfeit(), Is.True);
        }
        #endregion

        #region IsHomeWin
        [Test]
        public void IsHomeWin_NotPlayed_ReturnsFalse()
        {
            Fixture fixture = new Fixture() { IsPlayed = "N"};
            
            Assert.That(fixture.IsHomeWin(), Is.False);
        }

        [Test]
        public void IsHomeWin_HomeForfeits_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = homeTeamLeague.Team;
            
            Assert.That(fixture.IsHomeWin(), Is.False);
        }

        [Test]
        public void IsHomeWin_AwayForfeits_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = awayTeamLeague.Team;

            Assert.That(fixture.IsHomeWin(), Is.True);
        }

        [Test]
        public void IsHomeWin_NoForfeitAwayTeamScoreGreater_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            fixture.HomeTeamScore = 10;
            fixture.AwayTeamScore = 20;
            
            Assert.That(fixture.IsHomeWin(), Is.False);
        }

        [Test]
        public void IsHomeWin_NoForfeitHomeTeamScoreGreater_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            fixture.HomeTeamScore = 20;
            fixture.AwayTeamScore = 10;
            
            Assert.That(fixture.IsHomeWin(), Is.True);
        }
        #endregion

        #region IsAwayWin
        [Test]
        public void IsAwayWin_NotPlayed_ReturnsFalse()
        {
            Fixture fixture = new Fixture() { IsPlayed = "N"};
            
            Assert.That(fixture.IsAwayWin(), Is.False);
        }
        [Test]
        public void IsAwayWin_AwayForfeits_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = awayTeamLeague.Team;
            
            Assert.That(fixture.IsAwayWin(), Is.False);
        }

        [Test]
        public void IsAwayWin_HomeForfeits_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = true;
            fixture.ForfeitingTeam = homeTeamLeague.Team;

            Assert.That(fixture.IsAwayWin(), Is.True);
        }

        [Test]
        public void IsAwayWin_NoForfeitAwayTeamScoreGreater_ReturnsTrue()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            fixture.HomeTeamScore = 10;
            fixture.AwayTeamScore = 20;
            
            Assert.That(fixture.IsAwayWin(), Is.True);
        }

        [Test]
        public void IsAwayWin_NoForfeitHomeTeamScoreGreater_ReturnsFalse()
        {
            Fixture fixture = new Fixture(homeTeamLeague, awayTeamLeague, fixtureDate, user) { IsPlayed = "Y" };
            fixture.IsForfeit = false;
            fixture.HomeTeamScore = 20;
            fixture.AwayTeamScore = 10;
            
            Assert.That(fixture.IsAwayWin(), Is.False);
        }
        #endregion

        #region IsHomeTeam
        [Test]
        public void IsHomeTeam_NullTeamLeague_ThrowsArgumentException()
        {
            Fixture f = new Fixture();
            Assert.Throws<ArgumentException>(() => f.IsHomeTeam(null));
        }

        [Test]
        public void IsHomeTeam_PassAwayTeam_False()
        {
            TeamLeague homeTeamLeague = new TeamLeague() { Id = 1 };
            TeamLeague awayTeamLeague = new TeamLeague() { Id = 2 };
            Fixture f = new Fixture() { HomeTeamLeague = homeTeamLeague, AwayTeamLeague = awayTeamLeague};

            Assert.That(f.IsHomeTeam(awayTeamLeague), Is.False);
        }

        [Test]
        public void IsHomeTeam_PassHomeTeam_True()
        {
            TeamLeague homeTeamLeague = new TeamLeague() { Id = 1 };
            TeamLeague awayTeamLeague = new TeamLeague() { Id = 2 };
            Fixture f = new Fixture() { HomeTeamLeague = homeTeamLeague, AwayTeamLeague = awayTeamLeague};

            Assert.That(f.IsHomeTeam(homeTeamLeague), Is.True);
        }
        #endregion

        #region IsAwayTeam
        [Test]
        public void IsAwayTeam_NullTeamLeague_ThrowsArgumentException()
        {
            Fixture f = new Fixture();
            Assert.Throws<ArgumentException>(() => f.IsAwayTeam(null));
        }

        [Test]
        public void IsAwayTeam_PassHomeTeam_False()
        {
            TeamLeague homeTeamLeague = new TeamLeague() { Id = 1 };
            TeamLeague awayTeamLeague = new TeamLeague() { Id = 2 };
            Fixture f = new Fixture() { HomeTeamLeague = homeTeamLeague, AwayTeamLeague = awayTeamLeague};

            Assert.That(f.IsAwayTeam(homeTeamLeague), Is.False);
        }

        [Test]
        public void IsAwayTeam_PassAwayTeam_True()
        {
            TeamLeague homeTeamLeague = new TeamLeague() { Id = 1 };
            TeamLeague awayTeamLeague = new TeamLeague() { Id = 2 };
            Fixture f = new Fixture() { HomeTeamLeague = homeTeamLeague, AwayTeamLeague = awayTeamLeague};

            Assert.That(f.IsAwayTeam(awayTeamLeague), Is.True);
        }
        #endregion
    }
}
