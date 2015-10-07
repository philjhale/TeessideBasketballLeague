using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;
using Basketball.Web.BaseTypes;


namespace Basketball.Areas.TeamAdmin.ViewModels
{
    public class MyTeamViewModel
    {
        public Team Team { get; set; }
        public int? DayOfWeekId { get; set; }
        public SelectList DaysOfWeek { get; set; }

        public MyTeamViewModel()
        {
            Team = new Team();
            Team.GameDay = new DayOfWeek();
        }
    }
}
