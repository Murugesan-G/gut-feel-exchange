using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NTCYApplication.Dictionaries;

namespace NTCYApplication.Models.Food
{
    public class FoodOrder : FoodOrderInterface
    {

        private int _OrderId;
        private int _UserId;
        private string _MembershipNo;  
        public List<FoodOrderList> foodOrderList;
        private double _TotalGST;
        private string _Status;
        private int _TableNo;
        private double _TotalAmount;
        private double _GrossAmount;
        private string _FoodName;
        private string _Quantity;
        private float _Price;
        private float _GST;

        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string MembershipNo
        {
            get { return _MembershipNo; }
            set { _MembershipNo = value; }
        }
        public double TotalGST
        {
            get { return _TotalGST; }
            set { _TotalGST = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int TableNo
        {
            get { return _TableNo; }
            set { _TableNo = value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
     
        
        //public class FoodOrderList
        //{
        //  public string FoodName { get; set; }
        //public  string Quantity { get; set; }
        //public  string Price { get; set; }
        // public double GST { get; set; }

        public string FoodName
        {
            get { return _FoodName; }
            set { _FoodName = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public float Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public float GST
        {
            get { return _GST; }
            set { _GST = value; }
        }

        public double GrossAmount
        {
            get { return _GrossAmount; }
            set { _GrossAmount = value; }
        }




        //}


        Dictionary<string, object> FoodOrderDictionary = new Dictionary<string, object>();
        DBConnection db = new DBConnection();

        string BindDictionary()
        {
            FoodOrderDictionary.Add("OrderId", _OrderId);
            FoodOrderDictionary.Add("UserId", _UserId);
            FoodOrderDictionary.Add("MembershipNo", _MembershipNo);
            FoodOrderDictionary.Add("TotalGST", _TotalGST);
            FoodOrderDictionary.Add("foodOrderList", foodOrderList);
            FoodOrderDictionary.Add("GST", _TotalGST);
            FoodOrderDictionary.Add("Status", _Status);
            FoodOrderDictionary.Add("TableNo", _TableNo);
            FoodOrderDictionary.Add("TotalAmount", _TotalAmount);
            FoodOrderDictionary.Add("GrossAmount", _GrossAmount);

            return "Success";
        }
        string TakeOrder(Dictionary<string, object> FoodorderDict)
        {
            return "Success";
        }

        string CancelOrder(int OrderId)
        {
            return "Success";
        }

        string EditOrder(Dictionary<string, object> FoodorderDict)
        {

            return "Success";
        }

        string ViewAllOrder()
        {
            return "Success";
        }

        public int CreateFoodOrderDetails(Dictionary<string, object> FoodOrderIDictionary )
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            FoodOrderDictionary = FoodOrderIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertAllFoodOrder";
            cmd.Parameters.AddWithValue("@UserName", FoodOrderDictionary["UserName"]);
            cmd.Parameters.AddWithValue("@MembershipNo", FoodOrderDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@TotalGST", FoodOrderDictionary["TotalGST"]);
            cmd.Parameters.AddWithValue("@Status", FoodOrderDictionary ["Status"]);
            cmd.Parameters.AddWithValue("@TableNo", FoodOrderDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@TotalAmount", FoodOrderDictionary["TotalAmount"]);
            cmd.Parameters.AddWithValue("@GrossAmount", FoodOrderDictionary["GrossAmount"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlParameter Param = new SqlParameter("@OrderId", 0);
                    Param.Direction=ParameterDirection.Output;
                    cmd.Parameters.Add(Param);
                    response=cmd.ExecuteNonQuery();
                    int OrderId = Convert.ToInt32(Param.Value);
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
    
        public string UpdateFoodOrderDetails(Dictionary<string, object> FoodOrderDictionary)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> SelectFoodOrder(int OrderId)
        {
            throw new NotImplementedException();
        }

        public string DeleteFood(int OrderId)
        {
            throw new NotImplementedException();
        }

        public List<Food> ViewAllFoodDetails(string type)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Food> list = new List<Food>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetAllFood";
            cmd.Parameters.AddWithValue("@Category",type);
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
                            Food fd = new Food();
                            fd.FoodName=ds.Tables[0].Rows[i]["FoodName"].ToString();
                            fd.Price=Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());
                            fd.GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            list.Add(fd);
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

        public void InsertIntoSubFoodOrderList(Dictionary<string, object> foodOrderDictionary)
        {
            throw new NotImplementedException();
        }

        public string CompleteFoodOrder(Dictionary<string, object> FoodOrderIDictionary)
        {
            throw new NotImplementedException();
        }
    }
}