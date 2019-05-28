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
        [Route("College")]
        [HttpGet]
        public IEnumerable<Object> GetUniversityByPaper()
        {
            var context = new xPenEntities();
            var subjects = (from s in context.ViewIESQuestionPapers
                            group s by new { s.UnivID, s.UniversityName, s.CollegeID, s.CollegeName } into unilist
                            select new { unilist.Key }).ToList();
            return subjects;
        }

        [Route("College/{CollegeID}")]
        [HttpGet]
        public IEnumerable<Object> GetStreamByPaper(int CollegeID)
        {
            var context = new xPenEntities();
            var subjects = (from s in context.ViewIESQuestionPapers
                            where s.CollegeID == CollegeID
                            group s by new { s.StreamID, s.StreamName } into streamlist
                            select new { streamlist.Key }).ToList();
            return subjects;
        }



        [Route("{CollegeID}/{StreamID}")]
        [HttpGet]
        public IEnumerable<Object> GetSubjectsfromQuestionPaper(int CollegeID, int StreamID)
        {
            var context = new xPenEntities();
            var subjects = (from s in context.ViewIESQuestionPapers
                        
                            where s.CollegeID == CollegeID && s.StreamID == StreamID
                            group s by s.SubjectName into subjectlist
                            select new { subjectlist.Key }).ToList();
            return subjects;
        }

        [Route("{collegeID}/{streamID}/{SubjectName}")]
        [HttpGet]
        public IEnumerable<Object> GetYearsfromQuestionPaper(int collegeID, int streamID, String SubjectName)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.CollegeID == collegeID && s.StreamID == streamID && s.SubjectName == SubjectName
                        group s by s.Year into yearGroup
                        select new { yearGroup.Key }).ToList();

            return test;
        }


        [Route("{collegeID}/{streamID}/{SubjectName}/{Year}")]
        [HttpGet]
        public IEnumerable<Object> GetUnitforProfile(int collegeID, int streamID, String SubjectName, int Year)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.CollegeID == collegeID && s.StreamID == streamID && s.SubjectName == SubjectName && s.Year == Year
                        group s by s.Unit into unitGroup
                        select new { Unit = unitGroup.Key }).ToList();

            return test;
        }

        [Route("{collegeID}/{streamID}/{SubjectName}/{Year}/{Unit}")]
        [HttpGet]
        public IEnumerable<Object> GetQuestionforProfile(int collegeID, int streamID, String SubjectName, int Year, int Unit)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.CollegeID == collegeID && s.StreamID == streamID 
                        && s.SubjectName == SubjectName && s.Year == Year && s.Unit == Unit
                        select s).ToList();

            return test;
        }

        //[Route("{univID}/{streamID}/{semesterID}/{SubjectID}")]
        //[HttpGet]
        //public IEnumerable<ViewIESQuestionPaper> Get(int univID, int streamID, int semesterID ,int SubjectID)
        //{
        //    var context = new xPenEntities();
        //    var test = (from s in context.ViewIESQuestionPapers
        //                where s.UnivID == univID && s.StreamID == streamID && s.SemID == semesterID
        //               select s).ToList();

        //    return test;
        //}

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


        [Route("Paper/{PaperID}")]
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
