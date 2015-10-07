using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<News>               News               { get; set; }
        public List<MatchResult>        LatestMatchResults { get; set; }
        public List<DivisionStandings>  Divisions          { get; set; }
        public List<Event>              NextEvents         { get; set; }
        public List<Cup>                CupCompetitions    { get; set; }

        public HomeViewModel()
        {
            LatestMatchResults = new List<MatchResult>();
            Divisions          = new List<DivisionStandings>();
            CupCompetitions    = new List<Cup>();
        }
    }
}
