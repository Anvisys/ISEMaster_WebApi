using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESMater_WebAPI.Models
{
    public class SubjectDetails
    {
        public int? SubjectID;
        public string SubjectName;

     //   public List<SubjectDetails> Subjects;

        public SubjectDetails()
        {
        }
        public SubjectDetails(int? id ,string name)
        {
            SubjectID = id;
            SubjectName = name;
        }
    }
}