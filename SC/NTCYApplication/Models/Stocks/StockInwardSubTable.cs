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

namespace NTCYApplication.Models.Stocks
{
    public class StockInwardSubTable : StockInwardSubTableInterface
    {
        private int _StockSubId;
        private int _GrnId; 
        private int _LiquorId;
        private string _ItemName;
        private double _PurchaseOrderRate;
        private int _PurchaseOrderQty;
        private double _MRP;
        private string _SupplierName;
        private double _TaxAmount;
        private double _TaxPercentage;
        private double _DiscountAmount;
        private double _DiscountPercentage;
        private int _RejectedQty;
        private int _AcceptedQty;
        private string _RejectedRemarks;
        //13-6-18



        public int StockSubId
        {
            get { return _StockSubId; }
            set { _StockSubId = value; }
        }
        public int GrnId
        {
            get { return _GrnId; }
            set { _GrnId = value; }
        }    
        public int LiquorId
        {
            get { return _LiquorId; }
            set { _LiquorId= value; }
        }
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        public double PurchaseOrderRate
        {
            get { return _PurchaseOrderRate; }
            set { _PurchaseOrderRate = value; }
        }
        public int PurchaseOrderQty
        {
            get { return _PurchaseOrderQty; }
            set { _PurchaseOrderQty = value; }
        }
        public double MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }
        public double TaxAmount
        {
            get { return _TaxAmount; }
            set { _TaxAmount = value; }
        }
        public double TaxPercentage
        {
            get { return _TaxPercentage; }
            set { _TaxPercentage = value; }
        }
        public double DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        public double DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
        public int RejectedQty
        {
            get { return _RejectedQty; }
            set { _RejectedQty = value; }
        }
        public int AcceptedQty
        {
            get { return _AcceptedQty; }
            set { _AcceptedQty = value; }
        }
        public string RejectedRemarks
        {
            get { return _RejectedRemarks; }
            set { _RejectedRemarks=value; }
        }

        DBConnection db = new DBConnection();
        Dictionary<string, object> StockInwardSubTableDictionary = new Dictionary<string, object>();

        string BindDictionary()
        {
            StockInwardSubTableDictionary.Add("StockSubId", _StockSubId);
            StockInwardSubTableDictionary.Add("GrnId", _GrnId);
            StockInwardSubTableDictionary.Add("ItemId", _LiquorId);
            StockInwardSubTableDictionary.Add("ItemName", _ItemName);
            StockInwardSubTableDictionary.Add("PurchaseOrderRate", _PurchaseOrderRate);
            StockInwardSubTableDictionary.Add("PurchaseOrderQty", _PurchaseOrderQty);
            StockInwardSubTableDictionary.Add("MRP", _MRP);
            StockInwardSubTableDictionary.Add("SupplierName", _SupplierName);
            StockInwardSubTableDictionary.Add("TaxAmount", _TaxAmount);
            StockInwardSubTableDictionary.Add("TaxPercentage", _TaxPercentage);
            StockInwardSubTableDictionary.Add("DiscountAmount", _DiscountAmount);
            StockInwardSubTableDictionary.Add("DiscountPercentage", _DiscountPercentage);
            StockInwardSubTableDictionary.Add("RejectedQty", _RejectedQty);
            StockInwardSubTableDictionary.Add("AcceptedQty", _AcceptedQty);
            StockInwardSubTableDictionary.Add("RejectedRemarks", _RejectedRemarks);

            return "Success";
        }

