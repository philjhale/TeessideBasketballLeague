using NUnit.Framework;
using Basketball.Common;
using Basketball.Domain.Entities;
using System;
using Basketball.Common.Domain;

namespace Basketball.Tests.Domain
{
    [TestFixture]
    public class SeasonTests
    {
        [Test]
        public void CanCreateSeason()
        {
            int startYear = 2008; 
            int endYear = 2009;

            Season season = new Season(startYear, endYear);

            Assert.IsNotNull(season);
            Assert.That(season.StartYear, Is.EqualTo(startYear));
            Assert.That(season.EndYear, Is.EqualTo(endYear));
            Assert.AreEqual(season.StartYear.ToString().Length, 4);
            Assert.AreEqual(season.EndYear.ToString().Length, 4);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateSeasonWithLongStartYear()
        {
            new Season(20009, 2009);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateSeasonWithShortStartYear()
        {
            new Season(209, 2009);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateSeasonWithLongEndYear()
        {
            new Season(2009, 20010);
        }

        [Test]
        [ExpectedException(typeof(PreconditionException))]
        public void CannotCreateSeasonWithShortEndYear()
        {
            new Season(2009, 209);
        }

        [Test]
        public void CanGetSeasonToString()
        {
            Season season = new Season(2008, 2009);

            Assert.NotNull(season);
            Assert.That(season.ToString(), Is.EqualTo("2008/2009"));
        }

    }
}
