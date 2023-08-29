using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using NTCYApplication.Interfaces;
using NTCYApplication.Models.Club;
using NTCYApplication.Models.FrontOffice;
using NTCYApplication.Models.Room;
using NTCYApplication.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace NTCYApplication.Controllers.FrontOfficeC
{
    [Authorize(Roles = "Admin,FrontOffice,Management")]
    public class FrontOfficeController : Controller
    {
        FrontOfficePaymentInterface iFrontOfficePayment = new FrontOfficePayment();
        IRoomBooking iRoomBooking = new RoomBooking();
        FrontOfficePaymentInterface IFOP = new FrontOfficePayment();
        PaymentInterface Ipayment = new Payment();
        //PaymentSubTableInterface p2 = new PaymentSubTable();
        //
        // GET: /FrontOffice/
        [HttpGet]
        public ActionResult ViewAllFrontOfficePayment()
        {
            try
            {
                List<FrontOfficePayment> FrontOfficePaymentList;
                FrontOfficePaymentList = IFOP.ViewAllFrontOfficePayment();
                //FrontOfficePaymentList = new List<FrontOfficePayment>();
                //FrontOfficePayment fop = new FrontOfficePayment();
                // fop.BillDate = DateTime.Today;
                // FrontOfficePaymentList.Add(fop);

                if (FrontOfficePaymentList != null)
                {
                    ViewBag.FrontOfficePaymentList = FrontOfficePaymentList;
                }
                else
                {
                    ViewBag.message = "FrontOffice Payment Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }

            return View();
        }

        Dictionary<string, List<FrontOfficePayment>> DetailedBillDictionary = new Dictionary<string, List<FrontOfficePayment>>();
        private Task userManager;

        public ActionResult ViewBillPay(string MembershipNo/*, string MemberName*/, DateTime BillDate/*, string Status, string TotalAmount*/)
        {
            try
            {
                List<FrontOfficePayment> FrontOfficePaymentList;
                FrontOfficePaymentList = IFOP.ViewAllFrontOfficePayment();

                for (int i = 0; i < FrontOfficePaymentList.Count; i++)
                {
                    string d = FrontOfficePaymentList[i].BillDate.ToShortDateString();
                    string s = BillDate.ToShortDateString();
                    if (FrontOfficePaymentList[i].MembershipNo == MembershipNo && d == s)
                    {
                        ViewBag.MemberNo = MembershipNo;
                        ViewBag.MemberName = FrontOfficePaymentList[i].MemberName;
                        ViewBag.Date = FrontOfficePaymentList[i].BillDate;
                        ViewBag.Status = FrontOfficePaymentList[i].Status;
                        ViewBag.Amount = FrontOfficePaymentList[i].TotalAmount;
                    }

                }

                //ViewBag.MemberNo=MembershipNo;
                //ViewBag.MemberName=MemberName;
                //ViewBag.Date=BillDate;
                //ViewBag.Status=Status;
                //ViewBag.Amount=TotalAmount;
                DetailedBillDictionary = iFrontOfficePayment.ViewDetailedBill(MembershipNo, BillDate);
                List<FrontOfficePayment> FoodList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> LiquorList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> SubscriptionList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> OtherServicesList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> RoomBookingList = new List<FrontOfficePayment>();
                FoodList = DetailedBillDictionary["ListFood"];
                LiquorList = DetailedBillDictionary["ListLiquor"];
                SubscriptionList = DetailedBillDictionary["ListSubscriptionType"];
                OtherServicesList = DetailedBillDictionary["ListOtherServices"];
                RoomBookingList = DetailedBillDictionary["ListRoomBooking"];
                if (FoodList == null || FoodList.Count == 0)
                {
                    ViewBag.message = "Food Bill Details Not Available";

                }
                else
                {
                    ViewBag.FoodList = FoodList;
                    ViewBag.OrderId = FoodList[0].OrderId;
                    ViewBag.Uname = FoodList[0].UserName;
                }
                if (LiquorList == null || LiquorList.Count == 0)
                {
                    ViewBag.message = "Liquor Bill Details Not Available";
                }
                else
                {
                    ViewBag.LiquorList = LiquorList;
                    ViewBag.OrderId1 = LiquorList[0].OrderId;
                    ViewBag.Uname1 = LiquorList[0].UserName;
                    ViewBag.WaiterName = LiquorList[0].WaiterName;
                }
                if (SubscriptionList == null || SubscriptionList.Count == 0)
                {
                    ViewBag.message = "Subscription Bill Details Not Available";
                }
                else
                {
                    ViewBag.SubscriptionList = SubscriptionList;
                }

                if (OtherServicesList == null || OtherServicesList.Count == 0)
                {
                    ViewBag.message = "Other Services Bill Details Not Available";
                }
                else
                {
                    ViewBag.OtherServicesList = OtherServicesList;
                    ViewBag.OrderId2 = OtherServicesList[0].OrderId;
                }
                if (RoomBookingList == null || RoomBookingList.Count == 0)
                {
                    ViewBag.message = "Room Booking Details Not Available";

                }
                else
                {
                    ViewBag.RoomBookingList = RoomBookingList;
                    ViewBag.OrderId3 = RoomBookingList[0].OrderId;
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }


            return View();
        }


        public ActionResult ViewBillPayHistory(string MembershipNo, /*string MemberName,*/ DateTime BillDate/*, string Status, string TotalAmount*/)
        {
            try
            {
                List<FrontOfficePayment> FOPList;
                FOPList = IFOP.ViewBillHistory();

                for (int i = 0; i < FOPList.Count; i++)
                {

                    if (FOPList[i].MembershipNo == MembershipNo && FOPList[i].BillDate == BillDate)
                    {
                        ViewBag.MemberNo = MembershipNo;
                        ViewBag.MemberName = FOPList[i].MemberName;
                        ViewBag.Date = FOPList[i].BillDate;
                        ViewBag.Status = FOPList[i].Status;
                        ViewBag.Amount = FOPList[i].TotalAmount;
                    }

                }
                //ViewBag.MemberNo=MembershipNo;
                //ViewBag.MemberName=MemberName;
                //ViewBag.Date=BillDate;
                //ViewBag.Status=Status;
                //ViewBag.Amount=TotalAmount;
                int Flag = 1;
                DetailedBillDictionary = iFrontOfficePayment.ViewDetailedBillHistory(MembershipNo, BillDate, Flag);
                List<FrontOfficePayment> FoodList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> LiquorList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> SubscriptionList = new List<FrontOfficePayment>();
                List<FrontOfficePayment> OtherServicesList = new List<FrontOfficePayment>();
                FoodList = DetailedBillDictionary["ListFood"];
                LiquorList = DetailedBillDictionary["ListLiquor"];
                SubscriptionList = DetailedBillDictionary["ListSubscriptionType"];
                OtherServicesList = DetailedBillDictionary["ListOtherServices"];
                if (FoodList == null || FoodList.Count == 0)
                {
                    ViewBag.message = "Food Bill Details Not Available";

                }
                else
                {
                    ViewBag.FoodList = FoodList;
                    ViewBag.OrderId = FoodList[0].OrderId;
                }
                if (LiquorList == null || LiquorList.Count == 0)
                {
                    ViewBag.message = "Liquor Bill Details Not Available";
                }
                else
                {
                    ViewBag.LiquorList = LiquorList;
                    ViewBag.OrderId1 = LiquorList[0].OrderId;
                }
                if (SubscriptionList == null || SubscriptionList.Count == 0)
                {
                    ViewBag.message = "Subscription Bill Details Not Available";
                }
                else
                {
                    ViewBag.SubscriptionList = SubscriptionList;
                }
                if (OtherServicesList == null || OtherServicesList.Count == 0)
                {
                    ViewBag.message = "Other Services Bill Details Not Available";
                }
                else
                {
                    ViewBag.OtherServicesList = OtherServicesList;
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }


            return View();
        }


        [HttpPost]
        public ActionResult InsertAllPayment(Payment PaymentData)
        {
            try
            {
                if (PaymentData != null)
                {

                    Dictionary<string, object> PaymentDictionary = new Dictionary<string, object>();
                    Dictionary<string, object> PaymentSubTableDictionary = new Dictionary<string, object>();

                    PaymentDictionary.Add("MembershipNo", PaymentData.MembershipNo);
                    // string Shortdate = PaymentData.BillDate.ToShortDateString();
                    // string[] dateFormat = Shortdate.Split('-');
                    // string date = dateFormat[0]+"-"+dateFormat[2]+"-"+dateFormat[1];

                    PaymentDictionary.Add("BillDate", Convert.ToDateTime(PaymentData.BillDate));
                    PaymentDictionary.Add("ModeOfPayment", PaymentData.ModeOfPayment);
                    PaymentDictionary.Add("CardType", PaymentData.CardType);
                    PaymentDictionary.Add("CardNo_ChequeNo", PaymentData.CardNo_ChequeNo);
                    PaymentDictionary.Add("ExpDate", PaymentData.ExpDate);
                    PaymentDictionary.Add("BankName", PaymentData.BankName);
                    PaymentDictionary.Add("Amount", PaymentData.Amount);
                    string response = Ipayment.InsertPaymentDetails(PaymentDictionary);
                    if (response == "1")
                    {

                        ViewBag.message = "Payment was Successfull";
                    }
                    else
                    {
                        ViewBag.message = "Payment was UnSuccessfull";
                    }

                }


            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }

            return RedirectToAction("ViewAllFrontOfficePayment");
        }

        [HttpPost]
        public ActionResult InsertIndividualPayment(Payment PaymentData, string BillNo)
        {
            try
            {
                if (PaymentData != null)
                {
                    Dictionary<string, object> PaymentDictionary = new Dictionary<string, object>();
                    Dictionary<string, object> PaymentSubTableDictionary = new Dictionary<string, object>();
                    PaymentDictionary.Add("MembershipNo", PaymentData.MembershipNo);
                    PaymentDictionary.Add("ModeOfPayment", PaymentData.ModeOfPayment);
                    PaymentDictionary.Add("CardType", PaymentData.CardType);
                    PaymentDictionary.Add("CardNo_ChequeNo", PaymentData.CardNo_ChequeNo);
                    PaymentDictionary.Add("ExpDate", PaymentData.ExpDate);
                    PaymentDictionary.Add("BankName", PaymentData.BankName);
                    PaymentDictionary.Add("ServiceType", PaymentData.Flag);
                    PaymentDictionary.Add("BillDate", PaymentData.BillDate);
                    PaymentDictionary.Add("Amount", PaymentData.Amount);
                    string response = Ipayment.InsertIndividualPaymentDetails(PaymentDictionary);
                    if (Convert.ToInt32(response) >= 1)
                    {
                        string message = string.Empty;
                        string billnumber = string.Empty;
                        IMember iMember = new Member();
                        string recipient = iMember.GetMemberMobileNo(PaymentData.MembershipNo.Trim());
                        billnumber = BillNo.Trim() + " on " + PaymentData.BillDate.ToShortDateString();
                        try
                        {
                            string APIKey = "SgTUwSrw2v0-Ab5OE4IzL9ZmzWWDLzJT7hWdseFcPB";
                            //message = "Dear Member, Thanks for Paying " + PaymentData.Amount.ToString().Trim() + " amount towards bill number " + BillNo.Trim() + " on " + PaymentData.BillDate.ToShortDateString() + ".";
                            message = "Dear Member, Thanks for Paying " + PaymentData.Amount.ToString().Trim() + " amount towards bill number " + billnumber + ". - THE NEW TOWM CLUB YELAHANKA";
                            String encodedMessage = HttpUtility.UrlEncode(message);
                            using (var webClient = new WebClient())
                            {
                                ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                byte[] response1 = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection(){
                                                     {"apikey" , APIKey},
                                                     {"numbers" , recipient.Trim()},
                                                     {"message" , message},
                                                     {"sender" , "NTCYNK"}});
                                string result = System.Text.Encoding.UTF8.GetString(response1);
                                dynamic stuff = JObject.Parse(result);
                                string status = stuff.status;
                                string batch_Id = stuff.batch_id;
                                int res = iMember.SaveSMSLog(PaymentData.MembershipNo, recipient, float.Parse(PaymentData.Amount.ToString()), status, batch_Id);
                            }
                        }
                        catch (Exception e)
                        {
                            iMember.SaveSMSLog(PaymentData.MembershipNo, recipient, float.Parse(PaymentData.Amount.ToString()), e.Message, "0");
                            //throw (e);
                        }
                        return Json("Payment was Successfull", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Payment was not Successfull", JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }
            return RedirectToAction("ViewBillPay", "FrontOffice");
        }

        public ActionResult BillHistory()
        {
            try
            {
                List<FrontOfficePayment> FOPList;
                FOPList = IFOP.ViewBillHistory();
                if (FOPList != null)
                {
                    ViewBag.FOPList = FOPList;
                }
                else
                {
                    ViewBag.message = "FrontOffice Payment Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }


            return View();
        }

        public JsonResult SearchBillHistory(FrontOfficePayment PaymentData)
        {
            List<FrontOfficePayment> FOPList;
            Dictionary<string, object> FrontOfficePaymentDictionary = new Dictionary<string, object>();

            if (PaymentData.MemberName == "" || PaymentData.MemberName == null)
            {
                FrontOfficePaymentDictionary.Add("MemberName", DBNull.Value);
                FrontOfficePaymentDictionary.Add("FromDate", PaymentData.FromDate);
                FrontOfficePaymentDictionary.Add("ToDate", PaymentData.ToDate);
            }
            else
            {
                FrontOfficePaymentDictionary.Add("MemberName", PaymentData.MemberName);
                FrontOfficePaymentDictionary.Add("FromDate", PaymentData.FromDate);
                FrontOfficePaymentDictionary.Add("ToDate", PaymentData.ToDate);
            }
            FOPList = IFOP.SearchBillHistory(FrontOfficePaymentDictionary);
            if (FOPList != null)
            {
                ViewBag.FOPList = FOPList;
            }
            else
            {
                ViewBag.message = "FrontOffice Payment Details Not Available";
            }
            return Json(FOPList, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Attendance()
        {

            try
            {
                List<FrontOfficePayment> Attendance;
                Attendance = IFOP.Attendance();
                if (Attendance != null)
                {
                    ViewBag.Attendance = Attendance;
                }
                else
                {
                    ViewBag.message = "Attendance Details Not Available";
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }


            return View();
        }

        public ActionResult ServiceCollection()
        {
            Services srv = new Services();
            List<Services> svList = new List<Services>();

            //List<Services> svcList = new List<Services>();

            svList = srv.ShowAllServices();
            ViewBag.ServiceList = svList;
            return View();
        }

        [HttpPost]
        public ActionResult ServiceCollection(FormCollection Form)
        {
            try
            {
                Services srv = new Services();
                List<Services> svList = new List<Services>();
                ServicesCollection svc = new ServicesCollection();
                int response = 0;
                if ((Form["ServiceCollectionId"] == "0" || Form["ServiceCollectionId"] == "" || Form["ServiceCollectionId"] == null))
                {
                    svc.ServiceId = int.Parse(Form["ServiceId"]);
                    svc.MembershipNo = Form["MembershipNo"].ToString();
                    svc.NumberOfGuests = int.Parse(Form["NumberOfGuests"]);
                    svc.PayType = Form["PayType"].ToString();
                    svc.Amount = float.Parse(Form["Charges"]);
                    svc.GST = float.Parse(Form["GST"]);
                    svc.StartDate = DateTime.ParseExact(Form["StartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    svc.EndDate = DateTime.ParseExact(Form["EndDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    svc.Total = float.Parse(Form["Total"]);
                    svc.Status = Form["Status"].ToString();
                    response = svc.Save();
                    if (response == 1)
                    {
                        ViewBag.message = "Saved Successfully";
                    }
                    else
                    {
                        ViewBag.message = "Something Went Wrong...!";
                    }


                    //List<Services> svcList = new List<Services>();

                    svList = srv.ShowAllServices();
                    ViewBag.ServiceList = svList;

                }
                else
                {
                    svc.ServiceId = int.Parse(Form["ServiceId"]);
                    svc.ServCollectionId = int.Parse(Form["ServiceCollectionId"]);
                    svc.MembershipNo = Form["MembershipNo"].ToString();
                    svc.NumberOfGuests = int.Parse(Form["NumberOfGuests"]);
                    svc.PayType = Form["PayType"].ToString();
                    svc.Amount = float.Parse(Form["Charges"]);
                    svc.GST = float.Parse(Form["GST"]);
                    svc.StartDate = DateTime.ParseExact(Form["StartDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    svc.EndDate = DateTime.ParseExact(Form["EndDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    svc.Total = int.Parse(Form["Total"]);
                    svc.Status = Form["Status"].ToString();
                    response = svc.Save();
                    if (response == 1)
                    {
                        ViewBag.message = "Updated Successfully";
                    }
                    else
                    {
                        ViewBag.message = "Something Went Wrong...!";
                    }
                    svList = srv.ShowAllServices();
                    ViewBag.ServiceList = svList;
                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }
            return View();
        }


        [HttpGet]
        public ActionResult ViewServiceCollection(int ServCollectionId)
        {
            ServicesCollection sc = new ServicesCollection();
            List<ServicesCollection> svc = new List<ServicesCollection>();
            Services srv = new Services();
            List<Services> svList = new List<Services>();

            //List<Services> svcList = new List<Services>();

            svList = srv.ShowAllServices();
            ViewBag.ServiceList = svList;
            svc = sc.ViewServiceCollection(ServCollectionId);
            try
            {


                if (svc.Count > 0)
                {
                    ViewBag.submit = "Update";
                    ViewData["ServiceCollectionId"] = svc[0].ServCollectionId;
                    ViewData["MembershipNo"] = svc[0].MembershipNo;
                    ViewData["MemberName"] = svc[0].MemberName;
                    ViewData["NumberOfGuests"] = svc[0].NumberOfGuests;
                    ViewData["PayType"] = svc[0].PayType;
                    ViewData["Charges"] = svc[0].Amount;
                    ViewData["GST"] = svc[0].GST;
                    //DateTime sd = DateTime.Parse(svc[0].StartDate.ToString(), CultureInfo.InvariantCulture);
                    DateTime sd = Convert.ToDateTime(svc[0].StartDate.ToString());
                    ViewData["StartDate"] = sd.ToString("dd/MM/yyyy");
                    //DateTime ed = DateTime.Parse(svc[0].EndDate.ToString(), CultureInfo.InvariantCulture);
                    //ViewData["EndDate"]=ed.ToString("dd/MM/yyyy");
                    //DateTime.ParseExact(svc[0].StartDate.ToString(),"MM/dd/yyyy", CultureInfo.InvariantCulture);
                    // ViewData["EndDate"]=DateTime.ParseExact(svc[0].EndDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ViewData["Total"] = svc[0].Total;
                    ViewData["Status"] = svc[0].Status;
                    ViewData["ServiceId"] = svc[0].ServiceId;
                }
                else
                {
                    ViewBag.message = "Food Details Failed to Load";
                }

            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                ViewBag.innerEx = e.InnerException.Message;
            }

            return View("ServiceCollection", ViewBag.Data);
        }


        public ActionResult ViewAllServiceCollections()
        {
            ServicesCollection sc = new ServicesCollection();
            List<ServicesCollection> svc = new List<ServicesCollection>();
            svc = sc.ViewAllServiceCollections();
            return View(svc);
        }

        public ActionResult ViewAllPendingSubscriptions()
        {
            Member sc = new Member();
            List<Member> mps = new List<Member>();
            mps = sc.ViewAllPendingSubscriptions();
            return View(mps);
        }

        [HttpGet]
        public ActionResult DeleteGuestBill(int ServCollectionId)
        {
            try
            {
                string sub;
                sub = IFOP.DeleteServicesCollection(ServCollectionId);
                if (sub == null)
                {
                    ViewBag.message = "Unable to Delete Guest Bill";
                }
                else
                {
                    ViewBag.message = "Guest Bill Deleted ";
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.innerEx = ex.InnerException.Message;
            }
            return RedirectToAction("ViewAllServiceCollections");
        }

        public JsonResult GetGSTBasedOnService(string ServiceCode)
        {
            ServicesCollection sc = new ServicesCollection();
            float GST = 0;
            int serviceCode = 0;
            int.TryParse(ServiceCode.Trim(), out serviceCode);
            GST = sc.GetGSTBasedOnService(serviceCode);
            return Json(GST, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChargeBasedOnService(string paytype, string ServiceCode)
        {
            ServicesCollection sc = new ServicesCollection();
            float Charge = 0;
            int serviceCode = 0;
            int.TryParse(ServiceCode.Trim(), out serviceCode);
            Charge = sc.GetChargeBasedOnService(paytype, serviceCode);
            return Json(Charge, JsonRequestBehavior.AllowGet);
        }
        ApplicationDbContext context;
        public ActionResult DeleteBill(string uname, string pwd, string mNo, string date, string billType)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.FindAsync(uname, pwd).Result;
            DateTime billDate = DateTime.Now;
            DateTime.TryParse(date, out billDate);
            int count = 0;
            if (user == null)
            {
                return Json("Invalid Username or Password", JsonRequestBehavior.AllowGet);
            }
            if (user != null)
            {
                context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var rol = UserManager.GetRoles(user.Id);

                foreach (var rolenames in rol)
                {
                    if (rolenames.ToUpper() == "ADMIN" || rolenames.ToUpper() == "MANAGEMENT")
                    {
                        count = count + 1;
                    }
                }
                if (count <= 0)
                {
                    return Json("only admin/management can delete bill", JsonRequestBehavior.AllowGet);
                }
                if (count > 0)
                {
                    int res = 0;
                    res = Ipayment.DeleteBill(mNo, billDate, billType);
                    if (res > 0)
                    {
                        return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
                    }
                    if (res <= 0)
                    {
                        return Json("unable to delete bill", JsonRequestBehavior.AllowGet);
                    }

                }
            }

            return null;
        }
    }
}
