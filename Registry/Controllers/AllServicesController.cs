using Registry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Management;
using System.Web.Profile;

namespace Registry.Controllers
{
    [Route("api/AllServices")]
    public class AllServicesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            string serviceFilePath = Path.Combine(System.AppContext.BaseDirectory, "App_Data", "AllServices.txt");

            // Open the file to read from.
            string readText = File.ReadAllText(serviceFilePath);
            List<ServiceInfo> serviceInfos = ServiceInfo.FromJson(readText);
            return Ok(serviceInfos);
        }
    }
}
