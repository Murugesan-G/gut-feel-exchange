using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTCY_mvc4.Controllers
{
    [Authorize(Roles = "Admin,Management")]
    public class ClubCommitteeController : Controller
    {
        IClubCommmittee iCommittee = new ClubCommittee();
        IMember iMember = new Member();
        List<Member> searchList = new List<Member>();
        //
        // GET: /ClubCommittee/

        public ActionResult CreateCommittee()
        {
            try
            {
                List<Member> memberList;
                memberList=iCommittee.ShowMembers();
                if (memberList!=null)
                {
                    ViewBag.MemberList=memberList;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
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
        public ActionResult CreateCommittee(FormCollection Form)
        {
            try
            {
                string response = "";
                response=iCommittee.CheckCommitteeTenure();
                
                    Dictionary<string, object> ClubComitteeDictionary = new Dictionary<string, object>();
                    //string response = "";
                    var CommitteeId = Form["CommitteeId"];
                if (Form["CommitteeId"]=="0"||Form["CommitteeId"]==""||Form["CommitteeId"]==null)
                {
                    if (response=="Active")
                    {
                        ViewBag.message="you cannot add another, until the committee is inactive";
                    }
                    else
                    {
                        ViewBag.submit="Submit";

                        ClubComitteeDictionary.Add("ClubId", 13);
                        ClubComitteeDictionary.Add("Chairman", Form["Chairman"]+","+Form["ChairmanStartDate"]+","+Form["ChairmanEndDate"]+","+Form["ChairmanStatus"]);
                        ClubComitteeDictionary.Add("President", Form["President"]+","+Form["PresidentStartDate"]+","+Form["PresidentEndDate"]+","+Form["PresidentStatus"]+"");
                        ClubComitteeDictionary.Add("VicePresident", Form["VicePresident"]+","+Form["VicePresidentStartDate"]+","+Form["VicePresidentEndDate"]+","+Form["VicePresidentStatus"]);
                        ClubComitteeDictionary.Add("Secretary", Form["Secretary"]+","+Form["SecretaryStartDate"]+","+Form["SecretaryEndDate"]+","+Form["SecretaryStatus"]);
                        ClubComitteeDictionary.Add("Treasurer", Form["Treasurer"]+","+Form["TreasurerStartDate"]+","+Form["TreasurerEndDate"]+","+Form["TreasurerStatus"]);
                        ClubComitteeDictionary.Add("CommitteeMembers", Form["memberList"]);
                        ClubComitteeDictionary.Add("Status", Form["Status"]);
                        ClubComitteeDictionary.Add("StartDate", Form["StartDate"]);
                        ClubComitteeDictionary.Add("EndDate", Form["EndDate"]);
                        response=iCommittee.CreateCommittee(ClubComitteeDictionary);
                        if (response=="1")
                        {
                            ViewBag.message="Club Committee Inserted Successfully ";
                        }
                        else
                        {
                            ViewBag.message="Insertion Failed ";
                        }
                    }
                }

                else
                {
                    ClubComitteeDictionary.Add("CommitteeId", Form["CommitteeId"]);
                    ClubComitteeDictionary.Add("ClubId", 13);
                    ClubComitteeDictionary.Add("Chairman", Form["Chairman"]+","+Form["ChairmanStartDate"]+","+Form["ChairmanEndDate"]+","+Form["ChairmanStatus"]);
                    ClubComitteeDictionary.Add("President", Form["President"]+","+Form["PresidentStartDate"]+","+Form["PresidentEndDate"]+","+Form["PresidentStatus"]+"");
                    ClubComitteeDictionary.Add("VicePresident", Form["VicePresident"]+","+Form["VicePresidentStartDate"]+","+Form["VicePresidentEndDate"]+","+Form["VicePresidentStatus"]);
                    ClubComitteeDictionary.Add("Secretary", Form["Secretary"]+","+Form["SecretaryStartDate"]+","+Form["SecretaryEndDate"]+","+Form["SecretaryStatus"]);
                    ClubComitteeDictionary.Add("Treasurer", Form["Treasurer"]+","+Form["TreasurerStartDate"]+","+Form["TreasurerEndDate"]+","+Form["TreasurerStatus"]);
                    ClubComitteeDictionary.Add("CommitteeMembers", Form["memberList"]);
                    ClubComitteeDictionary.Add("Status", Form["Status"]);
                    ClubComitteeDictionary.Add("StartDate", Form["StartDate"]);
                    ClubComitteeDictionary.Add("EndDate", Form["EndDate"]);
                    response=iCommittee.UpdateCommittee(ClubComitteeDictionary);
                    if (response=="1")
                    {
                        List<Member> memberList;
                        memberList=iCommittee.ShowMembers();
                        if (memberList!=null)
                        {
                            ViewBag.MemberList=memberList;
                        }
                        else
                        {
                            ViewBag.message="Subscriptions Not Available";
                        }
                        ViewBag.submit="Submit";
                        ViewBag.message="Club Committee Updated Successfully ";
                    }
                    else
                    {
                        ViewBag.message="Updating Failed ";
                    }
                }
                
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return RedirectToAction("CreateCommittee");
        }


        [HttpGet]
        public ActionResult ViewCommitteeMemberDetails(int? CommitteeId)
        {
            try
            {
                Dictionary<string, object> ComitteeDict;
                ComitteeDict=iCommittee.SelectCommittee(CommitteeId);

                ViewBag.submit="Update";
                ViewData["CommitteeId"]=ComitteeDict["CommitteeId"].ToString();
                ViewData["ClubId"]=ComitteeDict["ClubId"].ToString();
                string[] Chairman = ComitteeDict["Chairman"].ToString().Split(',');
                string[] President = ComitteeDict["President"].ToString().Split(',');
                string[] VicePresident = ComitteeDict["VicePresident"].ToString().Split(',');
                string[] Secretary = ComitteeDict["Secretary"].ToString().Split(',');
                string[] Treasurer = ComitteeDict["Treasurer"].ToString().Split(',');
                ViewData["Chairman"]=Chairman[0].ToString().Trim();
                ViewData["ChairmanStartDate"]=Chairman[1].ToString().Trim();
                ViewData["ChairmanEndDate"]=Chairman[2].ToString().Trim();
                ViewData["ChairmanStatus"]=Chairman[3].ToString().Trim();

                ViewData["President"]=President[0].ToString().Trim();
                ViewData["PresidentStartDate"]=President[1].ToString().Trim();
                ViewData["PresidentEndDate"]=President[2].ToString().Trim();
                ViewData["PresidentStatus"]=President[3].ToString().Trim();

                ViewData["VicePresident"]=VicePresident[0].ToString().Trim();
                ViewData["VicePresidentStartDate"]=VicePresident[1].ToString().Trim();
                ViewData["VicePresidentEndDate"]=VicePresident[2].ToString().Trim();
                ViewData["VicePresidentStatus"]=VicePresident[3].ToString().Trim();

                ViewData["Secretary"]=Secretary[0].ToString().Trim();
                ViewData["SecretaryStartDate"]=Secretary[1].ToString().Trim();
                ViewData["SecretaryEndDate"]=Secretary[2].ToString().Trim();
                ViewData["SecretaryStatus"]=Secretary[3].ToString().Trim();


                ViewData["Treasurer"]=Treasurer[0].ToString().Trim();
                ViewData["TreasurerStartDate"]=Treasurer[1].ToString().Trim();
                ViewData["TreasurerEndDate"]=Treasurer[2].ToString().Trim();
                ViewData["TreasurerStatus"]=Treasurer[0].ToString().Trim();

                ViewData["CommitteeMembers"]=ComitteeDict["CommitteeMembers"].ToString();
                ViewData["Status"]=ComitteeDict["Status"].ToString();
                DateTime x = Convert.ToDateTime(ComitteeDict["StartDate"].ToString());
                ViewData["StartDate"]=x.ToShortDateString();
                DateTime y = Convert.ToDateTime(ComitteeDict["EndDate"].ToString());
                ViewData["EndDate"] = y.ToShortDateString();
                List<Member> memberList;
                memberList=iCommittee.ShowMembers();
                if (memberList!=null)
                {
                    ViewBag.MemberList=memberList;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View("CreateCommittee");
        }

        public ActionResult ViewCommitteeMembers()
        {
            try
            {
                List<ClubCommittee> committeeList;
                committeeList=iCommittee.ViewCommiteeMembers();
                if (committeeList!=null)
                {
                    ViewBag.CommitteeList=committeeList;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            return View();
        }



        //public ActionResult DeleteCommittee(int? CommitteeId)
        //{
        //    try
        //    {
        //        string sub;
        //        sub=iCommittee.DeleteCommittee(CommitteeId);
        //        if (sub==null)
        //        {
        //            ViewBag.message="Unable to Delete Committee";
        //        }
        //        else
        //        {
        //            ViewBag.message="Committee Deleted ";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.message=e.Message;
        //        ViewBag.innerEx=e.InnerException.Message;
        //    }
        //    return RedirectToAction("ViewCommitteeMembers");
        //}

        public JsonResult AutoComplete(string Prefix)
        {
            try
            {
                if (Prefix!=null)
                {

                    DataSet ds = iMember.SearchMembers(Prefix);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        searchList.Add(new Member
                        {

                            MemberName=dr["MemberName"].ToString()
                        });


                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }

            var search = searchList.ToList(); 

            return Json(search, JsonRequestBehavior.AllowGet);

        }
    }
}
