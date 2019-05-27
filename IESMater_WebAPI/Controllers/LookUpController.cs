using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.Entity;
using IESMater_WebAPI.Models;


namespace IESMater_WebAPI.Controllers
{

   

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api/Lookup")]
    public class LookUpController : ApiController
    {

        [Route("Universities")]
        [HttpGet]
        public IEnumerable<IESUniversity> GetUniversities()
        {
            Utility.log("Reading University List");

            var context = new xPenEntities();
            var soc = (from s in context.IESUniversities
                       select s).ToList();
            Utility.log("Found " + soc.Count);

            return soc;
        }

        [Route("University/{UnivName}")]
        [HttpGet]
        public IEnumerable<IESUniversity> GetFilterUniversity(String UnivName)
        {
            Utility.log("Reading University Name");
                
            var context = new xPenEntities();
            var soc = (from s in context.IESUniversities
                       where s.UniversityName.Contains(UnivName)
                       select s).ToList();
            Utility.log("Found " + soc.Count);

            return soc;
        }

        [Route("College/{UniversityID}/{CollegeName}")]
        [HttpGet]
        public IEnumerable<IESCollege> Get(int UniversityID, String CollegeName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESColleges
                       where s.UnivID == UniversityID && s.CollegeName.Contains(CollegeName)
                       select s).ToList();

            return soc;
        }

        [Route("Stream/{UniversityID}/{StreamName}")]
        [HttpGet]
        public IEnumerable<ViewIESStream> GetStream(int UniversityID, String StreamName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.ViewIESStreams
                       where s.UnivID == UniversityID && s.StreamName.Contains(StreamName)
                       select s).ToList();

            return soc;
        }

        [Route("Stream/{UniversityID}")]
        [HttpGet]
        public IEnumerable<ViewIESStream> GetAllStream(int UniversityID, String StreamName)
        {
            var context = new xPenEntities();
            var soc = (from s in context.ViewIESStreams
                       where s.UnivID == UniversityID && s.StreamName.Contains(StreamName)
                       select s).ToList();

            return soc;
        }

     
    }
}
