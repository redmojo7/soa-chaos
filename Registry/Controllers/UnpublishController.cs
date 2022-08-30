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
        [HttpPut]
        public IHttpActionResult Unpublish(string serviceName = null)
        {
            Console.WriteLine(serviceName);
            serviceDAO.UnPublishService(serviceName);
            return Ok(serviceName);
        }
    }
}
