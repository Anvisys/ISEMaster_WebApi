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
    [RoutePrefix("api/Paper")]
    public class TestController : ApiController
    {


        [Route("{univID}/{streamID}/{semesterID}/{SubjectID}")]
        [HttpGet]
        public IEnumerable<ViewIESQuestionPaper> Get(int univID, int streamID, int semesterID ,int SubjectID)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.UnivID == univID && s.StreamID == streamID && s.SemID == semesterID
                       select s).ToList();
          
            return test;
        }

        [Route("All")]
        [HttpGet]
        public IHttpActionResult GetAllPapers()
        {
            try
            {
                var context = new xPenEntities();
                var test = (from s in context.ViewIESQuestionPapers
                            select s).ToList();
                return Ok(test);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }

        }


        [Route("{PaperID}")]
        [HttpGet]
        public IHttpActionResult GetPaperDetails(int PaperID)
        {
            try
            {
                var context = new xPenEntities();
                var test = (from s in context.ViewIESQuestionPapers
                            where s.PaperID == PaperID
                            select s).First();
                return Ok(test);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }

        }


        [Route("New")]
        [HttpPost]
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
        [HttpPost]
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
