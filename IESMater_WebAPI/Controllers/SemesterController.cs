using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;

namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Semester")]
    public class SemesterController : ApiController
    {
        [Route("All")]
        [HttpGet]
        // GET: api/Semester
        public IEnumerable<Object> Get()
        {
            var context = new xPenEntities();
            var stream = (from q in context.IESSemesters
                           select q).ToList();
            return stream;
        }

        // GET: api/Semester/5
        public string Get(int id)
        {
            return "value";
        }

      
    }
}
