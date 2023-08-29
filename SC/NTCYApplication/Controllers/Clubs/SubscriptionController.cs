using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class SubscriptionController : Controller
    {
        ISubscription iSubscription = new Subscription();
        //
        // GET: /Subscription/
        [HttpGet]
        public ActionResult CreateSubscription()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSubscription(FormCollection Form)
        {
            Subscription subscription = new Subscription();

            subscription.SubscriptionId = (Form["SubscriptionId"] == "0" || Form["SubscriptionId"] == "" || Form["SubscriptionId"] == null) ? 0 :
                                                int.Parse(Form["SubscriptionId"].ToString());
            subscription.SubscriptionType = Form["SubscriptionType"].ToString();
            subscription.SubscriptionRate = Convert.ToDouble(Form["SubscriptionRate"].ToString());
            subscription.SubscriptionValidity = DateTime.ParseExact(Form["SubscriptionValidity"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);;
            subscription.PaymentInstallments = int.Parse(Form["Installments"].ToString());
            subscription.Status = Form["Status"].ToString();
            subscription.JoiningAmount = Convert.ToDouble(Form["JoiningAmount"].ToString());
            //subscription.Response = Form["Response"].ToString();



            int response = subscription.Save();
            if (response == 1)
            {
                ViewBag.message = "Successfully Inserted";
            }
            else
            {
                ViewBag.message = "Insertion was not successful";
            }

            return RedirectToAction("CreateSubscription");
        }


        
        //[HttpPost]
        //public ActionResult CreateSubscription(FormCollection Form)
        //{
        //    try
        //    {
        //        Dictionary<string, object> SubscriptionDictionary = new Dictionary<string, object>();
        //        string installments = Form["Installments"];
        //        if (Form["SubscriptionId"]=="0"||Form["SubscriptionId"]==""||Form["SubscriptionId"]==null)
        //        {
        //            ViewBag.submit="Submit";
        //            SubscriptionDictionary.Add("SubscriptionType", Form["SubscriptionType"]);
        //            SubscriptionDictionary.Add("SubscriptionRate", Form["SubscriptionRate"]);
        //            SubscriptionDictionary.Add("SubscriptionValidity", Form["SubscriptionValidity"]);

        //            if (installments==""||installments==null)
        //            {
        //                SubscriptionDictionary.Add("PaymentInstallments", 0);
        //            }
        //            else
        //            {
        //                SubscriptionDictionary.Add("PaymentInstallments", Form["Installments"]);
        //            }
        //            SubscriptionDictionary.Add("Status", Form["Status"]);
        //            SubscriptionDictionary.Add("JoiningAmount", Form["JoiningAmount"]);

        //            string response = iSubscription.CreateSubscription(SubscriptionDictionary);
        //            if (response=="1")
        //            {
        //                ViewBag.message="Subcription Details Inserted Successfully";
        //            }
        //            else
        //            {
        //                ViewBag.message="Subcription Details Inserted Successfully";
        //            }

        //        }
        //        else
        //        {
        //            SubscriptionDictionary.Add("SubscriptionId", Form["SubscriptionId"]);
        //            SubscriptionDictionary.Add("SubscriptionType", Form["SubscriptionType"]);
        //            SubscriptionDictionary.Add("SubscriptionRate", Form["SubscriptionRate"]);
        //            SubscriptionDictionary.Add("SubscriptionValidity", Form["SubscriptionValidity"]);
        //            if (installments==""||installments==null)
        //            {
        //                SubscriptionDictionary.Add("PaymentInstallments", 0);
        //            }
        //            else
        //            {
        //                SubscriptionDictionary.Add("PaymentInstallments", Form["Installments"]);
        //            }
        //            SubscriptionDictionary.Add("Status", Form["Status"]);
        //            SubscriptionDictionary.Add("JoiningAmount", Form["JoiningAmount"]);
        //            string response = iSubscription.UpdateSubscription(SubscriptionDictionary);
        //            ViewBag.submit="Submit";
        //            if (response=="1")
        //            {
        //                ViewBag.message="Subcription Details Updated Successfully ";
        //            }
        //            else
        //            {
        //                ViewBag.message="Subcription Details Inserted Successfully";
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.message=e.Message;
        //        ViewBag.innerEx=e.InnerException.Message;
        //    }

        //    return View();
        //}

        [HttpGet]
        public ActionResult ViewAllSubscriptions()
        {
            try
            {
                List<Subscription> sublist;
                sublist=iSubscription.ViewAllSubscriptions();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ViewSubscription(int? SubscriptionId)
        {
            try
            {
                Dictionary<string, object> SubscriptionDictionary;
                SubscriptionDictionary=iSubscription.SelectSubscription(SubscriptionId);
                if (SubscriptionDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["SubscriptionId"]=SubscriptionDictionary["SubscriptionId"].ToString();
                    ViewData["SubscriptionType"]=SubscriptionDictionary["SubscriptionType"].ToString();
                    ViewData["SubscriptionRate"]=SubscriptionDictionary["SubscriptionRate"].ToString();
                    
                    ViewData["SubscriptionValidity"]=SubscriptionDictionary["SubscriptionValidity"].ToString().Trim();
                    ViewBag.radio=SubscriptionDictionary["PaymentInstallments"];
                    ViewData["Status"]=SubscriptionDictionary["Status"].ToString();
                    ViewData["JoiningAmount"]=SubscriptionDictionary["JoiningAmount"].ToString();
                }
                else
                {
                    ViewBag.message="Subscription Failed to Load";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            //   return View("CreateSubscription");
            return View("CreateSubscription", ViewBag.Data);
        }


        public ActionResult DeleteSubscription(int? SubscriptionId)
        {
            try
            {
                string sub;
                sub=iSubscription.DeleteSubscription(SubscriptionId);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Subscription";
                }
                else
                {
                    ViewBag.message="Subsccription Deleted ";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("ViewAllSubscriptions");
           
        }
    }
}
