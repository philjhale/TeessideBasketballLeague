using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Service;
using Basketball.Service.Extensions;
using Basketball.Service.Interfaces;

using NUnit.Framework;

namespace Basketball.Tests.Service
{
    [TestFixture]
    public class StandingsCalculationTests
    {
        #region TiesExist
        [Test]
        public void TiesExist_PointsDifferent_ReturnsFalse()
        {
            var teamLeagueList = new List<TeamLeague>();
            teamLeagueList.Add(new TeamLeague() { Id = 1, PointsLeague = 1 });
            teamLeagueList.Add(new TeamLeague() { Id = 2, PointsLeague = 2 });

            Assert.That(teamLeagueList.TiesExist(), Is.False);
        }

        [Test]
        public void TiesExist_LeaguePointsSame_ReturnsTrue()
        {
            var teamLeagueList = new List<TeamLeague>();
            teamLeagueList.Add(new TeamLeague() { Id = 1, PointsLeague = 1 });
            teamLeagueList.Add(new TeamLeague() { Id = 2, PointsLeague = 1 });
            teamLeagueList.Add(new TeamLeague() { Id = 3, PointsLeague = 2 });

            Assert.That(teamLeagueList.TiesExist(), Is.True);
        }
        #endregion

        #region GetHomeWins + GetAwayWins + GetTotalWins
        [Test]
        public void HomeAndAwayWins_TwoHomeWinsOneAwayWin_ReturnsTwoHomeWinsAndOneAwayWin()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var teamLeagueC = new TeamLeague() { Id = 3 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); // A home win
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); // A home win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, false)); // A away win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // B win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueC, true)); 

