using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class CupLeagueTests
    {
        [Test]
        public void CanCreateCupLeague()
        {
            Cup cup = new Cup("Handicap Cup");
            League league = new League(new Season(2008, 2009), "League 1", 1, 1);

            CupLeague cupLeague = new CupLeague(cup, league);

            Assert.That(cupLeague.Cup.CupName, Is.EqualTo(cup.CupName));
            Assert.That(cupLeague.League.Season.StartYear == 2008);
            Assert.That(cupLeague.League.Season.EndYear == 2009);
            Assert.That(cupLeague.League.LeagueDescription == league.LeagueDescription);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupLeagueWithNullCup()
        {
            new CupLeague(null, new League(new Season(2008, 2009), "League 1", 1, 1));
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateCupLeagueWithNullLeague()
        {
            new CupLeague(new Cup("Handicap Cup"), null);
        }
    }
}
