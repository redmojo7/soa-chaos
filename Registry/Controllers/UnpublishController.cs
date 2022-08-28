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
        [HttpGet]
        public IHttpActionResult Get(string serviceName = null)
        {
            return Ok(serviceName);
        }
    }
}
