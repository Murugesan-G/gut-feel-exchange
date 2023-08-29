using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NTCYApplication.Models.Liquor
{
    public class LiquorOrderArea : ILiquorOrderArea
    {
        private int _OrderNumber;
        private string _MembershipNo;
        private int _TableNo;
        private string _Items;
        private float _Qty;
        private int _SubOrderId;
        private string _Type;
        private string _MemberName;
        public int OrderNumber
        {
            get { return _OrderNumber; }
            set { _OrderNumber=value; }
        }
        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo=value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type=value; }
        }
        public int TableNo
        {
            get { return _TableNo; }
            set { _TableNo=value; }
        }
        public string Items
        {
            get { return _Items; }
            set { _Items=value; }
        }
        public float Qty
        {
            get { return _Qty; }
            set { _Qty=value; }
        }
        public int SubOrderId
        {
            get { return _SubOrderId; }
            set { _SubOrderId=value; }
        }
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName=value; }
        }
        DBConnection db = new DBConnection();
        Dictionary<string, object> LiquorOrderAreaDictionary = new Dictionary<string, object>();


        string BindDictionary()
        {
            LiquorOrderAreaDictionary=new Dictionary<string, object>();
            LiquorOrderAreaDictionary.Add("OrderNumber", _OrderNumber);
            LiquorOrderAreaDictionary.Add("MembershipNo", _MembershipNo);
            LiquorOrderAreaDictionary.Add("TableNo", _TableNo);
            LiquorOrderAreaDictionary.Add("Items", _Items);
            LiquorOrderAreaDictionary.Add("Qty", _Qty);
            LiquorOrderAreaDictionary.Add("SubOrderId", _SubOrderId);
            LiquorOrderAreaDictionary.Add("Type", _Type);
            LiquorOrderAreaDictionary.Add("MemberName",_MemberName);
            return "Success";
        }
        public List<LiquorOrderArea> ViewAllLiquorOrders()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrderArea> list = new List<LiquorOrderArea>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAllLiquorOrderDetails";
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
                            LiquorOrderArea Det = new LiquorOrderArea();
                            Det._OrderNumber=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            Det._Items=ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det._Qty=float.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"].ToString());
                            Det._Type=ds.Tables[0].Rows[i]["Type"].ToString();
                            Det._MemberName=ds.Tables[0].Rows[i]["MemberName"].ToString();
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


        public string EditLiquorOrder(Dictionary<string, object> LiquorOrderAreaIdict)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            LiquorOrderAreaDictionary=LiquorOrderAreaIdict;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateLiquorOrderList";
            cmd.Parameters.AddWithValue("@SubOrderId", LiquorOrderAreaDictionary["SubOrderId"]);
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

        public List<LiquorOrderArea> ViewCompletedOrders()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrderArea> list = new List<LiquorOrderArea>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewCompletedLiquorOrder";
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
                            LiquorOrderArea Det = new LiquorOrderArea();
                            Det._OrderNumber=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            Det._Items=ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det._Qty=float.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"].ToString());
                            Det._Type=ds.Tables[0].Rows[i]["Type"].ToString();
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