using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using IESMater_WebAPI.Models;
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
        [HttpPost]
        public IHttpActionResult GetChecksum([FromBody]IESOrder value)
        {
            CustomResponse err = new CustomResponse();
            try
            {

                var context = new xPenEntities();
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    var prev = (from p in context.IESOrders
                                where p.PaperID == value.PaperID && p.UserID == value.UserID
                                select p).ToList();

                    if (prev.Count > 0)
                    {
                        context.IESOrders.RemoveRange(prev);
                        context.SaveChanges();
                    }

                    context.IESOrders.Add(value);
                    context.SaveChanges();

                    if (value.OrderID > 1)
                    {
                        checkSum cs = new checkSum();
                       String csm = CreateChecksum(value.OrderID, value.UserID, value.Paid);
                        string JSONresult = JsonConvert.SerializeObject(cs);
                        dbContextTransaction.Commit();
                        Transaction newTransaction = new Transaction();
                        newTransaction.UserID = value.UserID;
                        newTransaction.PaperID = value.PaperID;
                        newTransaction.Paid = value.Paid;
                        newTransaction.OrderID = value.OrderID;
                        newTransaction.CheckSum = csm;
                        return Ok(newTransaction);
                    }
                    else
                    {
                     
                        err.Response = "Error Saving Order";
                        dbContextTransaction.Rollback();
                        //return BadRequest(cs);
                        return Content(HttpStatusCode.BadRequest, err);
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
          
        }

        [Route("VerifyChecksum")]
        [HttpPost]
        public IHttpActionResult VerifyCheckSum([FromBody]Transaction trans)
        {
            CustomResponse res = new CustomResponse();
            try
            {
                Dictionary<String, String> paytmParams = new Dictionary<String, String>();
                String merchantMid = "JoyYHn78452054983473";
                // Key in your staging and production MID available in your dashboard
                String merchantKey = "DQW9YSzGhXyW5&hV";
                // Key in your staging and production MID available in your dashboard
                String orderId = trans.OrderID.ToString();
                String channelId = "WAP";
                String custId = trans.UserID.ToString();
                String mobileNo = "9591033223";
                String email = "amit_bansal73@yahoo.com";
                String txnAmount = trans.Paid.ToString();
                String website = "WEBSTAGING";
                // This is the staging value. Production value is available in your dashboard
                String industryTypeId = "Retail";
                // This is the staging value. Production value is available in your dashboard
                String callbackUrl = "https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=" + orderId;
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
                paytmParams.Add("CHECKSUMHASH", trans.CheckSum);


                bool result = VerifyChecksum(paytmParams);
                if (result)
                {
                    res.Response = "Ok";
                    int order_id = Convert.ToInt32(orderId);
                    var context = new xPenEntities();
                    var order = (from o in context.IESOrders
                                 where o.OrderID == order_id
                                 select o).First();
                    order.PurchaseDate = DateTime.UtcNow;
                    order.ClosureDate = DateTime.UtcNow.AddYears(1);
                    context.SaveChanges();
                }

                else
                    res.Response = "Fail";

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Response = "Server Error";
                return Ok(res);
            }
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

        private String CreateChecksum(int OrderId, int UserId, int Value)
        {
            Dictionary<String, String> paytmParams = new Dictionary<String, String>();
            String merchantMid = "JoyYHn78452054983473";
            // Key in your staging and production MID available in your dashboard
            String merchantKey = "DQW9YSzGhXyW5&hV";
            // Key in your staging and production MID available in your dashboard
            String orderId = OrderId.ToString();
            String channelId = "WAP";
            String custId = UserId.ToString();
            String mobileNo = "9591033223";
            String email = "amit_bansal73@yahoo.com";
            String txnAmount = Value.ToString();
            String website = "WEBSTAGING";
            // This is the staging value. Production value is available in your dashboard
            String industryTypeId = "Retail";
            // This is the staging value. Production value is available in your dashboard
            String callbackUrl = "https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=" + orderId;
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
            String merchantKey = "DQW9YSzGhXyW5&hV";
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
            bool results =  CheckSum.VerifyCheckSum(merchantKey, paytmParams, paytmChecksum);

            return results;
          
        }
    }
}
