using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace NTCYApplication.Models.FrontOffice
{
    public class FrontOfficePayment : FrontOfficePaymentInterface
    {
        private double _GST;
        private float _Qty;
        private string _LiquorName;
        private string _MembershipType;
        private double _SubscriptionAmountPaid;
        string _MembershipNo;
        string _MemberName;
        double _TotalAmount;
        DateTime _BillDate;
        //string Date;
        string _Status;
        string _CardType;
        string _CardNo_ChequeNo;
        double _ExpDate;
        int _OrderId;
        int _ServiceType;
        double _OrderAmount;
        double _Tax;
        DateTime _FromDate;
        DateTime _ToDate;
        string _Items;
        double _Payable;
        double _AmtPaid;
        double _ToBePaid;
        string _UserName;
        string _WaiterName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName=value; }
        }
        public string WaiterName
        {
            get { return _WaiterName; }
            set { _WaiterName = value; }
        }

        public double Payable
        {
            get { return _Payable; }
            set { _Payable=value; }
        }
        public double AmtPaid
        {
            get { return _AmtPaid; }
            set { _AmtPaid=value; }
        }

        public double ToBePaid
        {
            get { return _ToBePaid; }
            set { _ToBePaid=value; }
        }
        public string Items
        {
            get { return _Items; }
            set { _Items=value; }
        }

        public double GST
        {
            get { return _GST; }
            set { _GST=value; }
        }
        public float Qty
        {
            get { return _Qty; }
            set { _Qty=value; }
        }
        public string LiquorName
        {
            get { return _LiquorName; }
            set { _LiquorName=value; }
        }
        public string MembershipType
        {
            get { return _MembershipType; }
            set { _MembershipType=value; }
        }
        public double SubscriptionAmountPaid
        {
            get { return _SubscriptionAmountPaid; }
            set { _SubscriptionAmountPaid=value; }
        }
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId=value; }
        }
        public int ServiceType
        {
            get { return _ServiceType; }
            set { _ServiceType=value; }
        }
        public double OrderAmount
        {
            get { return _OrderAmount; }
            set { _OrderAmount=value; }
        }
        public double Tax
        {
            get { return _Tax; }
            set { _Tax=value; }
        }

        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo=value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName=value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount=value; }
        }
        public DateTime BillDate
        {
            get { return _BillDate; }
            set { _BillDate=value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }


        public string CardNo_ChequeNo
        {
            get { return _CardNo_ChequeNo; }
            set { _CardNo_ChequeNo=value; }
        }

        public string CardType
        {
            get { return _CardType; }
            set { _CardType=value; }
        }
        public double ExpDate
        {
            get { return _ExpDate; }
            set { _ExpDate=value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate=value; }
        }

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate=value; }
        }

        DBConnection db = new DBConnection();
        Dictionary<string, object> FrontOfficePaymentDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            FrontOfficePaymentDictionary=new Dictionary<string, object>();
            FrontOfficePaymentDictionary.Add("MembershipNo", _MembershipNo);
            FrontOfficePaymentDictionary.Add("MemberName", _MemberName);
            FrontOfficePaymentDictionary.Add("TotalAmount", _TotalAmount);
            FrontOfficePaymentDictionary.Add("Status", _Status);
            FrontOfficePaymentDictionary.Add("Date", _BillDate);
            FrontOfficePaymentDictionary.Add("FromDate", _FromDate);
            FrontOfficePaymentDictionary.Add("ToDate", _ToDate);
            //FrontOfficePaymentDictionary.Add("GST", _GST);
            return "Success";
        }

        public List<FrontOfficePayment> ViewAllFrontOfficePayment()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FrontOfficePayment> list = new List<FrontOfficePayment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetAllFrontOfficePayments";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool EOF = !reader.Read();

                    while (!EOF)
                    {
                        FrontOfficePayment Det = new FrontOfficePayment();
                        Det._MembershipNo=reader["MembershipNo"].ToString();
                        Det._MemberName=reader["MemberName"].ToString();
                        Det._TotalAmount= double.Parse(reader["TotalOrderAmount"].ToString());
                        Det._Status=reader["Status"].ToString();
                        Det._BillDate=(DateTime)reader["BillDate"];
                        list.Add(Det);
                        EOF=!reader.Read();
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }
        
        public List<FrontOfficePayment> ViewBillHistory()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FrontOfficePayment> list = new List<FrontOfficePayment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetAllBillHistory";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._MemberName=Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalOrderAmount"]);
                            Det._Status=Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["BillDate"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public List<FrontOfficePayment> ViewTempPayments()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FrontOfficePayment> list = new List<FrontOfficePayment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAllTempPayment";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._Tax=Convert.ToDouble(ds.Tables[0].Rows[i]["Tax"]);
                            Det._ServiceType=Convert.ToInt32(ds.Tables[0].Rows[i]["ServiceType"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["_OrderId"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                            Det._Status=Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public List<FrontOfficePayment> SearchBillHistory(Dictionary<string, object> FrontOfficePaymentIDictionary)
        {
            string response = string.Empty;
            FrontOfficePaymentDictionary=FrontOfficePaymentIDictionary;
            DataSet ds = new DataSet();
            List<FrontOfficePayment> list = new List<FrontOfficePayment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSearchBillHistory";
            cmd.Parameters.AddWithValue("@MemberName", FrontOfficePaymentDictionary["MemberName"]);
            cmd.Parameters.AddWithValue("@FromBillDate", FrontOfficePaymentDictionary["FromDate"]);
            cmd.Parameters.AddWithValue("@ToBillDate", FrontOfficePaymentDictionary["ToDate"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._MemberName=Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalOrderAmount"]);
                            Det._Status=Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["BillDate"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }



        public Dictionary<string, List<FrontOfficePayment>> ViewDetailedBill(string MembershipNo, DateTime BillDate)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            Dictionary<string, List<FrontOfficePayment>> MyLists = new Dictionary<string, List<FrontOfficePayment>>();
            SqlCommand cmd = new SqlCommand();
            List<FrontOfficePayment> ListFood = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListLiquor = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListSubscriptionType = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListOtherServices = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListRoomBooking = new List<FrontOfficePayment>();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewDetailedBill";
            cmd.Parameters.AddWithValue("@MembershipNo", MembershipNo);
            cmd.Parameters.AddWithValue("@BillDate", BillDate);
            cmd.Parameters.AddWithValue("@Flag", 0);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            if (Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"])==null||Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"])=="")
                            {
                                Det._MembershipNo="---";
                            }
                            else
                            {
                                Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            }
                            if (Convert.ToString(ds.Tables[0].Rows[i]["Price"])==null||Convert.ToString(ds.Tables[0].Rows[i]["Price"])=="")
                            {
                                Det._TotalAmount=0;
                            }
                            else
                            {
                                Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["Price"]);
                            }
                            if (Convert.ToString(ds.Tables[0].Rows[i]["Date"])==null||Convert.ToString(ds.Tables[0].Rows[i]["Date"])=="")
                            {
                                Det._BillDate=Convert.ToDateTime(01/01/2000);
                            }
                            else
                            {
                                Det._BillDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                            }
                            if (Convert.ToString(ds.Tables[0].Rows[i]["GST"])==null||Convert.ToString(ds.Tables[0].Rows[i]["GST"])=="")
                            {
                                Det._GST=0;
                            }
                            else
                            {
                                Det._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"]);
                            }



                            Det._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det._Qty=Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det._Items=ds.Tables[0].Rows[i]["FoodName"].ToString();
                            Det._UserName=ds.Tables[0].Rows[i]["UserName"].ToString();
                            ListFood.Add(Det);
                        }
                    }
                    for (int i = 0; i<ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[1].Rows[i]["MembershipNo"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[1].Rows[i]["Price"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[1].Rows[i]["Date"]);
                            Det._GST=Convert.ToDouble(ds.Tables[1].Rows[i]["GST"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[1].Rows[i]["OrderId"]);
                            Det._Qty=float.Parse(ds.Tables[1].Rows[i]["Quantity"].ToString());
                            Det._LiquorName=Convert.ToString(ds.Tables[1].Rows[i]["LiquorName"]);
                            Det._UserName=ds.Tables[1].Rows[i]["UserName"].ToString();
                            Det._WaiterName = ds.Tables[1].Rows[i]["WaiterName"].ToString();
                            ListLiquor.Add(Det);
                        }
                    }
                    for (int i = 0; i<ds.Tables[2].Rows.Count; i++)
                    {
                        if (ds.Tables[2].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            if (Convert.ToString(ds.Tables[2].Rows[i]["MembershipType"].ToString())==null||Convert.ToString(ds.Tables[2].Rows[i]["MembershipType"].ToString())=="")
                            {
                                Det._MembershipType="0";
                            }
                            else
                            {
                                Det._MembershipType=Convert.ToString(ds.Tables[2].Rows[i]["MembershipType"]);
                            }
                            if (Convert.ToString(ds.Tables[2].Rows[i]["Payable"].ToString())==null||Convert.ToString(ds.Tables[2].Rows[i]["Payable"].ToString())=="")
                            {
                                Det._Payable=0;
                            }
                            else
                            {
                                Det._Payable=Convert.ToDouble(ds.Tables[2].Rows[i]["Payable"]);
                            }
                            if (Convert.ToString(ds.Tables[2].Rows[i]["AmtPaid"].ToString())==null||Convert.ToString(ds.Tables[2].Rows[i]["AmtPaid"].ToString())=="")
                            {
                                Det._AmtPaid=0;
                            }
                            else
                            {
                                Det._AmtPaid=Convert.ToDouble(ds.Tables[2].Rows[i]["AmtPaid"]);
                            }
                            if (Convert.ToString(ds.Tables[2].Rows[i]["ToBePaid"].ToString())==null||Convert.ToString(ds.Tables[2].Rows[i]["ToBePaid"].ToString())=="")
                            {
                                Det._ToBePaid=0;
                            }
                            else
                            {
                                Det._ToBePaid=Convert.ToDouble(ds.Tables[2].Rows[i]["ToBePaid"]);
                            }
                            if (Convert.ToString(ds.Tables[2].Rows[i]["Status"].ToString())==null||Convert.ToString(ds.Tables[2].Rows[i]["Status"].ToString())=="")
                            {
                                Det._Status="0";
                            }
                            else
                            {
                                Det._Status=ds.Tables[2].Rows[i]["Status"].ToString();
                            }
                            ListSubscriptionType.Add(Det);
                        }

                    }
                    for (int i = 0; i<ds.Tables[3].Rows.Count; i++)
                    {
                        if (ds.Tables[3].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            if (Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"].ToString())=="")
                            {
                                Det._MembershipNo="0";
                            }
                            else
                            {
                                Det._MembershipNo=Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"]);
                            }
                            Det._Items=Convert.ToString(ds.Tables[3].Rows[i]["ServiceName"]);

                            if (Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"].ToString())=="")
                            {
                                Det._Status="0";
                            }
                            else
                            {
                                Det._Status=Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"]);
                            }
                            if (Convert.ToString(ds.Tables[3].Rows[i]["Charges"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["Charges"].ToString())=="")
                            {
                                Det._TotalAmount=0;
                            }
                            else
                            {

                                Det._TotalAmount=Convert.ToDouble(ds.Tables[3].Rows[i]["Charges"].ToString());
                            }
                            if (Convert.ToString(ds.Tables[3].Rows[i]["OrderId"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["OrderId"].ToString())=="")
                            {
                                Det._OrderId=0;
                            }
                            else
                            {
                                Det._OrderId=Convert.ToInt32(ds.Tables[3].Rows[i]["OrderId"]);
                            }
                                ListOtherServices.Add(Det);
                        }
                    }

                    for (int i = 0; i<ds.Tables[4].Rows.Count; i++)
                    {
                        if (ds.Tables[4].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            if (Convert.ToString(ds.Tables[4].Rows[i]["MembershipNo"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["MembershipNo"].ToString())=="")
                            {
                                Det._MembershipNo="0";
                            }
                            else
                            {
                                Det._MembershipNo=Convert.ToString(ds.Tables[4].Rows[i]["MembershipNo"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["FromDate"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["FromDate"].ToString())=="")
                            {
                                Det._FromDate=DateTime.Now;
                            }
                            else
                            {
                                Det._FromDate=Convert.ToDateTime(ds.Tables[4].Rows[i]["FromDate"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["ToDate"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["ToDate"].ToString())=="")
                            {
                                Det._ToDate=DateTime.Now;
                            }
                            else
                            {
                                Det._ToDate=Convert.ToDateTime(ds.Tables[4].Rows[i]["ToDate"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["OrderId"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["OrderId"].ToString())=="")
                            {
                                Det._OrderId=0;
                            }
                            else
                            {
                                Det._OrderId=Convert.ToInt32(ds.Tables[4].Rows[i]["OrderId"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["PaidStatus"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["PaidStatus"].ToString())=="")
                            {
                                Det._Status="";
                            }
                            else
                            {
                                Det._Status=Convert.ToString(ds.Tables[4].Rows[i]["PaidStatus"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["Charges"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["Charges"].ToString())=="")
                            {
                                Det._OrderAmount=0;
                            }
                            else
                            {
                                Det._OrderAmount=Convert.ToDouble(ds.Tables[4].Rows[i]["Charges"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["FromTime"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["FromTime"].ToString())=="")
                            {
                                Det._MemberName="0";
                            }
                            else
                            {
                                Det._MemberName=Convert.ToString(ds.Tables[4].Rows[i]["FromTime"]);
                            }
                            if (Convert.ToString(ds.Tables[4].Rows[i]["ToTime"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["ToTime"].ToString())=="")
                            {
                                Det._LiquorName="0";
                            }
                            else
                            {
                                Det._LiquorName=Convert.ToString(ds.Tables[4].Rows[i]["ToTime"]);
                            }
                            if(Convert.ToString(ds.Tables[4].Rows[i]["GST"].ToString())==null||Convert.ToString(ds.Tables[4].Rows[i]["GST"].ToString())=="")
                            {
                                Det._GST=0;
                            }
                            else
                            {
                                Det._GST=int.Parse(ds.Tables[4].Rows[i]["GST"].ToString());
                            }
                            ListRoomBooking.Add(Det);
                        }
                    }
                    MyLists.Add("ListFood", ListFood);
                    MyLists.Add("ListLiquor", ListLiquor);
                    MyLists.Add("ListSubscriptionType", ListSubscriptionType);
                    MyLists.Add("ListOtherServices", ListOtherServices);
                    MyLists.Add("ListRoomBooking", ListRoomBooking);
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return MyLists;
            }
        }

        public Dictionary<string, List<FrontOfficePayment>> ViewDetailedBillHistory(string MembershipNo, DateTime BillDate, int Flag)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            Dictionary<string, List<FrontOfficePayment>> MyLists = new Dictionary<string, List<FrontOfficePayment>>();
            SqlCommand cmd = new SqlCommand();
            List<FrontOfficePayment> ListFood = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListLiquor = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListSubscriptionType = new List<FrontOfficePayment>();
            List<FrontOfficePayment> ListOtherServices = new List<FrontOfficePayment>();

            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewDetailedBill";
            cmd.Parameters.AddWithValue("@MembershipNo", MembershipNo);
            cmd.Parameters.AddWithValue("@BillDate", BillDate);
            cmd.Parameters.AddWithValue("@Flag", Flag);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["Price"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                            Det._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det._Qty=Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det._Items=ds.Tables[0].Rows[i]["FoodName"].ToString();
                            ListFood.Add(Det);
                        }
                    }
                    for (int i = 0; i<ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[1].Rows[i]["MembershipNo"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[1].Rows[i]["Price"]);
                            Det._BillDate=Convert.ToDateTime(ds.Tables[1].Rows[i]["Date"]);
                            Det._GST=Convert.ToDouble(ds.Tables[1].Rows[i]["GST"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[1].Rows[i]["OrderId"]);
                            Det._Qty=Convert.ToInt32(ds.Tables[1].Rows[i]["Quantity"]);
                            Det._LiquorName=Convert.ToString(ds.Tables[1].Rows[i]["LiquorName"]);
                            ListLiquor.Add(Det);
                        }
                    }
                    for (int i = 0; i<ds.Tables[2].Rows.Count; i++)
                    {
                        if (ds.Tables[2].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipType=Convert.ToString(ds.Tables[2].Rows[i]["MembershipType"]);
                            Det._SubscriptionAmountPaid=Convert.ToDouble(ds.Tables[2].Rows[i]["SubscriptionAmountPaid"]);
                            Det._Status=ds.Tables[2].Rows[i]["PaymentStatus"].ToString();

                            ListSubscriptionType.Add(Det);
                        }

                    }
                    for (int i = 0; i<ds.Tables[3].Rows.Count; i++)
                    {
                        if (ds.Tables[3].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            if (Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"].ToString())=="")
                            {
                                Det._MembershipNo="0";
                            }
                            else
                            {
                                Det._MembershipNo=Convert.ToString(ds.Tables[3].Rows[i]["MembershipNo"]);
                            }
                            Det._Items=Convert.ToString(ds.Tables[3].Rows[i]["ServiceName"]);
                            if (Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"].ToString())=="")
                            {
                                Det._Status="0";
                            }
                            else
                            {
                                Det._Status=Convert.ToString(ds.Tables[3].Rows[i]["PaidStatus"]);
                            }
                            if (Convert.ToString(ds.Tables[3].Rows[i]["Charges"].ToString())==null||Convert.ToString(ds.Tables[3].Rows[i]["Charges"].ToString())=="")
                            {
                                Det._TotalAmount=0;
                            }
                            else
                            {

                                Det._TotalAmount=Convert.ToDouble(ds.Tables[3].Rows[i]["Charges"].ToString());
                            }
                            ListOtherServices.Add(Det);
                        }
                    }
                    MyLists.Add("ListFood", ListFood);
                    MyLists.Add("ListLiquor", ListLiquor);
                    MyLists.Add("ListSubscriptionType", ListSubscriptionType);
                    MyLists.Add("ListOtherServices", ListOtherServices);
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return MyLists;
            }
        }

        public List<FrontOfficePayment> Attendance()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FrontOfficePayment> list = new List<FrontOfficePayment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAttendanceDetails";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            FrontOfficePayment Det = new FrontOfficePayment();
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._MemberName=Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det._Qty=Convert.ToInt32(ds.Tables[0].Rows[i]["ServiceCode"]);
                            Det._Status=Convert.ToString(ds.Tables[0].Rows[i]["TimeSpent"]);
                            //  Det._BillDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["BillDate"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }

        public string DeleteServicesCollection(int ServCollectionId)
        {
            string count;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteServicesCollection";
            cmd.Parameters.AddWithValue("@ServiceCollectionId", ServCollectionId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    count = cmd.ExecuteNonQuery().ToString();
                }
                catch (SqlException e)
                {
                    count = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return count;
            }
        }
    }


}