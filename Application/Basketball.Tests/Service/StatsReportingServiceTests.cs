using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service;
using Basketball.Service.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Basketball.Tests.Service
{
    [TestFixture]
    class StatsReportingServiceTests
    {
        IStatsReportingService statsService;

        IStatsReportingRepository mockStatsReportingRepository;
        ICompetitionRepository mockCompetitionRepository;
        IOptionService mockOptionService;
        IFixtureService mockFixtureService;

        [SetUp]
        public void Setup()
        {
            this.mockStatsReportingRepository       = Substitute.For<IStatsReportingRepository>();
            mockCompetitionRepository = Substitute.For<ICompetitionRepository>();
            mockOptionService         = Substitute.For<IOptionService>();
            mockFixtureService        = Substitute.For<IFixtureService>();

            //PopulateData();

            statsService = new StatsReportingService(
                this.mockStatsReportingRepository,
                mockCompetitionRepository,
                mockOptionService,
                mockFixtureService);
        }

        [TearDown]
        public void TearDown()
        {
            statsService = null;
        }
    }
}
