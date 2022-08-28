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
        [HttpPost]
        public IHttpActionResult Get(ServiceInfo serviceInfo = null)
        {
            // check if it exist

            // if already published

            // publish

            return Ok(serviceInfo);
        }
    }
}
