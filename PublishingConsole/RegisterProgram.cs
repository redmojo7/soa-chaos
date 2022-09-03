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
        private PublishServiceATO publish;
        public RegisterProgram()
        {
            welcome = new WelcomeProgram();
            publish =  new PublishServiceATO();
        }

        internal void Register()
        {
            bool successful = false;
            while (!successful)
            {
                string result = null;
                Console.WriteLine("Enter your username:");
                var username = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                string password = PasswordUtil.GetPassword();

                result = publish.Register(username, password);
                if (result == "successfully register")
                {
                    Console.WriteLine("\nYou have successfully registered !!!");
                    Console.WriteLine("Please login now.");
                }
                else if(result == "already registered")
                {
                    Console.WriteLine("\nThat username is taken. Try another.");
                }
            }
            successful = true;
        }
    }
}
