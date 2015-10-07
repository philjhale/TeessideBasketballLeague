using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using NUnit.Framework;
using NSubstitute;

namespace Basketball.Tests.Data
{
    public class FixtureRepositoryTests
    {
        private IFixtureRepository fixtureRepository;

        [SetUp]
        public void Setup()
        {
            fixtureRepository = new FixtureRepository(new TestBasketballContext());
        }

        [TearDown]
        public void TearDown()
        {
            fixtureRepository = null;
        }

        #region Get
        [Test]
        public void Get_Id_Success()
        {
            var fixture = fixtureRepository.Get(1);

            Assert.That(fixture, Is.Not.Null);
            Assert.That(fixture.HomeTeamLeague.Id, Is.EqualTo(1));
        }

        [Test]
        public void Get_All_Success()
        {
            var fixtures = fixtureRepository.Get();

            Assert.That(fixtures, Is.Not.Null);
            Assert.That(fixtures.Count(), Is.EqualTo(16));
        } 
        #endregion

        #region GetFixturesThatCanBePenalised
        [Ignore] // Can't run this test because of "This function can only be invoked from LINQ to Entities." exception
        [Test]
        public void GetFixturesThatCanBePenalised_TwoDaysTwoLateResults_Success()
        {
            List<Fixture> fixtures = fixtureRepository.GetFixturesThatCanBePenalised(1, 2).ToList();

            Assert.That(fixtures, Is.Not.Null);
           // Assert.That(fixtures[0], Is.EqualTo(2));
        }

        #endregion
    }
}
