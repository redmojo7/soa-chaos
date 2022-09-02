using Authenticator;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Registry.DAO;
using Registry.DTO;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Management;
using System.Web.Profile;

namespace Registry.Controllers
{
    [Route("api/AllServices")]
    public class AllServicesController : ApiController
    {
        private ServiceDAO serviceDAO;

        private AuthServiceInterface foob;
        public AllServicesController()
        {
            serviceDAO = new ServiceDAO();

            ChannelFactory<AuthServiceInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/AuthService";
            foobFactory = new ChannelFactory<AuthServiceInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            HttpRequestHeaders headers = this.Request.Headers;
            string token = string.Empty;
            if (headers.Contains("token"))
            {
                string result = null;
                token = headers.GetValues("token").First();
                Console.WriteLine(token);
                foob.Validate(token, out result);
                if (result != "validated")
                {
                    string bad = JsonConvert.SerializeObject(new BadInfoDTO("Denied", "Authentication Error"));
                    //return BadRequest(new BadInfoDTO("Denied", "Authentication Error"));
                    //return BadRequest(bad);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
                }
            }
            List<ServiceInfo> serviceInfos = serviceDAO.GetAllService();
            //return Ok(serviceInfos);
            return Json<List<ServiceInfo>>(serviceInfos);
        }
    }
}
