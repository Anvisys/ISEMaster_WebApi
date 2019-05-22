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
        public IEnumerable<IESCollege> GetCollege()
        {
            var context = new xPenEntities();
            var colleges = (from s in context.IESColleges
                            select s).ToList();
            return colleges;
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
                        College_Name = college_course.Name,
                        UnivID = college_course.univ_id
                    };
                    context.IESColleges.Add(college);
                    context.SaveChanges();

                    List<int> courses = college_course.Stream;
                    foreach (int i in courses)
                    {
                        context.IESCourses.Add(
                            new IESCourse
                            {
                                CollegeID = college.CollegeID,
                                StreamID = i
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



        // PUT: api/Colleges/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Colleges/5
        public void Delete(int id)
        {
        }
    }
}
