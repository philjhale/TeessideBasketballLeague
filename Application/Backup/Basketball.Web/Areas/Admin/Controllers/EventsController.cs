using System.Web.Mvc;
using Basketball.Domain;
using System.Collections.Generic;
using System.Web.Security;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using System.Linq;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;


namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class EventsController : BaseController
    {
        private readonly IEventService eventService;

        public EventsController(IEventService eventService)
        {
            Check.Require(eventService != null, "eventService may not be null");
            
            this.eventService = eventService;
        }

        public ActionResult Index()
        {
            List<Event> eventList = eventService.Get(orderBy: q => q.OrderByDescending(e => e.Id));

            return View(eventList);
        }

        public ActionResult Create()
        {
            return View(new Event());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                eventService.Insert(@event);
                eventService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        public ActionResult Edit(int id)
        {
            Event evt = eventService.Get(id);
            return View(evt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event evt)
        {
            if (ModelState.IsValid)
            {
                eventService.Update(evt);
                eventService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
 
                return View(evt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Event eventToDelete)
        {
            bool success = true;

            if (eventToDelete != null)
            {
                try
                {
                    eventService.Delete(eventToDelete);
                    eventService.Commit();
                }
                catch
                {
                    success = false;
                }
            }

            if (success)
                SuccessMessage(FormMessages.DeleteSuccess);
            else
                ErrorMessage(FormMessages.DeleteFail);

            return RedirectToAction("Index");
        }

    }
}
