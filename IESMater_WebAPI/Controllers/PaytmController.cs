using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using Newtonsoft.Json;
using Paytm.Checksum;

namespace IESMater_WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Paytm")]
    public class PaytmController : ApiController
    {
        // GET: api/Paytm
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("GetChecksum")]
        [HttpGet]
        public IHttpActionResult GetChecksum()
        {
           
            checkSum cs = new checkSum();
            cs.checksum = CreateChecksum();
            string JSONresult = JsonConvert.SerializeObject(cs);
            return Ok(cs);
        }

        [Route("VerifyChecksum")]
        [HttpPost]
        public void IHttpActionResult([FromBody]Dictionary<String, String> value)
        {
           bool result =   VerifyChecksum(value);
        }

        // PUT: api/Paytm/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Paytm/5
        public void Delete(int id)
        {
        }


        private class checkSum {
            public string checksum;
        }

        private String CreateChecksum()
        {
            Dictionary<String, String> paytmParams = new Dictionary<String, String>();
            String merchantMid = "JoyYHn78452054983473";
            // Key in your staging and production MID available in your dashboard
            String merchantKey = "DQW9YSzGhXyW5&hV";
            // Key in your staging and production MID available in your dashboard
            String orderId = "order1";
            String channelId = "WAP";
            String custId = "cust123";
            String mobileNo = "9591033223";
            String email = "amit_bansal73@yahoo.com";
            String txnAmount = "10.00";
            String website = "WEBSTAGING";
            // This is the staging value. Production value is available in your dashboard
            String industryTypeId = "Retail";
            // This is the staging value. Production value is available in your dashboard
            String callbackUrl = "https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=order1";
            paytmParams.Add("MID", merchantMid);
            paytmParams.Add("CHANNEL_ID", channelId);
            paytmParams.Add("WEBSITE", website);
            paytmParams.Add("CALLBACK_URL", callbackUrl);
            paytmParams.Add("CUST_ID", custId);
            paytmParams.Add("MOBILE_NO", mobileNo);
            paytmParams.Add("EMAIL", email);
            paytmParams.Add("ORDER_ID", orderId);
            paytmParams.Add("INDUSTRY_TYPE_ID", industryTypeId);
            paytmParams.Add("TXN_AMOUNT", txnAmount);
            String paytmChecksum = CheckSum.GenerateCheckSum(merchantKey, paytmParams);

            return paytmChecksum;

        }

        private bool VerifyChecksum(Dictionary<String,String> request)
        {
            String merchantKey = "gKpu7IKaLSbkchFS";
            Dictionary<String, String> paytmParams = new Dictionary<String, String>();
            string paytmChecksum = "";

            foreach (string key in request.Keys)
            {
                paytmParams.Add(key.Trim(), request[key].Trim());
            }
            if (paytmParams.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = paytmParams["CHECKSUMHASH"];
                paytmParams.Remove("CHECKSUMHASH");
            }
            return  CheckSum.VerifyCheckSum(merchantKey, paytmParams, paytmChecksum);
          
        }
    }
}
