using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    [Route("api/AddTwoNumbers")]
    public class ADDTwoNumbersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int operand1 = 0, int operand2 = 0)
        {
            return Ok(operand1 + operand2);
        }
    }
}
