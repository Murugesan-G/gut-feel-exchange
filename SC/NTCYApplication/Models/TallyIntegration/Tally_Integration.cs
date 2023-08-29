using NTCYApplication.Models.Club;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace NTCYApplication.Models.TallyIntegration
{
    public class Tally_Integration
    {
        ClubAccounts CA = new ClubAccounts();
        string Url = "";
        string Port = "";
        string Username = "";
        string Password = "";
        string BarDues = "";
        string MonthlySubscription = "";
        string Ledger3 = "";
        string Ledger4 = "";
        public Tally_Integration()
        {

        }


        public string TallyConfig()
        {
            Dictionary<string, object> TallyIntegrationDictionary = new Dictionary<string, object>();
            TallyIntegrationDictionary=CA.ViewDetails();
            Url=TallyIntegrationDictionary["Url"].ToString();
            Port=TallyIntegrationDictionary["Port"].ToString();
            Username=TallyIntegrationDictionary["Username"].ToString();
            Password=TallyIntegrationDictionary["Password"].ToString();
            BarDues=TallyIntegrationDictionary["Ledger1"].ToString();
            MonthlySubscription=TallyIntegrationDictionary["Ledger2"].ToString();
            Ledger3=TallyIntegrationDictionary["Ledger3"].ToString();
            Ledger4=TallyIntegrationDictionary["Ledger4"].ToString();
            //string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
            //             "<root>"+

            //              "<TallyBaseURL>"+Url+"</TallyBaseURL>"+
            //              "<Port>"+Port+"</Port>"+
            //              "<CompanyName>Spigot</CompanyName>"+
            //              "<!--SVCURRENTCOMPANY-->"+
            //              "<CrLedgerName>Sales Bill Account</CrLedgerName>"+
            //              "<DbLedgerName>Sales Patient Account</DbLedgerName>"+

            //              "<BarDuesLedgerName>"+BarDues+"</BarDuesLedgerName>"+
            //              "<MonthlySubscriptionLedgerName>"+MonthlySubscription+"</MonthlySubscriptionLedgerName>"+

            //              "<CashLedgerName>CashLedger</CashLedgerName>"+
            //              "<BankAccountLedgerName>BankAccountLedger</BankAccountLedgerName>"+

            //              "<VoucherType>Receipt</VoucherType>"+
            //              "<ReceiptVoucherType>Receipt</ReceiptVoucherType>"+
            //              "<!--Aplicable Values BILLWISE, CONSOLIDATED-->"+
            //               "<ExportPostingType>"+
            //                 "<Values>BILLWISE</Values>"+
            //               "</ExportPostingType>"+
            //               "<!--Aplicable Values Day, Week, Month-->"+
            //                 "<ExportDateType>"+
            //                   "<Value>DAY</Value>"+
            //                   "<Value>MONTH</Value>"+
            //                 "</ExportDateType>"+
            //               "</root>";
            return Url+":"+Port;
        }


        private string ProcessResponse(string xmlResponse)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlResponse);
            XmlNode node = doc.SelectSingleNode("ENVELOPE//HEADER//STATUS");
            if (node.InnerText.Trim()=="1")
            {
                XmlNode voucherNode = doc.SelectSingleNode("ENVELOPE//BODY//DATA//IMPORTRESULT//LASTVCHID");
                if (voucherNode.InnerText.Trim()!="0")
                {
                    int voucherNumber = Convert.ToInt32(voucherNode.InnerText.Trim());
                    return voucherNumber.ToString();
                }
            }
            return "-1";
        }

        private string CreateVoucher(string billNo, string memNo, string date,
            string voucherType, string narrationText, string amount, string ledgerType, string ledgerName, string strPayMode)
        {
            WebRequest req;
            WebResponse rsp;
            TallyConfig();
            try
            {
                string serviceURL = Url+":"+Port;
                req=WebRequest.Create(serviceURL);

                req.Method="POST";
                req.ContentType="text/xml";
                req.Credentials=CredentialCache.DefaultNetworkCredentials;
                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                string xmlRequest = GetXMLRequestForVoucherCreation(date, narrationText,
                    voucherType,billNo, memNo, amount, ledgerType, ledgerName, strPayMode);

                writer.WriteLine(xmlRequest);
                writer.Close();

                rsp=req.GetResponse();
                StreamReader sr = new StreamReader(rsp.GetResponseStream());
                string result = sr.ReadToEnd();
                sr.Close();

                string res = ProcessResponse(result);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ModifyVoucher(string billNo, string memNo, string date,
            string voucherType, string narrationText, string amount, string ledgerType, string ledgerName, string strPayMode)
        {
            WebRequest req;
            WebResponse rsp;

            try
            {
                string serviceURL = Url+":"+Port;
                req=WebRequest.Create(serviceURL);
                req.Method="POST";
                req.ContentType="text/xml";
                req.Credentials=CredentialCache.DefaultNetworkCredentials;
                StreamWriter writer = new StreamWriter(req.GetRequestStream());
                string xmlRequest = GetXMLRequestForVoucherModification(date, narrationText, voucherType,
                     billNo, memNo, amount, ledgerType, ledgerName, strPayMode);

                writer.WriteLine(xmlRequest);
                writer.Close();

                rsp=req.GetResponse();
                StreamReader sr = new StreamReader(rsp.GetResponseStream());
                string result = sr.ReadToEnd();
                sr.Close();

                string res = ProcessResponse(result);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GetXMLRequestForVoucherCreation(string date, string narration, string voucherTypename,
            string voucherNumber,string memNo, string amount, string ledgerType, string ledgerName, string strPayMode)
        {
            string xmlHeader = GetXMLHeader();
            string xmlDesc = GetXMLDescription();
            string xmlData = GetXMLData("CREATE", date, narration, voucherTypename,
            voucherNumber,memNo, amount, ledgerType, ledgerName, strPayMode);

            string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
                "<ENVELOPE>"+
                    xmlHeader+
                    "<BODY>"+
                        xmlDesc+
                        xmlData+
                    "</BODY>"+
                "</ENVELOPE>";

            return xmlRequest;
        }
        public string RequestForVoucherCreation(string strDate, string strNartxt, string strVTypeName, string strVNumber, string strDLname, string strCLname, string strAmount)
        {
            string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
                "<ENVELOPE>"+
                    "<HEADER>"+
                        "<VERSION>1</VERSION>"+
                        "<TALLYREQUEST>Import</TALLYREQUEST>"+
                        "<TYPE>Data</TYPE>"+
                        "<ID>Vouchers</ID>"+
                    "</HEADER>"+
                    "<BODY>"+
                        "<DESC>"+
                        "</DESC>"+
                        "<DATA>"+
                            "<TALLYMESSAGE>"+
                                "<VOUCHER>"+
                                    "<DATE>"+strDate+"</DATE>"+
                                    "<NARRATION>"+strNartxt+"</NARRATION>"+
                                    "<VOUCHERTYPENAME>"+strVTypeName+"</VOUCHERTYPENAME>"+
                                    "<VOUCHERNUMBER>"+strVNumber+"</VOUCHERNUMBER>"+
                                    "<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+strDLname+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>-"+strAmount+"</AMOUNT>"+
                                    "</ALLLEDGERENTRIES.LIST>"+
                                    "<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+strCLname+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>"+strAmount+"</AMOUNT>"+
                                    "</ALLLEDGERENTRIES.LIST>"+
                                "</VOUCHER>"+
                            "</TALLYMESSAGE>"+
                        "</DATA>"+
                    "</BODY>"+
                "</ENVELOPE>";

            return xmlRequest;
        }

        private string GetXMLRequestForVoucherModification(string date, string narration, string voucherTypename,
                    string voucherNumber,string memNo, string amount, string ledgerType, string ledgerName, string strPayMode)
        {
            string xmlHeader = GetXMLHeader(); 
            string xmlDesc = GetXMLDescription();
            string xmlData = GetXMLData("UPDATE", date, narration, voucherTypename,
            voucherNumber,memNo, amount, ledgerType, ledgerName, strPayMode);


            string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
                "<ENVELOPE>"+
                    xmlHeader+
                    "<BODY>"+
                        xmlDesc+
                        xmlData+
                    "</BODY>"+
                "</ENVELOPE>";

            return xmlRequest;
        }

        private string GetXMLHeader()
        {
            string xmlHeader = "<HEADER>"+
                        "<VERSION>1</VERSION>"+
                        "<TALLYREQUEST>Import</TALLYREQUEST>"+
                        "<TYPE>Data</TYPE>"+
                        "<ID>Vouchers</ID>"+
                    "</HEADER>";

            return xmlHeader;
        }

        private string GetXMLDescription()
        {
            string companyName = "NTCY";
            string xmlDesc = "<DESC>"+
                            "<STATICVARIABLES>"+
                                "<SVCURRENTCOMPANY>"+
                                    companyName+
                                "</SVCURRENTCOMPANY>"+
                            "</STATICVARIABLES>"+
                        "</DESC>";

            return xmlDesc;
        }

        private string GetXMLData(string command, string date, string narration, string voucherTypename,
            string voucherNumber,string memNo, string amount, string ledgerType, string ledgerName, string strPayMode)
        {
            string CashAmountData = "";
            string BankAccountChargeData = "";
            if (strPayMode=="Cash")
            {
                CashAmountData="<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+ledgerName+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>-"+amount+"</AMOUNT>"+
                                    "</ALLLEDGERENTRIES.LIST>";
            }
            else
            {
                BankAccountChargeData="<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+ledgerName+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>-"+amount+"</AMOUNT>"+
                                    "</ALLLEDGERENTRIES.LIST>";
            }

            string xmlData = "";

            if (command=="CREATE")
            {
                xmlData="<DATA>"+
                            "<TALLYMESSAGE>"+
                                "<VOUCHER>"+
                                    "<DATE>"+date+"</DATE>"+
                                    "<NARRATION>"+narration+"</NARRATION>"+
                                    "<VOUCHERTYPENAME>"+voucherTypename+"</VOUCHERTYPENAME>"+
                                    "<VOUCHERNUMBER>"+voucherNumber+"</VOUCHERNUMBER>"+
                                    "<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+ledgerName+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>"+amount+"</AMOUNT>"+
                                    "</ALLLEDGERENTRIES.LIST>"+
                                    "<ALLLEDGERENTRIES.LIST>"+
                                    "<LEDGERNAME>"+ledgerName+"</LEDGERNAME>"+
                                    "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
                                    "<AMOUNT>-"+amount+"</AMOUNT>"+
                                "</ALLLEDGERENTRIES.LIST>"+
                                 CashAmountData+BankAccountChargeData+
                                "</VOUCHER>"+
                            "</TALLYMESSAGE>"+
                        "</DATA>";
            }
            else if (command=="UPDATE")
            {
                xmlData="<DATA>"+
                            "<TALLYMESSAGE>"+
                                @"<VOUCHER DATE = '"+date+"' TAGNAME = 'VOUCHERID' TAGVALUE='"+voucherNumber+
                                "' Action='Alter' VCHTYPE = '"+voucherTypename+"'>"+
                                    "<DATE>"+date+"</DATE>"+
                                    "<NARRATION>"+narration+"</NARRATION>"+
                                    "<VOUCHERTYPENAME>"+voucherTypename+"</VOUCHERTYPENAME>"+
                                    "<PARTICULARS>"+memNo+"</PARTICULARS>"+
                                    "<VOUCHERID>"+voucherNumber+"</VOUCHERID>"+
                                    "<ALLLEDGERENTRIES.LIST>"+
                                        "<LEDGERNAME>"+ledgerName+"</LEDGERNAME>"+
                                        "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
                                        "<AMOUNT>-"+amount+"</AMOUNT>"+
                                          CashAmountData+BankAccountChargeData+
                                    "</ALLLEDGERENTRIES.LIST>"+
                                "</VOUCHER>"+
                            "</TALLYMESSAGE>"+
                        "</DATA>";
            }
            else
            {
            }

            return xmlData;
        }

        public string PostDataToTally(string command, string billNo, string memNo, string date, string amount,
            string voucherType, string narrationText, string ledgerType, string ledgerName, string strPayMode)
        {
            try
            {
                string result = "";
                if (command.ToUpper().Trim()=="CREATE")
                {
                    result=CreateVoucher(billNo, memNo, date, voucherType, narrationText, amount, ledgerType, ledgerName, strPayMode);
                }
                else if (command.ToUpper().Trim()=="UPDATE")
                {
                    result=ModifyVoucher(billNo, memNo, date, voucherType, narrationText, amount, ledgerType, ledgerName, strPayMode);
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public string PostDataToTally(string strCmd, string strBillNo, string strMemNo, string strDate, string strAmount, string strVoucherType, string strNartxt, string strDLname, string strCLname, string strPayMode)
        //{
        //    string result = "";
        //    if (strCmd.ToUpper().Trim()=="CREATE")
        //    {
        //        result=CreateVoucher(strBillNo, strMemNo, strDate, strAmount, strVoucherType, strNartxt, strCLname,);
        //    } 
        //    else if (strCmd.ToUpper().Trim()=="UPDATE")
        //    {
        //        result=ModifyVoucher(strBillNo, strMemNo, strDate, strAmount, strVoucherType, strNartxt, strCLname);
        //    }
        //    return result;
        //public string XMLProcessResponse(string xmlres)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xmlres);
        //    XmlNode node = doc.SelectSingleNode("ENVELOPE//HEADER//STATUS");
        //    if (node.InnerText.Trim()=="1")
        //    {
        //        return "Success";
        //    }
        //    return "Failed";
        //}

        //public string CreateVoucher(string strBillno, string strMemNo, string strDate, string strAmount, string strVTypeName, string strNartxt,  string strDLname, string strCLname)
        //{
        //    WebRequest Wreq;
        //    WebResponse Wrsp;

        //    try
        //    {
        //        // WebRequest.Create("");
        //        //string serviceURL = TallyServiceURL();
        //        Wreq=WebRequest.Create("http://localhost:9000");

        //        Wreq.Method="POST";
        //        Wreq.ContentType="text/xml";
        //        Wreq.Credentials=CredentialCache.DefaultNetworkCredentials;
        //        StreamWriter writer = new StreamWriter(Wreq.GetRequestStream());
        //        string xmlRequest = RequestForVoucherCreation(strDate, strNartxt, strVTypeName, strMemNo, strDLname, strCLname, strAmount);

        //        writer.WriteLine(xmlRequest);
        //        writer.Close();

        //        Wrsp=Wreq.GetResponse();
        //        StreamReader sr = new StreamReader(Wrsp.GetResponseStream());
        //        string result = sr.ReadToEnd();
        //        sr.Close();

        //        string res = XMLProcessResponse(result);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string RequestForVoucherCreation(string strDate, string strNartxt, string strVTypeName, string strVNumber, string strDLname, string strCLname, string strAmount)
        //{
        //    string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
        //        "<ENVELOPE>"+
        //            "<HEADER>"+
        //                "<VERSION>1</VERSION>"+
        //                "<TALLYREQUEST>Import</TALLYREQUEST>"+
        //                "<TYPE>Data</TYPE>"+
        //                "<ID>Vouchers</ID>"+
        //            "</HEADER>"+
        //            "<BODY>"+
        //                "<DESC>"+
        //                "<SVCURRENTCOMPANY>"+"NCTY" +"</SVCURRENTCOMPANY>"+
        //                "</DESC>"+
        //                "<DATA>"+
        //                    "<TALLYMESSAGE>"+
        //                        "<VOUCHER>"+
        //                            "<DATE>"+strDate+"</DATE>"+
        //                            "<NARRATION>"+strNartxt+"</NARRATION>"+
        //                            "<VOUCHERTYPENAME>"+strVTypeName+"</VOUCHERTYPENAME>"+
        //                            "<VOUCHERNUMBER>"+strVNumber+"</VOUCHERNUMBER>"+
        //                            "<ALLLEDGERENTRIES.LIST>"+
        //                                "<LEDGERNAME>"+strDLname+"</LEDGERNAME>"+
        //                                "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
        //                                "<AMOUNT>-"+strAmount+"</AMOUNT>"+
        //                            "</ALLLEDGERENTRIES.LIST>"+
        //                            "<ALLLEDGERENTRIES.LIST>"+
        //                                "<LEDGERNAME>"+strCLname+"</LEDGERNAME>"+
        //                                "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>"+
        //                                "<AMOUNT>"+strAmount+"</AMOUNT>"+
        //                            "</ALLLEDGERENTRIES.LIST>"+
        //                        "</VOUCHER>"+
        //                    "</TALLYMESSAGE>"+
        //                "</DATA>"+
        //            "</BODY>"+
        //        "</ENVELOPE>";

        //    return xmlRequest;
        //}

        //public string ModifyVoucher(string strBillno, string strMemNo, string strDate, string strVTypeName, string strNartxt, string strAmount, string strDLname, string strCLname)
        //{
        //    WebRequest Wreq;
        //    WebResponse Wrsp;

        //    try
        //    {
        //        // WebRequest.Create("");
        //        //string serviceURL = TallyServiceURL();
        //        Wreq=WebRequest.Create("http://localhost:9000");

        //        Wreq.Method="POST";
        //        Wreq.ContentType="text/xml"; 
        //        Wreq.Credentials=CredentialCache.DefaultNetworkCredentials;
        //        StreamWriter writer = new StreamWriter(Wreq.GetRequestStream());
        //        string xmlRequest = RequestForVoucherModification(strDate, strNartxt, strVTypeName, strBillno, strMemNo, strDLname, strCLname, strAmount);

        //        writer.WriteLine(xmlRequest);
        //        writer.Close();

        //        Wrsp=Wreq.GetResponse();
        //        StreamReader sr = new StreamReader(Wrsp.GetResponseStream());
        //        string result = sr.ReadToEnd();
        //        sr.Close();

        //        string res = XMLProcessResponse(result);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string RequestForVoucherModification(string strDate, string strNartxt, string strVTypeName, string strVNumber,string MemNo, string strDLname, string strCLname, string strAmount)
        //{
        //    string xmlRequest = @"<?xml version=""1.0"" encoding=""utf-8"" ?>"+
        //        "<ENVELOPE>"+
        //            "<HEADER>"+
        //                "<VERSION>1</VERSION>"+
        //                "<TALLYREQUEST>Import</TALLYREQUEST>"+
        //                "<TYPE>Data</TYPE>"+
        //                "<ID>Vouchers</ID>"+
        //            "</HEADER>"+
        //            "<BODY>"+
        //                "<DESC>"+
        //                "</DESC>"+
        //                "<DATA>"+
        //                    "<TALLYMESSAGE>"+
        //                        @"<VOUCHER DATE = '"+strDate+"' TAGNAME = 'Voucher Number' TAGVALUE='"+strVNumber+
        //                        "' Action='Alter' VCHTYPE = '"+strVTypeName+"'>"+
        //                            "<DATE>"+strDate+"</DATE>"+
        //                            "<NARRATION>"+strNartxt+"</NARRATION>"+
        //                            "<VOUCHERTYPENAME>"+strVTypeName+"</VOUCHERTYPENAME>"+
        //                            "<VOUCHERNUMBER>"+strVNumber+"</VOUCHERNUMBER>"+
        //                            "<ALLLEDGERENTRIES.LIST>"+
        //                                "<LEDGERNAME>"+strDLname+"</LEDGERNAME>"+
        //                                "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>"+
        //                                "<AMOUNT>-"+strAmount+"</AMOUNT>"+
        //                            "</ALLLEDGERENTRIES.LIST>"+
        //                            "<ALLLEDGERENTRIES.LIST>"+
        //                                "<LEDGERNAME>"+strCLname+"</LEDGERNAME>"+
        //                                "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>"+
        //                                "<AMOUNT>"+strAmount+"</AMOUNT>"+
        //                            "</ALLLEDGERENTRIES.LIST>"+
        //                        "</VOUCHER>"+
        //                    "</TALLYMESSAGE>"+
        //                "</DATA>"+
        //            "</BODY>"+
        //        "</ENVELOPE>";

        //    return xmlRequest;
        //}

        //public string PostDataToTally(string strCmd, string strBillNo, string strMemNo, string strDate, string strAmount,string strVoucherType, string strNartxt,  string strDLname, string strCLname,string strPayMode)
        //{
        //    string result = "";
        //    if (strCmd.ToUpper().Trim()=="CREATE")
        //    {
        //        result=CreateVoucher(strBillNo, strMemNo, strDate, strAmount, strVoucherType, strNartxt, strCLname,);
        //    } 
        //    else if (strCmd.ToUpper().Trim()=="UPDATE")
        //    {
        //        result=ModifyVoucher(strBillNo, strMemNo, strDate, strAmount, strVoucherType, strNartxt, strCLname);
        //    }
        //    return result;
        //}

    }
}