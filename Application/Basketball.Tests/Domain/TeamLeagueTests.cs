using NUnit.Framework;
using Basketball.Common.Domain;
using Basketball.Domain.Entities;
using System;


namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class TeamLeagueTests
    {
        Season season;
        Team team;
        League league;

        [TestFixtureSetUp]
        public void Setup()
        {
            season = new Season(2008, 2009);
            team = new Team("name", "name");
            league = new League(season, "league desc", 1, 1);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            season = null;
            team = null;
            league = null;
        }

        [Test]
        public void CanCreateTeamLeague()
        {
            string teamName = "Velocity";
            string teamNameLong = "Nunthorpe Velocity";
            Season season = new Season(2008, 2009);
            League league = new League(season, "Mens", 1, 1);
            Team team = new Team(teamName, teamNameLong); ; 

            TeamLeague teamLeague = new TeamLeague(league, team, teamName, teamNameLong);

            Assert.IsNotNull(teamLeague);
            Assert.That(teamLeague.TeamName, Is.EqualTo(teamName));
            Assert.That(teamLeague.TeamNameLong, Is.EqualTo(teamNameLong));
            Assert.That(teamLeague.Team, Is.EqualTo(team));
            Assert.That(teamLeague.League, Is.EqualTo(league));

            // Assert defaults
            Assert.That(teamLeague.GamesLostAway, Is.EqualTo(0));
            Assert.That(teamLeague.GamesLostHome, Is.EqualTo(0));
            Assert.That(teamLeague.GamesLostTotal, Is.EqualTo(0));
            Assert.That(teamLeague.GamesPct, Is.EqualTo(0));
            Assert.That(teamLeague.GamesPlayed, Is.EqualTo(0));
            Assert.That(teamLeague.GamesWonAway, Is.EqualTo(0));
            Assert.That(teamLeague.GamesWonHome, Is.EqualTo(0));
            Assert.That(teamLeague.GamesWonTotal, Is.EqualTo(0));
            Assert.That(teamLeague.PointsAgainstPerGameAvg, Is.EqualTo(0));
            Assert.That(teamLeague.PointsLeague, Is.EqualTo(0));
            Assert.That(teamLeague.PointsScoredAgainst, Is.EqualTo(0));
            Assert.That(teamLeague.PointsScoredDifference, Is.EqualTo(0));
            Assert.That(teamLeague.PointsScoredFor, Is.EqualTo(0));
            Assert.That(teamLeague.PointsScoredPerGameAvg, Is.EqualTo(0));
            Assert.That(teamLeague.Streak, Is.EqualTo(null));


        }
        
        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithNullLeague()
        {
            new TeamLeague(null, team, "name", "name");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithNullTeam()
        {
            new TeamLeague(league, null, "name", "name");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithNullTeamName()
        {
            new TeamLeague(league, team, null, "longname");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithBlankTeamName()
        {
            new TeamLeague(league, team, "  ", "longname");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithNullTeamNameLong()
        {
            new TeamLeague(league, team, "name", null);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamLeagueWithBlankTeamNameLong()
        {
            new TeamLeague(league, team, "name", " ");
        }

    }
}
