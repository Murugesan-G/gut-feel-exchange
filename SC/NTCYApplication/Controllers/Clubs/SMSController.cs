using Newtonsoft.Json.Linq;
using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class SMSController : Controller
    {
        [HttpGet]
        public ActionResult SendSMS()
        {
            try
            {
                IMember iMember = new Member();
                List<Member> sublist;
                sublist=iMember.ViewAllMembers();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Member details Not Available";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message=ex.Message;
                ViewBag.innerEx=ex.InnerException.Message;
            }
            return View();
        }



        public ActionResult SendSMS(string mNo, string mobileNo,string msg)
        {
            var status = "";
            int mobNoLength = mobileNo.Trim().Length;
            if (mobNoLength<10)
            {

            }
            else
            {
                string message = string.Empty;
                try
                {
                   // IMember mem = new Member();
                   // DataSet ds = mem.GetMemberDue(mNo.Trim());
                    string recipient = mobileNo.Trim();
                    string APIKey = "SgTUwSrw2v0-Ab5OE4IzL9ZmzWWDLzJT7hWdseFcPB";
                    //if(ds.Tables.Count>0)
                    //{
                    //    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    //    {
                            //message="Dear Member, you have a due bill for "+ds.Tables[0].Rows[i]["TotalAmount"].ToString()+
                            //    " amount towards bill number "+ds.Tables[0].Rows[i]["BillNo"].ToString()+
                            //    " on "+ds.Tables[0].Rows[i]["Date"].ToString()+" date. Please pay when you visit next time without fail.";
                            message=msg.Trim();

                            String encodedMessage = HttpUtility.UrlEncode(message);

                            using (var webClient = new WebClient())
                            {
                                byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection(){

                                             {"apikey" , APIKey},
                                             {"numbers" , recipient},
                                             {"message" , message},
                                             {"sender" , "NTCYNK"}});

                                string result = System.Text.Encoding.UTF8.GetString(response);

                                var jsonObject = JObject.Parse(result);

                                status=jsonObject["status"].ToString();

                            }
                        //}
                   // }
                    return Json(status, JsonRequestBehavior.AllowGet);

                }
                catch (Exception e)
                {

                    throw (e);

                }
            }
            return null;
     
        }
    }
}