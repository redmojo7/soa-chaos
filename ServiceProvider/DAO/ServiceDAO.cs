using Authenticator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Web;

namespace RegServiceProvider.DAO
{
    public class ServiceDAO
    {
        private static string serviceFilePath = Path.Combine(System.AppContext.BaseDirectory, "App_Data", "AllServices.txt");

        private AuthServiceInterface foob;
        public ServiceDAO()
        {
            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        internal string ValidateToken(HttpRequestHeaders headers)
        {
            string result = null;
            string token = string.Empty;
            if (headers.Contains("token"))
                token = headers.GetValues("token").First();
                foob.Validate(token, out result);
            return result;
        }
    }
}