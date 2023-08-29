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
    public class StockAdjustment : StockAdjustmentInterface
    {
        private int _StockId;
        private int _LiquorId;
        private int _UserId;
        private DateTime _Date;
        private float _Qty_Bottles;
        private float _Qty_Pegs;
        private double _PegAmount;
        private double _BottleAmount;
        private string _LiquorName;
        private double _SellingPricePerPeg;
        private double _SellingPricePerBottle;
        private double _CurrentStockBottles;
        private double _CurrentStockPegs;
        private string _GrnNo;
        private string _Flag;
        private string _Remarks;

        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        public double SellingPricePerPeg
        {
            get { return _SellingPricePerPeg; }
            set { _SellingPricePerPeg = value; }
        }
        public double SellingPricePerBottle
        {
            get { return _SellingPricePerBottle; }
            set { _SellingPricePerBottle = value; }
        }
        public double CurrentStockBottles
        {
            get { return _CurrentStockBottles; }
            set { _CurrentStockBottles = value; }
        }
        public double CurrentStockPegs
        {
            get { return _CurrentStockPegs; }
            set { _CurrentStockPegs = value; }
        }
        public string LiquorName
        {
            get { return _LiquorName; }
            set { _LiquorName = value; }
        }
        public int StockId
        {
            get { return _StockId; }
            set { _StockId = value; }
        }
        public int LiquorId
        {
            get { return _LiquorId; }
            set { _LiquorId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public float Qty_Bottles
        {
            get { return _Qty_Bottles; }
            set { _Qty_Bottles = value; }
        }
        public float Qty_Pegs
        {
            get { return _Qty_Pegs; }
            set { _Qty_Pegs = value; }
        }
        public double PegAmount
        {
            get { return _PegAmount; }
            set { _PegAmount = value; }
        }
        public double BottleAmount
        {
            get { return _BottleAmount; }
            set { _BottleAmount = value; }
        }

        public string GrnNo
        {
            get { return _GrnNo; }
            set { _GrnNo = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        DBConnection db = new DBConnection();
        Dictionary<string, object> StockAdjustmentDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            StockAdjustmentDictionary.Add("StockId", _StockId);
            StockAdjustmentDictionary.Add("LiquorId", _LiquorId);
            StockAdjustmentDictionary.Add("UserId", _UserId);
            StockAdjustmentDictionary.Add("Date", _Date);
            StockAdjustmentDictionary.Add("Qty_Bottles", _Qty_Bottles);
            StockAdjustmentDictionary.Add("Qty_Pegs", _Qty_Pegs);
            StockAdjustmentDictionary.Add("PegAmount", _PegAmount);
            StockAdjustmentDictionary.Add("BottleAmount", _BottleAmount);
            StockAdjustmentDictionary.Add("LiquorName", _LiquorName);
            StockAdjustmentDictionary.Add("GrnNo", _GrnNo);
            StockAdjustmentDictionary.Add("CurrentStockBottles", _CurrentStockBottles);
            StockAdjustmentDictionary.Add("CurrentStockPegs", _CurrentStockPegs);
            StockAdjustmentDictionary.Add("Remarks", _Remarks);

            return "Success";
        }


        public int AddStockAdjustmentDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        { 
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            StockAdjustmentDictionary = StockAdjustmentIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertAllStockAdjustment";
            //cmd.Parameters.AddWithValue("@StockId", StockAdjustmentIDictionary["StockId"]);
            cmd.Parameters.AddWithValue("@LiquorId", StockAdjustmentIDictionary["LiquorId"]);
            cmd.Parameters.AddWithValue("@UserId", StockAdjustmentIDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@Date", StockAdjustmentIDictionary["Date"]);
            cmd.Parameters.AddWithValue("@Qty_Bottles", StockAdjustmentIDictionary["Qty_Bottles"]);
            cmd.Parameters.AddWithValue("@Qty_Pegs", StockAdjustmentIDictionary["Qty_Pegs"]);
            cmd.Parameters.AddWithValue("@PegAmount", StockAdjustmentIDictionary["PegAmount"]);
            cmd.Parameters.AddWithValue("@BottleAmount", StockAdjustmentIDictionary["BottleAmount"]);
            cmd.Parameters.AddWithValue("@LiquorName", StockAdjustmentIDictionary["LiquorName"]);
            cmd.Parameters.AddWithValue("@GrnNo", StockAdjustmentIDictionary["GrnNo"]);
            cmd.Parameters.AddWithValue("@CurrentStockBottles", StockAdjustmentIDictionary["CurrentStockBottles"]);
            cmd.Parameters.AddWithValue("@CurrentStockPegs", StockAdjustmentIDictionary["CurrentStockPegs"]);
            cmd.Parameters.AddWithValue("@Flag", StockAdjustmentIDictionary["Flag"]);
            cmd.Parameters.AddWithValue("@Remarks", StockAdjustmentIDictionary["Remarks"]);
            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    SqlParameter Param = new SqlParameter("@StockId", 0);
                    Param.Direction=ParameterDirection.Output;
                    cmd.Parameters.Add(Param);
                    response=cmd.ExecuteNonQuery();
                    int StockId = Convert.ToInt32(Param.Value);
                    response=StockId;
                    //Check for errors using try catch 
                    // response = cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    response=StockId;
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }

        //public List<StockAdjustment> ViewAllStockAdjustmentDetails()
        //{
        //    string response = string.Empty;
        //    DataSet ds = new DataSet();
        //    List<StockAdjustment> list = new List<StockAdjustment>();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "sp_ViewAllStockAdjustment";
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
        //                StockAdjustment Det = new StockAdjustment();
        //                Det._StockId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockId"].ToString());
        //                //Det._LiquorId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"].ToString());
        //                //Det._UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"].ToString());
        //                //Det._Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());
        //                //Det._Qty_Bottles = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty_Bottles"].ToString());
        //                //Det._Qty_Pegs = Convert.ToInt32(ds.Tables[0].Rows[i]["Qty_Pegs "].ToString());
        //                Det._PegAmount = Convert.ToDouble(ds.Tables[0].Rows[i]["PegAmount"].ToString());
        //                Det._BottleAmount = Convert.ToDouble(ds.Tables[0].Rows[i]["BottleAmount"].ToString());
        //                Det._LiquorName = ds.Tables[0].Rows[i]["LiquorName"].ToString();
        //                Det._GrnNo = Convert.ToInt32(ds.Tables[0].Rows[i]["GrnNo"].ToString());
        //                Det._CurrentStockBottles = Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockBottles"].ToString());
        //                Det._CurrentStockPegs = Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockPegs"].ToString());
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

        public List<StockAdjustment> ViewAllStockAdjustmentDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<StockAdjustment> list = new List<StockAdjustment>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_ViewAllStockAdjustment";
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
                            StockAdjustment Det = new StockAdjustment();
                            Det._StockId=Convert.ToInt32(ds.Tables[0].Rows[i]["StockId"].ToString());
                            Det._LiquorId=Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"].ToString());
                            Det._UserId=Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"].ToString());
                            Det._Date=Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"].ToString());
                            Det._Qty_Bottles=float.Parse(ds.Tables[0].Rows[i]["Qty_Bottles"].ToString());
                            Det._Qty_Pegs=float.Parse(ds.Tables[0].Rows[i]["Qty_Pegs"].ToString());
                            Det._PegAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["PegAmount"].ToString());
                            Det._BottleAmount=Convert.ToDouble(ds.Tables[0].Rows[i]["BottleAmount"].ToString());
                            Det._LiquorName=Convert.ToString(ds.Tables[0].Rows[i]["LiquorName"].ToString());
                            Det._GrnNo=Convert.ToString(ds.Tables[0].Rows[i]["GrnNo"].ToString());
                            Det._Flag=Convert.ToString(ds.Tables[0].Rows[i]["Add_Sub"].ToString());
                            Det._Remarks=Convert.ToString(ds.Tables[0].Rows[i]["Remarks"].ToString());
                            //Det._CurrentStockBottles = Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockBottles"].ToString());
                            //Det._CurrentStockPegs = Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockPegs"].ToString());
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


        public DataSet GetLiquorStockDetails(int LiquorId)
        {
            string response = string.Empty;
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetAllStockAdjustment";
            cmd.Parameters.AddWithValue("@LiquorId", LiquorId);
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


        public string UpdateStockDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        {
            string response;
            StockAdjustmentDictionary = StockAdjustmentIDictionary;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "";
            cmd.Parameters.AddWithValue("@StockId", StockAdjustmentDictionary["StockId"]);
            cmd.Parameters.AddWithValue("@LiquorId", StockAdjustmentDictionary["LiquorId"]);
            cmd.Parameters.AddWithValue("@UserId", StockAdjustmentDictionary["UserId"]);
            cmd.Parameters.AddWithValue("@Date", StockAdjustmentDictionary["Date"]);
            cmd.Parameters.AddWithValue("@Qty_Bottles", StockAdjustmentDictionary["Qty_Bottles"]);
            cmd.Parameters.AddWithValue("@Qty_Pegs", StockAdjustmentDictionary["Qty_Pegs"]);
            cmd.Parameters.AddWithValue("@PegAmount", StockAdjustmentDictionary["PegAmount"]);
            cmd.Parameters.AddWithValue("@BottleAmount", StockAdjustmentDictionary["BottleAmount"]);
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


        //public int AddStockAdjustmentDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        public Dictionary<string, object> SelectStockAdjustment(int StockId)
        {
            throw new NotImplementedException();
        }

        //public string UpdateStockDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //int StockAdjustmentInterface.GetLiquorStockDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //List<StockAdjustment> StockAdjustmentInterface.GetLiquorStockDetails(int LiquorId)
        //{
        //    throw new NotImplementedException();
        //}

        //public int GetLiquorStockDetails(Dictionary<string, object> StockAdjustmentIDictionary)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<StockAdjustment> ViewAllStockAdjustmentDetails()
        //{
        //    throw new NotImplementedException();
        //}
    }
}