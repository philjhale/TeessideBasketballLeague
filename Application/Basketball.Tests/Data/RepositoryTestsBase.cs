using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Basketball.Domain.Entities;
using Basketball.Data;

namespace Basketball.Tests.Data
{
    /// <summary>
    /// This class is used to set up data for all the other repository tests
    /// </summary>
    //[TestFixture]
    //[Category("DB Tests")]
    public class RespositoryTestsBase
    {
        //public ISeasonRepository seasonRepository = new SeasonRepository();
        //public ITeamLeagueRepository teamLeagueRepository = new TeamLeagueRepository();
        //public IRepository<Team> teamRepository = new Repository<Team>();
        //public IFixtureRepository fixtureRepository = new FixtureRepository();
        //public IContactRepository contactRepository = new ContactRepository();
        
        //public IStatsRepository statsRepository = new StatsRepository();
        //public IPlayerRepository playerRepository = new PlayerRepository();
        public User user { get; set; }
        public Season oldSeason { get; set; }
        public Season currentSeason { get; set; }

        public League mensDiv1League { get; set; }
        public League mensDiv2League { get; set; }
        public League oldMensDiv1League { get; set; }

        // Teams Div 1
        public Team velocityTeam { get; set; }
        public Team heatTeam { get; set; }
        public Team vikingsTeam { get; set; }
        // Teams Div 2
        public Team heatbTeam { get; set; }
        public Team ucaTeam { get; set; }
        public Team explosionTeam { get; set; }

        // TeamLeagues Div1
        public TeamLeague velocityTeamLeague { get; set; }
        public TeamLeague heatTeamLeague { get; set; }
        public TeamLeague vikingsTeamLeague { get; set; }
        // TeamLeagues Div2
        public TeamLeague heatbTeamLeague { get; set; }
        public TeamLeague ucaTeamLeague { get; set; }
        public TeamLeague explosionTeamLeague { get; set; }
        // Old TeamLeagues Div1
        public TeamLeague oldVelocityTeamLeague { get; set; }
        public TeamLeague oldHeatTeamLeague { get; set; }

        public List<Fixture> CurrentSeasonFixtures { get; set; }

