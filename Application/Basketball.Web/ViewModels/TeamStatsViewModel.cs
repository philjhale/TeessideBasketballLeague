using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.ViewModels
{
    public class TeamStatsViewModel : FilterBySeasonViewModel
    {
        public List<DivisionStandings> DivisionStandings { get; set; }

        public TeamStatsViewModel()
        {
            DivisionStandings = new List<DivisionStandings>();
        }
    }
}
