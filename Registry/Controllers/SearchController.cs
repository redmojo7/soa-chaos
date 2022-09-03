using Authenticator;
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
    [Route("api/search")]
    public class SearchController : ApiController
    {

        private ServiceDAO serviceDAO;
        public SearchController()
        {
            serviceDAO = new ServiceDAO();
        }
        [HttpGet]
        public IHttpActionResult Get(string key = "")
        {
            string result = serviceDAO.ValidateToken(this.Request.Headers);
            if (result != "validated")
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new BadInfoDTO("Denied", "Authentication Error")));
            }
            ServiceDAO serviceATO = new ServiceDAO();
            List<ServiceInfo> services = serviceATO.SearchService(key);
            return Json<List<ServiceInfo>>(services);
        }
    }
}
