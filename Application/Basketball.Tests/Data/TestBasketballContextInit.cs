using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;
using Basketball.Data.Interfaces;
using Basketball.Data;

namespace Basketball.Tests.Data
{
    public partial class TestBasketballContext
    {
        private User myUser;

        private Season currentSeason;
        private Season oldSeason;

        private League mensDiv1CurrentLeague;
        private League mensDiv2CurrentLeague;
        private League mensOldDiv1League;

        private Team velocity;
        private Team heatA;
        private Team easingwold;
        private Team heatB;
        private Team uca;
        private Team explosion;

        private TeamLeague velocityDiv1TeamLeague;
        private TeamLeague heatDiv1TeamLeague;
        private TeamLeague easingwoldDiv1TeamLeague;
        private TeamLeague heatbDiv2TeamLeague;
        private TeamLeague ucaDiv2TeamLeague;
        private TeamLeague explosionDiv2TeamLeague;
        private TeamLeague velocityOldDiv1TeamLeague;
        private TeamLeague heatOldDiv1TeamLeague;

        public TestBasketballContext()
        {
            Users = new TestDbSet<User>()
            {
                AddUser(1, out myUser)
            };
            Seasons = new TestDbSet<Season>
	        {
                AddSeason(1, 2010, 2011, out currentSeason),
				AddSeason(2, 2011, 2012, out oldSeason)
	        };
            Leagues = new TestDbSet<League>
            {
                AddLeague(1, currentSeason, "Mens", out mensDiv1CurrentLeague),
                AddLeague(2, currentSeason, "Mens 2", out mensDiv2CurrentLeague),
                AddLeague(3, oldSeason, "Mens", out mensOldDiv1League)
            };
            Teams = new TestDbSet<Team>
            {
                // Teams Div 1
                AddTeam(1, "Velocity", "Nunthorpe Velocity", out velocity),
                AddTeam(2, "Heat A", "Hartlepool Heat A", out heatA),
                AddTeam(3, "Easingwold", "Easingwold Vikings", out easingwold),
                // Teams Div 2
                AddTeam(4, "Heat B", "Hartlepool Heat B", out heatB),
                AddTeam(5, "Team UCA", "Team UCA", out uca),
                AddTeam(6, "Egglescliffe Explosion", "Explosion", out explosion)
            };
            TeamLeagues = new TestDbSet<TeamLeague>
            {
                // TeamLeagues Div1
                AddTeamLeague(1, velocity, mensDiv1CurrentLeague, out velocityDiv1TeamLeague), // 1
                AddTeamLeague(2, heatA, mensDiv1CurrentLeague, out heatDiv1TeamLeague),
                AddTeamLeague(3, easingwold, mensDiv1CurrentLeague, out easingwoldDiv1TeamLeague),
                // TeamLeagues Div2
                AddTeamLeague(4, heatB, mensDiv2CurrentLeague, out heatbDiv2TeamLeague), // 4
                AddTeamLeague(5, uca, mensDiv2CurrentLeague, out ucaDiv2TeamLeague),
                AddTeamLeague(6, explosion, mensDiv2CurrentLeague, out explosionDiv2TeamLeague),
                // Old TeamLeagues Div1
                AddTeamLeague(7, velocity, mensOldDiv1League, out velocityOldDiv1TeamLeague),
                AddTeamLeague(8, heatA, mensOldDiv1League, out heatOldDiv1TeamLeague)
            };
            Fixtures = new TestDbSet<Fixture>
            {
                // Fixtures Velocity
                this.AddPlayedFixture(1, this.velocityDiv1TeamLeague, this.heatDiv1TeamLeague, "01/03/2009", 70, 60, myUser), // 1
                this.AddPlayedFixture(2, this.velocityDiv1TeamLeague, this.easingwoldDiv1TeamLeague, "10/03/2009", 80, 75, myUser), // 2
                this.AddFixture(3, this.heatDiv1TeamLeague, this.velocityDiv1TeamLeague, "11/03/2009", "13/03/2009 23:59:59", myUser), // 3 Result just in time
                this.AddFixture(4, this.easingwoldDiv1TeamLeague, this.velocityDiv1TeamLeague, "15/03/2009", "16/03/2099", myUser), // 4 // Result day after
                // Fixtures Heat
                this.AddFixture(5, this.heatDiv1TeamLeague, this.velocityDiv1TeamLeague, "11/03/2009", "13/03/2009", myUser), // 5 - 8 Result two days after
                this.AddFixture(6, this.heatDiv1TeamLeague, this.easingwoldDiv1TeamLeague, "06/03/2009", "06/03/2009", myUser), // Result same day
                this.AddFixture(7, this.velocityDiv1TeamLeague, this.heatDiv1TeamLeague, "18/03/2009", "21/03/2009 00:00:01", myUser), // Result late
                this.AddFixture(8, this.easingwoldDiv1TeamLeague, this.heatDiv1TeamLeague, "01/03/2009", "04/03/2009", myUser), // Result late
                // Fixtures Vikings
                this.AddFixture(9, this.easingwoldDiv1TeamLeague, this.velocityDiv1TeamLeague, "05/03/2009", "10/03/2009", myUser, false), // 9 - 11 Result late but no penalty
                this.AddFixture(10, this.easingwoldDiv1TeamLeague, this.heatDiv1TeamLeague, "06/03/2009", "06/03/2009", myUser),
                this.AddFixture(11, this.velocityDiv1TeamLeague, this.easingwoldDiv1TeamLeague, "18/03/2009", "18/03/2009", myUser),
                this.AddFixture(12, this.heatDiv1TeamLeague, this.easingwoldDiv1TeamLeague, "01/03/2009", "01/03/2009", myUser),
                // Old fixtures
                this.AddFixture(13, this.velocityOldDiv1TeamLeague, this.heatDiv1TeamLeague, "01/03/2006", "01/03/2006", myUser), // 12
                this.AddFixture(14, this.heatOldDiv1TeamLeague, this.easingwoldDiv1TeamLeague, "10/03/2006", "10/03/2006", myUser), // 13
                // Fixtures Heat B
                this.AddFixture(15, this.heatbDiv2TeamLeague, this.ucaDiv2TeamLeague, "05/03/2009", "05/03/2006", myUser), // 14
                this.AddFixture(16, this.ucaDiv2TeamLeague, this.heatbDiv2TeamLeague, "10/03/2009", "10/03/2009", myUser)
            };
        }


