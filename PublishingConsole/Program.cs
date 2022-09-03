using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PublishingConsole
{
    internal class Program
    {
        private static RegisterProgram register;
        private static LoginProgram login;
        public Program()
        {

        }

        static void Main(string[] args)
        {

            register = new RegisterProgram();
            login = new LoginProgram();

            bool successful = false;
            while (!successful) 
            {
                //Start:
                Console.WriteLine("Welcom to the Service Publishing Console.\r");
                Console.WriteLine("------------------------\n");
                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tl - Login");
                Console.WriteLine("\tr - Registration");
                Console.Write("Your option? ");

                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "l":
                        login.Login();
                        break;
                    case "r":
                        register.Register();
                        break;
                    default:
                        break;
                }

            }


            Console.WriteLine("System Online");
            Console.ReadLine();
        }
    }
}
