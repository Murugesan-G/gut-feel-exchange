using NTCYApplication.Models.Club;
using NTCYApplication.Models.Liquor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NTCYApplication.Models.TallyIntegration
{
    public class NTCYTallyIntegration
    {
        
        DBConnection db = new DBConnection();
       
        public DataTable GetLiquorBills(string LastOrderId)  
        {
            //string response = string.Empty;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetLiquorBills";
            cmd.Parameters.AddWithValue("@LastOrderId",LastOrderId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    db.CloseConnection();
                }
               
                return dt;
            }
        }


        public DataTable GetSubscriptionBills(string LastSubId) 
        {
            //string response = string.Empty;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetSubscriptionBills";
            cmd.Parameters.AddWithValue("@LastSubId", LastSubId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    for (int i = 0; i<dt.Rows.Count; i++)
                    {
                        if (dt.Rows.Count>0)
                        {
                            Subscription sub = new Subscription();
                            sub.SubPayId=Convert.ToInt32(dt.Rows[i]["SubCollectionId"]);
                            sub.MemberNo=Convert.ToString(dt.Rows[i]["MembershipNo"]);
                            sub.SubAmt=Convert.ToDouble(dt.Rows[i]["SubscriptionPaid"]);
                            sub.BillDate=DateTime.Parse(dt.Rows[i]["PaidDate"].ToString());
                            sub.SubscriptionType=dt.Rows[i]["SubscriptionType"].ToString();
                            dt.Rows.Add(sub);
                        }
                    }
                }
                catch(Exception e)
                {

                }
                finally
                {
                    db.CloseConnection();
                }
                return dt;
            }
        }
    }
}