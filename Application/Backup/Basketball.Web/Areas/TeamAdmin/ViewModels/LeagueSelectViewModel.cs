using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Basketball.Common.Resources;

namespace Basketball.Web.Areas.TeamAdmin.ViewModels
{
    public class LeagueSelectViewModel
    {
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public int LeagueId  { get; set; }

        public List<SelectListItem> Leagues { get; set; }
    }

}