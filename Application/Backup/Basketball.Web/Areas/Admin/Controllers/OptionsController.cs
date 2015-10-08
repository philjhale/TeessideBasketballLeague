using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Areas.Admin.Controllers
{
    public class OptionsController : BaseController
    {
        private readonly IOptionService optionService;

        public OptionsController(IOptionService optionService)
        {
            Check.Require(optionService != null, "optionService may not be null");
            this.optionService = optionService;
        }

        public ActionResult Index()
        {
            List<Option> optionList = optionService.Get(orderBy: q => q.OrderBy(o => o.Name));
            return View(optionList);
        }


        public ActionResult Edit(int id)
        {
            Option option = optionService.Get(id);
            return View(option);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Option option)
        {
            if (ModelState.IsValid)
            {
                optionService.Update(option);
                optionService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(option);
        }
    }
}
