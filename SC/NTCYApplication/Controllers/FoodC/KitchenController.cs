using NTCYApplication.Interfaces;
using NTCYApplication.Models.Food;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;


namespace NTCYApplication.Controllers.FoodC
{
    [Authorize(Roles = "Admin,Management")]
    public class KitchenController : Controller
    {
        FoodInterface fi = new Food();
        // GET: /Kitchen/

        [HttpGet]
        public ActionResult CreateFoodItems()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFoodItems(FormCollection FormData)
        {
            Food fd = new Food();
            List<Food> gstList;
            gstList=fi.ViewAllFoodDetails();
            try
            {
                Dictionary<string, object> FoodDictionary = new Dictionary<string, object>();

                var FoodId = FormData["Clubid"];
                if (FormData["Foodid"]=="0"||FormData["Foodid"]==""||FormData["Foodid"]==null)
                {
                    ViewBag.submit="Submit";
                    // FoodDictionary.Add("FoodId", FormData["FoodId"]);
                    FoodDictionary.Add("ItemCode", FormData["ItemCode"]);
                    FoodDictionary.Add("ItemName", FormData["ItemName"]);
                    FoodDictionary.Add("ItemCategory", FormData["ItemCategory"]);
                    FoodDictionary.Add("Description", FormData["Description"]);
                    FoodDictionary.Add("Quantity", FormData["Quantity"]);
                    FoodDictionary.Add("RatePerUnit", FormData["RatePerUnit"]);
                    for(int i = 0; i<gstList.Count; i++)
                    {
                        int foodid = gstList[i].FoodId;
                        float gst = float.Parse(FormData["GST"].ToString());
                        fd.UpdateFoodGst(foodid, gst);
                    }

                    FoodDictionary.Add("GST", FormData["GST"]);
                    FoodDictionary.Add("Status", FormData["Status"]);
                    fi.CreateFoodDetails(FoodDictionary);
                    ViewBag.message="Food Details Inserted Successfully";

                }
                else
                {
                    FoodDictionary.Add("FoodId", FormData["FoodId"]);
                    FoodDictionary.Add("ItemCode", FormData["ItemCode"]);
                    FoodDictionary.Add("ItemName", FormData["ItemName"]);
                    FoodDictionary.Add("ItemCategory", FormData["ItemCategory"]);
                    FoodDictionary.Add("Description", FormData["Description"]);
                    FoodDictionary.Add("Quantity", FormData["Quantity"]);
                    FoodDictionary.Add("RatePerUnit", FormData["RatePerUnit"]);
                    for (int i = 0; i<gstList.Count; i++)
                    {
                        int foodid = gstList[i].FoodId;
                        float gst = float.Parse(FormData["GST"].ToString());
                        fd.UpdateFoodGst(foodid, gst);
                    }
                    FoodDictionary.Add("GST", FormData["GST"]);
                    FoodDictionary.Add("Status", FormData["Status"]);
                    fi.UpdateFoodDetails(FoodDictionary);
                    ViewBag.submit="Submit";
                    ViewBag.message="Food Details Updated Successfully ";
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
        public ActionResult ViewAllFoodItems()
        {
            try
            {
                List<Food> sublist;
                sublist=fi.ViewAllFoodDetails();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Food Details Not Available";
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
        public ActionResult ViewFood(int FoodId)
        {
            try
            {
                Dictionary<string, object> FoodDictionary;
                FoodDictionary=fi.SelectFood(FoodId);
                if (FoodDictionary.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["FoodId"]=FoodDictionary["FoodId"].ToString();
                    ViewData["ItemCode"]=FoodDictionary["ItemCode"].ToString();
                    ViewData["ItemName"]=FoodDictionary["ItemName"].ToString();
                    ViewData["ItemCategory"]=FoodDictionary["ItemCategory"].ToString();
                    ViewData["Description"]=FoodDictionary["Description"].ToString();
                    ViewData["Quantity"]=FoodDictionary["Quantity"].ToString();
                    ViewData["RatePerUnit"]=FoodDictionary["RatePerUnit"].ToString();
                    ViewData["GST"]=FoodDictionary["GST"].ToString();
                    ViewData["Status"]=FoodDictionary["Status"].ToString();

                }
                else
                {
                    ViewBag.message="Food Details Failed to Load";
                }

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return View("CreateFoodItems", ViewBag.Data);
        }

        public ActionResult DeleteFoodDetails(int FoodId)
        {
            try
            {
                string sub;
                sub=fi.DeleteFood(FoodId);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Food Details";
                }
                else
                {
                    ViewBag.message="Food Details Deleted ";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("ViewAllFoodItems");
        }
    }
}