            Assert.That(teamLeagueA.GetHomeWins(fixtures), Is.EqualTo(2));
            Assert.That(teamLeagueA.GetAwayWins(fixtures), Is.EqualTo(1));
            Assert.That(teamLeagueA.GetTotalWins(fixtures), Is.EqualTo(3));
        }

        [Test]
        public void GetHomeWins_AwayForfeit_ReturnsOne()
        {
            TeamLeague teamLeague = new TeamLeague() { Id = 1, Team = new Team() { Id = 12 } };
            List<Fixture> fixtures = new List<Fixture>();
            Team forfeitingTeam = new Team() { Id = 11 };
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = teamLeague,
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = forfeitingTeam},
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            Assert.That(teamLeague.GetHomeWins(fixtures), Is.EqualTo(1));
        }

        [Test]
        public void GetHomeWins_HomeForfeit_ReturnsZero()
        {
            Team forfeitingTeam = new Team() { Id = 11 };
            TeamLeague teamLeague = new TeamLeague() { Id = 1, Team = forfeitingTeam };
            List<Fixture> fixtures = new List<Fixture>();
            
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = teamLeague,
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = new Team() { Id = 12 }},
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            Assert.That(teamLeague.GetHomeWins(fixtures), Is.EqualTo(0));
        }

        [Test]
        public void GetHomeLosses_HomeForfeitIncludeForfeits_ReturnsOne()
        {
            Team forfeitingTeam = new Team() { Id = 11 };
            TeamLeague teamLeague = new TeamLeague() { Id = 1, Team = forfeitingTeam };
            List<Fixture> fixtures = new List<Fixture>();
            
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = teamLeague,
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = new Team() { Id = 12 }},
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            Assert.That(teamLeague.GetHomeLosses(fixtures, true), Is.EqualTo(1));
        }

        [Test]
        public void GetHomeLosses_HomeForfeitExcludeForfeits_ReturnsZero()
        {
            Team forfeitingTeam = new Team() { Id = 11 };
            TeamLeague teamLeague = new TeamLeague() { Id = 1, Team = forfeitingTeam };
            List<Fixture> fixtures = new List<Fixture>();
            
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 2, Team = new Team() { Id = 12 }},
                AwayTeamLeague = teamLeague,
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            Assert.That(teamLeague.GetHomeLosses(fixtures, false), Is.EqualTo(0));
        }
        #endregion

        #region GetHomeLosses + GetAwayLosses + GetTotalLosses
        [Test]
        public void HomeAndAwayLosses_TwoHomeLossesOneAwayWin_ReturnsTwoHomeLossesAndOneAwayWin()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var teamLeagueC = new TeamLeague() { Id = 3 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, false)); // A home loss
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, false)); // A home loss
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // A away loss
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // B win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueC, true)); 

            Assert.That(teamLeagueA.GetHomeLosses(fixtures), Is.EqualTo(2));
            Assert.That(teamLeagueA.GetAwayLosses(fixtures), Is.EqualTo(2));
            Assert.That(teamLeagueA.GetTotalLosses(fixtures), Is.EqualTo(4));
        }
        #endregion

        #region GetLeaguePointsFromWins
        [Test]
        public void GetLeaguePointsFromWins_FourGamesTwoWinsHomeAndAway_ReturnsSix()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); // A win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, false)); // A win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // B win

            Assert.That(teamLeagueA.GetLeaguePointsFromWins(fixtures), Is.EqualTo(StandingsCalculations.LeaguePointsWin * 2));
        }
        #endregion

        #region GetLeaguePointsFromWins
        [Test]
        public void GetLeaguePointsFromLosses_FourGamesTwoLossesHomeAndAway_ReturnsTwo()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, false)); // A loss
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // A loss
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, false)); // B loss

            Assert.That(teamLeagueA.GetLeaguePointsFromLosses(fixtures), Is.EqualTo(StandingsCalculations.LeaguePointsLoss * 2));
        }
        #endregion

        #region GetPointsScoredFor
        [Test]
        public void GetPointsScoredFor_OneWinOneLoss_ReturnsForty()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); // A win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // A loss

            Assert.That(teamLeagueA.GetPointsScoredFor(fixtures), Is.EqualTo(40));
        }
        #endregion

        #region GetPointsScoredAgainst
        [Test]
        public void GetPointsScoredAgainst_OneWinOneLoss_ReturnsForty()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); // A win
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); // A loss

            Assert.That(teamLeagueA.GetPointsScoredAgainst(fixtures), Is.EqualTo(40));
        }
        #endregion

        #region GetGamesPlayed
        [Test]
        public void GetGamesPlayed_TwoPlayedOneUnplayed_ReturnsTwo()
        {
            var teamLeagueA = new TeamLeague() { Id = 1 };
            var teamLeagueB = new TeamLeague() { Id = 2 };
            var teamLeagueC = new TeamLeague() { Id = 3 };
            var fixtures = new List<Fixture>();
            fixtures.Add(GetFixture(teamLeagueA, teamLeagueB, true)); 
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true)); 
            fixtures.Add(GetFixture(teamLeagueC, teamLeagueB, true)); 
            fixtures.Add(GetFixture(teamLeagueB, teamLeagueA, true, false)); 

            Assert.That(teamLeagueA.GetGamesPlayed(fixtures), Is.EqualTo(2));
        }
        #endregion

        #region GetForfeitedGames
        [Test]
        public void GetForfeitedGames_OneHomeForfeitOneAwayForfeit_ReturnsTwo()
        {
            List<Fixture> fixtures = new List<Fixture>();
            Team forfeitingTeam = new Team() { Id = 11 };
            TeamLeague forfeitingTeamLeague =  new TeamLeague() { Id = 1, Team = forfeitingTeam };
            TeamLeague winningTeamLeague =  new TeamLeague() { Id = 2, Team = new Team() { Id = 12 } };
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = forfeitingTeamLeague,
                AwayTeamLeague = winningTeamLeague,
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = winningTeamLeague,
                AwayTeamLeague = forfeitingTeamLeague,
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam
            });

            Assert.That(forfeitingTeamLeague.GetGamesPlayed(fixtures), Is.EqualTo(2));
        }
        #endregion

        private Fixture GetFixture(TeamLeague homeTeamLeague, TeamLeague awayTeameLeague, bool isHomeWin, bool isPlayed = true)
        {
            var fixture = new Fixture() { HomeTeamLeague = homeTeamLeague, AwayTeamLeague = awayTeameLeague };

            fixture.IsPlayed = isPlayed ? "Y" : "N";

            if(isHomeWin)
            {
                fixture.HomeTeamScore = 30;
                fixture.AwayTeamScore = 10;
            }
            else
            {
                fixture.HomeTeamScore = 10;
                fixture.AwayTeamScore = 30;
            }

            return fixture;
        }
    }
}
