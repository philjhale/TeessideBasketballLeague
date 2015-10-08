using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class CupWinnerViewModel
    {
        public int CupWinnerId { get; set; }

        public int CupId { get; set; }
        public int TeamId { get; set; }

        public List<SelectListItem> Cups { get; set; }
        public List<SelectListItem> Teams { get; set; }

        public void MapToModel(CupWinner leagueWinner)
        {
            this.CupWinnerId = leagueWinner.Id;
            this.CupId       = leagueWinner.Cup.Id;
            TeamId           = leagueWinner.Team.Id;
        }
    }
}