using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registry.Controllers
{
    [Route("api/search")]
    public class SearchController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string key = null)
        {
            return Ok(key);
        }
    }
}
