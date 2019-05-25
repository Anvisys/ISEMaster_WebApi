using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESMater_WebAPI.Models
{
    public class StreamDetails
    {
        public int streamid { get; set; }
        public string stremname { get; set; }

        public StreamDetails() { }

        public StreamDetails(int id ,string name) {
            streamid = id;
            stremname = name;
        }

    }
}