using System.Collections.Generic;
using System.Web.Mvc;
using Basketball.Common.Domain;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Basketball.Web.Validation;
using System.Linq;
using System.Linq.Expressions;
using Error = Basketball.Domain.Entities.Error;

namespace Basketball.Web.Areas.Admin.Controllers
{
    [SiteAdmin]
    public class ErrorsController : BaseController
    {
        private readonly IErrorService errorService;

        public ErrorsController(IErrorService errorService)
        {
            Check.Require(errorService != null, "errorService may not be null");
            this.errorService = errorService;
        }

        public ActionResult Index()
        {
            List<Error> errorList = errorService.Get(orderBy: x => x.OrderByDescending(d => d.Id));
            return View(errorList);
        }

    }
}
