using Authenticator;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PublishingConsole
{
    public class LoginProgram
    {
        private WelcomeProgram welcome;
        private AuthServiceInterface foob;
        private PublishServiceATO publishServiceATO;
        private string token;
        public LoginProgram()
        {
            welcome = new WelcomeProgram();
            publishServiceATO = new PublishServiceATO();
        }

        internal void Login()
        {
            bool successful = false;
            while (!successful)
            {
                string result = null;
                Console.WriteLine("Write your username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = PasswordUtil.GetPassword();
                // login
                result = publishServiceATO.Login(username, password);
                if (result != null && result.Length > 0)
                {
                    Console.WriteLine("\nYou have successfully logged in !!!");
                    token = result;
                    successful = true;
                    welcome.Welcome(token);
                }
                else 
                {
                    Console.WriteLine("\nYour username or password is incorect, try again !!!");
                }
            }
        }
    }
}
