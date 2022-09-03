using Authenticator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Registry.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PublishingConsole
{
    public class PublishServiceATO
    {
        private RestClient client;
        private AuthServiceInterface foob;
        public PublishServiceATO()
        {
            string URL = "https://localhost:44388/";
            client = new RestClient(URL);
            RestRequest restRequest = new RestRequest("api/AllServices", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);

            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string AUTH_URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, AUTH_URL);
            foob = foobFactory.CreateChannel();

        }
        public string Register(string userName, string password)
        {
            string result = null;
            foob.Register(userName, password, out result);
            return result;
        }

        public string Login(string userName, string password)
        {
            string result = null;
            foob.Login(userName, password, out result);
            return result;
        }
    }
}
