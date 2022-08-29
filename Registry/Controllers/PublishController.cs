using Registry.DAO;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registry.Controllers
{
    [Route("api/Publish")]
    public class PublishController : ApiController
    {
        //[HttpPost]
        //public IHttpActionResult Get(ServiceInfo serviceInfo = null)
        [HttpGet]
        public IHttpActionResult Get()
        {
            // check if it exist

            // if already published

            // publish
            ServiceInfo serviceInfo = new ServiceInfo("ADDTwoNumbers", "Adding two Number", new Uri("https://localhost:44392/ADDTwoNumbers"), 2, "integer");
            ServiceDAO serviceDAO = new ServiceDAO();
            serviceDAO.PublishService(serviceInfo);

            return Ok(new {Consumes = "application/json" , Values = serviceInfo});
        }
    }
}