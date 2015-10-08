using System;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [FixtureAdmin]
    public class RefereesController : BaseController
    {
        private readonly IRefereeService refereeService;

        public RefereesController(IRefereeService refereeService)
        {
            Check.Require(refereeService != null, "refereeService may not be null");
            this.refereeService = refereeService;
        }

        public ActionResult Index()
        {
            return View(refereeService.GetAllRefereesWithCurrentSeasonFixtureCount());
        }
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Referee referee)
        {
            if (ModelState.IsValid)
            {
                refereeService.Insert(referee);
                refereeService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(referee);
        }

        public ActionResult Edit(int id)
        {
            return View(refereeService.Get(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Referee referee)
        {
            if (ModelState.IsValid)
            {
                refereeService.Update(referee);
                refereeService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(referee);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Referee referee = refereeService.Get(id);

            try
            {
                if (referee != null)
                {
                    refereeService.Delete(referee);
                    refereeService.Commit();

                    SuccessMessage(FormMessages.DeleteSuccess);
                }
            }
            catch (Exception)
            {
                ErrorMessage(FormMessages.DeleteFail);
            }

            return RedirectToAction("Index");
        }

    }
}
