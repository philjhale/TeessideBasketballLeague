using System.Collections.Generic;
using System.Web.Mvc;

using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class PenaltyViewModel
    {
        public Penalty Penalty { get; set; }

        public int LeagueId { get; set; }
        public int TeamId { get; set; }

        public List<SelectListItem> Leagues { get; set; }
        public List<SelectListItem> Teams { get; set; }

        public PenaltyViewModel()
        {
            Penalty = new Penalty();
        }

        public void MapToModel(Penalty penalty)
        {
            Penalty = penalty;
            LeagueId = penalty.League.Id;
            TeamId = penalty.Team.Id;
        }
    }
}