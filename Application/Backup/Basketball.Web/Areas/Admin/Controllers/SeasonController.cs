using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class SeasonController : BaseController
    {
        private readonly ICompetitionService competitionService;

        public SeasonController(ICompetitionService competitionService)
        {
            this.competitionService = competitionService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeason()
        {
            Season nextSeason = competitionService.CreateNextSeason(competitionService.GetCurrentSeason());
            
            if (nextSeason == null)
            {
                ErrorMessage(FormMessages.SeasonCreateCurrentYearFail);
                return View("Create");
            }

            // Save the season to the database
            competitionService.SaveSeason(nextSeason);
            competitionService.Commit();

            SuccessMessage(FormMessages.SeasonCreateSuccess);

            return RedirectToAction("Create", "Leagues");
        }
    }
}
