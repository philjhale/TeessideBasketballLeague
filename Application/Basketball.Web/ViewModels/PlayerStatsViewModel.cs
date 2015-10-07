using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewModels
{
    public class PlayerStatsViewModel
    {
        public Player Player { get; set; }
        public List<PlayerFixture> CurrentSeasonFixtureStats { get; set; }
        public List<PlayerSeasonStats> SeasonStats { get; set; }
        public PlayerCareerStats CareerStats { get; set; } 
    }
}
