using System.Web.Mvc;

using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class FeedbackController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "ContactUs");
        }
    }
}