using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class BiometricController : Controller
    {
        IBiometric iBiometric = new Biometric();
        //
        // GET: /Biometric/

        public ActionResult CreateBiometricDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBiometricDetails(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> BiometricDictionary = new Dictionary<string, object>();
                var data = iBiometric.ViewBiometricDetails();
                if (data.Count==1)
                {
                    ViewBag.submit="Submit";
                    BiometricDictionary.Add("IPAddress", Form["IPAddress"]);
                    //BiometricDictionary.Add("DataBaseName", Form["DataBaseName"]);
                    BiometricDictionary.Add("Catalog", Form["Catalog"]);
                    BiometricDictionary.Add("Username", Form["Username"]);
                    BiometricDictionary.Add("Password", Form["Password"]);
                    BiometricDictionary.Add("Status", Form["Status"]);
                    iBiometric.CreateBiometricDetails(BiometricDictionary);
                    ViewBag.message="Biometric Details Inserted Successfully";
                }
                else
                {
                    BiometricDictionary.Add("IPAddress", Form["IPAddress"]);
                    //BiometricDictionary.Add("DataBaseName", Form["DataBaseName"]);
                    BiometricDictionary.Add("Catalog", Form["Catalog"]);
                    BiometricDictionary.Add("Username", Form["Username"]);
                    BiometricDictionary.Add("Password", Form["Password"]);
                    BiometricDictionary.Add("Status", Form["Status"]);
                    iBiometric.EditBiometricDetails(BiometricDictionary);

                    ViewBag.submit="Submit";
                    ViewBag.message="Biometric Details Updated Successfully ";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        public ActionResult ViewBiometricDetails()
        {
            try
            {
                Dictionary<string, object> BiometricDictionary;
                BiometricDictionary=iBiometric.ViewBiometricDetails();
                if (BiometricDictionary.Count==1)
                {
                    ViewBag.Error=BiometricDictionary["response"].ToString();
                }
                else
                {
                    ViewData["IPAddress"]=BiometricDictionary["IPAddress"].ToString();
                    //ViewData["DataBaseName"]=BiometricDictionary["DataBaseName"].ToString();
                    ViewData["Catalog"]=BiometricDictionary["Catalog"].ToString();
                    ViewData["Username"]=BiometricDictionary["Username"].ToString();
                    ViewData["Password"]=BiometricDictionary["Password"].ToString();
                    ViewData["Status"]=BiometricDictionary["Status"].ToString();
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        public ActionResult EditBiometricDetails()
        {
            try
            {
                Dictionary<string, object> BiometricDictionary;
                BiometricDictionary=iBiometric.ViewBiometricDetails();
                ViewBag.submit="Update";
                ViewData["IPAddress"]=BiometricDictionary["IPAddress"].ToString();
                //ViewData["DataBaseName"]=BiometricDictionary["DataBaseName"].ToString();
                ViewData["Catalog"]=BiometricDictionary["Catalog"].ToString();
                ViewData["Username"]=BiometricDictionary["Username"].ToString();
                ViewData["Password"]=BiometricDictionary["Password"].ToString();
                ViewData["Status"]=BiometricDictionary["Status"].ToString();

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("~/Views/Biometric/CreateBiometricDetails.cshtml");
        }

    }
}
