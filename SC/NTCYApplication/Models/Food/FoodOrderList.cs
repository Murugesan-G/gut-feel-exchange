using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace NTCYApplication.Models.Food
{
    public class FoodOrderList : FoodOrderListInterface
    {
        private int _SubOrderId;
        private int _OrderId;
        private string _FoodName;
        private string _Quantity;
        private string _Price;
        private double _GST;
        private string _Status;


        public int SubOrderId
        {
            get { return _SubOrderId; }
            set { _SubOrderId=value; }
        }
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId=value; }
        }
        public string FoodName
        {
            get { return _FoodName; }
            set { _FoodName=value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity=value; }
        }
        public string Price
        {
            get { return _Price; }
            set { _Price=value; }
        }
        public double GST
        {
            get { return _GST; }
            set { _GST=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }

        public Dictionary<string, object> FoodOrderListIDictionary { get; private set; }

        DBConnection db = new DBConnection();
        public Dictionary<string, object> FoodOrderListDictionary = new Dictionary<string, object>();

        public string BindDictionary()
        {
            FoodOrderListDictionary=new Dictionary<string, object>();
            FoodOrderListDictionary.Add("SubOrderId", _SubOrderId);
            FoodOrderListDictionary.Add("OrderId", _OrderId);
            FoodOrderListDictionary.Add("FoodName", _FoodName);
            FoodOrderListDictionary.Add("Quantity", _Quantity);
            FoodOrderListDictionary.Add("Price", _Price);
            FoodOrderListDictionary.Add("GST", _GST);
            FoodOrderListDictionary.Add("Status", _Status);

            return "Success";
        }


        public List<FoodOrderList> ViewAllFoodOrderDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FoodOrderList> list = new List<FoodOrderList>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_InsertAllFoodOrder";
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
                            FoodOrderList Det = new FoodOrderList();
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det._FoodName=ds.Tables[0].Rows[i]["FoodName"].ToString();
                            Det._Quantity=ds.Tables[0].Rows[i]["Quantity"].ToString();
                            Det._Price=ds.Tables[0].Rows[i]["Price"].ToString();
                            Det._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            Det._Status=ds.Tables[0].Rows[i]["Status"].ToString();
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

        public int CreateFoodOrderlist(Dictionary<string, object> FoodOrderListDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            //  FoodOrderListDictionary = FoodOrderListIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_InsertAllFoodOrderList";

            cmd.Parameters.AddWithValue("@OrderId", FoodOrderListDictionary["OrderId"]);
            cmd.Parameters.AddWithValue("@FoodName", FoodOrderListDictionary["FoodName"]);
            cmd.Parameters.AddWithValue("@Quantity", FoodOrderListDictionary["Quantity"]);
            cmd.Parameters.AddWithValue("@Price", FoodOrderListDictionary["Price"]);
            cmd.Parameters.AddWithValue("@GST", FoodOrderListDictionary["GST"]);
            cmd.Parameters.AddWithValue("@Status", FoodOrderListDictionary["Status"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlParameter Param = new SqlParameter("@SubOrderId", 0);
                    Param.Direction=ParameterDirection.Output;
                    cmd.Parameters.Add(Param);
                    response=cmd.ExecuteNonQuery();
                    int SubOrderId = Convert.ToInt32(Param.Value);
                    //response=OrderId;
                }
                catch (SqlException e)
                {
                    response=SubOrderId;
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }
    }
}
