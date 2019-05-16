using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace IESMater_WebAPI.Models
{
    public class Utility
    {
        private static string FileName = System.Web.HttpContext.Current.Server.MapPath(@"~\Content\LogFile.txt");
        public static void log(String Message)
        {
            StreamWriter errWriter = new StreamWriter(FileName, true);
            errWriter.WriteLine(Message);

            errWriter.Close();
        }


        public static string sendSMS(String textMessage, String MobileNumber)
        {
            String message = HttpUtility.UrlEncode(textMessage);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , "msfOwE8inQk-tKtISb5ZeGF09HkktGreaH64DJlkg3"},
                {"numbers" , MobileNumber},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;

            }
        }
    }
}