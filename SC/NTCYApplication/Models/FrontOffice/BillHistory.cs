using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace NTCYApplication.Models.FrontOffice
{
    public class BillHistory : FOBillHistoryInterface
    {
        private string _BillNumber;
        private string _MembershipNo;
        private string _MemberName;
        private double _TotalAmount;
        private string _Status;

        public string BillNumber
        {
            get { return _BillNumber; }
            set { _BillNumber = value; }
        }
        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo = value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        DBConnection db = new DBConnection();
        Dictionary<string, object> FrontOfficeBillHistoryDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            FrontOfficeBillHistoryDictionary = new Dictionary<string, object>();

            FrontOfficeBillHistoryDictionary.Add("BillNumber", _BillNumber);
            FrontOfficeBillHistoryDictionary.Add("MembershipNo", _MembershipNo);
            FrontOfficeBillHistoryDictionary.Add("MemberName", _MemberName);
            FrontOfficeBillHistoryDictionary.Add("TotalAmount", _TotalAmount);
            FrontOfficeBillHistoryDictionary.Add("Status", _Status);
            return "Success";
        }

        public List<BillHistory> ViewAllFrontOfficeBillHistory()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<BillHistory> list = new List<BillHistory>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
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
                            BillHistory Det = new BillHistory();
                            Det._BillNumber=Convert.ToString(ds.Tables[0].Rows[i]["BillNumber"]);
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._MemberName=Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalAmount"]);
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

       
    }
}