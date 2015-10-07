using System.Web.Mvc;
using Basketball.Common.BaseTypes;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;

namespace Basketball.Web.Areas.TeamAdmin.Controllers
{
    [TeamAdmin]
    public class FaqsController : BaseController
    {
        private readonly IFaqService faqService;

        public FaqsController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public ActionResult Index()
        {
            return View(faqService.Get());
        }
    }
}
