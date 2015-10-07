using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;
using Basketball.Common.Testing;

namespace Basketball.Tests
{
    public class ControllerTestsUtil
    {
        public static Event CreateEvent()
        {
            Event evt = new Event("Title", "Desc", DateTime.Now);
            EntityIdSetter.SetIdOf(evt, 1);
            return evt;
        }

        public static List<Event> CreateEventList()
        {
            List<Event> eventList = new List<Event>();

            eventList.Add(new Event("Title", "Desc", DateTime.Now));
            eventList.Add(new Event("CVL", "Junior CVL", DateTime.Now));

            return eventList;
        }

        public static User CreateUser()
        {
            User user = new User();
            
            user.Team = CreateTeam();

            EntityIdSetter.SetIdOf(user, 1);
            return user;
        }

        public static TeamLeague CreateTeamLeague()
        {
            League league = new League(new Season(2008, 2009), "Mens", 1, 1);
            EntityIdSetter.SetIdOf(league, 1);
            Team team = new Team("Velocity", "Velocity");
            TeamLeague teamLeague = new TeamLeague(league, team, "Velocity", "Velocity");

            return teamLeague;
        }

        public static IList<TeamLeague> CreateTeamLeagueList()
        {
            IList<TeamLeague> list = new List<TeamLeague>();

            League league = new League(new Season(2008, 2009), "Mens", 1, 1);
            EntityIdSetter.SetIdOf(league, 1);
            Team team = new Team("Velocity", "Velocity");
            EntityIdSetter.SetIdOf(team, 1);
            Team team2 = new Team("Heat", "Heat");
            EntityIdSetter.SetIdOf(team2, 2);

            TeamLeague teamLeague = new TeamLeague(league, team, "Velocity", "Velocity");
            EntityIdSetter.SetIdOf(teamLeague, 1);

            TeamLeague teamLeague2 = new TeamLeague(league, team2, "Velocity", "Velocity");
            EntityIdSetter.SetIdOf(teamLeague2, 2);

            list.Add(teamLeague);
            list.Add(teamLeague2);

            return list;
        }

        public static Team CreateTeam()
        {
            Team team = new Team("Team name", "Team name long");
            //Basketball.Domain.Entities.DayOfWeek dow = new Basketball.Domain.Entities.DayOfWeek("Monday");
            //EntityIdSetter.SetIdOf(dow, 1);
            EntityIdSetter.SetIdOf(team, 1);

            //team.GameDay = dow;

            return team;
        }

        public static List<Team> CreateTeamList()
        {
            List<Team> teamList = new List<Team>();
            Team team = new Team("Name", "Name Long");
            EntityIdSetter.SetIdOf(team, 1);
            teamList.Add(team);

            team = new Team("Name", "Name Long");
            EntityIdSetter.SetIdOf(team, 2);
            teamList.Add(team);

            return teamList;
        }

        public static Season CreateSeason()
        {
            Season season = new Season(2008, 2009);
            EntityIdSetter.SetIdOf(season, 1);

            return season;
        }

        public static IList<Season> CreateSeasonList()
        {
            IList<Season> seasonList = new List<Season>();

            Season season = new Season(2007, 2008);
            EntityIdSetter.SetIdOf(season, 1);
            seasonList.Add(season);

            season = new Season(2008, 2009);
            EntityIdSetter.SetIdOf(season, 2);

            seasonList.Add(season);
            return seasonList;
        }

        public static IList<Cup> CreateCupList()
        {
            IList<Cup> cupList = new List<Cup>();
            cupList.Add(CreateCup(1));
            cupList.Add(CreateCup(2));

            return cupList;
        }

        public static League CreateLeague()
        {
            return new League(CreateSeason(), "League Desc", 1, 1);
        }

        public static IList<League> CreateLeagueList()
        {
            IList<League> leagueList = new List<League>();
            leagueList.Add(new League(CreateSeason(), "Div 1", 1, 1));
            leagueList.Add(new League(CreateSeason(), "Div 2", 2, 2));

            return leagueList;
        }

        public static LeagueWinner CreateLeagueWinner()
        {
            return new LeagueWinner(new League(CreateSeason(), "Desc", 1, 1), CreateTeam());
        }

        public static CupWinner CreateCupWinner()
        {
            return new CupWinner(CreateSeason(), CreateCup(1), CreateTeam());
        }

        public static Cup CreateCup(int id)
        {
            Cup cup = new Cup("Cup Name");
            EntityIdSetter.SetIdOf(cup, id);
            return cup;
        }

