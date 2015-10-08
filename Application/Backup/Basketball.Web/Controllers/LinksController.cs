using System.Web.Mvc;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class LinksController : BaseController
    {
		[OutputCache(CacheProfile="Public")]
        public ActionResult Index()
        {
            return View();
        }
    }
}