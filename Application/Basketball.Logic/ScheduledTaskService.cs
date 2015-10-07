using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Caching;

using Basketball.Common.Util;

namespace Basketball.Service
{
    // http://blog.stackoverflow.com/2008/07/easy-background-tasks-in-aspnet/
    public static class ScheduledTaskService
    {
        // Not really bothered about hardcoding values. They aren't going to change
        private static CacheItemRemovedCallback OnCacheRemove = null;
        private const int twentyFourHoursInSeconds = 86400;
        private const string lateMatchResultEmailSubject = "TBL check late match results";
        private const string lateMatchResultEmailAddress = "philjhale@gmail.com";
        private const string lateMatchResultUrl = "http://www.teessidebasketball.co.uk/ScheduledTask/CheckLateMatchResults";

        public static void AddCheckForLateMatchResultsTask()
        {
            #if !DEBUG
                AddTask("CheckForLateMatchResults", twentyFourHoursInSeconds);
            #endif
        }

        private static void AddTask(string name, int seconds)
        {
            OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(name, seconds, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        private static void CacheItemRemoved(string taskName, object expiresSeconds, CacheItemRemovedReason reason)
        {
            RunLateMatchResultCheckAndEmailResults();

            AddTask(taskName, Convert.ToInt32(expiresSeconds));
        } 

        private static void RunLateMatchResultCheckAndEmailResults()
        {
            WebRequest getUrl = WebRequest.Create(lateMatchResultUrl);
            Stream objStream = getUrl.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);
            string response = objReader.ReadToEnd();

            Email resultsEmail = new Email(false, lateMatchResultEmailAddress);
            resultsEmail.Send(lateMatchResultEmailSubject, response);
        }
    }
}
