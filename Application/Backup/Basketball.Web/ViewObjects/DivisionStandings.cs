using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewObjects
{
    public class DivisionStandings
    {
        public string Name { get; set; }
        public List<TeamLeague> Standings { get; set; }
    }
}
