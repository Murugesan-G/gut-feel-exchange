using NTCYApplication.Models.Club;
using NTCYApplication.Models.TallyIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace NTCYApplication.Controllers.TallyIntegration
{
    public class TallyIntegrationController : Controller
    {
        ClubAccounts ca = new ClubAccounts();
        NTCYTallyIntegration NTI = new NTCYTallyIntegration();
        Tally_Integration TI = new Tally_Integration();

        string strVoucherType = "Reciepts";
       
        string BarDuesLedger = "";
        string MonthlySubscriptionLedger = "";
        string ledgerType = "CreditLedger";
       
        // GET: TallyIntegration
        public ActionResult Index()
        {
            Dictionary<string, object> TallyIntegrationDictionary = new Dictionary<string, object>();
            TallyIntegrationDictionary=ca.ViewDetails();
           
            BarDuesLedger=TallyIntegrationDictionary["Ledger1"].ToString();
            MonthlySubscriptionLedger=TallyIntegrationDictionary["Ledger2"].ToString();
           

            DataTable dt = new DataTable();
            Session["LastOrderId"]=0;
            string LastOrderId = Session["LastOrderId"].ToString();
            dt=NTI.GetLiquorBills(LastOrderId);

            if (dt.Rows.Count>0)
            {
                if (Session["LastOrderId"].ToString()=="0")
                {
                    Session["LastOrderId"]= dt.Rows[0]["OrderId"].ToString();
                    for (int i = 0; i<dt.Rows.Count; i++)
                    {

                        Session["LastOrderId"]=dt.Rows[i]["OrderId"].ToString();
                    }
                    PostLiquorDataTotally(dt);
                }
                else
                {
                    for (int i = 0; i<dt.Rows.Count; i++)
                    {

                        Session["LastOrderId"]=LastOrderId.Replace(dt.Rows[i-1]["OrderId"].ToString(), dt.Rows[i]["OrderId"].ToString());
                    }
                    PostLiquorDataTotally(dt);
                }
               

                

            }
            else
            {
                PostLiquorDataTotally(dt);
            }

            return View();
        }

        public void PostLiquorDataTotally(DataTable dt)
        { 
            string status = "";
            double sum = 0;
            foreach(DataRow dr in dt.Rows)
            {
                sum=sum+Convert.ToDouble(dr["TotalAmount"].ToString());
                string Narration = "Billwise- BillNo:"+dr["OrderId"]+",MembershipNo:"+dr["MembershipNo"]+",BillDate:"+dr["Date"]+"Total:"+dr["TotalAmount"];
                string PayMode = dr["ModeOfPayment"].ToString();
                if (PayMode=="Cash")
                {
                    status=TI.PostDataToTally("Create", dr["OrderId"].ToString(), dr["MembershipNo"].ToString(), dr["Date"].ToString(), dr["TotalAmount"].ToString(), strVoucherType,Narration, ledgerType, BarDuesLedger, PayMode);
                }
                else
                {
                    status=TI.PostDataToTally("Create", dr["OrderId"].ToString(), dr["MembershipNo"].ToString(), dr["Date"].ToString(), dr["TotalAmount"].ToString(), strVoucherType, Narration, ledgerType, BarDuesLedger, PayMode);

                }

            }
        }

        //public string CreateVoucherXml(DataTable dt) // request xml and response for ledger creation
        //{
        //    Dictionary<string, object> TallyIntegrationDictionary = new Dictionary<string, object>();
        //    TallyIntegrationDictionary=ca.ViewDetails();
        //    string Url = TallyIntegrationDictionary["Url"].ToString();
        //    string Port = TallyIntegrationDictionary["Port"].ToString();
        //    string Username = TallyIntegrationDictionary["Username"].ToString();
        //    string Password = TallyIntegrationDictionary["Password"].ToString();
        //    string BarDues = TallyIntegrationDictionary["Ledger1"].ToString();
        //    string MonthlySubscription = TallyIntegrationDictionary["Ledger2"].ToString();
        //    string Ledger3 = TallyIntegrationDictionary["Ledger3"].ToString();
        //    string Ledger4 = TallyIntegrationDictionary["Ledger4"].ToString();

        //    try 
        //    {
        //        string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
        //    "<ENVELOPE>"+
        //        "<HEADER>"+
        //            "<VERSION>1</VERSION>"+
        //            "<TALLYREQUEST>Import</TALLYREQUEST>"+
        //            "<TYPE>Data</TYPE>"+
        //            "<ID>Vouchers</ID>"+
        //        "</HEADER>"+
        //        "<BODY>"+
        //            "<DESC>"+
        //            "</DESC>"+
        //            "<DATA>"+
        //                "<TALLYMESSAGE>"+
        //                    "<VOUCHER>"+
        //                        "<DATE>"+strDate+"</DATE>"+
        //                        "<NARRATION>"+strNartxt+"</NARRATION>"+
        //                        "<VOUCHERTYPENAME>"+strVTypeName+"</VOUCHERTYPENAME>"+
        //                        "<VOUCHERNUMBER>"+strVNumber+"</VOUCHERNUMBER>"+
        //                        "<ALLLEDGERENTRIES.LIST>"+
        //                            "<LEDGERNAME>"+strDLname+"</LEDGERNAME>"+
        //                            "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
        //                            "<AMOUNT>-"+strAmount+"</AMOUNT>"+
        //                        "</ALLLEDGERENTRIES.LIST>"+
        //                        "<ALLLEDGERENTRIES.LIST>"+
        //                            "<LEDGERNAME>"+strCLname+"</LEDGERNAME>"+
        //                            "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>"+
        //                            "<AMOUNT>"+strAmount+"</AMOUNT>"+
        //                        "</ALLLEDGERENTRIES.LIST>"+
        //                    "</VOUCHER>"+
        //                "</TALLYMESSAGE>"+
        //            "</DATA>"+
        //        "</BODY>"+
        //    "</ENVELOPE>";

        //        return xmlRequest;
        //        //String xml = xmlstc;
        //        //String lLedgerResponse = SendReqst(xml);
        //        //MessageBox.Show(lLedgerResponse);
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}