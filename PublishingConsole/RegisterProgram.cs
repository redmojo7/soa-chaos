using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PublishingConsole
{
    public class RegisterProgram
    {
        private WelcomeProgram welcome;
        public RegisterProgram()
        {
            welcome = new WelcomeProgram();
        }

        internal void Register()
        {
            bool successfull = false;
            while (!successfull)
            {
                Console.WriteLine("Enter your username:");
                var username = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                SecureString password = PasswordUtil.GetPassword();
                Console.WriteLine(password);

                if (username != "")
                {
                    Console.WriteLine("You have successfully registered !!!");
                    //Console.ReadLine();
                    successfull = true;
                    //break;
                }
            }
            welcome.Welcome();
        }
    }
}
