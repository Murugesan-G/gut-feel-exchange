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
    public class PaymentSubTable: PaymentSubTableInterface
    {
        private int _SubPaymentId;
        private int _PaymentId;
        private string _CardType;
        private string _CardNo_ChequeNo;
        private double _ExpDate;

        public int SubPaymentId
        {
            get { return _SubPaymentId; }
            set { _SubPaymentId = value; }
        }
        public int PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }
        public string CardNo_ChequeNo
        {
            get { return _CardNo_ChequeNo; }
            set { _CardNo_ChequeNo = value; }
        }

        public string CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        public double ExpDate
        {
            get { return _ExpDate; }
            set { _ExpDate = value; }
        }

        public Dictionary<string, object> PaymentSubTableDictionary { get; set; }

        DBConnection db = new DBConnection();
      //  public Dictionary<string, object> PaymentSubTableDictionary = new Dictionary<string, object>();

        public string BindDictionary()
        {
            PaymentSubTableDictionary = new Dictionary<string, object>();
            PaymentSubTableDictionary.Add("SubPaymentId", _SubPaymentId);
            PaymentSubTableDictionary.Add("PaymentId", _PaymentId);
            PaymentSubTableDictionary.Add("CardType", _CardType);
            PaymentSubTableDictionary.Add("CardNo_ChequeNo", _CardNo_ChequeNo);
            PaymentSubTableDictionary.Add("ExpDate", _ExpDate);
    
            return "Success";
        }


        public int InsertPaymentsubTable(Dictionary<string, object> PaymentSubTableDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            //  FoodOrderListDictionary = FoodOrderListIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertAllPaymentSubTable";

            cmd.Parameters.AddWithValue("@SubPaymentId", PaymentSubTableDictionary["SubPaymentId"]);
            cmd.Parameters.AddWithValue("@PaymentId", PaymentSubTableDictionary["PaymentId"]);
            cmd.Parameters.AddWithValue("@CardType", PaymentSubTableDictionary["CardType"]);
            cmd.Parameters.AddWithValue("@CardNo_ChequeNo", PaymentSubTableDictionary["CardNo_ChequeNo"]);
            cmd.Parameters.AddWithValue("@ExpDate", PaymentSubTableDictionary["ExpDate"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlParameter Param = new SqlParameter("@SubPaymentId", 0);
                    Param.Direction=ParameterDirection.Output;
                    cmd.Parameters.Add(Param);
                    response=cmd.ExecuteNonQuery();
                    int SubOrderId = Convert.ToInt32(Param.Value);
                    response=SubPaymentId;
                }
                catch (SqlException e)
                {
                    response=SubPaymentId;
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }


        public List<PaymentSubTable> ViewAllPaymentSubTable()
        {
            throw new NotImplementedException();
        }
       
    }
}