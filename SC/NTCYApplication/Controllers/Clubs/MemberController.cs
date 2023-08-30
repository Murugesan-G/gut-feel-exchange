using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Mvc;
using System.Threading;

namespace NTCYApplication.Controllers.Clubs
{
    [Authorize(Roles = "Admin,Management, BarWaiter, RestaurantWaiter")]

    public class MemberController : Controller
    {
        IMember iMember = new Member();

        private static ReaderWriterLock lockobj=new ReaderWriterLock();

        //
        // GET: /Member/
        List<Subscription> List;
        [Authorize(Roles = "Admin,Management")]
        [HttpGet]
        public ActionResult CreateMember()
        {
            try
            {
                List=iMember.ShowMemberShipTypes();

                if (List!=null)
                {
                    ViewBag.SubList=List;
                }
                else
                {
                    ViewBag.message="Subscriptions Not Available";
                }
            }     
           catch(Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            return View();
        }


        [Authorize(Roles = "Admin,Management")]
        [HttpPost]
        public ActionResult CreateMember(FormCollection Form)
        {

            lockobj.AcquireWriterLock(-1);
            Member member = new Member();
            member.MemId =(Form["MemId"]=="0"||Form["MemId"]==""||Form["MemId"]==null) ? 0 :
                                                int.Parse(Form["MemId"].ToString());

            member.MemberId = (Form["MemberId"].ToString());
            member.ClubId = int.Parse(Form["ClubId"].ToString());
            member.MembershipNo = Form["MembershipNo"].ToString();
            member.MemberName=Form["MemberName"].ToString();
            member.Address=Form["Address"].ToString();
            member.DOB=DateTime.ParseExact(Form["DOB"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            member.Gender = Form["Gender"].ToString();
            member.MobileNo = Form["MobileNo"].ToString();

            if (Convert.ToString(Form["AltMobileNo"])==null||Convert.ToString(Form["AltMobileNo"])=="")
            {
                member.AltMobileNo= Convert.ToString("0");
            }
            else
            {
                member.AltMobileNo= Form["AltMobileNo"].ToString();
            }
            member.EmailId= Form["EmailId"].ToString();
            member.ProximityCardNo= Form["ProximityCardNo"].ToString();
            member.Guests= Form["Guests"].ToString();
            member.GuestCards= Form["GuestCards"].ToString();
            member.AmenitiesInterested= Form["AmenitiesInterested"].ToString();
            member.MembershipType= Form["MembershipType"].ToString();
            member.MemberSince=DateTime.ParseExact(Form["MemberSince"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);         
            member.MemberShipStartDate=DateTime.ParseExact(Form["MemberShipStartDate"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);            
            member.MemberShipStatus= Form["MemberShipStatus"].ToString();
            member.InitialMembershipAmount=float.Parse(Form["InitialMembershipAmount"].ToString());
            member.MembershipValidity=DateTime.ParseExact(Form["MembershipValidity"].ToString().ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            member.LastSubscriptionPaid=Convert.ToDouble( Form["LastSubscriptionPaid"].ToString());
            member.SubscriptionAmountPaid= Convert.ToDouble(Form["SubscriptionAmountPaid"].ToString());
            //member.SpouseName= Form["SpouseName"].ToString();
            if (Convert.ToString(Form["SpouseName"])==null||Convert.ToString(Form["SpouseName"])=="")
            {
                member.SpouseName= Convert.ToString("NA").ToString();
            }
            else
            {
                member.SpouseName= Form["SpouseName"].ToString();
            }
            member.FathersName= Form["FatherName"].ToString();
            //member.Child1sName= Form[" Child1sName"].ToString();
            if (Convert.ToString(Form["Child1sName"])==null||Convert.ToString(Form["Child1sName"])=="")
            {
                member.Child1sName= Convert.ToString("NA").ToString();
            }
            else
            {
                member.Child1sName= Form["Child1sName"].ToString();
            }
            //member.Child2sName= Form["Child2sName"].ToString();
            if (Convert.ToString(Form["Child2sName"])==null||Convert.ToString(Form["Child2sName"])=="")
            {
                member.Child2sName= Convert.ToString("NA").ToString();
            }
            else
            {
                member.Child2sName= Form["Child2sName"].ToString();
            }
            member.Alive= Form["RadioHidden"].ToString();
            member.Qualification= Form["Qualification"].ToString();
            member.MaritalStatus= Form["MaritalStatus"].ToString();
            member.Profession= Form["Profession"].ToString();


            if (Convert.ToString(Form["DOBOfChild1"])==null||Convert.ToString(Form["DOBOfChild1"])=="")
            {
                member.DOBOfChild1=DateTime.ParseExact("01/01/2000","dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                member.DOBOfChild1=DateTime.ParseExact(Form["DOBOfChild1"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (Convert.ToString(Form["DOBOfChild2"])==null||Convert.ToString(Form["DOBOfChild2"])=="")
            {
                member.DOBOfChild2= DateTime.ParseExact("01/01/2000","dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                member.DOBOfChild2=DateTime.ParseExact(Form["DOBOfChild2"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (Convert.ToString(Form["DOBOfSpouse"])==null||Convert.ToString(Form["DOBOfSpouse"])=="")
            {
                member.DOBOfSpouse= DateTime.ParseExact("01/01/2000","dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                member.DOBOfSpouse=DateTime.ParseExact(Form["DOBOfSpouse"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (Convert.ToString(Form["DOBOfFather"])==null||Convert.ToString(Form["DOBOfFather"])=="")
            {
                member.DOBOfFather= DateTime.ParseExact("01/01/2000","dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                member.DOBOfFather=DateTime.ParseExact(Form["DOBOfFather"].ToString(),"dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            member.Hobbies= Form["Hobbies"].ToString();
            member.Balance=float.Parse(Form["Balance"].ToString());
            member.PaymentStatus= Form["PaymentStatus"].ToString();
            member.MemberType = Form["MemberType"].ToString();
            member.Salutation = Form["Salutation"].ToString();

            int response =member.Save();         
            //iMember.CreateMember(MemberDictionary);
            if(response ==1)
            {
                ViewBag.message="Successfully Inserted";
            }
            else
            {
                ViewBag.message="Insertion was not successful";
            }

            List=iMember.ShowMemberShipTypes();

            if (List!=null)
            {
                ViewBag.SubList=List;
            }
            else
            {
                ViewBag.message="Subscriptions Not Available";
            }
            lockobj.ReleaseWriterLock();
            return View();
        }



        //[Authorize(Roles = "Admin,Management")]
        //[HttpPost]
        //public ActionResult CreateMembers(FormCollection Form) 
        //{
        //    try
        //    {
        //        List<string> errList = new List<string>();
        //        errList.Add("Entered Controller");
        //        Dictionary<string, string> MemberDictionary = new Dictionary<string, string>();
        //        var MembershipNo = Form["MemberId"];
        //        if (Form["MemberId"] == "0" || Form["MemberId"] == "" || Form["MemberId"] == null)
        //        {
        //            errList.Add("Entered If");
        //            ViewBag.submit = "Submit";
        //            MemberDictionary.Add("ClubId", Form["ClubId"]);
        //            MemberDictionary.Add("MembershipNo", Form["MembershipNo"]);
        //            MemberDictionary.Add("MemberName", Form["MemberName"]);
        //            MemberDictionary.Add("Address", Form["Address"]);
        //            MemberDictionary.Add("DOB", Form["DOB"]);
        //            MemberDictionary.Add("Gender", Form["Gender"]);
        //            MemberDictionary.Add("MobileNo", Form["MobileNo"]);
                   
        //            if (Convert.ToString(Form["AltMobileNo"]) == null || Convert.ToString(Form["AltMobileNo"]) == "")
        //            {
        //                MemberDictionary.Add("AltMobileNo", Convert.ToString("0"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("AltMobileNo", Form["AltMobileNo"]);
        //            }
        //            MemberDictionary.Add("EmailId", Form["EmailId"]);
        //            MemberDictionary.Add("ProximityCardNo", Form["ProximityCardNo"]);
        //            MemberDictionary.Add("Guests", Form["Guests"]);
        //            MemberDictionary.Add("GuestCards", Form["GuestCards"]);
        //            MemberDictionary.Add("AmenitiesInterested", Form["AmenitiesInterested"]);
        //            MemberDictionary.Add("MembershipType", Form["MembershipType"]);
        //            MemberDictionary.Add("MemberSince", Form["MemberSince"]);
        //            MemberDictionary.Add("MemberShipStartDate", Form["MemberShipStartDate"]);
        //            MemberDictionary.Add("MemberShipStatus", Form["MemberShipStatus"].ToString());
        //            MemberDictionary.Add("InitialMembershipAmount", Form["InitialMembershipAmount"]);
        //            MemberDictionary.Add("MembershipValidity", Form["MembershipValidity"]);
        //            MemberDictionary.Add("LastSubscriptionPaid", Form["LastSubscriptionPaid"]);
        //            MemberDictionary.Add("SubscriptionAmountPaid", Form["SubscriptionAmountPaid"]);
        //            //MemberDictionary.Add("SpouseName", Form["SpouseName"]);
        //            if(Convert.ToString(Form["SpouseName"]) == null || Convert.ToString(Form["SpouseName"])=="")
        //            {
        //                MemberDictionary.Add("SpouseName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("SpouseName", Form["SpouseName"]);
        //            }
        //            MemberDictionary.Add("FathersName", Form["FatherName"]);
        //            //MemberDictionary.Add("Child1sName", Form[" Child1sName"]);
        //            if (Convert.ToString(Form["Child1sName"]) == null || Convert.ToString(Form["Child1sName"]) == "")
        //            {
        //                MemberDictionary.Add("Child1sName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("Child1sName", Form["Child1sName"]);
        //            }
        //            //MemberDictionary.Add("Child2sName", Form["Child2sName"]);
        //            if (Convert.ToString(Form["Child2sName"]) == null || Convert.ToString(Form["Child2sName"]) == "")
        //            {
        //                MemberDictionary.Add("Child2sName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("Child2sName", Form["Child2sName"]);
        //            }
        //            MemberDictionary.Add("Alive", Form["RadioHidden"]);
        //            MemberDictionary.Add("Qualification", Form["Qualification"]);
        //            MemberDictionary.Add("MaritalStatus", Form["MaritalStatus"]);
        //            MemberDictionary.Add("Profession", Form["Profession"]);
                    

        //            if (Convert.ToString(Form["DOBOfChild1"]) == null || Convert.ToString(Form["DOBOfChild1"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfChild1", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfChild1", Form["DOBOfChild1"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfChild2"])== null||Convert.ToString(Form["DOBOfChild2"])=="")
        //            {
        //                MemberDictionary.Add("DOBOfChild2", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfChild2",Form["DOBOfChild2"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfSpouse"])== null || Convert.ToString(Form["DOBOfSpouse"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfSpouse", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfSpouse", Form["DOBOfSpouse"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfFather"]) == null || Convert.ToString(Form["DOBOfFather"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfFather", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfFather", Form["DOBOfFather"]);
        //            }
                  
        //            MemberDictionary.Add("Hobbies", Form["Hobbies"]);
        //            MemberDictionary.Add("Balance", Form["Balance"]);
        //            MemberDictionary.Add("PaymentStatus", Form["PaymentStatus"]);

        //            errList.Add("Dictionary Populated");
        //            ViewBag.errList=errList;

        //            iMember.CreateMember(MemberDictionary);
        //            ViewBag.message = "Successfully Inserted";

        //        }

        //        else
        //        {
        //            MemberDictionary.Add("MemberId", Form["MemberId"]);
        //            MemberDictionary.Add("ClubId", Form["ClubId"]);
        //            MemberDictionary.Add("MembershipNo", Form["MembershipNo"]);
        //            MemberDictionary.Add("MemberName", Form["MemberName"]);
        //            MemberDictionary.Add("Address", Form["Address"]);
        //            MemberDictionary.Add("DOB", Form["DOB"]);
        //            MemberDictionary.Add("Gender", Form["Gender"]);
        //            MemberDictionary.Add("MobileNo", Form["MobileNo"]);

        //            if (Convert.ToString(Form["AltMobileNo"]) == null || Convert.ToString(Form["AltMobileNo"]) == "")
        //            {
        //                MemberDictionary.Add("AltMobileNo", Convert.ToString("0"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("AltMobileNo", Form["AltMobileNo"]);
        //            }
        //            MemberDictionary.Add("EmailId", Form["EmailId"]);
        //            MemberDictionary.Add("ProximityCardNo", Form["ProximityCardNo"]);
        //            MemberDictionary.Add("Guests", Form["Guests"]);
        //            MemberDictionary.Add("GuestCards", Form["GuestCards"]);
        //            MemberDictionary.Add("AmenitiesInterested", Form["AmenitiesInterested"]);
        //            MemberDictionary.Add("MembershipType", Form["MembershipType"]);
        //            MemberDictionary.Add("MemberSince", Form["MemberSince"]);
        //            MemberDictionary.Add("MemberShipStartDate", Form["MemberShipStartDate"]);
        //            MemberDictionary.Add("MemberShipStatus", Form["MemberShipStatus"].ToString());
        //            MemberDictionary.Add("InitialMembershipAmount", Form["InitialMembershipAmount"]);
        //            MemberDictionary.Add("MembershipValidity", Form["MembershipValidity"]);
        //            MemberDictionary.Add("LastSubscriptionPaid", Form["LastSubscriptionPaid"]);
        //            MemberDictionary.Add("SubscriptionAmountPaid", Form["SubscriptionAmountPaid"]);
        //            //MemberDictionary.Add("SpouseName", Form["SpouseName"]);
        //            if (Convert.ToString(Form["SpouseName"]) == null || Convert.ToString(Form["SpouseName"]) == "")
        //            {
        //                MemberDictionary.Add("SpouseName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("SpouseName", Form["SpouseName"]);
        //            }
        //            MemberDictionary.Add("FathersName", Form["FatherName"]);
        //            //MemberDictionary.Add("Child1sName", Form[" Child1sName"]);
        //            if (Convert.ToString(Form["Child1sName"]) == null || Convert.ToString(Form["Child1sName"]) == "")
        //            {
        //                MemberDictionary.Add("Child1sName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("Child1sName", Form["Child1sName"]);
        //            }
        //            //MemberDictionary.Add("Child2sName", Form["Child2sName"]);
        //            if (Convert.ToString(Form["Child2sName"]) == null || Convert.ToString(Form["Child2sName"]) == "")
        //            {
        //                MemberDictionary.Add("Child2sName", Convert.ToString("NA"));
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("Child2sName", Form["Child2sName"]);
        //            }
        //            MemberDictionary.Add("Alive", Form["RadioHidden"]);
        //            MemberDictionary.Add("Qualification", Form["Qualification"]);
        //            MemberDictionary.Add("MaritalStatus", Form["MaritalStatus"]);
        //            MemberDictionary.Add("Profession", Form["Profession"]);


        //            if (Convert.ToString(Form["DOBOfChild1"]) == null || Convert.ToString(Form["DOBOfChild1"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfChild1", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfChild1", Form["DOBOfChild1"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfChild2"]) == null || Convert.ToString(Form["DOBOfChild2"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfChild2", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfChild2", Form["DOBOfChild2"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfSpouse"]) == null || Convert.ToString(Form["DOBOfSpouse"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfSpouse", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfSpouse", Form["DOBOfSpouse"]);
        //            }
        //            if (Convert.ToString(Form["DOBOfFather"]) == null || Convert.ToString(Form["DOBOfFather"]) == "")
        //            {
        //                MemberDictionary.Add("DOBOfFather", Convert.ToDateTime("01/01/2000").ToString());
        //            }
        //            else
        //            {
        //                MemberDictionary.Add("DOBOfFather", Form["DOBOfFather"]);
        //            }

        //            MemberDictionary.Add("Hobbies", Form["Hobbies"]);
        //            MemberDictionary.Add("Balance", Form["Balance"]);
        //            MemberDictionary.Add("PaymentStatus", Form["PaymentStatus"]);
        //            iMember.UpdateMember(MemberDictionary);
        //            ViewBag.submit = "Submit";
        //            ViewBag.message = "Successfully Updated";

        //        }
        //        List = iMember.ShowMemberShipTypes();

        //        if (List != null)
        //        {
        //            ViewBag.SubList = List;
        //        }
        //        else
        //        {
        //            ViewBag.message = "Membership details Not Available";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.message = e.Message;
        //        ViewBag.innerEx = e.InnerException.Message;
        //    }
        //    return RedirectToAction("CreateMember", "Member");
        //    //return View();
        //}
        [Authorize(Roles = "Admin,Management")]
        [HttpGet]
        public ActionResult ViewMemberDetails(int MemId)
        {
            try
            {
                Dictionary<string, object> MemberDict;
                MemberDict=iMember.ViewMemberDetails(MemId);
                ViewBag.submit="Update";
                ViewData["MemId"]=MemberDict["MemId"].ToString();
                ViewData["MemberId"]=MemberDict["MemberId"].ToString();
                ViewData["MembershipNo"]=MemberDict["MembershipNo"].ToString();
                ViewData["clubid"]=MemberDict["ClubId"].ToString();
                ViewData["MemberName"]=MemberDict["MemberName"].ToString();
                ViewData["Address"]=MemberDict["Address"].ToString();
                ViewData["DOB"]=MemberDict["DOB"].ToString();
                ViewData["Gender"]=MemberDict["Gender"].ToString();
                ViewData["MobileNo"]=MemberDict["MobileNo"].ToString();
                ViewData["AltMobileNo"]=MemberDict["AltMobileNo"].ToString();
                ViewData["EmailId"]=MemberDict["EmailId"].ToString();
                ViewData["ProximityCardNo"]=MemberDict["ProximityCardNo"].ToString();
                ViewData["Guests"]=MemberDict["Guests"].ToString();
                ViewData["GuestCards"]=MemberDict["GuestCards"].ToString();
                ViewData["AmenitiesInterested"]=MemberDict["AmenitiesInterested"].ToString();
                ViewData["MembershipType"]=MemberDict["MembershipType"].ToString();
                ViewData["MemberSince"]=MemberDict["MemberSince"].ToString();
                ViewData["MemberShipStartDate"]=MemberDict["MemberShipStartDate"].ToString();
                ViewData["MemberShipStatus"]=MemberDict["MemberShipStatus"].ToString();
                ViewData["InitialMembershipAmount"]=MemberDict["InitialMembershipAmount"].ToString();
                ViewData["MembershipValidity"]=MemberDict["MembershipValidity"].ToString();
                ViewData["LastSubscriptionPaid"]=MemberDict["LastSubscriptionPaid"].ToString();
                ViewData["SubscriptionAmountPaid"]=MemberDict["SubscriptionAmountPaid"].ToString();
                ViewData["SpouseName"] = MemberDict["SpouseName"].ToString();
                ViewData["FatherName"] = MemberDict["FathersName"].ToString();
                ViewData["Child1sName"] = MemberDict["Child1sName"].ToString();
                ViewData["Child2sName"] = MemberDict["Child2sName"].ToString();
                ViewData["Alive"] = MemberDict["Alive"].ToString();
                ViewData["Qualification"] = MemberDict["Qualification"].ToString();
                ViewData["MaritalStatus"] = MemberDict["MaritalStatus"].ToString();
                ViewData["Profession"] = MemberDict["Profession"].ToString();
                ViewData["DOBOfChild1"] = MemberDict["DOBOfChild1"].ToString();
                ViewData["DOBOfChild2"] = MemberDict["DOBOfChild2"].ToString();
                ViewData["DOBOfSpouse"] = MemberDict["DOBOfSpouse"].ToString();
                ViewData["DOBOfFather"] = MemberDict["DOBOfFather"].ToString();
                ViewData["Hobbies"] = MemberDict["Hobbies"].ToString();
                ViewData["Balance"] = MemberDict["Balance"].ToString();
                ViewData["PaymentStatus"] = MemberDict["PaymentStatus"].ToString();
                ViewData["MemberType"] = MemberDict["MemberType"].ToString();
                ViewData["Salutation"] = MemberDict["Salutation"].ToString();
                List =iMember.ShowMemberShipTypes();

                if (List!=null)
                {
                    ViewBag.SubList=List;
                }
                else
                {
                    ViewBag.message="Members details Not Available";
                }
            }
            catch(Exception e)
            {
                ViewBag.message=e.Message;
                ViewBag.innerEx=e.InnerException.Message;
            }
            
            return View("CreateMember");
        }

        [Authorize(Roles = "Admin,Management")]
        [HttpGet]
        public ActionResult ViewMembers() 
        {
            try
            {
                List<Member> sublist;
                sublist=iMember.ViewAllMembers();
                if (sublist!=null)
                {
                    ViewBag.SubList=sublist;
                }
                else
                {
                    ViewBag.message="Member details Not Available";
                }
            }catch(Exception ex)
            {
                ViewBag.message=ex.Message;
                ViewBag.innerEx=ex.InnerException.Message;
            }
            return View();
        }

        [Authorize(Roles = "Admin,Management")]
        [HttpGet]
        public ActionResult DeleteMember(int MemId)
        {
            try
            {
                string sub;
                sub=iMember.DeleteMember(MemId);
                if (sub==null)
                {
                    ViewBag.message="Unable to Delete Subscription";
                }
                else
                {
                    ViewBag.message="Subsccription Deleted ";
                }
            }
            catch(Exception ex)
            { 
                ViewBag.message=ex.Message;
                ViewBag.innerEx=ex.InnerException.Message;
            }
            return RedirectToAction("ViewMembers");
        }


        [Authorize(Roles = "Admin,Management, BarWaiter, RestaurantWaiter")]
        public JsonResult GetMembers(string Prefix)
        {

            IMember iMember = new Member();
            // Generate Member List
            List<Member> memberList = new List<Member>();
            DataSet ds = iMember.GetMembers(Prefix);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                memberList.Add(new Member
                {

                    MembershipNo = dr["MemberShipNo"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    Status = dr["Status"].ToString()
                });


            }
            return Json(memberList, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Management, BarWaiter, RestaurantWaiter")]
        public JsonResult GetMember(string sMembershipNo)
        {

            IMember iMember = new Member();
            // Generate Member List
            List<Member> memberList = new List<Member>();
            DataSet ds = iMember.GetMember(sMembershipNo);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                memberList.Add(new Member
                {
                    MembershipNo = dr["MemberShipNo"].ToString(),
                    MemberName = dr["MemberName"].ToString(),
                    Status = dr["Status"].ToString()
                });
            }
            return Json(memberList, JsonRequestBehavior.AllowGet);
        }

        public string GetMemberMobileNo(string mNo)
        {
            string mobileNo = "";
            IMember iMember = new Member();
            mobileNo=iMember.GetMemberMobileNo(mNo);
            return mobileNo;
        }
    }
}
