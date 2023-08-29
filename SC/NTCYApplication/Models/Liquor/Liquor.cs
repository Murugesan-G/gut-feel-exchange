using NTCYApplication.Dictionaries;
using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace NTCYApplication.Models.Liquor
{
    public class Liquor : LiquorInterface
    {
        int _LiquorId;
        string _LiquorName;
        int _LiquorCategoryId;
        DateTime _EffectiveDate;
        string _CategoryName;
        string _PegorBottle;
        double _PegsPerBottle;
        double _Volume;
        double _SellingPriceBottle;
        double _SellingPricePeg;
        string _Status;
        double _GST;
        private double _CurrentStockBottles;
        private double _CurrentStockPegs;


        public double CurrentStockBottles
        {
            get { return _CurrentStockBottles; }
            set { _CurrentStockBottles=value; }
        }
        public double CurrentStockPegs
        {
            get { return _CurrentStockPegs; }
            set { _CurrentStockPegs=value; }
        }
        public int LiquorId
        {
            get { return _LiquorId; }
            set { _LiquorId=value; }
        }
        public string LiquorName
        {
            get { return _LiquorName; }
            set { _LiquorName=value; }
        }
        public int LiquorCategoryId
        {
            get { return _LiquorCategoryId; }
            set { _LiquorCategoryId=value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName=value; }
        }

        public DateTime EffectiveDate
        {
            get { return _EffectiveDate; }
            set { _EffectiveDate=value; }
        }

        public string PegorBottle
        {
            get { return _PegorBottle; }
            set { _PegorBottle=value; }
        }
        public double PegsPerBottle
        {
            get { return _PegsPerBottle; }
            set { _PegsPerBottle=value; }
        }
        public double Volume
        {
            get { return _Volume; }
            set { _Volume=value; }
        }

        public double SellingPriceBottle
        {
            get { return _SellingPriceBottle; }
            set { _SellingPriceBottle=value; }
        }
        public double SellingPricePeg
        {
            get { return _SellingPricePeg; }
            set { _SellingPricePeg=value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status=value; }
        }
        public double GST
        {
            get { return _GST; }
            set { _GST=value; }
        }
        DBConnection db = new DBConnection();
        Dictionary<string, object> LiquorDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            LiquorDictionary=new Dictionary<string, object>();
            LiquorDictionary.Add("LiquorId", _LiquorId);
            LiquorDictionary.Add("LiquorName", _LiquorName);
            LiquorDictionary.Add("LiquorCategoryId", _LiquorCategoryId);
            LiquorDictionary.Add("CategoryName", _CategoryName);
            LiquorDictionary.Add("EffectiveDate", _EffectiveDate);
            LiquorDictionary.Add("PegorBottle", _PegorBottle);
            LiquorDictionary.Add("PegsPerBottle", _PegsPerBottle);
            LiquorDictionary.Add("Volume", _Volume);
            //LiquorDictionary.Add("RatePerPeg", _RatePerPeg);
            //LiquorDictionary.Add("RatePerBottle", _RatePerBottle);
            LiquorDictionary.Add("SellingPriceBottle", _SellingPriceBottle);
            LiquorDictionary.Add("SellingPricePeg", _SellingPricePeg);
            LiquorDictionary.Add("GST", _GST);
            LiquorDictionary.Add("Status", _Status);
            LiquorDictionary.Add("LiquorCategoryId", _LiquorCategoryId);
            return "Success";
        }
        public string CreateLiquorDetails(Dictionary<string, object> LiquorIDictionary)
        {

            string response;
            SqlCommand cmd = new SqlCommand();
            LiquorDictionary=LiquorIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertLiquor";
            cmd.Parameters.AddWithValue("@LiquorName", LiquorDictionary["LiquorName"]);
            cmd.Parameters.AddWithValue("@Liquor_Cat_Id", LiquorDictionary["LiquorCategoryId"]);
            //cmd.Parameters.AddWithValue("@Vendor", LiquorDictionary["Vendor"]);
            DateTime effectiveDate = DateTime.ParseExact(LiquorDictionary["EffectiveDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            cmd.Parameters.AddWithValue("@EffectiveDate", effectiveDate);
            cmd.Parameters.AddWithValue("@PegorBottle", LiquorDictionary["PegorBottle"]);
            cmd.Parameters.AddWithValue("@PegsPerBottle", LiquorDictionary["QuantityNoOfPegs"]);
            cmd.Parameters.AddWithValue("@Volume", LiquorDictionary["QuantityPerBottle"]);
            cmd.Parameters.AddWithValue("@SellingPricePerBottle", LiquorDictionary["SellingPriceBottle"]);
            cmd.Parameters.AddWithValue("@SellingPricePerPeg", LiquorDictionary["SellingPricePeg"]);
            cmd.Parameters.AddWithValue("@Status", LiquorDictionary["Status"]);
            cmd.Parameters.AddWithValue("@GST_Rate", LiquorDictionary["GST"]);
            //   cmd.Parameters.AddWithValue("@CategoryName", LiquorDictionary["CategoryName"]);
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

        public string UpdateLiquorDetails(Dictionary<string, object> LiquorIDictionary)
        {

            string response;
            SqlCommand cmd = new SqlCommand();
            LiquorDictionary=LiquorIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateLiquor";
            cmd.Parameters.AddWithValue("@LiquorId", LiquorDictionary["LiquorId"]);
            cmd.Parameters.AddWithValue("@LiquorName", LiquorDictionary["LiquorName"]);
            cmd.Parameters.AddWithValue("@Liquor_Cat_Id", LiquorDictionary["LiquorCategoryId"]);
            //cmd.Parameters.AddWithValue("@Vendor", LiquorDictionary["Vendor"]);
            cmd.Parameters.AddWithValue("@EffectiveDate", Convert.ToDateTime(LiquorDictionary["EffectiveDate"]));
            cmd.Parameters.AddWithValue("@PegorBottle", LiquorDictionary["PegorBottle"]);
            cmd.Parameters.AddWithValue("@PegsPerBottle", LiquorDictionary["QuantityNoOfPegs"]);
            cmd.Parameters.AddWithValue("@Volume", LiquorDictionary["QuantityPerBottle"]);
            cmd.Parameters.AddWithValue("@SellingPricePerBottle", LiquorDictionary["SellingPriceBottle"]);
            cmd.Parameters.AddWithValue("@SellingPricePerPeg", LiquorDictionary["SellingPricePeg"]);
            cmd.Parameters.AddWithValue("@Status", LiquorDictionary["Status"]);
            cmd.Parameters.AddWithValue("@GST_Rate", LiquorDictionary["GST"]);
            // cmd.Parameters.AddWithValue("@CategoryName", LiquorDictionary["CategoryName"]);
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

        public List<Liquor> ViewAllLiquorDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Liquor> list = new List<Liquor>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewAllLiquorDetails";
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
                            Liquor Det = new Liquor();
                            Det._LiquorId=Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"]);
                            Det._LiquorName=Convert.ToString(ds.Tables[0].Rows[i]["LiquorName"]);
                            Det._CategoryName=ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            Det._EffectiveDate=DateTime.Parse(ds.Tables[0].Rows[i]["EffectiveDate"].ToString()/*, CultureInfo.CreateSpecificCulture("en-IN")*/);
                            Det.PegsPerBottle=Convert.ToDouble(ds.Tables[0].Rows[i]["PegsPerBottle"].ToString());
                            Det.Volume=Convert.ToDouble(ds.Tables[0].Rows[i]["Volume"]);
                            //Det._RatePerBottle=Convert.ToDouble(ds.Tables[0].Rows[i]["RatePerBottle"].ToString());
                            //Det._RatePerPeg=Convert.ToDouble(ds.Tables[0].Rows[i]["RatePerPeg"].ToString());
                            Det._GST=Convert.ToDouble(ds.Tables[0].Rows[i]["GST_Rate"].ToString());
                            Det._Status=ds.Tables[0].Rows[i]["Status"].ToString();
                            Det._SellingPriceBottle=Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPricePerBottle"].ToString());
                            Det._SellingPricePeg=Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPricePerPeg"].ToString());
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

        public string DeleteLiquor(int LiquorId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteLiquor";
            cmd.Parameters.AddWithValue("@LiquorId", LiquorId);
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

        public Dictionary<string, object> SelectLiquor(int LiquorId)
        {
            LiquorDict subscription = new LiquorDict();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spViewLiquor";
            cmd.Parameters.AddWithValue("@LiquorId", LiquorId);
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


        public List<Liquor> ViewAllLiquorCategory()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<Liquor> list = new List<Liquor>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetAllLiquorCategory";
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
                            Liquor category = new Liquor();
                            category._LiquorCategoryId=Convert.ToInt16(ds.Tables[0].Rows[i]["Liquor_Cat_Id"]);
                            category._CategoryName=ds.Tables[0].Rows[i]["CategoryName"].ToString();
                            list.Add(category);
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

        public DataSet GetLiquors(string Prefix)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetLiquor";
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }

                return ds;
            }
        }

        public string UpdateGst(int LiquorId,float gst)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateLiquorGst";
            cmd.Parameters.AddWithValue("@LiquorId", LiquorId);
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

        //public DataSet GetLiquorStockDetails(int? LiquorId)
        //{
        //    string response = string.Empty;
        //    DataSet ds = new DataSet();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "sp_GetAllStockAdjustment";
        //    cmd.Parameters.AddWithValue("@LiquorId", LiquorId);
        //    try
        //    {
        //        SqlConnection MyCon = db.OpenConnection();
        //        cmd.Connection = MyCon;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);

        //    }
        //    catch (SqlException e)
        //    {
        //        response = e.ToString();

        //    }
        //    db.CloseConnection();

        //    return ds;
        //}
        //public DataSet GetLiquors(string Prefix)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
