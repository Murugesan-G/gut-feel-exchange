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
    public class StockInward : StockInwardInterface

    {
        private int _GrnId;
        private string _GrnNo;
        private DateTime _GrnDate;
        private string _PurchaseOrder;
        private DateTime _PurchaseOrderDate;
        private string _DeliveryChallan;
        private DateTime _Deliverydate;
        private string _Supplier;
        private double _TotalAmount;
        private double _TotalTax;
        private double _TotalDiscount;
        private double _NetAmount;
        // private int _StockSubId;
        //private double _PurchaseOrderRate;
        //private double _PurchaseOrderQty;
        //private double _MRP;
        //private double _TaxAmount;
        //private double _TaxPercentage;
        //private double _DiscountAmount;
        //private double _DiscountPercentage;
        //private int _RejectedQty;
        //private int _AcceptedQty;

        //public int AcceptedQty 
        //{
        //    get { return _AcceptedQty; }
        //    set { _AcceptedQty=value; }
        //}
        //public int StockSubId
        //{
        //    get { return _StockSubId; }
        //    set { _StockSubId = value; }
        //}
        //public double PurchaseOrderRate
        //{
        //    get { return _PurchaseOrderRate; }
        //    set { _PurchaseOrderRate = value; }
        //}

        //public double DiscountAmount
        //{
        //    get { return _DiscountAmount; }
        //    set { _DiscountAmount = value; }
        //}
        //public double PurchaseOrderQty
        //{
        //    get { return _PurchaseOrderQty; }
        //    set { _PurchaseOrderQty = value; }
        //}
        //public double MRP
        //{
        //    get { return _MRP; }
        //    set { _MRP = value; }
        //}
        //public double TaxAmount
        //{
        //    get { return _TaxAmount; }
        //    set { _TaxAmount = value; }
        //}
        //public double TaxPercentage
        //{
        //    get { return _TaxPercentage; }
        //    set { _TaxPercentage = value; }
        //}
        //public double DiscountPercentage
        //{
        //    get { return _DiscountPercentage; }
        //    set { _DiscountPercentage = value; }
        //}
        //public int RejectedQty
        //{
        //    get { return _RejectedQty; }
        //    set { _RejectedQty = value; }
        //}
        public int GrnId
        {
            get { return _GrnId; }
            set { _GrnId=value; }
        }
        public string GrnNo
        {
            get { return _GrnNo; }
            set { _GrnNo=value; }
        }
        public DateTime GrnDate
        {
            get { return _GrnDate; }
            set { _GrnDate=value; }
        }
        public string PurchaseOrder
        {
            get { return _PurchaseOrder; }
            set { _PurchaseOrder=value; }
        }
        public DateTime PurchaseOrderDate
        {
            get { return _PurchaseOrderDate; }
            set { _PurchaseOrderDate=value; }
        }
        public string DeliveryChallan
        {
            get { return _DeliveryChallan; }
            set { _DeliveryChallan=value; }
        }
        public DateTime Deliverydate
        {
            get { return _Deliverydate; }
            set { _Deliverydate=value; }
        }
        public string Supplier
        {
            get { return _Supplier; }
            set { _Supplier=value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount=value; }
        }
        public double TotalTax
        {
            get { return _TotalTax; }
            set { _TotalTax=value; }
        }
        public double TotalDiscount
        {
            get { return _TotalDiscount; }
            set { _TotalDiscount=value; }
        }
        public double NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount=value; }
        }

        DBConnection db = new DBConnection();
        Dictionary<string, object> StockInwardDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            StockInwardDictionary.Add("GrnId", _GrnId);
            StockInwardDictionary.Add("GrnNo", _GrnNo);
            StockInwardDictionary.Add("GrnDate", _GrnDate);
            StockInwardDictionary.Add("PurchaseOrder", _PurchaseOrder);
            StockInwardDictionary.Add("PurchaseOrderDate", _PurchaseOrderDate);
            StockInwardDictionary.Add("DeliveryChallan", _DeliveryChallan);
            StockInwardDictionary.Add("Deliverydate", _Deliverydate);
            StockInwardDictionary.Add("Supplier", _Supplier);
            StockInwardDictionary.Add("TotalAmount", _TotalAmount);
            StockInwardDictionary.Add("TotalTax", _TotalTax);
            StockInwardDictionary.Add("TotalDiscount", _TotalDiscount);
            StockInwardDictionary.Add("NetAmount", _NetAmount);

            return "Success";
        }

        public int AddStockDetails(Dictionary<string, object> StockInwardIDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            StockInwardDictionary=StockInwardIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spInsertRecipt";
            //cmd.Parameters.AddWithValue("@GrnId", StockInwardIDictionary["GrnId"]);
            cmd.Parameters.AddWithValue("@GrnNo", StockInwardIDictionary["GrnNo"]);
            DateTime grnDate = Convert.ToDateTime(StockInwardIDictionary["GrnDate"]);
            cmd.Parameters.AddWithValue("@GrnDate", grnDate);
            cmd.Parameters.AddWithValue("@PurchaseOrder", StockInwardIDictionary["PurchaseOrder"]);
            DateTime PurchaseOrderDate = Convert.ToDateTime(StockInwardIDictionary["PurchaseOrderDate"]);
            cmd.Parameters.AddWithValue("@PurchaseOrderDate", PurchaseOrderDate);
            cmd.Parameters.AddWithValue("@DeliveryChallan", StockInwardIDictionary["DeliveryChallan"]);

            DateTime Deliverydate = Convert.ToDateTime(StockInwardIDictionary["Deliverydate"]);
            cmd.Parameters.AddWithValue("@Deliverydate", Deliverydate);
            cmd.Parameters.AddWithValue("@Supplier", StockInwardIDictionary["Supplier"]);
            cmd.Parameters.AddWithValue("@TotalAmount", StockInwardIDictionary["TotalAmount"]);
            cmd.Parameters.AddWithValue("@TotalTax", StockInwardIDictionary["TotalTax"]);
            cmd.Parameters.AddWithValue("@TotalDiscount", StockInwardIDictionary["TotalDiscount"]);
            cmd.Parameters.AddWithValue("@NetAmount", StockInwardIDictionary["NetAmount"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlParameter Param = new SqlParameter("@GrnId", 0);
                    Param.Direction=ParameterDirection.Output;
                    cmd.Parameters.Add(Param);
                    response=cmd.ExecuteNonQuery();
                    int GrnId = Convert.ToInt32(Param.Value);
                    response=GrnId;
                    //Check for errors using try catch 
                    // response = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    response=GrnId;
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }


        public string UpdateStockDetails(Dictionary<string, object> StockInwardIDictionary)
        {
            string response;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="";
            cmd.Parameters.AddWithValue("@GrnId", StockInwardIDictionary["GrnId"]);
            cmd.Parameters.AddWithValue("@GrnNo", StockInwardIDictionary["GrnNo"]);
            cmd.Parameters.AddWithValue("@GrnDate", StockInwardIDictionary["GrnDate"]);
            cmd.Parameters.AddWithValue("@PurchaseOrder", StockInwardIDictionary["PurchaseOrder"]);
            cmd.Parameters.AddWithValue("@PurchaseOrderDate", StockInwardIDictionary["PurchaseOrderDate"]);
            cmd.Parameters.AddWithValue("@DeliveryChallan", StockInwardIDictionary["DeliveryChallan"]);
            cmd.Parameters.AddWithValue("@Deliverydate", StockInwardIDictionary["Deliverydate"]);
            cmd.Parameters.AddWithValue("@Supplier", StockInwardIDictionary["Supplier"]);
            cmd.Parameters.AddWithValue("@TotalAmount", StockInwardIDictionary["TotalAmount"]);
            cmd.Parameters.AddWithValue("@TotalTax", StockInwardIDictionary["TotalTax"]);
            cmd.Parameters.AddWithValue("@TotalDiscount", StockInwardIDictionary["TotalDiscount"]);
            cmd.Parameters.AddWithValue("@NetAmount", StockInwardIDictionary["NetAmount"]);

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

        public Dictionary<string, object> SelectStock(int GrnId)
        {
            throw new NotImplementedException();
        }

        public List<StockInward> ViewAllStockDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<StockInward> list = new List<StockInward>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_ViewAllRecieptDetails";
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
                            StockInward Det = new StockInward();
                            Det._GrnId=Convert.ToInt32(ds.Tables[0].Rows[i]["GrnId"].ToString());
                            Det._GrnNo=ds.Tables[0].Rows[i]["GrnNo"].ToString();
                            Det._GrnDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["GrnDate"].ToString());
                            Det._PurchaseOrder=ds.Tables[0].Rows[i]["PurchaseOrderNo"].ToString();
                            Det._PurchaseOrderDate=Convert.ToDateTime(ds.Tables[0].Rows[i]["PurchaseOrderDate"].ToString());
                            Det._DeliveryChallan=ds.Tables[0].Rows[i]["DeliveryChallanNo"].ToString();
                            Det._Deliverydate=Convert.ToDateTime(ds.Tables[0].Rows[i]["Deliverydate"].ToString());
                            Det._Supplier=ds.Tables[0].Rows[i]["Supplier"].ToString();
                            Det._TotalAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalAmount"].ToString());
                            Det._TotalTax=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalTax"].ToString());
                            Det._TotalDiscount=Convert.ToDouble(ds.Tables[0].Rows[i]["TotalDiscount"].ToString());
                            Det._NetAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["NetAmount"].ToString());
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

        //public List<StockInward> ViewDetailStock(int GrnId)
        //{
        //    string response = string.Empty;
        //    DataSet ds = new DataSet();
        //    List<StockInward> list = new List<StockInward>();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "sp_ViewDetailStock";
        //    cmd.Parameters.AddWithValue("@GrnId", GrnId);
        //    try
        //    {
        //        SqlConnection MyCon = db.OpenConnection();
        //        cmd.Connection = MyCon;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                StockInward Det = new StockInward();
        //                Det._StockSubId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockSubId"].ToString());
        //                Det._GrnId = Convert.ToInt32(ds.Tables[0].Rows[i]["GrnId"].ToString());                     
        //                Det._PurchaseOrderRate =Convert.ToDouble(ds.Tables[0].Rows[i]["PurchaseOrderRate"].ToString());
        //                Det._PurchaseOrderQty = Convert.ToDouble(ds.Tables[0].Rows[i]["PurchaseOrderQty"].ToString());                  
        //                Det._MRP = Convert.ToDouble(ds.Tables[0].Rows[i]["MRP"].ToString());
        //                Det._TaxAmount = Convert.ToDouble(ds.Tables[0].Rows[i]["TaxAmount"].ToString());
        //                Det._TaxPercentage = Convert.ToDouble(ds.Tables[0].Rows[i]["TaxPercentage"].ToString());
        //                Det._DiscountAmount = Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountAmount"].ToString());
        //                Det._DiscountPercentage = Convert.ToDouble(ds.Tables[0].Rows[i]["DiscountPercentage"].ToString());
        //                Det._RejectedQty = Convert.ToInt32(ds.Tables[0].Rows[i]["RejectedQty"].ToString());
        //                Det._AcceptedQty=Convert.ToInt32(ds.Tables[0].Rows[i]["AcceptedQty"].ToString());
        //                list.Add(Det);
        //            }
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        response = e.ToString();
        //    }
        //    db.CloseConnection();
        //    return list;
        //}

        public int DeleteStockDetail(int GrnId)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spDeleteStockDetail";
            cmd.Parameters.AddWithValue("@GrnId", GrnId);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    // response = e.ToString();
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        public DataSet GetStockInwards(string Prefix, string LName)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="sp_GetStockInwards";
            cmd.Parameters.AddWithValue("@Prefix", Prefix);
            cmd.Parameters.AddWithValue("@LName", LName);
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

        public string GetGrnNo()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spGetGrnNo";
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {

                    response=cmd.ExecuteScalar().ToString();
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

        //public DataSet GetStockInwards(string Prefix)
        //{
        //    throw new NotImplementedException();
        //}


        //public string UpdateStockDetails(Dictionary<string, object> StockInwardIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //string StockInwardInterface.AddStockDetails(Dictionary<string, object> StockInwardIDictionary)
        //{
        //    throw new NotImplementedException();
        //}
    }
}