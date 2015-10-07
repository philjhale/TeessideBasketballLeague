using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class OneOffVenuesController : BaseController
    {
        private readonly IOneOffVenueService oneOffVenueService;

        public OneOffVenuesController(IOneOffVenueService oneOffVenueService)
        {
            Check.Require(oneOffVenueService != null, "oneOffVenueService may not be null");
            this.oneOffVenueService = oneOffVenueService;
        }

        public ActionResult Index()
        {
            return View(oneOffVenueService.Get());
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(OneOffVenue oneOffVenue)
        {
            if (ModelState.IsValid)
            {
                oneOffVenueService.Insert(oneOffVenue);
                oneOffVenueService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(oneOffVenue);
        }

        public ActionResult Edit(int id)
        {
            return View(oneOffVenueService.Get(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(OneOffVenue oneOffVenue)
        {
            if (ModelState.IsValid)
            {
                oneOffVenueService.Update(oneOffVenue);
                oneOffVenueService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(oneOffVenue);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            OneOffVenue oneOffVenue = oneOffVenueService.Get(id);

            if (oneOffVenue != null)
            {
                oneOffVenueService.Delete(oneOffVenue);
                oneOffVenueService.Commit();

                SuccessMessage(FormMessages.DeleteSuccess);
            }

            return RedirectToAction("Index");
        }

    }
}
