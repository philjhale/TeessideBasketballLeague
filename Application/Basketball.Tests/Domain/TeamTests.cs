using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class TeamTests
    {
        #region Creation Tests
        [Test]
        public void CanCreateTeam()
        {
            string teamName = "Velocity"; 
            string teamNameLong = "Nunthorpe Velocity";

            Team season = new Team(teamName, teamNameLong);

            Assert.IsNotNull(season);
            Assert.That(season.TeamName, Is.EqualTo(teamName));
            Assert.That(season.TeamNameLong, Is.EqualTo(teamNameLong));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamWithNullTeamName()
        {
            new Team(null, "sdf");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamWithEmptyTeamName()
        {
            new Team("  ", "name");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamWithNullTeamNameLong()
        {
            new Team("name", null);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateTeamWithEmptyTeamNameLong()
        {
            new Team("name", "");
        }

        [Test]
        public void CanCreateTeamWithValidWebSite()
        {
            String url = "http://www.bbc.co.uk";

            Team team = new Team("Name", "Long name");
            team.WebSiteUrl = url;

            Assert.IsNotNull(team);
            Assert.That(url, Is.EqualTo(team.WebSiteUrl));
        }

        //[Test]
        //public void CannotCreateTeamInvalidWebSite()
        //{
        //    String url = "www.bbc.co.uk";

        //    Team team = new Team("Name", "Long name");
        //    team.WebSiteUrl = url;

        //    Assert.IsNotNull(team);
        //    Assert.That(url, Is.EqualTo(team.WebSiteUrl));
        //}

        #endregion
    }
}
