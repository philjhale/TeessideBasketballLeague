using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class LeagueWinnerViewModel
    {
        public int LeagueWinnerId { get; set; }

        public int LeagueId { get; set; }
        public int TeamId { get; set; }

        public List<SelectListItem> Leagues { get; set; }
        public List<SelectListItem> Teams { get; set; }

        public void MapToModel(LeagueWinner leagueWinner)
        {
            LeagueWinnerId = leagueWinner.Id;
            LeagueId       = leagueWinner.League.Id;
            TeamId         = leagueWinner.Team.Id;
        }
    }
}