        public static Fixture CreateFixture(int id)
        {
            Season season = CreateSeason();
            EntityIdSetter.SetIdOf(season, 1);
            Team team = new Team("teamName", "teamNameLong");
            League league = new League(season, "league desc", 1, 1);
            EntityIdSetter.SetIdOf(league, 1);
            TeamLeague teamLeague = new TeamLeague(league, team, "teamName", "teamNameLong");

            Fixture fixture = new Fixture(teamLeague, teamLeague, DateTime.Today, CreateUser());
            fixture.IsCupFixture = false;
            EntityIdSetter.SetIdOf(fixture, id);
            return fixture;
        }

        public static List<Fixture> CreateFixtureList()
        {
            return CreateFixtureList(2);
        }

        public static List<Fixture> CreateFixtureList(int numberInList)
        {
            List<Fixture> fixtureList = new List<Fixture>();

            for(int i = 0; i < numberInList; i++)
                fixtureList.Add(CreateFixture(i+1));

            return fixtureList;
        }

        public static Option CreateOption()
        {
            Option option = new Option("This is a headline", "Here is some option");
            EntityIdSetter.SetIdOf(option, 1);
            return option;
        }

        public static List<Option> CreateOptionList()
        {
            List<Option> optionList = new List<Option>();

            optionList.Add(new Option("Event announced", "There is going to be an event held at xyz"));
            optionList.Add(new Option("Another subject", "Blah blah blah"));

            return optionList;
        }

        public static Player CreatePlayer()
        {
            Player player = new Player("Phil", "Hale", CreateTeam());
            EntityIdSetter.SetIdOf(player, 1);

            return player;
        }

        /// <summary>
        /// Simulate the last Player entered being returned
        /// </summary>
        /// <returns></returns>
        public static List<Player> CreatePlayerList(int numPlayersInList, bool addInvalidPlayer)
        {
            List<Player> playerList = new List<Player>();

            for (int i = 0; i < numPlayersInList; i++)
                playerList.Add(new Player("Joe", "Blogs"));

            if (addInvalidPlayer)
                playerList.Add(new Player() { Forename = "Forename" });

            return playerList;
        }

        public static PlayerFixture CreatePlayerFixture(int id)
        {
            Player player = CreatePlayer();

            Fixture fixture = CreateFixture(id);
            TeamLeague tl = CreateTeamLeague();

            PlayerFixture playerFixture = new PlayerFixture(tl, fixture, player, 10, 2);
            EntityIdSetter.SetIdOf(playerFixture, id);

            return playerFixture;
        }

        public static List<PlayerFixture> CreatePlayerFixtureList(int numPlayersInList)
        {
            List<PlayerFixture> playerFixtureList = new List<PlayerFixture>();

            for (int i = 0; i < numPlayersInList; i++)
                playerFixtureList.Add(CreatePlayerFixture(i));

            return playerFixtureList;
        }

        public static List<bool> CreateHasPlayedList(int numPlayersInList, int numPlayed)
        {
            List<bool> hasPlayedList = new List<bool>();
            int playedCount = 0;

            if (numPlayed > numPlayersInList)
                numPlayed = numPlayersInList;

            for (int i = 0; i < numPlayersInList; i++) {
                if (playedCount < numPlayed)
                    hasPlayedList.Add(true);
                else
                    hasPlayedList.Add(false);

                playedCount++;
            }

            return hasPlayedList;
        }

        public static PlayerSeasonStats CreatePlayerSeasonStats()
        {
            PlayerSeasonStats p = new PlayerSeasonStats(CreatePlayer(), CreateSeason(), 100, 20, 5, 1);
            EntityIdSetter.SetIdOf(p, 1);

            return p;
        }

        public static PlayerSeasonStats CreatePlayerSeasonStats(int id, Player player, Season season)
        {
            PlayerSeasonStats p = new PlayerSeasonStats(player, season, 100, 20, 5, 1);
            EntityIdSetter.SetIdOf(p, id);

            return p;
        }

        public static IList<PlayerSeasonStats> CreatePlayerSeasonStatsList()
        {
            IList<PlayerSeasonStats> stats = new List<PlayerSeasonStats>();
            Player p = CreatePlayer();
            Season s = CreateSeason();

            stats.Add(new PlayerSeasonStats(p, s, 50, 5, 5, 1));
            stats.Add(new PlayerSeasonStats(p, s, 50, 5, 5, 1));

            return stats;
        }

        public static PlayerCareerStats CreatePlayerCareerStats()
        {
            PlayerCareerStats p = new PlayerCareerStats(CreatePlayer(), 100, 20, 5, 1);
            EntityIdSetter.SetIdOf(p, 1);

            return p;
        }

    }
}
