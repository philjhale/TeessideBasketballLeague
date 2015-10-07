using System.Collections.Generic;
using System.Web;
using Basketball.Data.Interfaces;

namespace Basketball.Data
{
	public class SessionRepository : ISessionRepository
	{
		public List<string> Roles()
		{
			return (List<string>)HttpContext.Current.Session["roles"];
		}
		
		public void Roles(List<string> roles)
		{
			HttpContext.Current.Session["roles"] = roles;
		}

        public string GetLoggedInUsername()
        {
            return HttpContext.Current.User.Identity.Name;
        }
	}
}