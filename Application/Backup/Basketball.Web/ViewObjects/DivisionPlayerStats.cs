using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewObjects
{
    public class DivisionPlayerStats
    {
        public string Name { get; set; }
        public List<PlayerLeagueStats> TopAvgScorers { get; set; }
    }
}
