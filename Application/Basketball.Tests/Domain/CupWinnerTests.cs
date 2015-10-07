using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class CupWinnerTests
    {
        Season season;
        Cup cup;
        Team team;

        public CupWinnerTests() 
        {
            season = new Season(2008, 2009);
            cup = new Cup("My Cup");
            team = new Team("Velocity", "Nunthorpe Velocity");
        }

        [Test]
        public void CanCreateCupWinner()
        {
            CupWinner cupWinner = new CupWinner(season, cup, team);

            Assert.That(cupWinner.Season.Id, Is.EqualTo(season.Id));
            Assert.That(cupWinner.Cup.Id, Is.EqualTo(cup.Id));
            Assert.That(cupWinner.Team.Id, Is.EqualTo(team.Id));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupWinnerWithNullSeason()
        {
            new CupWinner(null, cup, team);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupWinnerWithNullCup()
        {
            new CupWinner(season, null, team);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupWinnerWithNullTeam()
        {
            new CupWinner(season, cup, null);
        }

        //[Test]
        //[ExpectedException(typeof(PreconditionException))]
        //public void CannotCreateCupWinnerWithNullMessage()
        //{
        //    new CupWinner("subject", null, "1");
        //}

    }
}
