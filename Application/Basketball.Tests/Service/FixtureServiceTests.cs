using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service;
using Basketball.Service.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Basketball.Tests.Service
{
    [TestFixture]
    public class FixtureServiceTests
    {
        private IFixtureService fixtureService;

        private IFixtureRepository mockFixtureRepository;
        private ICompetitionService mockCompetitionService;
        private IOptionService mockOptionService;

        [SetUp]
        public void SetUp()
        {
            mockFixtureRepository = Substitute.For<IFixtureRepository>();
            mockCompetitionService = Substitute.For<ICompetitionService>();
            mockOptionService = Substitute.For<IOptionService>();

            fixtureService = new FixtureService(
                mockFixtureRepository,
                mockCompetitionService,
                mockOptionService);
        }

        [TearDown]
        public void TearDown()
        {
            fixtureService = null;
        }


   


        #region SaveForfeitedMatchResult
        // TODO
        #endregion
    }
}
