using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authenticator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hey, welcome to Auth server");
            // This is the actual host service system
            ServiceHost host;
            // This represents a tcp/ip binding in the Windows network stack
            NetTcpBinding tcp = new NetTcpBinding();
            // Bind server to the implementation of DataServer
            host = new ServiceHost(typeof(AuthServiceImpl));
            /* Present the publicly accessible interface to the client. 0.0.0.0 tells .net to
             accept on any interface. :8100 means this will use port 8100. DataService is a name for the
             actual service, this can be any string.*/

            host.AddServiceEndpoint(typeof(AuthServiceInterface), tcp, "net.tcp://0.0.0.0:8100/AuthService");
            // And open the host for business!
            host.Open();
            Console.WriteLine("System Online");

            // set clearing token task
            SetScheduleTask();
            ConsoleKeyInfo cki;

            // https://docs.microsoft.com/en-us/dotnet/api/system.console.keyavailable?redirectedfrom=MSDN&view=net-6.0#System_Console_KeyAvailable
            do
            {
                Console.WriteLine("\nPress a key to display; press the 'x' key to quit.");

                // Your code could perform some useful task in the following loop. However,
                // for the sake of this example we'll merely pause for a quarter second.

                while (Console.KeyAvailable == false)
                    Thread.Sleep(250); // Loop until input is entered.

                cki = Console.ReadKey(true);
                Console.WriteLine("You pressed the '{0}' key.", cki.Key);
            } while (cki.Key != ConsoleKey.X);


            // Don't forget to close the host after you're done!
            host.Close();
            Console.ReadLine();
        }

        private static void SetScheduleTask()
        {
            bool successful = false;
            while (!successful)
            {
                Console.WriteLine("\nPlease set the execution period for clearing the token (unit: minutes) :\t");
                var minutesString = Console.ReadLine();
                long intervalMinutes = 0;
                if (long.TryParse(minutesString, out intervalMinutes))
                {
                    // intervalMinutes*60*1000 set as milliseconds
                    ScheduledTask.Schedule_Timer(intervalMinutes * 60 * 1000);
                    successful = true;
                }
            }
        }

    }
}
