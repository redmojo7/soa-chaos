using Microsoft.Win32;
using Newtonsoft.Json;
using Registry.Models;
using RestSharp;
using System;
using System.Net;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PublishingConsole
{
    internal class WelcomeProgram
    {
        private RestClient client;
        public WelcomeProgram()
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
        }

        internal void Welcome()
        {
            bool successfull = false;
            while (!successfull)
            {
                Console.WriteLine("Welcom to the Service Publishing Console.\r");
                Console.WriteLine("------------------------\n");
                // Ask the user to choose an option.
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tp - Publish");
                Console.WriteLine("\tu - Unpublish");
                Console.Write("Your option? ");

                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "p":
                        PublishAsync();
                        break;
                    case "u":
                        UnpublishAsync();
                        break;
                    default:
                        break;
                }
            }
        }

        private async void PublishAsync()
        {
            Console.WriteLine("Write service name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter service description:");
            var description = Console.ReadLine();
            Console.WriteLine("Write service API Endpoint:");
            var apiEndpoint = Console.ReadLine();
            Console.WriteLine("Enter number Of operands:");
            var numberOfOperands = Console.ReadLine();
            Console.WriteLine("Enter operand type:");
            var operandType = Console.ReadLine();


            RestRequest restRequest = new RestRequest("api/publish", Method.Post);
            ServiceInfo info = new ServiceInfo(name, description, new Uri(apiEndpoint), int.Parse(numberOfOperands), operandType);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(info));
            restRequest.AddHeader("Content-type", "application/json");
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
        }


        private async void UnpublishAsync()
        {
            Console.WriteLine("Write service API Endpoint:");
            string apiEndpoint = Console.ReadLine();

            RestRequest restRequest = new RestRequest("api/unpublish", Method.Get);
            restRequest.AddOrUpdateParameter("apiEndpoint", apiEndpoint);
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
        }
    }
}