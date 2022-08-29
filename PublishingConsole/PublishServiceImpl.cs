using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PublishingConsole
{
    public class PublishServiceImpl : PublishServiceInterface
    {
        private RestClient client;
        public PublishServiceImpl()
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
        }

        public void Login(in string UserName, in string Password)
        {
            throw new NotImplementedException();
        }

        public void PublishService(in string Name, in string Description, in Uri ApiEndpoint, in int NumberOfOperands, in string OperandType)
        {
            ServiceInfo serviceInfo = new ServiceInfo(Name, Description, ApiEndpoint, NumberOfOperands, OperandType);   
            RestRequest restRequest = new RestRequest("api/publish", Method.Post);
            restRequest.AddHeader("Content-type", "application/json");
            restRequest.AddBody(serviceInfo);
            client.Execute(restRequest);
        }

        public void Registration(in string UserName, in string Password)
        {
            throw new NotImplementedException();
        }

        public void UnPublishService(in Uri uri)
        {
            RestRequest restRequest = new RestRequest("api/unpublish", Method.Put);
            restRequest.AddHeader("Content-type", "application/json");
            restRequest.AddBody(uri);
            client.Execute(restRequest);
        }
    }
}
