using System.Web;
using System.Web.SessionState;

namespace Basketball.Web.Areas.Admin.Extensions
{
    public static class AdminSessionExtensions
    {
         public static int GetPlayerTeamIdFilter(this HttpSessionStateBase session)
         {
             if(session["PlayerTeamIdFilter"] != null)
                 return (int)session["PlayerTeamIdFilter"];

             return -1;
         }

         public static void SetPlayerTeamIdFilter(this HttpSessionStateBase session, int teamId)
         {
             session["PlayerTeamIdFilter"] = teamId;
         }
    }
}