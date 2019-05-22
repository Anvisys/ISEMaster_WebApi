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
        public IEnumerable<ViewIESQuestionPaper> Get(int univID, int streamID, int semesterID ,int SubjectID)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.UnivID == univID && s.StreamID == streamID && s.SemesterID == semesterID
                       select s).ToList();
          
            return test;
        }


        [Route("New")]
        [HttpGet]
        public IHttpActionResult PostNewTest([FromBody]IESQuestionPaper value)
        {
            try
            {
                var context = new xPenEntities();
                context.IESQuestionPapers.Add(value);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }

        }


        [Route("Order")]
        [HttpGet]
        public IHttpActionResult PostOrder([FromBody]IESOrder value)
        {
            try
            {
                var context = new xPenEntities();
                context.IESOrders.Add(value);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }

        }


        [Authorize]
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
