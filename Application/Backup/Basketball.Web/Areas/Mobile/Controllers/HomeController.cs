using System.Web.Mvc;

namespace Basketball.Web.Areas.Mobile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Home", new { area = "" });
        }

    }
}
