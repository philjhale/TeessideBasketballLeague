using System.Collections.Generic;
using System.Web.Mvc;

using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class PlayerViewModel
    {
        public Player Player { get; set; }

        public int? TeamId { get; set; }

        public List<SelectListItem> Teams { get; set; }

        public PlayerViewModel()
        {
            Player = new Player();
        }

        public void MapToModel(Player player)
        {
            Player = player;
            if(player.Team != null)
                TeamId = player.Team.Id;
        }
    }
}