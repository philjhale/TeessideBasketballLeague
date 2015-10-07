using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Basketball.Common.Resources;

namespace Basketball.Web.Areas.TeamAdmin.ViewModels
{
    public class CupSelectViewModel
    {
        [Required(ErrorMessage = FormMessages.FieldMandatory)]
        public int CupId  { get; set; }

        public List<SelectListItem> Cups { get; set; }
    }

}