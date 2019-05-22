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
        public IEnumerable<ViewIESStream> GetIESStream()
        {
            var context = new xPenEntities();
            var stream = (from s in context.ViewIESStreams
                          select s).ToList();
            return stream;
        }

        // GET: api/Stream/5
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Stream/5
        public void Delete(int id)
        {
        }
    }
}
