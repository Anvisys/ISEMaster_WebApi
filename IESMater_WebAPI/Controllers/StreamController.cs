using IESMater_WebAPI.Models;
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
    [RoutePrefix("api/stream")]
    public class StreamController : ApiController
    {

        [Route("All")]
        [HttpGet]
        // GET: api/Stream
        public IHttpActionResult GetIESStream()
        {
            try
            {
                var context = new xPenEntities();
                var stream = from s in context.ViewIESStreams
                             group s by new { s.StreamID, s.StreamName } into newgroup
                             select new
                             {
                                 StreamName = newgroup.Key.StreamName,
                                 StreamID = newgroup.Key.StreamID,
                                 CollegeCount = newgroup.Select(c => c.CollegeID).Distinct().Count()
                             };


                return Ok(stream.ToList());
            }
            catch (Exception ex)
            {
                var response = new CustomResponse();
                response.Response = "Server Error";
                return InternalServerError(ex.InnerException);
            }
        }



        [Route("Colleges/{CollegeID}")]
        [HttpGet]
        // GET: api/Stream/5
        public IEnumerable<ViewIESStream> GetStreamofCollege(int collegeid)
        {
            var context = new xPenEntities();
            var stream = (from s in context.ViewIESStreams
                            where s.CollegeID==collegeid
                            select s).ToList();
            return stream;
        }

        [Route("{StreamID}")]
        [HttpGet]
        // GET: api/Stream/5

            //Get Semester Of a Stream
        public IEnumerable<ViewIESSemester> GetSemestesofStream(int streamid)
        {
            var context = new xPenEntities();
            var semesters = (from s in context.ViewIESSemesters
                          where s.StreamID == streamid
                          select s).ToList();
            return semesters;
        }

        [Route("New")]
        [HttpPost]
        // POST: api/Stream
        public IHttpActionResult AddNewUniversity([FromBody]IESStream newstream)
        {
            try
            {
                var context = new xPenEntities();
                context.IESStreams.Add(newstream);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        // PUT: api/Stream/5
       

        // DELETE: api/Stream/5
        public void Delete(int id)
        {
        }
    }
}