        public string AddStockSubTableDetails(Dictionary<string, object> StockInwardSubTableIDictionary)
        {
            string response ;
            SqlCommand cmd = new SqlCommand();
            StockInwardSubTableDictionary = StockInwardSubTableIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText ="sp_InsertRecieptDetails";
            //cmd.Parameters.AddWithValue("@GrnId", StockInwardIDictionary["GrnId"]);
            cmd.Parameters.AddWithValue("@GrnId", StockInwardSubTableDictionary["GrnId"]);
            cmd.Parameters.AddWithValue("@LiquorId", StockInwardSubTableDictionary["LiquorId"]);
            cmd.Parameters.AddWithValue("@ItemName", StockInwardSubTableDictionary["ItemName"]);
            cmd.Parameters.AddWithValue("@PurchaseOrderRate", StockInwardSubTableDictionary["PurchaseOrderRate"]);
            cmd.Parameters.AddWithValue("@PurchaseOrderQty", StockInwardSubTableDictionary["PurchaseOrderQty"]);
            cmd.Parameters.AddWithValue("@MRP", StockInwardSubTableDictionary["MRP"]);
            cmd.Parameters.AddWithValue("@TaxAmount", StockInwardSubTableDictionary["TaxAmount"]);
            cmd.Parameters.AddWithValue("@TaxPercentage", StockInwardSubTableDictionary["TaxPercentage"]);
            cmd.Parameters.AddWithValue("@DiscountAmount", StockInwardSubTableDictionary["DiscountAmount"]);
            cmd.Parameters.AddWithValue("@DiscountPercentage", StockInwardSubTableDictionary["DiscountPercentage"]);
            cmd.Parameters.AddWithValue("@RejectedQty", StockInwardSubTableDictionary["RejectedQty"]);
            cmd.Parameters.AddWithValue("@AcceptedQty", StockInwardSubTableDictionary["AcceptedQty"]);
            cmd.Parameters.AddWithValue("@RejectedRemarks", StockInwardSubTableDictionary["RejectedRemarks"]);
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

        //public string AddStockSubTableDetails(Dictionary<string, object> StockInwardSubTableIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        public Dictionary<string, object> SelectStockSubTable(int StockSubId)
        {
            throw new NotImplementedException();
        }

        public string UpdateStockSubDetails(Dictionary<string, object> StockInwardIDictionary)
        {
            throw new NotImplementedException();
        }

        public List<StockInwardSubTable> ViewDetailStock(int GrnId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<StockInwardSubTable> list = new List<StockInwardSubTable>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_ViewDetailStock";
            cmd.Parameters.AddWithValue("@GrnId", GrnId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //SqlConnection MyCon = db.OpenConnection();
                    //cmd.Connection=MyCon;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            StockInwardSubTable Det = new StockInwardSubTable();
                            Det._StockSubId=Convert.ToInt32(ds.Tables[0].Rows[i]["StockSubId"].ToString());
                            Det._GrnId=Convert.ToInt32(ds.Tables[0].Rows[i]["GrnId"].ToString());
                            Det._PurchaseOrderRate=Convert.ToDouble(ds.Tables[0].Rows[i]["PurchaseOrderRate"].ToString());
                            Det._PurchaseOrderQty=Convert.ToInt16(ds.Tables[0].Rows[i]["PurchaseOrderQty"].ToString());
                            Det._MRP=Convert.ToDouble(ds.Tables[0].Rows[i]["MRP"].ToString());
                            Det._TaxAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TaxAmount"].ToString());
                            Det._TaxPercentage=Convert.ToDouble(ds.Tables[0].Rows[i]["TaxPercentage"].ToString());
                            Det._DiscountAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountAmount"].ToString());
                            Det._DiscountPercentage=Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountPercentage"].ToString());
                            Det._RejectedQty=Convert.ToInt32(ds.Tables[0].Rows[i]["RejectedQty"].ToString());
                            Det._AcceptedQty=Convert.ToInt32(ds.Tables[0].Rows[i]["AcceptedQty"].ToString());
                            Det._RejectedRemarks=ds.Tables[0].Rows[i]["RejectedRemarks"].ToString();
                            Det._ItemName=ds.Tables[0].Rows[i]["LiquorName"].ToString();
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


        public List<StockInwardSubTable> ViewAllStockSubTableDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<StockInwardSubTable> list = new List<StockInwardSubTable>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_ViewDetailStock";
            cmd.Parameters.AddWithValue("@GrnId", GrnId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    //SqlConnection MyCon = db.OpenConnection();
                    //cmd.Connection=MyCon;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i<ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count>0)
                        {
                            StockInwardSubTable Det = new StockInwardSubTable();
                            Det._StockSubId=Convert.ToInt32(ds.Tables[0].Rows[i]["StockSubId"].ToString());
                            Det._GrnId=Convert.ToInt32(ds.Tables[0].Rows[i]["GrnId"].ToString());
                            Det._PurchaseOrderRate=Convert.ToDouble(ds.Tables[0].Rows[i]["PurchaseOrderRate"].ToString());
                            Det._PurchaseOrderQty=Convert.ToInt16(ds.Tables[0].Rows[i]["PurchaseOrderQty"].ToString());
                            Det._MRP=Convert.ToDouble(ds.Tables[0].Rows[i]["MRP"].ToString());
                            Det._TaxAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TaxAmount"].ToString());
                            Det._TaxPercentage=Convert.ToDouble(ds.Tables[0].Rows[i]["TaxPercentage"].ToString());
                            Det._DiscountAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountAmount"].ToString());
                            Det._DiscountPercentage=Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountPercentage"].ToString());
                            Det._RejectedQty=Convert.ToInt32(ds.Tables[0].Rows[i]["RejectedQty"].ToString());
                            Det._AcceptedQty=Convert.ToInt32(ds.Tables[0].Rows[i]["AcceptedQty"].ToString());
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