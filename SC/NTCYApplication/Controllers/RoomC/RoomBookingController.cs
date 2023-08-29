using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using NTCYApplication.Models.Room;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.RoomC
{
    [Authorize(Roles = "Admin,FrontOffice,Management")]
    public class RoomBookingController : Controller
    {
        IRoomBooking iRoomBooking = new RoomBooking();

        public ActionResult Room_Bookings()
        {
            try
            {
                IClubCommmittee iCommittee = new ClubCommittee();
                IRoom iRoom = new Room();
                // Generate Member List
                List<Member> memberList;
                memberList=iCommittee.ShowMembers();
                if (memberList!=null)
                {
                    ViewBag.MemberList=memberList;
                }
                else
                {
                    ViewBag.MemberList="Members Not Available";
                }

                // Generate Room List
                List<Room> roomList;
                roomList=iRoom.ViewAllRoom();
                List<Room> unallocatedrooms = new List<Room>();
                for (int i=0; i<roomList.Count; i++)
                {
                    if (roomList[i].Status=="Available")
                    {

                        unallocatedrooms.Add(roomList[i]);

                    }
                    else
                    {
                        ViewBag.RoomList="Rooms Not Available";
                    }
                }
                ViewBag.RoomList=unallocatedrooms;

                //if (roomList!=null)
                //{
                //    ViewBag.RoomList=roomList;
                //}
                //else
                //{
                //    ViewBag.RoomList="Rooms Not Available";
                //}
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            
            return View();
        }




        public ActionResult Room_Booking(FormCollection Form)
        {
            RoomBooking roombooking = new RoomBooking();           
            roombooking.BookingId = (Form["BookingId"] == "0" || Form["BookingId"] == "" || Form["BookingId"] == null) ? 0 :
                                                int.Parse(Form["BookingId"].ToString());
            roombooking.MemberId = Form["MembershipNo"].ToString();
            roombooking.RoomNo = Form["RoomNo"].ToString();
            roombooking.Charges = float.Parse(Form["Charges"].ToString());
            roombooking.FromDate = DateTime.ParseExact(Form["FromDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            roombooking.ToDate = DateTime.ParseExact(Form["ToDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            roombooking.FromTime = Form["FromTime"].ToString();
            roombooking.ToTime = Form["ToTime"].ToString();
            //roombooking.AvailabilityStatus = Form["AvailabilityStatus"].ToString();
            roombooking.AvailabilityStatus = "Booked";
            roombooking.ExtraBed=Form["hdnextrabed"].ToString();


            int response = roombooking.Save();
            if (response == 1)
            {
                ViewBag.message = "Successfully Inserted";
            }
            else
            {
                ViewBag.message = "Insertion was not successful";
            }

            return RedirectToAction("Room_Bookings");
        }

        //public ActionResult Room_Booking(FormCollection Form)
        //{

        //    try
        //    {
        //        Dictionary<string, object> RoomBookingDict = new Dictionary<string, object>();

        //        if (Form["BookingId"]=="0"||Form["BookingId"]==""||Form["BookingId"]==null)
        //        {
        //            RoomBookingDict.Add("MemberId", Form["MembershipNo"]);
        //            RoomBookingDict.Add("RoomNo", Form["RoomNo"]);
        //            RoomBookingDict.Add("Charges", Form["Charges"]);
        //            RoomBookingDict.Add("FromDate", Form["FromDate"]);
        //            RoomBookingDict.Add("ToDate", Form["ToDate"]);
        //            RoomBookingDict.Add("FromTime", Form["FromTime"]);
        //            RoomBookingDict.Add("ToTime", Form["ToTime"]);
        //            //RoomBookingDict.Add("AvailabilityStatus", Form["AvailabilityStatus"]);
        //            RoomBookingDict.Add("AvailabilityStatus", "Booked");
        //            string response = iRoomBooking.AllocateRoom(RoomBookingDict);
        //            ViewBag.message="Successfully Created";
        //        }
        //        else
        //        {
        //            RoomBookingDict.Add("BookingId", Form["BookingId"]);
        //            RoomBookingDict.Add("MemberId", Form["MembershipNo"]);
        //            RoomBookingDict.Add("RoomNo", Form["RoomNo"]);
        //            RoomBookingDict.Add("Charges", Form["Charges"]);
        //            RoomBookingDict.Add("FromDate", Form["FromDate"]);
        //            RoomBookingDict.Add("ToDate", Form["ToDate"]);
        //            RoomBookingDict.Add("FromTime", Form["FromTime"]);
        //            RoomBookingDict.Add("ToTime", Form["ToTime"]);
        //            //RoomBookingDict.Add("AvailabilityStatus", Form["AvailabilityStatus"]);
        //            RoomBookingDict.Add("AvailabilityStatus", "Booked");
        //            string response = iRoomBooking.UpdateRoomBookingDetails(RoomBookingDict);
        //            ViewBag.message="Successfully Booked";
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.message=e.Message;
        //        ViewBag.innerEx=e.InnerException.Message;
        //    }


        //    return RedirectToAction("Room_Bookings");
        //}
        public ActionResult View_Room_Bookings()
        {
            List<RoomBooking> allocatedrooms = new List<RoomBooking>();
            try
            {
                List<RoomBooking> RoomBookings;
                RoomBookings=iRoomBooking.ViewAllRoomBooking();
               
                for (int i = 0; i<RoomBookings.Count; i++)
                {
                    if (RoomBookings[i].AvailabilityStatus=="Booked")
                    {
                        allocatedrooms.Add(RoomBookings[i]);
                    }
                    else
                    {
                        ViewBag.message="Rooms Booked Not Available";
                    }
                }
               // ViewBag.RoomBookings=allocatedrooms;
                //if (RoomBookings!=null)
                //{
                //    ViewBag.RoomBookings=RoomBookings;
                //}
                //else
                //{
                //    ViewBag.message="Room Booking Details Not Available";
                //}

            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           
            return View(allocatedrooms);
        }

        public JsonResult View_Room_Bookings1()
        {
            List<RoomBooking> allocatedrooms = new List<RoomBooking>();
            List<RoomBooking> RoomBookings;
            RoomBookings = iRoomBooking.ViewAllRoomBooking();
            for (int i = 0; i < RoomBookings.Count; i++)
            {
                if (RoomBookings[i].AvailabilityStatus == "Booked")
                {
                    allocatedrooms.Add(RoomBookings[i]);
                  
                    
                }
                else
                {
                    ViewBag.message = "Rooms Booked Not Available";
                }
            }
            return Json(allocatedrooms, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRoomBooking(int BookingId, string RoomNo)
        {
            try
            {
                string sub;
                sub=iRoomBooking.DeleteRoomBooking(BookingId,RoomNo);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Room Booking Details";
                }
                else
                {
                    ViewBag.message="Room Booking Details Deleted ";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           
            return RedirectToAction("View_Room_Bookings");
        }

        [HttpGet]
        public ActionResult ViewRoomBooking(int BookingId)
        {

            try
            {
                IClubCommmittee iCommittee = new ClubCommittee();
                IRoom iRoom = new Room();
                // Generate Member List
                List<Member> memberList;
                memberList=iCommittee.ShowMembers();
                if (memberList!=null)
                {
                    ViewBag.MemberList=memberList;
                }
                else
                {
                    ViewBag.MemberList="Members Not Available";
                }

                // Generate Room List
                List<Room> roomList;
                roomList=iRoom.ViewAllRoom();
                if (roomList!=null)
                {
                    ViewBag.RoomList=roomList;
                }
                else
                {
                    ViewBag.RoomList="Rooms Not Available";
                }
                Dictionary<string, object> RoomBookingDict;
                RoomBookingDict=iRoomBooking.ViewRoomBooking(BookingId);
                if (RoomBookingDict.Count>0)
                {
                    ViewBag.submit="Update";
                    ViewData["BookingId"]=RoomBookingDict["BookingId"].ToString();
                    ViewData["MemberShipNo"]=RoomBookingDict["MemberId"].ToString();
                    ViewData["RoomNo"]=RoomBookingDict["RoomNo"].ToString();
                    ViewData["Charges"]=RoomBookingDict["Charges"].ToString();
                    ViewData["FromDate"]=RoomBookingDict["FromDate"].ToString();
                    ViewData["ToDate"]=RoomBookingDict["ToDate"].ToString();
                    ViewData["FromTime"]=RoomBookingDict["FromTime"].ToString();
                    ViewData["ToTime"]=RoomBookingDict["ToTime"].ToString();
                    //ViewData["AvailabilityStatus"]=RoomBookingDict["AvailabilityStatus"].ToString();

                }
                else
                {
                    ViewBag.message="Room Booking Details Failed to Load";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            // return RedirectToAction("Room_Bookings");
            return View("Room_Bookings", ViewBag.Data);
        }
    }
}