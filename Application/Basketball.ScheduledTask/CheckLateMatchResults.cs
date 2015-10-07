using System;
using System.Net;

namespace Basketball.ScheduledTask
{
    class CheckLateMatchResults
    {
        static void Main(string[] args)
        {
            string lateMatchResultResponse = new WebClient().DownloadString("http://www.teessidebasketball.co.uk/ScheduledTask/CheckLateMatchResults");

            Console.Out.WriteLine(lateMatchResultResponse);
        }
    }
}
