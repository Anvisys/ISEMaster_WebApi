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
    [RoutePrefix("api/subject")]
    public class SubjectController : ApiController
    {
        [Route("All")]
        [HttpGet]
        // GET: api/Subject
        public IEnumerable<IESSubject> GetSubjects()
        {
            var context = new xPenEntities();
            var subjects = (from s in context.IESSubjects
                            select s).ToList();
            return subjects;
        }



        [Route("New")]
        [HttpPost]
        // POST: api/Subject
        public IHttpActionResult AddNewSubject([FromBody]IESSubject subject)
        {
            try
            {
                var context = new xPenEntities();
                context.IESSubjects.Add(subject);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {

                return InternalServerError();
            }
        }

        // PUT: api/Subject/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Subject/5
        public void Delete(int id)
        {
        }
    }
}
