using System.Web.Mvc;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;
using Basketball.Service.Interfaces;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [FixtureAdmin]
    public class FixtureHistoryController : BaseController
    {
        private readonly IFixtureHistoryService fixtureHistoryService;

        public FixtureHistoryController(IFixtureHistoryService fixtureHistoryHistoryService)
        {
            this.fixtureHistoryService = fixtureHistoryHistoryService;
        }

        public ActionResult Index(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction("Index", "Fixtures");

            return View(fixtureHistoryService.Get(x => x.Fixture_Id == id.Value));
        }
    }
}
