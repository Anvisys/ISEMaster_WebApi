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
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {


        [Route("{univID}/{streamID}/{semesterID}/{SubjectID}")]
        [HttpGet]
        public IEnumerable<IESSubjectiveTest> Get(int univID, int streamID, int semesterID ,int SubjectID)
        {
            var context = new xPenEntities();
            var test = (from s in context.IESSubjectiveTests
                        where s.UnivID == univID && s.StreamID == streamID && s.SemID == semesterID
                       select s).ToList();
          
            return test;
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
