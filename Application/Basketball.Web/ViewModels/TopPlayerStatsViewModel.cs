using System.Collections.Generic;
using Basketball.Domain.Entities;
using Basketball.Web.ViewObjects;

namespace Basketball.Web.ViewModels
{
    public class TopPlayerStatsViewModel : FilterBySeasonViewModel
    {
        public bool SeasonHasStats { get; set; }
        public List<DivisionPlayerStats> DivisionPlayerStats { get; set; }
        
        public TopPlayerStatsViewModel()
        {
            DivisionPlayerStats = new List<DivisionPlayerStats>();
            SeasonHasStats = false;
        }
    }
}
