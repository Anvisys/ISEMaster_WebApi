using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;

using IESMater_WebAPI.Models;

namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        // GET: api/Class
        //[Route()]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [Route("{CollegeID}/{StreamID}/{Semester}")]
        [HttpGet]
        public IEnumerable<ViewIESSubject> GetSemesterID(int collegeID, int streamID, int Semester)
        {
            var context = new xPenEntities();
            var subject = (from s in context.ViewIESSubjects
                           where s.CollegeID == collegeID && s.StreamID == streamID && s.SemesterID == Semester
                             select s);
            return subject;

        }

        //[Route("SubjectID/{ClassID}")]
        //[HttpGet]
        //public IEnumerable<IESClass_Subject> GetSubjectsIDForClass(int classID)
        //{
        //    var context = new xPenEntities();
        //    var subjectID = (from s in context.IESClass_Subject
        //                   where s.ClassID == classID
        //                   select s).ToList();
        //    return subjectID;
        //}


        //[Route("Subject/{SubjectID}")]
        //[HttpGet]
        //public IEnumerable<IESSubject> GetSubjectsForClass(int subjectID)
        //{
        //    var context = new xPenEntities();
        //    var subject = (from s in context.IESSubjects
        //                   where s.SubjectID == subjectID
        //                   select s).ToList();
        //    return subject;
        //}

        // POST: api/Class
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Class/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Class/5
        public void Delete(int id)
        {
        }
    }
}
