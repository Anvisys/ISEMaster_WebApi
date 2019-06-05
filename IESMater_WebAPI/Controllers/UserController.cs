using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

using IESMater_WebAPI.Models;
using Newtonsoft.Json.Linq;

namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api/User")]

    public class UserController : ApiController
    {
        [Route("Email")]
        [HttpPost]
        public IESUserProfile GetUserProfile([FromBody]Login value)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESUserProfiles
                       where s.Email == value.Email
                       select s).FirstOrDefault();
            if (soc == null)
            {
                return new IESUserProfile { UserID = -99, Email = "", Name = "", Address = "", Password = "", MobileNumber = "", Token = "", ActivationDate = DateTime.Now };
            }
            else
            return soc;
        }

        [Route("Mobile/{Mobile}")]
        [HttpGet]

        public IESUserProfile GetUserProfilebyMobile(String Mobile)
        {
            var context = new xPenEntities();
            var soc = (from s in context.IESUserProfiles
                       where s.MobileNumber == Mobile
                       select s).FirstOrDefault();
            return soc;
        }




        [Route("Register")]
        [HttpPost]
       
        public IHttpActionResult Post([FromBody]IESUserProfile profile)
        {
            try
            {
                var context = new xPenEntities();
                var user = (from u in context.IESUserProfiles
                            where u.Email == profile.Email || u.MobileNumber == profile.MobileNumber
                            select u).ToList();
                if (user.Count > 0)
                {
                    return BadRequest("User Already Exist");
                }
                else
                {
                    context.IESUserProfiles.Add(profile);
                    context.SaveChanges();
                    return Ok(profile);
                }
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }



        [Route("Login")]
        [HttpPost]

        public IHttpActionResult PostLogin([FromBody]Login profile)
        {
            try
            {
                var context = new xPenEntities();
                var user = (from u in context.IESUserProfiles
                            where u.Email.ToLower() == profile.Email.ToLower() && u.Password == profile.Password
                            select u).ToList();
                if (user.Count == 0)
                {
                    var _user = new IESUserProfile { UserID = 0, Name = "", Address = "", Email = "", Password = "", Token = "", MobileNumber = "", ActivationDate = DateTime.Now };
                    return Ok(_user);
                }
                 else if (user.Count > 1)
                {
                    return BadRequest("Multiple Existence");
                }
                else
                {
                    
                    return Ok(user.First());
                }
            }
            catch (Exception Ex)
            {
                return InternalServerError();
            }
        }

        [Route("GetAcademic/{UserID}")]
        [HttpGet]

        public IHttpActionResult GetAcademicProfile(int UserID)
        {
            try
            {
                var context = new xPenEntities();
                var profile = (from u in context.ViewIESAcademicProfiles
                              where u.UserID == UserID
                              select u).First();
             
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
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    var ac = (from ap in context.IESAcademicProfiles
                              where ap.UserID == profile.UserID
                              select ap).ToList();

                    if (ac.Count > 0)
                    {
                        context.IESAcademicProfiles.RemoveRange(ac);
                        context.SaveChanges();
                    }

                    context.IESAcademicProfiles.Add(profile);
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }

                CustomResponse cr = new CustomResponse();
                cr.Response = "Ok";
                return Ok(cr);
            }
            catch (Exception Ex)
            {
                CustomResponse cr = new CustomResponse();
                cr.Response = "Fail";
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
                var context = new xPenEntities();
                string Message = "Register with IES Master using OTP " + value.String_OTP;

                var user = (from u in context.IESUserProfiles
                            where u.MobileNumber == value.MobileNumber
                            select u).ToList();
                if (user.Count == 1)
                {
                    String result = Utility.sendSMS(Message, value.MobileNumber);
                    JObject json = JObject.Parse(result);
                    String status = json.GetValue("status").ToString();
                    return Ok(json);
                }
                else if (user.Count == 0)
                {
                    CustomResponse cr = new CustomResponse();
                    cr.Response = "Mobile number is not registered";
                    return BadRequest();
                }
                else {
                    return BadRequest();
                }
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