using System.Web.Mvc;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class StandingsController : BaseController
    {
        public ActionResult ViewStandings()
        {
            return RedirectToActionPermanent("Standings", "Stats");
        }

    }
}
