using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PublishingConsole
{
    public class LoginProgram
    {
        WelcomeProgram welcome;
        public LoginProgram()
        {
            welcome = new WelcomeProgram();
        }

        internal void Login()
        {
            bool successfull = false;
            while (!successfull)
            {
                Console.WriteLine("Write your username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                SecureString password = PasswordUtil.GetPassword();
                Console.WriteLine(password);
                if (username != "")
                {
                    Console.WriteLine("You have successfully logged in !!!");
                    //Console.ReadLine();
                    successfull = true;
                    //break;
                }

                if (!successfull)
                {
                    Console.WriteLine("Your username or password is incorect, try again !!!");
                }
                welcome.Welcome();
            }
        }
    }
}
