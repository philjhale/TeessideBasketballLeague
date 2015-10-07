using System;
using System.Collections.Generic;
using System.Linq;
using Basketball.Data.Interfaces;
using Basketball.Domain.Entities;
using Basketball.Domain.Entities.ValueObjects;
using Basketball.Service;
using Basketball.Service.Interfaces;

using NSubstitute;

using NUnit.Framework;

namespace Basketball.Tests.Service
{
    [TestFixture]
    public class MatchResultServiceTests
    {
        IMatchResultService matchResultService;

        IMatchResultRepository mockMatchResultRepository;
        IPlayerService mockPlayerService;
        ICompetitionService mockCompetitionService;
        IFixtureService mockFixtureService;
        IStatsReportingService mockStatsReportingService;

        [SetUp]
        public void Setup()
        {
            mockMatchResultRepository            = Substitute.For<IMatchResultRepository>();;
            mockPlayerService                    = Substitute.For<IPlayerService>();
            mockFixtureService                   = Substitute.For<IFixtureService>();
            mockCompetitionService               = Substitute.For<ICompetitionService>();
            mockStatsReportingService            = Substitute.For<IStatsReportingService>();
            
            
            PopulateData();

            matchResultService = new MatchResultService(
                mockMatchResultRepository,
                mockPlayerService,
                mockCompetitionService,
                mockFixtureService,
                mockStatsReportingService);
        }

        #region SaveMatchStats
        // Only testing loop which builds up playesr to update (method needs refactoring really)
        [Test]
        [Ignore] // This method have too many dependencies to test properly
        public void SaveMatchStats_AssemblePlayersToUpdateList_NonExistingPlayerThatPlayed_PlayerInserted()
        {
            List<PlayerFixtureStats> pfs = new List<PlayerFixtureStats>();
            pfs.Add(new PlayerFixtureStats() {PlayerFixtureId = 1, HasPlayed = true, PointsScored = 10, Fouls = 1, IsMvp = false});

            this.mockStatsReportingService.GetPlayerFixture(1).Returns(new PlayerFixture() {PointsScored = 10, Fouls = 1, IsMvp = "N"});

            matchResultService.SaveMatchStats(true, pfs, ValidTeamHomeLeague, null);
            
            this.mockMatchResultRepository.ReceivedWithAnyArgs().InsertOrUpdatePlayerFixture(null);
            this.mockMatchResultRepository.DidNotReceiveWithAnyArgs().DeletePlayerFixture(null);
        }
        #endregion

