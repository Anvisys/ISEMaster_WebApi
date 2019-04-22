using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;


namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [Route("{Email}")]
        [HttpGet]
        public IESUserProfile GetUserProfile(String Email)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESUserProfiles
                       where s.Email == Email
                       select s).FirstOrDefault();

            return soc;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Register")]
        [HttpGet]
        public IHttpActionResult Post([FromBody]IESUserProfile profile)
        {
            try
            {
                var context = new xPenEntities();
                context.IESUserProfiles.Add(profile);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}