        public RespositoryTestsBase()
        {
            CurrentSeasonFixtures = new List<Fixture>();

            user = AddUser();

            // Seasons
            oldSeason = AddSeason(2006, 2007);
            currentSeason = AddSeason(2008, 2009);

            // Leagues
            mensDiv1League = AddLeague(currentSeason, "Mens");
            mensDiv2League = AddLeague(currentSeason, "Mens 2");
            oldMensDiv1League = AddLeague(oldSeason, "Mens");

            // Teams Div 1
            Team velocityTeam = AddTeam("Velocity", "Nunthorpe Velocity");
            Team heatTeam = AddTeam("Heat A", "Hartlepool Heat A");
            Team vikingsTeam = AddTeam("Easingwold", "Easingwold Vikings");
            // Teams Div 2
            Team heatbTeam = AddTeam("Heat B", "Hartlepool Heat B");
            Team ucaTeam = AddTeam("Team UCA", "Team UCA");
            Team explosionTeam = AddTeam("Egglescliffe Explosion", "Explosion");

            // TeamLeagues Div1
            TeamLeague velocityTeamLeague = AddTeamLeague(velocityTeam, mensDiv1League); // 1
            TeamLeague heatTeamLeague = AddTeamLeague(heatTeam, mensDiv1League);
            TeamLeague vikingsTeamLeague = AddTeamLeague(vikingsTeam, mensDiv1League);
            // TeamLeagues Div2
            TeamLeague heatbTeamLeague = AddTeamLeague(heatbTeam, mensDiv2League); // 4
            TeamLeague ucaTeamLeague = AddTeamLeague(ucaTeam, mensDiv2League);
            TeamLeague explosionTeamLeague = AddTeamLeague(explosionTeam, mensDiv2League);
            // Old TeamLeagues Div1
            TeamLeague oldVelocityTeamLeague = AddTeamLeague(velocityTeam, oldMensDiv1League);
            TeamLeague oldHeatTeamLeague = AddTeamLeague(heatTeam, oldMensDiv1League);


            // Fixtures Velocity
            Fixture velocityHeatFixture = this.AddFixture(velocityTeamLeague, heatTeamLeague, "01/03/2009", user); // 1
            Fixture velocityVikingsFixture = this.AddFixture(velocityTeamLeague, vikingsTeamLeague, "10/03/2009", user); // 2
            this.AddFixture(heatTeamLeague, velocityTeamLeague, "11/03/2009", user); // 3
            Fixture vikingsVelocityFixture = this.AddFixture(vikingsTeamLeague, velocityTeamLeague, "15/03/2009", user); // 4

            // Fixtures Heat
            this.AddFixture(heatTeamLeague, velocityTeamLeague, "11/03/2009", user); // 5 - 8
            this.AddFixture(heatTeamLeague, vikingsTeamLeague, "06/03/2009", user);
            this.AddFixture(velocityTeamLeague, heatTeamLeague, "18/03/2009", user);
            this.AddFixture(vikingsTeamLeague, heatTeamLeague, "01/03/2009", user);

            // Fixtures Vikings
            this.AddFixture(vikingsTeamLeague, velocityTeamLeague, "05/03/2009", user); // 9 - 11
            this.AddFixture(vikingsTeamLeague, heatTeamLeague, "06/03/2009", user);
            this.AddFixture(velocityTeamLeague, vikingsTeamLeague, "18/03/2009", user);
            this.AddFixture(heatTeamLeague, vikingsTeamLeague, "01/03/2009", user);

            // Old fixtures
            Fixture oldVelocityHeatFixture = this.AddFixture(oldVelocityTeamLeague, heatTeamLeague, "01/03/2007", user); // 12
            Fixture oldHeatVelocityFixture = this.AddFixture(oldHeatTeamLeague, vikingsTeamLeague, "10/03/2007", user); // 13

            // Fixtures Heat B
            Fixture heatbUcaFixture = this.AddFixture(heatbTeamLeague, ucaTeamLeague, "05/03/2009", user); // 14
            Fixture ucaHeatbFixture = this.AddFixture(ucaTeamLeague, heatbTeamLeague, "10/03/2009", user);

            // Add two penalties for explosion
            //AddPenalty(mensDiv2League, explosionTeam, 1, "Reason 1", velocityHeatFixture);
            //AddPenalty(mensDiv2League, explosionTeam, 2, "Reason 2", velocityHeatFixture);

            // Add some contact types and contacts
            //ContactType ctChairman = AddContactType("Chairman");

            //ContactType ctRef = AddContactType("Ref");
            //Contact cPhil = AddContact("Phil", "Hale");
            //Contact cJoe = AddContact("Joe", "Blogs");

            //AddContactTypeRel(cPhil, ctChairman);
            //AddContactTypeRel(cPhil, ctRef);
            //AddContactTypeRel(cJoe, ctRef);

            Player philHale = AddPlayer("Phil", "Hale", velocityTeam); //1
            Player lewisTovey = AddPlayer("Lewis", "Tovey", velocityTeam);
            Player adamTovey = AddPlayer("Adam", "Tovey", velocityTeam);
            Player anthRobinson = AddPlayer("Anth", "Robinson", heatbTeam); //4
            Player joeBloggs = AddPlayer("Joe", "Bloggs", heatbTeam);
            Player nigelClack = AddPlayer("Nigel", "Clack", vikingsTeam);
            Player danZero = AddPlayer("Dan", "Nogames", vikingsTeam); //7
            Player jimmmyTwoLeagues = AddPlayer("Jimmy", "Twoleagues", velocityTeam); //8

            AddPlayerFixture(velocityTeamLeague, velocityHeatFixture, philHale, 16, 3, true); //1
            AddPlayerFixture(velocityTeamLeague, velocityHeatFixture, lewisTovey, 16, 4, true); //2
            AddPlayerFixture(velocityTeamLeague, velocityHeatFixture, adamTovey, 10, 2, false); //3

            AddPlayerFixture(heatTeamLeague, velocityHeatFixture, anthRobinson, 20, 2, false); //4
            AddPlayerFixture(heatTeamLeague, velocityHeatFixture, joeBloggs, 3, 5, false); //5

            AddPlayerFixture(velocityTeamLeague, vikingsVelocityFixture, philHale, 11, 5, true); //6
            AddPlayerFixture(velocityTeamLeague, vikingsVelocityFixture, lewisTovey, 20, 1, false); //7
            AddPlayerFixture(velocityTeamLeague, vikingsVelocityFixture, adamTovey, 8, 4, false); //8

            AddPlayerFixture(vikingsTeamLeague, vikingsVelocityFixture, nigelClack, 5, 5, false); //9
            AddPlayerFixture(vikingsTeamLeague, vikingsVelocityFixture, danZero, 0, 0, true); //10

            // Old fixtures
            AddPlayerFixture(oldVelocityTeamLeague, oldVelocityHeatFixture, philHale, 22, 3, false); //11
            AddPlayerFixture(oldHeatTeamLeague, oldVelocityHeatFixture, anthRobinson, 30, 3, false); //12

            // Jimmy twoleagues fixtures
            AddPlayerFixture(velocityTeamLeague, velocityHeatFixture, jimmmyTwoLeagues, 10, 1, false); //13
            AddPlayerFixture(velocityTeamLeague, vikingsVelocityFixture, jimmmyTwoLeagues, 12, 2, false); //14
            // Change league for player
            jimmmyTwoLeagues.Team = heatbTeam;
            //SavePlayer(jimmmyTwoLeagues);
            AddPlayerFixture(heatbTeamLeague, heatbUcaFixture, jimmmyTwoLeagues, 8, 1, false); //15
            AddPlayerFixture(heatbTeamLeague, ucaHeatbFixture, jimmmyTwoLeagues, 5, 5, false); //16

            // PlayerSeasonStats
            AddPlayerSeasonStats(philHale, oldSeason, 22, 3, 1, 1);
            AddPlayerSeasonStats(philHale, currentSeason, 16, 3, 1, 2); // Only add one game
            AddPlayerSeasonStats(lewisTovey, currentSeason, 36, 5, 2, 1);
            AddPlayerSeasonStats(adamTovey, currentSeason, 18, 6, 2, 0);

            // PlayerCareerStats
            AddPlayerCareerStats(philHale, 49, 11, 3, 3); //1

            // PlayerLeagueStats
            AddPlayerLeagueStats(philHale, oldSeason, oldMensDiv1League, 22, 3, 1, 1);
            AddPlayerLeagueStats(philHale, currentSeason, mensDiv1League, 27, 8, 2, 2);
            AddPlayerLeagueStats(lewisTovey, currentSeason, mensDiv1League, 36, 5, 2, 1);
            AddPlayerLeagueStats(adamTovey, currentSeason, mensDiv1League, 18, 6, 2, 0);
            AddPlayerLeagueStats(jimmmyTwoLeagues, currentSeason, mensDiv1League, 22, 3, 2, 0);
            AddPlayerLeagueStats(jimmmyTwoLeagues, currentSeason, mensDiv2League, 13, 6, 2, 0);


            //seasonRepository.DbContext.CommitChanges();
        }

