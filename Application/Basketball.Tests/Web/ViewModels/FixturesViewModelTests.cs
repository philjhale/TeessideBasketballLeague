using System;
using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Web.ViewModels;
using Basketball.Web.ViewObjects;
using NUnit.Framework;

namespace Basketball.Tests.Web.ViewModels
{
    [TestFixture]
    public class FixturesViewModelTests
    {
        [Test]
        public void GetFixturesByMonth_ThreeMonthsWithSixFixtures_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            // TODO Fix this horrible stuff
            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 1, 1), Id = 1, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00"}}, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });
            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 1, 2), Id = 2, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00" } }, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });

            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 2, 3), Id = 3, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00" } }, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });
            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 2, 4), Id = 4, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00" } }, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });

            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 3, 22), Id = 5, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00" } }, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });
            fixtures.Add(new Fixture() { FixtureDate = new DateTime(2011, 3, 23), Id = 6, HomeTeamLeague = new TeamLeague() { TeamName = "TeamA", Team = new Team() { TipOffTime = "19:00" } }, AwayTeamLeague = new TeamLeague() { TeamName = "TeamB" } });

            FixturesViewModel model = new FixturesViewModel();

            List<FixturesByMonth> list = model.GetFixturesByMonth();

            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[0].Month, Is.EqualTo("January 2011"));
            Assert.That(list[1].Month, Is.EqualTo("February 2011"));
            Assert.That(list[2].Month, Is.EqualTo("March 2011"));

            Assert.That(list[0].Fixtures.Count, Is.EqualTo(2));
            Assert.That(list[1].Fixtures.Count, Is.EqualTo(2));
            Assert.That(list[2].Fixtures.Count, Is.EqualTo(2));

            Assert.That(list[0].Fixtures[0].Day, Is.EqualTo("1st"));
            Assert.That(list[0].Fixtures[1].Day, Is.EqualTo("2nd"));

            Assert.That(list[1].Fixtures[0].Day, Is.EqualTo("3rd"));
            Assert.That(list[1].Fixtures[1].Day, Is.EqualTo("4th"));

            Assert.That(list[2].Fixtures[0].Day, Is.EqualTo("22nd"));
            Assert.That(list[2].Fixtures[1].Day, Is.EqualTo("23rd"));
        }
    }
}
