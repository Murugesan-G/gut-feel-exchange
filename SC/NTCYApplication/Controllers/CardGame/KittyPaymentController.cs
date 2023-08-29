using NTCYApplication.Interfaces;
using NTCYApplication.Models.CardGame;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.CardGame
{
    [Authorize(Roles = "Admin,KittyPaymentCollector,Management")]
    public class KittyPaymentController : Controller
    {
        ICardGame iCardGame = new CardRoomGame();
        IClubCommmittee iCommittee = new ClubCommittee();
        //
        // GET: /KittyPayment/

        public ActionResult KittyPaymentView()
        {
            try
            {
                string Status = "Not Paid";
                List<CardRoomGame> gameList;
                gameList=iCardGame.ViewAllKittyCollection(Status);
                if (gameList!=null&&gameList.Count>0)
                {
                    ViewBag.CardGameList=gameList;
                }
                else
                {
                    ViewBag.CardGameList=null;
                }

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
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
           
            return View();
        }


        public ActionResult PayKittyAmount(int? GameId,double AmountPaid)
        {
            try
            {
                Dictionary<string, object> CardGameDictionary = new Dictionary<string, object>();
          
            if (GameId>0)
            {
                //ViewBag.submit="Submit";
                CardGameDictionary.Add("GameId", GameId);
                CardGameDictionary.Add("UserId", 1);
                CardGameDictionary.Add("AmountPaid", AmountPaid);
                CardGameDictionary.Add("Status","Paid");
                CardGameDictionary.Add("Date", DateTime.Now);
                string response=iCardGame.EditKittyCollection(CardGameDictionary);
                if (response=="1")
                {
                    ViewBag.Status="Paid";
                }
            }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            

            return RedirectToAction("KittyPaymentView");
        }

        //public ActionResult SearchMember(string MemberShipNo) 
        //{
        //    string Status = "Not Paid";
        //    List<CardRoomGame> gameList;
        //    gameList=iCardGame.SearchMemberById(MemberShipNo,Status);
        //    if (gameList!=null&&gameList.Count>0)
        //    {
        //        ViewBag.CardGameList=gameList;
        //    }
        //    else
        //    {
        //        ViewBag.CardGameList=null;
        //    }
        //    return RedirectToAction("KittyPaymentView");
        //}

        public JsonResult SearchMember(string MembershipName)
        {
            string Status = "Not Paid";
            List<CardRoomGame> gameList;
            gameList=iCardGame.SearchMemberById(MembershipName, Status);
            if (gameList!=null&&gameList.Count>0)
            {
                ViewBag.CardGameList=gameList;
            }
            else
            {
                ViewBag.CardGameList=null;
            }
            return Json(gameList, JsonRequestBehavior.AllowGet);
        }
    }
}
