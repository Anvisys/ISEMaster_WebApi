using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IESMater_WebAPI.Models
{
    public class Transaction
    {
        public int UserID;
        public int PaperID;
        public int OrderID;
        public int Paid;
        public string CheckSum;

    }
}