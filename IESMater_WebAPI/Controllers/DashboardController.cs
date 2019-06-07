using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        // GET: api/Dashboard
        [HttpGet]
        [Route("top3")]
        public IEnumerable<object> GetTop3()
        {
            var context = new xPenEntities();
            var result = (from s in context.ViewIESUniversityWithCounts
                          orderby s.StudentCount descending
                        select s).Take(3); 
            return result ;
        }

        // GET: api/Dashboard/5
        [HttpGet]
        [Route("popularity")]
        public IEnumerable<object> GetCout()
        {
            var context = new xPenEntities();
            var countresult = (from s in context.IESOrders
                               group s.UserID by DbFunctions.TruncateTime(s.PurchaseDate) into usergroup
                               select new   {
                                                Date = usergroup.Key,
                                                UserCount= usergroup.Count()
                               }
                               ).ToList();
            return countresult;
        }

        // POST: api/Dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dashboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
        }
    }
}
