using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basketball.Domain.Entities;

namespace Basketball.Web.ViewModels
{
    public class FilterBySeasonViewModel
    {
        public int FilterBySeasonId { get; set; }
        public List<Season> Seasons { get; set; }

        public FilterBySeasonViewModel()
        {
            Seasons = new List<Season>();
        }
    }
}
