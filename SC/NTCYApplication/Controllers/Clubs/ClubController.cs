using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management")]
    public class ClubController : Controller
    {
        ClubInterface ci = new Club();

        // GET: /Admin/

        [HttpGet]
        public ActionResult CreateClub()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateClub(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> ClubDictionary = new Dictionary<string, object>();
                var Clubid = Form["Clubid"];
                if (Form["Clubid"]=="0"||Form["Clubid"]==""||Form["Clubid"]==null)
                {
                    ViewBag.submit="Submit";
                    ClubDictionary.Add("ClubName", Form["clubname"]);
                    ClubDictionary.Add("Address", Form["address"]);
                    ClubDictionary.Add("RegistrationNumber", Form["registration"]);
                    ClubDictionary.Add("LiquorLicense", Form["liquorlicense"]);
                    ClubDictionary.Add("RestaurantLicense", Form["restaurantLicense"]);
                    ClubDictionary.Add("GSTNumber", Form["gst"]);
                    ClubDictionary.Add("PAN", Form["pan"]);
                    ClubDictionary.Add("TAN", Form["tan"]);
                    ClubDictionary.Add("MobileNumber", Form["mobile"]);
                    ClubDictionary.Add("ContactPerson", Form["contactPerson"]);
                    ClubDictionary.Add("PhoneNumber", Form["phone"]);
                    ClubDictionary.Add("EmailID", Form["email"]);
                    ClubDictionary.Add("Amenities", Form["amenities"]);
                    ClubDictionary.Add("Logo", Form["logo"]);
                    ClubDictionary.Add("About", Form["about"]);
                    // ClubDictionary.Add("DateOfIncorporation", Form["doi"]);
                    ClubDictionary.Add("DateOfIncorporation", DateTime.ParseExact(Form["doi"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    ci.CreateClubDetails(ClubDictionary);
                    ViewBag.message="Club Details Inserted Successfully";
                }
                else
                {

                    ClubDictionary.Add("ClubId", Clubid);
                    ClubDictionary.Add("ClubName", Form["clubname"]);
                    ClubDictionary.Add("Address", Form["address"]);
                    ClubDictionary.Add("RegistrationNumber", Form["registration"]);
                    ClubDictionary.Add("LiquorLicense", Form["liquorlicense"]);
                    ClubDictionary.Add("RestaurantLicense", Form["restaurantLicense"]);
                    ClubDictionary.Add("GSTNumber", Form["gst"]);
                    ClubDictionary.Add("PAN", Form["pan"]);
                    ClubDictionary.Add("TAN", Form["tan"]);
                    ClubDictionary.Add("MobileNumber", Form["mobile"]);
                    ClubDictionary.Add("ContactPerson", Form["contactPerson"]);
                    ClubDictionary.Add("PhoneNumber", Form["phone"]);
                    ClubDictionary.Add("EmailID", Form["email"]);
                    ClubDictionary.Add("Amenities", Form["amenities"]);
                    ClubDictionary.Add("Logo", Form["logo"]);
                    ClubDictionary.Add("About", Form["about"]);
                    ClubDictionary.Add("DateOfIncorporation", Form["doi"]);

                    ci.UpdateClubDetails(ClubDictionary);

                    ViewBag.submit="Submit";
                    ViewBag.message="Club Details Updated Successfully ";

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
        public ActionResult ViewClubDetails()
        {
            List<Club> clubList = new List<Club>();
            try
            {
                clubList=ci.ViewClubDetails(0);
                ViewBag.ClubDetails=clubList;
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            //return View("~/Views/Club/CreateClub.cshtml");
            return View(clubList);
        }


        public ActionResult EditClubDetails(int ClubId)
        {
            Dictionary<string, object> ClubDict;
            ClubDict=ci.EditClubDetails(ClubId);

            ViewBag.submit="Update";
            ViewData["clubid"]=ClubDict["ClubId"].ToString();
            ViewData["clubname"]=ClubDict["ClubName"].ToString();
            ViewData["address"]=ClubDict["Address"].ToString();
            ViewData["registration"]=ClubDict["RegistrationNumber"].ToString();
            ViewData["liquorlicense"]=ClubDict["LiquorLicense"].ToString();
            ViewData["restaurantLicense"]=ClubDict["RestaurantLicense"].ToString();
            ViewData["gst"]=ClubDict["GSTNumber"].ToString();
            ViewData["email"]=ClubDict["EmailID"].ToString();
            ViewData["pan"]=ClubDict["PAN"].ToString();
            ViewData["tan"]=ClubDict["TAN"].ToString();
            ViewData["mobile"]=ClubDict["MobileNumber"].ToString();
            ViewData["contactPerson"]=ClubDict["ContactPerson"].ToString();
            ViewData["phone"]=ClubDict["PhoneNumber"].ToString();
            ViewData["amenities"]=ClubDict["Amenities"].ToString();
            ViewData["about"]=ClubDict["About"].ToString();
            //ViewData["logo"]=ClubDict["Logo"].();
            //DateTime dt = DateTime.ParseExact(ClubDict["DateOfIncorporation"].ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            //string s = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime x = Convert.ToDateTime(ClubDict["DateOfIncorporation"].ToString());
            ViewData["doi"] = x.ToShortDateString();

            return View("~/Views/Club/CreateClub.cshtml");
        }
    }
}