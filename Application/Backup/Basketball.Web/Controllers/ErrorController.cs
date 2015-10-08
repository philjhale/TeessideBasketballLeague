using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class ErrorController : BaseController
    {


        public ActionResult Unknown()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

    }
}
