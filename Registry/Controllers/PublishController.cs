using Registry.DAO;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Authenticator;

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
            string result = serviceDAO.ValidateToken(this.Request.Headers);
            if (result != "validated")
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
            }
            serviceDAO.PublishService(serviceInfo);
            Console.WriteLine(serviceInfo.ToString());

            return Ok();
        }
    }
}