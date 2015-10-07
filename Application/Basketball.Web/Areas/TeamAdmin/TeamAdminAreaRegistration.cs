using System.Web.Mvc;

namespace Basketball.Web.Areas.TeamAdmin
{
    public class TeamAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TeamAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TeamAdmin_default",
                "TeamAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
