using System.Web.Mvc;
using Basketball.Web.BaseTypes;
using Basketball.Service.Interfaces;
using System.Linq;

namespace Basketball.Web.Controllers
{
    public class ViewNewsController : BaseController
    {
        readonly INewsService newsService;

        public ViewNewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult Index()
        {
            // Add option
            return View(newsService.GetTop(15, orderBy: q => q.OrderByDescending(n => n.Id)));
        }

        [OutputCache(CacheProfile="Public")]
        public ActionResult Item(int id)
        {
            return View(newsService.Get(id));
        }
    }
}
