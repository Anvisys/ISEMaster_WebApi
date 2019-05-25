using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESMater_WebAPI.Models
{
    public class College_Courses
    {
        public string CollegeName;
        public int UnivID;
        public List<IESStream> Streams;

    }
}