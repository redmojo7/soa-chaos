using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Authenticator
{
    internal class ScheduledTask
    {
        private static Timer checkForTime;

        private static long intervalMS;

        private static long logNumber = 0;

        private static AuthServiceInterface authService;

        internal static void Schedule_Timer(long intervalMilliseconds)
        {
            intervalMS = intervalMilliseconds;
            checkForTime = new Timer(intervalMS);
            Console.WriteLine("### Timer Started ###");
            checkForTime.Elapsed += new ElapsedEventHandler(CheckForTime_Elapsed);
            checkForTime.Start();

            authService = new AuthServiceImpl();
        }

        static void CheckForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Console.WriteLine(string.Format("\n[task-{0}][{1}:] {2}", logNumber, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt"), "### Timer Stopped ###"));
            checkForTime.Stop();
            Console.WriteLine("### Scheduled Task Started ###");
            Console.WriteLine("Performing scheduled task: Clearing token...");
            authService.ClearingToken();

            System.Console.WriteLine(string.Format("[task-{0}][{1}:] {2}", logNumber, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt"), "### Task Finished ###\n"));
            logNumber++;
            Schedule_Timer(intervalMS);
        }
    }
}
