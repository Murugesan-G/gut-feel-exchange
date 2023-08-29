using NTCYApplication.Interfaces;
using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTCYApplication.Controllers.LiquorC
{
    [Authorize(Roles = "Admin,Management")]
    public class LiquorController : Controller
    {
        LiquorInterface li = new Liquor();
        List<Liquor> liquorlist;
        //
        // GET: /Liquor/

        [HttpGet]
        public ActionResult CreateLiquorItem()
        {
            try
            {
                List<Liquor> catList;
                catList=li.ViewAllLiquorCategory();
                if (catList!=null)
                {
                    ViewBag.CategoryList=catList;
                }
                else
                {
                    ViewBag.message="Liquor Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateLiquorItem(FormCollection FormData)
        {
            List<Liquor> gstList = new List<Liquor>();
            gstList=li.ViewAllLiquorDetails();
            Liquor liquor = new Liquor();
            try
            {
                Dictionary<string, object> LiquorDictionary = new Dictionary<string, object>();
                var LiquorId = FormData["LiquorId"];
                if (FormData["LiquorId"]=="0"||FormData["LiquorId"]==""||FormData["LiquorId"]==null)
                {
                    ViewBag.submit="Submit";
                    LiquorDictionary.Add("LiquorName", FormData["LiquorName"]);
                    LiquorDictionary.Add("PegorBottle", FormData["PegorBottleChk"]);
                    LiquorDictionary.Add("LiquorCategoryId", FormData["LiquorCategory"]);
                    //LiquorDictionary.Add("Vendor", FormData["Vendor"]);
                    LiquorDictionary.Add("EffectiveDate", FormData["Date"]);
                    LiquorDictionary.Add("QuantityNoOfPegs", FormData["QuantityNoOfPegs"]);
                    LiquorDictionary.Add("QuantityPerBottle", FormData["QuantityPerBottle"]);
                    //LiquorDictionary.Add("RatePerPeg", FormData["RatePerPeg"]);
                    //LiquorDictionary.Add("RatePerBottle", FormData["RatePerBottle"]);
                    LiquorDictionary.Add("SellingPriceBottle", FormData["SellingPriceBottle"]);
                    LiquorDictionary.Add("SellingPricePeg", FormData["SellingPricePeg"]);
                   
                    for (int i = 0; i<gstList.Count; i++)
                    {
                        if(gstList[i].CategoryName!="Miscellaneous")
                        {
                            int liquorid = gstList[i].LiquorId;
                            float gst = float.Parse(FormData["GST"].ToString());
                            liquor.UpdateGst(liquorid, gst);
                        }
                    }

                    LiquorDictionary.Add("GST", FormData["GST"]);
                    LiquorDictionary.Add("Status", FormData["Status"]);
                  //  LiquorDictionary.Add("CategoryName", FormData["LiquorCategory"]);
                    li.CreateLiquorDetails(LiquorDictionary);
                    ViewBag.message="Liquor Details Inserted Successfully";

                }
                else
                {
                    LiquorDictionary.Add("LiquorId", FormData["LiquorId"]);
                    LiquorDictionary.Add("LiquorName", FormData["LiquorName"]);
                    LiquorDictionary.Add("PegorBottle", FormData["PegorBottleChk"]);
                    LiquorDictionary.Add("LiquorCategoryId", FormData["LiquorCategory"]);
                    //LiquorDictionary.Add("Vendor", FormData["Vendor"]);
                    LiquorDictionary.Add("EffectiveDate", FormData["Date"]);
                    LiquorDictionary.Add("QuantityNoOfPegs", FormData["QuantityNoOfPegs"]);
                    LiquorDictionary.Add("QuantityPerBottle", FormData["QuantityPerBottle"]);
                    //LiquorDictionary.Add("RatePerPeg", FormData["RatePerPeg"]);
                    //LiquorDictionary.Add("RatePerBottle", FormData["RatePerBottle"]);
                    LiquorDictionary.Add("SellingPriceBottle", FormData["SellingPriceBottle"]);
                    LiquorDictionary.Add("SellingPricePeg", FormData["SellingPricePeg"]);
                    for (int i = 0; i<gstList.Count; i++)
                    {
                        if (gstList[i].CategoryName!="Miscellaneous")
                        {
                            int liquorid = gstList[i].LiquorId;
                            float gst = float.Parse(FormData["GST"].ToString());
                            liquor.UpdateGst(liquorid, gst);
                        }
                    }
                    LiquorDictionary.Add("GST", FormData["GST"]);
                    LiquorDictionary.Add("Status", FormData["Status"]);
                    //LiquorDictionary.Add("CategoryName", FormData["LiquorCategory"]);
                    li.UpdateLiquorDetails(LiquorDictionary);
                    ViewBag.message="Liquor Details Updated Successfully";
                }

                List<Liquor> catList;
                catList=li.ViewAllLiquorCategory();
                if (catList!=null)
                {
                    ViewBag.CategoryList=catList;
                }
                else
                {
                    ViewBag.message="Liquor Details Not Available";
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
        public ActionResult ViewAllLiquorItem()
        {
            try
            { 
               
                liquorlist=li.ViewAllLiquorDetails().ToList();
                if (liquorlist!=null)
                {
                    ViewBag.LiquorList=liquorlist;
                }
                else
                {
                    ViewBag.message="Liquor Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View(liquorlist);
        }

        [HttpGet]
        public ActionResult ViewLiquor(int LiquorId)
        {
            try
            {
                Dictionary<string, object> LiquorDictionary;
                LiquorDictionary=li.SelectLiquor(LiquorId);
                if (LiquorDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["LiquorId"]=LiquorDictionary["LiquorId"].ToString();
                    ViewData["LiquorName"]=LiquorDictionary["LiquorName"].ToString();
                    ViewData["LiquorCategoryId"]=LiquorDictionary["CategoryName"].ToString();
                    ViewData["Date"]=LiquorDictionary["EffectiveDate"].ToString();
                    ViewData["QuantityNoOfPegs"]=LiquorDictionary["PegsPerBottle"].ToString();
                    ViewData["QuantityPerBottle"]=LiquorDictionary["Volume"].ToString();
                    //ViewData["RatePerPeg"] = LiquorDictionary["RatePerPeg"].ToString();
                    //ViewData["RatePerBottle"] = LiquorDictionary["RatePerBottle"].ToString();
                    ViewData["SellingPriceBottle"]=LiquorDictionary["SellingPriceBottle"].ToString();
                    ViewData["SellingPricePeg"]=LiquorDictionary["SellingPricePeg"].ToString();
                    ViewData["GST"]=LiquorDictionary["GST"].ToString();
                    ViewData["Status"]=LiquorDictionary["Status"].ToString();
                }
                else
                {
                    ViewBag.message="Liquor Details Failed to Load";
                }

                List<Liquor> catList;
                catList=li.ViewAllLiquorCategory();
                if (catList!=null)
                {
                    ViewBag.CategoryList=catList;
                }
                else
                {
                    ViewBag.message="Liquor Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("CreateLiquorItem", ViewBag.Data);
        }
        public ActionResult DeleteLiquor(int LiquorId)
        {
            try
            {
                string sub;
                sub=li.DeleteLiquor(LiquorId);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Liquor Details";
                }
                else
                {
                    ViewBag.message="Liquor Details Deleted ";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("ViewAllLiquorItem");
        }
    }
}
