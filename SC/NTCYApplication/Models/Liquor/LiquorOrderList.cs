using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NTCYApplication.Models.Liquor
{
    public class LiquorOrderList : LiquorOrderListInterface
    {
        private int _SubOrderId;
        private int _OrderId;
        private string _LiquorName;
        private float _Quantity;
        private string _Price;
        private double _GST;
        private string _Type;
        private int _LiquorId;
        public int SubOrderId
        { 
            get { return _SubOrderId; }
            set { _SubOrderId = value; }
        }
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        public string LiquorName
        {
            get { return _LiquorName; }
            set { _LiquorName = value; }
        }
        public float Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public double GST
        {
            get { return _GST; }
            set { _GST = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public int LiquorId
        {
            get { return _LiquorId; }
            set { _LiquorId=value; }
        }

        public Dictionary<string, object> LiquorOrderListIDictionary { get; private set; }
       // public object GST { get; internal set; }

        DBConnection db = new DBConnection();
        public Dictionary<string, object> LiquorOrderListDictionary = new Dictionary<string, object>();
        public string BindDictionary()
        {
            LiquorOrderListDictionary = new Dictionary<string, object>();
            LiquorOrderListDictionary.Add("SubOrderId", _SubOrderId);
            LiquorOrderListDictionary.Add("OrderId", _OrderId);
            LiquorOrderListDictionary.Add("LiquorName", _LiquorName);
            LiquorOrderListDictionary.Add("Quantity", _Quantity);
            LiquorOrderListDictionary.Add("Price", _Price);
            LiquorOrderListDictionary.Add("GST", _GST);
            LiquorOrderListDictionary.Add("Type", _Type);
            LiquorOrderListDictionary.Add("LiquorId", _LiquorId);
            return "Success";
        }

        public List<LiquorOrderList> ViewallLiquorOrderDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrderList> list = new List<LiquorOrderList>();
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
                            LiquorOrderList Det = new LiquorOrderList();
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det._LiquorName=ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det._Quantity=float.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Det._Price=ds.Tables[0].Rows[i]["Price"].ToString();
                            Det._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());

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

        public int CreateLiquorOrderList(Dictionary<string, object> LiquorOrderListDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            //  FoodOrderListDictionary = FoodOrderListIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertAllLiquorOrderList";

            cmd.Parameters.AddWithValue("@OrderId", LiquorOrderListDictionary["OrderId"]);
            cmd.Parameters.AddWithValue("@LiquorName", LiquorOrderListDictionary["LiquorName"]);
            cmd.Parameters.AddWithValue("@Quantity", LiquorOrderListDictionary["Quantity"]);
            cmd.Parameters.AddWithValue("@Price", LiquorOrderListDictionary["Price"]);
            cmd.Parameters.AddWithValue("@GST", LiquorOrderListDictionary["GST"]);
            cmd.Parameters.AddWithValue("@Status", "Processing");
            cmd.Parameters.AddWithValue("@Type", LiquorOrderListDictionary["Type"]);
            cmd.Parameters.AddWithValue("@LiquorId", LiquorOrderListDictionary["LiquorId"]);
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
                    response=OrderId;
                }
                catch (SqlException e)
                {
                    response=OrderId;
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public List<LiquorOrderList> ViewAllLiquorOrderDetails()
        {
            throw new NotImplementedException();
        }
    }
}