        private User AddUser()
        {
            User myUser = new User();
            myUser.UserName = "Phil";
            myUser.SiteAdmin = true;
            return myUser;
        }

        private Season AddSeason(int startYear, int endYear)
        {
            Season season = new Season(startYear, endYear);

            //season = seasonRepository.SaveOrUpdate(season);
            //FlushSessionAndEvict(season);

            return season;
        }

        private League AddLeague(Season season, string leagueDescription)
        {
            League league = new League(season, leagueDescription, 1, 1);

            //league = seasonRepository.SaveOrUpdateLeague(league);
            //FlushSessionAndEvict(league);
            return league;
            //Console.WriteLine("league id = " + league.Id + " - season id " + league.Season.Id);
        }

        private Team AddTeam(string teamName, string teamNameLong)
        {
            Team team = new Team(teamName, teamNameLong);

            //team = teamRepository.SaveOrUpdate(team);
            //FlushSessionAndEvict(team);

            return team;
        }

        private TeamLeague AddTeamLeague(Team team, League league)
        {
            TeamLeague teamLeague = new TeamLeague(league, team, team.TeamName, team.TeamNameLong);

            //teamLeague = teamLeagueRepository.SaveOrUpdate(teamLeague);
            //FlushSessionAndEvict(teamLeague);

            return teamLeague;
        }

