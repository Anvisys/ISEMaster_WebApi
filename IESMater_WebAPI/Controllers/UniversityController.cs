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
    [RoutePrefix("api/university")]
    public class UniversityController : ApiController
    {
        // GET: api/University
        [Route("All")]
        [HttpGet]
        public IEnumerable<IESUniversity> GetUniversityAll()
        {
            var context = new xPenEntities();
            var university = (from s in context.IESUniversities
                              select s).ToList();
            return university;
        }

        // GET: api/University/5
        [Route("New")]
        [HttpPost]
        public IHttpActionResult Get([FromBody]IESUniversity university)
        {
            try
            {
                var context = new xPenEntities();
                context.IESUniversities.Add(university);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
          //  return "value";
        }

        // POST: api/University
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/University/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/University/5
        public void Delete(int id)
        {
        }
    }
}
