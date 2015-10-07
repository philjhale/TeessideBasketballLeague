using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Common.Extensions;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class PlayersViewModel
    {
        public int                  FilterByTeamId                 { get; set; }

        public List<Player>         Players                        { get; private set; }
        public List<SelectListItem> Teams                          { get; private set; }

        public PlayersViewModel() { }

        public PlayersViewModel(List<Player> players, List<Team> teams, int filterByTeamId)
        {
            this.Players        = players;
            this.Teams          = teams.ToSelectListWithHeader(x => x.TeamNameLong, x => x.Id.ToString(), null, "All", "-1");
            this.FilterByTeamId = filterByTeamId;
        }
    }
}