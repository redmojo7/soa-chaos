using Newtonsoft.Json;
using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PublishingConsole
{
    public class PublishServiceATO
    {
        private RestClient client;
        public PublishServiceATO()
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);

        }
        public void Register()
        { 
        
        }

        public void Login()
        {

        }

        public async Task PublishAsync(string name, string description, Uri apiEndpoint, int numberOfOperands, string operandType)
        {
            RestRequest restRequest = new RestRequest("api/publish", Method.Post);
            ServiceInfo info = new ServiceInfo(name, description, apiEndpoint,numberOfOperands, operandType);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(info));
            restRequest.AddHeader("Content-type", "application/json");
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
        }

        public async Task UnPublishAsync(string uriStr)
        {
            //Uri uri = new Uri(uriStr);
            RestRequest restRequest = new RestRequest("api/unpublish", Method.Put);
            restRequest.AddParameter("serviceName", uriStr);
            RestResponse restResponse = await client.ExecuteAsync(restRequest);
        }
    }
}
