using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Domain.Entities;

namespace Basketball.Web.Areas.TeamAdmin.ViewModels
{
    public class EditPlayerViewModel
    {
        public Player Player { get; set; }
        public List<SelectListItem> HeightFeet { get; set; }
        public List<SelectListItem> HeightInches { get; set; } 
    }

}