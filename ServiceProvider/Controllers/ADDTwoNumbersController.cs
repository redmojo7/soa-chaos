using Authenticator;
using RegServiceProvider.DAO;
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
        private ServiceDAO serviceDAO;
        public ADDTwoNumbersController()
        {
            serviceDAO = new ServiceDAO();
        }
        [HttpGet]
        public IHttpActionResult Get(int operand1 = 0, int operand2 = 0)
        {
            string result = serviceDAO.ValidateToken(this.Request.Headers);
            if (result != "validated")
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
            }
            return Ok(operand1 + operand2);
        }
    }
}
