using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace NTCYApplication.Models.Food
{
    public class KitchenOrder : KitchenOrderInterface
    {

        private int _OrderNumber;
        private string _MembershipNo;
        private string _TableNo;
        private string _FoodName;
        private int _Quantity;
        private int _SubOrderId;
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
        public string TableNo
        {
            get { return _TableNo; }
            set { _TableNo=value; }
        }
        public string FoodName
        {
            get { return _FoodName; }
            set { _FoodName=value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity=value; }
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
        Dictionary<string, object> KitchenOrderDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            KitchenOrderDictionary=new Dictionary<string, object>();
            KitchenOrderDictionary.Add("OrderNumber", _OrderNumber);
            KitchenOrderDictionary.Add("MembershipNo", _MembershipNo);
            KitchenOrderDictionary.Add("TableNo", _TableNo);
            KitchenOrderDictionary.Add("FoodName", _FoodName);
            KitchenOrderDictionary.Add("Quantity", _Quantity);
            KitchenOrderDictionary.Add("SubOrderId", _SubOrderId);
            KitchenOrderDictionary.Add("MemberName", _MemberName);
            return "Success";
        }


        public string CreateKitchenOrder(Dictionary<string, object> KitchenOrderIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            KitchenOrderDictionary=KitchenOrderIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetAllFoodOrderList";
            cmd.Parameters.AddWithValue("@UserId", KitchenOrderDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@MembershipNo", KitchenOrderDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@TotalGST", KitchenOrderDictionary["TotalGST"]);
            cmd.Parameters.AddWithValue("@Status", KitchenOrderDictionary["Status"]);
            cmd.Parameters.AddWithValue("@TableNo", KitchenOrderDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@TotalAmount", KitchenOrderDictionary["TotalAmount"]);
            cmd.Parameters.Add("@OrderId", SqlDbType.Int).Direction=ParameterDirection.Output;
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery().ToString();
                    int OrderId = Convert.ToInt32(cmd.Parameters["@OrderId"].Value);
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

        public string UpdateKitchenOrder(Dictionary<string, object> KitchenOrderIDictionary)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            KitchenOrderDictionary=KitchenOrderIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateKitchenOrder";
            cmd.Parameters.AddWithValue("@UserId", KitchenOrderDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@MembershipNo", KitchenOrderDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@TotalGST", KitchenOrderDictionary["TotalGST"]);
            cmd.Parameters.AddWithValue("@Status", KitchenOrderDictionary["Status"]);
            cmd.Parameters.AddWithValue("@TableNo", KitchenOrderDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@TotalAmount", KitchenOrderDictionary["TotalAmount"]);
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


        public List<KitchenOrder> ViewAllKitchenOrder()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<KitchenOrder> list = new List<KitchenOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetAllFoodOrderList";
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
                            KitchenOrder Det = new KitchenOrder();
                            Det._OrderNumber=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._TableNo=Convert.ToString(ds.Tables[0].Rows[i]["TableNo"]);
                            Det._FoodName=Convert.ToString(ds.Tables[0].Rows[i]["FoodName"]);
                            Det._Quantity=Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"].ToString());
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


        public string DeleteKitchenOrder(int OrderId)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteEmployee";
            cmd.Parameters.AddWithValue("@OrderId", OrderId);
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

        public string EditKitchenOrder(Dictionary<string, object> KitchenOrderdict)
        {
            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            KitchenOrderDictionary=KitchenOrderdict;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateFoodOrderList";
            cmd.Parameters.AddWithValue("@SubOrderId", KitchenOrderDictionary["SubOrderId"]);
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

        public List<KitchenOrder> ViewCompletedOrders()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<KitchenOrder> list = new List<KitchenOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewCompletedKitchenOrder";
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
                            KitchenOrder Det = new KitchenOrder();
                            Det.OrderNumber=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            Det._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det._TableNo=Convert.ToString(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            Det._FoodName=Convert.ToString(ds.Tables[0].Rows[i]["FoodName"].ToString());
                            Det._Quantity=Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Det._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"].ToString());

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
         
        public List<KitchenOrder> DisplayCompletedOrders() 
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<KitchenOrder> list = new List<KitchenOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDisplayCompletedOrders";
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
                            KitchenOrder KO = new KitchenOrder();
                            KO._OrderNumber=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            KO._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            KO._TableNo=Convert.ToString(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            KO._FoodName=Convert.ToString(ds.Tables[0].Rows[i]["FoodName"].ToString());
                            KO._Quantity=Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            list.Add(KO);
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

        public string ServeOrder(int OrderNo, string Item)
        {
            string response = string.Empty;          
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrderNo", OrderNo);
            cmd.Parameters.AddWithValue("@Item",Item);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spServeFoodOrder";
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
    }
}