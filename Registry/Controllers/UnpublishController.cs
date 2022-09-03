using Authenticator;
using Registry.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registry.Controllers
{
    [Route("api/unpublish")]
    public class UnpublishController : ApiController
    {
        private ServiceDAO serviceDAO;
        public UnpublishController()
        {
            serviceDAO = new ServiceDAO();
        }
        [HttpGet]
        public IHttpActionResult Unpublish(string apiEndpoint)
        {
            string result = serviceDAO.ValidateToken(this.Request.Headers);
            if (result != "validated")
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
            }
            Console.WriteLine("Unpublish: ", apiEndpoint);
            serviceDAO.UnPublishService(apiEndpoint);
            return Ok();
        }
    }
}
