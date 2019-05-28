using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IESMater_WebAPI.Controllers
{
    [RoutePrefix("api/UserDetails")]
    public class UserDetailsController : ApiController
    {
        [Authorize]
	        //[Route("")]
        public IHttpActionResult Get()
        {
	            return Ok(UserDetails.CreateUserDetails());
        }
}

    public class UserDetails
	    {
	        public int UserID { get; set; }
	        public string UserName { get; set; }
	        public string UserCity { get; set; }
	        public Boolean IsUserValid { get; set; }
	
	        public static List<UserDetails> CreateUserDetails()
	        {
	            List<UserDetails> UserDetailsList = new List<UserDetails>
                {
	                new UserDetails {UserID = 10248, UserName = "Asif Khan", UserCity = "Amman", IsUserValid = true },
	                new UserDetails {UserID = 10249, UserName = "Sam Cook", UserCity = "Dubai", IsUserValid = false},
	                new UserDetails {UserID = 10250,UserName = "Alan Joe", UserCity = "Jeddah", IsUserValid = false },
	                new UserDetails {UserID = 10251,UserName = "Liz Buttler", UserCity = "Abu Dhabi", IsUserValid = false},
	                new UserDetails {UserID = 10252,UserName = "Yasmeen Rami", UserCity = "Kuwait", IsUserValid = true}
	            };
	
	            return UserDetailsList;
	        }
	    }
}