        //public void AddTeamLeaguePenalty(int teamLeagueId, int penaltyPoints)
        //{
        //    TeamLeague teamLeague = teamLeagueRepository.Get(teamLeagueId);

        //    teamLeague.PointsPenalty = penaltyPoints;

        //    //teamLeague = teamLeagueRepository.SaveOrUpdate(teamLeague);
        //    //FlushSessionAndEvict(teamLeague);
        //}

        private Fixture AddFixture(TeamLeague home, TeamLeague away, string fixtureDate, User lastUpdatedBy)
        {
            Fixture fixture = new Fixture(home, away, DateTime.Parse(fixtureDate), lastUpdatedBy);

            fixture.IsCupFixture = false;
            //fixture = fixtureRepository.SaveOrUpdate(fixture);
            //FlushSessionAndEvict(fixture);

            CurrentSeasonFixtures.Add(fixture);

            return fixture;
        }

        //protected Fixture AddResult(int fixtureId, int homeScore, int awayScore)
        //{
        //    Fixture fixture = fixtureRepository.Get(fixtureId);
        //    //Console.WriteLine("Adding fixture - " + fixture.HomeTeamLeague.TeamName + " vs " + fixture.AwayTeamLeague.TeamName);
        //    fixture.HomeTeamScore = homeScore;
        //    fixture.AwayTeamScore = awayScore;
        //    fixture.IsPlayed = "Y";

        //    //fixtureRepository.SaveOrUpdate(fixture);
        //    //FlushSessionAndEvict(fixture);

        //    return fixture;
        //}

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

        //private ContactType AddContactType(string desc)
        //{
        //    ContactType contactType = new ContactType(desc);

        //    contactType = contactRepository.SaveOrUpdateContactType(contactType);
        //    FlushSessionAndEvict(contactType);
        //    return contactType;
        //}

        //private Contact AddContact(string forename, string surname)
        //{
        //    Contact contact = new Contact(forename, surname);

        //    contact = contactRepository.SaveOrUpdateContact(contact);
        //    FlushSessionAndEvict(contact);
        //    return contact;
        //}

        //private ContactTypeRel AddContactTypeRel(Contact contact, ContactType contactType)
        //{
        //    ContactTypeRel rel = new ContactTypeRel(contact, contactType);

        //    rel = contactRepository.SaveOrUpdateContactTypeRel(rel);
        //    FlushSessionAndEvict(rel);

        //    return rel;
        //}

        //protected void DeleteAllContacts()
        //{
        //    contactRepository.DeleteContactTypeRelsForContact(1);
        //    contactRepository.DeleteContactTypeRelsForContact(2);

        //    contactRepository.DeleteContact(contactRepository.GetContact(1));
        //    contactRepository.DeleteContact(contactRepository.GetContact(2));

        //    contactRepository.DeleteContactType(contactRepository.GetContactType(1));
        //    contactRepository.DeleteContactType(contactRepository.GetContactType(2));

        //    contactRepository.DbContext.CommitChanges();
        //}

        // Just a dummy method so the NUnit doesn't hand out a big
        // fat fail for having no tests
        //[Test]
        //public void Test()
        //{
        //}
        protected Player AddPlayer(string forename, string surname, Team team)
        {
            Player player = new Player(forename, surname, team);

            //playerRepository.SaveOrUpdate(player);
            //FlushSessionAndEvict(player);
            return player;
        }

        //protected Player SavePlayer(Player player)
        //{
        //    playerRepository.SaveOrUpdate(player);
        //    FlushSessionAndEvict(player);
        //    return player;
        //}

        private PlayerFixture AddPlayerFixture(TeamLeague teamLeague, Fixture fixture, Player player, int points, int fouls, bool isMvp)
        {
            PlayerFixture playerFixture = new PlayerFixture(teamLeague, fixture, player, points, fouls);

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
    }
}
