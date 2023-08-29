using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web; 
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using NTCYApplication.Dictionaries;
using NTCYApplication.Models;

namespace NTCYApplication.Models.Food
{
    public class Food : FoodInterface
    {
        private int _FoodId;
        private string _FoodCode;
        private string _FoodName;
        private string _Category;
        private string _FoodDescription;
        private string _Quantity;
        private double _Price;
        private double _GST;
        private string _Status;
        public int FoodId
        {
            get { return _FoodId; }
            set { _FoodId = value; }
        }
        public string FoodCode
        {
            get { return _FoodCode; }
            set { _FoodCode = value; }
        }
        public string FoodName
        {
            get { return _FoodName; }
            set { _FoodName = value; }
        }
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        public string FoodDescription
        {
            get { return _FoodDescription; }
            set { _FoodDescription = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public double GST
        {
            get { return _GST; }
            set { _GST = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        DBConnection db = new DBConnection();
        Dictionary<string, object> FoodDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            FoodDictionary.Add("FoodId", _FoodId);
            FoodDictionary.Add("Category", _Category);
            FoodDictionary.Add("FoodCode", _FoodCode);
            FoodDictionary.Add("FoodName", _FoodName);
            FoodDictionary.Add("FoodDescription", _FoodDescription);
            FoodDictionary.Add("Quantity", _Quantity);
            FoodDictionary.Add("Price", _Price);
            FoodDictionary.Add("GST", _GST);
            FoodDictionary.Add("Status", _Status);

            return "Success";
        }

        public string CreateFoodDetails(Dictionary<string, object> FoodIDictionary)
        {

            string response = string.Empty;
            SqlCommand cmd = new SqlCommand();
            FoodDictionary = FoodIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertFood";
            cmd.Parameters.AddWithValue("@FoodCode", FoodDictionary["ItemCode"]);
            cmd.Parameters.AddWithValue("@FoodName", FoodDictionary["ItemName"]);
            cmd.Parameters.AddWithValue("@Category", FoodDictionary["ItemCategory"]);
            cmd.Parameters.AddWithValue("@FoodDescription", FoodDictionary["Description"]);
            cmd.Parameters.AddWithValue("@Quantity", FoodDictionary["Quantity"]);
            cmd.Parameters.AddWithValue("@Price", FoodDictionary["RatePerUnit"]);
            cmd.Parameters.AddWithValue("@GST", FoodDictionary["GST"]);
            cmd.Parameters.AddWithValue("@Status", FoodDictionary["Status"]);

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

        public string UpdateFoodDetails(Dictionary<string, object> FoodIDictionary)
        {

            string response = string.Empty;
            FoodDictionary = FoodIDictionary;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateFood";
            cmd.Parameters.AddWithValue("@FoodId", FoodDictionary["FoodId"]);
            cmd.Parameters.AddWithValue("@FoodCode", FoodDictionary["ItemCode"]);
            cmd.Parameters.AddWithValue("@FoodName", FoodDictionary["ItemName"]);
            cmd.Parameters.AddWithValue("@Category", FoodDictionary["ItemCategory"]);
            cmd.Parameters.AddWithValue("@FoodDescription", FoodDictionary["Description"]);
            cmd.Parameters.AddWithValue("@Quantity", FoodDictionary["Quantity"]);
            cmd.Parameters.AddWithValue("@Price", FoodDictionary["RatePerUnit"]);
            cmd.Parameters.AddWithValue("@GST", FoodDictionary["GST"]);
            cmd.Parameters.AddWithValue("@Status", FoodDictionary["Status"]);

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

        public Dictionary<string, object> SelectFood(int FoodId)
        {
            FoodDict Food = new FoodDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewFood";
            cmd.Parameters.AddWithValue("@FoodId", FoodId);
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
                            dict=Food.BindDictionary(ds);
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

        public string DeleteFood(int FoodId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteFood";
            cmd.Parameters.AddWithValue("@FoodId", FoodId);
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

        public List<Food> ViewAllFoodDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Food> list = new List<Food>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewAllFoodDetails";
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
                            Food Det = new Food();
                            Det._FoodId=Convert.ToInt32(ds.Tables[0].Rows[i]["FoodId"]);
                            Det._FoodCode=ds.Tables[0].Rows[i]["FoodCode"].ToString();
                            Det._FoodName=ds.Tables[0].Rows[i]["FoodName"].ToString();
                            Det._Category=ds.Tables[0].Rows[i]["Category"].ToString();
                            Det._FoodDescription=ds.Tables[0].Rows[i]["FoodDescription"].ToString();
                            Det._Quantity=ds.Tables[0].Rows[i]["Quantity"].ToString();
                            Det._Price=Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());
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

        public string UpdateFoodGst(int FoodId,float gst)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateFoodGst";
            cmd.Parameters.AddWithValue("@FoodId", FoodId);
            cmd.Parameters.AddWithValue("@GST", gst);
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