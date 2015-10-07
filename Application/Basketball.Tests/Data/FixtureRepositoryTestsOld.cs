using NUnit.Framework;
using Basketball.Domain;
using System.Collections.Generic;
using Basketball.Data;
using System;
using Basketball.Data.Interfaces;
using Rhino.Mocks;
using System.Linq;
using System.Data.Entity;

namespace Basketball.Tests.Data
{
    // TODO?
    [TestFixture]
    [Category("DB Tests")]
    public class FixtureRepositoryTests// : BasketballRepositoryTestsBase
    {
        private IFixtureRepository fixtureRepository;
        List<Fixture> fixtures;

        #region Setup
        [SetUp]
        public void Startup()
        {
            fixtureRepository = new FixtureRepository(new TestBasketballContext());
        }

        [TearDown]
        public void TearDown()
        {
            fixtureRepository = null;
            fixtures = null;
        }
        #endregion

        #region Tests

        [Test]
        public void CanGetLatestMatchResults()
        {
            fixtures = fixtureRepository.GetLatestMatchResults(2);

            Assert.That(fixtures, Is.Not.Null);
            Assert.That(fixtures.Count, Is.EqualTo(2));
            Assert.That(fixtures[0].HomeTeamLeague.Team.TeamName, Is.EqualTo("Velocity"));
        }

        //[Test]
        //public void CanGetLateFixtures()
        //{
        //    IList<Fixture> list = fixtureRepository.GetFixturesWithLateResult(2, 48);

        //    Assert.That(list, Is.Not.Null);
        //    Assert.That(list.Count, Is.EqualTo(14));
        //}

        //[Test]
        //public void CanGetLateFixturesOneResultAddedInTime()
        //{
        //    // Fixture date 01/03/2009
        //    AddResult(1, 100, 80, 1, 3, 2009, 21, 0);
        //    IList<Fixture> list = fixtureRepository.GetFixturesWithLateResult(2, 48);

        //    Assert.That(list, Is.Not.Null);
        //    Assert.That(list.Count, Is.EqualTo(13));
        //}

        //[Test]
        //public void CanGetLateFixturesTwoResultsAddedInTime()
        //{
        //    AddResult(1, 100, 80, 3, 3, 2009, 23, 59); //01/03/2009
        //    AddResult(2, 100, 80, 11, 3, 2009, 18, 5); // 10/03/2009
        //    IList<Fixture> list = fixtureRepository.GetFixturesWithLateResult(2, 48);

        //    Assert.That(list, Is.Not.Null);
        //    Assert.That(list.Count, Is.EqualTo(12));
        //}

        //[Test]
        //public void CanGetLateFixturesOneResultAddedLate()
        //{
        //    AddResult(1, 100, 80, 4, 3, 2009, 0, 1); //01/03/2009

        //    //AddResult(2, 100, 80, 2009, 10, 3, 18, 5); // 10/03/2009
        //    IList<Fixture> list = fixtureRepository.GetFixturesWithLateResult(2, 48);

        //    Assert.That(list, Is.Not.Null);
        //    Assert.That(list.Count, Is.EqualTo(14));
        //}

        //[Test]
        //public void CanGetLateFixturesTwoResultsAddedLate()
        //{
        //    AddResult(1, 100, 80, 7, 3, 2009, 18, 30); //01/03/2009
        //    AddResult(2, 100, 80, 13, 3, 2008, 21, 5); // 10/03/2009
        //    IList<Fixture> list = fixtureRepository.GetFixturesWithLateResult(2, 48);

        //    Assert.That(list, Is.Not.Null);
        //    Assert.That(list.Count, Is.EqualTo(14));
        //}
        #endregion
    }
}
