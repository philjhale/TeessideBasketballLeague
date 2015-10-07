using System.Web.Mvc;
using System.Web.WebPages;

using Basketball.Web.BaseTypes;

namespace Basketball.Web.Controllers
{
    public class MobileDesktopSiteSwitcherController : BaseController
    {

        public RedirectResult SwitchSite(bool mobile, string returnUrl)
        {
            if (Request.Browser.IsMobileDevice == mobile)
            {
                HttpContext.ClearOverriddenBrowser();
            }
            else
            {
                HttpContext.SetOverriddenBrowser(mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);
            }

            return Redirect(returnUrl);
        }

    }
}