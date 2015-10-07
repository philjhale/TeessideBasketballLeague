using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class PenaltyTests
    {

        #region Creation Tests
        [Test]
        public void CanCreatePenalty()
        {
            Season season = new Season(2008, 2009);
            League league = new League(season, "League desc", 1, 1); 
            Team team = new Team("Velocity", "Nunthorpe Velocity");
            DateTime penaltyDate = DateTime.Now;
            string reason = "Lions vs Velocity fixture not entered on web site within 48 hours";
            int points = 1;

            Penalty penalty = new Penalty(league, team, points, reason);

            Assert.That(penalty.League.LeagueDescription, Is.EqualTo(league.LeagueDescription));
            Assert.That(penalty.Team.TeamName, Is.EqualTo(team.TeamName));
            Assert.That(penalty.Reason, Is.EqualTo(reason));
            Assert.That(penalty.PenaltyDate.Date, Is.EqualTo(penaltyDate.Date));
            Assert.That(penalty.Points, Is.EqualTo(points));
            Assert.That(penalty.Fixture, Is.Null);
        }

        [Test]
        public void CanCreatePenaltyWithFixture()
        {
            Season season = new Season(2008, 2009);
            League league = new League(season, "League desc", 1, 1);
            Team team = new Team("Velocity", "Nunthorpe Velocity");
            Fixture fixture = ControllerTestsUtil.CreateFixture(1);
            DateTime penaltyDate = DateTime.Now;
            string reason = "Lions vs Velocity fixture not entered on web site within 48 hours";
            int points = 1;

            Penalty penalty = new Penalty(league, team, points, reason, fixture);

            Assert.That(penalty.League.LeagueDescription, Is.EqualTo(league.LeagueDescription));
            Assert.That(penalty.Team.TeamName, Is.EqualTo(team.TeamName));
            Assert.That(penalty.Reason, Is.EqualTo(reason));
            Assert.That(penalty.PenaltyDate.Date, Is.EqualTo(penaltyDate.Date));
            Assert.That(penalty.Points, Is.EqualTo(points));
            Assert.That(penalty.Fixture, Is.EqualTo(fixture));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithNullLeague()
        {
            new Penalty(null, new Team(), 1, "Reason");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithNullTeam()
        {
            new Penalty(new League(), null, 1, "Reason");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithZeroPoints()
        {
            new Penalty(new League(), null, 0, "Reason");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithLessThanZeroPoints()
        {
            new Penalty(new League(), null, -1, "Reason");
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithNullReason()
        {
            new Penalty(new League(), new Team(), 1, null);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreatePenaltyWithBlankReason()
        {
            new Penalty(new League(), new Team(), 1, "  ");
        }

        #endregion
    }
}
