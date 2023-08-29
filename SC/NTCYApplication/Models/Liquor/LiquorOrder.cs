using NTCYApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NTCYApplication.Models.Liquor
{
    public class LiquorOrder : LiquorOrderInterface
    {
        private int _OrderId;
        private int _UserId;
        private string _MembershipNo; 
        private string _Status; 
        private double _TotalGST;
        private int _TableNo;
        private string _WaiterName;
        private double _TotalAmount;
        private double _GrossAmount;
        private string _LiquorName;
        private float _Quantity;
        private string _Price;
        private float _GST;
        private DateTime _Date;
        string _Served; 
        int _SubOrderId;
        string _PaymentMode;

        
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
        List<LiquorOrderList> liquorOrderList { get; set; }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public double TotalGST
        {
            get { return _TotalGST; }
            set { _TotalGST = value; }
        }
        public int TableNo
        {
            get { return _TableNo; }
            set { _TableNo = value; }
        }
        public string WaiterName
        {
            get { return _WaiterName; }
            set { _WaiterName = value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }

        public double GrossAmount
        {
            get { return _GrossAmount; }
            set { _GrossAmount = value; }
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
        public float GST
        {
            get { return _GST; }
            set { _GST = value; }
        }
        public string Served
        {
            get { return _Served; }
            set { _Served=value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date=value; }
        }

        public int SubOrderId 
        {
            get { return _SubOrderId; }
            set { _SubOrderId=value; }
        }
        public string PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode=value; }
        }

        DBConnection db = new DBConnection();
        Dictionary<string, object> LiquorOrderDictionary = new Dictionary<string, object>();
        string BindDictionary()
        {
            LiquorOrderDictionary.Add("OrderId", _OrderId);
            LiquorOrderDictionary.Add("UserId", _UserId);
            LiquorOrderDictionary.Add("MembershipNo", _MembershipNo);
            LiquorOrderDictionary.Add("TotalGST", _TotalGST);
            LiquorOrderDictionary.Add("liquorOrderList", liquorOrderList);
            LiquorOrderDictionary.Add("GST", _TotalGST);
            LiquorOrderDictionary.Add("Quantity", _Quantity);
            LiquorOrderDictionary.Add("Status", _Status);
            LiquorOrderDictionary.Add("TableNo", _TableNo);
            LiquorOrderDictionary.Add("WaiterName", _WaiterName);
            LiquorOrderDictionary.Add("TotalAmount", _TotalAmount);
            LiquorOrderDictionary.Add("GrossAmount", _GrossAmount);
            LiquorOrderDictionary.Add("Served", _Served);
            return "Success";
        }

       
        public List<LiquorOrder> TakeLiquorOrder()
        {
            throw new NotImplementedException();
        }

        public int CreateLiquorOrderDetails(Dictionary<string, object> LiquorOrderIDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            LiquorOrderDictionary = LiquorOrderIDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertAllLiquororder";
            cmd.Parameters.AddWithValue("@UserName", LiquorOrderDictionary["UserName"]);
            cmd.Parameters.AddWithValue("@MembershipNo", LiquorOrderDictionary["MembershipNo"]);
            cmd.Parameters.AddWithValue("@TotalAmount", LiquorOrderDictionary["TotalAmount"]);
            cmd.Parameters.AddWithValue("@TotalGST", LiquorOrderDictionary["TotalGST"]);
            cmd.Parameters.AddWithValue("@TableNo", LiquorOrderDictionary["TableNo"]);
            cmd.Parameters.AddWithValue("@WaiterName", LiquorOrderDictionary["WaiterName"]);
            cmd.Parameters.AddWithValue("@Status","UnPaid");
            cmd.Parameters.AddWithValue("@GrossAmount", LiquorOrderDictionary["GrossAmount"]);


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
                    //response = Convert.ToInt32(e);
                }
                finally
                {
                    db.CloseConnection();
                }
                return response;
            }
        }



        public string TakeOrder(Dictionary<string, object> FoodIDictionary)
        {
            throw new NotImplementedException();
        }

        public string UpdateLiquorOrderDetails(Dictionary<string, object> FoodIDictionary)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> SelectLiquorOrder(int FoodId)
        {
            throw new NotImplementedException();
        }

        public string DeleteLiquorOrder(int FoodId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Liquor>> ViewLiquorDetails()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            Dictionary<string, List<Liquor>> MyLists = new Dictionary<string, List<Liquor>>();
            SqlCommand cmd = new SqlCommand();
            List<Liquor> ListLiquor = new List<Liquor>();
            List<Liquor> ListLiquorM = new List<Liquor>();
        

            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetAllLiquorDetails";
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
                            if (Convert.ToString(ds.Tables[0].Rows[i]["CurrentStockBottles"])==null||Convert.ToString(ds.Tables[0].Rows[i]["CurrentStockBottles"])=="")
                            { Det.CurrentStockBottles=0; }
                            else
                            {
                                Det.CurrentStockBottles=Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockBottles"]);
                            }
                            if (Convert.ToString(ds.Tables[0].Rows[i]["CurrentStockPegs"])==null||Convert.ToString(ds.Tables[0].Rows[i]["CurrentStockPegs"])=="")
                            {
                                Det.CurrentStockPegs=0;
                            }
                            else
                            {
                                Det.CurrentStockPegs=Convert.ToDouble(ds.Tables[0].Rows[i]["CurrentStockPegs"]);
                            }
                            if (Convert.ToString(ds.Tables[0].Rows[i]["LiquorId"])==null||Convert.ToString(ds.Tables[0].Rows[i]["LiquorId"])=="")
                            {
                                Det.LiquorId=0;
                            }
                            else
                            {
                                Det.LiquorId=Convert.ToInt32(ds.Tables[0].Rows[i]["LiquorId"]);
                            }
                            Det.LiquorName=ds.Tables[0].Rows[i]["LiquorName"].ToString();
                            Det.LiquorCategoryId=Convert.ToInt32(ds.Tables[0].Rows[i]["Liquor_Cat_Id"]);
                            Det.PegorBottle=ds.Tables[0].Rows[i]["PegOrBottle"].ToString();
                            Det.SellingPricePeg=Convert.ToInt32(ds.Tables[0].Rows[i]["SellingPricePerPeg"]);
                            Det.SellingPriceBottle=Convert.ToDouble(ds.Tables[0].Rows[i]["SellingPricePerBottle"].ToString());
                            Det.GST=Convert.ToInt32(ds.Tables[0].Rows[i]["GST_Rate"]);
                            ListLiquor.Add(Det);
                        }
                    }
                    for (int i = 0; i<ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows.Count>0)
                        {
                            Liquor Det = new Liquor();
                            Det.LiquorName=Convert.ToString(ds.Tables[1].Rows[i]["LiquorName"]);
                            Det.LiquorCategoryId=Convert.ToInt32(ds.Tables[1].Rows[i]["Liquor_Cat_Id"].ToString());
                            Det.SellingPriceBottle=Convert.ToDouble(ds.Tables[1].Rows[i]["SellingPricePerBottle"].ToString());
                            Det.LiquorId=Convert.ToInt32(ds.Tables[1].Rows[i]["LiquorId"].ToString());
                            Det.GST=Convert.ToDouble(ds.Tables[1].Rows[i]["GST_Rate"]);
                            Det.CurrentStockBottles=Convert.ToDouble(ds.Tables[1].Rows[i]["CurrentStockBottles"]);
                            ListLiquorM.Add(Det);
                        }
                    }
                    MyLists.Add("ListLiquorM", ListLiquorM);
                    MyLists.Add("ListLiquor", ListLiquor);


                }
                catch (SqlException e)
                {
                    response=e.ToString();

                }
                finally
                {
                    db.CloseConnection();
                }
                return MyLists;
            }
        }

        public List<LiquorOrder> DisplayCompletedOrders()
        {
            string response = string.Empty;
            DataSet ds = new DataSet();
            List<LiquorOrder> list = new List<LiquorOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDisplayCompletedLiquorOrders";
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
                            LiquorOrder Lo = new LiquorOrder();
                            Lo._OrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["OrderNumber"]);
                            Lo._MembershipNo=Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Lo._TableNo=Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"].ToString());
                            Lo._WaiterName = Convert.ToString(ds.Tables[0].Rows[i]["WaiterName"].ToString());
                            Lo._LiquorName=Convert.ToString(ds.Tables[0].Rows[i]["LiquorName"].ToString());
                            Lo._Quantity=float.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                            Lo._Served=ds.Tables[0].Rows[i]["ServedStatus"].ToString();
                            Lo._SubOrderId=Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            list.Add(Lo);
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

        public int ServedStatus(Dictionary<string, object> LiquorServedDictionary)
        {
            int response = 0;
            SqlCommand cmd = new SqlCommand();
            LiquorOrderDictionary=LiquorServedDictionary;
            cmd.Parameters.Clear();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="spUpdateServedStatus";
            cmd.Parameters.AddWithValue("@SubOrderId", LiquorOrderDictionary["SubOrderId"]);
            //cmd.Parameters.AddWithValue("@MembershipNo", LiquorOrderDictionary["MembershipNo"]);

            using (SqlConnection MyCon = db.OpenConnection())
            {
                cmd.Connection=MyCon;
                try
                {
                    response=cmd.ExecuteNonQuery();

                }
                catch (SqlException e)
                {
                    response=Convert.ToInt32(e);
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