        #region Methods
        private User AddUser(int id, out User user)
        {
            user = new User()
            {
                Email = "stuff@email.com",
                Password = "abc",
                UserName = "Phil",
                Id = id
            };

            return user;
        }

        private Season AddSeason(int id, int startYear, int endYear, out Season season)
        {
            season = new Season(startYear, endYear);
            season.Id = id;

            return season;
        }

        private League AddLeague(int id, Season season, string leagueDescription, out League league)
        {
            league = new League(season, leagueDescription, 1, 1);
            league.Id = id;

            return league;
            //Console.WriteLine("league id = " + league.Id + " - season id " + league.Season.Id);
        }

        private Team AddTeam(int id, string teamName, string teamNameLong, out Team team)
        {
            team = new Team(teamName, teamNameLong);
            team.Id = id;
            
            return team;
        }

        private TeamLeague AddTeamLeague(int id, Team team, League league, out TeamLeague teamLeague)
        {
            teamLeague = new TeamLeague(league, team, team.TeamName, team.TeamNameLong);
            teamLeague.Id = id;

            return teamLeague;
        }

        //public void AddTeamLeaguePenalty(int teamLeagueId, int penaltyPoints)
        //{
        //    TeamLeague teamLeague = teamLeagueRepository.Get(teamLeagueId);

        //    teamLeague.PointsPenalty = penaltyPoints;