        #region UpdatePlayerLeagueStats
        [Test]
        public void UpdatePlayerLeagueStats_PlayersInListAndNoExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10}};
            League league = new League() { Id = 5, Season = new Season()};

            this.mockStatsReportingService.GetPlayerFixturesForLeagueAndPlayer(playerFixture.Player.Id, league.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerLeagueStats(playerFixture.Player.Id, league.Id).Returns((PlayerLeagueStats)null);

            var returnValue = matchResultService.UpdatePlayerLeagueStats(playerFixture, league);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(returnValue.GamesPlayed,   Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints,   Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls,    Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards,     Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame,  Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerLeagueStats(returnValue);
        }

        [Test]
        public void UpdatePlayerLeagueStats_PlayersInListAndExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10 } };
            League league = new League() { Id = 5, Season = new Season() };

            this.mockStatsReportingService.GetPlayerFixturesForLeagueAndPlayer(playerFixture.Player.Id, league.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerLeagueStats(playerFixture.Player.Id, league.Id).Returns(new PlayerLeagueStats());

            var returnValue = matchResultService.UpdatePlayerLeagueStats(playerFixture, league);

            Assert.That(returnValue, Is.Not.Null);
            Assert.That(returnValue.GamesPlayed, Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints, Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls, Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards, Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame, Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerLeagueStats(returnValue);
        }
        #endregion

        #region UpdateSeasonLeagueStats
        [Test]
        public void UpdatePlayerSeasonStats_PlayersInListAndNoExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10 } };
            Season season = new Season() { Id = 5 };

            this.mockStatsReportingService.GetPlayerFixtureStatsForSeason(playerFixture.Player.Id, season.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerSeasonStats(playerFixture.Player.Id, season.Id).Returns((PlayerSeasonStats)null);

            var returnValue = matchResultService.UpdatePlayerSeasonStats(playerFixture, season);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(returnValue.GamesPlayed,   Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints,   Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls,    Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards,     Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame,  Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerSeasonStats(returnValue);
        }

        [Test]
        public void UpdatePlayerSeasonStats_PlayersInListAndExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10 } };
            Season season = new Season() { Id = 5 };

            this.mockStatsReportingService.GetPlayerFixtureStatsForSeason(playerFixture.Player.Id, season.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerSeasonStats(playerFixture.Player.Id, season.Id).Returns(new PlayerSeasonStats());

            var returnValue = matchResultService.UpdatePlayerSeasonStats(playerFixture, season);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(returnValue.GamesPlayed,   Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints,   Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls,    Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards,     Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame,  Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerSeasonStats(returnValue);
        }
        #endregion

        #region UpdateCareerLeagueStats
        [Test]
        public void UpdatePlayerCareerStats_PlayersInListAndNoExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10 } };

            this.mockStatsReportingService.GetPlayerFixtureStatusForAllSeasons(playerFixture.Player.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerCareerStatsByPlayerId(playerFixture.Player.Id).Returns((PlayerCareerStats)null);

            var returnValue = matchResultService.UpdatePlayerCareerStats(playerFixture);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(returnValue.GamesPlayed,   Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints,   Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls,    Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards,     Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame,  Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerCareerStats(returnValue);
        }

        [Test]
        public void UpdatePlayerCareerStats_PlayersInListAndExistingRecord_Success()
        {
            PlayerFixture playerFixture = new PlayerFixture() { Player = new Player() { Id = 10 } };

            this.mockStatsReportingService.GetPlayerFixtureStatusForAllSeasons(playerFixture.Player.Id).Returns(PlayerFixturesWithStats);
            this.mockStatsReportingService.GetPlayerCareerStatsByPlayerId(playerFixture.Player.Id).Returns(new PlayerCareerStats());

            var returnValue = matchResultService.UpdatePlayerCareerStats(playerFixture);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(returnValue.GamesPlayed,   Is.EqualTo(PlayerFixturesWithStatsGamesPlayed));
            Assert.That(returnValue.TotalPoints,   Is.EqualTo(PlayerFixturesWithStatsTotalPoints));
            Assert.That(returnValue.TotalFouls,    Is.EqualTo(PlayerFixturesWithStatsTotalFouls));
            Assert.That(returnValue.MvpAwards,     Is.EqualTo(PlayerFixturesWithStatsMvpAwards));
            Assert.That(returnValue.PointsPerGame, Is.EqualTo(PlayerFixturesWithStatsPointsPerGame));
            Assert.That(returnValue.FoulsPerGame,  Is.EqualTo(PlayerFixturesWithStatsFoulsPerGame));

            this.mockMatchResultRepository.Received().SavePlayerCareerStats(returnValue);
        }
        #endregion

        #region UpdateTeamLeagueStats
        [Test]
        public void UpdateTeamLeagueStats_OneHomeLoss_Success()
        {
            List<Fixture> fixturesForOneHomeLoss = new List<Fixture>();
            fixturesForOneHomeLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 40,
                AwayTeamScore = 100,
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLoss);

            TeamLeague returnValue = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                         IsNotNull(returnValue);
            Assert.That(returnValue.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(returnValue.PointsLeague,                                           Is.EqualTo(1), "pts league incorrect");
            Assert.That(returnValue.GamesWonTotal,                                          Is.EqualTo(0), "games won incorrect");
            Assert.That(returnValue.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(returnValue.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(returnValue.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(returnValue.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(returnValue.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(returnValue.GamesPct,                                               Is.EqualTo(0.00), "games pct incorrect");
            Assert.That(returnValue.PointsScoredFor,                                        Is.EqualTo(40), "pts for incorrect");
            Assert.That(returnValue.PointsScoredAgainst,                                    Is.EqualTo(100), "pts against incorrect");
            Assert.That(returnValue.PointsScoredDifference,                                 Is.EqualTo(-60), "pts diff incorrect");
            Assert.That(returnValue.Streak,                                                 Is.EqualTo("L1"), "streak incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvg, 2),           Is.EqualTo(40f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsAgainstPerGameAvg, 2),          Is.EqualTo(100f), "pts against pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-60f), "pts scored pg diff incorrect");
            Assert.That(returnValue.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(returnValue.GamesForfeited,                                         Is.EqualTo(0));
            
            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeLossOneAwayWin_Success()
        {
            List<Fixture> fixturesForOneHomeLossOneAwayWin = new List<Fixture>();
            fixturesForOneHomeLossOneAwayWin.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 50,
                AwayTeamScore = 70,
            });
            fixturesForOneHomeLossOneAwayWin.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 70,
                AwayTeamScore = 90,
            });
            

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLossOneAwayWin);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(2), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(4), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(1), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(0.50), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(140), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(140), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(0), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W1"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(70f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(70f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(0f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeLossTwoAwayWins_Success()
        {
            List<Fixture> fixturesForOneHomeLossTwoAwayWins = new List<Fixture>();
            fixturesForOneHomeLossTwoAwayWins.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 3,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 70,
                AwayTeamScore = 75,
            });
            fixturesForOneHomeLossTwoAwayWins.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 40,
                AwayTeamScore = 50,
            });
            fixturesForOneHomeLossTwoAwayWins.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 80,
                AwayTeamScore = 90,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLossTwoAwayWins);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(3), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(7), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(2), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(2), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That((float)Math.Round(tl.GamesPct, 2),                         Is.EqualTo(0.67f), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(205), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(200), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(5), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W2"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(68.33f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(66.67f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(1.67f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeLossTwoPenaltyPoints_Success()
        {
            List<Fixture> fixturesForOneHomeLoss = new List<Fixture>();
            fixturesForOneHomeLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 40,
                AwayTeamScore = 100,
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 2));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLoss);

            TeamLeague returnValue = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                         IsNotNull(returnValue);
            Assert.That(returnValue.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(returnValue.PointsLeague,                                           Is.EqualTo(-1), "pts league incorrect");
            Assert.That(returnValue.GamesWonTotal,                                          Is.EqualTo(0), "games won incorrect");
            Assert.That(returnValue.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(returnValue.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(returnValue.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(returnValue.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(returnValue.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(returnValue.GamesPct,                                               Is.EqualTo(0.00), "games pct incorrect");
            Assert.That(returnValue.PointsScoredFor,                                        Is.EqualTo(40), "pts for incorrect");
            Assert.That(returnValue.PointsScoredAgainst,                                    Is.EqualTo(100), "pts against incorrect");
            Assert.That(returnValue.PointsScoredDifference,                                 Is.EqualTo(-60), "pts diff incorrect");
            Assert.That(returnValue.Streak,                                                 Is.EqualTo("L1"), "streak incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvg, 2),           Is.EqualTo(40f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsAgainstPerGameAvg, 2),          Is.EqualTo(100f), "pts against pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-60f), "pts scored pg diff incorrect");
            Assert.That(returnValue.PointsPenalty,                                          Is.EqualTo(2));
            Assert.That(returnValue.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeWin_Success()
        {
            List<Fixture> fixturesForOneHomeLoss = new List<Fixture>();
            fixturesForOneHomeLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 80,
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLoss);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(3), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(1.00), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(100), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(80), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(20), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W1"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(100f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(80f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(20f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeWinOneAwayLoss_Success()
        {
            List<Fixture> fixturesForOneHomeWinOneAwayLoss = new List<Fixture>();
            fixturesForOneHomeWinOneAwayLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 80,
                AwayTeamScore = 70,
            });
            fixturesForOneHomeWinOneAwayLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 65,
                AwayTeamScore = 60,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeWinOneAwayLoss);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(2), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(4), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(1), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(0.50), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(135), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(140), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(-5), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("L1"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(67.50f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(70f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-2.5f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeWinOnePenaltyPoint_Success()
        {
            List<Fixture> fixturesForOneHomeLoss = new List<Fixture>();
            fixturesForOneHomeLoss.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 40,
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 1));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeLoss);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(2), "pts league incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(1), "pts penalty incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(1.00), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(100), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(40), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(60), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W1"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(100f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(40f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(60f), "pts scored pg diff incorrect");
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeWinsOneHomeLossOneAwayLoss_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 3,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 60,
                AwayTeamScore = 55,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 40,
                AwayTeamScore = 50,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 90,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(3), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(5), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(2), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(1), "games lost away incorrect");
            Assert.That((float)Math.Round(tl.GamesPct, 2),                         Is.EqualTo(0.33f), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(195), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(200), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(-5), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("L2"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(65f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(66.67f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-1.67f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_TwoHomeLossesOneAwayLoss_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 3,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 70,
                AwayTeamScore = 65,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 40,
                AwayTeamScore = 50,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 80,
                AwayTeamScore = 90,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(3), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(3), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(0), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(3), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(2), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(1), "games lost away incorrect");
            Assert.That((float)Math.Round(tl.GamesPct, 2),                         Is.EqualTo(0.00f), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(185), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(210), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(-25), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("L3"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(61.67f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(70f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-8.33f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeWinOneAwayWin_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 60,
                AwayTeamScore = 66,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 80,
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(2), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(6), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(2), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(1), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(1.00), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(166), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(140), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(26), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W2"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(83f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(70f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(13f), "pts scored pg diff incorrect");
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_TwoHomeWinsOneAwayLoss_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 3,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 60,
                AwayTeamScore = 55,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 80,
                AwayTeamScore = 50,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 90,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(3), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(7), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(2), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(2), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(1), "games lost away incorrect");
            Assert.That((float)Math.Round(tl.GamesPct, 2),                         Is.EqualTo(0.67f), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(235), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(200), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(35), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("L1"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(78.33f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(66.67f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(11.67f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_TwoHomeWinsOneAwayWin_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 3,
                HomeTeamLeague = new TeamLeague() { Id = 2 },
                AwayTeamLeague = new TeamLeague() { Id = 1 },
                HomeTeamScore = 60,
                AwayTeamScore = 65,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 60,
                AwayTeamScore = 50,
            });
            fixtures.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1 },
                AwayTeamLeague = new TeamLeague() { Id = 2 },
                HomeTeamScore = 100,
                AwayTeamScore = 90,
            });


            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(3), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(9), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(3), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(2), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(1), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That((float)Math.Round(tl.GamesPct, 2),                         Is.EqualTo(1.00f), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(225), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(200), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(25), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo("W3"), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(75f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(66.67f), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(8.33f), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_ZeroGames_Success()
        {
            List<Fixture> fixtures = new List<Fixture>();

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixtures);

            TeamLeague tl = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                IsNotNull(tl);
            Assert.That(tl.GamesPlayed,                                            Is.EqualTo(0), "games played incorrect");
            Assert.That(tl.PointsLeague,                                           Is.EqualTo(0), "pts league incorrect");
            Assert.That(tl.GamesWonTotal,                                          Is.EqualTo(0), "games won incorrect");
            Assert.That(tl.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(tl.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(tl.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(tl.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(tl.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(tl.GamesPct,                                               Is.EqualTo(0.00), "games pct incorrect");
            Assert.That(tl.PointsScoredFor,                                        Is.EqualTo(0), "pts for incorrect");
            Assert.That(tl.PointsScoredAgainst,                                    Is.EqualTo(0), "pts against incorrect");
            Assert.That(tl.PointsScoredDifference,                                 Is.EqualTo(0), "pts diff incorrect");
            Assert.That(tl.Streak,                                                 Is.EqualTo(""), "streak incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvg, 2),           Is.EqualTo(0), "pts scored pg incorrect");
            Assert.That((float)Math.Round(tl.PointsAgainstPerGameAvg, 2),          Is.EqualTo(0), "pts against pg incorrect");
            Assert.That((float)Math.Round(tl.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(0), "pts scored pg diff incorrect");
            Assert.That(tl.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(tl.GamesForfeited,                                         Is.EqualTo(0));

            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeGameHomeForfeit_Success()
        {
            List<Fixture> fixturesForOneHomeForfeit = new List<Fixture>();
            Team forfeitingHomeTeam = new Team() { Id = 11 };
            fixturesForOneHomeForfeit.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1, Team = forfeitingHomeTeam },
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = new Team() { Id = 12 }},
                IsForfeit = true,
                ForfeitingTeam = forfeitingHomeTeam
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeForfeit);

            TeamLeague returnValue = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                         IsNotNull(returnValue);
            Assert.That(returnValue.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(returnValue.PointsLeague,                                           Is.EqualTo(0), "pts league incorrect");
            Assert.That(returnValue.GamesWonTotal,                                          Is.EqualTo(0), "games won incorrect");
            Assert.That(returnValue.GamesWonHome,                                           Is.EqualTo(0), "games won home incorrect");
            Assert.That(returnValue.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(returnValue.GamesLostTotal,                                         Is.EqualTo(1), "games lost incorrect");
            Assert.That(returnValue.GamesLostHome,                                          Is.EqualTo(1), "games lost home incorrect");
            Assert.That(returnValue.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(returnValue.GamesPct,                                               Is.EqualTo(0.00), "games pct incorrect");
            Assert.That(returnValue.PointsScoredFor,                                        Is.EqualTo(0), "pts for incorrect");
            Assert.That(returnValue.PointsScoredAgainst,                                    Is.EqualTo(20), "pts against incorrect");
            Assert.That(returnValue.PointsScoredDifference,                                 Is.EqualTo(-20), "pts diff incorrect");
            Assert.That(returnValue.Streak,                                                 Is.EqualTo("L1"), "streak incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvg, 2),           Is.EqualTo(0f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsAgainstPerGameAvg, 2),          Is.EqualTo(20f), "pts against pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(-20f), "pts scored pg diff incorrect");
            Assert.That(returnValue.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(returnValue.GamesForfeited,                                         Is.EqualTo(1));
            
            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeGameAwayForfeit_Success()
        {
            List<Fixture> fixturesForOneHomeForfeit = new List<Fixture>();
            Team forfeitingAwayTeam = new Team() { Id = 12 };
            fixturesForOneHomeForfeit.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1, Team = new Team() { Id = 11 } },
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = forfeitingAwayTeam },
                IsForfeit = true,
                ForfeitingTeam = forfeitingAwayTeam
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeForfeit);

            TeamLeague returnValue = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                         IsNotNull(returnValue);
            Assert.That(returnValue.GamesPlayed,                                            Is.EqualTo(1), "games played incorrect");
            Assert.That(returnValue.PointsLeague,                                           Is.EqualTo(3), "pts league incorrect");
            Assert.That(returnValue.GamesWonTotal,                                          Is.EqualTo(1), "games won incorrect");
            Assert.That(returnValue.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(returnValue.GamesWonAway,                                           Is.EqualTo(0), "games won away incorrect");
            Assert.That(returnValue.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(returnValue.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(returnValue.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(returnValue.GamesPct,                                               Is.EqualTo(1.00), "games pct incorrect");
            Assert.That(returnValue.PointsScoredFor,                                        Is.EqualTo(20), "pts for incorrect");
            Assert.That(returnValue.PointsScoredAgainst,                                    Is.EqualTo(0), "pts against incorrect");
            Assert.That(returnValue.PointsScoredDifference,                                 Is.EqualTo(20), "pts diff incorrect");
            Assert.That(returnValue.Streak,                                                 Is.EqualTo("W1"), "streak incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvg, 2),           Is.EqualTo(20f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsAgainstPerGameAvg, 2),          Is.EqualTo(0f), "pts against pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(20f), "pts scored pg diff incorrect");
            Assert.That(returnValue.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(returnValue.GamesForfeited,                                         Is.EqualTo(0));
            
            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void UpdateTeamLeagueStats_OneHomeGameOneAwayOtherTeamForfeitBothGames_Success()
        {
            List<Fixture> fixturesForOneHomeOneAwayBothForfeit = new List<Fixture>();
            Team forfeitingTeam1 = new Team() { Id = 12 };
            Team forfeitingTeam2 = new Team() { Id = 13 };
            fixturesForOneHomeOneAwayBothForfeit.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 1,
                HomeTeamLeague = new TeamLeague() { Id = 1, Team = new Team() { Id = 11 } },
                AwayTeamLeague = new TeamLeague() { Id = 2, Team = forfeitingTeam1 },
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam1
            });
            fixturesForOneHomeOneAwayBothForfeit.Add(new Fixture()
            {
                IsPlayed = "Y",
                IsCupFixture = false,
                Id = 2,
                HomeTeamLeague = new TeamLeague() { Id = 2, Team = forfeitingTeam2 },
                AwayTeamLeague = new TeamLeague() { Id = 1, Team = new Team() { Id = 11 } },
                IsForfeit = true,
                ForfeitingTeam = forfeitingTeam2
            });

            this.mockCompetitionService.GetTeamLeague(1).Returns(this.GetValidTeamLeague(1, 0));
            mockFixtureService.GetPlayedFixturesForTeamInReverseDateOrder(1).Returns(fixturesForOneHomeOneAwayBothForfeit);

            TeamLeague returnValue = matchResultService.UpdateTeamLeagueStats(1);

            Assert.                                                                         IsNotNull(returnValue);
            Assert.That(returnValue.GamesPlayed,                                            Is.EqualTo(2), "games played incorrect");
            Assert.That(returnValue.PointsLeague,                                           Is.EqualTo(6), "pts league incorrect");
            Assert.That(returnValue.GamesWonTotal,                                          Is.EqualTo(2), "games won incorrect");
            Assert.That(returnValue.GamesWonHome,                                           Is.EqualTo(1), "games won home incorrect");
            Assert.That(returnValue.GamesWonAway,                                           Is.EqualTo(1), "games won away incorrect");
            Assert.That(returnValue.GamesLostTotal,                                         Is.EqualTo(0), "games lost incorrect");
            Assert.That(returnValue.GamesLostHome,                                          Is.EqualTo(0), "games lost home incorrect");
            Assert.That(returnValue.GamesLostAway,                                          Is.EqualTo(0), "games lost away incorrect");
            Assert.That(returnValue.GamesPct,                                               Is.EqualTo(1.00), "games pct incorrect");
            Assert.That(returnValue.PointsScoredFor,                                        Is.EqualTo(40), "pts for incorrect");
            Assert.That(returnValue.PointsScoredAgainst,                                    Is.EqualTo(0), "pts against incorrect");
            Assert.That(returnValue.PointsScoredDifference,                                 Is.EqualTo(40), "pts diff incorrect");
            Assert.That(returnValue.Streak,                                                 Is.EqualTo("W2"), "streak incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvg, 2),           Is.EqualTo(20f), "pts scored pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsAgainstPerGameAvg, 2),          Is.EqualTo(0f), "pts against pg incorrect");
            Assert.That((float)Math.Round(returnValue.PointsScoredPerGameAvgDifference, 2), Is.EqualTo(20f), "pts scored pg diff incorrect");
            Assert.That(returnValue.PointsPenalty,                                          Is.EqualTo(0));
            Assert.That(returnValue.GamesForfeited,                                         Is.EqualTo(0));
            
            this.mockMatchResultRepository.ReceivedWithAnyArgs().UpdateTeamLeague(null);
        }

        [Test]
        public void ResetStats_Success()
        {
            TeamLeague tl = this.GetValidTeamLeague(1, 0);

            var returnValue = matchResultService.ResetStats(tl);

            Assert.That(returnValue,               Is.Not.Null);
            Assert.That(tl.GamesPlayed,            Is.EqualTo(0));
            Assert.That(tl.PointsLeague,           Is.EqualTo(0));
            Assert.That(tl.GamesWonTotal,          Is.EqualTo(0));
            Assert.That(tl.GamesWonHome,           Is.EqualTo(0));
            Assert.That(tl.GamesWonAway,           Is.EqualTo(0));
            Assert.That(tl.GamesLostTotal,         Is.EqualTo(0));
            Assert.That(tl.GamesLostHome,          Is.EqualTo(0));
            Assert.That(tl.GamesLostAway,          Is.EqualTo(0));
            Assert.That(tl.GamesPct,               Is.EqualTo(0.00));
            Assert.That(tl.PointsScoredFor,        Is.EqualTo(0));
            Assert.That(tl.PointsScoredAgainst,    Is.EqualTo(0));
            Assert.That(tl.PointsScoredDifference, Is.EqualTo(0));
            Assert.That(tl.Streak,                 Is.EqualTo(null));
            Assert.That(tl.Id,                     Is.EqualTo(1));
            Assert.That(tl.GamesForfeited,         Is.EqualTo(0));
        }
        #endregion

        #region Test Data
        private TeamLeague ValidTeamHomeLeague;
        private TeamLeague ValidAwayHomeLeague;

        private List<PlayerFixture> PlayerFixturesWithStats;
        private int PlayerFixturesWithStatsTotalPoints;
        private int PlayerFixturesWithStatsTotalFouls;
        private int PlayerFixturesWithStatsGamesPlayed;
        private int PlayerFixturesWithStatsMvpAwards;
        private decimal PlayerFixturesWithStatsPointsPerGame;
        private decimal PlayerFixturesWithStatsFoulsPerGame;

        private void PopulateData()
        {
            League league = new League() { Id = 1 };

            ValidTeamHomeLeague = new TeamLeague() { League = league };
            ValidAwayHomeLeague = new TeamLeague() { League = league };

            PlayerFixturesWithStats = new List<PlayerFixture>();
            PlayerFixturesWithStats.Add(new PlayerFixture() { PointsScored = 10, Fouls = 1, IsMvp = "N"});
            PlayerFixturesWithStats.Add(new PlayerFixture() { PointsScored = 5, Fouls = 2, IsMvp = "Y" });
            PlayerFixturesWithStatsTotalPoints = 15;
            PlayerFixturesWithStatsTotalFouls = 3;
            PlayerFixturesWithStatsGamesPlayed = PlayerFixturesWithStats.Count;
            PlayerFixturesWithStatsMvpAwards = 1;
            PlayerFixturesWithStatsPointsPerGame = (decimal)PlayerFixturesWithStatsTotalPoints / PlayerFixturesWithStatsGamesPlayed;
            PlayerFixturesWithStatsFoulsPerGame = (decimal)PlayerFixturesWithStatsTotalFouls / PlayerFixturesWithStatsGamesPlayed;
        }

        private TeamLeague GetValidTeamLeague(int id = 99, int penaltyPoints = 0)
        {
            return new TeamLeague()
            {
                Id = id,
                PointsLeague = 1,
                GamesPlayed = 2,
                GamesPct = 3,
                GamesWonTotal = 4,
                GamesWonHome = 5,
                GamesWonAway = 6,
                GamesLostTotal = 7,
                GamesLostHome = 8,
                GamesLostAway = 9,
                PointsScoredFor = 10,
                PointsScoredAgainst = 11,
                PointsScoredDifference = 12,
                Streak = "val",
                PointsAgainstPerGameAvg = 13,
                PointsScoredPerGameAvg = 14,
                PointsPenalty = penaltyPoints
            };

        }
        #endregion

        #region SaveMatchResult
        [Test]
        public void SaveMatchResult_GameNotCancelled_Success()
        {
            Fixture fixture = new Fixture() { IsCancelled = "N" };
            User user = new User() { UserName = "Phil "};

            matchResultService.SaveMatchResult(fixture, user);

            Assert.That(fixture.IsCancelled, Is.EqualTo("N"));
            Assert.That(fixture.ResultAddedDate, Is.Not.Null);
            Assert.That(fixture.LastUpdated, Is.Not.Null);
            Assert.That(fixture.LastUpdatedBy.UserName, Is.EqualTo(user.UserName));

            mockFixtureService.Received().Update(fixture);
            mockFixtureService.Received().Commit();
        }
        #endregion
    }
}
