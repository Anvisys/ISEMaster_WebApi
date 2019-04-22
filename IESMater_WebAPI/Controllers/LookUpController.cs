using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;

namespace IESMater_WebAPI.Controllers
{
   

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api/Lookup")]
    public class LookUpController : ApiController
    {

        

        [Route("University/{UnivName}")]
        [HttpGet]
        public IEnumerable<IESUniversity> GetUniversity(String UnivName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESUniversities
                       where s.Name.Contains(UnivName)
                       select s).ToList();

            return soc;
        }

        [Route("College/{CollegeName}/{UniversityID}")]
        [HttpGet]
        public IEnumerable<IESCollege> Get(int UniversityID, String CollegeName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESColleges
                       where s.UnivID == UniversityID && s.College_Name.Contains(CollegeName)
                       select s).ToList();

            return soc;
        }

        [Route("Stream/{UniversityID}/{StreamName}")]
        [HttpGet]
        public IEnumerable<IESStream> GetStream(int UniversityID, String StreamName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESStreams
                       where s.UnivID == UniversityID && s.Stream_Name.Contains(StreamName)
                       select s).ToList();

            return soc;
        }

        [Route("Stream/{UniversityID}")]
        [HttpGet]
        public IEnumerable<IESStream> GetAllStream(int UniversityID, String StreamName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESStreams
                       where s.UnivID == UniversityID && s.Stream_Name.Contains(StreamName)
                       select s).ToList();

            return soc;
        }

     
    }
}
