using Microsoft.Data.SqlClient;
using NTCY.Models.StockDetails;
using NTCY.Models;
using System.Data;
using NTCY.Models.Foods;

namespace NTCY.Services.StockDetails
{
    public class StockService : IStockService
    {
        public int AddStockInwardDetails(StockInward stockInward)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            int iGrnNo = 0;

            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand("spInsertStockInward", MyCon);
                scCommand.Parameters.Clear();
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.Parameters.AddWithValue("@GrnNo", stockInward.GrnNo);
                scCommand.Parameters.AddWithValue("GrnDate", stockInward.GrnDate);
                scCommand.Parameters.AddWithValue("@PurchaseOrderNo", stockInward.PurchaseOrderNo);
                scCommand.Parameters.AddWithValue("@PermitNo", stockInward.PermitNo);
                scCommand.Parameters.AddWithValue("@PurchaseOrderDate", stockInward.PurchaseOrderDate);
                scCommand.Parameters.AddWithValue("@DeliveryChallanNo", stockInward.DeliveryChallanNo);
                scCommand.Parameters.AddWithValue("@Deliverydate", stockInward.Deliverydate);
                scCommand.Parameters.AddWithValue("@Supplier", stockInward.Supplier);
                scCommand.Parameters.AddWithValue("@TotalAmount", stockInward.TotalAmount);
                scCommand.Parameters.AddWithValue("@TotalTax", stockInward.TotalTax);
                scCommand.Parameters.AddWithValue("@TotalDiscount", stockInward.TotalDiscount);
                scCommand.Parameters.AddWithValue("@NetAmount", stockInward.NetAmount);
                scCommand.Parameters.AddWithValue("@UserId", stockInward.UserId);
                scCommand.Parameters.AddWithValue("@Remarks", stockInward.Remarks);

                SqlParameter Param = new SqlParameter("@GrnId", 0);
                Param.Direction = ParameterDirection.Output;
                scCommand.Parameters.Add(Param);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    iGrnNo = scCommand.ExecuteNonQuery();
                    iGrnNo = Convert.ToInt32(Param.Value);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
            return iGrnNo;
        }

        public void AddStockInwardSubDetails(Dictionary<string, object> stockInwardSub, int iGrnNo)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            ClubCommitteeDet Det = new ClubCommitteeDet();

            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand();
                scCommand.Parameters.Clear();
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandText = "spInsertStockInwardSub";
                scCommand.Parameters.AddWithValue("@GrnId", iGrnNo);
                scCommand.Parameters.AddWithValue("@LiquorId", stockInwardSub["LiquorId"]);
                scCommand.Parameters.AddWithValue("@PurchaseOrderRate", stockInwardSub["PurchaseOrderRate"]);
                scCommand.Parameters.AddWithValue("@PurchaseOrderQty", stockInwardSub["PurchaseOrderQty"]);
                scCommand.Parameters.AddWithValue("@MRP", stockInwardSub["MRP"]);
                scCommand.Parameters.AddWithValue("@TaxAmount", stockInwardSub["TaxAmount"]);
                scCommand.Parameters.AddWithValue("@TaxPercentage", stockInwardSub["TaxPercentage"]);
                scCommand.Parameters.AddWithValue("@DiscountAmount", stockInwardSub["DiscountAmount"]);
                scCommand.Parameters.AddWithValue("@DiscountPercentage", stockInwardSub["DiscountPercentage"]);
                scCommand.Parameters.AddWithValue("@RejectedQty", stockInwardSub["RejectedQty"]);
                scCommand.Parameters.AddWithValue("@AcceptedQty", stockInwardSub["AcceptedQty"]);
                scCommand.Parameters.AddWithValue("@RejectedRemarks", stockInwardSub["RejectedRemarks"]);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    scCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
        }

        public void AddStockAdjustmentDetails(Dictionary<string, object> stockAdjustment)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            ClubCommitteeDet Det = new ClubCommitteeDet();

            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                SqlCommand scCommand = new SqlCommand();
                scCommand.Parameters.Clear();
                scCommand.CommandType = CommandType.StoredProcedure;
                scCommand.CommandText = "spInsertStockAdjustment";
                scCommand.Parameters.AddWithValue("@LiquorId", stockAdjustment["LiquorId"]);
                scCommand.Parameters.AddWithValue("@LiquorName", stockAdjustment["LiquorName"]);
                scCommand.Parameters.AddWithValue("@UserId", stockAdjustment["UserId"]);
                scCommand.Parameters.AddWithValue("@GrnNo", stockAdjustment["GrnNo"]);
                scCommand.Parameters.AddWithValue("@Date", stockAdjustment["Date"]);
                scCommand.Parameters.AddWithValue("@CurrentStockBottles", stockAdjustment["CurrentStockBottles"]);
                scCommand.Parameters.AddWithValue("@CurrentStockPegs", stockAdjustment["CurrentStockPegs"]);
                scCommand.Parameters.AddWithValue("@Qty_Bottles", stockAdjustment["Qty_Bottles"]);
                scCommand.Parameters.AddWithValue("@Qty_Pegs", stockAdjustment["Qty_Pegs"]);
                scCommand.Parameters.AddWithValue("@Add_Sub", stockAdjustment["Add_Sub"]);
                scCommand.Parameters.AddWithValue("@PegAmount", stockAdjustment["PegAmount"]);
                scCommand.Parameters.AddWithValue("@BottleAmount", stockAdjustment["BottleAmount"]);
                scCommand.Parameters.AddWithValue("@Remarks", stockAdjustment["Remarks"]);

                try
                {
                    if (scCommand.Connection.State == ConnectionState.Closed)
                    {
                        scCommand.Connection.Open();
                    }
                    scCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    scCommand.Connection.Close();
                }
            }
        }

        public string GetGrnNo()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            string sGrnNo = "";

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FoodOrder> list = new List<FoodOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetGrnNo";
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    MyCon.Open();
                    sGrnNo = cmd.ExecuteScalar().ToString();
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return sGrnNo;
            }
        }
    }
}
