using System.Collections.Generic;
using Basketball.Domain.Entities;
using System.Linq;

namespace Basketball.Web.ViewModels
{
    public class PastWinnersViewModel
    {
        public List<LeagueWinner>  LeagueWinners  { get; private set; } 
        public List<CupWinner>     CupWinners     { get; private set; }

        public PastWinnersViewModel(List<LeagueWinner> leagueWinners, List<CupWinner> cupWinners)
        {
            this.LeagueWinners = leagueWinners;
            this.CupWinners    = cupWinners;
        }

        public List<Season> GetSeasonsForLeagueAndCupWinners()
        {
            List<Season> seasons = new List<Season>();

            if(LeagueWinners != null)
                seasons.AddRange(LeagueWinners.Select(lw => lw.League.Season).ToList());

            if(CupWinners != null)
                seasons.AddRange(CupWinners.Select(cw => cw.Season).ToList());

            return seasons.Distinct().OrderByDescending(s => s.Id).ToList();
        }

        public List<LeagueWinner> GetLeagueWinnersForSeason(Season season)
        {
            if(LeagueWinners == null)
                return new List<LeagueWinner>();

            return LeagueWinners.Where(lw => lw.League.Season.Id == season.Id).ToList();
        }

        public List<CupWinner> GetCupWinnersForSeason(Season season)
        {
            if(CupWinners == null)
                return new List<CupWinner>();

            return CupWinners.Where(cw => cw.Season.Id == season.Id).ToList();
        }
    }
}