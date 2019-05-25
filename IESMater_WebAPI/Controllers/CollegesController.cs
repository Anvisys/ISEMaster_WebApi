//colleges controller

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
    [RoutePrefix("api/Colleges")]
    public class CollegesController : ApiController
    {
        // GET: api/Colleges
        [Route("All")]
        [HttpGet]
        public IEnumerable<Object> GetCollege()
        {
            var context = new xPenEntities();
            var college = (from c in context.ViewIESColleges
                           group c by new { c.CollegeID, c.College_Name } into CollegeGroup
                           select new
                           {
                               CollegeID = CollegeGroup.Key.CollegeID,
                               collegeName = CollegeGroup.Key.College_Name,
                               StreamCount = CollegeGroup.Count()
                           });
      
            return college;
        }

        // GET: api/Colleges/5
        [Route("University/{univId}")]
        [HttpGet]
        public IEnumerable<IESCollege> Get(int univId)
        {
            var context = new xPenEntities();
            var college = (from s in context.IESColleges
                           where s.UnivID == univId
                           select s).ToList();
            return college;
        }

        [Route("New")]
        [HttpPost]
        public IHttpActionResult AddNewCollege([FromBody]IESCollege college)
        {
            try
            {
                var context = new xPenEntities();
                context.IESColleges.Add(college);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Route("NewWithStream")]
        [HttpPost]
        public IHttpActionResult AddNewCollegeWithStream([FromBody]College_Courses college_course)
        {
            try
            {
                var context = new xPenEntities();
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IESCollege college = new IESCollege
                    {
                        College_Name = college_course.CollegeName,
                        UnivID = college_course.UnivID
                    };
                    context.IESColleges.Add(college);
                    context.SaveChanges();

                    List<IESStream> courses = college_course.Streams;
                    foreach (IESStream i in courses)
                    {
                        context.IESCourses.Add(
                            new IESCourse
                            {
                                CollegeID = college.CollegeID,
                                StreamID = i.StreamID
                            }
                            );
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }



        //[Route("{CollegeID}")]
        //[HttpGet]
        //public IEnumerable<ViewIESStream> GetSemester(int collegeid )
        //{
        //    var context = new xPenEntities();
        //    var semester = (from s in context.ViewIESStreams
        //                    where s.CollegeID == collegeid
        //                   select s).ToList();
        //    return semester;
        //}

        // DELETE: api/Colleges/5
        public void Delete(int id)
        {
        }
    }
}
