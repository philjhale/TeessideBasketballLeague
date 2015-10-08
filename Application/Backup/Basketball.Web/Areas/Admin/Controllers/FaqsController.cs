using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Common.BaseTypes;
using Basketball.Common.Domain;
using Basketball.Common.Resources;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class FaqsController : BaseController
    {
        private readonly IFaqService faqService;

        public FaqsController(IFaqService faqService)
        {
            Check.Require(faqService != null, "faqService may not be null");
            this.faqService = faqService;
        }

        public ActionResult Index()
        {
            List<Faq> faqList = faqService.Get();
            return View(faqList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.LastUpdated = DateTime.Now;
                faqService.Insert(faq);
                faqService.Commit();

                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        public ActionResult Edit(int id)
        {
            return View(faqService.Get(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Faq faq)
        {
            if (ModelState.IsValid)
            {
                faq.LastUpdated = DateTime.Now;
                faqService.Update(faq);
                faqService.Commit();
                SuccessMessage(FormMessages.SaveSuccess);
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Faq faq = faqService.Get(id);

            if (faq != null)
            {
                faqService.Delete(faq);
                faqService.Commit();

                SuccessMessage(FormMessages.DeleteSuccess);
            }

            return RedirectToAction("Index");
        }

    }
}
