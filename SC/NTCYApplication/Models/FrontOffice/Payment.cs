using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NTCYApplication.Dictionaries;

namespace NTCYApplication.Models.FrontOffice
{
    public class Payment : PaymentInterface
    {
        int _OrderId;
        string _MembershipNo;
        DateTime _BillDate;
        string _Status;
        string _ModeOfPayment;
        string _CardType;
        string _CardNo_ChequeNo;
        string _ExpDate;
        string _BankName;
        int _Flag;
        double _Amount;
        public double Amount
        {
            get { return _Amount; }
            set { _Amount=value; }
        }
        public int Flag
        {
            get { return _Flag; }
            set { _Flag=value; }
        }
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId=value; }
        }
        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo=value; }
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
        public string ModeOfPayment
        {
            get { return _ModeOfPayment; }
            set { _ModeOfPayment=value; }
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
        public string ExpDate
        {
            get { return _ExpDate; }
            set { _ExpDate=value; }
        }

        public string BankName
        {
            get { return _BankName; }
            set { _BankName=value; }
        }



        Dictionary<string, object> PaymentDictionary = new Dictionary<string, object>();
        DBConnection db = new DBConnection();

        string BindDictionary()
        {
            PaymentDictionary.Add("Amount", _Amount);
            PaymentDictionary.Add("MembershipNo", _MembershipNo);
            PaymentDictionary.Add("BillDate", _BillDate);
            PaymentDictionary.Add("Status", _Status);
            PaymentDictionary.Add("OrderId", _OrderId);
            PaymentDictionary.Add("ModeOfPayment", _ModeOfPayment);
            PaymentDictionary.Add("CardType", _CardType);
            PaymentDictionary.Add("CardNo_ChequeNo", _CardNo_ChequeNo);
            PaymentDictionary.Add("ExpDate", _ExpDate);
            PaymentDictionary.Add("BankName", _BankName);

            return "Success";
        }
        public string InsertPaymentDetails(Dictionary<string, object> PaymentIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            PaymentDictionary=PaymentIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spPayConsolidatedBill";
            cmd.Parameters.AddWithValue("@MembershipNo", PaymentDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@BillDate", Convert.ToDateTime(PaymentDictionary["BillDate"]));
            cmd.Parameters.AddWithValue("@BankName", PaymentDictionary["BankName"]);
            cmd.Parameters.AddWithValue("@ModeofPayment", PaymentDictionary["ModeOfPayment"]);
            cmd.Parameters.AddWithValue("@CardType", PaymentDictionary["CardType"]);
            cmd.Parameters.AddWithValue("@CardNo_ChequeNo", PaymentDictionary["CardNo_ChequeNo"]);
            cmd.Parameters.AddWithValue("@ExpDate", PaymentDictionary["ExpDate"]);
            cmd.Parameters.AddWithValue("@Amount", PaymentDictionary["Amount"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    response=cmd.ExecuteNonQuery().ToString();

                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public string InsertIndividualPaymentDetails(Dictionary<string, object> PaymentIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            PaymentDictionary=PaymentIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            if (PaymentDictionary["ServiceType"].ToString()!="5")
            {
                cmd.CommandText="spInsertIndividualPayment";
                //cmd.CommandText="spPayConsolidatedBill"; 
                cmd.Parameters.AddWithValue("@MembershipNo", PaymentDictionary["MembershipNo"]);
                cmd.Parameters.AddWithValue("@BillDate", Convert.ToDateTime(PaymentDictionary["BillDate"]));
                cmd.Parameters.AddWithValue("@BankName", PaymentDictionary["BankName"]);
                cmd.Parameters.AddWithValue("@ModeofPayment", PaymentDictionary["ModeOfPayment"]);
                cmd.Parameters.AddWithValue("@CardType", PaymentDictionary["CardType"]);
                cmd.Parameters.AddWithValue("@CardNo_ChequeNo", PaymentDictionary["CardNo_ChequeNo"]);
                cmd.Parameters.AddWithValue("@ExpDate", PaymentDictionary["ExpDate"].ToString());
                cmd.Parameters.AddWithValue("@Amount", PaymentDictionary["Amount"]);
                cmd.Parameters.AddWithValue("@ServiceType", PaymentDictionary["ServiceType"]);
            }
            else if(PaymentDictionary["ServiceType"].ToString()=="5")
            {
                //cmd.CommandText="spInsertIndividualPayment";
                cmd.CommandText="spPayConsolidatedBill"; 
                cmd.Parameters.AddWithValue("@MembershipNo", PaymentDictionary["MembershipNo"]);
                cmd.Parameters.AddWithValue("@BillDate", Convert.ToDateTime(PaymentDictionary["BillDate"]));
                cmd.Parameters.AddWithValue("@BankName", PaymentDictionary["BankName"]);
                cmd.Parameters.AddWithValue("@ModeofPayment", PaymentDictionary["ModeOfPayment"]);
                cmd.Parameters.AddWithValue("@CardType", PaymentDictionary["CardType"]);
                cmd.Parameters.AddWithValue("@CardNo_ChequeNo", PaymentDictionary["CardNo_ChequeNo"]);
                cmd.Parameters.AddWithValue("@ExpDate", PaymentDictionary["ExpDate"]);
                cmd.Parameters.AddWithValue("@Amount", PaymentDictionary["Amount"]);
                //cmd.Parameters.AddWithValue("@ServiceType", PaymentDictionary["ServiceType"]);
            }
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    response=cmd.ExecuteNonQuery().ToString();

                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public int DeleteBill(string membershipNo, DateTime billDate,string billType)
        {
            int res = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            //cmd.CommandText="sp_DeleteBill";
            cmd.CommandText = "sp_DeleteLiquorBill";
            cmd.Parameters.AddWithValue("@MembershipNo", membershipNo);
            cmd.Parameters.AddWithValue("@Date", billDate);
            //Commented by MurugesanG On 14-Feb-2023
            //cmd.Parameters.AddWithValue("@Billtype", billType);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    res=cmd.ExecuteNonQuery();

                }
                catch (SqlException e)
                {
                  
                }
                finally
                {
                    db.CloseConnection();
                }
                return res;
            }
          
        }
    }
}