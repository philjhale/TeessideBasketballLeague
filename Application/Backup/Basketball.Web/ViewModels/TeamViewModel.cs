using System.Collections.Generic;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewModels
{
    public class TeamViewModel
                                                                    {
        public Team                     Team                        { get; set; }
        public List<PlayerSeasonStats>  Players                     { get; set; }
        public List<LeagueWinner>       LeagueWins                  { get; set; }
        public List<CupWinner>          CupWins                     { get; set;} 

        public int                      TotalWins                   { get; set; }
        public int                      TotalLosses                 { get; set; }
        public int                      TotalPointsFor              { get; set; }
        public int                      TotalPointsAgainst          { get; set; }
        public decimal                  AveragePointsPerGameFor     { get; set; }
        public decimal                  AveragePointsPerGameAgainst { get; set; }

        public Fixture                  BiggestHomeWin              { get; set; }
        public Fixture                  BiggestAwayWin              { get; set; }
        // Highest scoring player
        // Player with most MVPs
    }
}
