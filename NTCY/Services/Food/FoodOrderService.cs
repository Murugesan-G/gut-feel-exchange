using Microsoft.Data.SqlClient;
using NTCY.Models.Foods;
using System.Collections.Generic;
using System.Data;

namespace NTCY.Services.FoodOrderS
{
    public class FoodOrderService : IFoodOrderService
    {
        public List<Models.Foods.Food> ViewAllFoodDetails(string type)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");
            DataSet ds = new DataSet();
            List<Models.Foods.Food> list = new List<Models.Foods.Food>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spGetFoodDetailsByType";
            cmd.Parameters.AddWithValue("@Type", type);
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Models.Foods.Food Det = new Models.Foods.Food();
                            Det.FoodId = Convert.ToInt32(ds.Tables[0].Rows[i]["FoodId"]);
                            Det.FoodCode = ds.Tables[0].Rows[i]["FoodCode"].ToString();
                            Det.FoodName = ds.Tables[0].Rows[i]["FoodName"].ToString();
                            Det.Category = ds.Tables[0].Rows[i]["Category"].ToString();
                            Det.FoodDescription = ds.Tables[0].Rows[i]["FoodDescription"].ToString();
                            Det.Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
                            Det.Price = Convert.ToDouble(ds.Tables[0].Rows[i]["Price"].ToString());
                            Det.GST = Convert.ToDouble(ds.Tables[0].Rows[i]["GST"].ToString());
                            Det.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    //return list;

                }
                finally
                {
                    //
                }
            }
            return list;
        }

        public List<FoodOrder> ViewAllFoodOrder()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FoodOrder> list = new List<FoodOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetAllFoodOrderList";
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            FoodOrder Det = new FoodOrder();
                            Det.OrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det.SubOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                            Det.MembershipNo = Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det.TableNo = Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"]);
                            Det.MemberName = Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det.FoodName = Convert.ToString(ds.Tables[0].Rows[i]["FoodName"]);
                            Det.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return list;
            }
        }

        public List<FoodOrder> ViewCompletedOrders()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            string cnn = configuration.GetConnectionString("WebApiDatabase");

            string response = string.Empty;
            DataSet ds = new DataSet();
            List<FoodOrder> list = new List<FoodOrder>();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spViewCompletedFoodOrder";
            using (SqlConnection MyCon = new SqlConnection(cnn))
            {
                cmd.Connection = MyCon;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            FoodOrder Det = new FoodOrder();
                            Det.OrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["OrderId"]);
                            Det.SubOrderId = Convert.ToInt32(ds.Tables[0].Rows[i]["SubOrderId"]);
                            Det.UserName = Convert.ToString(ds.Tables[0].Rows[i]["UserName"]);
                            Det.MembershipNo = Convert.ToString(ds.Tables[0].Rows[i]["MembershipNo"]);
                            Det.TableNo = Convert.ToInt32(ds.Tables[0].Rows[i]["TableNo"]);
                            Det.MemberName = Convert.ToString(ds.Tables[0].Rows[i]["MemberName"]);
                            Det.FoodName = Convert.ToString(ds.Tables[0].Rows[i]["FoodName"]);
                            Det.Quantity = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
                            Det.Status = Convert.ToString(ds.Tables[0].Rows[i]["Status"]);
                            list.Add(Det);
                        }
                    }
                }
                catch (SqlException e)
                {
                    response = e.ToString();
                }
                finally
                {
                    //
                }
                return list;
            }
        }
    }
}
