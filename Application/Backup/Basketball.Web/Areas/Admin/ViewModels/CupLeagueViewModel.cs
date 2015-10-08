using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class CupLeagueViewModel
    {
        public int CupLeagueId { get; set; }

        public int LeagueId { get; set; }
        public int CupId { get; set; }

        public List<SelectListItem> Leagues { get; set; }
        public List<SelectListItem> Cups { get; set; }

        //public void MapToModel(TeamLeague teamLeague)
        //{
        //    TeamLeagueId = teamLeague.Id;
        //    LeagueId = teamLeague.League.Id;
        //    TeamId = teamLeague.Team.Id;
        //}
    }
}