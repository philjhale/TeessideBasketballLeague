using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Basketball.Common.Extensions;
using Basketball.Domain.Entities;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.ViewModels
{
    public class FixturesViewModel
    {
        public string               SeasonName             { get; private set; }
        public int                  FilterByTeamId         { get; set; }
        public string               FilterByLeagueOrCupId  { get; set; }
        public string               FilterByIsPlayed       { get; set; }
        public List<SelectListItem> LeaguesAndCups         { get; private set; }
        public List<SelectListItem> Teams                  { get; private set; }
        public List<Fixture>        Fixtures               { get; private set; }

        public FixturesViewModel() { }

        public FixturesViewModel(string seasonName, List<Team> teams, List<League> leagues, List<Cup> cups, List<Fixture> fixtures)
        {
            this.PopulateData(seasonName, teams, leagues, cups, fixtures);
            this.FilterByIsPlayed                            = "N";
        }

        private const string LeagueIdentifier = "L";
        private const string CupIdentifier = "C";
        public void PopulateData(string seasonName, List<Team> teams, List<League> leagues, List<Cup> cups, List<Fixture> fixtures)
        {
            this.SeasonName                                  = seasonName;
            this.Teams                                       = teams.ToSelectListWithHeader(x => x.TeamNameLong, x => x.Id.ToString(), null, "All", "-1");
            this.LeaguesAndCups                              = leagues.ToSelectListWithHeader(l => l.ToString(), l => LeagueIdentifier + l.Id.ToString(), null, "All", "-1");
            this.LeaguesAndCups.AddRange(cups.ToSelectList(c => c.CupName, l => CupIdentifier + l.Id.ToString(), null));
            this.Fixtures                                    = fixtures;
        }

        public bool IsFilteredByLeague()
        {
            return FilterByLeagueOrCupId != "-1" && FilterByLeagueOrCupId.StartsWith(LeagueIdentifier);
        }

        public bool IsFilteredByCup()
        {
            return FilterByLeagueOrCupId != "-1" && FilterByLeagueOrCupId.StartsWith(CupIdentifier);
        }

        public int? GetLeagueOrCupId()
        {
            if(FilterByLeagueOrCupId == "-1")
                return null;

            return int.Parse(FilterByLeagueOrCupId.Substring(1, FilterByLeagueOrCupId.Length - 1));
        }

        public List<FixturesByMonth> GetFixturesByMonth()
        {
             List<FixturesByMonth> fixturesByMonth = new List<FixturesByMonth>();

            if(Fixtures == null || Fixtures.Count == 0)
                return fixturesByMonth;

            Fixtures = Fixtures.OrderBy(f => f.FixtureDate).ToList();

            List<Fixture> fixturesInMonth = new List<Fixture>();
            for (int i = 0; i < Fixtures.Count; i++)
            {
                var fixture = Fixtures[i];

                fixturesInMonth.Add(fixture);

                if(i + 1 == Fixtures.Count || fixture.FixtureDate.Month != Fixtures[i + 1].FixtureDate.Month)
                {
                    fixturesByMonth.Add(new FixturesByMonth(fixture.FixtureDate, fixturesInMonth));
                    fixturesInMonth = new List<Fixture>();
                }

            }

            return fixturesByMonth;
        }
    }
}
