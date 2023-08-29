using NTCYApplication.Interfaces;
using NTCYApplication.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.RoomC
{
    [Authorize(Roles = "Admin,Management")]
    public class RoomController : Controller
    {
        IRoom iRoom = new Room();
        //
        // GET: /Room/

        public ActionResult CreateRoom()
        {
            try
            {
                List<SelectListItem> status = new List<SelectListItem>();
                status.Add(new SelectListItem { Text="Active", Value="Active" });
                status.Add(new SelectListItem { Text="InActive", Value="InActive" });
                status.Add(new SelectListItem { Text="Disabled", Value="Disabled" });

                ViewData["Status"]=status;
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoom(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> RoomDictionary = new Dictionary<string, object>();

                var Clubid = Form["RoomId"];
                if (Form["RoomId"]=="0"||Form["RoomId"]==""||Form["RoomId"]==null)
                {
                    ViewBag.submit="Submit";
                    RoomDictionary.Add("RoomNo", Form["RoomNo"]);
                    RoomDictionary.Add("RoomName", Form["RoomName"]);
                    RoomDictionary.Add("Charges", Form["Charges"]);
                    RoomDictionary.Add("Status", Form["Status"]);
                    RoomDictionary.Add("GST", Form["GST"]);
                    RoomDictionary.Add("RoomAllocated", "RoomAllocated");
                    iRoom.CreateRoom(RoomDictionary);
                    ViewBag.message="Room Details Inserted Successfully";
                }
                else
                {

                    RoomDictionary.Add("RoomId", Form["RoomId"]);
                    RoomDictionary.Add("RoomName", Form["RoomName"]);
                    RoomDictionary.Add("RoomNo", Form["RoomNo"]);
                    RoomDictionary.Add("Charges", Form["Charges"]);
                    RoomDictionary.Add("Status", Form["Status"]);
                    RoomDictionary.Add("GST", Form["GST"]);
                    RoomDictionary.Add("RoomAllocated", Form["RoomAllocated"]);
                    iRoom.EditRoom(RoomDictionary);
                    ViewBag.submit="Submit";
                    ViewBag.message="Room Details Updated Successfully ";

                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }


        public ActionResult ViewAllRoom()
        {
            List<Room> roomList;
            try
            {
                
                roomList=iRoom.ViewAllRoom();

                if (roomList!=null||roomList.Count!=0)
                {
                    ViewBag.RoomList=roomList;
                }
                else
                {
                    ViewBag.RoomList="0";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }


        public ActionResult SelectRoom(int? RoomId)
        {
            try
            {
                Dictionary<string, object> RoomDictionary;
                RoomDictionary=iRoom.SelectRoom(RoomId);
                if (RoomDictionary.Count>0)
                {
                    ViewBag.Submit="Update";
                    ViewData["RoomId"]=RoomDictionary["RoomId"].ToString();
                    ViewData["RoomName"]=RoomDictionary["RoomName"].ToString();
                    ViewData["RoomNo"]=RoomDictionary["RoomNo"].ToString();
                    ViewData["Charges"]=RoomDictionary["Charges"].ToString();
                    ViewData["Status"]=RoomDictionary["Status"].ToString();
                    ViewData["GST"]= RoomDictionary["GST"].ToString();
                    ViewData["RoomAllocated"] = RoomDictionary["RoomAllocated"].ToString();
                }
                else
                {
                    ViewBag.message="Rooms Failed to Load";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("CreateRoom");
        }


        public ActionResult DeleteRoom(int? RoomId)
        {
            try
            {
                string sub;
                sub=iRoom.DeleteRoom(RoomId);
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
            return RedirectToAction("ViewAllRoom");
            // return View();
        }
    }
}
