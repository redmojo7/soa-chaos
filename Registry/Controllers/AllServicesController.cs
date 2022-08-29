using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Registry.DAO;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [HttpGet]
        public IHttpActionResult Get()
        {
            ServiceDAO serviceDAO = new ServiceDAO();
            List<ServiceInfo> serviceInfos = serviceDAO.GetAllService();
            //return Ok(serviceInfos);
            return Json<List<ServiceInfo>>(serviceInfos);
        }
    }
}
