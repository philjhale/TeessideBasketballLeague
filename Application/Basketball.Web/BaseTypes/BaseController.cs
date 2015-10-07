using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Basketball.Common.Util;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.Validation;
using Ninject;

namespace Basketball.Web.BaseTypes
{
    [BasketballAuthorize] // Should probably renamed this because it's actually setting ViewBag values
    public class BaseController : Controller
    {
        // This makes me die a little inside but the alternative seems to be constructor injection
        // and therefore changing all the constuctors for the subclasses
        [Inject]
        public IErrorService ErrorService { get; set; }

        // http://colinmackay.co.uk/blog/2011/05/02/custom-error-pages-and-error-handling-in-asp-net-mvc-3-2/
        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                ErrorService.Insert(new Error(
                       filterContext.Exception.Message,
                       filterContext.Exception.StackTrace,
                       filterContext.HttpContext.User.Identity.Name));

                ErrorService.Commit();
            }
            catch (Exception ex)
            {
                // If the code ever reaches this point then I'm screwed
                // TODO Email here
                Email email = new Email(false, "philjhale@gmail.com");

                // Send email BEFORE user is saved. Otherwise you could reset their password to something unknown
                // No error handling. Same old story. Can't be arse at the moment
                email.Send("TBL Exception (Inner) - " + ex.Message, ex.StackTrace + "\n\n\n" + (ex.InnerException != null ? ex.InnerException.Message + "\n\n" + ex.InnerException.StackTrace : "No inner exception"));
            }

            Email email2 = new Email(false, "philjhale@gmail.com");

            // Send email BEFORE user is saved. Otherwise you could reset their password to something unknown
            // No error handling. Same old story. Can't be arse at the moment
            string subject = string.Format("TBL exception - {0}", filterContext.Exception.Message);
            var stacktrace = new StringBuilder();
            
            Exception currentEx = filterContext.Exception;

            while(currentEx != null)
            {
                stacktrace.AppendLine(currentEx.Message);
                stacktrace.AppendLine(currentEx.StackTrace);
                stacktrace.AppendLine("--------------------");
                currentEx = currentEx.InnerException;
            }
            
            email2.Send(subject, stacktrace.ToString());

            //email2.Send("TBL Exception - " + filterContext.Exception.Message, filterContext.Exception.StackTrace + "\n\n\n" + (filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message + "\n\n" + filterContext.Exception.InnerException.StackTrace : "No inner exception"));
            // TODO Email?
        }

        public void SuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public void ErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
         
        // Adding a dependency allows the entire cache to be cleared programmicatically
        // http://stackoverflow.com/questions/11585/clearing-page-cache-in-asp-net/2876701#2876701
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase httpContext = filterContext.HttpContext;
            httpContext.Response.AddCacheItemDependency("Pages");

            base.OnActionExecuting(filterContext);
        }

        protected void ClearCache()
        {
            HttpRuntime.Cache.Insert("Pages", DateTime.Now);
            
        }
    }
}
