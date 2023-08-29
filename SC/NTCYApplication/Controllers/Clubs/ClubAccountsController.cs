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
    public class ClubAccountsController : Controller
    {

        IClubAccounts iAccounts = new ClubAccounts();
        //
        // GET: /ClubAccounts/

        public ActionResult CreateAccountDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccountDetails(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> TallyIntegrationDictionary = new Dictionary<string, object>();
                var data = iAccounts.ViewDetails();
                if (data.Count==1)
                {
                    ViewBag.submit="Submit";
                    TallyIntegrationDictionary.Add("Url", Form["Url"]);
                    TallyIntegrationDictionary.Add("Port", Form["Port"]);
                    TallyIntegrationDictionary.Add("Username", Form["Username"]);
                    TallyIntegrationDictionary.Add("Password", Form["Password"]);
                    TallyIntegrationDictionary.Add("Ledger1", Form["Ledger1"]);
                    TallyIntegrationDictionary.Add("Ledger2", Form["Ledger2"]);
                    TallyIntegrationDictionary.Add("Ledger3", Form["Ledger3"]);
                    TallyIntegrationDictionary.Add("Ledger4", Form["Ledger4"]);
                    TallyIntegrationDictionary.Add("Status", "Active");

                    iAccounts.CreateDetails(TallyIntegrationDictionary);
                    ViewBag.message="Tally Integration Details Successfully Inserted";
                }
                else
                {
                    TallyIntegrationDictionary.Add("Url", Form["Url"]);
                    TallyIntegrationDictionary.Add("Port", Form["Port"]);
                    TallyIntegrationDictionary.Add("Username", Form["Username"]);
                    TallyIntegrationDictionary.Add("Password", Form["Password"]);
                    TallyIntegrationDictionary.Add("Ledger1", Form["Ledger1"]);
                    TallyIntegrationDictionary.Add("Ledger2", Form["Ledger2"]);
                    TallyIntegrationDictionary.Add("Ledger3", Form["Ledger3"]);
                    TallyIntegrationDictionary.Add("Ledger4", Form["Ledger4"]);
                    TallyIntegrationDictionary.Add("Status", Form["Status"]);
                    iAccounts.EditDetails(TallyIntegrationDictionary);
                    ViewBag.submit="Submit";
                    ViewBag.message="Tally Integration Details Successfully Updated";
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
        public ActionResult ViewAccountDetails()
        {
            try
            {
                Dictionary<string, object> TallyIntegrationDictionary;
                TallyIntegrationDictionary=iAccounts.ViewDetails();
                if (TallyIntegrationDictionary.Count==1)
                {
                    ViewBag.Error=TallyIntegrationDictionary["response"].ToString();
                    // return new JavaScriptResult { Script="alert('No Data');" };
                }
                else
                {
                    ViewData["Url"]=TallyIntegrationDictionary["Url"].ToString();
                    ViewData["Port"]=TallyIntegrationDictionary["Port"].ToString();
                    ViewData["Username"]=TallyIntegrationDictionary["Username"].ToString();
                    ViewData["Password"]=TallyIntegrationDictionary["Password"].ToString();
                    ViewData["Ledger1"]=TallyIntegrationDictionary["Ledger1"].ToString();
                    ViewData["Ledger2"]=TallyIntegrationDictionary["Ledger2"].ToString();
                    ViewData["Ledger3"]=TallyIntegrationDictionary["Ledger3"].ToString();
                    ViewData["Ledger4"]=TallyIntegrationDictionary["Ledger4"].ToString();
                    ViewData["Status"]=TallyIntegrationDictionary["Status"].ToString();
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
        public ActionResult EditClubAccountDetails()
        {
            try
            {
                Dictionary<string, object> TallyIntegrationDictionary;
                TallyIntegrationDictionary=iAccounts.ViewDetails();
                if (TallyIntegrationDictionary.Count==0)
                {
                    ViewBag.Error="Results are Empty";
                }
                else
                {
                    ViewBag.submit="Update";
                    ViewData["Url"]=TallyIntegrationDictionary["Url"].ToString();
                    ViewData["Port"]=TallyIntegrationDictionary["Port"].ToString();
                    ViewData["Username"]=TallyIntegrationDictionary["Username"].ToString();
                    ViewData["Password"]=TallyIntegrationDictionary["Password"].ToString();
                    ViewData["Ledger1"]=TallyIntegrationDictionary["Ledger1"].ToString();
                    ViewData["Ledger2"]=TallyIntegrationDictionary["Ledger2"].ToString();
                    ViewData["Ledger3"]=TallyIntegrationDictionary["Ledger3"].ToString();
                    ViewData["Ledger4"]=TallyIntegrationDictionary["Ledger4"].ToString();
                    ViewData["Status"]=TallyIntegrationDictionary["Status"].ToString();
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("~/Views/ClubAccounts/CreateAccountDetails.cshtml");
        }
    }
}
