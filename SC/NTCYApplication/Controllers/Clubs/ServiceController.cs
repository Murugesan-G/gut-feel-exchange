using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class ServiceController : Controller
    {
        //
        // GET: /Service/
        ServiceInterface si = new Services();

        [HttpGet]
        public ActionResult CreateService()
        {

            return View();
        }


        [HttpPost]
        public ActionResult CreateService(FormCollection Form)
        {
            Services service = new Services();

            service.ServiceId = (Form["ServiceId"] == "0" || Form["ServiceId"] == "" || Form["ServiceId"] == null) ? 0 :
                                                int.Parse(Form["ServiceId"].ToString());
            service.ServiceCode = Form["ServiceCode"].ToString();
            service.ServiceName = Form["ServiceName"].ToString();
            service.Description = Form["Description"].ToString();
            service.AvailabilityStatus = Form["AvailabilityStatus"].ToString();
            service.PerHour = Convert.ToDouble(Form["PerHour"].ToString());
            service.PerHalfDay = Convert.ToDouble(Form["PerHalfDay"].ToString());
            service.FullDay = Convert.ToDouble(Form["FullDay"].ToString());
            service.FullMonth = Convert.ToDouble(Form["FullMonth"].ToString());
            service.Rate = Convert.ToDouble(Form["Rate"].ToString());
            service.Validity = DateTime.ParseExact(Form["Validity"].ToString(),"dd/MM/yyyy",CultureInfo.InvariantCulture);
            service.GST=Convert.ToDouble(Form["GST"]);
            int response = service.Save();
            //iMember.CreateMember(MemberDictionary);
            if (response == 1)
            {
                ViewBag.message = "Successfully Inserted";
            }
            else
            {
                ViewBag.message = "Insertion was not successful";
            }

            return RedirectToAction("CreateService");
        }

        //[HttpPost]
        //public ActionResult CreateService(FormCollection FormData)
        //{
        //    try
        //    {
        //        Dictionary<string, object> ServiceDictionary = new Dictionary<string, object>();

        //        var ServiceId = FormData["ServiceId"];
        //        if (FormData["ServiceId"]=="0"||FormData["ServiceId"]==""||FormData["ServiceId"]==null)
        //        {
        //            ViewBag.submit="Submit";
        //            ServiceDictionary.Add("ServiceCode", FormData["ServiceCode"]);
        //            ServiceDictionary.Add("ServiceName", FormData["ServiceName"]);
        //            ServiceDictionary.Add("Description", FormData["Description"]);
        //            ServiceDictionary.Add("AvailabilityStatus", FormData["AvailabilityStatus"]);
        //            ServiceDictionary.Add("Validity", FormData["Validity"]);
        //            if (FormData["PerHour"]==""||FormData["PerHour"]==null)
        //            {
        //                ServiceDictionary.Add("PerHour", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("PerHour", FormData["PerHour"]);
        //            }

        //            if (FormData["PerHalfDay"]==""||FormData["PerHalfDay"]==null)
        //            {
        //                ServiceDictionary.Add("PerHalfDay", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("PerHalfDay", FormData["PerHalfDay"]);
        //            }

        //            if (FormData["FullDay"]==""||FormData["FullDay"]==null)
        //            {
        //                ServiceDictionary.Add("FullDay", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("FullDay", FormData["FullDay"]);
        //            }

        //            if (FormData["FullMonth"]==""||FormData["FullMonth"]==null)
        //            {
        //                ServiceDictionary.Add("FullMonth", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("FullMonth", FormData["FullMonth"]);
        //            }
        //            if (FormData["Rate"]==""||FormData["Rate"]==null)
        //            {
        //                ServiceDictionary.Add("Rate", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("Rate", FormData["Rate"]);
        //            }
        //            //ServiceDictionary.Add("Validity", FormData["Validity"]);
        //            si.CreateService(ServiceDictionary);
        //            ViewBag.message="Service Created Successfully";
        //        }

        //        else
        //        {
        //            ServiceDictionary.Add("ServiceId", FormData["ServiceId"]);
        //            ServiceDictionary.Add("ServiceCode", FormData["ServiceCode"]);
        //            ServiceDictionary.Add("ServiceName", FormData["ServiceName"]);
        //            ServiceDictionary.Add("Description", FormData["Description"]);
        //            ServiceDictionary.Add("AvailabilityStatus", FormData["AvailabilityStatus"]);
        //            ServiceDictionary.Add("Validity", FormData["Validity"]);
        //            if (FormData["PerHour"]==""||FormData["PerHour"]==null)
        //            {
        //                ServiceDictionary.Add("PerHour", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("PerHour", FormData["PerHour"]);
        //            }

        //            if (FormData["PerHalfDay"]==""||FormData["PerHalfDay"]==null)
        //            {
        //                ServiceDictionary.Add("PerHalfDay", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("PerHalfDay", FormData["PerHalfDay"]);
        //            }

        //            if (FormData["FullDay"]==""||FormData["FullDay"]==null)
        //            {
        //                ServiceDictionary.Add("FullDay", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("FullDay", FormData["FullDay"]);
        //            }

        //            if (FormData["FullMonth"]==""||FormData["FullMonth"]==null)
        //            {
        //                ServiceDictionary.Add("FullMonth", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("FullMonth", FormData["FullMonth"]);
        //            }
        //            if (FormData["Rate"]==""||FormData["Rate"]==null)
        //            {
        //                ServiceDictionary.Add("FullMonth", 0);
        //            }
        //            else
        //            {
        //                ServiceDictionary.Add("Rate", FormData["Rate"]);
        //            }
        //            //ServiceDictionary.Add("Validity", FormData["Validity"]);

        //            si.UpdateService(ServiceDictionary);

        //            ViewBag.submit="Submit";
        //            ViewBag.message="Service Details Updated Successfully ";

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
        public ActionResult ViewAllServices()
        {
            try
            {
                List<Services> sublist;
                sublist=si.ViewAllServices();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Services Not Available";
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
        public ActionResult ViewService(int ServiceId)
        {
            try
            {
                Dictionary<string, object> ServiceDictionary;
                ServiceDictionary=si.SelectService(ServiceId);
                if (ServiceDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["ServiceId"]=ServiceDictionary["ServiceId"].ToString();
                    ViewData["ServiceCode"]=ServiceDictionary["ServiceCode"].ToString();
                    ViewData["ServiceName"]=ServiceDictionary["ServiceName"].ToString();
                    ViewData["Description"]=ServiceDictionary["Description"].ToString();
                    ViewData["AvailabilityStatus"]=ServiceDictionary["AvailabilityStatus"].ToString();
                    ViewData["PerHour"]=ServiceDictionary["PerHour"].ToString();
                    ViewData["PerHalfDay"]=ServiceDictionary["PerHalfDay"];
                    ViewData["FullDay"]=ServiceDictionary["FullDay"].ToString();
                    ViewData["FullMonth"]=ServiceDictionary["FullMonth"].ToString();
                    ViewData["Rate"]=ServiceDictionary["Rate"].ToString();
                    DateTime x =Convert.ToDateTime (ServiceDictionary["Validity"].ToString());
                    string s = x.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ViewData["Validity"]=s;
                    ViewData["GST"]=ServiceDictionary["GST"].ToString();

                }
                else
                {
                    ViewBag.message="Service Failed to Load";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("CreateService", ViewBag.Data);
        }


        public ActionResult DeleteService(int ServiceId)
        {
            try
            {
                string sub;
                sub=si.DeleteService(ServiceId);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Service";
                }
                else
                {
                    ViewBag.message="Service Deleted";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("ViewAllServices");
        }
    }
}
