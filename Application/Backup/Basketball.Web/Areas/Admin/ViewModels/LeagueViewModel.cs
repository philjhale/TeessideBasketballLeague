using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.Admin.ViewModels
{
    public class LeagueViewModel
    {
        public League League { get; set; }

        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public int SeasonId { get; set; }
        public List<SelectListItem> Seasons { get; set; }

        public LeagueViewModel()
        {
            League = new League();
            League.Season = new Season();
        }
    }
}