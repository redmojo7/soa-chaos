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
    [Route("api/publish")]
    public class PublishController : ApiController
    {
        private ServiceDAO serviceDAO;
        public PublishController()
        {
            serviceDAO = new ServiceDAO();
        }

        [HttpPost]
        public IHttpActionResult Publish(ServiceInfo serviceInfo)
        {
            serviceDAO.PublishService(serviceInfo);
            Console.WriteLine(serviceInfo.ToString());

            return Ok();
        }
    }
}