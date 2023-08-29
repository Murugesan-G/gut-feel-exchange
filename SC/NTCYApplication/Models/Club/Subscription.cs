using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace NTCYApplication.Models.Club
{
    public class Subscription : ISubscription
    {
        private int _SubscriptionId; 
        private string _SubscriptionType;
        private double _SubscriptionRate;
        private DateTime _SubscriptionValidity;
        private double _JoiningAmount;
        private int _PaymentInstallments;
        private string _Status;
        private string _Response;
        private int _SubPayId;
        DateTime _BillDate;
        string _MemberNo;
        double _SubAmt;

        public int SubPayId
        {
            get { return _SubPayId; }
            set { _SubPayId=value; }
        }
        public DateTime BillDate
        {
            get { return _BillDate; }
            set { _BillDate=value; }
        }
        public string MemberNo
        {
            get { return _MemberNo; }
            set { _MemberNo=value; }
        }
        public double SubAmt
        {
            get { return _SubAmt; }
            set { _SubAmt=value; }
        }


        public int SubscriptionId
        {
            get { return _SubscriptionId; }
            set { _SubscriptionId = value; }
        }
        public string SubscriptionType
        {
            get { return _SubscriptionType; }
            set { _SubscriptionType = value; }
        }
        public double SubscriptionRate
        {
            get { return _SubscriptionRate; }
            set { _SubscriptionRate = value; }
        }
        public DateTime SubscriptionValidity
        {
            get { return _SubscriptionValidity; }
            set { _SubscriptionValidity = value; }
        }
        public int PaymentInstallments
        {
            get { return _PaymentInstallments; }
            set { _PaymentInstallments = value; }
        }
        public double JoiningAmount
        {
            get { return _JoiningAmount; }
            set { _JoiningAmount = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Response
        {
            get { return _Response; }
            set { _Response = value; }
        }


        Dictionary<string, object> SubscriptionDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            SubscriptionDictionary.Add("SubscriptionId", _SubscriptionId);
            SubscriptionDictionary.Add("SubscriptionType", _SubscriptionType);
            SubscriptionDictionary.Add("SubscriptionRate", _SubscriptionRate);
            SubscriptionDictionary.Add("SubscriptionValidity", _SubscriptionValidity);
            SubscriptionDictionary.Add("PaymentInstallments", _PaymentInstallments);
            SubscriptionDictionary.Add("Status", _Status);
            SubscriptionDictionary.Add("JoiningAmount", _JoiningAmount);
            //SubscriptionDictionary.Add("Response", _Response);

            return "Success";
        }

        DBConnection db = new DBConnection();


        public int Save()
        {
            int resultval = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            if (this.SubscriptionId == 0)
            {
                cmd.CommandText = "spInsertSubscription";
            }
            else
            {
                cmd.CommandText = "spUpdateSubscription";
                cmd.Parameters.AddWithValue("@SubscriptionId", this.SubscriptionId);
            }

            cmd.Parameters.AddWithValue("@SubscriptionType", this.SubscriptionType);
            cmd.Parameters.AddWithValue("@SubscriptionRate", this.SubscriptionRate);
            cmd.Parameters.AddWithValue("@SubscriptionValidity", this.SubscriptionValidity);
            cmd.Parameters.AddWithValue("@PaymentInstallments", this.PaymentInstallments);
            cmd.Parameters.AddWithValue("@Status", this.Status);
            cmd.Parameters.AddWithValue("@JoiningAmount", this.JoiningAmount);
            //cmd.Parameters.AddWithValue("@Response", this.Response);         
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    resultval = cmd.ExecuteNonQuery();
                }
                finally
                {
                    db.CloseConnection();
                }

            }


            return resultval;
        }


        public string CreateSubscription(Dictionary<string, object> SubcriptionDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SubscriptionDictionary=SubcriptionDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertSubscription";
            cmd.Parameters.AddWithValue("@SubscriptionType", SubscriptionDictionary["SubscriptionType"]);
            cmd.Parameters.AddWithValue("@SubscriptionRate", SubscriptionDictionary["SubscriptionRate"]);
            DateTime subscriptionValidity = DateTime.ParseExact(SubscriptionDictionary["SubscriptionValidity"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@SubscriptionValidity", subscriptionValidity);
            cmd.Parameters.AddWithValue("@PaymentInstallments", SubscriptionDictionary["PaymentInstallments"]);
            cmd.Parameters.AddWithValue("@JoiningAmount", SubscriptionDictionary["JoiningAmount"]);
            cmd.Parameters.AddWithValue("@Status", SubscriptionDictionary["Status"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    //Check for errors using try catch 
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

        public string UpdateSubscription(Dictionary<string, object> SubcriptionDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            SubscriptionDictionary=SubcriptionDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateSubscription";
            cmd.Parameters.AddWithValue("@SubscriptionId", SubscriptionDictionary["SubscriptionId"]);
            cmd.Parameters.AddWithValue("@SubscriptionType", SubscriptionDictionary["SubscriptionType"]);
            cmd.Parameters.AddWithValue("@SubscriptionRate", SubscriptionDictionary["SubscriptionRate"]);
           // DateTime subscriptionValidity = DateTime.ParseExact(SubscriptionDictionary["SubscriptionValidity"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@SubscriptionValidity",Convert.ToDateTime(SubscriptionDictionary["SubscriptionValidity"]));
            cmd.Parameters.AddWithValue("@PaymentInstallments", SubscriptionDictionary["PaymentInstallments"]);
            cmd.Parameters.AddWithValue("@JoiningAmount", SubscriptionDictionary["JoiningAmount"]);
            cmd.Parameters.AddWithValue("@Status", SubscriptionDictionary["Status"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    //Check for errors using try catch 
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

        public string DeleteSubscription(int? SubscriptionId)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteSubscription";
            cmd.Parameters.AddWithValue("@SubscriptionId", SubscriptionId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    //Check for errors using try catch 
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

        public List<Subscription> ViewAllSubscriptions()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Subscription> list = new List<Subscription>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewAllSubscription";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool EOF = !reader.Read();

                    while (!EOF)
                    {
                        Subscription Det = new Subscription();
                        Det._SubscriptionId = (int)reader["SubscriptionId"];
                        Det._SubscriptionRate = double.Parse(reader["SubscriptionRate"].ToString());
                        Det._SubscriptionType = reader["SubscriptionType"].ToString();
                        Det._SubscriptionValidity = (DateTime)reader["SubscriptionValidity"];
                        Det._PaymentInstallments = (int)reader["PaymentInstallments"];
                        Det._JoiningAmount = double.Parse(reader["JoiningAmount"].ToString());
                        Det._Status = reader["Status"].ToString();                       
                        list.Add(Det);
                        EOF = !reader.Read();
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return list;
            }
        }
        //public List<Subscription> ViewAllSubscriptions()
        //{
        //    string response = string.Empty;
        //    DataSet ds = new DataSet();
        //    List<Subscription> list = new List<Subscription>();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType=CommandType.StoredProcedure;
        //    cmd.CommandText="spViewAllSubscription";
        //    using (SqlConnection MyCon = db.OpenConnection())
        //    {
        //        cmd.Connection=MyCon;
        //        try
        //        {

        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(ds);
        //            for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
        //            {
        //                if (ds.Tables[0].Rows.Count>0)
        //                {
        //                    Subscription sub = new Subscription();
        //                    sub._SubscriptionId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubscriptionId"]);
        //                    sub._SubscriptionRate=Convert.ToDouble(ds.Tables[0].Rows[i]["SubscriptionRate"]);
        //                    sub._SubscriptionType=ds.Tables[0].Rows[i]["SubscriptionType"].ToString();
        //                    sub._SubscriptionValidity= Convert.ToDateTime(ds.Tables[0].Rows[i]["SubscriptionValidity"].ToString());
        //                    sub._PaymentInstallments= Convert.ToInt32(ds.Tables[0].Rows[i]["PaymentInstallments"]);
        //                    sub._JoiningAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["JoiningAmount"]);
        //                    sub._Status=ds.Tables[0].Rows[i]["Status"].ToString();
        //                    //dict=subscription.BindDictionary(ds);
        //                    list.Add(sub);
        //                }
        //            }

        //        }
        //        catch (SqlException e)
        //        {
        //            response=e.ToString();

        //        }
        //        finally
        //        {
        //            db.CloseConnection();
        //        }
        //        return list;
        //    }
        //}

        public Dictionary<string, object> SelectSubscription(int? SubscriptionId)
        {
            SubscriptionDict subscription = new SubscriptionDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spSelectSubscription";
            cmd.Parameters.AddWithValue("@SubscriptionId", SubscriptionId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    if (da==null)
                    {
                        response="Data is Unavailable";
                    }
                    else
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            dict=subscription.BindDictionary(ds);
                        }

                    }

                }
                catch (SqlException e)
                {
                    // response=e.ToString();
                    dict.Add("Response", e.ToString());
                }
                finally
                {
                    db.CloseConnection();
                }
                return dict;
            }
        }
    }
}