using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.FrontOffice
{
    public class ServicesCollection
    {
        int _ServCollectionId;
        int _ServiceId;
        string _ServiceName;
        string _MembershipNo;
        string _MemberName;
        int _NumberOfGuests;
        string _PayType;
        float _Amount;
        DateTime _StartDate;
        DateTime _EndDate;
        string _Status;
        float _Total;
        float _GST;


        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName=value; }
        }
        public float GST
        {
            get { return _GST; }
            set { _GST=value; }
        }
        public float Total
        {
            get { return _Total; }
            set { _Total=value; }
        }
        public int NumberOfGuests
        {
            get { return _NumberOfGuests; }
            set { _NumberOfGuests=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }
        public int ServCollectionId
        {
            get { return _ServCollectionId; }
            set { _ServCollectionId=value; }
        }
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId=value; }
        }
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName=value; }
        }
        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo=value; }
        }
        public string PayType
        {
            get { return _PayType; }
            set { _PayType=value; }
        }
        public float Amount
        {
            get { return _Amount; }
            set { _Amount=value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate=value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate=value; }
        }

        DBConnection db = new DBConnection();

        public int Save()
        {
            int resultval = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            if (this.ServCollectionId==0)
            {
                cmd.CommandText="spInsertServicesCollection";
            }
            else
            {
               
                cmd.CommandText="spUpdateServicesCollection";
                cmd.Parameters.AddWithValue("@ServiceCollectionId", this.ServCollectionId);
            }
            cmd.Parameters.AddWithValue("@ServiceId", this.ServiceId);
            cmd.Parameters.AddWithValue("@MembershipNo", this.MembershipNo);
            cmd.Parameters.AddWithValue("@NumberOfGuests", this.NumberOfGuests);
            cmd.Parameters.AddWithValue("@PayType", this.PayType);
            cmd.Parameters.AddWithValue("@Amount", this.Amount);
            cmd.Parameters.AddWithValue("@GST", this.GST);
            cmd.Parameters.AddWithValue("@StartDate", this.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", this.EndDate);
            cmd.Parameters.AddWithValue("@Total",this._Total);
            cmd.Parameters.AddWithValue("@Status", this.Status);
          
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //Check for errors using try catch 
                    resultval=cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //resultval=e.Message.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }

            }

            return resultval;
        }


        public List<ServicesCollection> ViewAllServiceCollections()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<ServicesCollection> list = new List<ServicesCollection>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllServiceCollection";
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
                            ServicesCollection sub = new ServicesCollection();
                            sub._ServiceName=(ds.Tables[0].Rows[i]["ServiceName"].ToString());
                            sub._MembershipNo=ds.Tables[0].Rows[i]["MembershipNo"].ToString();
                            sub._NumberOfGuests=Convert.ToInt32(ds.Tables[0].Rows[i]["NumberOfGuests"]);
                            sub._Amount=float.Parse(ds.Tables[0].Rows[i]["Amount"].ToString());
                            sub._GST=float.Parse(ds.Tables[0].Rows[i]["GST"].ToString());
                            sub._PayType=ds.Tables[0].Rows[i]["PayType"].ToString();
                            sub._StartDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"].ToString());
                            sub._EndDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["EndDate"].ToString());
                            sub._Total=float.Parse(ds.Tables[0].Rows[i]["Total"].ToString());
                            sub._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            sub._MemberName=ds.Tables[0].Rows[i]["MemberName"].ToString();
                            sub._ServCollectionId=Convert.ToInt32(ds.Tables[0].Rows[i]["ServCollectionId"]);
                            list.Add(sub);
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



        public List<ServicesCollection> ViewServiceCollection( int ServCollectionId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<ServicesCollection> list = new List<ServicesCollection>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewServiceCollection";
            cmd.Parameters.AddWithValue("@ServCollectionId",ServCollectionId);
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
                            ServicesCollection sub = new ServicesCollection();
                            sub._ServiceName=(ds.Tables[0].Rows[i]["ServiceName"].ToString());
                            sub._MembershipNo=ds.Tables[0].Rows[i]["MembershipNo"].ToString();
                            sub._NumberOfGuests=Convert.ToInt32(ds.Tables[0].Rows[i]["NumberOfGuests"]);
                            sub._Amount=float.Parse(ds.Tables[0].Rows[i]["Amount"].ToString());
                            sub._GST=float.Parse(ds.Tables[0].Rows[i]["GST"].ToString());
                            sub._PayType=ds.Tables[0].Rows[i]["PayType"].ToString();
                            sub._StartDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["StartDate"].ToString());
                            sub._EndDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["EndDate"].ToString());
                            sub._Total=Convert.ToInt32(ds.Tables[0].Rows[i]["Total"]);
                            sub._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            sub._MemberName=ds.Tables[0].Rows[i]["MemberName"].ToString();
                            sub._ServCollectionId=Convert.ToInt32(ds.Tables[0].Rows[i]["ServCollectionId"]);
                            sub._ServiceId=Convert.ToInt32(ds.Tables[0].Rows[i]["ServiceId"]);
                            list.Add(sub);
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

       public float GetGSTBasedOnService(int serviceCode)
        {
            float GST = 0;
            string response = ""; 
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetGSTBasedOnService";
            cmd.Parameters.AddWithValue("@ServiceCode", serviceCode);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    float.TryParse(cmd.ExecuteScalar().ToString(),out GST);
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }              
            }
            return GST;
        }
        public float GetChargeBasedOnService(string paytype,int serviceCode)
        {
            float Charge = 0;
            string response = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetChargeBasedOnService";
            cmd.Parameters.AddWithValue("@Paytype", paytype);
            cmd.Parameters.AddWithValue("@ServiceCode", serviceCode);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    float.TryParse(cmd.ExecuteScalar().ToString(), out Charge);
                }
                catch (SqlException e)
                {
                    response=e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
            }
            return Charge;
        }
    }      
}