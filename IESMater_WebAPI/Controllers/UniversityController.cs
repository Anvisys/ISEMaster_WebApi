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
        public IEnumerable<Object> GetUniversityAll()
        {
            var context = new xPenEntities();
          

            var University = from u in context.IESUniversities
                             join c in context.IESColleges
                               on u.UnivID equals c.UnivID into pj
                             from sub in pj.DefaultIfEmpty()
                             select new { u.UnivID, u.UniversityName, sub.CollegeID };

            var uni = (from c in University
                       group c by new { c.UnivID, c.UniversityName} into UnivGroup
                       select new
                       {
                           UniversityName = UnivGroup.Key.UniversityName,
                           UnivID = UnivGroup.Key.UnivID,
                           CollegeCount = UnivGroup.Count()
                       });
            return uni;
        }

        [Route("collegeList")]
        [HttpGet]
        public IEnumerable<Object> GetcollegeList()
        {
            var context = new xPenEntities();
            var collegeList = (from s in context.ViewIESStudentCollegeCounts
                               select s).ToList();
            return collegeList;
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
