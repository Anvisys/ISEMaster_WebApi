using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

using IESMater_WebAPI.Models;

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
        [HttpPost]
       
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


        [Route("GetAcademic/{UserID}")]
        [HttpPost]

        public IHttpActionResult GetAcademicProfile(int UserID)
        {
            try
            {
                var context = new xPenEntities();
                var profile = from u in context.IESAcademicProfiles
                              where u.UserID == UserID
                              select u;
             
                return Ok(profile);
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }

        [Route("AddAcademic")]
        [HttpPost]

        public IHttpActionResult PostProfile([FromBody]IESAcademicProfile profile)
        {
            try
            {
                var context = new xPenEntities();
                context.IESAcademicProfiles.Add(profile);
                context.SaveChanges();
                return Ok(profile);
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }

        [Route("{UserID}")]
        [HttpGet]

        public IHttpActionResult GetProfile(int UserID)
        {
            try
            {
                var context = new xPenEntities();

                var profile = (from p in context.ViewIESAcademicProfiles
                               where p.UserID == UserID
                               select p).First();
                context.SaveChanges();
                return Ok(profile);
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }


        [Route("SendOTP")]
        [HttpPost]

        public IHttpActionResult PostOTP([FromBody]OTP value)
        {
            try
            {
                string Message = "Register with IES Master using OTP " + value.String_OTP; 
                String result = Utility.sendSMS(Message,value.MobileNumber);
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