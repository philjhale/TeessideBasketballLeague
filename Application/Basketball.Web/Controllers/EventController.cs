using System.Web.Mvc;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

		[OutputCache(CacheProfile="Public")]
        public ActionResult View(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");

            Event evt = eventService.Get(id.Value);

            if(evt == null)
                return RedirectToAction("Index", "Home");

            return View(evt);
        }
    }
}