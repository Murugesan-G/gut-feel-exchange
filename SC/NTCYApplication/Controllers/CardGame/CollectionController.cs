using NTCYApplication.Interfaces;
using NTCYApplication.Models.CardGame;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCYApplication.Controllers.CardGame
{
    [Authorize(Roles = "Admin,KittyPaymentCollector,Management")]
    public class CollectionController : Controller
    {
        CardTableInterface viewCard = new CardTable();
        ICardGame iCardGame = new CardRoomGame();
        IClubCommmittee iCommittee = new ClubCommittee();
        IMember iMember = new Member();
        //
        // GET: /Collection/

        public ActionResult Collection()
        {
            try
            {
                List<Member> memberList;
                memberList=iMember.ViewAllMembers();
                ViewBag.MemberList=memberList;

                List<CardTable> tableNoList;
                tableNoList=viewCard.ViewAllCardTables();
                if (tableNoList.Count>0)
                {
                    ViewBag.TableNoList=tableNoList;
                }
                else
                {
                    ViewBag.TableNoList="Table Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }



            return View();
        }

        public JsonResult AutoComplete(string Prefix)
        {

            IMember iMember = new Member();
            // Generate Member List
            List<Member> memberList = new List<Member>();
            DataSet ds = iMember.SearchMembers(Prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                memberList.Add(new Member
                {

                    MemberName=dr["MemberName"].ToString()
                });


            }
            return Json(memberList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Collection(FormCollection Form)
        {
            try
            {
                Dictionary<string, object> CardGameDictionary = new Dictionary<string, object>();
                var GameId = Form["GameId"];
                if (Form["GameId"]=="0"||Form["GameId"]==""||Form["GameId"]==null)
                {
                    ViewBag.submit="Submit";

                    CardGameDictionary.Add("UserId", 1);
                    CardGameDictionary.Add("TableNo", Form["TableNo"]);

                    if(Form["GuestName"]==""||Form["GuestName"]==null)
                    {
                        CardGameDictionary.Add("Winner", Form["MemberName"]);
                    }
                    else
                    {
                        CardGameDictionary.Add("Winner", Form["GuestName"]+"(Guest)");
                    }
                   
                    CardGameDictionary.Add("Game", Form["Game"]);
                    if (Form["AmountPaid"]==""||Form["AmountPaid"]==null)
                    {
                        CardGameDictionary.Add("AmountPaid", 0);
                       
                    }
                    else
                    {
                        CardGameDictionary.Add("AmountPaid", Form["AmountPaid"]);
                        
                    }
                    CardGameDictionary.Add("Status", "Not Paid");
                    CardGameDictionary.Add("Date", DateTime.Now);
                    iCardGame.CreateKittyCollection(CardGameDictionary);
                    ViewBag.message="Card Game Inserted Successfully ";

                    //List<Member> memberList;
                    //memberList=iMember.ViewAllMembers();
                    //ViewBag.MemberList=memberList;

                    //List<CardTable> tableNoList;
                    //tableNoList=viewCard.ViewAllCardTables();
                    //if (tableNoList.Count>0)
                    //{
                    //    ViewBag.TableNoList=tableNoList;
                    //}
                    //else
                    //{
                    //    ViewBag.TableNoList="Table Details Not Available";
                    //}

                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return RedirectToAction("Collection");
            //return View("Collection");
        }

        public ActionResult KittyCollectionTableView()
        {
            try
            {
                string Status = "Paid";
                List<CardRoomGame> gameList;
                gameList=iCardGame.ViewPaidKittyCollection(Status);
                if (gameList!=null&&gameList.Count>0)
                {
                    ViewBag.CardGameList=gameList;
                }
                else
                {
                    ViewBag.CardGameList=null;
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }


            return View();
        }

    }
}
