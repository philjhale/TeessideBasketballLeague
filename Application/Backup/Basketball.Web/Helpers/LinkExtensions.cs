using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Basketball.Web.Helpers
{
    public static class LinkExtensions
    {
        /// <summary>
        /// Outputs a link to the team information page
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">No need to encode</param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static MvcHtmlString TeamLink(this HtmlHelper htmlHelper,
            string linkText,
            int teamId)
        {
            return htmlHelper.ActionLink(htmlHelper.Encode(linkText), "View", "Teams", new { id = teamId }, new { title = "Click to view team information" });
        }

        /// <summary>
        /// Outputs a link to the team information page
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">No need to encode</param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static MvcHtmlString PlayerLink(this HtmlHelper htmlHelper,
            string linkText,
            int playerId)
        {
            return htmlHelper.ActionLink(linkText, "Player", "Stats", new { id = playerId }, new { title = "Click to view player stats" });
        }

        public static MvcHtmlString FixtureLink(this HtmlHelper htmlHelper,
            string linkText,
            int fixtureId)
        {
            return htmlHelper.ActionLink(htmlHelper.Encode(linkText), "ViewMatch", "Stats", new { id = fixtureId }, new { title = "Click to view fixture stats" });
        }
    }
}
