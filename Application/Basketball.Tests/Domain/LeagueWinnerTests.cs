using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class LeagueWinnerTests
    {
        Season season;
        League league;
        Team team;

        public LeagueWinnerTests() 
        {
            season = new Season(2008, 2009);
            league = new League(season, "My League", 1, 1);
            team = new Team("Velocity", "Nunthorpe Velocity");
        }

        [Test]
        public void CanCreateLeagueWinner()
        {
            LeagueWinner cupWinner = new LeagueWinner(league, team);

            Assert.That(cupWinner.League.Id, Is.EqualTo(league.Id));
            Assert.That(cupWinner.Team.Id, Is.EqualTo(team.Id));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateLeagueWinnerWithNullLeague()
        {
            new LeagueWinner(null, team);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateLeagueWinnerWithNullTeam()
        {
            new LeagueWinner(league, null);
        }


    }
}
