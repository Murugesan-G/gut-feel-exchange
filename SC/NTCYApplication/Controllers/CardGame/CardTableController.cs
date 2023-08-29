using NTCYApplication.Interfaces;
using NTCYApplication.Models.CardGame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.CardGame
{
    [Authorize(Roles = "Admin,Management")]
    public class CardTableController : Controller
    {
        //
        // GET: /CardTable/
        CardTableInterface ci = new CardTable();

        [HttpGet]
        public ActionResult CreateCardTable()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCardTable(FormCollection FormData)
        {
            try
            {
                Dictionary<string, object> CardTableDictionary = new Dictionary<string, object>();
                var TableNo = FormData["TableNo"];
                if (FormData["TableNo"]=="0"||FormData["TableNo"]==""||FormData["TableNo"]==null)
                {
                    ViewBag.submit="Submit";
                    CardTableDictionary.Add("TableName", FormData["TableName"]);
                    CardTableDictionary.Add("Status", FormData["Status"]);

                    ci.CreateCardTableDetails(CardTableDictionary);
                    ViewBag.message="Card Table Details Inserted Successfully";
                }
                else
                {
                    CardTableDictionary.Add("TableName", FormData["TableName"]);
                    CardTableDictionary.Add("Status", FormData["Status"]);
                    CardTableDictionary.Add("TableNo", FormData["TableNo"]);
                    ci.UpdateCardTable(CardTableDictionary);
                    ViewBag.submit="Submit";
                    ViewBag.message="Card Table Details Updated Successfully ";
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
        public ActionResult ViewAllCardTable()
        {
            try
            {
                List<CardTable> tablelist;
                tablelist=ci.ViewAllCardTables();
                if (tablelist!=null)
                {
                    ViewBag.TableList=tablelist;
                }
                else
                {
                    ViewBag.message="Card Table Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           
            return View();
        }

        public ActionResult DeleteCardTable(int TableNo)
        {
            try
            {
                string sub;
                sub=ci.DeleteCardTable(TableNo);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Card Table Details";
                }
                else
                {
                    ViewBag.message="Card Table Deleted ";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           
            return RedirectToAction("ViewAllCardTable");
           
        }

        [HttpGet]
        public ActionResult ViewCardTable(int TableNo)
        {
            try
            {
                Dictionary<string, object> CardTableDictionary;
                CardTableDictionary=ci.SelectCardTable(TableNo);
                if (CardTableDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["TableNo"]=CardTableDictionary["TableNo"].ToString();
                    ViewData["TableName"]=CardTableDictionary["TableName"].ToString(); ;
                    ViewData["Status"]=CardTableDictionary["Status"].ToString();
                }
                else
                {
                    ViewBag.message="Card Table Details Failed to Load";
                }
            }
            catch(Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
          
            return View("CreateCardTable", ViewBag.Data);
        }
    }
}
