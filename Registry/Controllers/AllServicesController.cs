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
        public AllServicesController()
        {
            serviceDAO = new ServiceDAO();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            string result = serviceDAO.ValidateToken(this.Request.Headers);
            if (result != "validated")
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
            }
            List<ServiceInfo> serviceInfos = serviceDAO.GetAllService();
            return Json<List<ServiceInfo>>(serviceInfos);
        }
    }
}
