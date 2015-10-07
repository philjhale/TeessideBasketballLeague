using NUnit.Framework;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class LeagueTests
    {
        [Test]
        public void CanCreateLeague()
        {
            Season season = new Season(2008, 2009); ; 
            string leagueDescription = "Mens Division";
            int divisionNo = 1;
            int displayOrder = 2;

            League league = new League(season, leagueDescription, divisionNo, displayOrder);

            Assert.IsNotNull(league);
            Assert.That(league.Season.Id == season.Id);
            Assert.That(league.LeagueDescription == leagueDescription);
            Assert.That(league.DivisionNo == divisionNo);
            Assert.That(league.DisplayOrder == displayOrder);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateLeagueWithNullSeason()
        {
            new League(null, "desc", 1, 1);
        }

        [Test]
        public void LeagueNameWithDivisionValid()
        {
            Season season = new Season(2008, 2009); ;
            string leagueDescription = "Mens";
            int divisionNo = 1;
            int displayOrder = 2;

            League league = new League(season, leagueDescription, divisionNo, displayOrder);

            Assert.IsNotNull(league);
            Assert.That(league.ToString() == "Mens Division 1");
        }

        [Test]
        public void BlankLeagueNameWithDivisionValid()
        {
            Season season = new Season(2008, 2009); ;
            string leagueDescription = " ";
            int divisionNo = 1;
            int displayOrder = 2;

            League league = new League(season, leagueDescription, divisionNo, displayOrder);

            Assert.IsNotNull(league);
            Assert.That(league.ToString(), Is.EqualTo("Division 1"));
        }

        //[Test]
        //public void LeagueNameWithoutDivisionValid()
        //{
        //    Season season = new Season(2008, 2009); ;
        //    string leagueDescription = "Mens";
        //    int divisionNo = null;
        //    int displayOrder = 2;

        //    League league = new League(season, leagueDescription, divisionNo, displayOrder);

        //    Assert.IsNotNull(league);
        //    Assert.That(league.ToString() == "Mens");
        //}
    }
}
