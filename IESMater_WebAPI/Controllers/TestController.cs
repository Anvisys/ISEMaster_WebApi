﻿using IESMater_WebAPI.Models;
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
                            group s by new { s.UniversityID, s.UniversityName, s.CollegeID, s.CollegeName } into unilist
                            select new { unilist.Key.UniversityID, unilist.Key.UniversityName, unilist.Key.CollegeID, unilist.Key.CollegeName }).ToList();
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
                            select new { streamlist.Key.StreamID, streamlist.Key.StreamName }).ToList();
            return subjects;
        }

        [Route("University/{UniversityID}")]
        [HttpGet]
        public IEnumerable<Object> GetStreamByPaperForUniversity(int UniversityID)
        {
            var context = new xPenEntities();
            var subjects = (from s in context.ViewIESQuestionPapers
                            where s.UniversityID == UniversityID
                            group s by new { s.StreamID, s.StreamName } into streamlist
                            select new { streamlist.Key.StreamID, streamlist.Key.StreamName }).ToList();
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
                            select new { subjectname=subjectlist.Key }).ToList();
            return subjects;
        }


        [Route("GetSubject/{UniversityID}/{StreamID}")]
        [HttpGet]
        public IEnumerable<Object> GetSubjectsfromQuestionPaperforUniversity(int UniversityID, int StreamID)
        {
            var context = new xPenEntities();
            var subjects = (from s in context.ViewIESQuestionPapers

                            where s.UniversityID == UniversityID && s.StreamID == StreamID
                            group s by new {s.SubjectID, s.SubjectName } into subjectlist
                            select new {SubjectId= subjectlist.Key.SubjectID, SubjectName = subjectlist.Key.SubjectName }).ToList();
            return subjects;
        }



        [Route("GetYear/{UniversityID}/{streamID}/{SubjectId}")]
        [HttpGet]
        public IHttpActionResult GetYearsfromQuestionPaper(int UniversityID, int streamID, int SubjectId)
        {
            try
            {
                var context = new xPenEntities();
                var test = (from s in context.ViewIESQuestionPapers
                            where s.UniversityID == UniversityID && s.StreamID == streamID && s.SubjectID == SubjectId
                            group s by s.Year into yearGroup
                            select new { year = yearGroup.Key }).ToList();

                return Ok(test);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.InnerException);
            }
        }


        [Route("GetUnit/{UniversityID}/{streamID}/{SubjectId}/{Year}")]
        [HttpGet]
        public IEnumerable<Object> GetUnitforProfile(int UniversityID, int streamID, int SubjectId, int Year)
        {
            var context = new xPenEntities();
            var test = (from s in context.ViewIESQuestionPapers
                        where s.UniversityID == UniversityID && s.StreamID == streamID && s.SubjectID == SubjectId && s.Year == Year
                        group s by s.Unit into unitGroup
                        select new { Unit = unitGroup.Key }).ToList();

            return test;
        }

        [Route("{UniversityID}/{streamID}/{SubjectId}/{Year}/{Unit}/{UserId}")]
        [HttpGet]
        public IEnumerable<Object> GetQuestionforProfile(int UniversityID, int streamID, int SubjectId, int Year, int Unit, int UserId)
        {
            var context = new xPenEntities();

            var order = (from o in context.IESOrders
                         where o.UserID == UserId
                         select o).ToList();

            var test = (from s in context.ViewIESQuestionPapers
                        where s.UniversityID == UniversityID && s.StreamID == streamID 
                        && s.SubjectID == SubjectId && s.Year == Year && s.Unit == Unit
                        select s).ToList();

            //var myTest = from t in test
            //             join o in order.DefaultIfEmpty(new IESOrder {Paid=0, PurchaseDate = DateTime.Now.AddYears(1), ClosureDate= DateTime.Now.AddYears(1) }) on t.PaperID equals o.PaperID
            //             select new { t.PaperID, t.UniversityName, t.SubjectName, t.Unit, t.CollegeName, t.Year, o.Paid , o.PurchaseDate, o.ClosureDate };


            var qry = test.GroupJoin(
                order,
                t1=> t1.PaperID,
                o1=> o1.PaperID,
                (x,y)=> new {test =x, order=y })
                .SelectMany(
                y=> y.order.DefaultIfEmpty(new IESOrder {OrderID=0, PaperID =0, UserID=0, Paid=0, PurchaseDate= DateTime.Now.AddYears(1), ClosureDate = DateTime.Now.AddYears(1) }),
                (x,y) => new { test=x.test, order = y }
                ).ToList();

            return qry;
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
            CustomResponse cr = new CustomResponse();
            try
            {
                var context = new xPenEntities();
                var old = (from q in context.IESQuestionPapers
                           where q.UniversityID == value.UniversityID && q.SubjectID == value.SubjectID && q.Year == value.Year
                           select q).ToList();
                if (old.Count > 0)
                {
                    cr.Response = "Paper Already Exist";
                    return BadRequest(cr.Response);
                }
                else
                {
                    cr.Response = "Updated";
                    context.IESQuestionPapers.Add(value);
                    context.SaveChanges();
                    return Ok(cr);
                }
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
            List<IESOrder> list = new List<IESOrder>();
          
                var context = new xPenEntities();
                var prev = (from s in context.IESOrders
                            where s.PaperID == value.PaperID && s.UserID == value.UserID
                             select s).ToList();

            if (prev.Count > 0)
            {
                return InternalServerError();
            }
            else {
                context.IESOrders.Add(value);
                context.SaveChanges();
                return Ok();
            }


            

            //try
            //{
            //    var context = new xPenEntities();
            //    context.IESOrders.Add(value);
            //    context.SaveChanges();
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return InternalServerError(ex.InnerException);
            //}

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
