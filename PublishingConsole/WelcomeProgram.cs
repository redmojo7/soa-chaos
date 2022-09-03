using Authenticator;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Registry.DAO;
using Registry.Models;
using RestSharp;
using System;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PublishingConsole
{
    internal class WelcomeProgram
    {
        private RestClient client;
        private AuthServiceInterface foob;
        private bool successful;
        public WelcomeProgram()
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);

            successful = false;

            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string AUTH_URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, AUTH_URL);
            foob = foobFactory.CreateChannel();
        }

        internal void Welcome(string token)
        {
            successful = false;
            while (!successful)
            {
                Console.WriteLine("Welcom to the Service Publishing Console.\r");
                Console.WriteLine("------------------------\n");
                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tp - Publish");
                Console.WriteLine("\tu - Unpublish");
                Console.WriteLine("\tq - Quit");
                Console.Write("Your option? ");

                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "p":
                        Publish(token);
                        break;
                    case "u":
                        Unpublish(token);
                        break;
                    case "q":
                        successful = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Publish(string token)
        {
            string result = null;
            Console.WriteLine("Write service name: (Such as: ADDTwoNumbers)");
            var name = Console.ReadLine();
            Console.WriteLine("Enter service description: (Such as: Adding two Number)");
            var description = Console.ReadLine();
            Console.WriteLine("Write service API Endpoint: (Such as: https://localhost:44392/api/ADDTwoNumbers)");
            var apiEndpoint = Console.ReadLine();
            Console.WriteLine("Enter number Of operands: (Such as: 2)");
            var numberOfOperands = Console.ReadLine();
            Console.WriteLine("Enter operand type: (Such as: integer)");
            var operandType = Console.ReadLine();

            // Validate token
            foob.Validate(token, out result);
            if (result != "validated")
            {
                Console.WriteLine("\nDenied. Authentication Error");
                successful = true;
                return;
            }

            RestRequest restRequest = new RestRequest("api/publish", Method.Post);
            restRequest.AddHeader("token", token);
            ServiceInfo info = new ServiceInfo(name, description, new Uri(apiEndpoint), int.Parse(numberOfOperands), operandType);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(info));
            restRequest.AddHeader("Content-type", "application/json");
            RestResponse restResponse = client.Execute(restRequest);
            if (restResponse.IsSuccessful) 
            {
                Console.WriteLine("Service " + name + " has been published!");
            }
            else
            {
                Console.WriteLine("Error. Service " + name + " has not been published!");
            }
        }


        private void Unpublish(string token)
        {
            string result = null;
            Console.WriteLine("Write service API Endpoint: (Such as: https://localhost:44392/api/ADDTwoNumbers)");
            string apiEndpoint = Console.ReadLine();
            // Validate token
            foob.Validate(token, out result);
            if (result != "validated")
            {
                Console.WriteLine("\nDenied. Authentication Error");
                successful = true;
                return;
            }

            RestRequest restRequest = new RestRequest("api/unpublish", Method.Get);
            restRequest.AddHeader("token", token);
            restRequest.AddOrUpdateParameter("apiEndpoint", apiEndpoint);
            RestResponse restResponse = client.Execute(restRequest);

            if (restResponse.IsSuccessful)
            {
                Console.WriteLine("Service " + apiEndpoint + " has been unpublished!");
            }
            else
            {
                Console.WriteLine("Error. Service " + apiEndpoint + " has not been unpublished!");
            }
        }
    }
}