        //    //teamLeague = teamLeagueRepository.SaveOrUpdate(teamLeague);
        //    //FlushSessionAndEvict(teamLeague);
        //}

        private Fixture AddFixture(int id, TeamLeague home, TeamLeague away, string fixtureDate, string resultAddedDate, User lastUpdatedBy, bool penaltyAllowed = true)
        {
            Fixture fixture = new Fixture(home, away, DateTime.Parse(fixtureDate), lastUpdatedBy);
             fixture.ResultAddedDate = DateTime.Parse(resultAddedDate);
             fixture.IsPenaltyAllowed = penaltyAllowed;

            fixture.IsCupFixture = false;

            return fixture;
        }

        private Fixture AddPlayedFixture(int id, TeamLeague home, TeamLeague away, string fixtureDate, int homeScore, int awayScore, User lastUpdatedBy)
        {
            Fixture fixture = new Fixture(home, away, DateTime.Parse(fixtureDate), lastUpdatedBy);
            // myUser Result added date
            fixture.IsCupFixture = false;
            fixture.IsPlayed = "Y";
            fixture.HomeTeamScore = homeScore;
            fixture.AwayTeamScore = awayScore;
            fixture.Id = id;

            return fixture;
        }

        //protected void AddResult(int fixtureId, int homeScore, int awayScore, int day, int month, int year, int hour, int minute)
        //{
        //    Fixture f = AddResult(fixtureId, homeScore, awayScore);

        //    f.ResultAddedDate = new DateTime(year, month, day, hour, minute, 0);
        //    fixtureRepository.SaveOrUpdate(f);
        //    FlushSessionAndEvict(f);
        //}

        //private void AddPenalty(League league, Team team, int points, string reason, Fixture fixture)
        //{
        //    Penalty penalty = new Penalty(league, team, points, reason, fixture);

        //    penaltyRepository.SaveOrUpdate(penalty);
        //    FlushSessionAndEvict(penalty);
        //}


        protected Player AddPlayer(int id, string forename, string surname, Team team)
        {
            Player player = new Player(forename, surname, team);
            player.Id = id;
            
            return player;
        }


        private PlayerFixture AddPlayerFixture(int id, TeamLeague teamLeague, Fixture fixture, Player player, int points, int fouls, bool isMvp)
        {
            PlayerFixture playerFixture = new PlayerFixture(teamLeague, fixture, player, points, fouls);
            playerFixture.Id = id;

            if (isMvp)
                playerFixture.IsMvp = "Y";
            else
                playerFixture.IsMvp = "N";

            //statsRepository.SaveOrUpdatePlayerFixture(playerFixture);
            //FlushSessionAndEvict(playerFixture);
            return playerFixture;
        }

        protected PlayerSeasonStats AddPlayerSeasonStats(Player player, Season season, int totalPoints, int totalFouls, int gamesPlayed, int mvpAwards)
        {
            PlayerSeasonStats stats = new PlayerSeasonStats(player, season, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            //statsRepository.SaveOrUpdatePlayerSeasonStats(stats);
            //FlushSessionAndEvict(stats);
            return stats;
        }

        protected PlayerCareerStats AddPlayerCareerStats(Player player, int totalPoints, int totalFouls, int gamesPlayed, int mvpAwards)
        {
            PlayerCareerStats stats = new PlayerCareerStats(player, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            //statsRepository.SaveOrUpdatePlayerCareerStats(stats);
            //FlushSessionAndEvict(stats);
            return stats;
        }

        protected PlayerLeagueStats AddPlayerLeagueStats(Player player, Season season, League league, int totalPoints, int totalFouls, int gamesPlayed, int mvpAwards)
        {
            PlayerLeagueStats stats = new PlayerLeagueStats(player, season, league, totalPoints, totalFouls, gamesPlayed, mvpAwards);

            //statsRepository.SaveOrUpdatePlayerLeagueStats(stats);
            //FlushSessionAndEvict(stats);
            return stats;
        }
        #endregion
    }
}
