using System.Collections.Generic;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Service;
using NSubstitute;
using NUnit.Framework;

namespace Basketball.Tests.Service
{
    [TestFixture]
    public class CompetitionServiceTests
    {
        CompetitionService competitionService;

        ICompetitionRepository mockCompetitionRepository;

        [SetUp]
        public void Setup()
        {
            mockCompetitionRepository = Substitute.For<ICompetitionRepository>();

            competitionService = new CompetitionService(mockCompetitionRepository);
        }

        #region GetStandingsForLeague
        [Test]
        public void GetStandingsForLeague_NoTies_TeamsReturnedWithNoChange()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            basicLeagueStands.Add(new TeamLeague() { Id = 1,  PointsLeague = 10 });
            basicLeagueStands.Add(new TeamLeague() { Id = 2,  PointsLeague = 9 });
            basicLeagueStands.Add(new TeamLeague() { Id = 3,  PointsLeague = 8 });
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            mockCompetitionRepository.DidNotReceiveWithAnyArgs().GetFixturesForTeamLeagues(null);

            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings[0].Id, Is.EqualTo(1));
            Assert.That(standings[1].Id, Is.EqualTo(2));
            Assert.That(standings[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void GetStandingsForLeague_TwoTeamTieResolvedByFixtureLeaguePointsTwoWins_TeamBAboveA()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            TeamLeague tiedTeamA = new TeamLeague() { Id = 1,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamB = new TeamLeague() { Id = 2,  PointsLeague = 10, PointsScoredDifference = 100 };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(new TeamLeague() { Id = 3,  PointsLeague = 5, PointsScoredDifference = 50 });
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 50, IsPlayed = "Y" });
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 50, AwayTeamScore = 40, IsPlayed = "Y" });
            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // B team should be top
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings[0].Id, Is.EqualTo(2));
            Assert.That(standings[1].Id, Is.EqualTo(1));
            Assert.That(standings[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void GetStandingsForLeague_TwoTeamTieResolvedByFixtureLeaguePointsTwoWinsEachButOneLossForfeit_TeamBAboveA()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            Team forfeitingTeam = new Team() { Id = 1 };
            TeamLeague tiedTeamA = new TeamLeague() { Id = 1,  PointsLeague = 10, PointsScoredDifference = 100, Team = forfeitingTeam};
            TeamLeague tiedTeamB = new TeamLeague() { Id = 2,  PointsLeague = 10, PointsScoredDifference = 100, Team = new Team() { Id = 2 },  };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(new TeamLeague() { Id = 3,  PointsLeague = 5, PointsScoredDifference = 50 });
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A wins
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B wins
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A wins
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 20, AwayTeamScore = 0, IsForfeit = true, ForfeitingTeam = forfeitingTeam, IsPlayed = "Y"}); // B wins (A forfeit)
            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // B team should be top
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(3));
            Assert.That(standings[0].Id, Is.EqualTo(tiedTeamB.Id));
            Assert.That(standings[1].Id, Is.EqualTo(tiedTeamA.Id));
            Assert.That(standings[2].Id, Is.EqualTo(3));
        }

        [Test]
        public void GetStandingsForLeague_ThreeTeamTieResolvedByFixtureLeaguePoints_Team_C_B_A()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            basicLeagueStands.Add(new TeamLeague() { Id = 1,  PointsLeague = 11, PointsScoredDifference = 50 });
            TeamLeague tiedTeamA = new TeamLeague() { Id = 2,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamB = new TeamLeague() { Id = 3,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamC = new TeamLeague() { Id = 4,  PointsLeague = 10, PointsScoredDifference = 100 };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(tiedTeamC);
            basicLeagueStands.Add(new TeamLeague() { Id = 5,  PointsLeague = 5, PointsScoredDifference = 50 });
            
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 20, AwayTeamScore = 40, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamC, HomeTeamScore = 20, AwayTeamScore = 40, IsPlayed = "Y" }); // C win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // C win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // C win

            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // C, B, A
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(5));
            Assert.That(standings[0].Id, Is.EqualTo(1));
            Assert.That(standings[1].Id, Is.EqualTo(tiedTeamC.Id));
            Assert.That(standings[2].Id, Is.EqualTo(tiedTeamB.Id));
            Assert.That(standings[3].Id, Is.EqualTo(tiedTeamA.Id));
            Assert.That(standings[4].Id, Is.EqualTo(5));
        }

        [Test]
        public void GetStandingsForLeague_ThreeTeamTieResolvedByFixtureScoreDifference_Team_C_B_A()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            basicLeagueStands.Add(new TeamLeague() { Id = 1,  PointsLeague = 11, PointsScoredDifference = 50 });
            TeamLeague tiedTeamA = new TeamLeague() { Id = 2,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamB = new TeamLeague() { Id = 3,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamC = new TeamLeague() { Id = 4,  PointsLeague = 10, PointsScoredDifference = 100 };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(tiedTeamC);
            
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 41, AwayTeamScore = 20, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamA, HomeTeamScore = 42, AwayTeamScore = 20, IsPlayed = "Y" }); // C win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // C win

            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // C, B, A
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(4));
            Assert.That(standings[0].Id, Is.EqualTo(1));
            Assert.That(standings[1].Id, Is.EqualTo(tiedTeamC.Id));
            Assert.That(standings[2].Id, Is.EqualTo(tiedTeamB.Id));
            Assert.That(standings[3].Id, Is.EqualTo(tiedTeamA.Id));
        }

        [Test]
        public void GetStandingsForLeague_ThreeTeamTieResolvedByFixtureTotalScoreDifference_Team_C_B_A()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            basicLeagueStands.Add(new TeamLeague() { Id = 1,  PointsLeague = 11, PointsScoredDifference = 50 });
            TeamLeague tiedTeamA = new TeamLeague() { Id = 2,  PointsLeague = 10, PointsScoredDifference = 100 };
            TeamLeague tiedTeamB = new TeamLeague() { Id = 3,  PointsLeague = 10, PointsScoredDifference = 101 };
            TeamLeague tiedTeamC = new TeamLeague() { Id = 4,  PointsLeague = 10, PointsScoredDifference = 102 };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(tiedTeamC);
            
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // C win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamB, HomeTeamScore = 40, AwayTeamScore = 20 , IsPlayed = "Y"}); // C win

            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // C, B, A
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(4));
            Assert.That(standings[0].Id, Is.EqualTo(1));
            Assert.That(standings[1].Id, Is.EqualTo(tiedTeamC.Id));
            Assert.That(standings[2].Id, Is.EqualTo(tiedTeamB.Id));
            Assert.That(standings[3].Id, Is.EqualTo(tiedTeamA.Id));
        }

        [Test]
        public void GetStandingsForLeague_TwoTiesOfTwoTeamsResolvedByFixtureLeaguePointsDifference_Team_D_C_B_A()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            basicLeagueStands.Add(new TeamLeague() { Id = 1,  PointsLeague = 11, PointsScoredDifference = 50 });
            TeamLeague tiedTeamA = new TeamLeague() { Id = 2,  PointsLeague = 8, PointsScoredDifference = 100 };
            TeamLeague tiedTeamB = new TeamLeague() { Id = 3,  PointsLeague = 8, PointsScoredDifference = 100 };
            TeamLeague tiedTeamC = new TeamLeague() { Id = 5,  PointsLeague = 5, PointsScoredDifference = 100 };
            TeamLeague tiedTeamD = new TeamLeague() { Id = 6,  PointsLeague = 5, PointsScoredDifference = 100 };
            basicLeagueStands.Add(tiedTeamA);
            basicLeagueStands.Add(tiedTeamB);
            basicLeagueStands.Add(new TeamLeague() { Id = 4,  PointsLeague = 4, PointsScoredDifference = 50 });
            basicLeagueStands.Add(tiedTeamC);
            basicLeagueStands.Add(tiedTeamD);
            basicLeagueStands.Add(new TeamLeague() { Id = 7,  PointsLeague = 3, PointsScoredDifference = 50 });
            
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            
            // A and B play
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamA, AwayTeamLeague = tiedTeamB, HomeTeamScore = 20, AwayTeamScore = 40, IsPlayed = "Y" }); // B win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamB, AwayTeamLeague = tiedTeamA, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // B win

            // C and D play
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamC, AwayTeamLeague = tiedTeamD, HomeTeamScore = 20, AwayTeamScore = 40, IsPlayed = "Y" }); // D win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tiedTeamD, AwayTeamLeague = tiedTeamC, HomeTeamScore = 40, AwayTeamScore = 20, IsPlayed = "Y" }); // D win

            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            // B beats A, D beats C
            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(7));
            Assert.That(standings[0].Id, Is.EqualTo(1));
            Assert.That(standings[1].Id, Is.EqualTo(tiedTeamB.Id));
            Assert.That(standings[2].Id, Is.EqualTo(tiedTeamA.Id));
            Assert.That(standings[3].Id, Is.EqualTo(4));
            Assert.That(standings[4].Id, Is.EqualTo(tiedTeamD.Id));
            Assert.That(standings[5].Id, Is.EqualTo(tiedTeamC.Id));
            Assert.That(standings[6].Id, Is.EqualTo(7));
        }

        // ThreeTeamTie fixtures league oints, score difference and total score difference
        // TieResolvedByFixturesForfeit
        // TODO Forfeits
        // TODO Win/loss the same
        // TODO Win/loss the same and fixture points difference
        // Two sets of ties

         [Test]
        public void GetStandingsForLeague_TwoTeamTieResolvedByFixtureScoreDifference_Team_Tigers_Lions()
        {
            List<TeamLeague> basicLeagueStands = new List<TeamLeague>();
            TeamLeague lions = new TeamLeague() { Id = 1,  PointsLeague = 34, PointsScoredDifference = 270 };
            TeamLeague tigers = new TeamLeague() { Id = 2,  PointsLeague = 34, PointsScoredDifference = 259 };
            basicLeagueStands.Add(lions);
            basicLeagueStands.Add(tigers);
            
            mockCompetitionRepository.GetBasicStandingsForLeague(1).ReturnsForAnyArgs(basicLeagueStands.AsQueryable());

            List<Fixture> tiedTeamFixtures = new List<Fixture>();
            
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = lions, AwayTeamLeague = tigers, HomeTeamScore = 50, AwayTeamScore = 62, IsPlayed = "Y" }); // A win
            tiedTeamFixtures.Add(new Fixture() { HomeTeamLeague = tigers, AwayTeamLeague = lions, HomeTeamScore = 60, AwayTeamScore = 70, IsPlayed = "Y" }); // B win

            mockCompetitionRepository.GetFixturesForTeamLeagues(null).ReturnsForAnyArgs(tiedTeamFixtures);

            var standings = competitionService.GetStandingsForLeague(1);
            Assert.That(standings.Count, Is.EqualTo(2));
            Assert.That(standings[0].Id, Is.EqualTo(tigers.Id));
            Assert.That(standings[1].Id, Is.EqualTo(lions.Id));
        }
        #endregion
    }
}
