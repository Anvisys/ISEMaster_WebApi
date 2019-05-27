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
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        [Route("Univ/{UnivID}/Stream/{StreamId}/Subject/{SubId}")]
        [HttpGet]
        public IEnumerable<ViewIESQuestionPaper> GetforSubject(int UnivID, int StreamId, int SubId)
        {
            var context = new xPenEntities();
            var stream = (from q in context.ViewIESQuestionPapers
                          where q.UnivID == UnivID && q.StreamID == StreamId && q.SubjectID == SubId
                          select q).ToList();
            return stream;
        }

        [Route("Univ/{UnivID}/Stream/{StreamId}/Subject/{SubId}/Year{Year}")]
        [HttpGet]
        public IEnumerable<ViewIESQuestionPaper> GetForYear(int UnivID, int StreamId, int SubId, int Year)
        {
            var context = new xPenEntities();
            var stream = (from q in context.ViewIESQuestionPapers
                          where q.UnivID == UnivID && q.StreamID == StreamId && q.SubjectID == SubId && q.Year == Year
                          select q).ToList();
            return stream;
        }


        [Route("AddQuestion/{PaperID}")]
        [HttpPost]
        public IHttpActionResult PostQuestion([FromBody]IESQuestion question, int PaperID)
        {
            try
            {
                var context = new xPenEntities();
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    if (question.QuestionID <= 0)
                    {
                        context.IESQuestions.Add(question);
                        context.SaveChanges();
                    }

                    context.IESPaper_Question.Add( new IESPaper_Question{
                        PaperID = PaperID,
                        QuesID = question.QuestionID
                    });
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

        }

        // PUT: api/Question/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Question/5
        public void Delete(int id)
        {
        }
    }
}
