using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Basketball.Common.ModelMetaData;
using Basketball.Service.Configuration;
using MvcSiteMapProvider.Web;

namespace Basketball
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("StandingsGet", "Standings/{filterBySeasonId}", new { controller = "Stats", action = "StandingsGet" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[] { "Basketball.Web.Controllers" }
            );

            //routes.MapRoute(
            //    "MobileDefault", // Route name
            //    "Mobile/{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
            //    new string[] { "Basketball.Web.Areas.Mobile.Controllers" }
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            XmlSiteMapController.RegisterRoutes(RouteTable.Routes);

            MvcHandler.DisableMvcResponseHeader = true;

            // http://haacked.com/archive/2011/07/14/model-metadata-and-validation-localization-using-conventions.aspx
            ModelMetadataProviders.Current = new ConventionalModelMetadataProvider(false);

            // Allows cache to be cleared programmicatically
            // http://stackoverflow.com/questions/11585/clearing-page-cache-in-asp-net/2876701#2876701
            HttpRuntime.Cache.Insert("Pages", DateTime.Now);

            // This doesn't seem to work. Just fires randomly
            //ScheduledTaskService.AddCheckForLateMatchResultsTask();

            EntityFrameworkConfiguration.SetInitializer();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        // Without this method it seems that IsNewSession always returns true. How odd
        protected void Session_Start()
        {
        }

    }
}