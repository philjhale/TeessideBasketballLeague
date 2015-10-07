using System;
using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewModels
{
    public class MatchStatsViewModel
    {
        public Fixture Fixture { get; set; }
        public List<PlayerFixture> HomePlayerStats { get; set; }
        public List<PlayerFixture> AwayPlayerStats { get; set; }
        public List<Fixture> FixtureHistory { get; set; }
        public int HomeTeamFixtureWins { get; set; }
        public int AwayTeamFixtureWins { get; set; }

        // Should this calculation be here?
        /// <exception cref="InvalidOperationException"></exception>
        public void SetHistoryFixtureWins()
        {
            int homeWins = 0;
            int awayWins = 0;
            List<Fixture> allFixtures = new List<Fixture>();

            // If fixture is null then something is horribly wrong
            if (Fixture == null)
                throw new InvalidOperationException("Method should not be called before Fixture is populateds");

            allFixtures.Add(Fixture);

            if(FixtureHistory != null)
                allFixtures.AddRange(FixtureHistory);

            foreach(var f in allFixtures)
            {
                // Count the home and away wins. Note in this instance the home team is the home team for the currently
                // viewed fixture
                if ((f.HomeTeamScore > f.AwayTeamScore && f.HomeTeamLeague.Team.Id == Fixture.HomeTeamLeague.Team.Id)
                    || (f.AwayTeamScore > f.HomeTeamScore && f.AwayTeamLeague.Team.Id == Fixture.HomeTeamLeague.Team.Id))
                    homeWins++;
                else
                    awayWins++;
            }

            HomeTeamFixtureWins = homeWins;
            AwayTeamFixtureWins = awayWins;
        }
    }
}
