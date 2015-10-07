using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Basketball.Domain.Entities;
using Basketball.Service.Interfaces;
using Basketball.Web.BaseTypes;
using Ninject;

namespace Basketball.Web.Validation
{
    // TODO Fix session/authorisation issue. A user can be authenicated for longer than the session lasts
    // To be fair this mostly happens in dev but a user can still be authenicated but have no roles
    public class SiteAdminAttribute : BasketballAuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(IsAuthenticated(filterContext) && MembershipService.IsSiteAdmin(GetUser(filterContext))))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "LogOn" }, { "controller", "Account" }, { "area", "" } });
        }
    }

    public class FixtureAdminAttribute : BasketballAuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(IsAuthenticated(filterContext) && (MembershipService.IsFixtureAdmin(GetUser(filterContext)) || MembershipService.IsSiteAdmin(GetUser(filterContext)))))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "LogOn" }, { "controller", "Account" }, { "area", "" } });
        }
    }

    public class TeamAdminAttribute : BasketballAuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!(IsAuthenticated(filterContext) && (MembershipService.IsSiteAdmin(GetUser(filterContext)) || MembershipService.IsTeamAdmin(GetUser(filterContext)))))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "LogOn" }, { "controller", "Account" }, { "area", "" } });
        }
    }

    /// <summary>
    /// Controls access and sets roles in BaseViewModel
    /// </summary>
    public class BasketballAuthorizeAttribute : ActionFilterAttribute
    {
        // Not particularly happy about this being public
        [Inject]
        public IMembershipService MembershipService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            SetRolesInViewBag(filterContext);
            ValidateAuthenticatedUserHasRoles(filterContext);
        }

        private void ValidateAuthenticatedUserHasRoles(ActionExecutedContext filterContext)
        {
            // It's possible that authentication (cookies) could last longer than the roles stored in the session
            // so it we find a use is authenticated but has no roles, boot them the fuck out
            if (IsAuthenticated(filterContext) && MembershipService.GetRolesForUserFromSession(GetUser(filterContext)).Count == 0)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "LogOn" }, { "controller", "Account" }, { "area", "" } });
            }
        }

        public bool IsAuthenticated(ControllerContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }

        private void SetRolesInViewBag(ControllerContext context)
        {
            // All controllers must inherit from BaseController otherwise the ViewBag values with be null
            if (!(context.Controller.GetType().IsSubclassOf(typeof(BaseController))))
                throw new InvalidOperationException("Cannot call this method if the controller does not inherit from BaseController");
                
            //if (context.Controller.ViewData.Model == null)
              //  context.Controller.ViewData.Model = new BaseViewModel();

            //if(context.Controller.ViewData.GetType().IsSubclassOf(typeof(BaseViewModel)))

            // Setting ViewBag values is less than ideal but it's better than having a BaseViewModel which everything inherits from
            context.Controller.ViewBag.IsAdmin = MembershipService.IsSiteAdmin(GetUser(context));
            context.Controller.ViewBag.IsFixtureAdmin = MembershipService.IsFixtureAdmin(GetUser(context));
            context.Controller.ViewBag.IsTeamAdmin = MembershipService.IsTeamAdmin(GetUser(context));
        }

        protected string GetUser(ControllerContext context)
        {
            return context.HttpContext.User.Identity.Name;
        }

    }
}
